using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PartyBot.Modules
{
    public class TestModule : ModuleBase<SocketCommandContext>
    {
        [Command("test")]
        public async Task test(SocketGuildUser user)
        {
            var message = await Context.Channel.GetMessageAsync(552567071490310165) as IUserMessage;
            var reactions = message.Reactions;
            foreach (var r in reactions)
            {
                await message.RemoveReactionAsync(r.Key, user as IUser);
            }
        }
    }
}
