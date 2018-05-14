using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingCar.Infrastructure.Interface;
using ParkingCar.Model;

namespace ParkingCar.Infrastructure.Classes
{
    class Parking : IParking, IGetTransaction
    {
        private IWriringToFile writeToFile;
        private List<Transaction> tranList;
        private List<Car> carList;
        private static double balance;
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
                    Console.WriteLine("It's a 3 sec{0}", balance);
                }
                else
                {
                    Console.WriteLine("It's a 10 sec");
                    writeToFile.PushToFile(tranList, path, balance);
                    tranList.Clear();
                }
            };
        }
        public Task<List<Transaction>> GetTranForMinute(List<Transaction> list)
        {
            var task = Task<List<Transaction>>.Run(() =>
            {
                var ls = list.Where(i => i.DateTimeTran.Minute - 1 == DateTime.Now.Minute)
                      .Select(i => i).ToList();
                return ls;
            });
            return task;
        }
    }
}

