using Helperland.Models;
using System.Collections.Generic;
using System.Linq;

namespace Helperland.ViewModels.CustomerService
{
    public class FavouriteProvidersViewModel
    {
        public IEnumerable<User> ServiceProviders { get; set; }
        public IEnumerable<Rating> Rating { get; set; }
        public List<int> FavouriteSpIds { get; set; }
        public List<int> BlockedSpIds { get; set; }

        public int totalCleanings { get; set; }
    }
}
