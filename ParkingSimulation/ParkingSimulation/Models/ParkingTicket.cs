using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingSimulation.Models
{
    class ParkingTicket
    {
        public int Id { get; set; }
        public int SlotNumber { get; set; }
        public String VehicleNumber { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public bool IsValid { get; set; }

        public ParkingTicket(int ticketId,int slotNumber, String vehicleNumber){
            this.Id = ticketId;
            this.SlotNumber = slotNumber;
            this.VehicleNumber = vehicleNumber;
            this.InTime = DateTime.Now;
            this.IsValid = true;
        }

        public void DisplayTicketDetails()
        {
            Console.WriteLine("Your Ticket Id : " + this.Id + "\tVehicle NO : " + this.VehicleNumber + "\tSlot NO : " + this.SlotNumber + "\tInTime : " + this.InTime);
        }
    }
}
