using System;
using System.Collections.Generic;

namespace Helperland.Models
{
    public partial class State
    {
        public State()   
        {
            City = new HashSet<City>();       // for child table of foregin key
        }

        public int Id { get; set; }
        public string StateName { get; set; }

        public ICollection<City> City { get; set; }  //for foregin key call
    }
}
