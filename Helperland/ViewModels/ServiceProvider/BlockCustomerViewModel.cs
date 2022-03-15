using Helperland.Models;
using System.Collections.Generic;

namespace Helperland.ViewModels.ServiceProvider
{
    public class BlockCustomerViewModel
    {
        public IEnumerable<User> allCustomers { get; set; }
        public IEnumerable<User> blockedCustomers { get; set; }
    }
}
