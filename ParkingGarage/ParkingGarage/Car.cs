using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingGarage
{
    public class Car:Vehicle
    {
        public Car()
        {
            vehicleType = VehicleType.Car;
        }

        public override bool CanFitInSlot(Slot slot)
        {
            return (slot.CanFitCar || slot.IsLarge);
        }
    }
}
