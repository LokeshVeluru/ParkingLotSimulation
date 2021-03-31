using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingSimulation.Models
{
    class HeavyVehicle : Vehicle
    {
        public HeavyVehicle(String vehicleNo)
        {
            this.Number = vehicleNo;
            this.Type = VehicleType.HEAVY_VEHICLE;
        }
    }
}
