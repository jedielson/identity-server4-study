using System.ComponentModel.DataAnnotations;

namespace JediIdentityServerWithAspNetIdentity.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
