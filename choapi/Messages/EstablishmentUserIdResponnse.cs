namespace choapi.Messages
{
    public class EstablishmentUserIdResponnse : ResponseBase
    {
        public List<EstablishmentReponse> Establishments { get; set; } = new List<EstablishmentReponse>();
    }
}
