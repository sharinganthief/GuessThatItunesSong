using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using iTunesLib;

namespace ItunesPlaylistExport
{
	class Program
	{
		static iTunesApp itunes = new iTunesLib.iTunesApp();

		static void Main(string[] args)
		{
			string pathToExport = "D:\\Music\\cache";
			string playlistName = "S4 2";
			Export(playlistName, pathToExport);
		}

		private static void Export(string playlistName, string pathToExport)
		{
			//IITLibraryPlaylist mainLibrary = itunes.LibraryPlaylist;
			IITPlaylist playlist = itunes.LibrarySource.Playlists.Cast<IITPlaylist>().FirstOrDefault(pl => pl.Name.Equals(playlistName, StringComparison.InvariantCultureIgnoreCase));


			List<string> filePaths = GetTracks(playlist);

			int songCount = 0;
			foreach (string filePath in filePaths)
			{
				string fileName = Path.GetFileName(filePath);

				if (fileName == null) continue;

				List<char> invalidCharacters = Path.GetInvalidFileNameChars().ToList();
				invalidCharacters.AddRange(Path.GetInvalidPathChars());

				fileName = invalidCharacters.Aggregate(fileName, (current, invalidCharacter) => current.Replace(invalidCharacter, '.'));


				string destination = Path.Combine(pathToExport, fileName);

				Console.WriteLine("{0}. {1}", ++songCount, filePath);

				if (File.Exists(destination)) continue;

				File.Copy(filePath, destination);
			}

			Process.Start(pathToExport);
			Console.ReadLine();
		}

		// getthe tracks from the the specified playlist
		private static List<string> GetTracks(IITPlaylist playlist)
		{
			List<string> filePaths = new List<string>();

			// get the collection of tracks from the playlist
			IITTrackCollection tracks = playlist.Tracks;
			int numTracks = tracks.Count;
			for (int currTrackIndex = 1; currTrackIndex <= numTracks; currTrackIndex++)
			{


				IITTrack currTrack = tracks[currTrackIndex];

				if (!currTrack.Enabled)
				{
					continue;
				}

				if (currTrack.Kind == ITTrackKind.ITTrackKindFile)
				{
					IITFileOrCDTrack file = (IITFileOrCDTrack)currTrack;
					if (file.Location != null)
					{
						FileInfo fi = new FileInfo(file.Location);
						if (fi.Exists)
						{
							filePaths.Add(file.Location);

						}
						else
							Console.WriteLine("not found " + file.Location);
					}
				}
			}

			return filePaths;
		}
	}
}
