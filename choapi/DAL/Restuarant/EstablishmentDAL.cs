using choapi.Models;

namespace choapi.DAL
{
    public class EstablishmentDAL : IEstablishmentDAL
    {
        private readonly ChoDBContext _context;

        public EstablishmentDAL(ChoDBContext choDBContext)
        {
            _context = choDBContext;
        }

        public Establishment Add(Establishment model)
        {
            _context.Establishment.Add(model);

            _context.SaveChanges();

            return model;
        }

        public Establishment Update(Establishment model)
        {
            _context.Establishment.Update(model);

            _context.SaveChanges();

            return model;
        }

        public EstablishmentImages AddImage(EstablishmentImages model)
        {
            _context.EstablishmentImages.Add(model);

            _context.SaveChanges();

            return model;
        }

        public List<EstablishmentImages>? AddImages(List<EstablishmentImages> model)
        {
            if (model != null)
            {
                _context.EstablishmentImages.AddRange(model);

                _context.SaveChanges();

                return model;
            }

            return null;
        }

        public Establishment? GetEstablishment(int id)
        {
            return _context.Establishment.FirstOrDefault(r => r.Establishment_Id == id);
        }

        public EstablishmentImages? GetEstablishmentImage(int id)
        {
            return _context.EstablishmentImages.FirstOrDefault(r => r.EstablishmentImage_Id == id);
        }

        public List<EstablishmentImages>? GetEstablishmentImages(int id)
        {
            return _context.EstablishmentImages.Where(i => i.Establishment_Id == id).ToList();
        }

        public List<Establishment>? GetEstablishments(int? userId)
        {
            if (userId == null)
                return _context.Establishment.ToList();
            else
                return _context.Establishment.Where(r => r.User_Id == userId).ToList();
        }

        public List<Establishment>? GetEstablishmentsByCategoryId(int id)
        {
            return _context.Establishment.Where(r => r.Category_Id == id).ToList();
        }

        public EstablishmentImages UpdateImage(EstablishmentImages model)
        {
            _context.EstablishmentImages.Update(model);

            _context.SaveChanges();

            return model;
        }

        public void DeleteImage(EstablishmentImages model)
        {
            _context.EstablishmentImages.Remove(model);

            _context.SaveChanges();
        }

        public Menus Add(Menus model)
        {
            _context.Menus.Add(model);

            _context.SaveChanges();

            return model;
        }

        public List<Menus>? GetMenus(int establishmentId)
        {
            return _context.Menus.Where(m => m.Establishment_Id == establishmentId).ToList();
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

        public List<EstablishmentCuisines>? GetEstablishmentCuisines(int? establishmentId)
        {
            if (establishmentId == null)
            {
                return _context.EstablishmentCuisines.ToList();
            }
            else
            {
                return _context.EstablishmentCuisines.Where(c => c.Establishment_Id == establishmentId).ToList();
            }
        }

        public EstablishmentBookType Add(EstablishmentBookType model)
        {
            _context.EstablishmentBookType.Add(model);

            _context.SaveChanges();

            return model;
        }

        public List<EstablishmentBookType>? GetBookTypes(int? establishmentId)
        {
            if (establishmentId == null)
            {
                return _context.EstablishmentBookType.Where(c => c.Is_Deleted != true).ToList();
            }
            else
            {
                return _context.EstablishmentBookType.Where(c => c.Establishment_Id == establishmentId && c.Is_Deleted != true).ToList();
            }
        }

        public EstablishmentBookType? GetBookType(int id)
        {
            return _context.EstablishmentBookType.FirstOrDefault(b => b.EstablishmentBookType_Id == id && b.Is_Deleted != true);
        }

        public EstablishmentBookType UpdateBookType(EstablishmentBookType model)
        {
            _context.EstablishmentBookType.Update(model);

            _context.SaveChanges();

            return model;
        }
    }
}
