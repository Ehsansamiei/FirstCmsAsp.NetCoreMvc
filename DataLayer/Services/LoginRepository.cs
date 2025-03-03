using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DrakeCmsContext _db;
        public LoginRepository(DrakeCmsContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }


        public bool IsExistUser(string UserName, string Password)
        {
            return _db.AdminLogin.Any(u => u.UserName == UserName && u.Password == Password);
        }


        public  async Task<AdminLogin> GetUserByName(string UserName)
        {
            return _db.AdminLogin.Where(u => u.UserName == UserName).FirstOrDefault();
        }

    }
}
