using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingCar.Model
{
    class Transaction
    {
        public int IdCar { get; set; }
        public DateTime DateTimeTran { get; set; }
        public int WriteOffs { get; set; }
    }
}
