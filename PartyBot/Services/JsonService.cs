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
    readonly string path = Directory.GetCurrentDirectory().Substring(0,
        Directory.GetCurrentDirectory().LastIndexOf(@"bin\"));

    public async Task addRule(string s)
    {

    }

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
                        path + @"LocalJson\" + fileName);
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
        string files = path + @"LocalJson\";
        var myFile = new DirectoryInfo(files).GetFiles()
         .OrderByDescending(f => f.LastWriteTime)
         .First();
        return myFile;
    }

    public async Task<string> GetJson(string name)
    {
        string files = path + @"LocalJson\";
        if (File.Exists(files + name))
            return files + name;
        await LoggingService.LogInformationAsync("Json", "Could not find a file with that specified name");
        return "File not found";
    }

    public async Task<IEnumerable<string>> GetAllJsonInFolder()
    {
        string files = path + @"LocalJson\";
        foreach (var item in Directory.EnumerateFiles(files))
        {
            await LoggingService.LogInformationAsync("Json", item);
        }
        return Directory.EnumerateFiles(files);
    }

    private async Task<List<string>> GetRules()
    {
        string rulesPath = path + @"\Stats\Rules.txt";
        string[] array = await File.ReadAllLinesAsync(rulesPath);
        return array.ToList();
    }

    public async Task NewRule(string p1, string p2, Optional<string> p3, Optional<string> p4)
    {
        string rulesPath = path + @"\Stats\Rules.txt";
        List<string> players = new List<string>
        {
            p1,
            p2
        };
        if (p3.GetType() != null)
        {
            players.Add((string)p3);
        }
        if (p4.GetType() != null)
        {
            players.Add((string)p4);
        }
        await File.AppendAllLinesAsync(rulesPath, players);
    }

    public async Task ListRules(ISocketMessageChannel channel)
    {
        foreach (string rule in GetRules().Result)
        {
            await channel.SendMessageAsync(rule);
        }
    }

}
