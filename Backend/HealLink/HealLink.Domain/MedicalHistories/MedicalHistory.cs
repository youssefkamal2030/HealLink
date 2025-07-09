using HealLink.Domain.Patients;
namespace HealLink.Domain.MedicalHistories
{
    public class MedicalHistory
    {

        public Guid Id { get; private set; }
        public Guid PatientId { get; private set; }
        public Patient Patient { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public string? Description { get; private set; } = null;
        public string? FileLink { get; private set; } = null;

        public MedicalHistoryType Type { get; private set; } = MedicalHistoryType.Medication;

        private MedicalHistory() { }

        public MedicalHistory(Guid patientId, MedicalHistoryType type, string? description = null, string? fileLink = null)
        {
            Id = Guid.NewGuid();
            PatientId = patientId;
            Type = type;
            Description = description;
            FileLink = fileLink;
            CreatedAt = DateTime.UtcNow;
        }


    }
}