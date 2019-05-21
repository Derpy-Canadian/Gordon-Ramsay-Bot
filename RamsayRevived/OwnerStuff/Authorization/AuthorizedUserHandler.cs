using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using Discord.WebSocket;

namespace RamsayRevived.OwnerStuff.Authorization
{
    public class AuthorizedUserHandler
    {
        public static List<AuthorizedUser> authUsers;

        static AuthorizedUserHandler()
        {
            if(File.Exists("Resources\\AuthorizedUsers.bin"))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    FileStream str = new FileStream("Resources\\AuthorizedUsers.bin", FileMode.Open);
                    authUsers = formatter.Deserialize(str) as List<AuthorizedUser>;
                    str.Close();
                }
                catch (Exception e)
                {
                    Bot.ThrowError(e.Message);
                }
            }
            else
            {
                Bot.CheckDirs();
                authUsers = new List<AuthorizedUser>();
                SaveUsers();
            }
        }

        public static void SaveUsers()
        {
            Bot.CheckDirs();
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream str = new FileStream("Resources\\AuthorizedUsers.bin", FileMode.Create);
            formatter.Serialize(str, authUsers);
            str.Close();
        }

        public static void AuthorizeUser(SocketUser user)
        {
            AuthorizedUser authUser = new AuthorizedUser()
            {
                userID = user.Id,
                username = user.Username
            };
            authUsers.Add(authUser);
            SaveUsers();
        }

        public static AuthorizedUser GetAuthUser(SocketUser user)
        {
            AuthorizedUser authUser = new AuthorizedUser()
            {
                userID = user.Id,
                username = user.Username
            };
            if (authUsers.Contains(authUser))
            {
                return authUser;
            }
            else
            {
                return null;
            }
        }
    }
}
