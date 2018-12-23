using Discord.Commands;
using PartyBot.Services;
using System.Threading.Tasks;

namespace PartyBot.Modules
{
    public class AdminModule : ModuleBase<SocketCommandContext>
    {
        public BotService BotService { get; set; }
        public AudioService AudioService { get; }

        [Command("Info")]
        public async Task Info()
            => await ReplyAsync("", false, await BotService.DisplayInfoAsync(Context));

        [Command("Prefix")]
        public async Task Prefix()
        {

        }
    }
}
