using System;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;

namespace HealLink.Domain.Entities
{
    // TODO: [MISSING-FEATURE] No API endpoints exist for TestResult — Patient.UploadTestResult() is implemented
    //   but never called from the application layer. Need:
    //   - UploadTestResultCommand + handler (goes through patient.UploadTestResult(), enforces guardian auth)
    //   - GetTestResultsQuery + handler (get all results for a patient)
    //   - TestResultsController: POST /api/testresults, GET /api/testresults/patient/{patientId}
    //   - ITestResultRepository + implementation + DI registration
    // TODO: [AGGREGATE] TestResult belongs inside PatientAggregate as an owned entity — UploadTestResult() in
    //   PatientAggregate already enforces the guardian authorization (BR-MED-03). Direct construction and
    //   persistence of TestResult outside the aggregate bypasses that invariant entirely.
    // TODO: [AGGREGATE] UpdateFile() and UpdateDescription() should only be callable through PatientAggregate —
    //   direct calls from the application layer bypass the aggregate boundary. Add
    //   Patient.UpdateTestResult(Guid resultId, string filePath, FileType fileType, Guid actingUserId) that
    //   finds the result in _testResults and delegates to result.UpdateFile().
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
