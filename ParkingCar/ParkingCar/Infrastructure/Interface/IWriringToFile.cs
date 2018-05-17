using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using ParkingCar.Model;

namespace ParkingCar.Infrastructure.Interface
{
    interface IWriringToFile
    {
        void PushToFile(List<Transaction> list, string path, double balance);
        Task<string> GetTargetPath(string path);
        Task<byte[]> GetInfo(List<Transaction> list);
        Task Write(FileStream fs, byte[] info);
    }
}
