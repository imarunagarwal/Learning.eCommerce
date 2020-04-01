using System;
using System.ComponentModel.DataAnnotations;
using static UserWebApi.SharedLayer.Enums.Enums;

namespace UserWebApi.WebApi.ViewModels
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required,DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [Required, RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&]).{8,}")]
        public string Password { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public int Age { get; set; }

        [Required, RegularExpression(@"^{10}[0-9]{10}$")]
        public string PhoneNo { get; set; }
    }
}
