using System;

namespace RamsayRevived
{
    public class DailyReport
    {
        public DateTime dayOfReport { get; set; }
        public int messagesSent { get; set; }
        public int commandsRan { get; set; }
        public int guildsJoined { get; set; }
        public int guildsLeft { get; set; }
        public int usersJoined { get; set; }
        public int usersLeft { get; set; }
        public int insultsSent { get; set; }
    }
}
