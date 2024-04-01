using choapi.Models;

namespace choapi.DAL
{
    public interface IUserDAL
    {
        User? Add(User user);

        User? GetUserByUsername(string username);
    }
}
