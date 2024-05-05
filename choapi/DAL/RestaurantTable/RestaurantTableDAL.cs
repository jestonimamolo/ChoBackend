using choapi.Models;

namespace choapi.DAL
{
    public class RestaurantTableDAL : IRestaurantTableDAL
    {
        private readonly ChoDBContext _context;

        public RestaurantTableDAL(ChoDBContext choDBContext)
        {
            _context = choDBContext;
        }

        public RestaurantTable Add(RestaurantTable model)
        {
            _context.RestaurantTable.Add(model);

            _context.SaveChanges();

            return model;
        }

        public RestaurantTable Update(RestaurantTable model)
        {
            _context.RestaurantTable.Update(model);

            _context.SaveChanges();

            return model;
        }

        public void Delete(RestaurantTable model)
        {
            _context.RestaurantTable.Remove(model);

            _context.SaveChanges();
        }

        public RestaurantTable? GetRestaurantTable(int id)
        {
            return _context.RestaurantTable.FirstOrDefault(r => r.RestaurantTable_Id == id);
        }

        public List<RestaurantTable>? GetRestaurantTables(int restaurantId)
        {
            return _context.RestaurantTable.Where(r => r.Restaurant_Id == restaurantId).ToList();
        }
    }
}
