namespace choapi.DTOs
{
    public class ManagerDTO
    {
        public int Manager_Id { get; set; }

        public int Restaurant_Id { get; set; }

        public int? Created_By { get; set; } = null;

        public DateTime? Created_Date { get; set; } = null;
    }
}
