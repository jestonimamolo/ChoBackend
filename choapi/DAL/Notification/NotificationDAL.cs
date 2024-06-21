using choapi.Models;

namespace choapi.DAL
{
    public class NotificationDAL : INotificationDAL
    {
        private readonly ChoDBContext _context;

        public NotificationDAL(ChoDBContext choDBContext)
        {
            _context = choDBContext;
        }

        public Notification Add(Notification model)
        {
            _context.Notification.Add(model);

            _context.SaveChanges();

            return model;
        }

        public Notification Delete(Notification model)
        {
            _context.Notification.Update(model);

            _context.SaveChanges();

            return model;
        }

        public Notification? Get(int id)
        {
            return _context.Notification.FirstOrDefault(m => m.Notification_Id == id && m.Is_Deleted != true);
        }

        public List<Notification>? GetByUser(int id)
        {
            return _context.Notification.Where(m => m.Sender_Id == id && m.Is_Deleted != true).ToList();
        }

        public Notification Update(Notification model)
        {
            _context.Notification.Update(model);

            _context.SaveChanges();

            return model;
        }
    }
}
