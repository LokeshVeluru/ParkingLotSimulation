using System;
using System.Collections.Generic;
using System.Text;
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
            Console.WriteLine("Enter number of slots for 2-wheeler parking: ");
            int M = Convert.ToInt32(Console.ReadLine());
            this.InitializeLot(M, VehicleType.TWO_WHEELER, "T-");
            Console.WriteLine("Enter number of slots for 4-wheeler parking: ");
            int N = Convert.ToInt32(Console.ReadLine());
            this.InitializeLot(N, VehicleType.FOUR_WHEELER, "F-");
            Console.WriteLine("Enter number of slots for heavy vehicle parking: ");
            int O = Convert.ToInt32(Console.ReadLine());
            this.InitializeLot(O, VehicleType.HEAVY_VEHICLE, "H-");
        }

        public void InitializeLot(int slotCount, VehicleType type,String slotId)
        {
            for(int i = 1; i <= slotCount; i++)
            {
                parkingLot.AddSlot(slotNumber: $"{slotId}{i}", type);
            }
        }

        public void Start()
        {
            while (true)
            {
                Console.Write("1.Park a vehicle\t2.Unpark a vehicle\t3.View Current occupancy details\t4.exit\nEnter your choice:");
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
                        ViewOccupancy();
                        break;
                    case 4:
                        goto outer;

                }
            }
        outer:
            Console.WriteLine("Press any key to exit....");
            Console.ReadKey();
        }

        public void ParkVehicle()
        {
            VehicleType type;
            Console.WriteLine("1."+ VehicleType.TWO_WHEELER+"\t2."+ VehicleType.FOUR_WHEELER+"\t3."+ VehicleType.HEAVY_VEHICLE);
            Console.Write("Enter your vehicle type : ");
            type = (VehicleType)Convert.ToInt32(Console.ReadLine());

            for(int i = 0; i < parkingLot.slots.Count ; i++)
            {
                if(parkingLot.slots[i].Type == type && parkingLot.slots[i].Availability == SlotAvailability.FREE)
                {
                    String vehicleNumber;
                    Console.Write("Enter your vehicle number: ");
                    vehicleNumber = Console.ReadLine();
                    ParkingTicket ticket = new ParkingTicket(parkingLot.slots[i].SlotNumber, vehicleNumber);
                    tickets.Add(ticket);
                    Console.WriteLine("Vehicle " + vehicleNumber + " is parked at slot " + parkingLot.slots[i].SlotNumber);
                    parkingLot.slots[i].ToggleAvailability();
                    return;
                }
            }
            Console.WriteLine("No slots are available.");
        }
        public void UnparkVehicle()
        {
            String vehicleNumber;
            VehicleType type;
            Console.Write("Enter your vehicle number : ");
            vehicleNumber = Console.ReadLine();
            Console.WriteLine("1." + VehicleType.TWO_WHEELER + "\t2." + VehicleType.FOUR_WHEELER + "\t3." + VehicleType.HEAVY_VEHICLE);
            Console.Write("Enter your vehicle type : ");
            type = (VehicleType)Convert.ToInt32(Console.ReadLine());
            var map = new Dictionary<string, int>() { { "T", 1 }, { "F", 2 }, { "H", 3 } };
            foreach (ParkingTicket ticket in tickets)
            {
                if (ticket.VehicleNumber == vehicleNumber && map[ticket.SlotNumber[0].ToString()] == (int)type)
                {
                    int index = Convert.ToInt32(ticket.SlotNumber[2..]) - 1;
                    for(int i = 0; i < parkingLot.slots.Count; i++)
                    {
                        if(parkingLot.slots[i].Type == type)
                        {
                            parkingLot.slots[i + index].ToggleAvailability();
                            break;
                        }
                    }
                    Console.WriteLine("Vehicle " + vehicleNumber + " is unparked from the slot " + ticket.SlotNumber);
                    tickets.Remove(ticket);
                    return;
                }
            }
            Console.WriteLine("Details entered are not correct.");
        }

        public void ViewOccupancy()
        {
            Console.WriteLine();
            foreach (Slot slot in parkingLot.slots)
            {
                Console.Write("Slot " + slot.SlotNumber + "  :  ");
                if (slot.Availability == SlotAvailability.FREE)
                {
                    Console.WriteLine(SlotAvailability.FREE);
                }
                else
                {
                    foreach (ParkingTicket ticket in tickets)
                    {
                        if (ticket.SlotNumber == slot.SlotNumber)
                        {
                            Console.WriteLine(ticket.VehicleNumber);
                            break;
                        }
                    }
                }
            }
            Console.WriteLine();
        }
    }
}
