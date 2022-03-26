using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.Models.VehicleModels
{
    public class VehicleBrand
    {
        public int Id { get; set; } //101
        public string Name { get; set; } //Kia
        public ICollection<Vehicle> Vehicles { get; set; } = new HashSet<Vehicle>(); //Seltos,Sonet
    }
}
