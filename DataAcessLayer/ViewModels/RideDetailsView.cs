﻿using DataAcessLayer.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAcessLayer.ViewModels
{
    public class RideDetailsView
    {
        public int Id { get; set; }
        //to be delete 
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public string Publisher_Profile { get; set; }



        public string PickUp_LatLong { get; set; }
        public string DropOff_LatLong { get; set; }

        [Display(Name = "Date")]
        public DateTime JourneyDate { get; set; }

        public DateTime PickUp_Time { get; set; }
        public DateTime DropOff_Time { get; set; }


        public Boolean IsInstant_Approval { get; set; }
        public int Price_Seat { get; set; }

        public string Client_Departure_LatLong { get; set; }
        public string Client_Destination_LatLong { get; set; }

        public double Pickup_DifferDistance { get; set; }
        public double Drop_DifferDistance { get; set; }

        public string DepartureCity { get; set; }
        public string DestinationCity { get; set; }


    }
}
