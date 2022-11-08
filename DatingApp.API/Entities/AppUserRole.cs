using Microsoft.AspNetCore.Identity;

namespace DatingApp.API.Entities
{
    public class AppUserRole : IdentityUserRole<int>
    {
        public AppUser user { get; set; }
        public AppRole Role { get; set; }
    }
}