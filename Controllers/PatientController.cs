using healLink.Application.Patients.Commands.DeleteGuardian;
using healLink.Application.Patients.Commands.DeleteMedicalHistrory;
using HealLink.Contracts.Patient;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace HealLink.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class PatientController(ISender _sender) : ApiController
    {


        #region The following methods are used to handle medical history operations for patients.
        [HttpPost("AddMedicalHistory")]
        public async Task<IActionResult> AddMedicalHistoryAsync(AddMedicalHistoryRequest request)
        {
            var command = request.ToCommand();
            var result = await _sender.Send(command);
            
            return result.Match(
                success => Ok(success),
                error => Problem(error)
            );
        }

        [HttpGet("GetMedicalHistoryByUserId/{id}")]
        public async Task<IActionResult> GetMedicalHistoryByUserIdAsync(GetMedicalHistoryRequest request)
        {
            var query = request.ToQuery();
            var result = await _sender.Send(query);
            
            return result.Match(
                success => Ok(success),
                error => Problem(error)
            );
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatientAsync(Guid id)
        {
            var command = new DeleteMedicalHistoryCommand(id);
            var result = await _sender.Send(command);
            

            return result.Match(
                success => Ok("Patient deleted successfully."),
                error => Problem(error)
            );
        }
        #endregion


        #region The following methods are used to handle patient Guardian.

        [HttpPost("AddGuardian")]
        public async Task<IActionResult> AddGuardianAsync(AddGuardianRequest request)
        {

            var command = request.ToCommand();
            var result = await _sender.Send(command);

            return result.Match(
                success => Ok("Guardian added successfully."),
                error => Problem(error)
            );


        }



        [HttpGet("GetPatientGuardians/{userId}")]
        public async Task<IActionResult> GetPatientGuardiansAsync(GetPatientGuardianRequest request)
        {
            var query = request.ToQuery();
            var result = await _sender.Send(query);
            return result.Match(
                success => Ok(success),
                error => Problem(error)
            );


        }


        [HttpDelete("DeleteGuardian/{id}")]
        public async Task<IActionResult> DeleteGuardianAsync(Guid id)
        {
            var command = new DeleteGuardianCommand(id);
            var result = await _sender.Send(command);
            return result.Match(
                success => Ok("Guardian deleted successfully."),
                error => Problem(error)
            );
        }

        #endregion




    }

    





}