using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using WebApp.Models.Identity;
using WebApp.Services;

namespace WebApp.Factories
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<CustomIdentityUser>
    {

        /// <summary>
        /// skapa en claim för att hantera rollerna automatiskt så den första blir admin och resten users
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="optionsAccessor"></param>
        /// 


        //private readonly UserManager<CustomIdentityUser> userManager;
        //private readonly UserService _userService;
        //vill använda services men blir error lifetime scoped 

        public CustomClaimsPrincipalFactory(UserManager<CustomIdentityUser> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
            //_userService = userService;
            //this.userManager = userManager;
        }

        //protected override async Task<ClaimsIdentity> GenerateClaimsAsync(CustomIdentityUser user) // överskriva den standarden på generateclaimsasync
        //{
            //var claimsIdentity = await base.GenerateClaimsAsync(user);

            //var userProfileEntity = await _userService.GetUserProfileAsync(user.Id);
            //claimsIdentity.AddClaim(new Claim("DisplayName", $"{user.FirstName}")); // här vill vi hämta upp användarinformationen


            ////idk....
            //var userRole = await _seedService.GetRoleAsync(user.Id);
            //claimsIdentity.AddClaim(new Claim("Role", $"{userRole?.Name}"));

            //return claimsIdentity;
        //}
    }
}

