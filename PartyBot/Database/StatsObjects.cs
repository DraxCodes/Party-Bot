using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace csharpi.Database
{
	public class AnimeObject
	{
		[Key]
		public string name { get; set; }
		public int AnnId { get; set; }


		public List<SongStats> songs { get; set; }
		public AnimeObject()
		{
			name = "";
			songs = new List<SongStats>();
		}
		public AnimeObject(string n)
		{
			name = n;
			songs = new List<SongStats>();
		}
		public AnimeObject(string n, List<SongStats> s)
		{
			name = n;
			songs = s;
		}
	}

	public class SongStats
	{
		List<PlayerStats> list;
		public string songName { get; set; }
		public string artist { get; set; }
		public string type { get; set; }
		public string showName { get; set; }
	}

	public class PlayerStats
	{
		public string playerName { get; set; }
		public int totalTimesPlayed { get; set; }
		public int timesCorrect { get; set; }
		public bool fromList { get; set; }

		public PlayerStats()
		{
			playerName = "";
			totalTimesPlayed = 0;
			timesCorrect = 0;
			fromList = false;
		}
		public PlayerStats(string player)
		{
			playerName = player;
			totalTimesPlayed = 0;
			timesCorrect = 0;
			fromList = false;
		}
		public PlayerStats(string player, bool list)
		{
			playerName = player;
			totalTimesPlayed = 0;
			timesCorrect = 0;
			fromList = list;
		}

		public void Update(bool correct, bool list)
		{
			if (list)
				fromList = true;
			if (correct)
				timesCorrect += 1;
			totalTimesPlayed += 1;
		}
	}

	public class PlayerStatsByRule : PlayerStats
	{
		public string rule { get; set; }


		public PlayerStatsByRule(string player, string r)
		{
			playerName = player;
			totalTimesPlayed = 0;
			timesCorrect = 0;
			fromList = false;
			rule = r;
		}
		public PlayerStatsByRule(string player, bool list, string r)
		{
			playerName = player;
			totalTimesPlayed = 0;
			timesCorrect = 0;
			fromList = list;
			rule = r;
		}

		public bool RuleMatch(string tempRule)
		{
			if (tempRule.Equals(rule))
				return true;
			return false;
		}

	}
}
