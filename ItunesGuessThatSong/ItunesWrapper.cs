using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using iTunesLib;

namespace ItunesPlaylistExport
{
	public class ItunesWrapper
	{
		
		static iTunesApp itunes = new iTunesLib.iTunesApp();

		public ItunesWrapper()
		{
			
		}

		public void PlayRandomSong()
		{
			IITTrackCollection tracks = itunes.LibraryPlaylist.Tracks;
			Random r = new Random();
			bool notPlaying = true;
			while (notPlaying)
			{
				try
				{
					int randomIndex = r.Next(0, tracks.Count);
					tracks[randomIndex].Play();
					notPlaying = false;
				}
				catch (Exception e)
				{
					
				}
			}
		}
		public string CurrentSongName()
		{
			return itunes.CurrentTrack.Name;
		}

		public void PlaySong()
		{
			itunes.Play();
		}

		public void PauseSong()
		{
			itunes.Pause();
		}

		public void NextSong()
		{
			itunes.NextTrack();
		}

		public void PreviousSong()
		{
			itunes.PreviousTrack();
		}
	}
}
