namespace choapi.Messages
{
    public class RestaurantTableResponse : ResponseBase
    {
        public int RestaurantTable_Id { get; set; }

        public int Restaurant_Id { get; set; }

        public int? Capacity { get; set; } = null;

        public string? Time_Start { get; set; } = null;
    }
}
