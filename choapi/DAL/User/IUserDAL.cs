using choapi.Models;

namespace choapi.DAL
{
    public interface IUserDAL
    {
        Users Add(Users user);

        Users? GetUserByUsername(string username);

        Users? GetUser(int id);

        Users Update(Users model);

        void Delete(Users model);
    }
}
