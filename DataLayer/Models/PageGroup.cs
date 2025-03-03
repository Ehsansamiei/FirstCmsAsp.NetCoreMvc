using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class PageGroup
    {
        [Key]
        public int GroupId { get; set; }

        [Display(Name = "Page title")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [MaxLength(100)]
        public string GroupTitile { get; set; }

        //Navigation Property
        // Each page blongs to one pageGroup and each pageGroup can have many pages
        public virtual ICollection<Page> Pages { get; set; }

        public PageGroup()
        {

        }
    }
}
