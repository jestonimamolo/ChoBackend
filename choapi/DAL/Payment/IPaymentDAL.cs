using choapi.Models;

namespace choapi.DAL
{
    public interface IPaymentDAL
    {
        Payment? Add(Payment model);
    }
}
