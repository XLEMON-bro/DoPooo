using System.ComponentModel.DataAnnotations;

namespace DB.Entities
{
    public class User
    {
        [Required]
        public int UserId { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        public string Name { get; set; }

        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        public string Surname { get; set; }
    }
}
