using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bun_Ducky
{
	public partial class Form1 : Form
	{
		List<GameSave> savedGames = new List<GameSave>();
		public Form1()
		{
			InitializeComponent();
			WindowState = FormWindowState.Maximized;
			pictureBox1.Dock = DockStyle.Fill;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Form2 f2 = new Form2(null);
			this.Hide();
			f2.Show();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			pictureBox1.Image = Image.FromFile("mainBG.jpeg");
			pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			button1.Enabled = false;
			button1.Visible = false;

			button2.Enabled = false;
			button2.Visible = false;

			savedGamesList.Visible = true;
			savedGamesList.Enabled = true;

			button3.Visible = false;

			button4.Visible = true;
			button4.Enabled = true;

			button5.Visible = true;
			button5.Enabled = true;
			ReadSavedGames();

		}

		void ReadSavedGames()
		{

			StreamReader sr = new StreamReader("saves.txt");
			int ct = 0;
			savedGames.Clear();
			savedGamesList.Items.Clear();
			while (!sr.EndOfStream)
			{
				string line = sr.ReadLine();
				if (ct > 0) // Skip header line
				{
					string[] temp = line.Split(',');
					GameSave save = new GameSave();
					save.id = Convert.ToInt16(temp[0]);        // ID
					save.level = Convert.ToInt16(temp[1]);     // Level
					save.score = Convert.ToInt16(temp[2]);     // Score
					save.hasKey = Convert.ToBoolean(temp[3]);  // HasKey
					save.isRat = Convert.ToBoolean(temp[4]);   // IsRat
					save.duckX = Convert.ToInt16(temp[5]);     // DuckX
					save.duckY = Convert.ToInt16(temp[6]);     // DuckY
					save.rabbitX = Convert.ToInt16(temp[7]);   // RabbitX
					save.rabbitY = Convert.ToInt16(temp[8]);   // RabbitY
					save.keysCollected = Convert.ToInt16(temp[9]);   // KeysCollected
					save.chicksCollected = Convert.ToInt16(temp[10]); // ChicksCollected
					save.repairedElevator = Convert.ToBoolean(temp[11]); // elevators code
					save.stoleTut = Convert.ToBoolean(temp[12]); // elevators code
					savedGames.Add(save);
					savedGamesList.Items.Add($"{save.id} - Level {save.level} - Score {save.score}");

				}

				// Display in listbox

				ct++;
			}
			sr.Close();
			if (savedGames.Count == 0)
			{
				savedGamesList.Items.Add("No saved games found");
				savedGamesList.Enabled = false;
			}
			else
			{
				savedGamesList.Enabled = true;
			}
		}
		public class GameSave
		{
			public int id;
			public int level;
			public int score;
			public bool hasKey;
			public bool isRat;
			public int duckX;
			public int duckY;
			public int rabbitX;
			public int rabbitY;
			public int keysCollected;
			public int chicksCollected;
            public bool repairedElevator;
            public bool stoleTut;
        }

        private void button4_Click(object sender, EventArgs e)
		{
			if (savedGamesList.SelectedIndex >= 0 && savedGamesList.SelectedIndex < savedGames.Count)
			{
				GameSave selectedSave = savedGames[savedGamesList.SelectedIndex];
				Form2 f2 = new Form2(selectedSave);
				this.Hide();
				f2.Show();
			}
			else
			{
				MessageBox.Show("Please select a saved game first");
			}
		}

		private void button5_Click(object sender, EventArgs e)
		{
			button1.Enabled = true;
			button1.Visible = true;
			button2.Enabled = true;
			button2.Visible = true;
			button3.Enabled = true;
			button3.Visible = true;
			button4.Visible = false;
			button4.Enabled = false;
			button5.Visible = false;
			button5.Enabled = false;
			savedGamesList.Visible = false;
			savedGamesList.Enabled = false;
		}
	}
}