using Microsoft.AspNetCore.Identity;

namespace AllUp2.ViewModels.AdminVM.Role
{
    public class RoleVM
    {
        public List<IdentityRole> Roles { get; set; }
        public IList<string> UserRole { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
    }
}
