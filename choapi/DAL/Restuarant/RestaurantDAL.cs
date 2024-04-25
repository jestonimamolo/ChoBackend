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

        public Restaurants Update(Restaurants model)
        {
            _context.Restaurants.Update(model);

            _context.SaveChanges();

            return model;
        }

        public RestaurantImages AddImage(RestaurantImages model)
        {
            _context.RestaurantImages.Add(model);

            _context.SaveChanges();

            return model;
        }

        public List<RestaurantImages>? AddImages(List<RestaurantImages> model)
        {
            if (model != null)
            {
                _context.RestaurantImages.AddRange(model);

                _context.SaveChanges();

                return model;
            }

            return null;
        }

        public Restaurants? GetRestaurant(int id)
        {
            return _context.Restaurants.FirstOrDefault(r => r.Restaurant_Id == id);
        }

        public RestaurantImages? GetRestaurantImage(int id)
        {
            return _context.RestaurantImages.FirstOrDefault(r => r.RestaurantImages_Id == id);
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

        public RestaurantImages UpdateImage(RestaurantImages model)
        {
            _context.RestaurantImages.Update(model);

            _context.SaveChanges();

            return model;
        }

        public void DeleteImage(RestaurantImages model)
        {
            _context.RestaurantImages.Remove(model);

            _context.SaveChanges();
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

        public NonOperatingHours Add(NonOperatingHours model)
        {
            _context.NonOperatingHours.Add(model);

            _context.SaveChanges();

            return model;
        }

        public void Delete(NonOperatingHours model)
        {
            _context.NonOperatingHours.Remove(model);

            _context.SaveChanges();
        }

        public NonOperatingHours Update(NonOperatingHours model)
        {
            _context.NonOperatingHours.Update(model);

            _context.SaveChanges();

            return model;
        }

        public NonOperatingHours? GetNonOperatingHours(int id)
        {
            return _context.NonOperatingHours.FirstOrDefault(n => n.NonOperatingHours_Id == id);
        }

        public List<NonOperatingHours>? GetNonOperatingHoursByRestaurantId(int id)
        {
            return _context.NonOperatingHours.Where(n => n.Restaurant_Id == id).ToList();
        }

        public RestaurantCuisines Add(RestaurantCuisines model)
        {
            _context.RestaurantCuisines.Add(model);

            _context.SaveChanges();

            return model;
        }

        public RestaurantCuisines UpdateCuisine(RestaurantCuisines model)
        {
            _context.RestaurantCuisines.Update(model);

            _context.SaveChanges();

            return model;
        }

        public void DeleteCuisine(RestaurantCuisines model)
        {
            _context.RestaurantCuisines.Remove(model);

            _context.SaveChanges();
        }

        public RestaurantCuisines? GetRestaurantCuisine(int id)
        {
            return _context.RestaurantCuisines.FirstOrDefault(c => c.RestaurantCuisine_Id == id);
        }

        public List<RestaurantCuisines>? GetRestaurantCuisines(int? restaurantId)
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
