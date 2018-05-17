using System.Collections.Generic;
using ParkingCar.Model;

namespace ParkingCar.Infrastructure.Classes
{
    static class Settings
    {
        static public int ParkingSpace { get; set; } //= 1125;
        static public float Fine { get; set; } // = 0.2f;
        static public int TimeOut { get; set; } // = 3;

        static public Dictionary<CarType, int> priceOfParking;

        static public string Path { get; set; }
        static Settings()
        {
            Path = "Data\\Transactions.log";
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
}
