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
            string path = Settings.Path;

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



            CarType _type = CarType.Truck;
            Console.WriteLine("Please, Enter you add, del, start, stop, bal, tran,amount, space");

           
            do
            {
                var q = Console.ReadLine();
                switch (q)
                {
                    case "add":
                        Console.WriteLine(" Please, enter CarType");
                        Console.WriteLine("Do you want to enter Truck = 2, Car = 5, Bus =3, Motorcycle =4");
                        var n = Console.ReadLine();
                        switch (n)
                        {
                            case "2":
                                _type = CarType.Truck;
                                break;
                            case "5":
                                _type = CarType.Cars;
                                break;
                            case "3":
                                _type = CarType.Bus;
                                break;
                            case "4":
                                _type = CarType.Motorcycle;
                                break;
                        }
                        Console.WriteLine("Enter balance car");
                        var bc = Console.ReadLine();
                        cars.Add(new Car { IdCar = cars.Count + 1, CType = _type, Balance = int.Parse(bc) });                      
                        break;
                    case "del":
                        Console.WriteLine(" Please, enter ID Car");
                        var del = Console.ReadLine();
                        cars.RemoveAt(int.Parse(del));                     
                        break;
                    case "start":
                        Start(tran, true);                     
                        break;
                    case "stop":
                        Start(tran, false);                   
                        break;
                    case "bal":
                        Console.WriteLine(Parking.balance);                
                        break;
                    case "tran":
                        var list = p.GetTranForMinute(tranList).GetAwaiter().GetResult();
                        foreach (var i in list)
                        {
                            Console.WriteLine("{0}   {1}   {2}", i.DateTimeTran, i.IdCar, i.WriteOffs);
                        }                   
                        break;
                    case "amout":
                        break;
                    case "space":
                        Console.WriteLine(Settings.ParkingSpace - cars.Count);                   
                        break;
                    default:
                        Console.WriteLine("You are entered fault command");                   
                        break;
                }
            } while (true);
            Console.ReadKey();
        }

        public static void Start(CounterTranHandler tran, bool enableTimer)
        {
            tran.WripperTime(3000, enableTimer);
            tran.WripperTime(30000, enableTimer);
        }
  
    }
}










