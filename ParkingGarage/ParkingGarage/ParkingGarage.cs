using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingGarage
{
    public class ParkingGarage: IPaymentCalculator,IPaymentCollector
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<Level> Levels { get; set; }
        public bool IsFull { get {
                foreach (var level in Levels)
                    foreach (var slot in level.Slots)
                        if (slot.IsEmpty)
                            return false;
                return true;
            } }

        public Dictionary<VehicleType,float> ChargePerHour { get; set; }
        public Dictionary<VehicleType, float> ChargePerDay { get; set; }
        public Dictionary<VehicleType, float> ChargePerMonth { get; set; }

        public float Calculate(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public void Collect(float amount)
        {
            throw new NotImplementedException();
        }
        
    }
}
