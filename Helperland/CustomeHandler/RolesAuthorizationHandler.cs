using Helperland.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.CustomeHandler
{
    public class RolesAuthorizationHandler :AuthorizationHandler<RolesAuthorizationRequirement>,IAuthorizationHandler
    {
        public HelperlandContext _helperlandContext;
        public RolesAuthorizationHandler(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;

        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesAuthorizationRequirement requirement)
        {
            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var validRole = false;
            if(requirement.AllowedRoles == null || requirement.AllowedRoles.Any() == false)
            {
               validRole = true;
            }
            else
            {
                var claims=context.User.Claims;
                var userId = claims.FirstOrDefault(c => c.Type == "userId").Value;
                var roles=requirement.AllowedRoles;
                validRole =  _helperlandContext.User.Where(p => roles.Contains(p.UserTypeId.ToString()) && p.UserId.ToString()==userId).Any();
            }

            if (validRole)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;

        }
    }
}
