using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.Models.Location
{
    public class Country
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public ICollection<State> States { get; set; }
    }
}
