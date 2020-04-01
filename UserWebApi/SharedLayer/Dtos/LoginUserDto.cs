using System.ComponentModel.DataAnnotations;

namespace UserWebApi.SharedLayer.Dtos
{
    public class LoginUserDto
    {
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&]).{8,}")]
        public string Password { get; set; }
    }
}
