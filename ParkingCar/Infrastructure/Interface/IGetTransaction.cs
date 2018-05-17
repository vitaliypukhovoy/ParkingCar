using System.Collections.Generic;
using System.Threading.Tasks;
using ParkingCar.Model;

namespace ParkingCar.Infrastructure.Interface
{
    interface IGetTransaction
    {
        Task<List<Transaction>> GetTranForMinute(List<Transaction> list);
    }

}
