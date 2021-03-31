using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingSimulation.Models
{
    class FourWheeler : Vehicle
    {
        public FourWheeler(String vehicleNo)
        {
            this.Number = vehicleNo;
            this.Type = VehicleType.FOUR_WHEELER;
        }
    }
}
