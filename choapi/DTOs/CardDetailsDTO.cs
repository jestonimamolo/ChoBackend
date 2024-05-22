﻿namespace choapi.DTOs
{
    public class CardDetailsDTO
    {
        public int CardDetails_Id { get; set; }

        public string Card_Token { get; set; } = string.Empty;

        public int? User_Id { get; set; } = null;

        public int? Establishment_Id { get; set; } = null;

        public bool? Is_Active { get; set; } = null;

        public bool? Is_Deleted { get; set; } = null;
    }
}
