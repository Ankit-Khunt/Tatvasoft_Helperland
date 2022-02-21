using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;
namespace Helperland.CustomeHandler
{
    public class PolicyAuthorizationHandler :AuthorizationHandler<CustomUserRequireClaim>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomUserRequireClaim requirement)
        {
           if(context.User==null || !context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return Task.CompletedTask; 
            }

            var hasClaim = context.User.Claims.Any(x => x.Type == requirement.ClaimType);

            if (hasClaim)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            context.Fail();
            return Task.CompletedTask;
        }
    }
}
