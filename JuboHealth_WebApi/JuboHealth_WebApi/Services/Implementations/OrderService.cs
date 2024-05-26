using JuboHealth_WebApi.ProtocolModel.Order;
using JuboHealth_WebApi.Services.Interfaces;

namespace JuboHealth_WebApi.Services.Implementations
{
    public class OrderService : IOrderService
    {
        public Task<EditOrderResponse> EditOrder(EditOrderRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetOrdersResponse> GetOrders(GetOrdersRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
