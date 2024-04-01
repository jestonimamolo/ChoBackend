namespace choapi.DTOs
{
    public class RestaurantTableDto
    {
        public required int Restaurant_Id { get; set; }

        public int? Table_Number { get; set; } = null;

        public int? Capacity { get; set; } = null;

        public bool? Is_Reserved { get; set; } = null;
    }
}
