using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PartyBot.Handlers
{
    public class Filehandler
    {
        private readonly DiscordSocketClient _client;
        /* This file is where we can store all the File Helper Methods (So to speak). 
             We want to asynchronously Deserialize Json files and then store this info somewhere. 
             We should probably do our best to keep these methods private. */


        private async Task DeserializeJson(SocketMessage socketMessage)
        {
            if (socketMessage.Attachments.Any())
            {
                await socketMessage.Channel.SendMessageAsync(socketMessage.Attachments.First().ToString());
            }
            
        }

    }
}
