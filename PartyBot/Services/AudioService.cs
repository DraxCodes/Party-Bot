using Discord;
using Discord.WebSocket;
using PartyBot.DataStructs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victoria;
using Victoria.Entities;
using Victoria.Entities.Enums;

namespace PartyBot.Services
{
    public sealed class AudioService
    {
        private Lavalink _lavalink;

        public AudioService(Lavalink lavalink)
            => _lavalink = lavalink;

        private readonly Lazy<ConcurrentDictionary<ulong, AudioOptions>> _lazyOptions
            = new Lazy<ConcurrentDictionary<ulong, AudioOptions>>();

        private ConcurrentDictionary<ulong, AudioOptions> Options
            => _lazyOptions.Value;

        public async Task<string> JoinOrPlayAsync(SocketGuildUser user, IMessageChannel textChannel, ulong guildId)
            => await JoinOrPlayAsync(user, textChannel, guildId);

        /*This is ran when a user uses either the command !Join or !Play
            I decided to put these two commands as one, will probably change it in future. 
            Task Returns a string for now for use in the Command Module (ReplyAsync).
            TODO: Return Embed to make it all pretty. */
        public async Task<string> JoinOrPlayAsync(SocketGuildUser user, IMessageChannel textChannel, ulong guildId, string query)
        {
            //Check If User Is Connected To Voice Cahnnel.
            if (user.VoiceChannel == null)
                return "You must be connected to a voice channel.";

            //Check if user who used !Join is a user that has already summoned the Bot.
            if (Options.TryGetValue(user.Guild.Id, out var options) && options.Summoner.Id != user.Id)
                return $"I can't join another voice channel till {options.Summoner} disconnects me.";

            //If The user hasn't provided a Search string from the !Play command, then they must have used the !Join command.
            //Join the voice channel the user is in.
            if (query == null)
            {
                await _lavalink.DefaultNode.ConnectAsync(user.VoiceChannel, textChannel /*This Param is Optional, Only used If we want to bind the Bot to a TextChannel For commands.*/);
                Options.TryAdd(user.Guild.Id, new AudioOptions
                {
                    Summoner = user
                });
                return $"Now connected to {user.VoiceChannel.Name} and bound to {textChannel.Name}. Get Ready For Betrays...";
            }
            else
            {
                try
                {
                    //Try get the player. If it returns null then the user has used the command !Play without using the command !Join.
                    var player = _lavalink.DefaultNode.GetPlayer(guildId);
                    if (player == null)
                    {
                        //User Used Command !Play before they used !Join
                        //So We Create a Connection To The Users Voice Channel.
                        await _lavalink.DefaultNode.ConnectAsync(user.VoiceChannel, textChannel);
                        Options.TryAdd(user.Guild.Id, new AudioOptions
                        {
                            Summoner = user
                        });
                        //Now we can set the player to out newly created player.
                        player = _lavalink.DefaultNode.GetPlayer(guildId);
                    }

                    //Find The Youtube Track the User requested.
                    LavaTrack track;
                    var search = await _lavalink.DefaultNode.SearchYouTubeAsync(query);

                    //If we couldn't find anything, tell the user.
                    if (search.LoadResultType == LoadResultType.NoMatches)
                        return $"BAMBOOZLED! I wasn't able to find anything for {query}.";

                    //Get the first track from the search results.
                    //TODO: Add a 1-5 list for the user to pick from. (Like Fredboat)
                    track = search.Tracks.FirstOrDefault();

                    //If the Bot is already playing music, or if it is paused but still has music in the playlist, Add the requested track to the queue.
                    if(player.IsPlaying || player.IsPaused)
                    {
                        player.Queue.Enqueue(track);
                        return $"{track.Title} has been added to queue.";
                    }

                    //Player was not playing anything, so lets play the requested track.
                    await player.PlayAsync(track);
                    return $"Now Playing: {track.Title} - {track.Uri}";
                }
                //If after all the checks we did, something still goes wrong. Tell the user about it so they can report it back to us.
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            
        }

        /*This is ran when a user uses the command !Leave.
            Task Returns a string for now for use in the Command Module (ReplyAsync).
            TODO: Return Embed to make it all pretty. */
        public async Task<string> LeaveAsync(ulong guildId)
        {
            try
            {
                //Get The Player Via GuildID.
                var player = _lavalink.DefaultNode.GetPlayer(guildId);

                //if The Player is playing, Stop it.
                if (player.IsPlaying)
                    await player.StopAsync();

                //Leave the voice channel.
                var name = player.VoiceChannel.Name;
                await _lavalink.DefaultNode.DisconnectAsync(guildId);
                return $"I've left {name}. Thank you for playing moosik.";
            }
            //Tell the user about the error so they can report it back to us.
            catch (InvalidOperationException ex)
            {
                return ex.Message;
            }
        }
    }
}
