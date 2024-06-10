using choapi.Models;

namespace choapi.Messages
{
    public class EstablishmentReponse
    {
        public int Establishment_Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; } = null;

        public int? User_Id { get; set; } = null;

        public int? Credits { get; set; } = null;

        public string? Plan { get; set; } = null;

        public double? Latitude { get; set; } = null;

        public double? Longitude { get; set; } = null;

        public bool? Is_Promoted { get; set; } = null;

        public bool? Is_Active { get; set; } = null;

        public string? Address { get; set;} = null;

        public int? Promo_Credit { get; set; } = null;

        public string? Promo_Type { get; set; } = null;

        public string? Payment_Card_Option { get; set; } = null;

        public double? Distance { get; set; } = null;

        public List<int>? Save_User { get; set; } = null;

        public List<EstablishmentImages>? Images { get; set; } = null;
    }
}
