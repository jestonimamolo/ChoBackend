using choapi.Models;

namespace choapi.DAL
{
    public class InviteDAL : IInviteDAL
    {
        private readonly ChoDBContext _context;

        public InviteDAL(ChoDBContext choDBContext)
        {
            _context = choDBContext;
        }

        public Invite Add(Invite model)
        {
            _context.Invite.Add(model);

            _context.SaveChanges();

            return model;
        }

        public Invite Update(Invite model)
        {
            _context.Invite.Update(model);

            _context.SaveChanges();

            return model;
        }

        public Invite Delete(Invite model)
        {
            _context.Invite.Update(model);

            _context.SaveChanges();

            return model;
        }

        public Invite? Get(int id)
        {
            return _context.Invite.FirstOrDefault(m => m.Invite_Id == id && m.Is_Deleted != true);
        }

        public List<Invite>? GetByBooking(int id)
        {
            return _context.Invite.Where(m => m.Booking_Id == id && m.Is_Deleted != true).ToList();
        }

        public List<Invite>? GetByUser(int id)
        {
            return _context.Invite.Where(m => m.User_Id == id && m.Is_Deleted != true).ToList();
        }
    }
}
