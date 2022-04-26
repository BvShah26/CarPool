using System;
using System.Collections.Generic;
using System.Text;
using DataAcessLayer.Models.Rides;
using DataAcessLayer.Models.VehicleModels;

namespace DataAcessLayer.Models.Users
{
    public class Uservehicle
    {
        public int Id { get; set; }
        public string VehicleImage { get; set; }
        //public Boolean IsCar_NumberDisclosed { get; set; }
        //public string CarNumber { get; set; }


        public Vehicle Vehicle { get; set; } //seltos,creta...
        public int VehicleId { get; set; }


        public VehicleColor Color { get; set; }
        public int ColorId { get; set; }


        public ClientUsers UserOwner { get; set; }
        public int UserOwnerId { get; set; }

        public DateTime Manufacture_Year { get; set; }

        public bool IsDeleted { get; set; }

        //Collections

        //One user's vehicle can be used by his multipled Published Rides
        public ICollection<PublishRide> Published_Rides { get; set; } = new HashSet<PublishRide>();

    }
}
