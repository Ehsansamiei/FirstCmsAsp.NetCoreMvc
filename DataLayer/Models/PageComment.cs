using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class PageComment
    {

        [Key]
        public int CommentId { get; set; }


        [Display(Name = "News")]
        [Required(ErrorMessage = "Please Enter {0} ")]
        public int PageId { get; set; }


        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please Enter {0} ")]
        [MaxLength(100)]
        public string Name { get; set; }


        [Display(Name = "Email")]
        [MaxLength(150)]
        public string Email { get; set; }


        [Display(Name = "Site")]
        [MaxLength(150)]
        public string Website { get; set; }


        [Display(Name = "Comment")]
        [Required(ErrorMessage = "Please Enter {0} ")]
        [MaxLength(500)]
        public string Comment { get; set; }


        [Display(Name = "Create date")]
        public DateTime CreateDate { get; set; }


        // Navigation Property
        // Each page can have many comments and a Comment blong to just one page
        public virtual Page page { get; set; }


        public PageComment()
        {

        }

    }
}
