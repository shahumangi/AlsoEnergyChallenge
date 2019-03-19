using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingGarage
{
    public enum VehicleType
    {
        Car =1,
        MotorCycle =2,
        Bus =3
    }

    public abstract class Vehicle
    {
        protected VehicleType vehicleType;
        public bool IsParked {get;set;}
        public bool DidPark { get; set; } //after entering if got parking
        public Slot Slot { get; set; }
        public abstract bool CanFitInSlot(Slot slot);
        public DateTime? TimeEntered { get; set; }
        public DateTime? TimeLeft { get; set; }
        public VehicleType VehicleType { get; }
        public bool InGarage { get; set; }
        public string NumberPlate { get; set; }
        public bool MonthlyPermit { get; set; }
        public DateTime MonthlyPermitValidity { get; set; }

        public Slot FindParkingSlot(ParkingGarage parkingGarage)
        {
            foreach(var level in parkingGarage.Levels)
            {
                foreach(var slot in level.Slots)
                {
                    if(CanFitInSlot(slot))
                        return slot;
                }
            }
            return null;
        }

        public virtual void Park(Slot slot)
        {
            if (IsParked) //If vehicle is parked you can not park again
                return;
            if (!CanFitInSlot(slot))
                return;
            slot.IsEmpty = false;
            Slot = slot;
            IsParked = true;
            DidPark = true;
        }

        public void VacateSlot(Slot slot)
        {
            IsParked = false;
            slot.IsEmpty = false;
            Slot = null;
        }

        public void Enter(ParkingGarage parkingGarage)
        {
            if (InGarage)// If vehicle in garage cannot enter other garage
                return;
            if (parkingGarage.IsFull)
                return;
            InGarage = true;
        }

        public void Exit(ParkingGarage parkingGarage)
        {
            if (DidPark)
                Pay(parkingGarage);
            InGarage = false;
            DidPark = false;
        }

        public void Pay(ParkingGarage parkingGarage)
        {
            var amount = parkingGarage.Calculate(this);
            parkingGarage.Collect(amount);
        }
    }
}
