using choapi.Models;

namespace choapi.DAL
{
    public class SaveEstablishmentDAL : ISaveEstablishmentDAL
    {
        private readonly ChoDBContext _context;

        public SaveEstablishmentDAL(ChoDBContext choDBContext)
        {
            _context = choDBContext;
        }

        public SaveEstablishment Add(SaveEstablishment model)
        {
            _context.SaveEstablishment.Add(model);

            _context.SaveChanges();

            return model;
        }

        public SaveEstablishment Update(SaveEstablishment model)
        {
            _context.SaveEstablishment.Update(model);

            _context.SaveChanges();

            return model;
        }

        public SaveEstablishment Delete(SaveEstablishment model)
        {
            _context.SaveEstablishment.Update(model);

            _context.SaveChanges();

            return model;
        }

        public SaveEstablishment? Get(int id)
        {
            return _context.SaveEstablishment.FirstOrDefault(s => s.SaveEstablishment_Id == id && s.Is_Deleted != true);
        }

        public List<SaveEstablishment>? GetByUserId(int id)
        {
            return _context.SaveEstablishment.Where(s => s.User_Id  == id && s.Is_Deleted != true).ToList();
        }

        public List<int> GetSaveEstablishmentOfUser(int id)
        {
            return _context.SaveEstablishment.Where(s => s.Establishment_Id == id && s.Is_Deleted == true).Select(s => s.User_Id).ToList();
        }
    }
}
