namespace choapi.Messages
{
    public class RestaurantTablesResponse : ResponseBase
    {
        public List<RestaurantTableRestaurantResponse> RestaurantTables { get; set; } = new List<RestaurantTableRestaurantResponse>();
    }
}
