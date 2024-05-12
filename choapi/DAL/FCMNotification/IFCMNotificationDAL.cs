using choapi.Models;
using Microsoft.EntityFrameworkCore;

namespace choapi.DAL
{
    public interface IFCMNotificationDAL
    {
        FCMNotification Add(FCMNotification model);

        FCMNotification Update(FCMNotification model);

        FCMNotification Delete(FCMNotification model);

        FCMNotification? Get(int id);

        FCMNotification? GetByFCMId(string id);

        List<FCMNotification>? GetByUserId(int id);
    }
}
