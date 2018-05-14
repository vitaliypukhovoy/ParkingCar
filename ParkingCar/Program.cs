using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Threading;
using System.Timers;

namespace ParkingCar
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Car> c = new List<Car>
            {
                new Car {IdCar=1,CType=CarType.Truck, Balance=5000 },
            new Car { IdCar=2,CType=CarType.Cars, Balance=2000},
            new Car { IdCar=3,CType=CarType.Bus, Balance=3000},
            new Car { IdCar=3,CType=CarType.Truck, Balance=5000}
           };
          

             List<Transaction> tranList = new List<Transaction>();
            var t = new CounterTran();
            var p = new Parking(c, tranList, t);

            t.WripperTime(3000);
            t.WripperTime(10000);
             var q =Console.ReadLine();
            switch (q) {
                case "add":
                       break;
                case "del":
                    break;
                case "start":
                    break;
                case "bal":
                    break;
                case "tran":
                    break;
                case "amout":
                    break;
                case "space":
                    break;
            }
                    


            Console.ReadKey();
        }

    }

    static class Settings
    {
        static public int ParkingSpace { get; set; } //= 1125;
        static public float Fine { get; set; } // = 0.2f;
        static public int TimeOut { get; set; } // = 3;

        static public Dictionary<CarType, int> priceOfParking;
        static Settings()
        {
            ParkingSpace = 1125;
            TimeOut = 3000;
            Fine = 0.2f;
            priceOfParking = new Dictionary<CarType, int>
            {
                {CarType.Truck, 5 },
                {CarType.Cars, 3 },
                {CarType.Bus, 2 },
                {CarType.Motorcycle, 1 }
            };
        }
    }


    public class TransactEventHandler: EventArgs //ElapsedEventArgs
    {
        public TransactEventHandler(double signalTime)          
        {
            SignalTime = signalTime;
        }
      public  double SignalTime { get; private set; } 
     }

    public class CounterTran 
    {       
        public event EventHandler<TransactEventHandler> CounterEventHandler;// ElapsedEventHandler

        public void WripperTime(double sec)
        {
            Timer timer = new Timer();
            timer.Interval = sec;
            timer.Enabled = true;
            timer.Elapsed += (object sender, ElapsedEventArgs eplasedEventArg) =>
           {
               var handler = this.CounterEventHandler;
               if (handler != null)
               {
                   CounterEventHandler(this, new TransactEventHandler(timer.Interval));
               }

           };
            
        }               
    }

    class Parking
    {
        private List<Transaction> tranList;
        private List<Car> carList;       
        private double Balance;
        Dictionary<CarType, int> item = Settings.priceOfParking;
        int TimeOut = Settings.TimeOut;

        public Parking(List<Car> _carList, List<Transaction> _tranList, CounterTran _counterTran)  
        {
            carList = _carList;
            tranList = _tranList;

            _counterTran.CounterEventHandler += (Object sender, TransactEventHandler arg) =>
            {

                if (TimeOut == (int)arg.SignalTime)
                {
                    foreach (Car c in carList)
                    {
                        var value = item.FirstOrDefault(i => i.Key == c.CType).Value;
                       c.Balance =  c.Balance - value; 
                        Balance += value;
                        tranList.Add(new Transaction { IdCar = c.IdCar, DateTimeTran = DateTime.Now, WriteOffs = value });
                    }
                    Console.WriteLine("It's a 1 sec{0}", Balance);
                }
                else

                    Console.WriteLine("It's a 3 sec");
            };
                                                       
        }

        public void PushToList(Car c, int value)
        {
          
        }
    }

    class Car
    {
        public int IdCar { get; set; }
        public int Balance { get; set; }
        public CarType CType { get; set; }

    }

    enum CarType
    {
        Passenger = 1,
        Truck = 2,
        Bus = 3,
        Motorcycle = 4,
        Cars = 5
    }

    class Transaction
    {       
        public int IdCar { get; set; }
        public DateTime DateTimeTran { get; set; }
        public int WriteOffs { get; set; }
    }
}
