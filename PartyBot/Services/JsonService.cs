using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using PartyBot.Services;

public sealed class JsonService
{
    readonly string JsonFiles = Directory.GetCurrentDirectory().Substring(0,
        Directory.GetCurrentDirectory().LastIndexOf(@"bin\")) + @"\LocalJson";

    readonly string RulesPath = Directory.GetCurrentDirectory().Substring(0,
        Directory.GetCurrentDirectory().LastIndexOf(@"bin\")) + @"\Stats\Rules.txt";


    public async Task GetJsonFiles(ISocketMessageChannel channel, int i)
    {
        var messages = await channel.GetMessagesAsync(i).Flatten().ToArrayAsync();

        Console.WriteLine(messages.ToString());

        for (int k = 0; k < messages.Length; k++)
        {
            await DownloadJson(messages[k]);
        }
    }

    public async Task DownloadJson(IMessage message)
    {
        if (message.Attachments.Any())
        {
            try
            {
                for (int i = 0; i < message.Attachments.Count; i++)
                {
                    string fileName = message.Attachments.ElementAt(i).Filename;

                    //Create a WebClient and download the attached file
                    using var client = new WebClient();
                    client.DownloadFileAsync(new Uri(message.Attachments.ElementAt(i).Url),
                        JsonFiles + fileName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    //asynchronously converts the Json file to a GameData object
    public async Task<List<SongData>> ConvertJson(string filePath)
    {
        string contents = File.ReadAllText(filePath);
        var data = await Task.Run(() =>JsonConvert.DeserializeObject<List<SongData>>(contents));
        return data;
    }

    //asynchronously converts the Json file to a GameData object
    public async Task<List<SongData>> ConvertJson(FileInfo info)
    {
        string contents = File.ReadAllText(info.FullName);
        var data = await Task.Run(() => JsonConvert.DeserializeObject<List<SongData>>(contents));
        return data;
    }

    public FileInfo GetLastJson()
    {
        var myFile = new DirectoryInfo(JsonFiles).GetFiles()
         .OrderByDescending(f => f.LastWriteTime)
         .First();
        return myFile;
    }

    public async Task<string> GetJson(string name)
    {
        if (File.Exists(JsonFiles + name))
            return JsonFiles + name;
        await LoggingService.LogInformationAsync("Json", "Could not find a file with that specified name");
        return "File not found";
    }

    public async Task<IEnumerable<string>> GetAllJsonInFolder()
    {
        foreach (var item in Directory.EnumerateFiles(JsonFiles))
        {
            await LoggingService.LogInformationAsync("Json", item);
        }
        return Directory.EnumerateFiles(JsonFiles);
    }

    public async Task ListJsons(ISocketMessageChannel ch)
    {
        var filenames = from fullFilename
                in Directory.EnumerateFiles(JsonFiles)
                        select Path.GetFileName(fullFilename);
        foreach (var item in filenames)
        {
            await ch.SendMessageAsync(item);
        }
    }

    private async Task<List<string>> GetRules()
    {
        string[] array = await File.ReadAllLinesAsync(RulesPath);
        return array.ToList();
    }

    public async Task NewRule(string p1, string p2)
    {
        string rule = p1 + " " + p2;
        await File.AppendAllTextAsync(RulesPath, rule);
    }

    public async Task NewRule(string p1, string p2, string p3)
    {
        string rule = p1 + " " + p2 + " " + p3;
        await File.AppendAllTextAsync(RulesPath, rule);
    }

    public async Task NewRule(string p1, string p2, string p3, string p4)
    {
        string rule = p1 + " " + p2 + " " + p3 + " " + p4;
        await File.AppendAllTextAsync(RulesPath, rule);
    }

    public async Task ListRules(ISocketMessageChannel channel)
    {
        foreach (string rule in await GetRules())
        {
            await channel.SendMessageAsync(rule);
        }
    }

    public async Task DeleteRule(ISocketMessageChannel channel, string rule)
    {
        await channel.SendMessageAsync(rule);
        await Task.Run( () => File.WriteAllLines(RulesPath,
            File.ReadLines(RulesPath).Where(l => l != rule).ToList()));
    }

}
