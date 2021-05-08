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
        public async Task CatJam()
            => await Context.Channel.SendMessageAsync("https://tenor.com/view/cat-jam-gif-18110512");

        [Command("RateWaifu")]
        public async Task RateWaifu(string name)
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
            await Context.Channel.SendMessageAsync(name + " is a " + val);
        }
    }
}