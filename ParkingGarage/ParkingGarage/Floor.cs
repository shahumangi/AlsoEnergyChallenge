using System.Collections.Generic;

namespace ParkingGarage
{
    public class Level
    {
        public List<Slot> Slots { get; set; }
        public int Number { get; set; }
        public string Id { get; set; } //let's define Floor as 'A', 'B'
        //we can add sections to Floors but for now let's skip it
    }
}
