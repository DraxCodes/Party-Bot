using System;
using System.Collections;

public class GameData
{

    //An array which represents each song that played,
    //each individual object holds all information recorded
    //in the lobby.
    public SongData[] songs { get; set; }

    public ArrayList arrayList { get; set; }

    public GameData(SongData[] s)
    {
        songs = s;
    }
    public GameData()
    {
        arrayList = new ArrayList();
    }
}

public class SongData
{
    //The current game mode
    public string gameMode { get; set; }
    //name of the song
    public string name { get; set; }
    public string artist { get; set; }
    //The anime the song is from
    public Anime anime { get; set; }
    //Anime News Network ID of the show from which the song played
    public int annId { get; set; }
    //the number that comes after type, ex OP 2 or ED 3
    public int songNumber { get; set; }
    public int activePlayers { get; set; }
    public int totalPlayers { get; set; }
    //OP, Ed, or insert
    public string type { get; set; }
    public Urls urls { get; set; }
    public int startSample { get; set; }
    //length of the video
    public float videoLength { get; set; }
    //all players in the lobby or other players?
    public Player[] players { get; set; }
    public Fromlist[] fromList { get; set; }
    public bool correct { get; set; }
    //answer typed by the player
    public string selfAnswer { get; set; }
}

public class Anime
{
    //English translation of the title
    public string english { get; set; }
    //Romaji representation of the title
    public string romaji { get; set; }
}

public class Urls
{
    public Catbox catbox { get; set; }
    public Openingsmoe openingsmoe { get; set; }
}

public class Catbox
{
    //link to MP3 file hosted on Catbox
    public string _0 { get; set; }
    public string _480 { get; set; }
    //link to HD video file hosted on Catbox
    public string _720 { get; set; }
}

public class Openingsmoe
{
    //link to HD video hosted on OpeningsMoe
    public string _720 { get; set; }
}

public class Player
{
    //Name of the player
    public string name { get; set; }
    //score the player gave the show on MAL/Anilist etc
    public int score { get; set; }
    //did the Player get the correct answer
    public bool correct { get; set; }
    //answer put down
    public string answer { get; set; }
    //time it took to guess
    public int guessTime { get; set; }
    //was the player active
    public bool active { get; set; }
    //what position in the lobby was the player
    public int position { get; set; }
    public int positionSlot { get; set; }
}

public class Fromlist
{
    //name of the player
    public string name { get; set; }
    //in their list or not
    public int listStatus { get; set; }
    public object score { get; set; }

    public Fromlist(string n, int list, object s)
    {
        name = n;
        listStatus = list;
        score = s;
    }
    public Fromlist()
    {
        name = null;
        listStatus = 400;
        score = null;
    }
}
