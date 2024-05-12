using choapi.Models;

namespace choapi.DAL
{
    public class FCMNoticationDAL : IFCMNotificationDAL
    {
        private readonly ChoDBContext _context;

        public FCMNoticationDAL(ChoDBContext choDBContext)
        {
            _context = choDBContext;
        }

        public FCMNotification Add(FCMNotification model)
        {
            _context.FCMNotification.Add(model);

            _context.SaveChanges();

            return model;
        }

        public FCMNotification Update(FCMNotification model)
        {
            _context.FCMNotification.Update(model);

            _context.SaveChanges();

            return model;
        }

        public FCMNotification Delete(FCMNotification model)
        {
            _context.FCMNotification.Update(model);

            _context.SaveChanges();

            return model;
        }

        public FCMNotification? Get(int id)
        {
            return _context.FCMNotification.FirstOrDefault(p => p.FCMNotification_Id == id && p.Is_Active != false);
        }

        public FCMNotification? GetByFCMId(string id)
        {
            return _context.FCMNotification.FirstOrDefault(p => p.FCM_Id == id);
        }

        public List<FCMNotification>? GetByUserId(int id)
        {
            return _context.FCMNotification.Where(p => p.User_Id == id && p.Is_Active != false).ToList();
        }

        
    }
}
