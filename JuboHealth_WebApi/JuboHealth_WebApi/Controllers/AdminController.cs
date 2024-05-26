using JuboHealth_WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JuboHealth_WebApi.Controllers
{
    /// <summary>
    /// 註: 基於測試方便，專門給 admin 操作用的api
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService) 
        {
            this._adminService = adminService;
        }

        /// <summary>
        /// 將此專案的資料復原至初始狀態。仍保有至少五筆資料。
        /// </summary>
        /// <returns></returns>
        [HttpPost("ResetAllData")]
        public async Task<IActionResult> ResetAllData()
        {
            await _adminService.ResetAllData();
            return Ok();
        }
    }
}
