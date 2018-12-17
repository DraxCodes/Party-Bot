using Discord;
using Newtonsoft.Json;
using PartyBot.DataStructs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PartyBot.Handlers
{
    public static class Global
    {
        public static string ConfigPath { get; set; } = "config.json";
        public static BotConfig Config { get; set; }
        public static ulong ReactionMessageID { get; set; }

        public static void Initialize()
        {
            var json = string.Empty;
            if (!File.Exists(ConfigPath))
            {
                json = JsonConvert.SerializeObject(GenerateNewConfig(), Formatting.Indented);
                File.WriteAllText("config.json", json, new UTF8Encoding(false));
                Console.WriteLine(new LogMessage(LogSeverity.Error, ConfigPath, "No Config File Found, Making one"));
                Console.ReadKey();

                return;
            }
            json = File.ReadAllText(ConfigPath, new UTF8Encoding(false));
            Config = JsonConvert.DeserializeObject<BotConfig>(json);
        }

        private static BotConfig GenerateNewConfig() => new BotConfig
        {
            DiscordToken = "",
            DefaultPrefix = "!",
            GameStatus = "CHANGE ME IN CONFIG",
            BlacklistedChannels = new List<ulong>()
        };
    }
}
