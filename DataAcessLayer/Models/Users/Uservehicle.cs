using System;
using System.Collections.Generic;
using System.Text;
using DataAcessLayer.Models.VehicleModels;

namespace DataAcessLayer.Models.Users
{
    public class Uservehicle
    {
        public int Id { get; set; }
        public string VehicleImage { get; set; }
        public Boolean IsCar_NumberDisclosed { get; set; }
        public string CarNumber { get; set; }


        public Vehicle Vehicle { get; set; }
        public int VehicleId { get; set; }


        public VehicleColor Color { get; set; }
        public int ColorId { get; set; }


        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime Manufacture_Year { get; set; }

    }
}
