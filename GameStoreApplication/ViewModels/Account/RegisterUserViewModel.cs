namespace GameStoreApplication.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterUserViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string FullName { get; set; }

        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}$")]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
