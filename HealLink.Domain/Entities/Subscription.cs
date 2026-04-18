using System;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;
using HealLink.Domain.ValueObjects;

namespace HealLink.Domain.Entities
{
    public class Subscription : AggregateRoot
    {
        public Guid PatientId { get; private set; }
        public Guid DoctorId { get; private set; }
        public Doctor? Doctor { get; private set; }
        public Money Amount { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsMonthly { get; private set; }
    
        private Subscription() { } 

        public Subscription(Guid patientId, Guid doctorId, Money amount, DateTime startDate, DateTime endDate, bool isMonthly)
        {
            PatientId = patientId;
            DoctorId = doctorId;
            Amount = amount;
            StartDate = startDate;
            EndDate = endDate;
            IsMonthly = isMonthly;
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdateTimestamp();
        }

        public void Renew(DateTime newEndDate)
        {
            EndDate = newEndDate;
            IsActive = true;
            UpdateTimestamp();
        }

        public bool IsExpired()
        {
            return DateTime.UtcNow > EndDate;
        }
    }
}
