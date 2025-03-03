using Microsoft.AspNetCore.Http;

namespace DrakeCms.Areas.Admin.Dtos
{
    public class PageDto
    {
        public int PageId { get; set; }
        public int GroupId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Text { get; set; }
        public bool ShowInSlider { get; set; }
        public IFormFile ImageFile { get; set; } // برای گرفتن فایل عکس از ویو
    }
}
