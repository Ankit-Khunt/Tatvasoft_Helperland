using System;
using System.Collections.Generic;

namespace Helperland.Models
{
    public partial class FavoriteAndBlocked
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TargetUserId { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsBlocked { get; set; }

        public User TargetUser { get; set; }
        public User User { get; set; }
    }
}
