using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;

using System.Threading.Tasks;


namespace PartyBot.Modules
{
    public class DataModule : ModuleBase<SocketCommandContext>
    {

        public DataService DataService { get; set; }

        [Command("savejson")]
        public async Task GetFiles(int i)
            => await DataService.GetJsonFiles(Context.Channel, i);

        [Command("updatestats")]
        public async Task UpdateStats()
            => await DataService.GetAllJsonInFolder();

        [Command("listrules")]
        public async Task ListRules()
            => await DataService.ListRules(Context.Channel);

        [Command("newrule")]
        public async Task AddRule(string player, string player2)
            => await DataService.NewRule(player, player2);

        [Command("newrule")]
        public async Task AddRule(string player, string player2, string player3)
            => await DataService.NewRule(player, player2, player3);

        [Command("newrule")]
        public async Task AddRule(string player, string player2, string player3, string player4)
            => await DataService.NewRule(player, player2, player3, player4);

        [Command("deleterule")]
        public async Task DeleteRule(string rule)
            => await DataService.DeleteRule(Context.Channel, rule);

        [Command("listjsons")]
        public async Task PrintJsons()
            => await DataService.ListJsons(Context.Channel);
    }
}
