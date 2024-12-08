using Ganss.Xss;
using System.ComponentModel.DataAnnotations;

namespace BusinessAccessLayer.DTOS.AuthDtos
{
    public class TokenRequestModel
    {
        private string email;
        [Required]
        public string Email { get => email; set => email = new HtmlSanitizer().Sanitize(value); }
        private string password;
        [Required]
        public string Password { get => password; set => password = new HtmlSanitizer().Sanitize(value); }
    }
}
