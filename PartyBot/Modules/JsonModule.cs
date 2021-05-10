using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.IO;

using System.Threading.Tasks;


namespace PartyBot.Modules
{
    public class JsonModule : ModuleBase<SocketCommandContext>
    {

        public JsonService JsonService { get; set; }

        [Command("json")]
        public async Task GetFiles(int i)
            => await JsonService.GetJsonFiles(Context.Channel, i);
    }
}
