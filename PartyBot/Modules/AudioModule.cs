using Discord.Commands;
using Discord.WebSocket;
using PartyBot.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PartyBot.Modules
{
    public class AudioModule : ModuleBase<SocketCommandContext>
    {
        public AudioService AudioService { get; set; }

        [Command("Join")]
        public async Task JoinAndPlay()
            => await ReplyAsync("", false, await AudioService.JoinOrPlayAsync((SocketGuildUser)Context.User, Context.Channel, Context.Guild.Id));

        [Command("Leave")]
        public async Task Leave()
            => await ReplyAsync("", false, await AudioService.LeaveAsync(Context.Guild.Id));

        [Command("Play")]
        public async Task Play([Remainder]string search)
            => await ReplyAsync("", false, await AudioService.JoinOrPlayAsync((SocketGuildUser)Context.User, Context.Channel, Context.Guild.Id, search));

        [Command("Stop")]
        public async Task Stop()
            => await ReplyAsync("", false, await AudioService.StopAsync(Context.Guild.Id));

        [Command("List")]
        public async Task List()
            => await ReplyAsync("", false, await AudioService.ListAsync(Context.Guild.Id));

        [Command("Skip")]
        public async Task Delist(string id = null)
            => await ReplyAsync("", false, await AudioService.SkipTrackAsync(Context.Guild.Id));
    }
}
