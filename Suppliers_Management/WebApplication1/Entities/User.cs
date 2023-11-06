using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)] 
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

    }
}
