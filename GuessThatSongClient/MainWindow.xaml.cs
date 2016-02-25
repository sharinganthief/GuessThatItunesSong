using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace GuessThatSongClient
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		GuessThatSongGame game = new GuessThatSongGame();

		public MainWindow()
		{
			InitializeComponent();


			submissionStatus.Content = string.Empty;
			scoreLabel.Content = game.GetScore();
			songName.Content = string.Empty;
			game.StartRound();
			entryBox.Focus();

		}

		private void textBox1_KeyPress(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == Key.Return || e.Key == Key.Enter)
			{
				e.Handled = true;
				string entry = entryBox.Text;
				if (game.CheckEntry(entry))
				{
					if (game.GameWon())
					{
						MessageBox.Show(Properties.Resources.MainWindow_textBox1_KeyPress_You_win____);
						game = new GuessThatSongGame();
					}

					scoreLabel.Content = game.GetScore();
					songName.Content = game.GetCurrentSongName();
					submissionStatus.Content = "Correct!";
					button1.Content = "Next";
					entryBox.Text = string.Empty;
					Button_Click(null, null);
				}
				else
				{
					submissionStatus.Content = "Incorrect!";
				}
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			button1.Content = "Skip";
			game.StartRound();
			entryBox.Focus();
		}
	}
}
