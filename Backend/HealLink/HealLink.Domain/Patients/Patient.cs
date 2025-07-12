using HealLink.Domain.Doctors;
using HealLink.Domain.MedicalHistories;
using HealLink.Domain.PatientDoctorSubscriptions;
using HealLink.Domain.Patients.HealLink.Domain.Patients;
using HealLink.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Domain.Patients
{
    public class Patient
    {
        public Guid Id { get; private set; }
        public ICollection<PatientDoctorSubscription> _doctorPatients { get; private set; } = new List< PatientDoctorSubscription>();
        public ICollection<MedicalHistory> _medicalHistories { get; private set; } = new List<MedicalHistory>();
        public User User { get; private set; } = null!;
        public PatientGuardian? GuardianRelation { get; private set; }



        private Patient() { }

        public Patient(Guid userId)
        {
            Id = userId;
        }


    }
}
