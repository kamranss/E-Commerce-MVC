using System.ComponentModel.DataAnnotations;

namespace AllUp2.ViewModels.Account
{
    public class ChangePasswordVM
    {
        [Required,
        DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Required,
        DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required,
        DataType(DataType.Password),
        Compare(nameof(NewPassword))]
        public string NewConfirmedPAssword { get; set; }
    }
}
