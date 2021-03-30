using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingSimulation.Models
{
    class Vehicle
    {
        public String GetVehicleType()
        {
            return this.GetType().Name;
        }
    }
}
