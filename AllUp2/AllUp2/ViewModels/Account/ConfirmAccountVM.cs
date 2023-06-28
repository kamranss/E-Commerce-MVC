using System.ComponentModel.DataAnnotations;

namespace AllUp2.ViewModels.Account
{
    public class ConfirmAccountVM
    {
        public string Email { get; set; }
        [Required]
        public string OTP { get; set; }


        public ConfirmAccountVM()
        {

        }
        public ConfirmAccountVM(string email)
        {
            Email = email;
        }
    }
}
