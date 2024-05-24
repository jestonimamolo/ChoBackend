﻿using choapi.Models;

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

        public Availability Add(Availability model)
        {
            _context.Availability.Add(model);

            _context.SaveChanges();

            return model;
        }

        public Availability? GetAvailability(int id)
        {
            return _context.Availability.FirstOrDefault(a => a.Availability_Id == id);
        }

        public void DeleteAvailability(Availability model)
        {
            _context.Availability.Remove(model);

            _context.SaveChanges();
        }

        public Availability UpdateAvailability(Availability model)
        {
            _context.Availability.Update(model);

            _context.SaveChanges();

            return model;
        }

        public List<Availability>? GetAvailabilities(int establishmentId)
        {
            return _context.Availability.Where(a => a.Establishment_Id == establishmentId).ToList();
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

        public List<NonOperatingHours>? GetNonOperatingHoursByEstablishmentId(int id)
        {
            return _context.NonOperatingHours.Where(n => n.Establishment_Id == id).ToList();
        }

        public EstablishmentCuisines Add(EstablishmentCuisines model)
        {
            _context.EstablishmentCuisines.Add(model);

            _context.SaveChanges();

            return model;
        }

        public EstablishmentCuisines UpdateCuisine(EstablishmentCuisines model)
        {
            _context.EstablishmentCuisines.Update(model);

            _context.SaveChanges();

            return model;
        }

        public void DeleteCuisine(EstablishmentCuisines model)
        {
            _context.EstablishmentCuisines.Remove(model);

            _context.SaveChanges();
        }

        public EstablishmentCuisines? GetEstablishmentCuisine(int id)
        {
            return _context.EstablishmentCuisines.FirstOrDefault(c => c.EstablishmentCuisine_Id == id);
        }

        public List<EstablishmentCuisines>? GetEstablishmentCuisines(int? establishment)
        {
            if (establishment == null)
            {
                return _context.EstablishmentCuisines.ToList();
            }
            else
            {
                return _context.EstablishmentCuisines.Where(c => c.Establishment_Id == establishment).ToList();
            }
        }

        public EstablishmentBookType Add(EstablishmentBookType model)
        {
            _context.RestaurantBookType.Add(model);

            _context.SaveChanges();

            return model;
        }

        public List<EstablishmentBookType>? GetBookTypes(int? restaurantId)
        {
            if (restaurantId == null)
            {
                return _context.RestaurantBookType.Where(c => c.Is_Deleted != true).ToList();
            }
            else
            {
                return _context.RestaurantBookType.Where(c => c.Establishment_Id == restaurantId && c.Is_Deleted != true).ToList();
            }
        }

        public EstablishmentBookType? GetBookType(int id)
        {
            return _context.RestaurantBookType.FirstOrDefault(b => b.EstablishmentBookType_Id == id && b.Is_Deleted != true);
        }

        public EstablishmentBookType UpdateBookType(EstablishmentBookType model)
        {
            _context.RestaurantBookType.Update(model);

            _context.SaveChanges();

            return model;
        }
    }
}
