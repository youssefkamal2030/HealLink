using System;
using System.Linq;
using System.Threading.Tasks;
using healLink.Application.Commands.Prescriptions;
using healLink.Application.Queries.Prescriptions;
using HealLink.Contracts.Prescriptions.Requests;
using HealLink.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealLink.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PrescriptionsController(IMediator mediator) => _mediator = mediator;

        /// <summary>Doctor creates a prescription for a connected patient.</summary>
        [HttpPost]
        public async Task<IActionResult> CreatePrescription([FromBody] CreatePrescriptionRequest request)
        {
            var medications = request.Medications
                .Select(m => new MedicationDosage(m.MedicationName, m.Dosage, m.Instructions, m.ScheduledTimes))
                .ToList();

            var command = new CreatePrescriptionCommand(
                request.DoctorId, request.PatientId, request.Notes, medications, request.ExpiresAt);

            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(new { message = result.Error });
        }

        /// <summary>Get all prescriptions for a patient.</summary>
        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatient([FromRoute] Guid patientId)
        {
            var result = await _mediator.Send(new GetPrescriptionsByPatientQuery(patientId));
            return result.IsSuccess ? Ok(result.Value) : BadRequest(new { message = result.Error });
        }

        /// <summary>Get all prescriptions created by a doctor.</summary>
        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctor([FromRoute] Guid doctorId)
        {
            var result = await _mediator.Send(new GetPrescriptionsByDoctorQuery(doctorId));
            return result.IsSuccess ? Ok(result.Value) : BadRequest(new { message = result.Error });
        }
    }
}
