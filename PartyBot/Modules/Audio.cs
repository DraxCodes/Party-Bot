using Discord.Commands;
using Discord.WebSocket;
using PartyBot.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PartyBot.Modules
{
    public class Audio : ModuleBase<SocketCommandContext>
    {
        public AudioService AudioService { get; set; }

        [Command("Join")]
        public async Task JoinAndPlay()
            => await ReplyAsync(await AudioService.JoinOrPlayAsync((SocketGuildUser)Context.User, Context.Channel, Context.Guild.Id));

        [Command("Leave")]
        public async Task Leave()
            => await ReplyAsync(await AudioService.LeaveAsync(Context.Guild.Id));

        [Command("Play")]
        public async Task Play([Remainder]string search)
            => await ReplyAsync(await AudioService.JoinOrPlayAsync((SocketGuildUser)Context.User, Context.Channel, Context.Guild.Id, search));
    }
}
