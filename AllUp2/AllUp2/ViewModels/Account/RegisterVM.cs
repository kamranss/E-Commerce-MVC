using System.ComponentModel.DataAnnotations;

namespace AllUp2.ViewModels.Account
{
    public class RegisterVM
    {
        //[Required, StringLength(100)]
        //public string Name { get; set; }

        //[Required, StringLength(100)]
        //public string Surname { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; }
        public string UserName { get; set; }

        [Required, EmailAddress, DataType(DataType.EmailAddress)] // this is validating the email format both client and server side
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
