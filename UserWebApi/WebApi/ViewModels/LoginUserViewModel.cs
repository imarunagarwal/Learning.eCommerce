using System.ComponentModel.DataAnnotations;

namespace UserWebApi.WebApi.ViewModels
{
    /// <summary>
    /// The login viewmodel
    /// </summary>
    public class LoginUserViewModel
    {
        /// <summary>
        /// The Email id parameter for login
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        /// <summary>
        /// The password parameter for login
        /// </summary>
        [Required]
        [RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&]).{8,}")]
        public string Password { get; set; }
    }
}
