using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class AdminLogin
    {
        [Key]
        public int LoginId { get; set; }


        [Display(Name ="User Name")]
        [Required(ErrorMessage ="Please Enter {0}")]
        [MaxLength(150)]
        public string UserName { get; set; }


        [Display(Name ="Email")]
        [Required(ErrorMessage ="Please Enter {0}")]
        [MaxLength(200)]
        public string Email { get; set; }


        [Display(Name ="'Password")]
        [Required(ErrorMessage ="Please Enter {0}")]
        [MaxLength(150)]
        public string Password { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [MaxLength(50)]
        public string Role { get; set; }
    }
}
