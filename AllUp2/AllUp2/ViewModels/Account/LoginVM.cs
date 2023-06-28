using System.ComponentModel.DataAnnotations;

namespace AllUp2.ViewModels.Account
{
    public class LoginVM
    {
        [Required, StringLength(100)]
        public string UserNameOrEmail { get; set; }

        public bool RememberMe { get; set; }

        //[Required, EmailAddress, DataType(DataType.EmailAddress)]
        //public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
