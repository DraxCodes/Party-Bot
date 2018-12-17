using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using PartyBot.Handlers;
using System;
using System.Threading.Tasks;
using Victoria;

namespace PartyBot.Services
{
    public class DiscordService
    {
        private DiscordSocketClient _client;
        private ServiceProvider _services;
        private Lavalink _lavaLink;

        public async Task InitializeAsync()
        {
            _services = ConfigureServices();
            _client = _services.GetRequiredService<DiscordSocketClient>();
            _lavaLink = _services.GetRequiredService<Lavalink>();
            Global.Initialize();
            HookEvents();

            await _client.LoginAsync(TokenType.Bot, Global.Config.DiscordToken);
            await _client.StartAsync();

            await _services.GetRequiredService<CommandHandler>().InitializeAsync();

            await Task.Delay(-1);
        }

        private void HookEvents()
        {
            _lavaLink.Log += LogAsync;
            _client.Log += LogAsync;
            _services.GetRequiredService<CommandService>().Log += LogAsync;
            _client.Ready += OnReadyAsync;
        }

        private async Task OnReadyAsync()
        {
            
            var node = _lavaLink.AddNodeAsync(_client).ConfigureAwait(false);

            await _client.SetGameAsync(Global.Config.GameStatus);
        }

        private Task LogAsync(LogMessage logMessage)
        {
            Console.WriteLine(logMessage.ToString());

            return Task.CompletedTask;
        }

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandler>()
                .AddSingleton<Lavalink>()
                .AddSingleton<AudioService>()
                .BuildServiceProvider();
        }
    }
}
