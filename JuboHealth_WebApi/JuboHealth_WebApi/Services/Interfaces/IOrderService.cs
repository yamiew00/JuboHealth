using JuboHealth_WebApi.ProtocolModel.Order;

namespace JuboHealth_WebApi.Services.Interfaces
{
    public interface IOrderService
    {
        Task<AddOrderResponse> AddOrder(AddOrderRequest request);
        Task<EditOrderResponse> EditOrder(EditOrderRequest request);
        Task<GetOrdersResponse> GetOrders(GetOrdersRequest request);
    }
}
