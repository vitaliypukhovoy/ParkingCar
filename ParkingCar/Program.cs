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
            Console.WriteLine(Settings.priceOfParking);

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


    class Parking
    {
        private List<Car> carList;
        private List<Transaction> TranList;
        private double Balance;

        public Parking(List<Car> _carList, List<Transaction> _TranList, double _Balance)
        {
            carList = _carList;
            TranList = _TranList;
            Balance = _Balance;
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
        Cars = 4
    }

    class Transaction
    {
        public DateTime DateTimeTran { get; set; }
        public int IdCar { get; set; }
        public int MyProperty { get; set; }
    }
}
