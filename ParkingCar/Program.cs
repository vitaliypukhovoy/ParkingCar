using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingCar
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(Settings.priceOfParking.FirstOrDefault(i=> (int)i.Key == 5));
        }
    }

     static class   Settings
    {
       static public int ParkingSpace { get; set; } //= 1125;
       static public float Fine { get; set; } // = 0.2f;
       static public int TimeOut { get; set; } // = 3;

       static public Dictionary<CarType, int> priceOfParking;
       static  Settings()
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


    public class TransactEventHandler : EventArgs
    {
        public int Unit { get; set; }

    }

    public class CounterTran
    {

       public event EventHandler<TransactEventHandler> CounterEventHandler;

       public  void CounterTranaction()
        {
            CounterEventHandler(this, new TransactEventHandler { Unit  = 3 });
        }

      public void CounterWriteToLog()
        {
            CounterEventHandler(this, new TransactEventHandler { Unit = 30 });

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
                switch (arg.Unit)
                {
                    case 3:
                        //tranList.Add();
                        Console.WriteLine();
                        break;
                    case 30:

                        //tranList.Count(i => i.WriteOffs);
                        Console.WriteLine();
                        break;
                }

            };           
        }
    }

    class Car
    {
        public int Id { get; set; }
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
