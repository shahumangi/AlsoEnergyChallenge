using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingGarage
{
    public class Bus:Vehicle
    {
        public Bus()
        {
            vehicleType = VehicleType.Bus;
        }

        public override bool CanFitInSlot(Slot slot)
        {
            if (slot.IsLarge && slot.IsEmpty)
            {
                int number = slot.Number;
                var level = slot.Level;
                return level.Slots.Where(s => s.Row == slot.Row && s.Number > slot.Number && s.Number < slot.Number + 5).All(s => s.IsEmpty && s.IsLarge);
            }
            else
                return false;
        }
    }
}
