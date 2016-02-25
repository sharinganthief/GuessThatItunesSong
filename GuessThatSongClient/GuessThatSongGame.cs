using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuoVia.FuzzyStrings;
using ItunesPlaylistExport;

namespace GuessThatSongClient
{
	public class GuessThatSongGame
	{
		private readonly ItunesWrapper _wrapper = new ItunesWrapper();
		private readonly int _maxScore = 5;
		private int _currentScore = 0;
		public GuessThatSongGame()
		{
		}

		public GuessThatSongGame(int max)
		{
			_maxScore = max;
		}

		//List<int> triedSongs = new List<int>(); 

		public void StartRound()
		{
			_wrapper.PlayRandomSong();
		}

		public string GetCurrentSongName()
		{
			return _wrapper.CurrentSongName();
		}

		public bool CheckEntry(string entry)
		{
			string current = this.GetCurrentSongName();

			bool closeEnough = CloseEnough(entry, current);

			if (closeEnough)
			{
				this._currentScore++;
			}

			return closeEnough;
		}

		private bool CloseEnough(string entry, string current)
		{
			// Get a boolean determination of approximate equality

			string loweredEntry = entry.ToLower().Trim();
			string loweredcurrent = current.ToLower().Trim();

			if (loweredEntry.Equals(loweredcurrent)) return true;

			double dice = loweredEntry.DiceCoefficient(loweredcurrent);

			if (dice > .50)
			{
				return true;
			}
			
			int leven = loweredEntry.LevenshteinDistance(loweredcurrent);

			if (leven < 10)
			{
				return true;
			}
			Tuple<string, double> lcs = loweredEntry.LongestCommonSubsequence(loweredcurrent);

			if (lcs.Item2 > .50)
			{
				return true;
			}
			//string dme = entry.ToDoubleMetaphone();
			//string dmc = current.ToDoubleMetaphone();

			return false;
		}

		public string GetScore()
		{
			return String.Format("Score: {0}", _currentScore);
		}

		public bool GameWon()
		{
			return this._currentScore >= this._maxScore;
		}
	}
}
