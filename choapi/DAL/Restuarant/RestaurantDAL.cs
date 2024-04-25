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

        public Restaurants Add(Restaurants model)
        {
            _context.Restaurants.Add(model);

            _context.SaveChanges();

            return model;
        }

        public List<RestaurantImages>? AddImages(List<RestaurantImages> restaurantImages)
        {
            if (restaurantImages != null)
            {
                _context.RestaurantImages.AddRange(restaurantImages);

                _context.SaveChanges();

                return restaurantImages;
            }

            return null;
        }

        public Restaurants? GetRestaurant(int id)
        {
            return _context.Restaurants.FirstOrDefault(r => r.Restaurant_Id == id);
        }

        public List<RestaurantImages>? GetRestaurantImages(int id)
        {
            return _context.RestaurantImages.Where(i => i.Restaurant_Id == id).ToList();
        }

        public List<Restaurants>? GetRestaurants(int? userId)
        {
            if (userId == null)
                return _context.Restaurants.ToList();
            else
                return _context.Restaurants.Where(r => r.User_Id == userId).ToList();
        }

        public Menus Add(Menus model)
        {
            _context.Menus.Add(model);

            _context.SaveChanges();

            return model;
        }

        public List<Menus>? GetMenus(int restaurantId)
        {
            return _context.Menus.Where(m => m.Restaurant_Id == restaurantId).ToList();
        }

        public Menus? GetMenu(int id)
        {
            return _context.Menus.FirstOrDefault(m => m.Menu_Id == id);
        }

        public Menus UpdateMenu(Menus model)
        {
            _context.Menus.Update(model);

            _context.SaveChanges();

            return model;
        }

        public void DeleteMenu(Menus model)
        {
            _context.Menus.Remove(model);

            _context.SaveChanges();
        }

        public RestaurantAvailability Add(RestaurantAvailability model)
        {
            _context.RestaurantAvailability.Add(model);

            _context.SaveChanges();

            return model;
        }

        public RestaurantAvailability? GetAvailability(int id)
        {
            return _context.RestaurantAvailability.FirstOrDefault(a => a.RestaurantAvailability_Id == id);
        }

        public void DeleteAvailability(RestaurantAvailability model)
        {
            _context.RestaurantAvailability.Remove(model);

            _context.SaveChanges();
        }

        public RestaurantAvailability UpdateAvailability(RestaurantAvailability model)
        {
            _context.RestaurantAvailability.Update(model);

            _context.SaveChanges();

            return model;
        }

        public List<RestaurantAvailability>? GetAvailabilities(int restaurantId)
        {
            return _context.RestaurantAvailability.Where(a => a.Restaurant_Id == restaurantId).ToList();
        }

        public RestaurantCuisines Add(RestaurantCuisines model)
        {
            _context.RestaurantCuisines.Add(model);

            _context.SaveChanges();

            return model;
        }

        public List<RestaurantCuisines>? GetRestaurantCuicines(int? restaurantId)
        {
            if (restaurantId == null)
            {
                return _context.RestaurantCuisines.ToList();
            }
            else
            {
                return _context.RestaurantCuisines.Where(c => c.Restaurant_Id == restaurantId).ToList();
            }
        }

        public RestaurantBookType Add(RestaurantBookType model)
        {
            _context.RestaurantBookType.Add(model);

            _context.SaveChanges();

            return model;
        }

        public List<RestaurantBookType>? GetBookTypes(int? restaurantId)
        {
            if (restaurantId == null)
            {
                return _context.RestaurantBookType.ToList();
            }
            else
            {
                return _context.RestaurantBookType.Where(c => c.Restaurant_Id == restaurantId).ToList();
            }
        }

        public RestaurantBookType? GetBookType(int id)
        {
            return _context.RestaurantBookType.FirstOrDefault(b => b.RestaurantBookType_Id == id);
        }

        public RestaurantBookType UpdateBookType(RestaurantBookType model)
        {
            _context.RestaurantBookType.Update(model);

            _context.SaveChanges();

            return model;
        }
    }
}
