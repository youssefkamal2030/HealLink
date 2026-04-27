namespace healLink.Application.Interfaces;

public interface IPatientScopedRequest
{
    Guid PatientId { get; }
}
