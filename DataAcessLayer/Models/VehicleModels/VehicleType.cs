using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.Models.VehicleModels
{
    public class VehicleType
    {
        public int Id { get; set; } //200
        public string Title { get; set; } //SUV
        public ICollection<Vehicle> Vehicles { get; set; } //Seltos, Creta
    }
}