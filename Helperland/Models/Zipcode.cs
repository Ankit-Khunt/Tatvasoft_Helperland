using System;
using System.Collections.Generic;

namespace Helperland.Models
{
    public partial class Zipcode
    {
        public int Id { get; set; }
        public string ZipcodeValue { get; set; }
        public int CityId { get; set; }

        public City City { get; set; }
    }
}
