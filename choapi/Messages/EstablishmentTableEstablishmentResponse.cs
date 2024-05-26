namespace choapi.Messages
{
    public class EstablishmentTableEstablishmentResponse
    {
        public int EstablishmentTable_Id { get; set; }

        public int Establishment_Id { get; set; }

        public int? Capacity { get; set; } = null;

        public string? Time_Start { get; set; } = null;
    }
}
