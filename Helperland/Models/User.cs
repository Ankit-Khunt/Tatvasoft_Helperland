using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Helperland.Models
{
    public partial class User
    {
        public User()
        {
            FavoriteAndBlockedTargetUser = new HashSet<FavoriteAndBlocked>();
            FavoriteAndBlockedUser = new HashSet<FavoriteAndBlocked>();
            RatingRatingFromNavigation = new HashSet<Rating>();
            RatingRatingToNavigation = new HashSet<Rating>();
            ServiceRequestServiceProvider = new HashSet<ServiceRequest>();
            ServiceRequestUser = new HashSet<ServiceRequest>();
            UserAddress = new HashSet<UserAddress>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public int UserTypeId { get; set; }
        public int? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string UserProfilePicture { get; set; }
        public bool IsRegisteredUser { get; set; }
        public string PaymentGatewayUserRef { get; set; }
        public string ZipCode { get; set; }
        public bool WorksWithPets { get; set; }
        public int? LanguageId { get; set; }
        public int? NationalityId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int? Status { get; set; }
        public string BankTokenId { get; set; }
        public string TaxNo { get; set; }

        public ICollection<FavoriteAndBlocked> FavoriteAndBlockedTargetUser { get; set; }
        public ICollection<FavoriteAndBlocked> FavoriteAndBlockedUser { get; set; }
        public ICollection<Rating> RatingRatingFromNavigation { get; set; }
        public ICollection<Rating> RatingRatingToNavigation { get; set; }
        public ICollection<ServiceRequest> ServiceRequestServiceProvider { get; set; }
        public ICollection<ServiceRequest> ServiceRequestUser { get; set; }
        public ICollection<UserAddress> UserAddress { get; set; }
    }
}
