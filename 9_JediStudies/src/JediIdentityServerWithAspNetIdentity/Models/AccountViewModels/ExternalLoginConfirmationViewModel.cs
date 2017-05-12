using System.ComponentModel.DataAnnotations;

namespace JediIdentityServerWithAspNetIdentity.Models.AccountViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
