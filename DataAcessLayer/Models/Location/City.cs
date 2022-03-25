using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.Models.Location
{
    public class City
    {
        public int Id { get; set; }
        public string CityName { get; set; }

        public State State { get; set; }
        public int StateId { get; set; }

    }
}
