namespace HealLink.Contracts.Search.Responses
{
    public record PatientSearchResponse(
        List<PatientSearchResultDto> Patients,
        int TotalCount,
        int Page,
        int PageSize,
        int TotalPages
    );

    public record PatientSearchResultDto(
        Guid Id,
        Guid UserId,
        string FullName,
        string Email,
        Guid? GuardianId,
        string? GuardianName,
        DateTime CreatedAt
    );
}
