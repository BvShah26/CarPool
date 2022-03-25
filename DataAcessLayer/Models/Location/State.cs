using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.Models.Location
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<City> Cities { get; set; }

    }
}
