using choapi.Models;

namespace choapi.DAL
{
    public interface INotificationDAL
    {
        Notification Add(Notification model);

        Notification Update(Notification model);

        Notification Delete(Notification model);

        Notification? Get(int id);

        List<Notification>? GetByUser(int id);
    }
}
