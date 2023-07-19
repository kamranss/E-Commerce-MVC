using System.ComponentModel.DataAnnotations;

namespace AllUp2.ViewModels.Home
{
    public class CheckOutVM
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address Line 1 is required")]
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string Country { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "Zip Code is required")]
        public string ZipCode { get; set; }

        public List<BasketVM> BasketItems { get; set; }

    }
}
