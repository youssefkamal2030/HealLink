using System;

namespace HealLink.Contracts.Profile
{
    public record UpdateDoctorProfileRequest
    {
        // Personal Info
        public string FullName { get; init; } = string.Empty;
        public string Gender { get; init; } = string.Empty;
        public string Nationality { get; init; } = string.Empty;
        
        // Address
        public string Street { get; init; } = string.Empty;
        public string City { get; init; } = string.Empty;
        public string State { get; init; } = string.Empty;
        public string Country { get; init; } = string.Empty;
        
        // Professional Info
        public string Specialization { get; init; } = string.Empty;
        public string CurrentWorkplace { get; init; } = string.Empty;
        public string Phone { get; init; } = string.Empty;
        public bool? IsAvailableForChat { get; init; }
    }
}