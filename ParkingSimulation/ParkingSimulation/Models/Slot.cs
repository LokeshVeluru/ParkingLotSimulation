using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingSimulation.Models
{
    class Slot
    {
        public int Id { get; set; }
        public VehicleType Type { get; set; }
        public SlotAvailability Availability { get; set; }
        public Slot(int id, VehicleType type)
        {
            this.Id = id;
            this.Type = type;
            this.Availability = SlotAvailability.FREE;
        }

        public void SetAvaialability(SlotAvailability availability)
        {
            this.Availability = availability;
        }

        public void ToggleAvailability()
        {
            this.Availability = this.Availability == SlotAvailability.FREE ? SlotAvailability.FILLED : SlotAvailability.FREE;
        }
    }
}
