using System;

namespace ParkingCar.Infrastructure.Classes
{
    public class TransactEventHandler : EventArgs //ElapsedEventArgs
    {
        public TransactEventHandler(double signalTime)
        {
            SignalTime = signalTime;
        }
        public double SignalTime { get; private set; }
    }
}
