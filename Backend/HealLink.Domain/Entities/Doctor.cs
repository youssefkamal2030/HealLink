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
        public PersonalInfo PersonalInfo { get; private set; }
        public Address Address { get; private set; }

        public string LicenseNumber { get; private set; }
        public string SyndicateIdImagePath { get; private set; } = null!;
        public string PracticeLicenseNumber { get; private set; } = null!;
        public string IdFront { get; } = null!;
        public string IdBack { get; } = null!;
        public string Specialization { get; private set; }
        public string CurrentWorkplace { get; private set; }
        public string Phone { get; private set; } = null!;

        public bool IsAvailableForChat { get; private set; }
        public string QRCode { get; private set; }
        public DateTime QRCodeGeneratedAt { get; private set; }
        public bool IsApproved { get; private set; }

        private readonly List<Guid> _patientIds = new();
        public IReadOnlyCollection<Guid> PatientIds => _patientIds.AsReadOnly();
        public User User { get; private set; }
        public ICollection<Subscription> Subscriptions { get; set; } = [];

        public Doctor(
            Guid userId,
            PersonalInfo personalInfo,
            Address address,
            string syndicateImagePath,
            string Idfront,
            string Idback,
            string licenseNumber,
            string practiceLicenseNumber,
            string specialization,
            string currentWorkplace,
            string phone)
        {
            Id = userId;
            PersonalInfo = personalInfo;
            Address = address;
            SyndicateIdImagePath = syndicateImagePath;
           IdFront = Idfront ;
            IdBack = Idback;
            LicenseNumber = licenseNumber;
            PracticeLicenseNumber = practiceLicenseNumber;
            Specialization = specialization;
            CurrentWorkplace = currentWorkplace;
            Phone = phone;
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

        public void GenerateQRCode()
        {
            QRCode = Guid.NewGuid().ToString();
            QRCodeGeneratedAt = DateTime.UtcNow;
            UpdateTimestamp();
        }

        public bool IsQRCodeValid()
        {
            return DateTime.UtcNow.Subtract(QRCodeGeneratedAt).TotalMinutes < 5;
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