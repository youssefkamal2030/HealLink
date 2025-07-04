using HealLink.Domain.PatientDoctorSubscriptions;
using HealLink.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Domain.Doctors
{
    public class Doctor
    {
        public Guid Id { get; private set; }
        public string SyndicateIdImagePath { get; private set; } = null!;
        public string NationalID { get; private set; } = null!;
        public string PracticeLicenseNumber { get; private set; } = null!;
        public ICollection<PatientDoctorSubscription> _subscription { get; private set; } = new List<PatientDoctorSubscription>();
        public User User { get; private set; } = null!;


        public Doctor(Guid userId, string syndicateImagePath, string nationalId, string licenseNumber)
        {
            Id = userId;
            SyndicateIdImagePath = syndicateImagePath;
            NationalID = nationalId;
            PracticeLicenseNumber = licenseNumber;
        }
        private Doctor() { }

    }
}
