using choapi.Models;

namespace choapi.DAL
{
    public class EstablishmentTableDAL : IEstablishmentTableDAL
    {
        private readonly ChoDBContext _context;

        public EstablishmentTableDAL(ChoDBContext choDBContext)
        {
            _context = choDBContext;
        }

        public EstablishmentTable Add(EstablishmentTable model)
        {
            _context.EstablishmentTable.Add(model);

            _context.SaveChanges();

            return model;
        }

        public EstablishmentTable Update(EstablishmentTable model)
        {
            _context.EstablishmentTable.Update(model);

            _context.SaveChanges();

            return model;
        }

        public void Delete(EstablishmentTable model)
        {
            _context.EstablishmentTable.Remove(model);

            _context.SaveChanges();
        }

        public EstablishmentTable? GetEstablishmentTable(int id)
        {
            return _context.EstablishmentTable.FirstOrDefault(r => r.EstablishmentTable_Id == id);
        }

        public List<EstablishmentTable>? GetEstablishmentTables(int establishmentId)
        {
            return _context.EstablishmentTable.Where(r => r.Establishment_Id == establishmentId).ToList();
        }
    }
}
