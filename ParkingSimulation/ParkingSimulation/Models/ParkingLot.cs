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
        public void AddSlot(int id, VehicleType type)
        {
            slots.Add(new Slot(id,type));
        }
    }
}
