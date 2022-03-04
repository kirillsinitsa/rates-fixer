using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RatesFixer.Authentication.Entities
{
    public class InternalUserData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "User Id is required.")]
        [Range(1, 100000)]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Hashed Password is required.")]
        public string HashedPassword { get; set; }
        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }

        public InternalUserData() { }

        public InternalUserData(int userId, string hashedPassword, string role)
        {
            UserId = userId;
            HashedPassword = hashedPassword;
            Role = role;
        }
    }
}
