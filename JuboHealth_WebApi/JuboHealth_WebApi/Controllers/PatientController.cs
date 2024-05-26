using JuboHealth_WebApi.ProtocolModel.Patient;
using JuboHealth_WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JuboHealth_WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            this._patientService = patientService;
        }

        /// <summary>
        /// ¨ú±o patients ¦Cªí
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<GetPatientsResponse> GetPatients()
        {
            GetPatientsResponse response = await _patientService.GetPatients();
            return response;
        }
    }
}
