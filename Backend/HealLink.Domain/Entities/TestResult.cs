using System;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;

namespace HealLink.Domain.Entities
{
    public class TestResult : Entity
    {
        public Guid PatientId { get; private set; }
        public string TestName { get; private set; }
        public string Description { get; private set; }
        public DateTime TestDate { get; private set; }
        public string FilePath { get; private set; }
        public FileType FileType { get; private set; }
        public Guid? UploadedByGuardianId { get; private set; }

        private TestResult() { } // For EF

        public TestResult(Guid patientId, string testName, string description, DateTime testDate, string filePath, FileType fileType, Guid? uploadedByGuardianId = null)
        {
            PatientId = patientId;
            TestName = testName ?? throw new ArgumentNullException(nameof(testName));
            Description = description ?? string.Empty;
            TestDate = testDate;
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            FileType = fileType;
            UploadedByGuardianId = uploadedByGuardianId;
        }

        public void UpdateDescription(string description)
        {
            Description = description ?? string.Empty;
            UpdateTimestamp();
        }

        public void UpdateFile(string filePath, FileType fileType)
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            FileType = fileType;
            UpdateTimestamp();
        }
    }
}
