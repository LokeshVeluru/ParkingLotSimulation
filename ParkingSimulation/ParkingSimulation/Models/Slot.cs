using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingSimulation.Models
{
    class Slot
    {
        public String SlotNumber { get; set; }
        public VehicleType Type { get; set; }
        public SlotAvailability Availability { get; set; }
        public Slot(String slotNumber, VehicleType type)
        {
            this.SlotNumber = slotNumber;
            this.Type = type;
            this.Availability = SlotAvailability.FREE;
        }

        public void SetAvaialability(SlotAvailability availability)
        {
            this.Availability = availability;
        }

        public void ToggleAvailability()
        {
            if(Availability == SlotAvailability.FREE)
            {
                Availability = SlotAvailability.FILLED;
            }
            else
            {
                Availability = SlotAvailability.FREE;
            }
        }
    }
}
