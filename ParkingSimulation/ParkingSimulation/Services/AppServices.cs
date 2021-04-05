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
            foreach(var vehicletype in Enum.GetValues(typeof(VehicleType)).Cast<VehicleType>())
            {
                Console.Write("Enter number of slots for " + vehicletype + " parking: ");
                int slotCount = Convert.ToInt32(Console.ReadLine());
                this.AddSlots(slotCount, vehicletype);
            }
        }

        public void AddSlots(int slotCount, VehicleType type)
        {
            int count = parkingLot.slots.Count;
            for(int i = 1; i <= slotCount; i++)
            {
                parkingLot.AddSlot(count + i, type);
            }
        }
                
        public void StartParkingLot()
        {
            while (true)
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
                        System.Environment.Exit(1);
                        break;

                }
            }
        }

        public void ParkVehicle()
        {
            VehicleType type = ReadVehicleType();
            Slot slot = ChooseSlot(type);
            if(slot != null)
            {
                Console.Write("Enter your vehicle number: ");
                String vehicleNumber = Console.ReadLine();
                Vehicle vehicle = CreateVehicle(slot.Type, vehicleNumber);
                int ticketId = tickets.Count + 1;
                ParkingTicket parkingTicket = new ParkingTicket(ticketId, slot.Id, vehicleNumber);
                Console.WriteLine("Your vehicle is successfully parked.");
                DisplayTicketDetails(parkingTicket);
                tickets.Add(parkingTicket);
                slot.SetVehicle(vehicle);
                slot.ToggleAvailability();
            }
        }

        public VehicleType ReadVehicleType()
        {
            int type;
            Console.Write("1.Two Wheeler\t2.Four_Wheeler\t3.HeavyVehicle\nEnter your Vehicle type : ");
            do
            {
                type = Convert.ToInt32(Console.ReadLine());
                if (type >= 1 && type <= 3)
                {
                    break;
                }
                Console.Write("Enter valid vehicle type : ");
            } while (true);
            return (VehicleType )type;
        }

        public void DisplayTicketDetails(ParkingTicket ticket)
        {
            Console.WriteLine("Your Ticket Id : " + ticket.Id + "\tVehicle NO : " + ticket.VehicleNumber + "\tSlot NO : " + ticket.SlotNumber + "\tInTime : " + ticket.InTime);
        }

        public Slot ChooseSlot(VehicleType type)
        {
            try
            {
                var availableSlots = parkingLot.slots.Where(s => s.Availability == SlotAvailability.FREE && s.Type == type);
                if (!availableSlots.Any())
                {
                    Console.WriteLine("No slots are available.");
                }
                else
                {
                    foreach (Slot s in availableSlots)
                    {
                        Console.WriteLine("Slot-" + s.Id);
                    }
                    Console.Write("Enter slot number : ");
                    int slotNumber = Convert.ToInt32(Console.ReadLine());
                    return parkingLot.slots.Single(s => s.Id == slotNumber);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public Vehicle CreateVehicle(VehicleType type, String vehicleNo)
        {
            switch (type)
            {
                case VehicleType.TWO_WHEELER:
                    return new TwoWheeler(vehicleNo);
                case VehicleType.FOUR_WHEELER:
                    return new FourWheeler(vehicleNo);
                case VehicleType.HEAVY_VEHICLE:
                    return new HeavyVehicle(vehicleNo);
                default:
                    break;
            }
            return null;
        }

        public void UnparkVehicle()
        {
            try
            {
                Console.Write("Enter your ticket id : ");
                int ticketId = Convert.ToInt32(Console.ReadLine());
                var ticket = tickets.SingleOrDefault(t => t.Id == ticketId && t.IsValid == true);
                if (IsTicketValid(ticket))
                {
                    var slot = parkingLot.slots.SingleOrDefault(s => s.Id == ticket.SlotNumber);
                    slot.ToggleAvailability();
                    ticket.OutTime = DateTime.Now;
                    ticket.IsValid = false;
                    Console.WriteLine("Vehicle " + ticket.VehicleNumber + " is unparked from the slot " + ticket.SlotNumber + " on " + ticket.OutTime);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public bool IsTicketValid(ParkingTicket ticket)
        {
            if(ticket == null)
            {
                return false;
            }
            Slot slot = parkingLot.slots.Single(s => s.Id == ticket.SlotNumber);
            return slot.Vehicle != null;
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
