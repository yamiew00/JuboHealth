using JuboHealth_WebApi.ProtocolModel.Order;
using JuboHealth_WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JuboHealth_WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            this._orderService = orderService;
        }

        /// <summary>
        /// 取得單一 patient 的 order
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("orders")]
        public async Task<GetOrdersResponse> GetOrders(GetOrdersRequest request)
        {
            GetOrdersResponse response = await _orderService.GetOrders(request);
            return response;
        }

        /// <summary>
        /// 編輯單一醫囑
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("edit-order")]
        public async Task<EditOrderResponse> EditOrder(EditOrderRequest request)
        {
            EditOrderResponse response = await _orderService.EditOrder(request);
            return response;
        }
    }
}
