namespace choapi.Messages
{
    public class ManagersUserResponse : ResponseBase
    {
        public List<ManagerEstablishmentResponse> Managers { get; set; } = new List<ManagerEstablishmentResponse>();
    }
}
