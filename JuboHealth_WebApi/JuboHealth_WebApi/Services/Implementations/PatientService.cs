using JuboHealth_Model.Jubo;
using JuboHealth_WebApi.ProtocolModel.Patient;
using JuboHealth_WebApi.Services.Interfaces;
using MongoGogo.Connection;

namespace JuboHealth_WebApi.Services.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IGoCollection<Patient> _patientCollection;

        public PatientService(IGoCollection<Patient> patientCollection)
        {
            this._patientCollection = patientCollection;
        }

        public async Task<GetPatientsResponse> GetPatients()
        {
            var patients = await _patientCollection.FindAsync(_ => true);

            return new GetPatientsResponse
            {
                PatientInfos = patients.Select(patient => new GetPatientsResponsePatientInfo
                {
                    Id = patient.Id,
                    Name = patient.Name,
                    OrderId = patient.OrderId,
                }).ToList()
            };
        }
    }
}
