using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class PageRepository : IPageRepository
    {

        private DrakeCmsContext _db;
        public PageRepository(DrakeCmsContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }


        public IEnumerable<Page> GetAllPage()
        {
            return _db.Page.Include(a => a.pageGroup);
        }

        public Page GetPage(int PageId)
        {
            return _db.Page.Find(PageId);
        }

        public bool InsertPage(Page page)
        {
            try
            {
                _db.Page.Add(page);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdatePage(Page page)
        {
            try
            {
                _db.Page.Update(page);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeletePage(Page page)
        {
            try
            {
                _db.Entry(page).State = EntityState.Deleted;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeletePage(int PageId)
        {
            try
            {
                var page = GetPage(PageId);
                DeletePage(page);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Page> PagesInSlider()
        {
            return _db.Page.Where(p => p.ShowInSlider == true);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}