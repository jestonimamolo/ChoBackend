namespace choapi.Messages
{
    public class EstablishmentTablesResponse : ResponseBase
    {
        public List<EstablishmentTableEstablishmentResponse> EstablishmentTables { get; set; } = new List<EstablishmentTableEstablishmentResponse>();
    }
}
