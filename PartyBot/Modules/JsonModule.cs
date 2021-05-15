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
        public async Task AddRule(string player, string player2)
            => await JsonService.NewRule(player, player2);

        [Command("newrule")]
        public async Task AddRule(string player, string player2, string player3)
            => await JsonService.NewRule(player, player2, player3);

        [Command("newrule")]
        public async Task AddRule(string player, string player2, string player3, string player4)
            => await JsonService.NewRule(player, player2, player3, player4);

        [Command("deleterule")]
        public async Task DeleteRule(string rule)
            => await JsonService.DeleteRule(Context.Channel, rule);

        [Command("listjsons")]
        public async Task PrintJsons()
            => await JsonService.ListJsons(Context.Channel);
    }
}
