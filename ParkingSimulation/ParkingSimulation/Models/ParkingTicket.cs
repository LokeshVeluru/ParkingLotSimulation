using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingSimulation.Models
{
    class ParkingTicket
    {
        public String SlotNumber { get; set; }
        public String VehicleNumber { get; set; }
        

        public ParkingTicket(String slotNumber, String vehicleNumber){
            this.SlotNumber = slotNumber;
            this.VehicleNumber = vehicleNumber;
        }

    }
}
