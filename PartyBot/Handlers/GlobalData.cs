using Discord;
using Newtonsoft.Json;
using PartyBot.DataStructs;
using PartyBot.Services;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PartyBot.Handlers
{
    public class GlobalData
    {
        public static string ConfigPath { get; set; } = "config.json";
        public static BotConfig Config { get; set; }

        //Initialize the Config and Global Properties.
        //TODO (Check token validity before but starts,
        //right now if the config file is generated but the user hasn't filled it out it will throw a null exception error.
        public async Task InitializeAsync()
        {
            var json = string.Empty;

            //Check if Config.json Exists.
            if (!File.Exists(ConfigPath))
            {
                json = JsonConvert.SerializeObject(GenerateNewConfig(), Formatting.Indented);
                File.WriteAllText("config.json", json, new UTF8Encoding(false));
                await LoggingService.LogAsync("Bot", LogSeverity.Error, "No Config file found. A new one has been generated. Please close the & fill in the required section.");
                await Task.Delay(-1);
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
