using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class LoginViewModel
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [MaxLength(150)]
        public string UserName { get; set; }


        [Display(Name = "'Password")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [MaxLength(150)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name ="Remember me")]
        public bool RememberMe { get; set; }
    }
}
