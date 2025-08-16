using System;
using System.Collections.Generic;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;
using HealLink.Domain.ValueObjects;

namespace HealLink.Domain.Entities
{
    public class Doctor : Entity
    {
        public Guid UserId { get; private set; }
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

        private readonly List<Guid> _patientIds = new();
        public IReadOnlyCollection<Guid> PatientIds => _patientIds.AsReadOnly();
        public User? User { get; private set; }
        public ICollection<Subscription>? Subscriptions { get; set; }

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

        public void AddPatient(Guid patientId)
        {
            if (!_patientIds.Contains(patientId))
            {
                _patientIds.Add(patientId);
                UpdateTimestamp();
            }
        }

        public void RemovePatient(Guid patientId)
        {
            if (_patientIds.Contains(patientId))
            {
                _patientIds.Remove(patientId);
                UpdateTimestamp();
            }
        }
    }
}