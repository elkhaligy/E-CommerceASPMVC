using System.ComponentModel.DataAnnotations;

namespace Project.ViewModel
{
    public class AdminLoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public required string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }

}
