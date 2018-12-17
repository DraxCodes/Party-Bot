using System.Reflection;
using System.Threading.Tasks;
using PartyBot.Services;

namespace PartyBot
{
    class Program
    {
        private readonly Assembly _assembly = Assembly.GetExecutingAssembly();

        private static Task Main(string[] args)
            => new DiscordService().InitializeAsync();
    }
}
