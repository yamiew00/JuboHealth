using JuboHealth_Model;
using JuboHealth_Model.Jubo;
using JuboHealth_WebApi.Services.Interfaces;
using MongoGogo.Connection;

namespace JuboHealth_WebApi.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly IGoCollection<Patient> _patientCollection;
        private readonly IGoCollection<Order> _orderCollection;

        public AdminService(IGoCollection<Patient> patientCollection,
                            IGoCollection<Order> orderCollection)
        {
            this._patientCollection = patientCollection;
            this._orderCollection = orderCollection;
        }

        public async Task ResetAllData()
        {
            var initialState = new InitialState();

            //reset patient by replacements
            var patientBulker = _patientCollection.NewBulker();
            initialState.Patients.ForEach(patient =>
            {
                patientBulker.ReplaceOne(filter: dbPatient => dbPatient.Id == patient.Id,
                                         document: patient,
                                         isUpsert: true);
            });
            await patientBulker.SaveChangesAsync();

            //reset order by replacements
            var orderBulker = _orderCollection.NewBulker();
            initialState.Orders.ForEach(order =>
            {
                orderBulker.ReplaceOne(filter: dbOrder => dbOrder.Id == order.Id,
                                       document: order,
                                       isUpsert: true);
            });
            await orderBulker.SaveChangesAsync();
        }
    }

    /// <summary>
    /// 此專案的所有初始狀態
    /// </summary>
    public class InitialState
    {
        public List<Patient> Patients = new List<Patient>
        {
            new Patient
            {
                Id = 1,
                Name = "小民",
                OrderId = 1
            },
            new Patient
            {
                Id = 2,
                Name = "小華",
                OrderId = 2
            },
            new Patient
            {
                Id = 3,
                Name = "小美",
                OrderId = 3
            },
            new Patient
            {
                Id = 4,
                Name = "浩克",
                OrderId = 4
            },
            new Patient
            {
                Id = 5,
                Name = "閃電俠",
                OrderId = 5
            }
        };

        public List<Order> Orders = new List<Order>
        {
            new Order
            {
                Id = 1,
                Message = "超過120請施打8u"
            },
            new Order
            {
                Id = 2,
                Message = "空腹血糖過高，建議追蹤"
            },
            new Order
            {
                Id = 3,
                Message = "血壓偏高，請服藥"
            },
            new Order
            {
                Id = 4,
                Message = "體重超標，建議運動"
            },
            new Order
            {
                Id = 5,
                Message = "心跳過快，請休息"
            }
        };
    }
}
