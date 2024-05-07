using choapi.Models;

namespace choapi.DAL
{
    public class TransactionDAL : ITransactionDAL
    {
        private readonly ChoDBContext _context;

        public TransactionDAL(ChoDBContext choDBContext)
        {
            _context = choDBContext;
        }

        public Transaction Add(Transaction model)
        {
            _context.Transaction.Add(model);

            _context.SaveChanges();

            return model;
        }

        public Transaction Update(Transaction model)
        {
            _context.Transaction.Update(model);

            _context.SaveChanges();

            return model;
        }

        public Transaction Delete(Transaction model)
        {
            _context.Transaction.Update(model);

            _context.SaveChanges();

            return model;
        }

        public Transaction? Get(int id)
        {
            return _context.Transaction.FirstOrDefault(c => c.Transaction_Id == id && c.Is_Deleted != false);
        }

        public List<Transaction>? GetByUserId(int id)
        {
            return _context.Transaction.Where(c => c.User_Id == id && c.Is_Deleted != false).ToList();
        }
    }
}
