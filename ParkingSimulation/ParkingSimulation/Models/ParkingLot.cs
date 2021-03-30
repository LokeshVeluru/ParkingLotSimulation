using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingSimulation.Models
{
    class ParkingLot
    {
        public List<Slot> slots;
        public ParkingLot()
        {
            slots = new List<Slot>();
        }

        public Slot CreateSlot(String slotNumber, VehicleType type)
        {
            Slot newSlot = new Slot(slotNumber, type);
            return newSlot;
        }

        public void AddSlot(String slotNumber, VehicleType type)
        {
            slots.Add(this.CreateSlot(slotNumber, type));
        }
    }
}
