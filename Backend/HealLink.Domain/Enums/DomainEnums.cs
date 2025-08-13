namespace HealLink.Domain.Enums
{
    public enum Gender { Male, Female }
    public enum UserRole { Patient, Doctor, Guardian, Admin }
    public enum AccountStatus { Pending, Active, Suspended, Deactivated }
    public enum ConnectionStatus { Pending, Accepted, Rejected }
    public enum PaymentStatus { Pending, Completed, Failed, Refunded }
    public enum PaymentMethod { Instapay, Fawry, Visa, Mastercard, PayPal }
    public enum MessageStatus { Sent, Delivered, Read }
    public enum MedicationReminderStatus { Pending, Taken, Missed, Snoozed }
    public enum FileType { Image, PDF, Document }
    public enum Currency { EGP, USD, EUR }
    public enum PrescriptionStatus { Active, Inactive, Expired }
} 