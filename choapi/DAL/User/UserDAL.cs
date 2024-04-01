using choapi.Models;

namespace choapi.DAL
{
    public class UserDAL : IUserDAL
    {
        private readonly ChoDBContext _context;

        public UserDAL(ChoDBContext choDBContext) 
        {
            _context = choDBContext;
        }

        public User? Add(User user)
        {
            if (user != null)
            {
                _context.User.Add(user);

                _context.SaveChanges();
            }

            return user;
        }

        public User? GetUserByUsername(string username)
        {
            return _context.User.FirstOrDefault(u => u.Username == username);
        }
    }
}
