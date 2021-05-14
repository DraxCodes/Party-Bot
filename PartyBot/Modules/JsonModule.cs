using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;

using System.Threading.Tasks;


namespace PartyBot.Modules
{
    public class JsonModule : ModuleBase<SocketCommandContext>
    {

        public JsonService JsonService { get; set; }

        [Command("savejson")]
        public async Task GetFiles(int i)
            => await JsonService.GetJsonFiles(Context.Channel, i);

        [Command("updatestats")]
        public async Task UpdateStats()
            => await JsonService.GetAllJsonInFolder();

        [Command("listrules")]
        public async Task ListRules()
            => await JsonService.ListRules(Context.Channel);

        [Command("newrule")]
        public async Task AddRule(string player, string player2, Optional<string> player3, Optional<string> player4)
            => await JsonService.NewRule(player, player2, player3, player4);
    }
}
