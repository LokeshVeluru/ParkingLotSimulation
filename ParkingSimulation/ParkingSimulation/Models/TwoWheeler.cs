using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingSimulation.Models
{
    class TwoWheeler : Vehicle
    {
        public TwoWheeler(String vehicleNo)
        {
            this.Number = vehicleNo;
            this.Type = VehicleType.TWO_WHEELER;
        }
    }
}
