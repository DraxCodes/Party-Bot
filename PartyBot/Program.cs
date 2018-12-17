using System.Reflection;
using System.Threading.Tasks;
using PartyBot.Services;

namespace PartyBot
{
    class Program
    {
        /* Keep This File Super Simple. (This Method Requires C# 7.2 or Higher!) */
        private static Task Main(string[] args)
            => new DiscordService().InitializeAsync();
    }
}
