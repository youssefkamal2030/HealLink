using HealLink.Domain.Base;
using HealLink.Domain.DomainEvents;
using HealLink.Domain.Enums;
using HealLink.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealLink.Domain.Entities
{
    
    public class Doctor : AggregateRoot
    {
        public Guid UserId { get; private set; }
        public User? User { get; private set; }
        public Address? Address { get; private set; }

        public PersonalInfo? PersonalInfo { get; private set; }
        public string? SyndicateIdImagePath { get; private set; }
        public string? PracticeLicenseNumber { get; private set; }
        public string? Specialization { get; private set; }
        public string? CurrentWorkplace { get; private set; }

        public bool IsAvailableForChat { get; private set; } = false;
     public QRCode? QRCode { get; private set; }
        public bool IsApproved { get; private set; } = false;
        public DoctorRejection? Rejection { get; private set; }

        private readonly List<Subscription> _subscriptions = new();
        private readonly List<DoctorPatientConnection> _connections = new();
        private readonly List<Notification> _notifications = new();
        public IReadOnlyCollection<Subscription> Subscriptions => _subscriptions;
        public IReadOnlyCollection<DoctorPatientConnection> PatientConnections => _connections;
        public IReadOnlyCollection<Notification> Notifications => _notifications;


        public Doctor(
            Guid userId,
            PersonalInfo? personalInfo = null,
            Address? address = null,
            string? syndicateImagePath = null,
            string? practiceLicenseNumber = null,
            string? specialization = null,
            string? currentWorkplace = null
           )
        {
            UserId = userId;
            PersonalInfo = personalInfo;
            SyndicateIdImagePath = syndicateImagePath;
            PracticeLicenseNumber = practiceLicenseNumber;
            Specialization = specialization;
            CurrentWorkplace = currentWorkplace;
        }

        private Doctor() { }

        /// <summary>
        /// Factory method for initial doctor registration.
        /// Use this instead of the constructor — expresses intent and keeps construction knowledge in the domain.
        /// </summary>
        public static Doctor Register(
            Guid userId,
            string? syndicateImagePath = null,
            string? practiceLicenseNumber = null,
            string? specialization = null)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId cannot be empty.", nameof(userId));

            return new Doctor(userId, null, null, syndicateImagePath, practiceLicenseNumber, specialization, null);
        }

        public void Approve(Guid doctorId)
        {
            IsApproved = true;
            Rejection = null;
            UpdateTimestamp();
            AddDomainEvent( new DoctorApprovedEvent(doctorId) );
        }

        public void Reject(string reason, Guid adminId)
        {
            if (IsApproved)
                throw new InvalidOperationException("Cannot reject an already approved doctor.");

            Rejection = new DoctorRejection(reason, adminId, DateTime.UtcNow);
            UpdateTimestamp();
            AddDomainEvent(new DoctorRejectedEvent(Id, reason));
        }

        public void SetChatAvailability(bool isAvailable)
        {
            IsAvailableForChat = isAvailable;
            UpdateTimestamp();
        }

        public void UpdatePersonalInfo(PersonalInfo? personalInfo)
        {
            PersonalInfo = personalInfo;
            UpdateTimestamp();
        }

        public void UpdateAddress(Address? address)
        {
            Address = address;
            UpdateTimestamp();
        }

        public void UpdateProfessionalDetails(string? specialization, string? currentWorkplace)
        {
            Specialization = specialization;
            CurrentWorkplace = currentWorkplace;
      
            UpdateTimestamp();
        }

        public void GenerateQRCode()
        {
            QRCode = new QRCode(Guid.NewGuid().ToString(), DateTime.UtcNow);
            UpdateTimestamp();
        }

        public bool IsQRCodeValid()
        {
            return QRCode?.IsValid() ?? false;
        }

        public void RefreshQRCodeIfNeeded()
        {
            if (!IsQRCodeValid())
                GenerateQRCode();
        }

     
       
        public void AcceptPatientRequest(Guid connectionId)
        {
            var connection = PatientConnections.FirstOrDefault(c => c.Id == connectionId);
            if (connection == null) throw new InvalidOperationException("Connection not found.");
            if (connection.Status != ConnectionStatus.Pending) throw new InvalidOperationException("Not pending.");

            connection.Accept();
           

            AddDomainEvent(new ConnectionAcceptedEvent(
                connectionId,
                Id,
                connection.PatientId,
                DateTime.UtcNow
            ));
        }

        public void RejectPatientRequest(Guid connectionId)
        {
            var connection = PatientConnections.FirstOrDefault(c => c.Id == connectionId);
            if (connection == null) throw new InvalidOperationException("Connection not found.");
            if (connection.Status != ConnectionStatus.Pending) throw new InvalidOperationException("Not pending.");

            connection.Reject();
            _connections.Remove(connection);

            AddDomainEvent(new ConnectionRejectedEvent(
                connectionId,
                Id,
                connection.PatientId
            ));
        }

        public void AddConnection(DoctorPatientConnection connection)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (PatientConnections.Any(c => c.PatientId == connection.PatientId && c.Status != ConnectionStatus.Rejected))
                throw new InvalidOperationException("Connection already exists or pending.");
            _connections.Add(connection);
        }
    }
}