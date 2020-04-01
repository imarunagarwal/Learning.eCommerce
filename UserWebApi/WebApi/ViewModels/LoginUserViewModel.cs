using System.ComponentModel.DataAnnotations;

namespace UserWebApi.WebApi.ViewModels
{
    public class LoginUserViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [Required]
        [RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&]).{8,}")]
        public string Password { get; set; }
    }
}
