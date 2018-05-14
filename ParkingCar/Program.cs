﻿using System;
using System.Collections.Generic;
using ParkingCar.Model;
using ParkingCar.Infrastructure.Classes;
using ParkingCar.Infrastructure.Interface;

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

            var writeToFile = new WriringToFile(path, cars);
            List<Transaction> tranList = new List<Transaction>();
            var tran = new CounterTranHandler();
            var p = new Parking(writeToFile, cars, tranList, tran, price, timeOut, path);
            
             bool enableTimer = true;
             Start(tran, enableTimer);
            //Start(tran, false);
            var q = Console.ReadLine();
            switch (q)
            {
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

       public static void Start(CounterTranHandler tran, bool enableTimer)
        {
            tran.WripperTime(3000, enableTimer);
            tran.WripperTime(30000, enableTimer);
        }
    }
}










