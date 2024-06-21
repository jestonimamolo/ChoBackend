using choapi.Models;

namespace choapi.DAL
{
    public class ManagerDAL : IManagerDAL
    {
        private readonly ChoDBContext _context;

        public ManagerDAL(ChoDBContext choDBContext)
        {
            _context = choDBContext;
        }

        public Manager Add(Manager model)
        {
            _context.Manager.Add(model);

            _context.SaveChanges();

            return model;
        }

        public Manager Update(Manager model)
        {
            _context.Manager.Update(model);

            _context.SaveChanges();

            return model;
        }

        public Manager Delete(Manager model)
        {
            _context.Manager.Update(model);

            _context.SaveChanges();

            return model;
        }

        public Manager? Get(int id)
        {
            return _context.Manager.FirstOrDefault(m => m.Manager_Id == id && m.Is_Deleted != true);
        }

        public List<Manager>? GetByEstablishmentId(int id)
        {
            return _context.Manager.Where(m => m.Establishment_Id == id && m.Is_Deleted != true).ToList();
        }        
    }
}
