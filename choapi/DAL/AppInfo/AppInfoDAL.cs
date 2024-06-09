using choapi.Models;
using Microsoft.EntityFrameworkCore;

namespace choapi.DAL
{
    public class AppInfoDAL : IAppInfoDAL
    {
        private readonly ChoDBContext _context;

        public AppInfoDAL(ChoDBContext choDBContext)
        {
            _context = choDBContext;
        }

        public AppInfo Add(AppInfo model)
        {
            _context.AppInfo.Add(model);

            _context.SaveChanges();

            return model;
        }

        public AppInfo Delete(AppInfo model)
        {
            _context.AppInfo.Update(model);

            _context.SaveChanges();

            return model;
        }

        public AppInfo? Get(int id)
        {
            return _context.AppInfo.FirstOrDefault(p => p.Id == id && p.Is_Active != false);
        }

        public AppInfo Update(AppInfo model)
        {
            _context.AppInfo.Update(model);

            _context.SaveChanges();

            return model;
        }
    }
}
