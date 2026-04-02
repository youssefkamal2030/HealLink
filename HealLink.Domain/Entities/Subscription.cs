using System;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;
using HealLink.Domain.ValueObjects;

namespace HealLink.Domain.Entities
{
    // TODO: [DDD] Amount is typed as int — monetary values should use the Money value object (HealLink.Domain/ValueObjects/Money.cs) to encapsulate currency and amount together.
    // TODO: [DDD] Doctor navigation property has no setter visibility control — it should be private set or removed (use DoctorId for reference within the domain).
    // TODO: [DDD] No domain event raised on Deactivate() or Renew() — these are significant state transitions that downstream contexts may need to react to.
    // TODO: [DOMAIN-NEXT] Subscription already extends AggregateRoot — the first TODO above is stale, remove it.
    // TODO: [DOMAIN-NEXT] Replace `int Amount` with `Money Amount` — update the constructor signature from (int amount) to (Money amount). Update SubscriptionAggregate.AddPayment() and any callers accordingly.
    // TODO: [DOMAIN-NEXT] Make `Doctor Doctor` navigation property `private set` — callers should reference the doctor by DoctorId, not navigate through the entity.
    // TODO: [DOMAIN-NEXT] Raise SubscriptionDeactivatedEvent in Deactivate() and SubscriptionRenewedEvent in Renew(). Create both event classes in HealLink.Domain/DomainEvents/ following the same pattern as PrescriptionCreatedEvent.
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
