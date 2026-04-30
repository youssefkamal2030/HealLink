using healLink.Application.Common;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;

namespace healLink.Application.Authorization.Policies;

/// <summary>
/// Authorization policy that allows either the patient or their assigned guardian to access patient-scoped resources.
/// This policy is used for operations like uploading test results or confirming medication reminders.
/// 
/// USAGE:
/// Commands implement IAuthorizeRequest with Policy = "PatientOrGuardianAccess" and IPatientScopedRequest with PatientId.
/// Example: MarkReminderAsTakenCommand(ReminderId, PatientId) where PatientId identifies the patient
/// 
/// FLOW:
/// 1. Check if request implements IPatientScopedRequest (provides PatientId)
/// 2. Load the Patient by PatientId from the repository
/// 3. Check if currentUser.UserId matches patient.UserId (user is the patient)
/// 4. OR check if currentUser.UserId matches patient.GuardianId (user is the guardian)
/// 5. Return true if either condition is met, false otherwise
/// 6. Return false if patient not found (treat as unauthorized, not 404)
/// 
/// AUTHORIZATION RULES:
/// - Patient can access their own resources (currentUser.UserId == patient.UserId)
/// - Guardian can access their assigned patient's resources (currentUser.UserId == patient.GuardianId)
/// - Anyone else is denied access
/// </summary>
public class PatientOrGuardianAccessPolicy : IAuthorizationPolicy
{
    private readonly IPatientRepository _patientRepository;

    public PatientOrGuardianAccessPolicy(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    /// <summary>
    /// The policy name that commands reference via IAuthorizeRequest.Policy.
    /// Must match AuthorizationPolicies.PatientOrGuardianAccess constant.
    /// </summary>
    public string Name => AuthorizationPolicies.PatientOrGuardianAccess;

    /// <summary>
    /// Determines if the current user is authorized to access patient-scoped resources.
    /// </summary>
    /// <param name="currentUser">Service providing the authenticated user's ID from JWT claims</param>
    /// <param name="request">The command/query being executed (must implement IPatientScopedRequest)</param>
    /// <param name="cancellationToken">Cancellation token for async operations</param>
    /// <returns>
    /// True if current user is the patient OR the patient's guardian.
    /// False if user is neither, patient not found, or request doesn't implement IPatientScopedRequest.
    /// </returns>
    public async Task<bool> AuthorizeAsync(
        ICurrentUserService currentUser,
        object request,
        CancellationToken cancellationToken)
    {
        // STEP 1: Verify request provides a PatientId
        if (request is not IPatientScopedRequest patientRequest)
            return false;

        // STEP 2: Load the patient aggregate
        var patient = await _patientRepository.GetByPatientId(patientRequest.PatientId);

        // STEP 3: Return false if patient doesn't exist (treat as unauthorized, not 404)
        if (patient == null)
            return false;

        // STEP 4: Check if current user is the patient
        if (patient.UserId == currentUser.UserId)
            return true;

        // STEP 5: Check if current user is the patient's guardian
        if (patient.GuardianId.HasValue && patient.GuardianId.Value == currentUser.UserId)
            return true;

        // STEP 6: User is neither patient nor guardian - deny access
        return false;
    }
}
