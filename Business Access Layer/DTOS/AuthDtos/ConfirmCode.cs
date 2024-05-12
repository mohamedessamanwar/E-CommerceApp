using System.ComponentModel.DataAnnotations;

namespace Business_Access_Layer.DTOS.AuthDtos
{
    public class ConfirmCode
    {
        public string Code { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
