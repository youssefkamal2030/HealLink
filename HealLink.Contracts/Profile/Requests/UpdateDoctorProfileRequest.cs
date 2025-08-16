using System;

namespace HealLink.Contracts.Profile
{
    public record UpdateDoctorProfileRequest
    {
       
        public string? FullName { get; init; } = string.Empty;
        public string? Gender { get; init; } = string.Empty;
        public string? Nationality { get; init; } = string.Empty;
    
   
        public string? City { get; init; } = string.Empty;
        public string? Country { get; init; } = string.Empty;
       
        public string? Specialization { get; init; } = string.Empty;
        public string? CurrentWorkplace { get; init; } = string.Empty;
        public bool? IsAvailableForChat { get; init; }
    }
}