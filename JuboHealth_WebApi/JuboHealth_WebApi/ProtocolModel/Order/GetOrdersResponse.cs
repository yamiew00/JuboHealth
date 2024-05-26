using System.Text.Json.Serialization;

namespace JuboHealth_WebApi.ProtocolModel.Order
{
    public class GetOrdersResponse
    {
        [JsonPropertyName("orders")]
        public List<GetOrdersResponseOrderInfo> Orders { get; set; }
    }

    public class GetOrdersResponseOrderInfo
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Message")]
        public string Message { get; set; }
    }
}
