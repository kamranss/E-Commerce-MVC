using System.ComponentModel.DataAnnotations;

namespace AllUp2.ViewModels.Account
{
    public class ResetPasswordVM
    {
        [Required]
        [DataType(DataType.Password),
         Compare(nameof(Password))]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public int Userid { get; set; }
        public string Token { get; set; }
    }
}
