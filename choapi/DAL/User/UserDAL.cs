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

        public Users Add(Users user)
        {
            _context.Users.Add(user);

            _context.SaveChanges();

            return user;
        }

        public Users? GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public Users? GetUser(int id)
        {
            return _context.Users.FirstOrDefault(u => u.User_Id == id);
        }

        public Users Update(Users model)
        {
            _context.Users.Update(model);

            _context.SaveChanges();

            return model;
        }

        public void Delete(Users model)
        {
            _context.Users.Remove(model);

            _context.SaveChanges();
        }
    }
}
