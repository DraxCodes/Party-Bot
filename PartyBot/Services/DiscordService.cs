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
        private AudioService _audio;

        /* Initialize the Discord Client. */
        public async Task InitializeAsync()
        {
            _services = ConfigureServices();
            _client = _services.GetRequiredService<DiscordSocketClient>();
            _lavaLink = _services.GetRequiredService<Lavalink>();
            _audio = _services.GetService<AudioService>();
            var global = new Global().Initialize();
            HookEvents();

            await _client.LoginAsync(TokenType.Bot, Global.Config.DiscordToken);
            await _client.StartAsync();

            await _services.GetRequiredService<CommandHandler>().InitializeAsync();

            await Task.Delay(-1);
        }

        /* Hook Any Client Events Up Here. */
        private void HookEvents()
        {
            _lavaLink.Log += LogAsync;
            _client.Log += LogAsync;
            _services.GetRequiredService<CommandService>().Log += LogAsync;
            _client.Ready += OnReadyAsync;
        }

        /* Used when the Client Fires the ReadyEvent. */
        private async Task OnReadyAsync()
        {
            try
            {
                var node = await _lavaLink.AddNodeAsync(_client, new Configuration {
                    Severity = LogSeverity.Info
                });
                node.TrackFinished += _audio.OnFinshed;
                await _client.SetGameAsync(Global.Config.GameStatus);
            }
            catch (Exception ex)
            {
                await LoggingService.LogInformationAsync(ex.Source, ex.Message);
            }

        }

        /*Used whenever we want to log something to the Console. 
            Todo: Hook in a Custom LoggingService. */
        private async Task LogAsync(LogMessage logMessage)
        {
            await LoggingService.LogAsync(logMessage.Source, logMessage.Severity, logMessage.Message);
        }

        /* Configure our Services for Dependency Injection. */
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
