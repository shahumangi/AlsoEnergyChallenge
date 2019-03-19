using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingGarage
{
    interface IPaymentCalculator
    {
        Dictionary<VehicleType, float> ChargePerHour { get; set; }
        Dictionary<VehicleType, float> ChargePerDay { get; set; }
        Dictionary<VehicleType, float> ChargePerMonth { get; set; }
        float Calculate(Vehicle vehicle);
    }
}
