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
            var t = new CounterTran();
            var p = new Parking(null, null, t);

            t.WripperTime(1000);
            t.WripperTime(3000);
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
            TimeOut = 3;
            Fine = 0.2f;
            priceOfParking = new Dictionary<CarType, int>
            {
                {CarType.Truck, 1 },
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
            timer.Elapsed += (object sender, ElapsedEventArgs eplasedEventArg) =>
           {
               var handler = this.CounterEventHandler;
               if (handler != null)
               {
                   CounterEventHandler(this, new TransactEventHandler(timer.Interval));
               }

           };
            timer.Enabled = true;
        }               
    }

    class Parking
    {
        private List<Car> carList;
        private List<Transaction> tranList;
        private double Balance;

        public Parking(List<Car> _carList, List<Transaction> _tranList, CounterTran _counterTran)  // double _Balance)
        {
            carList = _carList;
            tranList = _tranList;
            //  Balance = _Balance;           

            _counterTran.CounterEventHandler += (Object sender, TransactEventHandler arg) =>
            {
                    switch ((int)arg.SignalTime)
                    {
                     case 1000:
                        Console.WriteLine("It's a 1 sec");
                        break;                    
                    case 3000:

                        Console.WriteLine("It's a 3 sec");
                        break;
                    }
            };
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
