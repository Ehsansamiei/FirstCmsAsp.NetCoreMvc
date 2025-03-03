using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IPageGroupRepository : IDisposable
    {
        IEnumerable<PageGroup> GetAllGroups();
        PageGroup GetPageGroupById(int GroupId);
        bool InsertGroup(PageGroup pageGroup);
        Task<bool> UpdateGroup(PageGroup pageGroup);
        bool DeleteGroup(int GroupId);
        bool DeleteGroup(PageGroup pageGroup);
        IEnumerable<ShowGroupsViewModel> GetGroupForView();

        Task SaveAsync();
    }
}
