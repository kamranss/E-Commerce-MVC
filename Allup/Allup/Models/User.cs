using Microsoft.AspNetCore.Identity;
using System.Security.Principal;

namespace Allup.Models
{
    public class User:IdentityUser
    {
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public string OTP { get; set; }
        public string? ConnectionId { get; set; }
    }
}
