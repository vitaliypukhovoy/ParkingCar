using System;
using System.Timers;

namespace ParkingCar.Infrastructure.Classes
{
    public class CounterTranHandler
    {
        public event EventHandler<TransactEventHandler> CounterEventHandler;// ElapsedEventHandler

        public void WripperTime(double sec, bool enableTimer)
        {
            Timer timer = new Timer();
            timer.Interval = sec;
            timer.Enabled = enableTimer;
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
}
