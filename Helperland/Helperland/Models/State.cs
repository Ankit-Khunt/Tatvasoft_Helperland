using System;
using System.Collections.Generic;

namespace Helperland.Models
{
    public partial class State
    {
        public State()
        {
            City = new HashSet<City>();
        }

        public int Id { get; set; }
        public string StateName { get; set; }

        public ICollection<City> City { get; set; }
    }
}
