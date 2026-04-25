namespace HealLink.Contracts.Search.Responses
{
    public record DoctorSearchResponse(
        List<DoctorSearchResultDto> Doctors,
        int TotalCount,
        int Page,
        int PageSize,
        int TotalPages
    );

    public record DoctorSearchResultDto(
        Guid Id,
        Guid UserId,
        string FullName,
        string Email,
        string? Specialization,
        string? CurrentWorkplace,
        string? City,
        string? Country,
        bool IsAvailableForChat,
        bool IsApproved,
        DateTime CreatedAt
    );
}
