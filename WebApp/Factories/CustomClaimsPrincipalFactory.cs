using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using WebApp.Models.Identity;

namespace WebApp.Factories
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<CustomIdentityUser>
    {
        public CustomClaimsPrincipalFactory(UserManager<CustomIdentityUser> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(CustomIdentityUser user) // överskriva den standarden på generateclaimsasync
        {
            var claimsIdentity = await base.GenerateClaimsAsync(user);
            claimsIdentity.AddClaim(new Claim("DisplayName", $"{user.FirstName}")); // här vill vi hämta upp användarinformationen


            //Saras kod för automatisk rollhantering 
            var roles = await UserManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return claimsIdentity;
        }
    }
}

