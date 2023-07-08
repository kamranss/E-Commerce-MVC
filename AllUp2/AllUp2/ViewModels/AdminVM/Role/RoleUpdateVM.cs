using AllUp2.Models;
using Microsoft.AspNetCore.Identity;

namespace AllUp2.ViewModels.AdminVM.Role
{
    public class RoleUpdateVM
    {
        public AppUser User { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}
