using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.CustomeHandler
{
   
        public class CustomUserRequireClaim : IAuthorizationRequirement
        {
            public string ClaimType { get; }
            public CustomUserRequireClaim(string claimType) 
            {
                ClaimType = claimType;
            }
        }
}

