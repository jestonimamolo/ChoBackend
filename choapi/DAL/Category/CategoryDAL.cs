using choapi.Models;

namespace choapi.DAL
{
    public class CategoryDAL : ICategoryDAL
    {
        private readonly ChoDBContext _context;

        public CategoryDAL(ChoDBContext choDBContext)
        {
            _context = choDBContext;
        }

        public Category Add(Category model)
        {
            _context.Category.Add(model);

            _context.SaveChanges();

            return model;
        }

        public Category? Get(int id)
        {
            return _context.Category.FirstOrDefault(c => c.Category_Id == id);
        }

        public Category Update(Category model)
        {
            _context.Category.Update(model);

            _context.SaveChanges();

            return model;
        }
    }
}
