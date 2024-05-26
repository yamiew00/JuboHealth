using JuboHealth_Model.Jubo;
using JuboHealth_WebApi.ProtocolModel.Order;
using JuboHealth_WebApi.Services.Interfaces;
using MongoDB.Driver;
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

        public async Task<AddOrderResponse> AddOrder(AddOrderRequest request)
        {
            //檢查patient存在性
            if(await _patientCollection.CountAsync(patient => patient.Id == request.PatientId) == 0)
            {
                //todo: 錯訊回傳格式應統一管理
                throw new Exception("無此patient");
            }

            //新增order。
            //註: 因為id指定用int，故要避開高併發造成的錯誤
            var newOrder = await AddOrderAsync(request.Message);

            //新增與 patient的關聯
            await _patientCollection.UpdateOneAsync(filter: patient => patient.Id == request.PatientId,
                                                    updateDefinitionBuilder: builder => builder.AddToSet(patient => patient.OrderId, newOrder.Id));

            return new AddOrderResponse();
        }

        /// <summary>
        /// 安全地新增order。因為id是int (auto-increment) 而非ObjectId才需要這麼做
        /// todo: 系統架構更大的話要做成repository pattern
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private async Task<Order> AddOrderAsync(string message)
        {
            var currentMaxIdOrder = await _orderCollection.FindOneAsync(filter: _ => true,
                                                                   goFindOption: new GoFindOption<Order>
                                                                   {
                                                                       Sort = sorter => sorter.OrderByDescending(order => order.Id)
                                                                   });
            
            try
            {
                var newOrder = new Order
                {
                    Id = currentMaxIdOrder.Id + 1,
                    Message = message,
                };
                await _orderCollection.InsertOneAsync(newOrder);
                return newOrder;
            }
            catch(Exception ex)
            {
                //利用id重複的話，mongodb會報錯的原理，不斷retry直到id正確被指定為唯一為止。
                return await AddOrderAsync(message);
            }
        }

        public async Task<EditOrderResponse> EditOrder(EditOrderRequest request)
        {
            //檢查存在性
            if(await _orderCollection.CountAsync(order => order.Id == request.OrderId) == 0)
            {
                //todo: 錯訊回傳格式應統一管理
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
