using System;
using System.Collections.Generic;
using ParkingSimulation.Models;
using ParkingSimulation.Services;

namespace ParkingSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            IAppServices service = new AppServices();

            service.CreateParkingLot();
            service.StartParking();
        }
    }
}