using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTO.AuthDTOs
{
    public class UserLoginRequest
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
