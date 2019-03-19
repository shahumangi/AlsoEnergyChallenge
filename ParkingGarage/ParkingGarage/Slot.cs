using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingGarage
{
    public class Slot
    {
        public Level Level { get; set; }
        public int Number { get; set; }
        public bool IsEmpty { get; set; }
        public bool CanFitCar { get; set; }
        public bool IsLarge { get; set; }
        public int Row { get; set; }
    }
}
