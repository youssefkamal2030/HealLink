namespace HealLink.Domain.Enums
{
    public enum Gender { Male, Female }
    public enum UserRole { Patient, Doctor, Guardian, Admin }
    // if there's a Pending status here why are we using a different logic to find the pending doctors for admin approval? we should use the same logic for consistency
    // is the pending status reflected on the database ?
    public enum AccountStatus { Pending, Active, Suspended, Deactivated }
    public enum ConnectionStatus { Pending, Accepted, Rejected, Terminated }
    public enum PaymentStatus { Pending, Completed, Failed, Refunded }
    public enum PaymentMethod { Instapay, Fawry, Visa, Mastercard, PayPal }
    public enum MessageStatus { Sent, Delivered, Read }
    public enum MedicationReminderStatus { Pending, Taken, Missed, Snoozed }
    public enum FileType { Image, PDF, Document }
    public enum Currency { EGP, USD, EUR }
    public enum PrescriptionStatus { Active, Inactive, Expired }
    public enum NotificationType { ConnectionAccepted, ConnectionRejected, ConnectionRequest, DoctorApproved, DoctorRejected}
} 