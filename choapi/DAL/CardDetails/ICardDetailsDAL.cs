using choapi.Models;

namespace choapi.DAL
{
    public interface ICardDetailsDAL
    {
        CardDetails Add(CardDetails model);

        CardDetails Update(CardDetails model);

        CardDetails Delete(CardDetails model);

        CardDetails DeActivate(CardDetails model);

        CardDetails? Get(int id);

        CardDetails? GetByUserId(int id);

        CardDetails? GetByEstablishmentId(int id);
    }
}
