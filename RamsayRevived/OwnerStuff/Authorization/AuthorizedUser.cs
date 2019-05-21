using System;

namespace RamsayRevived.OwnerStuff.Authorization
{
    [Serializable]
    public class AuthorizedUser
    {
        public string username { get; set; }
        public ulong userID { get; set; }
    }
}
