using choapi.Models;

namespace choapi.DAL
{
    public interface IInviteDAL
    {
        Invite Add(Invite model);

        Invite Update(Invite model);

        Invite Delete(Invite model);

        Invite? Get(int id);

        List<Invite>? GetByBooking(int id);

        List<Invite>? GetByUser(int id);
    }
}
