using choapi.Models;

namespace choapi.DAL
{
    public class PaymentDAL : IPaymentDAL
    {
        private readonly ChoDBContext _context;

        public PaymentDAL(ChoDBContext choDBContext)
        {
            _context = choDBContext;
        }

        public Payment? Add(Payment model)
        {
            if (model != null)
            {
                _context.Payment.Add(model);

                _context.SaveChanges();
            }
            return model;
        }
    }
}
