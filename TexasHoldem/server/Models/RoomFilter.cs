﻿namespace Server.Models
{
    public class RoomFilter
    {
        public string User;
        public string PlayerName;
        public int? PotSize;
        public bool? LeagueOnly;
        public string GameType;
        public int? BuyInPolicy;
        public int? ChipPolicy;
        public int? MinBet;
        public int? MinPlayers;
        public int? MaxPlayers;
        public bool? SpectatingAllowed;
    }
}