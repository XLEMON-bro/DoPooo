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
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[/d])(?=.*?[#?!@$%^&*-]).{8,30}$", ErrorMessage = "Required 1 Up and lower case symbol\n1 digital and 1 non-char\nLenth at least 8")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        public string Surname { get; set; }
    }
}
