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

        //Initialize the Config and Global Properties.
        //TODO (Check token validity before but starts,
        //right now if the config file is generated but the user hasn't filled it out it will throw a null exception error.
        public static void Initialize()
        {
            var json = string.Empty;

            //Check if Config.json Exists.
            if (!File.Exists(ConfigPath))
            {
                json = JsonConvert.SerializeObject(GenerateNewConfig(), Formatting.Indented);
                File.WriteAllText("config.json", json, new UTF8Encoding(false));
                Console.WriteLine(new LogMessage(LogSeverity.Error, ConfigPath, "No Config File Found, Making one"));
                Console.ReadKey();

                return;
            }

            //If Config.json exists, get the values and apply them to the Global Property (Config).
            json = File.ReadAllText(ConfigPath, new UTF8Encoding(false));
            Config = JsonConvert.DeserializeObject<BotConfig>(json);
        }

        //If no config is found, this structure is generated as an empty config. 
        private static BotConfig GenerateNewConfig() => new BotConfig
        {
            DiscordToken = "",
            DefaultPrefix = "!",
            GameStatus = "CHANGE ME IN CONFIG",
            BlacklistedChannels = new List<ulong>()
        };
    }
}
