namespace choapi.DTOs
{
    public class InviteDTO
    {
        public int Invite_Id { get; set; }

        public int User_Id { get; set; }

        public int? Invited_User { get; set; } = null;

        public int? Booking_Id { get; set; } = null;

        public string? Status { get; set; } = null;

        public DateTime? Created_Date { get; set; } = null;
    }
}
