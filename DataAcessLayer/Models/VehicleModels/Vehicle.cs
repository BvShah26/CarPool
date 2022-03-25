using System;
using System.Collections.Generic;
using System.Text;
using DataAcessLayer.Models.Users;

namespace DataAcessLayer.Models.VehicleModels
{
    public class Vehicle
    {
        public int Id { get; set; } //1
        public string Name { get; set; } //Seltos
        
        public int VehicleBrandId { get; set; }
        public VehicleBrand VehicleBrand { get; set; }  //Kia 101

        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; } //SUV

        public ICollection<Uservehicle> Uservehicles { get; set; }
    }
}
