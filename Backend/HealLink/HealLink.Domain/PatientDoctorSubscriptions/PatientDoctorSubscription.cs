using HealLink.Domain.Doctors;
using HealLink.Domain.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Domain.PatientDoctorSubscriptions
{
    public class PatientDoctorSubscription
    {

        public Guid Id { get; private set; }
        public Guid DoctorId { get; private set; }
        public Doctor Doctor { get; private set; } = null!;

        public Guid PatientId { get; private set; }
        public Patient Patient { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }

        private PatientDoctorSubscription() { }

    }
}
