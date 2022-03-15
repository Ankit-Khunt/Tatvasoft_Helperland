using Helperland.Models;
using System;

namespace Helperland.ViewModels.CustomerService
{
    public class RatingViewModel
    {
        public int RatingId { get; set; }
        public int ServiceRequestId { get; set; }
        public int RatingFrom { get; set; }
        public int RatingTo { get; set; }
        public decimal Ratings { get; set; }
        public string Comments { get; set; }
        public DateTime RatingDate { get; set; }
        public decimal OnTimeArrival { get; set; }
        public decimal Friendly { get; set; }
        public decimal QualityOfService { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string UserProfilePicture { get; set; }
    }
}
