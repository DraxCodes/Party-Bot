using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PartyBot.Services;
using System.Collections;
using System.Linq;

namespace PartyBot.Modules
{
    public class FunModule : ModuleBase<SocketCommandContext>
    {
        [Command("catjam")]
        public async Task CatJam(SocketMessage arg)
        {
            await arg.Channel.SendMessageAsync("https://tenor.com/view/cat-jam-gif-18110512");
        }

        [Command("ratewaifu")]
        public async Task RateWaifu(SocketMessage arg, string name)
        {
            ArrayList vals = new ArrayList();

            foreach (char c in name)
            {
                vals.Add(Convert.ToInt32(c));
            }
            int val = vals.Cast<int>().Sum() % 11;

            //gotta make sure people know the truth
            if (name.ToLower().Equals("yotsuba"))
            {
                val = 2;
            }
            await arg.Channel.SendMessageAsync(name + " is a " + val);
        }
    }
}