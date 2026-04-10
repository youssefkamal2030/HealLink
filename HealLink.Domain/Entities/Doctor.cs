using HealLink.Domain.Base;
using HealLink.Domain.DomainEvents;
using HealLink.Domain.Enums;
using HealLink.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace HealLink.Domain.Entities
{
    // TODO: [DDD] AddNotification() pushes directly onto the collection — notifications should be raised as domain events instead.
    
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

        public void Approve(Guid doctorId)
        {
            IsApproved = true;
            UpdateTimestamp();
            AddDomainEvent( new DoctorApprovedEvent(doctorId) );
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