using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ParkingSimulation.Models;

namespace ParkingSimulation.Services
{
    class AppServices : IAppServices
    {
        readonly ParkingLot parkingLot;
        readonly List<ParkingTicket> tickets;
        public AppServices()
        {
            parkingLot = new ParkingLot();
            tickets = new List<ParkingTicket>();
        }
        public void CreateParkingLot()
        {
            Console.Write("Enter number of slots for 2-wheeler parking: ");
            int M = Convert.ToInt32(Console.ReadLine());
            this.AddSlot(M, VehicleType.TWO_WHEELER);
            Console.Write("Enter number of slots for 4-wheeler parking: ");
            int N = Convert.ToInt32(Console.ReadLine());
            this.AddSlot(N, VehicleType.FOUR_WHEELER);
            Console.Write("Enter number of slots for heavy vehicle parking: ");
            int O = Convert.ToInt32(Console.ReadLine());
            this.AddSlot(O, VehicleType.HEAVY_VEHICLE);
        }

        public void AddSlot(int slotCount, VehicleType type)
        {
            int count = parkingLot.slots.Count;
            for(int i = 1; i <= slotCount; i++)
            {
                parkingLot.AddSlot(count + i, type);
            }
        }

        public void StartParking()
        {
            bool mainLoopCondition = true;
            while (mainLoopCondition)
            {
                Console.Write("\n1.Park a vehicle\t2.Unpark a vehicle\t3.View Current occupancy details\t4.exit\nEnter your choice:");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        ParkVehicle();
                        break;
                    case 2:
                        UnparkVehicle();
                        break;
                    case 3:
                        ViewCurrentOccupancy();
                        break;
                    case 4:
                        mainLoopCondition = false;
                        break;

                }
            }
        }

        public void ParkVehicle()
        {
            DisplayAvaialableSlots();
            Console.Write("Enter slot id you want to park(-1 if slot is not available) : ");
            int slotId = Convert.ToInt32(Console.ReadLine());
            if(slotId != -1)
            {
                var slot = parkingLot.slots.SingleOrDefault(s => s.Id == slotId);
                if (slot == null)
                {
                    Console.WriteLine("Enter valid slot id!!!");
                    this.ParkVehicle();
                }
                else
                {
                    Console.Write("Enter your vehicle number: ");
                    String vehicleNumber = Console.ReadLine();
                    int ticketId = tickets.Count + 1;
                    ParkingTicket parkingTicket = new ParkingTicket(ticketId, slot.Id, vehicleNumber);
                    Console.WriteLine("Your vehicle is successfully parked.");
                    parkingTicket.DisplayTicketDetails();
                    tickets.Add(parkingTicket);
                    slot.ToggleAvailability();
                }
            }
        }

        public void DisplayAvaialableSlots()
        {
            bool isAvailable = false;
            Console.WriteLine("Available Slots : ");
            foreach(Slot slot in parkingLot.slots)
            {
                if(slot.Availability == SlotAvailability.FREE)
                {
                    Console.WriteLine("Slot-" + slot.Id + " of type " + slot.Type);
                    isAvailable = true;
                }
            }
            if (!isAvailable)
            {
                Console.WriteLine("No slots are available.");
            }
        }

        public void UnparkVehicle()
        {
            Console.Write("Enter your ticket id : ");
            int ticketId = Convert.ToInt32(Console.ReadLine());
            var ticket = tickets.SingleOrDefault(t => t.Id == ticketId);
            if (ticket != null && ticket.IsValid)
            {
                parkingLot.slots[ticket.SlotNumber - 1].ToggleAvailability();
                ticket.OutTime = DateTime.Now;
                ticket.IsValid = false;
                Console.WriteLine("Vehicle " + ticket.VehicleNumber + " is unparked from the slot " + ticket.SlotNumber + " on " + ticket.OutTime);
            }
            else
            {
                Console.WriteLine("Enter valid ticket details!!");
                this.UnparkVehicle();
            }
        }

        public void ViewCurrentOccupancy()
        {
            Console.WriteLine();
            foreach (Slot slot in parkingLot.slots)
            {
                Console.Write("Slot " + slot.Id + "  :  ");
                if (slot.Availability == SlotAvailability.FREE)
                {
                    Console.WriteLine(SlotAvailability.FREE);
                }
                else
                {
                    var ticket = tickets.SingleOrDefault(t => t.SlotNumber == slot.Id && t.IsValid == true);
                    Console.WriteLine(ticket.VehicleNumber);
                }
            }
            Console.WriteLine();
        }
    }
}
