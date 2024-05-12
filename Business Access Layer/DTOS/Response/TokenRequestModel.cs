using System.ComponentModel.DataAnnotations;

namespace Business_Access_Layer.DTOS.Response
{
    public class TokenRequestModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
