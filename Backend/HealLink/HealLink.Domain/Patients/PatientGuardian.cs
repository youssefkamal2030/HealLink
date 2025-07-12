using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Domain.Patients
{
    namespace HealLink.Domain.Patients
    {
        public class PatientGuardian
        {
            public Guid Id { get; private set; }

            public Guid PatientId { get; private set; }
            public Patient Patient { get; private set; } = null!;

            public Guid GuardianId { get; private set; }
            public Patient Guardian { get; private set; } = null!;
            public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

            public string? RelationshipType { get; private set; }

            private PatientGuardian() { }

            public PatientGuardian(Guid patientId, Guid guardianId, string? relationshipType = null)
            {
                Id = Guid.NewGuid();
                PatientId = patientId;
                GuardianId = guardianId;
                RelationshipType = relationshipType;
            }
        }
    }

}
