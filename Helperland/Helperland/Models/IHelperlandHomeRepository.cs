using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models
{
    public interface IHelperlandHomeRepository
    {
        ContactUs Add(ContactUs ContactUs);
    }
}
