using choapi.Models;

namespace choapi.DAL
{
    public interface ICategoryDAL
    {
        Category Add(Category model);

        Category Update(Category model);

        Category? Get(int id);
    }
}
