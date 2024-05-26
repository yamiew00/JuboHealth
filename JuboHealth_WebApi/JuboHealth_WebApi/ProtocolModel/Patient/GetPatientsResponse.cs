using System.Text.Json.Serialization;

namespace JuboHealth_WebApi.ProtocolModel.Patient
{
    public class GetPatientsResponse
    {
        [JsonPropertyName("patients")]
        public List<GetPatientsResponsePatientInfo> PatientInfos { get; set; }
    }

    public class GetPatientsResponsePatientInfo
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("OrderId")]
        public List<int> OrderId { get; set; }
    }
}
