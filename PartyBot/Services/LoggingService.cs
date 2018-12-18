using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PartyBot.Services
{
    /* A Static Logging Service So it Can Be Used Throughout The Whole Bot Anywhere We Want. */
    public static class LoggingService
    {
        /* The Standard Way Log */ 
        public static async Task LogAsync(string src, LogSeverity severity, string message, Exception exception = null)
        {
            if (severity.Equals(null))
            {
                severity = LogSeverity.Warning;
            }
            await Append($"{GetSeverityString(severity)}", GetConsoleColor(severity));
            await Append($" [{SourceToString(src)}] ", ConsoleColor.DarkGray);

            if (!string.IsNullOrWhiteSpace(message))
                await Append($"{message}\n", ConsoleColor.White);
            else if (exception == null)
            {
                await Append("Uknown Exception. Exception Returned Null.\n", ConsoleColor.DarkRed);
            }
            else if (exception.Message == null)
                await Append($"Unknownk \n{exception.StackTrace}\n", GetConsoleColor(severity));
            else
                await Append($"{exception.Message ?? "Unknownk"}\n{exception.StackTrace ?? "Unknown"}\n", GetConsoleColor(severity));
        }

        /* The Way To Log Critical Errors*/
        public static async Task LogCriticalAsync(string source, string message, Exception exc = null)
            => await LogAsync(source, LogSeverity.Critical, message, exc);

        /* The Way To Log Basic Infomation */
        public static async Task LogInformationAsync(string source, string message)
            => await LogAsync(source, LogSeverity.Info, message);

        /* Format The Output */
        private static async Task Append(string message, ConsoleColor color)
        {
            await Task.Run(() => {
                Console.ForegroundColor = color;
                Console.Write(message);
            });
        }

        /* Swap The Normal Source Input To Something Neater */
        private static string SourceToString(string src)
        {
            switch (src.ToLower())
            {
                case "discord":
                    return "DISCD";
                case "victoria":
                    return "VICTR";
                case "audio":
                    return "AUDIO";
                case "admin":
                    return "ADMIN";
                case "gateway":
                    return "GTWAY";
                case "blacklist":
                    return "BLAKL";
                case "lavanode_0_socket":
                    return "LAVAS";
                case "lavanode_0":
                    return "LAVA#";
                case "bot":
                    return "BOTWN";
                default:
                    return src;
            }
        }

        /* Swap The Severity To a String So We Can Output It To The Console */
        private static string GetSeverityString(LogSeverity severity)
        {
            switch (severity)
            {
                case LogSeverity.Critical:
                    return "CRIT";
                case LogSeverity.Debug:
                    return "DBUG";
                case LogSeverity.Error:
                    return "EROR";
                case LogSeverity.Info:
                    return "INFO";
                case LogSeverity.Verbose:
                    return "VERB";
                case LogSeverity.Warning:
                    return "WARN";
                default: return "UNKN";
            }
        }

        /* Return The Console Color Based On Severity Selected */
        private static ConsoleColor GetConsoleColor(LogSeverity severity)
        {
            switch (severity)
            {
                case LogSeverity.Critical:
                    return ConsoleColor.Red;
                case LogSeverity.Debug:
                    return ConsoleColor.Magenta;
                case LogSeverity.Error:
                    return ConsoleColor.DarkRed;
                case LogSeverity.Info:
                    return ConsoleColor.Green;
                case LogSeverity.Verbose:
                    return ConsoleColor.DarkCyan;
                case LogSeverity.Warning:
                    return ConsoleColor.Yellow;
                default: return ConsoleColor.White;
            }
        }
    }
}
