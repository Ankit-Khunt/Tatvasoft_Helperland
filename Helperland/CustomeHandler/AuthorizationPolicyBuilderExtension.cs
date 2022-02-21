using Microsoft.AspNetCore.Authorization;

namespace Helperland.CustomeHandler
{
    public static class AuthorizationPolicyBuilderExtension
    {
        public static AuthorizationPolicyBuilder UseRequireCustomeClaim(this AuthorizationPolicyBuilder builder , string claimType )
        {
            builder.AddRequirements(new CustomUserRequireClaim(claimType));
            return builder;
        }
    }
}
    