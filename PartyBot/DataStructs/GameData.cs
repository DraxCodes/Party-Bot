using System;

public class GameData
{
    public SongData[] songs { get; set; }
}

public class SongData
{
    public string gameMode { get; set; }
    public string name { get; set; }
    public string artist { get; set; }
    public Anime anime { get; set; }
    public int annId { get; set; }
    public int songNumber { get; set; }
    public int activePlayers { get; set; }
    public int totalPlayers { get; set; }
    public string type { get; set; }
    public Urls urls { get; set; }
    public int startSample { get; set; }
    public float videoLength { get; set; }
    public Player[] players { get; set; }
    public Fromlist[] fromList { get; set; }
    public bool correct { get; set; }
    public string selfAnswer { get; set; }
}

public class Anime
{
    public string english { get; set; }
    public string romaji { get; set; }
}

public class Urls
{
    public Catbox catbox { get; set; }
    public Openingsmoe openingsmoe { get; set; }
}

public class Catbox
{
    public string _0 { get; set; }
    public string _480 { get; set; }
    public string _720 { get; set; }
}

public class Openingsmoe
{
    public string _720 { get; set; }
}

public class Player
{
    public string name { get; set; }
    public int score { get; set; }
    public bool correct { get; set; }
    public string answer { get; set; }
    public int guessTime { get; set; }
    public bool active { get; set; }
    public int position { get; set; }
    public int positionSlot { get; set; }
}

public class Fromlist
{
    public string name { get; set; }
    public int listStatus { get; set; }
    public object score { get; set; }
}
