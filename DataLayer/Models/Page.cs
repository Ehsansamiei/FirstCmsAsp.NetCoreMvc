using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Page
    {
        [Key]
        public int PageId { get; set; }


        [Display(Name = "Group Page")]
        [Required(ErrorMessage = "Please Enter {0} ")]
        public int GroupId { get; set; }


        [Display(Name = "Title")]
        [Required(ErrorMessage = "Please Enter {0} ")]
        [MaxLength(150)]
        public string Title { get; set; }


        [Display(Name = "Short descrioption")]
        [Required(ErrorMessage = "Please Enter {0} ")]
        [MaxLength(250)]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }


        [Display(Name = "Text")]
        [Required(ErrorMessage = "Please Enter {0} ")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }


        [Display(Name = "Visit")]
        public int Visit { get; set; }


        [Display(Name = "Image")]
        public string ImageName { get; set; }


        [Display(Name = "Show in slider")]
        public bool ShowInSlider { get; set; }


        [Display(Name = "Create date")]
        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime CreateDate { get; set; }


        //Navigation Property
        // Each page blongs to one pageGroup and each pageGroup can have many pages
        public virtual PageGroup pageGroup { get; set; }


        // Each page can have many comments and a Comment blong to just one page
        public virtual List<PageComment> pageComment { get; set; }
        public Page()
        {

        }
    }
}
