using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class PageGroupRepository : IPageGroupRepository
    {

        private readonly DrakeCmsContext _db;
        public PageGroupRepository(DrakeCmsContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IEnumerable<PageGroup> GetAllGroups()
        {
            return _db.PageGroup;
        }

        public PageGroup GetPageGroupById(int GroupId)
        {
            return _db.PageGroup.Find(GroupId);
        }

        public bool InsertGroup(PageGroup pageGroup)
        {
            try
            {

                _db.PageGroup.Add(pageGroup);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateGroup(PageGroup pageGroup)
        {
            //_db.Attach(pageGroup);
            Console.WriteLine($"Searching for GroupId: {pageGroup.GroupId}");

            _db.Entry(pageGroup).State = EntityState.Modified;
            //await _db.SaveChangesAsync();
            return true;
        }

        public bool DeleteGroup(int GroupId)
        {
            try
            {
                var group = GetPageGroupById(GroupId);
                DeleteGroup(group);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteGroup(PageGroup pageGroup)
        {
            try
            {
                _db.Entry(pageGroup).State = EntityState.Deleted;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public IEnumerable<ShowGroupsViewModel> GetGroupForView()
        {
            return _db.PageGroup.Select(g => new ShowGroupsViewModel
            {
                GroupId = g.GroupId,
                GroupTitile = g.GroupTitile,
                PageCount = g.Pages.Count
            });
        }


        public void Dispose()
        {
            _db.Dispose();
        }

    }
}
