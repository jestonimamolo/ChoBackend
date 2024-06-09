using choapi.Models;

namespace choapi.DAL
{
    public interface IAppInfoDAL
    {
        AppInfo Add(AppInfo model);

        AppInfo Update(AppInfo model);

        AppInfo Delete(AppInfo model);

        AppInfo? Get(int id);
    }
}
