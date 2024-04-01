using choapi.Models;

namespace choapi.DAL
{
    public class RestaurantDAL : IRestaurantDAL
    {
        private readonly ChoDBContext _context;

        public RestaurantDAL(ChoDBContext choDBContext)
        {
            _context = choDBContext;
        }

        public Restaurant? Add(Restaurant model)
        {
            if (model != null)
            {
                _context.Restaurant.Add(model);

                _context.SaveChanges();
            }
            return model;
        }

        public Restaurant? GetRestaurant(int id)
        {
            return _context.Restaurant.FirstOrDefault(r => r.Restaurant_Id == id);
        }

        public List<Restaurant>? GetRestaurants(int userIdManager)
        {
            return _context.Restaurant.Where(r => r.User_Id_Manager == userIdManager).ToList();
        }
    }
}
