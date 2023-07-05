using System.ComponentModel.DataAnnotations;

namespace AllUp2.ViewModels.Account
{
    public class ForgetPasswordVM
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}

