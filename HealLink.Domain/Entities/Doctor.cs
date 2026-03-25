using HealLink.Domain.Base;
using HealLink.Domain.DomainEvents;
using HealLink.Domain.Enums;
using HealLink.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace HealLink.Domain.Entities
{
    // TODO: [DDD] QRCode is stored as a raw string — it should use the existing QRCode value object (HealLink.Domain/ValueObjects/QRCode.cs) instead of duplicating the concept with QRCode + QRCodeGeneratedAt fields.
    // TODO: [DDD] GenerateQRCode() and IsQRCodeValid() duplicate logic already defined in the QRCode value object — consolidate into the value object.
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
        public string? QRCode { get; private set; }
        public DateTime? QRCodeGeneratedAt { get; private set; }
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

        public void Approve()
        {
            IsApproved = true;
            UpdateTimestamp();
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
            QRCode = Guid.NewGuid().ToString();
            QRCodeGeneratedAt = DateTime.UtcNow;
            UpdateTimestamp();
        }

        public bool IsQRCodeValid()
        {
            if (!QRCodeGeneratedAt.HasValue)
                return false;

            return DateTime.UtcNow.Subtract(QRCodeGeneratedAt.Value).TotalMinutes < 5;
        }

        public void RefreshQRCodeIfNeeded()
        {
            if (!IsQRCodeValid())
            {
                GenerateQRCode();
            }
        }

     
        public void AddNotification(Notification notification)
        {
            if (notification == null) throw new ArgumentNullException(nameof(notification));
            _notifications.Add(notification);
            UpdateTimestamp();
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