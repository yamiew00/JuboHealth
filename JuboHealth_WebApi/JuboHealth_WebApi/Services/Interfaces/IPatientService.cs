using JuboHealth_WebApi.ProtocolModel.Patient;

namespace JuboHealth_WebApi.Services.Interfaces
{
    public interface IPatientService
    {
        Task<GetPatientsResponse> GetPatients();
    }
}
