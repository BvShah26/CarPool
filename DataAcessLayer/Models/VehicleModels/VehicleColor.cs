using System;
using System.Collections.Generic;
using System.Text;
using DataAcessLayer.Models.Users;

namespace DataAcessLayer.Models.VehicleModels
{
    public class VehicleColor
    {
        public int Id { get; set; } //300
        public string Color { get; set; } //Red
        public ICollection<Uservehicle> Uservehicles { get; set; } = new HashSet<Uservehicle>();
    }
}
