using System.ComponentModel.DataAnnotations;

namespace BusinessAccessLayer.DTOS.AuthDtos
{
    public class TokenRequestModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
