namespace choapi.DTOs
{
    public class EstablishmentTableDTO
    {
        public int EstablishmentTable_Id { get; set; }

        public int Establishment_Id { get; set; }

        //public int? Table_Number { get; set; } = null;

        public int? Capacity { get; set; } = null;

        public string? Time_Start { get; set; } = null;

        //public bool? Is_Reserved { get; set; } = null;
    }
}
