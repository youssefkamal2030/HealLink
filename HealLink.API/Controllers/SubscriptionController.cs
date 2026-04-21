using System;
using System.Threading.Tasks;
using healLink.Application.Commands.Subscriptions;
using healLink.Application.Queries.Subscriptions;
using HealLink.Contracts.Subscriptions.Requests;
using HealLink.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealLink.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubscriptionController(IMediator mediator) => _mediator = mediator;

        /// <summary>Doctor creates a subscription for a connected patient.</summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubscriptionRequest request)
        {
            var command = new CreateSubscriptionCommand(
                request.DoctorId, request.PatientId,
                request.Amount, request.Currency,
                request.StartDate, request.EndDate, request.IsMonthly);

            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(new { message = result.Error });
        }

        /// <summary>Get all subscriptions for a patient.</summary>
        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatient([FromRoute] Guid patientId)
        {
            var result = await _mediator.Send(new GetSubscriptionsByPatientQuery(patientId));
            return result.IsSuccess ? Ok(result.Value) : BadRequest(new { message = result.Error });
        }

        /// <summary>Get all subscriptions for a doctor.</summary>
        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctor([FromRoute] Guid doctorId) 
        {
            var result = await _mediator.Send(new GetSubscriptionsByDoctorQuery(doctorId));
            return result.IsSuccess ? Ok(result.Value) : BadRequest(new { message = result.Error });
        }

        /// <summary>Add a payment to a subscription.</summary>
        [HttpPost("{subscriptionId}/payments")]
        public async Task<IActionResult> AddPayment([FromRoute] Guid subscriptionId, [FromBody] AddPaymentRequest request)
        {
            var command = new AddPaymentCommand(subscriptionId, request.Amount, request.Currency, request.PaymentMethod);
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(new { message = result.Error });
        }

        /// <summary>Mark a payment as completed.</summary>
        [HttpPut("{subscriptionId}/payments/{paymentId}/complete")]
        public async Task<IActionResult> CompletePayment([FromRoute] Guid subscriptionId, [FromRoute] Guid paymentId, [FromBody] CompletePaymentRequest request)
        {
            var result = await _mediator.Send(new CompletePaymentCommand(subscriptionId, paymentId, request.TransactionId));
            return result.IsSuccess ? Ok() : BadRequest(new { message = result.Error });
        }

        /// <summary>Mark a payment as failed.</summary>
        [HttpPut("{subscriptionId}/payments/{paymentId}/fail")]
        public async Task<IActionResult> FailPayment([FromRoute] Guid subscriptionId, [FromRoute] Guid paymentId, [FromBody] FailPaymentRequest request)
        {
            var result = await _mediator.Send(new FailPaymentCommand(subscriptionId, paymentId, request.FailureReason));
            return result.IsSuccess ? Ok() : BadRequest(new { message = result.Error });
        }

        /// <summary>Refund a completed payment.</summary>
        [HttpPut("{subscriptionId}/payments/{paymentId}/refund")]
        public async Task<IActionResult> RefundPayment([FromRoute] Guid subscriptionId, [FromRoute] Guid paymentId)
        {
            var result = await _mediator.Send(new RefundPaymentCommand(subscriptionId, paymentId));
            return result.IsSuccess ? Ok() : BadRequest(new { message = result.Error });
        }
    }
}
