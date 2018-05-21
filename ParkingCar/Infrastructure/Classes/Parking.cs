using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingCar.Infrastructure.Interface;
using ParkingCar.Model;
using System.Threading;

namespace ParkingCar.Infrastructure.Classes
{
    class Parking : IParking, IGetTransaction
    {
        private static readonly Lazy<Parking> instance =
        new Lazy<Parking>(() => new Parking(), LazyThreadSafetyMode.ExecutionAndPublication);

        private IWriringToFile writeToFile;
        private List<Transaction> tranList;
        private List<Car> carList;
        public static double balance;
        private Dictionary<CarType, int> price;
        private int timeOut = Settings.TimeOut;
        private string path;

        public Parking(IWriringToFile _writeToFile,
                       List<Car> _carList,
                       List<Transaction> _tranList,
                       CounterTranHandler _counterTran,
                       Dictionary<CarType, int> _price,
                       int _timeOut,
                       string _path)
        {
            writeToFile = _writeToFile;
            carList = _carList;
            tranList = _tranList;
            price = _price;
            path = _path;

            _counterTran.CounterEventHandler += (Object sender, TransactEventHandler arg) =>
            {

                if (timeOut == (int)arg.SignalTime)
                {
                    foreach (Car c in carList)
                    {
                        var value = price.FirstOrDefault(i => i.Key == c.CType).Value;
                        c.Balance = c.Balance - value;
                        balance += value;
                        tranList.Add(new Transaction { IdCar = c.IdCar, DateTimeTran = DateTime.Now, WriteOffs = value });
                    }
                    //Console.WriteLine("It's a 3 sec{0}", balance);
                }
                else
                {
                    //Console.WriteLine("It's a 10 sec");
                    writeToFile.PushToFile(tranList, path, balance);
                    tranList.Clear();
                }
            };
        }

        public static Parking Instance
        {
             get
            {
                return instance.Value;
            }
        }
    
        private Parking()
        {
        }
        public async Task<List<Transaction>> GetTranForMinute(List<Transaction> list)
        {
            return await Task.Run(() =>
           {
               var ls = list.Where(i => i.DateTimeTran.Minute == DateTime.Now.Minute)
                     .Select(i => i).ToList();
               return ls;
           });
            
        }
    }
}

