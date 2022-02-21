using System;
using System.Collections.Generic;

namespace Helperland.Models
{
    public partial class ServiceRequestExtra
    {
        public int ServiceRequestExtraId { get; set; }
        public int ServiceRequestId { get; set; }
        public int ServiceExtraId { get; set; }

        public ServiceRequest ServiceRequest { get; set; }
    }
}
