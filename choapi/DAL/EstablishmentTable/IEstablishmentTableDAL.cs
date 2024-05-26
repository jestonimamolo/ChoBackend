using choapi.Models;

namespace choapi.DAL
{
    public interface IEstablishmentTableDAL
    {
        EstablishmentTable Add(EstablishmentTable model);

        EstablishmentTable Update(EstablishmentTable model);

        void Delete(EstablishmentTable model);

        EstablishmentTable? GetEstablishmentTable(int id);

        List<EstablishmentTable>? GetEstablishmentTables(int establishmentId);
    }
}
