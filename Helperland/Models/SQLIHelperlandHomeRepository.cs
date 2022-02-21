using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models
{
    public class SQLIHelperlandHomeRepository :IHelperlandHomeRepository
    {
        private readonly HelperlandContext contex;
        public SQLIHelperlandHomeRepository(HelperlandContext contex)
        {
            this.contex = contex;
        }
        public ContactUs Add(ContactUs ContactUs)
        {
            contex.ContactUs.Add(ContactUs);
            contex.SaveChanges();
            return ContactUs;
        }

    }
}
