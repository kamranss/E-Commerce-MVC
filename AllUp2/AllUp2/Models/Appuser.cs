using Microsoft.AspNetCore.Identity;

namespace AllUp2.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public string? OTP { get; set; }
        public string? ConnectionId { get; set; }
    }
}
