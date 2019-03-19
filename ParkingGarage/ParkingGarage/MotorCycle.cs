using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingGarage
{
    public class MotorCycle : Vehicle
    {
        public MotorCycle()
        {
            vehicleType = VehicleType.MotorCycle;
        }
        public override bool CanFitInSlot(Slot slot)
        {
            return slot.IsEmpty;
        }
    }
}
