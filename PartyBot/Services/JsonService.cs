using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;

public sealed class JsonService
{
    readonly string path = Directory.GetCurrentDirectory().Substring(0,
        Directory.GetCurrentDirectory().LastIndexOf(@"bin\"));

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
    public async Task<GameData> ConvertJson(string filePath)
    {
        using FileStream stream = File.OpenRead(filePath);
        GameData data = await JsonSerializer.DeserializeAsync<GameData>(stream);
        return data;
    }

    //asynchronously converts the Json file to a GameData object
    public async Task<GameData> ConvertJson(FileInfo info)
    {
        using FileStream stream = File.OpenRead(info.FullName);
        GameData data = await JsonSerializer.DeserializeAsync<GameData>(stream);
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
}
