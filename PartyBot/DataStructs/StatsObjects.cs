using System;

public class AnimeObject
{
	public AnimeObject()
	{
	}
}

public class SongStats
{
	List<PlayerStats> list;
	
}

public class PlayerStats
{
	public int totalTimesPlayed { get; set; };
	public int timesCorrect { get; set; };
	public bool fromList { get; set; };
}
