using JuboHealth_Model.Jubo;
using JuboHealth_WebApi.ProtocolModel.Order;
using JuboHealth_WebApi.Services.Interfaces;
using MongoGogo.Connection;

namespace JuboHealth_WebApi.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IGoCollection<Patient> _patientCollection;
        private readonly IGoCollection<Order> _orderCollection;

        public OrderService(IGoCollection<Patient> patientCollection,
                            IGoCollection<Order> orderCollection)
        {
            this._patientCollection = patientCollection;
            this._orderCollection = orderCollection;
        }

        public async Task<EditOrderResponse> EditOrder(EditOrderRequest request)
        {
            //檢查存在性
            if(await _orderCollection.CountAsync(order => order.Id == request.OrderId) == 0)
            {
                //todo: 錯訊回傳格式將要統一管理
                throw new Exception("無此醫囑");
            }

            //更新
            await _orderCollection.UpdateOneAsync(filter: order => order.Id == request.OrderId,
                                                  updateDefinitionBuilder: builder => builder.Set(order => order.Message, request.Message));

            return new EditOrderResponse();
        }

        public async Task<GetOrdersResponse> GetOrders(GetOrdersRequest request)
        {
            // find patient
            var patientInfo = await _patientCollection.FindOneAsync(filter: patient => patient.Id == request.PatientId,
                                                                    projection: builder => builder.Include(patient => patient.OrderId));
            if(patientInfo == null)
            {
                return new GetOrdersResponse();
            }

            // find orders
            var orders = await _orderCollection.FindAsync(filter: order => patientInfo.OrderId.Contains(order.Id));
            return new GetOrdersResponse
            {
                Orders = orders.Select(order => new GetOrdersResponseOrderInfo
                {
                    Id = order.Id,
                    Message = order.Message
                }).ToList()
            };
        }
    }
}
