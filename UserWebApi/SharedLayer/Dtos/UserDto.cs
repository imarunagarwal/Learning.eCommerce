using System;
using System.ComponentModel.DataAnnotations;
using static UserWebApi.SharedLayer.Enums.Enums;

namespace UserWebApi.SharedLayer.Dtos
{
    public class UserDto
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&]).{8,}")]
        public string Password { get; set; }

        public Gender Gender { get; set; }

        public int Age { get; set; }

        [RegularExpression(@"^[0-9]{10}$")]
        public string PhoneNo { get; set; }
    }
}
