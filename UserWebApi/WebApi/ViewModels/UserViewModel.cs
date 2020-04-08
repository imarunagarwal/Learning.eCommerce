using System;
using System.ComponentModel.DataAnnotations;
using static UserWebApi.SharedLayer.Enums.Enums;

namespace UserWebApi.WebApi.ViewModels
{
    /// <summary>
    /// The user viewmodel
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// The User Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// The first name parameter of user viewmodel
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// The last name parameter of user viewmodel
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The emaild parameter of user viewmodel
        /// </summary>
        [Required,DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        /// <summary>
        /// The password parameter of user viewmodel
        /// </summary>
        [Required, RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&]).{8,}")]
        public string Password { get; set; }

        /// <summary>
        /// The gender parameter of user viewmodel
        /// </summary>
        [Required]
        public Gender Gender { get; set; }

        /// <summary>
        /// The age parameter of user viewmodel
        /// </summary>
        [Required]
        public int Age { get; set; }

        /// <summary>
        /// The phone number parameter of user viewmodel
        /// </summary>
        [Required, RegularExpression(@"^[0-9]{10}$")]
        public string PhoneNo { get; set; }
    }
}
