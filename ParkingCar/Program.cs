using System;
using System.Collections.Generic;
using ParkingCar.Model;
using ParkingCar.Infrastructure.Classes;
using ParkingCar.Infrastructure.Interface;
using System.Threading;
using System.Threading.Tasks;

namespace ParkingCar
{
    class Program
    {
        static void Main(string[] args)
        {

            Dictionary<CarType, int> price = Settings.priceOfParking;
            int timeOut = Settings.TimeOut;
            string path = "Data\\Transactions.log";

            List<Car> cars = new List<Car>
            {
            new Car {IdCar=1,CType=CarType.Truck, Balance=5000 },
            new Car { IdCar=2,CType=CarType.Cars, Balance=2000},
            new Car { IdCar=3,CType=CarType.Bus, Balance=3000},
            new Car { IdCar=3,CType=CarType.Truck, Balance=5000}
            };

            WriringToFile writeToFile = new WriringToFile(path, cars);
            List<Transaction> tranList = new List<Transaction>();
            CounterTranHandler tran = new CounterTranHandler();
            Parking p = new Parking(writeToFile, cars, tranList, tran, price, timeOut, path);
            IGetTransaction _p = p;


            Console.WriteLine("Please, Enter you add, del, start, stop, bal, tran,amount, space");
            var q = Console.ReadLine();
            while (true)
            {
                switch (q)
                {
                    case "add":
                        break;
                    case "del":
                        break;
                    case "start":
                        Start(tran, true);
                        q = Console.ReadLine();
                        break;
                    case "stop":
                        Start(tran, false);
                        q = Console.ReadLine();
                        break;
                    case "bal":
                        break;
                    case "tran":
                        var list = ShowTranForMinute(_p, tranList).Result;
                        foreach (var i in list)
                        {
                            Console.WriteLine("{0}   {1}   {2}", i.DateTimeTran, i.IdCar, i.WriteOffs);
                        }
                        q = Console.ReadLine();
                        break;
                    case "amout":
                        break;
                    case "space":
                        break;
                }
            }
            Console.ReadKey();
        }

        public static void Start(CounterTranHandler tran, bool enableTimer)
        {
            tran.WripperTime(3000, enableTimer);
            tran.WripperTime(30000, enableTimer);
        }

        public static async Task<List<Transaction>> ShowTranForMinute(IGetTransaction p, List<Transaction> list)
        {
            var tranForMinute = p.GetTranForMinute(list);
            Task tasks = Task.WhenAll(tranForMinute);

            try
            {
                await tasks;
            }
            catch (AggregateException aex)
            {
                aex.Flatten().Handle(ex =>
                {
                    Console.WriteLine(ex.Message);
                    return true;
                });
            }
            return await tranForMinute;
        }

    }
}










