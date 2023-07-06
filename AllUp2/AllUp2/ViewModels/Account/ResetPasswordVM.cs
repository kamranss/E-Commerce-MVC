using System.ComponentModel.DataAnnotations;

namespace AllUp2.ViewModels.Account
{
    public class ResetPasswordVM
    {
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password),
        Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }
        public string Userid { get; set; }
        public string Token { get; set; }
    }
}
