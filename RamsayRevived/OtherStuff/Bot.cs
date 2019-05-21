using Discord;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamsayRevived
{
    public static class Bot
    {
        public static ulong Starman = 363850072309497876;
        public static void ThrowError(string error)
        {
            StringBuilder sb = new StringBuilder();
            CheckDirs();
            sb.AppendLine(File.ReadAllText("Resources\\Logs\\ErrorLog.txt"));
            sb.AppendLine(DateTime.Now + ": " + error);
            File.WriteAllText("Resources\\Logs\\ErorrLog.txt", sb.ToString());
        }

        public static void CheckDirs()
        {
            if (!Directory.Exists("Resources"))
                Directory.CreateDirectory("Resources");
            if (!Directory.Exists("Resources\\Logs"))
                Directory.CreateDirectory("Resources\\Logs");
            if (!Directory.Exists("Resources\\Data"))
                Directory.CreateDirectory("Resources\\Data");
            if (!Directory.Exists("Resources\\Data\\OwnerData"))
                Directory.CreateDirectory("Resources\\Data\\OwnerData");
            if (!Directory.Exists("Resources\\Data\\UserData"))
                Directory.CreateDirectory("Resources\\Data\\UserData");
        }
    }
}
