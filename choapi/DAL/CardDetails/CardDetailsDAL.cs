using choapi.Models;

namespace choapi.DAL
{
    public class CardDetailsDAL : ICardDetailsDAL
    {
        private readonly ChoDBContext _context;

        public CardDetailsDAL(ChoDBContext choDBContext)
        {
            _context = choDBContext;
        }

        public CardDetails Add(CardDetails model)
        {
            _context.CardDetails.Add(model);

            _context.SaveChanges();

            return model;
        }

        public CardDetails DeActivate(CardDetails model)
        {
            _context.CardDetails.Update(model);

            _context.SaveChanges();

            return model;
        }

        public CardDetails Delete(CardDetails model)
        {
            _context.CardDetails.Update(model);

            _context.SaveChanges();

            return model;
        }

        public CardDetails? Get(int id)
        {
            return _context.CardDetails.FirstOrDefault(c => c.CardDetails_Id == id && c.Is_Deleted != true);
        }

        public CardDetails? GetByEstablishmentId(int id)
        {
            return _context.CardDetails.FirstOrDefault(c => c.Establishment_Id == id && c.Is_Deleted != true);
        }

        public CardDetails? GetByUserId(int id)
        {
            return _context.CardDetails.FirstOrDefault(c => c.User_Id == id && c.Is_Deleted != true);
        }

        public CardDetails Update(CardDetails model)
        {
            _context.CardDetails.Update(model);

            _context.SaveChanges();

            return model;
        }
    }
}
