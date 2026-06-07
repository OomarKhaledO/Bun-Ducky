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
using static Bun_Ducky.Form1;

namespace Bun_Ducky
{

	public partial class Form2 : Form
	{
		int lvl = 1;
		List<elevator> elevators = new List<elevator>();
		List<painting> paintings = new List<painting>();
		List<door> doors = new List<door>();
		List<items> item = new List<items>();
		List<bg> bgs = new List<bg>();
		List<hero> heros = new List<hero>();
		List<tile> tilesLvl1 = new List<tile>();
		List<tile> tilesPLvl1 = new List<tile>();
		List<tile> tilesPLvl2 = new List<tile>();
		List<tile> tilesWLvl1 = new List<tile>();
		List<ladder> ladders = new List<ladder>();
		List<key> keysLvl1 = new List<key>();
		List<sewer> sewers = new List<sewer>();
		List<box> boxes = new List<box>();
		List<brick> bricks = new List<brick>();
		List<chain> chains = new List<chain>();
		List<water> waters = new List<water>();
		List<chick> chicks = new List<chick>();
		List<frog> frogs = new List<frog>();
		List<stairs> stairss = new List<stairs>();
		List<security> securities = new List<security>();
		List<Bitmap> chickImgs = new List<Bitmap>();
        Bitmap off;
		Timer gameTimer = new Timer();
		bool showFrogDialog = false;
		bool showFrogDialog2 = false;
		int score = 0;
		int xStart = 0;
		int yStart = 0;
		int tilesXInc = 0;
		int tilesYInc = 0;
		int chainsYInc = 0;
		int waterOnCt = 0;
		bool showMenu = false;
		bool showChapterScreen = false;
		string chapterText = "";
		int chapterScreenTimer = 0;
		int chapterScreenDuration = 120;
		bool chapterScreenDone = false;
		GameSave save = new GameSave();
		int currentSaveId = -1;
        bool chickReloaded = false;
        int qCt = 0;
        Bitmap iconKey;
        Bitmap iconChick;
        Bitmap iconMonaliza;
        Bitmap iconTut;

        Font fontDash;
        Font fontDialog;
        Font fontDialog2;
        Font fontChapter;
        Font fontSurprised;
        Font fontLord;
        Font fontHmm;
        Brush brushYellow;
        Brush brushChapter;
        public Form2(GameSave save)
		{
			InitializeComponent();
			this.WindowState = FormWindowState.Maximized;
			this.save = save;
			Load += Form1_Load;
			Paint += Form1_Paint;
			KeyDown += Form1_KeyDown;
			KeyUp += Form1_KeyUp;
			gameTimer.Interval = 16;
			gameTimer.Tick += GameTimer_Tick;
			gameTimer.Start();
			FormClosed += Form2_FormClosed;

		}

		public void LoadGame(GameSave save)
		{
			currentSaveId = save.id;
			lvl = save.level;
			LoadLevel(lvl);
			score = save.score;
			heros[0].hasKey = save.hasKey;
			heros[0].isRat = save.isRat;
			heros[0].xDuck = save.duckX;
			heros[0].yDuck = save.duckY;
			heros[0].xRabbit = save.rabbitX;
			heros[0].yRabbit = save.rabbitY;
			heros[0].repairedElevator = save.repairedElevator;
			heros[0].stoleTut = save.stoleTut;
			
			for (int i = 0; i < save.chicksCollected && i < chicks.Count; i++)
			{
				if (chicks.Count > 0)
				{
					chicks.RemoveAt(chicks.Count - 1);
				}
			}
			if (save.hasKey)
			{
				keysLvl1.Clear();
			}
			if (save.chicksCollected > 0)
			{
				//MessageBox.Show("chicks collected > 0");
				chick dc = new chick();
				dc.imgs = new List<Bitmap>();
				for (int i = 0; i < 16; i++)
				{
					dc.imgs.Add(chickImgs[i]);
				}
				dc.x = 0;
				dc.y = 0;
				heros[0].distract = dc;
				heros[0].chickHoldCt = 0;
			}
            if (lvl == 2 && heros[0].repairedElevator && elevators.Count > 0)
            {
                elevators[0].moveDir = 1;
                elevators[0].speed = 2;
            }
            if (lvl == 2 && heros[0].stoleTut)
            {
                item.Clear();
				securities[1].state = 5;
            }
        }

		private void Form2_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			hero pnn = new hero();
			//   === < DUCK ===

			pnn.falling = new Bitmap("hFrames\\fall\\fall.png");
			//IDLE
			//RIGHT
			pnn.idelImgsDuckRight = new List<Bitmap>();

			for (int i = 0; i < 4; i++)
			{
				Bitmap b = new Bitmap("hFrames\\idel\\idle" + (i + 1) + ".png");

				pnn.idelImgsDuckRight.Add(b);
			}
			//LEFT
			pnn.idelImgsDuckLeft = new List<Bitmap>();
			for (int i = 0; i < 4; i++)
			{
				Bitmap b = new Bitmap("hFrames\\idel\\idle" + (i + 1) + ".png");

				pnn.idelImgsDuckLeft.Add(b);
			}
			//IDLE END
			//WALK
			//RIGHT
			pnn.walkImgsDuckRight = new List<Bitmap>();
			for (int i = 0; i < 4; i++)
			{
				Bitmap b = new Bitmap("hFrames\\walk\\Walk" + (i + 1) + ".png");

				pnn.walkImgsDuckRight.Add(b);
			}
			//LEFT
			pnn.walkImgsDuckLeft = new List<Bitmap>();
			for (int i = 0; i < 4; i++)
			{
				Bitmap b = new Bitmap("hFrames\\walk\\WalkB" + (i + 1) + ".png");

				pnn.walkImgsDuckLeft.Add(b);
			}
			//WALK END
			//RUN
			//RIGHT
			pnn.runImgsDuckRight = new List<Bitmap>();
			for (int i = 0; i < 4; i++)
			{
				Bitmap b = new Bitmap("hFrames\\run\\roll" + (i + 1) + ".png");

				pnn.runImgsDuckRight.Add(b);
			}
			//LEFT
			pnn.runImgsDuckLeft = new List<Bitmap>();
			for (int i = 0; i < 4; i++)
			{
				Bitmap b = new Bitmap("hFrames\\run\\roll" + (i + 1) + ".png");

				pnn.runImgsDuckLeft.Add(b);
			}
			//RUN END
			//ATTACK
			//RIGHT
			/*
			pnn.attkImgsDuckRight= new List<Bitmap>();
			for (int i = 0; i < 4; i++)
			{
				Bitmap b = new Bitmap("hFrames\\attack\\AttackB" + (i + 1) + ".png");
				pnn.attkImgsDuckRight.Add(b);
			}
			//LEFT
			pnn.attkImgsDuckLeft= new List<Bitmap>();
			for (int i = 0; i < 4; i++)
			{
				Bitmap b = new Bitmap("hFrames\\attack\\AttackB" + (i + 1) + ".png");
				pnn.attkImgsDuckLeft.Add(b);
			}
			*/
			//ATTACK END


			//CLIMB
			pnn.climbImgsDuck = new List<Bitmap>();
			for (int i = 0; i < 4; i++)
			{
				Bitmap b = new Bitmap("hFrames\\climb\\climb" + (i + 1) + ".png");
				pnn.climbImgsDuck.Add(b);
			}
			//JUMP
			//RIGHT
			pnn.jumpImgsDuckRight = new List<Bitmap>();
			for (int i = 0; i < 4; i++)
			{
				Bitmap b = new Bitmap("hFrames\\jump\\jump" + (i + 1) + ".png");

				pnn.jumpImgsDuckRight.Add(b);
			}
			//LEFT
			pnn.jumpImgsDuckLeft = new List<Bitmap>();
			for (int i = 0; i < 4; i++)
			{
				Bitmap b = new Bitmap("hFrames\\jump\\jump" + (i + 1) + ".png");

				pnn.jumpImgsDuckLeft.Add(b);
			}
			//JUMP END
			pnn.deathDuck = new Bitmap("hFrames\\death\\death.png");
			pnn.hitWallDuck = new Bitmap("hFrames\\hitWall\\wall_hit.png");
			//   === DUCK /> ===


			// = = = = = = = = = = = = = = = = = = =
			//   === < RABBIT ===
			pnn.xRabbit = pnn.xDuck;
			pnn.yRabbit = pnn.yDuck - 30;
			//IDLE
			//RIGHT
			pnn.idleImgsRabbitRight = new List<Bitmap>();

			for (int i = 0; i < 6; i++)
			{
				Bitmap b = new Bitmap("rFrames\\idel\\Idle" + (i + 1) + ".png");

				pnn.idleImgsRabbitRight.Add(b);
			}
			//LEFT
			pnn.idleImgsRabbitLeft = new List<Bitmap>();
			for (int i = 0; i < 6; i++)
			{
				Bitmap b = new Bitmap("rFrames\\idel\\IdleB" + (i + 1) + ".png");

				pnn.idleImgsRabbitLeft.Add(b);
			}
			//IDLE END
			//WALK
			//RIGHT
			pnn.walkImgsRabbitRight = new List<Bitmap>();
			for (int i = 0; i < 8; i++)
			{
				Bitmap b = new Bitmap("rFrames\\walk\\Walk" + (i + 1) + ".png");

				pnn.walkImgsRabbitRight.Add(b);
			}
			//LEFT
			pnn.walkImgsRabbitLeft = new List<Bitmap>();
			for (int i = 0; i < 8; i++)
			{
				Bitmap b = new Bitmap("rFrames\\walk\\WalkB" + (i + 1) + ".png");

				pnn.walkImgsRabbitLeft.Add(b);
			}
			//WALK END
			//RUN
			//RIGHT
			pnn.runImgsRabbitRight = new List<Bitmap>();
			for (int i = 0; i < 8; i++)
			{
				Bitmap b = new Bitmap("rFrames\\run\\Run" + (i + 1) + ".png");

				pnn.runImgsRabbitRight.Add(b);
			}
			//LEFT
			pnn.runImgsRabbitLeft = new List<Bitmap>();
			for (int i = 0; i < 8; i++)
			{
				Bitmap b = new Bitmap("rFrames\\run\\RunB" + (i + 1) + ".png");

				pnn.runImgsRabbitLeft.Add(b);
			}
			//RUN END
			//CLIMB
			pnn.climbImgsRabbitRight = new List<Bitmap>();
			for (int i = 0; i < 3; i++)
			{
				Bitmap b = new Bitmap("rFrames\\climb\\climb" + (i + 1) + ".png");

				pnn.climbImgsRabbitRight.Add(b);
			}
			pnn.rabbitMonalisa = new Bitmap("rFrames\\monalisa\\rabbitMonalisa.png");
			pnn.rabbitTut = new Bitmap("rFrames\\tut\\rabbitTut.png");
			//   === RABBIT /> ===
			pnn.transformImgs = new List<Bitmap>();
			for (int i = 0; i < 10; i++)
			{
				Bitmap b = new Bitmap("hFrames\\transform\\t" + (i + 1) + ".png");
				pnn.transformImgs.Add(b);
            }
            heros.Add(pnn);
			LoadLevel(lvl);
			if (save != null)
			{
				LoadGame(save);
			}
            iconKey = new Bitmap("lvl1\\key.png");
            iconChick = new Bitmap("lvl1\\chick\\chick12.png");
            iconMonaliza = new Bitmap("lvl2\\monaliza.png");
            iconTut = new Bitmap("lvl2\\tut2.png");

            fontDash = new Font("Arial", 16, FontStyle.Bold);
            fontDialog = new Font("Arial", 14, FontStyle.Bold);
            fontDialog2 = new Font("Arial", 12, FontStyle.Bold);
            fontChapter = new Font("Arial", 36, FontStyle.Bold);
            fontSurprised = new Font("Arial", 22, FontStyle.Bold);
            fontLord = new Font("Arial", 18, FontStyle.Bold);
            fontHmm = new Font("Arial", 18, FontStyle.Bold);
            brushYellow = new SolidBrush(Color.Yellow);

            //===========
            //LVL 2 START
            //===========
            tilesXInc = 0;



			off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);

			/*
			 if (lvl == 1)
			{
				pnn.xDuck = 650;
				pnn.yDuck = 950;
			}
			else if (lvl == 2)
			{
				pnn.xDuck = 950;
				pnn.yDuck = 950;
			} 
			 */
		}


		bool duckOnTile()
		{
			int duckFeetY = heros[0].yDuck + 70;
			int duckCenterX = heros[0].xDuck + 35;
			for (int i = 0; i < tilesLvl1.Count; i++)
			{
				if (duckCenterX >= tilesLvl1[i].x && duckCenterX <= tilesLvl1[i].x + tilesLvl1[i].img.Width)
				{
					if (duckFeetY >= tilesLvl1[i].y && duckFeetY <= tilesLvl1[i].y + 15)
					{
						return true;
					}
				}
			}
			return false;
		}


		bool duckOnTileP()
		{
			int duckFeetY = heros[0].yDuck + 70;
			int duckCenterX = heros[0].xDuck + 35;
			for (int i = 0; i < tilesPLvl1.Count; i++)
			{
				if (duckCenterX >= tilesPLvl1[i].x && duckCenterX <= tilesPLvl1[i].x + 50)
				{
					if (duckFeetY >= tilesPLvl1[i].y && duckFeetY <= tilesPLvl1[i].y + 15)
					{
						return true;
					}
				}
			}
			return false;
		}

		bool rabbitOnTileP()
		{
			int rabbitFeetY = heros[0].yRabbit + 100;
			int rabbitCenterX = heros[0].xRabbit + 50;
			for (int i = 0; i < tilesPLvl1.Count; i++)
			{
				if (rabbitCenterX >= tilesPLvl1[i].x && rabbitCenterX <= tilesPLvl1[i].x + 50)
				{
					if (rabbitFeetY >= tilesPLvl1[i].y && rabbitFeetY <= tilesPLvl1[i].y + 15)
					{
						return true;
					}
				}
			}
			return false;
		}


		bool duckOnBox()
		{
			int duckFeetY = heros[0].yDuck + 70;
			int duckCenterX = heros[0].xDuck + 35;
			for (int i = 0; i < boxes.Count; i++)
			{
				if (duckCenterX >= boxes[i].x && duckCenterX <= boxes[i].x + 120)
				{
					if (duckFeetY >= boxes[i].y && duckFeetY <= boxes[i].y + 15)
					{
						return true;
					}
				}
			}
			return false;
		}


		bool snapDuckToGround(int oldFeetY, int newFeetY)
		{
			int duckFeetY = heros[0].yDuck + 70;
			int duckCenterX = heros[0].xDuck + 35;

			// check normal tiles
			for (int i = 0; i < tilesLvl1.Count; i++)
			{
				if (duckCenterX >= tilesLvl1[i].x && duckCenterX <= tilesLvl1[i].x + tilesLvl1[i].img.Width)
				{
					if (oldFeetY <= tilesLvl1[i].y &&
						newFeetY >= tilesLvl1[i].y)
					{
						heros[0].yDuck = tilesLvl1[i].y - 70;
						return true;
					}

				}
			}

			// check tileP tiles
			for (int i = 0; i < tilesPLvl1.Count; i++)
			{
				if (duckCenterX >= tilesPLvl1[i].x && duckCenterX <= tilesPLvl1[i].x + 50)
				{
					if (oldFeetY <= tilesPLvl1[i].y &&
						newFeetY >= tilesPLvl1[i].y)
					{
						heros[0].yDuck = tilesPLvl1[i].y - 70;
						return true;
					}
				}
			}

			// check boxes
			for (int i = 0; i < boxes.Count; i++)
			{
				if (duckCenterX >= boxes[i].x && duckCenterX <= boxes[i].x + 120)
				{

					if (oldFeetY <= boxes[i].y &&
						newFeetY >= boxes[i].y)
					{
						heros[0].yDuck = boxes[i].y - 70;
						return true;
					}
				}
			}
			if (lvl == 2 && elevators.Count > 0)
			{
				tile floor = elevators[0].floor;
				if (duckCenterX >= floor.x && duckCenterX <= floor.x + elevators[0].img.Width)
				{
					if (oldFeetY <= floor.y && newFeetY >= floor.y)
					{
						heros[0].yDuck = floor.y - 70;
						return true;
					}
				}
			}
			return false;
		}


		bool duckOnLadder()
		{
			int duckCenterX = heros[0].xDuck + 35;
			int duckCenterY = heros[0].yDuck + 35;
			for (int i = 0; i < ladders.Count; i++)
			{
				int ladderRight = ladders[i].x + ladders[i].img.Width + 20;
				int ladderBottom = ladders[i].y + ladders[i].img.Height;

				if (duckCenterX >= ladders[i].x && duckCenterX <= ladderRight)
				{
					if (duckCenterY + 10 >= ladders[i].y - 10 && duckCenterY <= ladderBottom)
					{
						return true;
					}
				}
			}
			return false;
		}


		bool rabbitOnSewer()
		{
			int rabbitCenterX = heros[0].xRabbit + 50;
			int rabbitTop = heros[0].yRabbit;
			int rabbitBottom = heros[0].yRabbit + 100;

			for (int i = 0; i < sewers.Count; i++)
			{
				int sewerLeft = sewers[i].x;
				int sewerRight = sewers[i].x + sewers[i].img.Width + 10;
				int sewerTop = sewers[i].y;
				int sewerBottom = sewers[i].y + sewers[i].img.Height;

				if (rabbitCenterX >= sewerLeft && rabbitCenterX <= sewerRight)
				{
					if (rabbitBottom >= sewerTop && rabbitTop <= sewerBottom)
						return true;
				}
			}
			return false;
		}

		void applyGravityDuck()
		{
			if (duckOnLadder())
			{
				return;
			}
			if (heros[0].isClimbDuckUp || heros[0].isClimbDuckDn)
			{
				return;
			}
			if (heros[0].isJumpDuckUp || heros[0].isJumpDuckRight || heros[0].isJumpDuckLeft)
			{
				return;
			}
			if (duckOnTile() || duckOnTileP() || duckOnBox() || duckOnElevatorFloor())
			{
				heros[0].fallingFrameCount = 0;
				return;
			}

			int oldFeetY = heros[0].yDuck + 70;
			heros[0].fallingFrameCount++;
			heros[0].yDuck += 8;
			int newFeetY = heros[0].yDuck + 70;

			if (duckOnTile() || duckOnTileP() || duckOnBox() || duckOnElevatorFloor())
			{
				snapDuckToGround(oldFeetY, newFeetY);
			}
		}
		void checkWall(bool isRat)
		{
			if (isRat)
			{
				int rabbitRight = heros[0].xRabbit + heros[0].runImgsRabbitRight[0].Width;
				if (rabbitRight >= xStart + this.ClientSize.Width - 50)
				{
					heros[0].xRabbit = heros[0].xRabbit - 20;
				}
				if (heros[0].xRabbit <= 0)
				{
					heros[0].xRabbit = heros[0].xRabbit + 20;
				}
			}
			else
			{
				int duckRight = heros[0].xDuck + heros[0].walkImgsDuckRight[0].Width;

				if (duckRight >= xStart + this.ClientSize.Width)
				{
					if (heros[0].isRunDuck)
					{
						heros[0].isHitWall = true;
					}
					heros[0].xDuck = heros[0].xDuck - 20;
				}
				if (heros[0].xDuck <= 0)
				{
					if (heros[0].isRunDuck)
					{
						heros[0].isHitWall = true;
					}
					heros[0].xDuck = heros[0].xDuck + 20;
				}

			}

		}
		bool rabbitOnTile()
		{
			int rabbitFeetY = heros[0].yRabbit + 100;
			int rabbitCenterX = heros[0].xRabbit + 50;
			for (int i = 0; i < tilesLvl1.Count; i++)
			{
				if (rabbitCenterX >= tilesLvl1[i].x && rabbitCenterX <= tilesLvl1[i].x + tilesLvl1[i].img.Width)
				{
					if (rabbitFeetY >= tilesLvl1[i].y && rabbitFeetY <= tilesLvl1[i].y + 15)
						return true;
				}
			}
			return false;
		}



		bool rabbitOnBox()
		{
			int rabbitFeetY = heros[0].yRabbit + 100;
			int rabbitCenterX = heros[0].xRabbit + 50;
			for (int i = 0; i < boxes.Count; i++)
			{
				if (rabbitCenterX >= boxes[i].x && rabbitCenterX <= boxes[i].x + 120)
				{
					if (rabbitFeetY >= boxes[i].y && rabbitFeetY <= boxes[i].y + 15)
						return true;
				}
			}
			return false;
		}

		bool snapRabbitToGround(int oldFeetY, int newFeetY)
		{
			int rabbitCenterX = heros[0].xRabbit + 50;

			for (int i = 0; i < tilesLvl1.Count; i++)
			{
				if (rabbitCenterX >= tilesLvl1[i].x && rabbitCenterX <= tilesLvl1[i].x + tilesLvl1[i].img.Width)
				{
					if (oldFeetY <= tilesLvl1[i].y && newFeetY >= tilesLvl1[i].y)
					{
						heros[0].yRabbit = tilesLvl1[i].y - 100;
						return true;
					}
				}
			}
			for (int i = 0; i < tilesPLvl1.Count; i++)
			{
				if (rabbitCenterX >= tilesPLvl1[i].x && rabbitCenterX <= tilesPLvl1[i].x + 50)
				{
					if (oldFeetY <= tilesPLvl1[i].y && newFeetY >= tilesPLvl1[i].y)
					{
						heros[0].yRabbit = tilesPLvl1[i].y - 100;
						return true;
					}
				}
			}
			for (int i = 0; i < boxes.Count; i++)
			{
				if (rabbitCenterX >= boxes[i].x && rabbitCenterX <= boxes[i].x + 120)
				{
					if (oldFeetY <= boxes[i].y && newFeetY >= boxes[i].y)
					{
						heros[0].yRabbit = boxes[i].y - 100;
						return true;
					}
				}
			}
			return false;
		}
		bool duckOnSpecificTileP()
		{
			if (lvl != 1 || tilesPLvl1.Count < 4)
			{
				return false;
			}
			int duckFeetY = heros[0].yDuck + 70;
			int duckCenterX = heros[0].xDuck + 35;
			if (duckCenterX >= tilesPLvl1[3].x && duckCenterX <= tilesPLvl1[3].x + 50)
			{
				if (duckFeetY >= tilesPLvl1[3].y && duckFeetY <= tilesPLvl1[3].y + 15)
				{
					return true;
				}
			}
			return false;
		}

		bool rabbitOnSpecificTileP(tile t)
		{
			if (lvl != 1)
			{
				return false;
			}
			int rabbitFeetY = heros[0].yRabbit + 70;
			int rabbitCenterX = heros[0].xRabbit + 35;
			if (rabbitCenterX >= t.x && rabbitCenterX <= t.x + 50)
			{
				if (rabbitFeetY >= t.y && rabbitFeetY <= t.y + 15)
				{
					return true;
				}
			}
			return false;
		}
		bool duckOnElevatorFloor()
		{
			if (lvl != 2 || elevators.Count == 0)
			{
				return false;
			}
			tile floor = elevators[0].floor;
			int duckFeetY = heros[0].yDuck + 70;
			int duckCenterX = heros[0].xDuck + 35;
			if (duckCenterX >= floor.x && duckCenterX <= floor.x + elevators[0].img.Width)
			{
				if (duckFeetY >= floor.y && duckFeetY <= floor.y + 15)
				{
					return true;
				}
			}
			return false;
		}

		bool rabbitOnElevatorFloor()
		{
			if (lvl != 2 || elevators.Count == 0)
			{
				return false;
			}
			tile floor = elevators[0].floor;
			int rabbitFeetY = heros[0].yRabbit + 100;
			int rabbitCenterX = heros[0].xRabbit + 50;
			if (rabbitCenterX >= floor.x && rabbitCenterX <= floor.x + elevators[0].img.Width)
			{
				if (rabbitFeetY >= floor.y && rabbitFeetY <= floor.y + 15)
				{
					return true;
				}
			}
			return false;
		}
		void applyGravityRabbit()
		{
			if (heros[0].isClimbRabbitUp || heros[0].isClimbRabbitDn)
			{
				return;
			}
			if (rabbitOnTile() || rabbitOnTileP() || rabbitOnBox() || rabbitOnElevatorFloor())
			{
				return;
			}

			int oldFeetY = heros[0].yRabbit + 100;
			heros[0].yRabbit += 8;
			int newFeetY = heros[0].yRabbit + 100;

			if (rabbitOnTile() || rabbitOnTileP() || rabbitOnBox() || rabbitOnElevatorFloor())
			{
				snapRabbitToGround(oldFeetY, newFeetY);
			}
		}

		bool duckCollidesWithWall(int newX)
		{
			int duckLeft = newX;
			int duckRight = newX + 70;
			int duckTop = heros[0].yDuck;
			int duckBottom = heros[0].yDuck + 70;

			for (int i = 0; i < tilesWLvl1.Count; i++)
			{
				int wallLeft = tilesWLvl1[i].x;
				int wallRight = tilesWLvl1[i].x + tilesWLvl1[i].img.Width;
				int wallTop = tilesWLvl1[i].y;
				int wallBottom = tilesWLvl1[i].y + (tilesWLvl1[i].img.Height - 70);

				if (duckRight > wallLeft && duckLeft < wallRight)
				{
					if (duckBottom > wallTop && duckTop < wallBottom)
					{
						return true;
					}
				}
			}
			return false;
		}


		bool rabbitCollidesWithWall(int newX)
		{
			int currentX = heros[0].xRabbit;
			int rabbitLeft = newX;
			int rabbitRight = newX + 100;
			int rabbitTop = heros[0].yRabbit;
			int rabbitBottom = heros[0].yRabbit + 100;
			for (int i = 0; i < tilesWLvl1.Count; i++)
			{
				int wallLeft = tilesWLvl1[i].x;
				int wallRight = tilesWLvl1[i].x + tilesWLvl1[i].img.Width;
				int wallTop = tilesWLvl1[i].y;
				int wallBottom = tilesWLvl1[i].y + (tilesWLvl1[i].img.Height - 70);
				if (rabbitRight - 50 > wallLeft && rabbitLeft < wallRight)
				{
					if (rabbitBottom > wallTop && rabbitTop < wallBottom)
					{
						if (newX > currentX && currentX + 100 <= wallRight)
						{
							return true;
						}
						if (newX < currentX && currentX >= wallLeft)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		bool boxCollidesWithWall(int newX, int boxIndex)
		{
			int boxLeft = newX;
			int boxRight = newX + 120;
			int boxTop = boxes[boxIndex].y;
			int boxBottom = boxes[boxIndex].y + 120;

			for (int i = 0; i < tilesWLvl1.Count; i++)
			{
				int wallLeft = tilesWLvl1[i].x;
				int wallRight = tilesWLvl1[i].x + tilesWLvl1[i].img.Width;
				int wallTop = tilesWLvl1[i].y;
				int wallBottom = tilesWLvl1[i].y + (tilesWLvl1[i].img.Height - 70);

				if (boxRight > wallLeft && boxLeft < wallRight)
				{
					if (boxBottom > wallTop && boxTop < wallBottom)
					{
						return true;
					}
				}
			}
			return false;
		}
		int tileP_chainCt = 0;

		void LoadLevel(int newLvl)
		{
			tilesXInc = 0;
			tilesYInc = 0;
			chainsYInc = 0;

			doors.Clear();
			tilesLvl1.Clear();
			tilesPLvl1.Clear();
			tilesWLvl1.Clear();
			ladders.Clear();
			keysLvl1.Clear();
			sewers.Clear();
			boxes.Clear();
			chains.Clear();
			waters.Clear();
			chicks.Clear();
			frogs.Clear();
			bgs.Clear();
			stairss.Clear();
			elevators.Clear();
			securities.Clear();
			item.Clear();
			paintings.Clear();


			if (heros.Count > 0)
			{
				heros[0].isRat = false;
				heros[0].isWalkDuck = false;
				heros[0].isRunDuck = false;
				heros[0].isRightDuck = false;
				heros[0].isLeftDuck = false;
				heros[0].isJumpDuckUp = false;
				heros[0].isJumpDuckRight = false;
				heros[0].isJumpDuckLeft = false;
				heros[0].isClimbDuckUp = false;
				heros[0].isClimbDuckDn = false;
				heros[0].jumpVelocity = 0;
				heros[0].fallingFrameCount = 0;
				heros[0].isDead = false;
			}

			if (newLvl == 1)
			{
				if (heros.Count > 0 && currentSaveId == -1)
				{
					heros[0].xDuck = 650;
					heros[0].yDuck = 950;
					heros[0].xRabbit = 650;
					heros[0].yRabbit = 920;
					heros[0].hasKey = false;
				}

				bg bb = new bg();
				bb.img = new Bitmap("bg.png");
				bb.X = 0;
				bb.Y = 0;
				bgs.Add(bb);

				door d = new door();
				d.img = new Bitmap("lvl1\\door.png");
				d.img.MakeTransparent();
				d.x = 1935;
				d.y = 130;
				doors.Add(d);

				door dd = new door();
				dd.x = 650;
				dd.y = 805;
				dd.img = new Bitmap("lvl1\\startDoor.png");
				doors.Add(dd);

				water g0 = new water();
				g0.x = 100;
				g0.y = 100;
				g0.imgs = new List<Bitmap>();
				for (int j = 0; j < 8; j++)
				{
					Bitmap m = new Bitmap("lvl1\\water\\W100" + (j + 1) + ".png");
					g0.imgs.Add(m);
				}
				waters.Add(g0);

				chick ck = new chick();
				ck.x = 220;
				ck.y = 395;
				ck.imgs = new List<Bitmap>();
				for (int j = 0; j < 16; j++)
				{
					Bitmap m = new Bitmap("lvl1\\chick\\chick" + (j + 1) + ".png");
					chickImgs.Add(m);
					ck.imgs.Add(m);
				}
				chicks.Add(ck);


				
				key gk = new key();
				gk.x = 1725;
				gk.y = 1360;
				gk.imgs = new List<Bitmap>();
				for (int j = 0; j < 24; j++)
				{
					Bitmap m = new Bitmap("lvl1\\key\\key" + (j + 1) + ".png");
					gk.imgs.Add(m);
				}
				keysLvl1.Add(gk);

				for (int i = 0; i < 9; i++)
				{
					tile g = new tile();
					g.x = 740 + tilesXInc;
					g.y = 1670;
					g.img = new Bitmap("lvl1\\tiles\\tile4.png");
					tilesXInc += 70;
					tilesLvl1.Add(g);
				}

				box bx1 = new box();
				bx1.x = 305;
				bx1.y = 930;
				bx1.img = new Bitmap("lvl1\\box.png");
				boxes.Add(bx1);

				box bx2 = new box();
				bx2.x = 930;
				bx2.y = 1600;
				bx2.img = new Bitmap("lvl1\\box.png");
				boxes.Add(bx2);

				tilesXInc = 0;
				for (int i = 0; i < 14; i++)
				{
					tile g = new tile();
					g.x = 0 + tilesXInc;
					g.y = 1020;
					g.img = new Bitmap("lvl1\\tiles\\tile4.png");
					tilesXInc += 70;
					tilesLvl1.Add(g);
				}
				for (int i = 0; i < 12; i++)
				{
					tile g = new tile();
					g.x = 0 + tilesXInc;
					g.y = 1020;
					g.img = new Bitmap("lvl1\\tiles\\tile.png");
					tilesXInc += 70;
					tilesLvl1.Add(g);
				}
				tilesXInc = 0;

				tile t02 = new tile();
				t02.x = 855;
				t02.y = 180;
				t02.img = new Bitmap("lvl1\\tiles\\tile02.png");
				tilesLvl1.Add(t02);

				for (int i = 0; i < 2; i++)
				{
					ladder l = new ladder();
					l.x = 100;
					l.y = 660 + tilesYInc;
					l.img = new Bitmap("lvl1\\ladder.png");
					tilesYInc += 40;
					ladders.Add(l);
				}
				tilesYInc = 0;
				for (int i = 0; i < 2; i++)
				{
					ladder l = new ladder();
					l.x = 10;
					l.y = 400 + tilesYInc;
					l.img = new Bitmap("lvl1\\ladder.png");
					tilesYInc += 40;
					ladders.Add(l);
				}
				tilesYInc = 0;
				for (int i = 0; i < 3; i++)
				{
					ladder l = new ladder();
					l.x = 1220;
					l.y = 970 + tilesYInc;
					l.img = new Bitmap("lvl1\\ladder.png");
					tilesYInc += 200;
					ladders.Add(l);
				}
				tilesYInc = 0;

				for (int i = 0; i < 2; i++)
				{
					tile g = new tile();
					g.x = 0 + tilesXInc;
					g.y = 686;
					g.img = new Bitmap("lvl1\\tiles\\tile4.png");
					tilesXInc += 70;
					tilesLvl1.Add(g);
				}
				tilesXInc = 0;

				for (int i = 0; i < 5; i++)
				{
					tile g = new tile();
					g.x = 300 + tilesXInc;
					g.y = 640 + (-tilesYInc);
					g.img = new Bitmap("lvl1\\tiles\\tilleP.png");
					tilesXInc += 100;
					tilesYInc += 50;
					tilesPLvl1.Add(g);
				}
				tilesXInc = 0;
				tilesYInc = 0;

				chain g1 = new chain();
				g1.x = 600;
				g1.y = 10 + chainsYInc;
				g1.img = new Bitmap("lvl1\\chain.png");
				chainsYInc += g1.img.Height - 50;
				chains.Add(g1);

				chain g2c = new chain();
				g2c.x = 600;
				g2c.y = 10 + chainsYInc;
				g2c.img = new Bitmap("lvl1\\chain.png");
				chainsYInc += g2c.img.Height - 50;
				chains.Add(g2c);

				chain g3c = new chain();
				g3c.x = 600;
				g3c.y = 10 + chainsYInc;
				g3c.img = new Bitmap("lvl1\\chain.png");
				chains.Add(g3c);
				chainsYInc = 50;

				for (int i = 0; i < 20; i++)
				{
					tile g = new tile();
					g.x = 740 + tilesXInc;
					g.y = 400;
					g.img = new Bitmap("lvl1\\tiles\\tile4.png");
					tilesXInc += 70;
					tilesLvl1.Add(g);
				}
				tilesXInc = 0;

				for (int i = 0; i < 20; i++)
				{
					tile g = new tile();
					g.x = 0 + tilesXInc;
					g.y = 1060;
					g.img = new Bitmap("lvl1\\bricks2.png");
					tilesXInc += g.img.Width;
					tilesLvl1.Add(g);
				}
				tilesXInc = 0;

				for (int i = 0; i < 10; i++)
				{
					tile g = new tile();
					g.x = 740 + tilesXInc;
					g.y = 400;
					g.img = new Bitmap("lvl1\\tiles\\tile4.png");
					tilesXInc += 70;
					tilesLvl1.Add(g);
				}
				tilesXInc = 0;

				for (int i = 0; i < 4; i++)
				{
					tile g = new tile();
					g.x = 0 + tilesXInc;
					g.y = 420;
					g.img = new Bitmap("lvl1\\tiles\\tile01.png");
					tilesXInc += 70;
					tilesLvl1.Add(g);
				}
				tilesXInc = 0;

				for (int i = 0; i < 5; i++)
				{
					tile l = new tile();
					l.x = 1640 + tilesXInc;
					l.y = 1400;
					l.img = new Bitmap("lvl1\\tiles\\tile3.png");
					tilesXInc += 70;
					tilesLvl1.Add(l);
				}
				tilesXInc = 0;

				for (int i = 0; i < 30; i++)
				{
					tile l = new tile();
					l.x = 0 + tilesXInc;
					l.y = 1260;
					l.img = new Bitmap("lvl1\\tiles\\tile3.png");
					tilesXInc += 70;
					tilesLvl1.Add(l);
				}
				tilesXInc = 0;

				tile tw1 = new tile();
				tw1.x = 1635;
				tw1.y = 1260;
				tw1.img = new Bitmap("lvl1\\tiles\\tile3U.png");
				tilesWLvl1.Add(tw1);

				for (int i = 0; i < 5; i++)
				{
					tile l = new tile();
					l.x = 1860;
					l.y = 550 + tilesYInc;
					l.img = new Bitmap("lvl1\\tiles\\tile3U.png");
					tilesYInc += 70;
					tilesWLvl1.Add(l);
				}
				tilesYInc = 0;

				for (int i = 0; i < 4; i++)
				{
					tile l = new tile();
					l.x = 1540 + tilesXInc;
					l.y = 1680 + tilesYInc;
					l.img = new Bitmap("lvl1\\tiles\\tilleP.png");
					tilesXInc += 20;
					tilesYInc += 20;
					tilesPLvl1.Add(l);
				}
				tilesXInc = 0;
				tilesYInc = 0;

				for (int i = 0; i < 5; i++)
				{
					sewer l = new sewer();
					l.x = 2190;
					l.y = 1320 + tilesYInc;
					l.img = new Bitmap("lvl1\\sewer.png");
					tilesYInc += 50;
					sewers.Add(l);
				}
				tilesYInc = 0;

				for (int i = 0; i < 3; i++)
				{
					sewer l = new sewer();
					l.x = 1200;
					l.y = 100 + tilesYInc;
					l.img = new Bitmap("lvl1\\sewer2.png");
					tilesYInc += 50;
					sewers.Add(l);
				}
				tilesYInc = 0;

				for (int i = 0; i < 10; i++)
				{
					tile l = new tile();
					l.x = 2220;
					l.y = 1140 + tilesYInc;
					l.img = new Bitmap("lvl1\\tiles\\tile3U.png");
					tilesYInc += 50;
					tilesWLvl1.Add(l);
				}
				tilesYInc = 0;

				for (int i = 0; i < 3; i++)
				{
					tile l = new tile();
					l.x = 700 + tilesXInc;
					l.y = 1730;
					l.img = new Bitmap("lvl1\\tiles\\tile02.png");
					tilesXInc += 5;
					tilesWLvl1.Add(l);
				}
				tilesXInc = 0;

				for (int i = 0; i < 8; i++)
				{
					tile l = new tile();
					l.x = 1565 + tilesXInc;
					l.y = 1750;
					l.img = new Bitmap("lvl1\\tiles\\tile5.png");
					tilesXInc += 70;
					tilesLvl1.Add(l);
				}
				for (int i = 0; i < 2; i++)
				{
					tile l = new tile();
					l.x = 1565 + tilesXInc;
					l.y = 1750;
					l.img = new Bitmap("lvl1\\tiles\\tile5-0.5.png");
					tilesXInc += 70;
					tilesLvl1.Add(l);
				}
				tilesXInc = 0;

				tile tw2 = new tile();
				tw2.x = 756;
				tw2.y = 1420;
				tw2.img = new Bitmap("lvl1\\tiles\\tile3U.png");
				tilesWLvl1.Add(tw2);

				frog f = new frog();
				f.x = 1890;
				f.y = 1700;
				f.startX = f.x;
				f.IdleImgsFrog = new List<Bitmap>();
				f.isIdle = true;
				for (int i = 0; i < 8; i++)
				{
					Bitmap b = new Bitmap("lvl1\\frog\\idle\\frogIdle" + (i + 1) + ".png");
					f.IdleImgsFrog.Add(b);
				}
				frogs.Add(f);

                //sewer frgo
                frog f2 = new frog();
                f2.x = 990;
                f2.y = 120;
                f2.startX = f2.x;
                f2.IdleImgsFrog = new List<Bitmap>();
                f2.isIdle = true;
                for (int i = 0; i < 8; i++)
                {
                    Bitmap b = new Bitmap("lvl1\\frog\\idle\\frogIdle" + (i + 1) + ".png");
                    f2.IdleImgsFrog.Add(b);
                }
                frogs.Add(f2);

                showChapterScreen = true;
				chapterText = "Chapter 1: The Sewer";
				chapterScreenTimer = 0;
			}
			else if (newLvl == 2)
			{
				elevator ev = new elevator();
				ev.img = new Bitmap("lvl2\\elevator.png");
				ev.x = 1300;
				ev.y = 1750;
				ev.topY = 1240;
				ev.bottomY = 1751;
				ev.floor = new tile();
				ev.floor.x = ev.x;
				ev.floor.y = ev.y + ev.img.Height - 70;
				ev.floor.img = new Bitmap("lvl1\\tiles\\nothing.png");
				ev.moveDir = 0;
                tilesPLvl1.Add(ev.floor);
				elevators.Clear();
				elevators.Add(ev);
				if (heros.Count > 0 && currentSaveId == -1)
				{
					heros[0].xDuck = 915;
					heros[0].yDuck = 2094;
					heros[0].xRabbit = heros[0].xDuck;
					heros[0].yRabbit = heros[0].yDuck - 30;
					heros[0].hasKey = false;
				}

				painting monaliza = new painting();
				monaliza.x = 1200;
				monaliza.y = 280;
				monaliza.width = 160;
				monaliza.height = 220;
				monaliza.img = new Bitmap("lvl2\\monaliza.png");
				paintings.Add(monaliza);
				items itemm = new items();
				itemm.x = 1859;
				itemm.y = 920;
				itemm.width = 180;
				itemm.height = 200;
				itemm.img = new Bitmap("lvl2\\tut2.png");
				item.Add(itemm);
				bg bb = new bg();
				bb.img = new Bitmap("bg2.png");
				bb.X = 0;
				bb.Y = 0;
				bgs.Add(bb);

				for (int i = 0; i < 8; i++)
				{
					tile t = new tile();
					t.x = 0 + tilesXInc;
					t.y = 2164;
					t.img = new Bitmap("lvl2\\tiles\\tile1.png");
					tilesXInc += 129;
					tilesLvl1.Add(t);
				}
				tilesXInc = 0;

				for (int i = 0; i < 4; i++)
				{
					ladder t = new ladder();
					t.x = 100;
					t.y = 950 + tilesYInc - 4;
					t.img = new Bitmap("lvl2\\ladder.png");
					t.img.MakeTransparent(t.img.GetPixel(0, 0));

					tilesYInc += t.img.Height;
					ladders.Add(t);
				}
				tilesYInc = 0;

				for (int i = 0; i < 8; i++)
				{
					tile t = new tile();
					t.x = 0 + tilesXInc;
					t.y = 1746;
					t.img = new Bitmap("lvl2\\tiles\\tile2.png");
					tilesXInc += 129;
					tilesLvl1.Add(t);
				}
				tilesXInc = 0;

				tile t1 = new tile();
				t1.x = 0;
				t1.y = 1250;
				t1.img = new Bitmap("lvl2\\tiles\\tile2.png");
				tilesLvl1.Add(t1);

				tilesXInc = 0;
				for (int i = 0; i < 4; i++)
				{
					tile t = new tile();
					t.x = 265 + tilesXInc;
					t.y = 1215;
					t.img = new Bitmap("lvl2\\tiles\\tile3.png");
					tilesXInc += 200;
					tilesLvl1.Add(t);
				}
				tilesXInc = 0;

				for (int i = 0; i < 8; i++)
				{
					tile t = new tile();
					t.x = 1175 + tilesXInc;
					t.y = 1188;
					t.img = new Bitmap("lvl2\\tiles\\tile2.png");
					tilesXInc += 160;
					tilesLvl1.Add(t);
				}
				tilesXInc = 0;

				tile t2 = new tile();
				t2.x = 0;
				t2.y = 1000;
				t2.img = new Bitmap("lvl2\\tiles\\tile4.png");
				tilesLvl1.Add(t2);

				for (int i = 0; i < 8; i++)
				{
					tile t = new tile();
					t.x = 1585 + tilesXInc;
					t.y = 2010;
					t.img = new Bitmap("lvl2\\tiles\\tile2.png");
					tilesXInc += 160;
					tilesLvl1.Add(t);
				}
				tilesXInc = 0;

				for (int i = 0; i < 8; i++)
				{
					tile t = new tile();
					t.x = 1585 + tilesXInc;
					t.y = 1630;
					t.img = new Bitmap("lvl2\\tiles\\tile2.png");
					tilesXInc += 160;
					tilesLvl1.Add(t);
				}
				tilesXInc = 0;

				for (int i = 0; i < 6; i++)
				{
					ladder t = new ladder();
					t.x = 805;
					t.y = 600 + tilesYInc - 4;
					t.img = new Bitmap("lvl2\\ladder.png");
					t.img.MakeTransparent(t.img.GetPixel(0, 0));
					tilesYInc += t.img.Height - 10;
					ladders.Add(t);
				}
				tilesYInc = 0;

				for (int i = 0; i < 8; i++)
				{
					tile t = new tile();
					t.x = 735 + tilesXInc;
					t.y = 628;
					t.img = new Bitmap("lvl2\\tiles\\tile5.png");
					tilesXInc += t.img.Width;
					tilesLvl1.Add(t);
				}
				tilesXInc = 0;
				//Monlisa SEcurity
				for (int i = 0; i < 1; i++)
				{
					security s = new security();
					s.x = 2295;
					s.y = 510;
					s.startX = 2295;
					s.targetX = 1200;
					s.state = 0;
					s.stateCt = 0;
					s.facingLeft = true;
					s.idleImgsSec = new List<Bitmap>();
					s.seenCt = 0;
					s.hmmStr = ". h";
					s.hmmCt = 0;
					for (int j = 0; j < 4; j++)
					{
						Bitmap b = new Bitmap("lvl2\\security\\idle\\idle" + (j + 1) + ".png");
						s.idleImgsSec.Add(b);
					}
					s.walkImgsSecLeft = new List<Bitmap>();
					for (int j = 0; j < 8; j++)
					{
						Bitmap b = new Bitmap("lvl2\\security\\walk\\walkB" + (j + 1) + ".png");
						s.walkImgsSecLeft.Add(b);
					}
					s.walkImgsSecRight = new List<Bitmap>();
					for (int j = 0; j < 8; j++)
					{
						Bitmap b = new Bitmap("lvl2\\security\\walk\\walk" + (j + 1) + ".png");
						s.walkImgsSecRight.Add(b);
					}
					securities.Add(s);
				}

				//tena Security
				for (int i = 0; i < 1; i++)
				{
					security s = new security();
					s.x = 1710;
					s.y = 1062;
					s.startX = 1710;
					s.targetX = 595;
					s.state = 0;
					s.stateCt = 0;
					s.facingLeft = true;
					s.idleImgsSec = new List<Bitmap>();
					s.seenCt = 0;
					s.hmmStr = ". h";
					s.hmmCt = 0;
					for (int j = 0; j < 4; j++)
					{
						Bitmap b = new Bitmap("lvl2\\security\\idle\\idle" + (j + 1) + ".png");
						s.idleImgsSec.Add(b);
					}
					s.walkImgsSecLeft = new List<Bitmap>();
					for (int j = 0; j < 8; j++)
					{
						Bitmap b = new Bitmap("lvl2\\security\\walk\\walkB" + (j + 1) + ".png");
						s.walkImgsSecLeft.Add(b);
					}
					s.walkImgsSecRight = new List<Bitmap>();
					for (int j = 0; j < 8; j++)
					{
						Bitmap b = new Bitmap("lvl2\\security\\walk\\walk" + (j + 1) + ".png");
						s.walkImgsSecRight.Add(b);
					}
					securities.Add(s);
				}
				showChapterScreen = true;
				chapterText = "Chapter 2: The Museum";
				chapterScreenTimer = 0;
			}
			else if (newLvl == 3)
			{
				bg bb = new bg();
				bb.img = new Bitmap("bg3.png");
				bb.X = 0;
				bb.Y = 0;
				bgs.Add(bb);

				showChapterScreen = true;
				chapterText = "Chapter 3: ???";
				chapterScreenTimer = 0;
			}
			else
			{
				showChapterScreen = true;
				chapterText = "TO BE CONTINUED";
				chapterScreenTimer = 0;
			}
		}
		int chapterColorVal = 0;
		private void GameTimer_Tick(object sender, EventArgs e)
		{
			if (!showMenu)
			{
				if (showChapterScreen)
				{
					chapterScreenTimer++;
					if (chapterColorVal + 20 <= 255)
					{
						chapterColorVal += 20;
					}
					if (chapterScreenTimer > 100)
					{
						showChapterScreen = false;
						chapterScreenTimer = 0;
						chapterColorVal = 0;
					}
					drawDb(CreateGraphics());
					return;
				}
				if (heros[0].isDead)
				{
					drawDb(CreateGraphics());
					return;
				}
				this.Text = "" + heros[0].yDuck + "|||" + heros[0].xDuck;
				heros[0].canPushRight = false;
				heros[0].canPushLeft = false;
				int box1 = -1;
				
				if ((lvl == 1 && tilesPLvl1.Count >= 4) || (lvl == 2 && tilesPLvl1.Count == 1))
				{

					tileP_chainCt++;
					tile movingTile = new tile();

					if (lvl == 1)
					{
						movingTile = tilesPLvl1[3];
					}
					if (lvl == 2)
					{
						movingTile = tilesPLvl1[0];
					}

					if (lvl == 1)
					{
						if (tileP_chainCt < 30)
						{
							movingTile.y -= 7;
							chains[0].y -= 7;
							chains[1].y -= 7;
							chains[2].y -= 7;

							if (duckOnSpecificTileP())
							{
								heros[0].yDuck -= 7;
							}
							if (rabbitOnSpecificTileP(movingTile))
							{
								heros[0].yRabbit -= 7;
							}
						}
						else if (tileP_chainCt > 30)
						{
							if (tileP_chainCt > 60)
							{
								tileP_chainCt = 0;
							}
							else
							{
								movingTile.y += 7;
								chains[0].y += 7;
								chains[1].y += 7;
								chains[2].y += 7;

								if (duckOnSpecificTileP())
								{
									heros[0].yDuck += 7;
								}
								if (rabbitOnSpecificTileP(movingTile))
								{
									heros[0].yRabbit += 7;
								}
							}
						}
					}

					if (lvl == 2 && heros[0].repairedElevator)
					{

						for (int i = 0; i < elevators.Count; i++)
						{
							elevator ev = elevators[i];
							
							int dy = ev.speed * ev.moveDir;
							ev.y += dy;
							ev.floor.y += dy;
							
							if (ev.y >= ev.bottomY) ev.moveDir = -1;
							if (ev.y <= ev.topY) ev.moveDir = 1;

							if (duckOnElevatorFloor())
								heros[0].yDuck += dy;

							if (rabbitOnElevatorFloor())
								heros[0].yRabbit += dy;
						}
					}

					for (int i = 0; i < boxes.Count; i++)
					{
						// PUSH RIGHT
						if (heros[0].xDuck + 70 >= boxes[i].x - 15 &&
							heros[0].xDuck + 70 <= boxes[i].x + 15)
						{
							if (heros[0].yDuck + 70 >= boxes[i].y + 20 &&
								heros[0].yDuck <= boxes[i].y + 100)
							{
								heros[0].canPushRight = true;
								box1 = i;
								break;
							}
						}

						// PUSH LEFT
						if (heros[0].xDuck >= boxes[i].x + 120 - 15 &&
							heros[0].xDuck <= boxes[i].x + 120 + 15)
						{
							if (heros[0].yDuck + 70 >= boxes[i].y + 20 &&
								heros[0].yDuck <= boxes[i].y + 100)
							{
								heros[0].canPushLeft = true;
								box1 = i;
								break;
							}
						}
					}
				}

                if (heros[0].isTransforming)
                {
                    heros[0].transformTimer--;
					heros[0].currenttransformFrame = (heros[0].currenttransformFrame + 1) % heros[0].transformImgs.Count;

                    if (heros[0].transformTimer <= 0)
                    {
                        heros[0].isTransforming = false;
                    }
                    
                }
                if (heros[0].isRat)
				{
					// ===== RABBIT MOVEMENT =====
					bool onSewer = rabbitOnSewer();

					if (heros[0].isClimbRabbitUp)
					{

						if (onSewer)
						{
							heros[0].currentClimbFrameRabbitLeft = 0;
							heros[0].currentIdelFrameRabbitRight = 0;
							heros[0].currentClimbFrameRabbitRight = (heros[0].currentClimbFrameRabbitRight + 1) % heros[0].climbImgsRabbitRight.Count;
							heros[0].yRabbit -= 15;
						}
					}
					else if (heros[0].isClimbRabbitDn)
					{
						if (onSewer)
						{
							heros[0].currentClimbFrameRabbitLeft = 0;
							heros[0].currentIdelFrameRabbitRight = 0;
							heros[0].currentClimbFrameRabbitRight = (heros[0].currentClimbFrameRabbitRight + 1) % heros[0].climbImgsRabbitRight.Count;
							heros[0].yRabbit += 15;
						}
					}
					else if (heros[0].isWalkRabbit)
					{
						if (heros[0].isRightRabbit)
						{
							checkWall(heros[0].isRat);
							heros[0].currentIdelFrameRabbitLeft = 0;
							heros[0].currentIdelFrameRabbitRight = 0;
							heros[0].currentWalkFrameRabbitRight = (heros[0].currentWalkFrameRabbitRight + 1) % heros[0].walkImgsRabbitRight.Count;
							int newRX = heros[0].xRabbit + 5;
							if (!rabbitCollidesWithWall(newRX))
								heros[0].xRabbit = newRX;
						}
						else if (heros[0].isLeftRabbit)
						{
							checkWall(heros[0].isRat);
							heros[0].currentRunFrameRabbitRight = 0;
							heros[0].currentIdelFrameRabbitLeft = 0;
							heros[0].currentWalkFrameRabbitLeft = (heros[0].currentWalkFrameRabbitLeft + 1) % heros[0].walkImgsRabbitLeft.Count;
							int newRX = heros[0].xRabbit - 5;
							if (!rabbitCollidesWithWall(newRX))
								heros[0].xRabbit = newRX;
						}
					}
					else if (heros[0].isRunRabbit)
					{
						if (heros[0].isRightRabbit)
						{
							checkWall(heros[0].isRat);

							heros[0].currentIdelFrameRabbitRight = 0;
							heros[0].currentRunFrameRabbitRight = (heros[0].currentRunFrameRabbitRight + 1) % heros[0].runImgsRabbitRight.Count;
							int newRX = heros[0].xRabbit + 15;
							if (!rabbitCollidesWithWall(newRX))
								heros[0].xRabbit = newRX;
						}
						else if (heros[0].isLeftRabbit)
						{
							checkWall(heros[0].isRat);

							heros[0].currentRunFrameRabbitRight = 0;
							heros[0].currentIdelFrameRabbitLeft = 0;
							heros[0].currentRunFrameRabbitLeft = (heros[0].currentRunFrameRabbitLeft + 1) % heros[0].runImgsRabbitLeft.Count;
							int newRX = heros[0].xRabbit - 15;
							if (!rabbitCollidesWithWall(newRX))
								heros[0].xRabbit = newRX;
						}
						else
						{
							heros[0].currentIdelFrameRabbitRight = (heros[0].currentIdelFrameRabbitRight + 1) % heros[0].idleImgsRabbitRight.Count;
						}
					}
					else if (heros[0].isRabbitMonalisa)
					{

					}
					else

					{
						heros[0].currentIdelFrameRabbitRight = (heros[0].currentIdelFrameRabbitRight + 1) % heros[0].idleImgsRabbitRight.Count;
					}
					applyGravityRabbit();
				}
				else
				{
					// ===== DUCK MOVEMENT =====
					bool onLadder = duckOnLadder();

					if (heros[0].isClimbDuckUp)
					{

						if (onLadder)
						{
							heros[0].currentIdleFrameDuckRight = 0;
							heros[0].currentClimbFramesDuck = (heros[0].currentClimbFramesDuck + 1) % heros[0].climbImgsDuck.Count;
							heros[0].yDuck -= 8;
						}

					}
					else if (heros[0].isClimbDuckUp)
					{
						if (onLadder)
						{
							heros[0].currentIdleFrameDuckRight = 0;
							heros[0].currentClimbFramesDuck = (heros[0].currentClimbFramesDuck + 1) % heros[0].climbImgsDuck.Count;
							heros[0].yDuck -= 8;
						}
					}
					else if (heros[0].isClimbDuckDn)
					{
						if (onLadder)
						{
							heros[0].currentIdleFrameDuckRight = 0;
							heros[0].currentClimbFramesDuck = (heros[0].currentClimbFramesDuck + 1) % heros[0].climbImgsDuck.Count;
							heros[0].yDuck += 8;
						}
					}
					else if (onLadder && (heros[0].isClimbDuckUp || heros[0].isClimbDuckDn))
					{
						heros[0].currentIdleFrameDuckRight = (heros[0].currentIdleFrameDuckRight + 1) % heros[0].idelImgsDuckRight.Count;
					}
					// ---- JUMP ----
					else if (heros[0].isJumpDuckUp || heros[0].isJumpDuckRight || heros[0].isJumpDuckLeft)
					{

						if (heros[0].isJumpDuckRight)
						{
							checkWall(heros[0].isRat);
							heros[0].currentJumpFrameDuckRight = (heros[0].currentJumpFrameDuckRight + 1) % heros[0].jumpImgsDuckRight.Count;
							int newX = heros[0].xDuck + 5;
							if (!duckCollidesWithWall(newX))
							{
								heros[0].xDuck = newX;
							}
						}
						else if (heros[0].isJumpDuckLeft)
						{
							checkWall(heros[0].isRat);
							heros[0].currentJumpFrameDuckLeft = (heros[0].currentJumpFrameDuckLeft + 1) % heros[0].jumpImgsDuckLeft.Count;
							int newX = heros[0].xDuck - 5;
							if (!duckCollidesWithWall(newX))
							{
								heros[0].xDuck = newX;
							}
						}
						else
						{
							heros[0].currentJumpFrameDuckRight = (heros[0].currentJumpFrameDuckRight + 1) % heros[0].jumpImgsDuckRight.Count;
						}

						// move vertically by jumpVelocity
						int oldFeetY = heros[0].yDuck + 70;
						heros[0].yDuck += heros[0].jumpVelocity;
						heros[0].jumpVelocity += 2;
						int newFeetY = heros[0].yDuck + 70;
						// check landing
						if (heros[0].jumpVelocity > 0)
						{
							// only check  when falling
							if (snapDuckToGround(oldFeetY, newFeetY))
							{
								heros[0].isJumpDuckUp = false;
								heros[0].isJumpDuckRight = false;
								heros[0].isJumpDuckLeft = false;
								heros[0].jumpVelocity = 0;
								heros[0].fallingFrameCount = 0;
							}
						}
					}
					// ---- WALK / RUN / ATTACK / IDLE ----
					else if (heros[0].isWalkDuck)
					{
						if (heros[0].isRightDuck)
						{
							heros[0].currentIdleFrameDuckLeft = 0;

							if (heros[0].canPushRight)
							{
								checkWall(heros[0].isRat);
								heros[0].isPushDuckRight = true;
								heros[0].isPushDuckLeft = false;
								heros[0].currentWalkFramesDuckRight = 0;

								int newDuckX = heros[0].xDuck + 3;
								int newBoxX = boxes[box1].x + 3;

								if (!duckCollidesWithWall(newDuckX) && !boxCollidesWithWall(newBoxX, box1))
								{
									heros[0].xDuck = newDuckX;
									boxes[box1].x = newBoxX;
								}
							}
							else
							{
								checkWall(heros[0].isRat);
								heros[0].isPushDuckRight = false;
								heros[0].currentWalkFramesDuckRight =
									(heros[0].currentWalkFramesDuckRight + 1) % heros[0].walkImgsDuckRight.Count;

								int newX = heros[0].xDuck + 5;
								if (!duckCollidesWithWall(newX))
								{
									heros[0].xDuck = newX;
								}
							}
						}
						else if (heros[0].isLeftDuck)
						{
							heros[0].currentIdleFrameDuckRight = 0;

							if (heros[0].canPushLeft)
							{
								checkWall(heros[0].isRat);
								heros[0].isPushDuckLeft = true;
								heros[0].isPushDuckRight = false;
								heros[0].currentWalkFramesDuckLeft = 0;

								int newDuckX = heros[0].xDuck - 3;
								int newBoxX = boxes[box1].x - 3;

								if (!duckCollidesWithWall(newDuckX) && !boxCollidesWithWall(newBoxX, box1))
								{
									heros[0].xDuck = newDuckX;
									boxes[box1].x = newBoxX;
								}
							}
							else
							{
								checkWall(heros[0].isRat);
								heros[0].isPushDuckLeft = false;
								heros[0].currentWalkFramesDuckLeft =
									(heros[0].currentWalkFramesDuckLeft + 1) % heros[0].walkImgsDuckLeft.Count;

								int newX = heros[0].xDuck - 5;
								if (!duckCollidesWithWall(newX))
								{
									heros[0].xDuck = newX;
								}
							}
						}
					}
					else if (heros[0].isRunDuck)
					{
						if (heros[0].isRightDuck)
						{
							checkWall(heros[0].isRat);
							heros[0].currentIdleFrameDuckRight = 0;
							heros[0].currentRunFrameDuckRight = (heros[0].currentRunFrameDuckRight + 1) % heros[0].runImgsDuckRight.Count;
							int newX = heros[0].xDuck + 15;
							if (!duckCollidesWithWall(newX))
							{
								heros[0].xDuck = newX;
							}
						}
						else if (heros[0].isLeftDuck)
						{
							checkWall(heros[0].isRat);
							heros[0].currentIdleFrameDuckLeft = 0;
							heros[0].currentRunFrameDuckLeft = (heros[0].currentRunFrameDuckLeft + 1) % heros[0].runImgsDuckLeft.Count;
							int newX = heros[0].xDuck - 15;
							if (!duckCollidesWithWall(newX))
							{
								heros[0].xDuck = newX;
							}
						}
						else
						{
							heros[0].currentIdleFrameDuckRight = (heros[0].currentIdleFrameDuckRight + 1) % heros[0].idelImgsDuckRight.Count;
						}
					}
					else if (heros[0].isAttkDuck)
					{
						heros[0].currentIdleFrameDuckRight = 0;
						heros[0].currentAttkFrameDuckRight++;
						if (heros[0].currentAttkFrameDuckRight >= heros[0].attkImgsDuckRight.Count)
						{
							heros[0].currentAttkFrameDuckRight = 0;
							heros[0].isAttkDuck = false;
						}
					}
					else
					{
						heros[0].currentIdleFrameDuckRight = (heros[0].currentIdleFrameDuckRight + 1) % heros[0].idelImgsDuckRight.Count;
					}

					applyGravityDuck();
				}

				if (!heros[0].isRat)
				{
					xStart = heros[0].xDuck - this.ClientSize.Width / 2;
					yStart = heros[0].yDuck - this.ClientSize.Height / 2;
				}
				else
				{
					xStart = heros[0].xRabbit - this.ClientSize.Width / 2;
					yStart = (heros[0].yRabbit + 30) - this.ClientSize.Height / 2;
				}
				if (xStart < 0)
				{
					xStart = 0;
				}
				if (yStart < 0)
				{
					yStart = 0;
				}
				int maxX = bgs[0].img.Width - this.ClientSize.Width;
				int maxY = bgs[0].img.Height - this.ClientSize.Height;
				/*
				   if (lvl == 2)
			   {
				   maxX += 374;
				   maxY += 328;
			   }
				*/
				if (xStart > maxX)
				{
					xStart = maxX;
				}
				if (yStart > maxY)
				{
					yStart = maxY;
				}


				if (heros[0].chickHoldCt > 0)
				{
					heros[0].chickHoldCt--;
                    if (heros[0].chickHoldCt == 0)
                    {
                        qCt = 0;
                    }
                }



				// key pickup
				for (int i = 0; i < keysLvl1.Count; i++)
				{
					key ptrv = keysLvl1[i];
					ptrv.currentKeyFrame = (ptrv.currentKeyFrame + 1) % ptrv.imgs.Count;
				}

				for (int i = keysLvl1.Count - 1; i >= 0; i--)
				{
					int heroX = heros[0].isRat ? heros[0].xRabbit : heros[0].xDuck;
					int heroY = heros[0].isRat ? heros[0].yRabbit : heros[0].yDuck;
					int heroW = heros[0].isRat ? 100 : 70;
					int heroH = heros[0].isRat ? 100 : 70;

					if (heroX + heroW >= keysLvl1[i].x && heroX <= keysLvl1[i].x + 30)
					{
						if (heroY + heroH >= keysLvl1[i].y && heroY <= keysLvl1[i].y + 30)
						{
							keysLvl1.RemoveAt(i);
							heros[0].hasKey = true;
						}
					}
				}
				//chick
				for (int i = 0; i < chicks.Count; i++)
				{
					chick c = chicks[i];
					c.currentChickFrame = (c.currentChickFrame + 1) % c.imgs.Count;
				}
				for (int i = chicks.Count - 1; i >= 0; i--)
				{
					int heroX = 0;
					int heroY = 0;
					int heroW = 0;
					int heroH = 0;

					if (heros[0].isRat)
					{
						heroX = heros[0].xRabbit;
						heroY = heros[0].yRabbit;
						heroW = 100;
						heroH = 100;
					}
					else
					{
						heroX = heros[0].xDuck;
						heroY = heros[0].yDuck;
						heroW = 70;
						heroH = 70;
					}

					if (heroX + heroW >= chicks[i].x && heroX <= chicks[i].x + chicks[i].imgs[0].Width)
					{
						if (heroY + heroH >= chicks[i].y && heroY <= chicks[i].y + chicks[i].imgs[0].Height)
						{
							if (lvl == 1 && heros[0].distract == null)
							{
								heros[0].distract = chicks[i]; // store it
							}
							chicks.RemoveAt(i);
							score += 10;
						}
					}
				}
				// water toggle
				waterOnCt++;
				if (waterOnCt < 20)
				{
					for (int i = 0; i < waters.Count; i++)
					{
						waters[i].on = 1;
						waters[i].currentWaterFrame = (waters[i].currentWaterFrame + 1) % waters[i].imgs.Count;
					}
				}
				else if (waterOnCt < 40)
				{
					for (int i = 0; i < waters.Count; i++)
						waters[i].on = 0;
				}
				else
				{
					waterOnCt = 0;
				}

				for (int i = 0; i < waters.Count; i++)
				{
					if (waters[i].on == 1)
					{
						int heroX = 0;
						int heroY = 0;
						int heroW = 0;
						int heroH = 0;

						if (heros[0].isRat)
						{
							heroX = heros[0].xRabbit;
							heroY = heros[0].yRabbit;
							heroW = 100;
							heroH = 100;
						}
						else
						{
							heroX = heros[0].xDuck;
							heroY = heros[0].yDuck;
							heroW = 70;
							heroH = 70;
						}
						int waterRight = waters[i].x + (waters[i].imgs[0].Width / 2) + 20;
						int waterLeft = waters[i].x + (waters[i].imgs[0].Width / 2) - 20;
						int waterBottom = waters[i].y + waters[i].imgs[0].Height - 190;

						if (heroX + heroW >= waterLeft && heroX <= waterRight)
						{
							if (heroY + heroH >= waters[i].y && heroY <= waterBottom)
							{
								heros[0].isDead = true;
							}
						}
					}
				}
				if (lvl == 1 && frogs.Count > 0)
				{
					for (int i = 0; i < frogs.Count; i++)
					{
						frogs[i].currentIdleFrameFrog = (frogs[0].currentIdleFrameFrog + 1) % frogs[0].IdleImgsFrog.Count;
					}
				}
				// ===== SECURITY UPDATE =====
				if (lvl == 2)
				{	
					//Monalisa Security
					for (int i = 0; i < 1; i++)
					{
						int heroX = 0;
						int heroY = 0;

						if (heros[0].isRat)
						{
							heroX = heros[0].xRabbit;
							heroY = heros[0].yRabbit;
						}
						else
						{
							heroX = heros[0].xDuck;
							heroY = heros[0].yDuck;
						}

						security sec = securities[i];

						bool seesHero = false;
						if (heroX >= sec.x - 200 && heroX <= sec.x + 200)
						{
							if (heroY >= sec.y - 100 && heroY <= sec.y + 140)
							{
								if (!heros[0].isRabbitMonalisa)
								{
									seesHero = true;
								}
							}
						}

						bool paintingGone = false;
						if (paintings.Count == 0)
						{
							paintingGone = true;
						}

						bool isMimicking = heros[0].isRabbitMonalisa;

						if (sec.state == 0)
						{
							sec.currentIdleFrameSec =
								(sec.currentIdleFrameSec + 1) % sec.idleImgsSec.Count;

							sec.stateCt++;

							if (sec.stateCt >= 100)
							{
								sec.stateCt = 0;
								sec.state = 1;
								sec.facingLeft = true;
							}
						}
						else if (sec.state == 1)
						{
							sec.currentWalkFrameSecLeft =
								(sec.currentWalkFrameSecLeft + 1) % sec.walkImgsSecLeft.Count;

							sec.x -= 10;

							if (seesHero)
							{
								sec.stateCt = 0;
								sec.hmmStr = ". h";
								sec.hmmCt = 0;
								sec.state = 4;
								sec.facingLeft = false;
							}
							else if (sec.x <= sec.targetX + 30)
							{
								sec.stateCt = 0;

								if (paintingGone && !heros[0].isRabbitMonalisa)
								{
									sec.paintingWasStolen = true;
									sec.state = 4;
									sec.facingLeft = false;
								}
								else
								{
									sec.state = 2;
								}
							}
						}
						else if (sec.state == 2)
						{
							sec.currentIdleFrameSec =
								(sec.currentIdleFrameSec + 1) % sec.idleImgsSec.Count;

							sec.stateCt++;

							if (paintingGone && !isMimicking)
							{
								sec.paintingWasStolen = true;
								sec.stateCt = 0;
								sec.state = 4;
								sec.facingLeft = false;
							}
							else if (isMimicking)
							{
								sec.hmmCt++;

								if (sec.hmmCt >= 5)
								{
									sec.hmmCt = 0;
									sec.hmmStr = sec.hmmStr + "m";
								}

								if (sec.stateCt >= 200)
								{
									sec.stateCt = 0;
									sec.hmmCt = 0;
									sec.hmmStr = ". h";
									sec.state = 3;
									sec.facingLeft = false;
								}
							}
							else
							{
								if (sec.stateCt >= 180)
								{
									sec.stateCt = 0;
									sec.state = 3;
									sec.facingLeft = false;
								}
							}
						}
						else if (sec.state == 3)
						{
							sec.currentWalkFrameSecRight =
								(sec.currentWalkFrameSecRight + 1) % sec.walkImgsSecRight.Count;

							sec.x += 10;

							if (seesHero)
							{
								sec.stateCt = 0;
								sec.hmmStr = ". h";
								sec.hmmCt = 0;
								sec.state = 4;
								sec.facingLeft = false;
							}
							else if (sec.x >= sec.startX)
							{
								sec.x = sec.startX;
								sec.stateCt = 0;
								sec.state = 0;
								sec.facingLeft = true;
                                
                            }
						}
						else if (sec.state == 4)
						{
							sec.currentWalkFrameSecRight =
								(sec.currentWalkFrameSecRight + 1) % sec.walkImgsSecRight.Count;

							sec.x += 15;

							if (sec.x >= sec.startX + 20)
							{
								sec.x = sec.startX + 20;
								sec.stateCt = 0;

								if (sec.paintingWasStolen)
								{
									sec.state = 5;
								}
								else
								{
									sec.state = 0;
								}
							}
						}
						else if (sec.state == 5)
						{
							sec.currentIdleFrameSec =
								(sec.currentIdleFrameSec + 1) % sec.idleImgsSec.Count;
						}
					}

					// Tena Security
					// Tena Security
					for (int i = 0; i < 1; i++)
					{
						security sec = securities[1];

						if (sec.state != 5)
						{
							// chick expired — remove it
							if (heros[0].distract != null && heros[0].chickHoldCt == 1)
							{
								heros[0].distract = null;
							}

							if (sec.state == 0)
							{
								// just idle, do nothing until chick is placed
								sec.currentIdleFrameSec =
									(sec.currentIdleFrameSec + 1) % sec.idleImgsSec.Count;

								if (item.Count == 0 && !heros[0].isRabbitTut)
								{
									sec.state = 5;
								}
							}
							else if (sec.state == 6)
							{
								// PHASE 0: x-- until x <= 1165
								if (sec.x > 1165 && !sec.reached)
								{
									sec.currentWalkFrameSecLeft =
										(sec.currentWalkFrameSecLeft + 1) % sec.walkImgsSecLeft.Count;
									sec.facingLeft = true;
									sec.x -= 10;
								}
								// PHASE 0b: y++ until y >= 1150 (step down to lower floor)
								else if (sec.y < 1100 && !sec.reached)
								{
									sec.currentWalkFrameSecLeft =
										(sec.currentWalkFrameSecLeft + 1) % sec.walkImgsSecLeft.Count;
									sec.facingLeft = true;
									sec.y += 5;
									sec.x -= 10;
								}
								// PHASE 1: x-- until x <= 805
								else if (sec.x > 800 && !sec.reached)
								{
									sec.currentWalkFrameSecLeft =
										(sec.currentWalkFrameSecLeft + 1) % sec.walkImgsSecLeft.Count;
									sec.facingLeft = true;
									sec.x -= 10;
								}
								// PHASE 2: y++ until y >= 1680 (climb down ladder)
								else if (sec.y < 1620 && !sec.reached)
								{
									sec.currentWalkFrameSecLeft =
										(sec.currentWalkFrameSecLeft + 1) % sec.walkImgsSecLeft.Count;
									sec.y += 8;
								}
								// PHASE 3: x-- until x <= 550
								else if (sec.x > 550 && !sec.reached)
								{
									sec.currentWalkFrameSecLeft =
										(sec.currentWalkFrameSecLeft + 1) % sec.walkImgsSecLeft.Count;
									sec.facingLeft = true;
									sec.x -= 8;
								}
								// PHASE 4: wait at chick
								else if (sec.stateCt < 180)
								{
									sec.reached = true;
									sec.currentIdleFrameSec =
										(sec.currentIdleFrameSec + 1) % sec.idleImgsSec.Count;
									sec.stateCt++;
								}
								// RETURN PHASE 5: x++ until x >= 805
								else if (sec.x < 800)
								{
									sec.currentWalkFrameSecRight =
										(sec.currentWalkFrameSecRight + 1) % sec.walkImgsSecRight.Count;
									sec.facingLeft = false;
									sec.x += 8;
									heros[0].distract = null;
								}
								// RETURN PHASE 6: y-- until y <= 1150 (climb up ladder)
								else if (sec.y > 1100)
								{
									sec.currentWalkFrameSecRight =
										(sec.currentWalkFrameSecRight + 1) % sec.walkImgsSecRight.Count;
									sec.y -= 8;
								}

								// RETURN PHASE 7: x++ until x >= 1165
								else if (sec.x < 1165)
								{
									sec.currentWalkFrameSecRight =
										(sec.currentWalkFrameSecRight + 1) % sec.walkImgsSecRight.Count;
									sec.facingLeft = false;
									sec.x += 10;
								}
								else if (sec.y > 1062)
								{
									sec.currentWalkFrameSecRight =
										(sec.currentWalkFrameSecRight + 1) % sec.walkImgsSecRight.Count;
									sec.facingLeft = false;
									sec.y -= 5;
									sec.x += 10;
								}
								// RETURN PHASE 7b: y++ until y >= 1062 (step back up to original floor)

								// RETURN PHASE 8: x++ until x >= startX
								else if (sec.x < sec.startX)
								{
									sec.currentWalkFrameSecRight =
										(sec.currentWalkFrameSecRight + 1) % sec.walkImgsSecRight.Count;
									sec.facingLeft = false;
									sec.x += 10;
								}
								else
								{
									// back home
									sec.x = sec.startX;
									sec.stateCt = 0;
									sec.state = 0;
									sec.facingLeft = true;

									if (sec.reached)
									{
										chick dc = new chick();
										dc.imgs = new List<Bitmap>();
										for (int j = 0; j < 16; j++)
										{
											dc.imgs.Add(chickImgs[j]);
										}
										dc.x = 0;
										dc.y = 0;
										heros[0].distract = dc;
										heros[0].chickHoldCt = 0;
										sec.reached = false;
									}

								}
							}
						}
					}

				}
				// frog update
				/*
				for (int i = 0; i < frogs.Count; i++)
				{
					frog f = frogs[i];
					int heroX = heros[0].xDuck;
					int heroY = heros[0].yDuck;
					if (heros[0].isRat)
					{
						heroX = heros[0].xRabbit;
						heroY = heros[0].yRabbit;
					}

					if (f.isIdle)
					{
						f.currentIdleFrameFrog = (f.currentIdleFrameFrog + 1) % f.IdleImgsFrog.Count;
						if (heroX > f.x - 300)
						{
							if (heroX < f.x + 300)
							{
								f.isIdle = false;
								f.isHopToHero = true;
							}
						}
					}
					else if (f.isHopToHero)
					{
						if (f.x - heroX < 80 && f.x - heroX > -80)
						{
							f.isHopToHero = false;
							f.isAttacking = true;
							f.currentAttkFrameFrog = 0;
						}
						else if (f.x - heroX > 300 || f.x - heroX < -300)
						{
							f.isHopToHero = false;
							f.isHopBack = true;
						}
						else
						{
							f.currentHopFrameFrogLeft = (f.currentHopFrameFrogLeft + 1) % f.hopImgsFrogLeft.Count;
							if (heroX < f.x)
								f.x -= 3;
							else
								f.x += 3;
						}
					}
					else if (f.isAttacking)
					{
						f.currentAttkFrameFrog++;
						if (f.currentAttkFrameFrog >= f.attkImgsFrog.Count)
						{
							f.currentAttkFrameFrog = 0;
							if (f.x - heroX > 80 || f.x - heroX < -80)
							{
								f.isAttacking = false;
								f.isHopBack = true;
							}
						}
					}
					else if (f.isHopBack)
					{
						f.currentHopFrameFrogRigt = (f.currentHopFrameFrogRigt + 1) % f.hopImgsFrogRight.Count;
						if (f.x < f.startX)
						{
							f.x += 3;
						}
						else
						{
							f.isHopBack = false;
							f.isIdle = true;
							f.currentIdleFrameFrog = 0;
						}
					}
				}
				*/

				drawDb(CreateGraphics());
			}
			else
			{
				if (!heros[0].isDead)
				{
					button1.Visible = true;
					button1.Enabled = true;
				}
				button2.Visible = true;
				button2.Enabled = true;

				button3.Visible = true;
				button3.Enabled = true;

				if (!heros[0].isDead)
				{
					button4.Visible = true;
					button4.Enabled = true;
				}
			}
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				if (!showMenu)
				{
					showMenu = true;
				}
				else
				{
					showMenu = false;

					button1.Visible = false;
					button1.Enabled = false;

					button2.Visible = false;
					button2.Enabled = false;

					button3.Visible = false;
					button3.Enabled = false;


					button4.Visible = false;
					button4.Enabled = false;

				}
			}

			if (!heros[0].isTransforming)
			{
				if (e.KeyCode == Keys.G)
				{
					int heroX = 0;
					int heroY = 0;

					if (heros[0].isRat)
					{
						heroX = heros[0].xRabbit;
						heroY = heros[0].yRabbit;
					}
					else
					{
						heroX = heros[0].xDuck;
						heroY = heros[0].yDuck;
					}

					for (int i = 0; i < 1; i++)
					{
						if (heroX + 70 >= frogs[i].x - 100 && heroX <= frogs[i].x + 100)
						{
							if (heroY + 70 >= frogs[i].y - 100 && heroY <= frogs[i].y + 100)
							{
								if (showFrogDialog)
								{
									showFrogDialog = false;
								}
								else
								{
									showFrogDialog = true;
								}
								break;
							}
						}
					}
					for (int i = 0; i < 1; i++)
					{
						if (heroX + 70 >= frogs[1].x - 100 && heroX <= frogs[1].x + 100)
						{
							if (heroY + 70 >= frogs[1].y - 100 && heroY <= frogs[1].y + 100)
							{
								if (showFrogDialog2)
								{
									showFrogDialog2 = false;
								}
								else
								{
									showFrogDialog2 = true;
								}
								showFrogDialog = false;
								break;
							}
						}
					}

				}
				if (e.KeyCode == Keys.R)
				{
					if (heros[0].isDead)
					{
						// reset position
						score = 0;
						heros[0].xDuck = 650;
						heros[0].yDuck = 950;
						heros[0].xRabbit = 650;
						heros[0].yRabbit = 920;
						heros[0].isRat = false;
						heros[0].isDead = false;

						// reset all movement states
						heros[0].isWalkDuck = false;
						heros[0].isRunDuck = false;
						heros[0].isRightDuck = false;
						heros[0].isLeftDuck = false;
						heros[0].isJumpDuckUp = false;
						heros[0].isJumpDuckRight = false;
						heros[0].isJumpDuckLeft = false;
						heros[0].isClimbDuckUp = false;
						heros[0].isClimbDuckDn = false;
						heros[0].jumpVelocity = 0;
						heros[0].fallingFrameCount = 0;
						heros[0].hasKey = false;

						// reset key
						keysLvl1.Clear();
						key g = new key();
						g.x = 1725;
						g.y = 1360;
						g.imgs = new List<Bitmap>();
						for (int i = 0; i < 24; i++)
						{
							Bitmap b = new Bitmap("lvl1\\key\\key" + (i + 1) + ".png");
							g.imgs.Add(b);
						}
						keysLvl1.Add(g);

						// reset door
						doors.Clear();
						door d = new door();
						d.img = new Bitmap("lvl1\\door.png");
						d.img.MakeTransparent();
						d.x = 1935;
						d.y = 130;
						doors.Add(d);

						door d2 = new door();
						d2.x = 650;
						d2.y = 805;
						d2.img = new Bitmap("lvl1\\startDoor.png");
						doors.Add(d2);

					}


				}
				if (e.KeyCode == Keys.E)
				{
					// paintings pickup
					for (int i = paintings.Count - 1; i >= 0; i--)
					{
						int heroX = heros[0].isRat ? heros[0].xRabbit : heros[0].xDuck;
						int heroY = heros[0].isRat ? heros[0].yRabbit : heros[0].yDuck;
						int heroW = heros[0].isRat ? 100 : 70;
						int heroH = heros[0].isRat ? 100 : 70;

						if (heroX + heroW >= paintings[i].x && heroX <= paintings[i].x + paintings[i].width)
						{
							if (heroY + heroH >= paintings[i].y && heroY <= paintings[i].y + paintings[i].height)
							{
								paintings.RemoveAt(i);
								score += 10;
							}
						}
					}

					// items pickup
					for (int i = item.Count - 1; i >= 0; i--)
					{
						int heroX = heros[0].isRat ? heros[0].xRabbit : heros[0].xDuck;
						int heroY = heros[0].isRat ? heros[0].yRabbit : heros[0].yDuck;
						int heroW = heros[0].isRat ? 100 : 70;
						int heroH = heros[0].isRat ? 100 : 70;

						if (heroX + heroW >= item[i].x && heroX <= item[i].x + item[i].width)
						{
							if (heroY + heroH >= item[i].y && heroY <= item[i].y + item[i].height)
							{
								Form4 f4 = new Form4();
								f4.ShowDialog();

								if (f4.maskReleased)
								{
									heros[0].stoleTut = true;
									item.RemoveAt(i);
									score += 50;
								}
							}
						}
					}
					if (heros[0].hasKey)
					{
						int heroX = heros[0].isRat ? heros[0].xRabbit : heros[0].xDuck;
						int heroY = heros[0].isRat ? heros[0].yRabbit : heros[0].yDuck;
						int heroW = heros[0].isRat ? 100 : 70;
						int heroH = heros[0].isRat ? 100 : 70;

						for (int i = doors.Count - 1; i >= 0; i--)
						{
							if (heroX + heroW >= doors[i].x && heroX <= doors[i].x + doors[i].img.Width)
							{
								if (heroY + heroH >= doors[i].y && heroY <= doors[i].y + doors[i].img.Height + 30)
								{
									doors.RemoveAt(i);
									heros[0].hasKey = false;


									if (i == 0)
									{
										lvl++;
										currentSaveId = -1;
										LoadLevel(lvl);
									}
								}
							}
						}
					}

					//elevator code
					if (lvl == 2 && !heros[0].repairedElevator)
					{
						int heroX = 0;
						int heroY = 0;
						if (heros[0].isRat)
						{
							heroX = heros[0].xRabbit;
							heroY = heros[0].yRabbit;
						}
						else
						{
							heroX = heros[0].xDuck;
							heroY = heros[0].yDuck;
						}
						if (heroX >= 1050 && heroX <= 1110 && heroY >= 2060 && heroY <= 2120)
						{
							Form3 f3 = new Form3();
							f3.Location = new Point(this.Location.X + 900, this.Height - 500);
							f3.ShowDialog();

							if (f3.Complete == true)
							{
								heros[0].repairedElevator = true;
								elevators[0].moveDir = 1;
								elevators[0].speed = 2;
							}
						}
					}
				}
				if (e.KeyCode == Keys.T && !heros[0].isTransforming)
				{
					heros[0].isTransforming = true;
					heros[0].transformTimer = 10;
					if (heros[0].isRat)
					{
						heros[0].xDuck = heros[0].xRabbit;
						heros[0].yDuck = heros[0].yRabbit + 30;
						heros[0].isRat = false;

						heros[0].isRunRabbit = false;
						heros[0].isWalkRabbit = false;
						heros[0].isLeftRabbit = false;
						heros[0].isRightRabbit = false;
						heros[0].isClimbRabbitUp = false;
						heros[0].isClimbRabbitDn = false;
						heros[0].isIdelRabbit = true;


						heros[0].isWalkDuck = false;
						heros[0].isRunDuck = false;
						heros[0].isRightDuck = false;
						heros[0].isLeftDuck = false;
						heros[0].isJumpDuckUp = false;
						heros[0].isJumpDuckRight = false;
						heros[0].isJumpDuckLeft = false;
						heros[0].isClimbDuckUp = false;
						heros[0].isClimbDuckDn = false;
						heros[0].jumpVelocity = 0;
						heros[0].fallingFrameCount = 0;
						heros[0].isIdelDuck = true;
					}
					else
					{
						heros[0].xRabbit = heros[0].xDuck;
						heros[0].yRabbit = heros[0].yDuck - 30;
						heros[0].isRat = true;


						heros[0].isRunDuck = false;
						heros[0].isWalkDuck = false;
						heros[0].isLeftDuck = false;
						heros[0].isRightDuck = false;
						heros[0].isJumpDuckUp = false;
						heros[0].isJumpDuckRight = false;
						heros[0].isJumpDuckLeft = false;
						heros[0].isClimbDuckUp = false;
						heros[0].isClimbDuckDn = false;
						heros[0].jumpVelocity = 0;
						heros[0].fallingFrameCount = 0;
						heros[0].isIdelDuck = true;


						heros[0].isRunRabbit = false;
						heros[0].isWalkRabbit = false;
						heros[0].isLeftRabbit = false;
						heros[0].isRightRabbit = false;
						heros[0].isClimbRabbitUp = false;
						heros[0].isClimbRabbitDn = false;
						heros[0].isIdelRabbit = true;
					}
				}
				if (e.KeyCode == Keys.J)
				{
					if (heros[0].isRat && paintings.Count == 0)
					{
						if (heros[0].xRabbit > 1200 && heros[0].xRabbit < 1200 + 160
							&&
							heros[0].yRabbit < 600)
						{
							heros[0].isRabbitMonalisa = true;
						}
					}

					if (heros[0].isRat && item.Count == 0)
					{
						if (heros[0].xRabbit > 1859 && heros[0].xRabbit < 1859 + 180
							&&
							heros[0].yRabbit > 920 && heros[0].yRabbit < 920 + 200)
						{
							heros[0].isRabbitTut = true;
						}
					}
				}
				if (e.KeyCode == Keys.Q)
				{
					if (lvl == 2)
					{
						if (heros[0].distract != null)
						{
							if (qCt == 0 && heros[0].chickHoldCt == 0)
							{

								if (heros[0].xDuck >= 500 && heros[0].xDuck <= 700 && heros[0].yDuck >= 1580 && heros[0].yDuck <= 1750)
								{
									heros[0].distract.x = heros[0].xDuck;
									heros[0].distract.y = heros[0].yDuck + heros[0].idleImgsRabbitLeft[0].Height;
									heros[0].chickHoldCt = 400;
									qCt = 1;
								}
							}
							else if (qCt == 1)
							{

								if (securities.Count > 1 && securities[1].state == 0)
								{
									securities[1].state = 6;
									securities[1].stateCt = 0;
								}
								qCt = 0;
							}
						}
						else if (heros[0].distract == null && qCt == 0)
						{
							chick dc = new chick();
							dc.imgs = new List<Bitmap>();
							heros[0].chickHoldCt = 0;
							for (int j = 0; j < 16; j++)
							{
								dc.imgs.Add(chickImgs[j]);
							}
							dc.x = 0;
							dc.y = 0;
							heros[0].distract = dc;
							heros[0].chickHoldCt = 0;
						}

					}
				}

				if (e.KeyCode == Keys.Right)
				{
					if (heros[0].isRat)
					{

						heros[0].isRightRabbit = true;
						heros[0].isWalkRabbit = true;
						heros[0].isIdelRabbit = false;
						if (heros[0].isRunRabbit)
						{
							heros[0].isRunRabbit = true;
							heros[0].isWalkRabbit = false;
							heros[0].isIdelRabbit = false;
						}
					}
					if (!heros[0].isRat && !heros[0].isJumpDuckRight && !heros[0].isJumpDuckLeft && !heros[0].isJumpDuckUp)
					{

						if (!duckOnLadder() || (!heros[0].isClimbDuckUp && !heros[0].isClimbDuckDn))
						{
							heros[0].isRightDuck = true;
							heros[0].isWalkDuck = true;
							heros[0].isIdelDuck = false;
							if (heros[0].isRunDuck)
							{
								heros[0].isRunDuck = true;
								heros[0].isWalkDuck = false;
							}
						}
					}
				}
				if (e.KeyCode == Keys.Left)
				{
					if (heros[0].isRat)
					{

						heros[0].isLeftRabbit = true;
						heros[0].isWalkRabbit = true;
						heros[0].isIdelRabbit = false;
						if (heros[0].isRunRabbit)
						{
							heros[0].isRunRabbit = true;
							heros[0].isWalkRabbit = false;
							heros[0].isIdelRabbit = false;
						}
					}
					if (!heros[0].isRat && !heros[0].isJumpDuckRight && !heros[0].isJumpDuckLeft && !heros[0].isJumpDuckUp)
					{

						if (!duckOnLadder() || (!heros[0].isClimbDuckUp && !heros[0].isClimbDuckDn))
						{
							heros[0].isLeftDuck = true;
							heros[0].isWalkDuck = true;
							heros[0].isIdelDuck = false;
							if (heros[0].isRunDuck)
							{
								heros[0].isRunDuck = true;
								heros[0].isWalkDuck = false;
							}
						}
					}
				}
				if (e.KeyCode == Keys.Space)
				{
					if (heros[0].isRat)
					{
						heros[0].isRunRabbit = true;
						heros[0].isIdelRabbit = false;
					}
					else
					{
						heros[0].isRunDuck = true;
						heros[0].isIdelDuck = false;
					}
				}
				if (e.KeyCode == Keys.F)
				{
					if (!heros[0].isRat && !heros[0].isAttkDuck)
					{
						heros[0].isAttkDuck = true;
						heros[0].currentAttkFrameDuckRight = 0;
					}
				}
				if (e.KeyCode == Keys.Up)
				{
					if (!heros[0].isRat && !heros[0].isJumpDuckRight && !heros[0].isJumpDuckLeft && !heros[0].isJumpDuckUp)
					{
						// cannot jump while on ladder climbing
						if (!heros[0].isClimbDuckUp && !heros[0].isClimbDuckDn)
						{
							if (heros[0].isRightDuck)
							{
								heros[0].isJumpDuckRight = true;
								heros[0].jumpVelocity = -18;
							}
							else if (heros[0].isLeftDuck)
							{
								heros[0].isJumpDuckLeft = true;
								heros[0].jumpVelocity = -18;
							}
							else
							{
								heros[0].isJumpDuckUp = true;
								heros[0].jumpVelocity = -18;
							}
						}
					}
				}
				if (e.KeyCode == Keys.Down)
				{
					if (!heros[0].isRat)
					{

						if (duckOnLadder())
						{
							heros[0].isIdelDuck = false;
							heros[0].isClimbDuckDn = true;
							heros[0].isClimbDuckUp = false;
						}
					}
					else
					{

						if (rabbitOnSewer())
						{
							heros[0].isIdelRabbit = false;
							heros[0].isClimbRabbitDn = true;
							heros[0].isClimbRabbitUp = false;
						}
					}
				}
				if (e.KeyCode == Keys.C)
				{
					if (!heros[0].isRat)
					{
						if (duckOnLadder())
						{
							heros[0].isClimbDuckUp = true;
							heros[0].isClimbDuckDn = false;
							heros[0].isIdelDuck = false;
							heros[0].isJumpDuckLeft = false;
							heros[0].isJumpDuckRight = false;
							heros[0].isJumpDuckUp = false;

						}
					}
					else
					{

						if (rabbitOnSewer())
						{
							heros[0].isClimbRabbitUp = true;
							heros[0].isIdelRabbit = false;
						}
					}
				}
			}

		}

		private void Form1_KeyUp(object sender, KeyEventArgs e)
		{
			if (heros[0].isRat)
			{
				if (e.KeyCode == Keys.Right)
				{
					heros[0].isWalkRabbit = false;
					heros[0].isRightRabbit = false;
					heros[0].isRunRabbit = false;
					heros[0].isIdelRabbit = true;
				}
				if (e.KeyCode == Keys.Space)
				{
					heros[0].isRunRabbit = false;
					heros[0].isIdelRabbit = true;

				}
				if (e.KeyCode == Keys.Left)
				{
					heros[0].isRunRabbit = false;
					heros[0].isLeftRabbit = false;
					heros[0].isWalkRabbit = false;
					heros[0].isIdelRabbit = true;
				}
				if (e.KeyCode == Keys.C)
				{
					heros[0].isClimbRabbitUp = false;
				}
				if (e.KeyCode == Keys.Down)
				{
					heros[0].isClimbRabbitDn = false;
				}
				if (e.KeyCode == Keys.J)
				{
					heros[0].isRabbitMonalisa = false;
					heros[0].isRabbitTut = false;
				}
			}
			else
			{
				if (e.KeyCode == Keys.Right)
				{
					heros[0].isWalkDuck = false;
					heros[0].isRightDuck = false;
					heros[0].isRunDuck = false;
					heros[0].isPushDuckRight = false;
					heros[0].isIdelDuck = true;

				}
				if (e.KeyCode == Keys.Space)
				{
					heros[0].isRunDuck = false;
					heros[0].isIdelDuck = true;
				}
				if (e.KeyCode == Keys.Left)
				{
					heros[0].isRunDuck = false;
					heros[0].isLeftDuck = false;
					heros[0].isWalkDuck = false;
					heros[0].isPushDuckLeft = false;

					heros[0].isIdelDuck = true;
				}
				if (e.KeyCode == Keys.C)
				{
					heros[0].isClimbDuckUp = false;
				}
				if (e.KeyCode == Keys.Down)
				{
					heros[0].isClimbDuckDn = false;
				}
			}
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			drawDb(CreateGraphics());
		}

		public void drawDb(Graphics g)
		{
			if (off != null)
			{
				Graphics g2 = Graphics.FromImage(off);

				drawScene(g2);

				g.DrawImage(off, 0, 0);
			}
		}

		void drawScene(Graphics g2)
		{
			//g2.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
			g2.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

			g2.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
			g2.Clear(Color.Black);
			for (int i = 0; i < bgs.Count; i++)
			{

				bg pTrv = bgs[i];

				Rectangle rcDst = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
				Rectangle rcSrc = new Rectangle(xStart, yStart, this.ClientSize.Width, this.ClientSize.Height);
				g2.DrawImage(pTrv.img,
								rcDst,
								rcSrc,
								GraphicsUnit.Pixel
								);
			}

			for (int i = 0; i < doors.Count; i++)
			{
				door ptrv = doors[i];
				if (i == 0)
				{
					g2.DrawImage(ptrv.img, ptrv.x - xStart, ptrv.y - yStart, 160, 240);
				}
				if (i == 1)
				{
					g2.DrawImage(ptrv.img, ptrv.x - xStart, ptrv.y - yStart, 140, 220);

				}
			}
			for (int i = 0; i < sewers.Count; i++)
			{
				sewer ptrv = sewers[i];
				if (i == 0)
				{
					g2.DrawImage(ptrv.img, ptrv.x - xStart, ptrv.y - yStart, ptrv.img.Width, ptrv.img.Height - 100);
				}
				else
				{
					g2.DrawImage(ptrv.img, ptrv.x - xStart, ptrv.y - yStart, ptrv.img.Width, ptrv.img.Height);
				}
			}
			for (int i = 0; i < tilesLvl1.Count; i++)
			{
				tile ptrv = tilesLvl1[i];
				g2.DrawImage(ptrv.img, ptrv.x - xStart, ptrv.y - yStart, ptrv.img.Width, ptrv.img.Height);
			}
			for (int i = 0; i < tilesWLvl1.Count; i++)
			{
				tile ptrv = tilesWLvl1[i];
				g2.DrawImage(ptrv.img, ptrv.x - xStart, ptrv.y - yStart, ptrv.img.Width, ptrv.img.Height - 70);
			}

			for (int i = 0; i < tilesPLvl1.Count; i++)
			{
				tile ptrv = tilesPLvl1[i];
				if (lvl == 1)
				{
					g2.DrawImage(ptrv.img, ptrv.x - xStart, ptrv.y - yStart, 50, 50);
				}
				else if (lvl == 2)
				{
					g2.DrawImage(ptrv.img, ptrv.x - xStart, ptrv.y - yStart, elevators[0].img.Width, 50);

				}
			}
			for (int i = 0; i < chains.Count; i++)
			{
				chain ptrv = chains[i];
				g2.DrawImage(ptrv.img, ptrv.x - xStart, ptrv.y - yStart, 50, ptrv.img.Height - 50);
			}
			for (int i = 0; i < ladders.Count; i++)
			{
				ladder ptrv = ladders[i];
				g2.DrawImage(ptrv.img, ptrv.x - xStart, ptrv.y - yStart, ptrv.img.Width + 20, ptrv.img.Height);
			}
			for (int i = 0; i < keysLvl1.Count; i++)
			{
				key ptrv = keysLvl1[i];
				g2.DrawImage(ptrv.imgs[ptrv.currentKeyFrame], ptrv.x - xStart, ptrv.y - yStart, 30, 30);
			}
			for (int i = 0; i < doors.Count; i++)
			{
				door ptrv = doors[i];
				if (i == 0)
				{
					g2.DrawImage(ptrv.img, ptrv.x - xStart, ptrv.y - yStart, 160, 240);
				}
				if (i == 1)
				{
					g2.DrawImage(ptrv.img, ptrv.x - xStart, ptrv.y - yStart, 140, 220);

				}
			}

			for (int i = 0; i < stairss.Count; i++)
			{
				stairs ptrv = stairss[i];
				g2.DrawImage(ptrv.img, ptrv.x - xStart, ptrv.y - yStart, ptrv.img.Width, ptrv.img.Height);
			}


			for (int i = 0; i < elevators.Count; i++)
			{
				elevator ptrv = elevators[i];
				g2.DrawImage(ptrv.img, ptrv.x - xStart, ptrv.y - yStart, ptrv.img.Width, ptrv.img.Height);
			}
			for (int i = 0; i < heros.Count; i++)
			{
				hero ptrv = heros[i];

				if (heros[0].isTransforming)
				{
					g2.DrawImage(ptrv.transformImgs[ptrv.currenttransformFrame], (ptrv.xDuck - 10) - xStart, ptrv.yDuck - yStart, 120, 120);
                    
                }
				else if (!heros[0].isTransforming)
				{
					if (ptrv.isRat)
					{
						if (ptrv.isWalkRabbit)
						{
							if (ptrv.isRightRabbit)
							{
								g2.DrawImage(ptrv.walkImgsRabbitRight[ptrv.currentWalkFrameRabbitRight], ptrv.xRabbit - xStart, ptrv.yRabbit - yStart, 100, 100);
							}
							else if (ptrv.isLeftRabbit)
							{
								g2.DrawImage(ptrv.walkImgsRabbitLeft[ptrv.currentWalkFrameRabbitLeft], ptrv.xRabbit - xStart, ptrv.yRabbit - yStart, 100, 100);
							}
						}
						else if (ptrv.isRunRabbit)
						{
							if (ptrv.isRightRabbit)
							{
								g2.DrawImage(ptrv.runImgsRabbitRight[ptrv.currentRunFrameRabbitRight], ptrv.xRabbit - xStart, ptrv.yRabbit - yStart, 100, 100);
							}
							else if (ptrv.isLeftRabbit)
							{
								g2.DrawImage(ptrv.runImgsRabbitLeft[ptrv.currentRunFrameRabbitLeft], ptrv.xRabbit - xStart, ptrv.yRabbit - yStart, 100, 100);
							}
							else
							{
								g2.DrawImage(ptrv.idleImgsRabbitRight[ptrv.currentIdelFrameRabbitRight], ptrv.xRabbit - xStart, ptrv.yRabbit - yStart, 100, 100);
							}
						}
						else if (ptrv.isClimbRabbitUp || ptrv.isClimbRabbitDn)
						{
							g2.DrawImage(ptrv.climbImgsRabbitRight[ptrv.currentClimbFrameRabbitRight], ptrv.xRabbit - xStart, ptrv.yRabbit - yStart, 100, 100);
						}
						else if (ptrv.isRabbitMonalisa)
						{
							g2.DrawImage(ptrv.rabbitMonalisa, 1200 - xStart, 280 - yStart, 160, 220);
						}
						else if (ptrv.isRabbitTut)
						{
							g2.DrawImage(ptrv.rabbitTut, 1859 - xStart, 920 - yStart, 180, 200);
						}
						else
						{
							g2.DrawImage(ptrv.idleImgsRabbitRight[ptrv.currentIdelFrameRabbitRight], ptrv.xRabbit - xStart, ptrv.yRabbit - yStart, 100, 100);
						}
					}
					else
					{
						if (!heros[0].isDead)
						{
							if (ptrv.isWalkDuck)
							{
								if (ptrv.isRightDuck)
								{
									g2.DrawImage(ptrv.walkImgsDuckRight[ptrv.currentWalkFramesDuckRight], ptrv.xDuck - xStart, ptrv.yDuck - yStart, 70, 70);
								}
								else if (ptrv.isLeftDuck)
								{
									g2.DrawImage(ptrv.walkImgsDuckLeft[ptrv.currentWalkFramesDuckLeft], ptrv.xDuck - xStart, ptrv.yDuck - yStart, 70, 70);
								}
							}
							else if (ptrv.isRunDuck)
							{
								if (ptrv.isRightDuck)
								{
									g2.DrawImage(ptrv.runImgsDuckRight[ptrv.currentRunFrameDuckRight], ptrv.xDuck - xStart, ptrv.yDuck - yStart, 70, 70);
								}
								else if (ptrv.isLeftDuck)
								{
									g2.DrawImage(ptrv.runImgsDuckLeft[ptrv.currentRunFrameDuckLeft], ptrv.xDuck - xStart, ptrv.yDuck - yStart, 70, 70);
								}
								else
								{
									g2.DrawImage(ptrv.idelImgsDuckRight[ptrv.currentIdleFrameDuckRight], ptrv.xDuck - xStart, ptrv.yDuck - yStart, 70, 70);
								}
							}
							else if (ptrv.isClimbDuckUp || ptrv.isClimbDuckDn || duckOnLadder())
							{
								g2.DrawImage(ptrv.climbImgsDuck[ptrv.currentClimbFramesDuck], ptrv.xDuck - xStart, ptrv.yDuck - yStart, 70, 70);
							}
							else if (heros[0].isJumpDuckUp)
							{
								g2.DrawImage(ptrv.jumpImgsDuckRight[ptrv.currentJumpFrameDuckRight], ptrv.xDuck - xStart, ptrv.yDuck - yStart, 70, 70);
							}
							else if (heros[0].isJumpDuckRight)
							{
								g2.DrawImage(ptrv.jumpImgsDuckRight[ptrv.currentJumpFrameDuckRight], ptrv.xDuck - xStart, ptrv.yDuck - yStart, 70, 70);
							}
							else if (heros[0].isJumpDuckLeft)
							{
								g2.DrawImage(ptrv.jumpImgsDuckLeft[ptrv.currentJumpFrameDuckLeft], ptrv.xDuck - xStart, ptrv.yDuck - yStart, 70, 70);
							}
							/*
							else if (ptrv.isAttkDuck)
							{
								if (ptrv.isRightDuck)
								{
									g2.DrawImage(ptrv.attkImgsDuckRight[ptrv.currentAttkFrameDuckRight], ptrv.xDuck, ptrv.yDuck, 100, 100);
								}
								else if (ptrv.isLeftDuck)
								{
									g2.DrawImage(ptrv.attkImgsDuckLeft[ptrv.currentAttkFrameDuckLeft], ptrv.xDuck, ptrv.yDuck, 100, 100);
								}
							}
							*/
							else
							{
								if (heros[0].fallingFrameCount > 8)
								{
									g2.DrawImage(ptrv.falling, ptrv.xDuck - xStart, ptrv.yDuck - yStart, 70, 70);
								}
								else
								{
									g2.DrawImage(ptrv.idelImgsDuckRight[ptrv.currentIdleFrameDuckRight], ptrv.xDuck - xStart, ptrv.yDuck - yStart, 70, 70);
								}
							}
						}
						else
						{
							g2.DrawImage(ptrv.deathDuck, ptrv.xDuck - xStart, ptrv.yDuck - yStart, 70, 70);
						}
					}
				}
			}
			for (int i = 0; i < boxes.Count; i++)
			{
				box ptrv = boxes[i];
				g2.DrawImage(ptrv.img, ptrv.x - xStart, ptrv.y - yStart, 120, 120);
			}
			for (int i = 0; i < waters.Count; i++)
			{
				water ptrv = waters[i];
				if (ptrv.on == 1)
				{
					g2.DrawImage(ptrv.imgs[ptrv.currentWaterFrame], ptrv.x - xStart, ptrv.y - yStart, ptrv.imgs[0].Width, ptrv.imgs[0].Height - 190);
				}
			}
			for (int i = 0; i < chicks.Count; i++)
			{
				chick ptrv = chicks[i];

				g2.DrawImage(ptrv.imgs[ptrv.currentChickFrame], ptrv.x - xStart, ptrv.y - yStart, ptrv.imgs[0].Width, ptrv.imgs[0].Height);

			}

			// distraction chick
			if (heros[0].distract != null && heros[0].chickHoldCt > 0)
			{
				heros[0].distract.currentChickFrame =
					(heros[0].distract.currentChickFrame + 1) % heros[0].distract.imgs.Count;
				g2.DrawImage(
					heros[0].distract.imgs[heros[0].distract.currentChickFrame],
					heros[0].distract.x - xStart,
					heros[0].distract.y - yStart,
					heros[0].distract.imgs[0].Width,
					heros[0].distract.imgs[0].Height);
			}

			for (int i = 0; i < frogs.Count; i++)
			{
				frog ptrv = frogs[i];

				if (ptrv.isIdle)
				{
					g2.DrawImage(ptrv.IdleImgsFrog[ptrv.currentIdleFrameFrog], ptrv.x - xStart, ptrv.y - yStart, 100, 100);
				}
				
			}
			for (int i = 0; i < paintings.Count; i++)
			{
				painting p = paintings[i];
				g2.DrawImage(p.img, p.x - xStart, p.y - yStart, p.width, p.height);
			}


			for (int i = 0; i < item.Count; i++)
			{
				items p = item[i];
				g2.DrawImage(p.img, p.x - xStart, p.y - yStart, p.width, p.height);
			}

			for (int i = 0; i < securities.Count; i++)
			{
				security sec = securities[i];

				if (sec.state == 0 || sec.state == 2)
				{
					g2.DrawImage(sec.idleImgsSec[sec.currentIdleFrameSec], sec.x - xStart, sec.y - yStart, 140, 140);
				}
				else if (sec.state == 1 || (i == 1 && sec.state == 6 && sec.facingLeft))
				{
					g2.DrawImage(sec.walkImgsSecLeft[sec.currentWalkFrameSecLeft], sec.x - xStart, sec.y - yStart, 140, 140);
				}
				else if (sec.state == 3 || sec.state == 4 || (i == 1 && sec.state == 6 && !sec.facingLeft))
				{
					g2.DrawImage(sec.walkImgsSecRight[sec.currentWalkFrameSecRight], sec.x - xStart, sec.y - yStart, 140, 140);
				}
				else if (sec.state == 5)
				{
					g2.DrawImage(sec.idleImgsSec[sec.currentIdleFrameSec], sec.x - xStart, sec.y - yStart, 140, 140);
				}

				// state 6 phase 4 (waiting at chick) — idle frame
				if (i == 1 && sec.state == 6 && sec.stateCt > 0 && sec.stateCt < 180
					&& sec.x <= 550 && sec.y >= 1680)
				{
					g2.DrawImage(sec.idleImgsSec[sec.currentIdleFrameSec], sec.x - xStart, sec.y - yStart, 140, 140);
				}

				
				if (sec.state == 5 && i == 0) // Monalisa guard only
				{
					Font fSurprised = new Font("Arial", 22, FontStyle.Bold);
					g2.DrawString("?!", fSurprised, Brushes.Red, sec.x - xStart + 20, sec.y - yStart - 36);
				}
				if (sec.state == 5 && i == 1) // Tena only
				{
					Font fLord = new Font("Arial", 18, FontStyle.Bold);
					g2.DrawString("H holy f f.. fuck", fLord, Brushes.Red, sec.x - xStart + 10, sec.y - yStart - 36);
				}
				if (sec.state == 2 && i == 0 && heros[0].isRabbitMonalisa)
				{
					Font fHmm = new Font("Arial", 18, FontStyle.Bold);
					g2.DrawString(sec.hmmStr, fHmm, Brushes.White, sec.x - xStart + 10, sec.y - yStart - 36);
				}
			}
			if (!showMenu)
			{
				// dashboard
				g2.FillRectangle(Brushes.Black, 10, 10, 160, 50);
				g2.DrawRectangle(Pens.White, 10, 10, 160, 50);

				// dashboard
				g2.FillRectangle(Brushes.Black, 10, 10, 220, 60);
				g2.DrawRectangle(Pens.White, 10, 10, 220, 60);
				
				g2.DrawString("Score: " + score, fontDash, brushYellow, 20, 20);
				if (heros[0].isRat)
				{
					g2.DrawString("Bun",fontDash,brushYellow , 20, 42);
				}
				else
				{
					g2.DrawString("Ducky", fontDash, brushYellow, 20, 42);

				}

				// character box
				g2.FillRectangle(Brushes.Black, 180, 10, 50, 50);
				g2.DrawRectangle(Pens.White, 180, 10, 50, 50);

				if (heros[0].isRat)
				{
					g2.DrawImage(heros[0].idleImgsRabbitRight[0], 180, 10, 50, 50);
				}
				else
				{
					g2.DrawImage(heros[0].idelImgsDuckRight[0], 180, 10, 50, 50);
				}
				//inventory
				g2.FillRectangle(Brushes.Black, 10, 80, 50, 50);
				g2.DrawRectangle(Pens.White, 10, 80, 50, 50);

                if (heros[0].hasKey)
				{ 
                    g2.DrawImage(iconKey, 20, 90, 30, 30);
				}
				g2.FillRectangle(Brushes.Black, 60, 80, 50, 50);
				g2.DrawRectangle(Pens.White, 60, 80, 50, 50);

				if ( (chicks.Count == 0 && lvl == 1 ) || ( heros[0].chickHoldCt == 0 && lvl == 2 ))
				{
					g2.DrawImage(iconChick, 70, 90, 30, 30);
				}

				g2.FillRectangle(Brushes.Black, 110, 80, 50, 50);
				g2.DrawRectangle(Pens.White, 110, 80, 50, 50);
				if (paintings.Count == 0 && lvl > 1)
				{
					g2.DrawImage(iconMonaliza, 120, 90, 30, 30);
				}
				g2.FillRectangle(Brushes.Black, 160, 80, 50, 50);
				g2.DrawRectangle(Pens.White, 160, 80, 50, 50);
				if (item.Count == 0 && lvl > 1)
				{
					g2.DrawImage(iconTut, 170, 90, 30, 30);
				}
				if (showFrogDialog)
				{
					int dialogX = this.ClientSize.Width / 2 - 150;
					int dialogY = this.ClientSize.Height / 2 - 50;
					g2.FillRectangle(Brushes.Black, dialogX, dialogY, 300, 60);
					g2.DrawRectangle(Pens.White, dialogX, dialogY, 300, 60);
					g2.DrawString("Press T to transform!", new Font("Arial", 14, FontStyle.Bold), Brushes.White, dialogX + 20, dialogY + 18);
				}
                else if (showFrogDialog2)
                {
                    int dialogX = this.ClientSize.Width / 2 - 150;
                    int dialogY = this.ClientSize.Height / 2 - 50;

                    g2.FillRectangle(Brushes.Black, dialogX, dialogY, 370, 120);
                    g2.DrawRectangle(Pens.White, dialogX, dialogY, 370, 120);


                    g2.DrawString("The beginning comes from those who jump.", fontDialog2, brushYellow, dialogX + 10, dialogY + 18);
                    g2.DrawString("Then come those who lay eggs.", fontDialog2, brushYellow, dialogX + 10, dialogY + 38);
                    g2.DrawString("Together, they reveal the start.", fontDialog2, brushYellow, dialogX + 10, dialogY + 58);
                    g2.DrawString("The ending rests on the door where we meet..", fontDialog2, brushYellow, dialogX + 10, dialogY + 78);
                }
            }
			if (showChapterScreen)
			{
				g2.FillRectangle(Brushes.Black, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
                Brush b = new SolidBrush(Color.FromArgb(chapterColorVal, chapterColorVal, chapterColorVal));
                g2.DrawString(chapterText, fontChapter, b, this.ClientSize.Width / 2 - 250, this.ClientSize.Height / 2);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			button1.Visible = false;
			button1.Enabled = false;

			button2.Visible = false;
			button2.Enabled = false;

			button3.Visible = false;
			button3.Enabled = false;

			button4.Visible = false;
			button4.Enabled = false;

			showMenu = false;

		}

		private void button2_Click(object sender, EventArgs e)
		{
			Form1 f1 = new Form1();
			this.Hide();
			f1.Show();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			currentSaveId = -1;
			lvl = 1;
			score = 0;
			showMenu = false;

			button1.Visible = false;
			button1.Enabled = false;

			button2.Visible = false;
			button2.Enabled = false;

			button3.Visible = false;
			button3.Enabled = false;

			button4.Visible = false;
			button4.Enabled = false;


			// reset hero
			heros[0].xDuck = 650;
			heros[0].yDuck = 950;
			heros[0].xRabbit = 650;
			heros[0].yRabbit = 920;
			heros[0].isRat = false;
			heros[0].isDead = false;
			heros[0].hasKey = false;
			heros[0].jumpVelocity = 0;
			heros[0].fallingFrameCount = 0;


			LoadLevel(1);
		}

		private void button4_Click(object sender, EventArgs e)
		{

			List<GameSave> allSaves = new List<GameSave>();

			StreamReader sr = new StreamReader("saves.txt");
			int ct = 0;
			while (!sr.EndOfStream)
			{
				string line = sr.ReadLine();
				if (ct > 0)
				{
					string[] temp = line.Split(',');
					GameSave existingSave = new GameSave();
					existingSave.id = Convert.ToInt16(temp[0]);
					existingSave.level = Convert.ToInt16(temp[1]);
					existingSave.score = Convert.ToInt16(temp[2]);
					existingSave.hasKey = Convert.ToBoolean(temp[3]);
					existingSave.isRat = Convert.ToBoolean(temp[4]);
					existingSave.duckX = Convert.ToInt16(temp[5]);
					existingSave.duckY = Convert.ToInt16(temp[6]);
					existingSave.rabbitX = Convert.ToInt16(temp[7]);
					existingSave.rabbitY = Convert.ToInt16(temp[8]);
					existingSave.keysCollected = Convert.ToInt16(temp[9]);
					existingSave.chicksCollected = Convert.ToInt16(temp[10]);
					existingSave.repairedElevator = Convert.ToBoolean(temp[11]);
					existingSave.stoleTut = Convert.ToBoolean(temp[12]);
					allSaves.Add(existingSave);
				}
				ct++;
			}
			sr.Close();
			GameSave newSave = new GameSave();
			bool isOverwrite = false;
			if (currentSaveId != -1)
			{
				for (int i = 0; i < allSaves.Count; i++)
				{
					if (allSaves[i].id == currentSaveId)
					{

						newSave = allSaves[i];
						isOverwrite = true;
						break;
					}
				}
			}
			if (!isOverwrite)
			{
				if (allSaves.Count > 0)
				{
					newSave.id = allSaves[allSaves.Count - 1].id + 1;
				}
				else
				{
					newSave.id = 1;
				}
			}

			newSave.level = lvl;
			newSave.score = score;
			newSave.hasKey = heros[0].hasKey;
			newSave.isRat = heros[0].isRat;
			newSave.duckX = heros[0].xDuck;
			newSave.duckY = heros[0].yDuck;
			newSave.rabbitX = heros[0].xRabbit;
			newSave.rabbitY = heros[0].yRabbit;
			newSave.repairedElevator = heros[0].repairedElevator;
			newSave.stoleTut = heros[0].stoleTut;

			if (!heros[0].hasKey)
			{
				newSave.keysCollected = 0;
			}
			newSave.chicksCollected = 1 - chicks.Count;


			if (isOverwrite)
			{
				for (int i = 0; i < allSaves.Count; i++)
				{
					if (allSaves[i].id == currentSaveId)
					{
						allSaves[i] = newSave;
						break;
					}
				}
			}
			else
			{
				allSaves.Add(newSave);
				currentSaveId = newSave.id;
			}

			StreamWriter sw = new StreamWriter("saves.txt");
			sw.WriteLine("ID,Level,Score,HasKey,IsRat,DuckX,DuckY,RabbitX,RabbitY,KeysCollected,ChicksCollected,repairedElevator,stoleTut");
			for (int i = 0; i < allSaves.Count; i++)
			{
				sw.WriteLine($"{allSaves[i].id},{allSaves[i].level},{allSaves[i].score},{allSaves[i].hasKey},{allSaves[i].isRat}," +
							 $"{allSaves[i].duckX},{allSaves[i].duckY},{allSaves[i].rabbitX},{allSaves[i].rabbitY}," +
							 $"{allSaves[i].keysCollected},{allSaves[i].chicksCollected},{allSaves[i].repairedElevator},{allSaves[i].stoleTut}");
			}
			sw.Close();

			string message = "";
			if (isOverwrite)
			{
				message = $"Game Saved! (Overwrote Save ID: {newSave.id})";
			}
			else
			{
				message = $"Game Saved! (New Save ID: {newSave.id})";
			}
			MessageBox.Show(message, "Save Game");


			showMenu = false;
			button1.Visible = false;
			button1.Enabled = false;
			button2.Visible = false;
			button2.Enabled = false;
			button3.Visible = false;
			button3.Enabled = false;
			button4.Visible = false;
			button4.Enabled = false;
		}

		private void Form2_Load(object sender, EventArgs e)
		{

		}
	}
	public class bg
	{
		public int X, Y;
		public Bitmap img;
	}
	class door
	{
		public int x;
		public int y;
		public Bitmap img;
	}
	class hero
	{
		//============================
		public bool hasKey;
		public bool isDead;
		public bool repairedElevator;
		public bool stoleTut;
		public List<Bitmap> walkImgsDuckRight;
		public List<Bitmap> walkImgsDuckLeft;

		public List<Bitmap> idelImgsDuckRight;
		public List<Bitmap> idelImgsDuckLeft;

		public List<Bitmap> runImgsDuckRight;
		public List<Bitmap> runImgsDuckLeft;

		public List<Bitmap> attkImgsDuckRight;
		public List<Bitmap> attkImgsDuckLeft;

		public List<Bitmap> climbImgsDuck;
		public List<Bitmap> jumpImgsDuckRight;
		public List<Bitmap> jumpImgsDuckLeft;
		public Bitmap falling;
		public Bitmap deathDuck;
		public Bitmap hitWallDuck;


		public int currentClimbFramesDuck;
		public int currentWalkFramesDuckRight;
		public int currentWalkFramesDuckLeft;

		public int currentIdleFrameDuckRight;
		public int currentIdleFrameDuckLeft;

		public int currentAttkFrameDuckRight;
		public int currentAttkFrameDuckLeft;

		public int currentRunFrameDuckRight;             //DUCK
		public int currentRunFrameDuckLeft;

		public int currentJumpFrameDuckRight;
		public int currentJumpFrameDuckLeft;

		public int jumpVelocity;
		public bool isOnGround;
		public int xDuck;
		public int yDuck;
		public bool isClimbDuckUp;
		public bool isClimbDuckDn;
		public bool isWalkDuck;
		public bool isRunDuck;
		public bool isIdelDuck;
		public bool isAttkDuck;
		public bool isRightDuck;
		public bool isLeftDuck;

		public bool isJumpDuckUp;
		public bool isJumpDuckRight;
		public bool isJumpDuckLeft;
		public bool isPushDuckRight;
		public bool isPushDuckLeft;
		public bool canPushRight;
		public bool canPushLeft;
		public bool isHitWall;
		public int fallingFrameCount;
		//============================

		public List<Bitmap> walkImgsRabbitRight;
		public List<Bitmap> walkImgsRabbitLeft;

		public List<Bitmap> idleImgsRabbitRight;
		public List<Bitmap> idleImgsRabbitLeft;

		public List<Bitmap> runImgsRabbitRight;
		public List<Bitmap> runImgsRabbitLeft;
		public List<Bitmap> climbImgsRabbitRight;
		public List<Bitmap> climbImgsRabbitLeft;
		public Bitmap rabbitMonalisa;
		public Bitmap rabbitTut;
		public bool isRabbitMonalisa;
		public bool isRabbitTut;

		public int currentWalkFrameRabbitRight;
		public int currentWalkFrameRabbitLeft;

		public int currentIdelFrameRabbitRight;
		public int currentIdelFrameRabbitLeft;

		public int currentRunFrameRabbitRight;             //RABBIT
		public int currentRunFrameRabbitLeft;             //RABBIT


		public int currentClimbFrameRabbitRight;
		public int currentClimbFrameRabbitLeft;

		public int xRabbit;
		public int yRabbit;
		public bool isWalkRabbit;
		public bool isRunRabbit;
		public bool isIdelRabbit;
		public bool isRightRabbit;
		public bool isLeftRabbit;
		public bool isClimbRabbit;
		public bool isClimbRabbitDn;
		public bool isClimbRabbitUp;
		//============================
		public bool isRat;

		public chick distract;       // the chick the hero is holding
		public int chickHoldCt;

        public bool isTransforming = false;
        public int transformTimer = 0;
		public List<Bitmap> transformImgs;
		public int currenttransformFrame;
    }
	class frog
	{
		public int x;
		public int y;
		public int startX;
		public List<Bitmap> IdleImgsFrog;
		public List<Bitmap> hopImgsFrogRight;
		public List<Bitmap> hopImgsFrogLeft;
		public List<Bitmap> attkImgsFrog;
		public int currentIdleFrameFrog;
		public int currentHopFrameFrogRigt;
		public int currentHopFrameFrogLeft;
		public int currentAttkFrameFrog;
		public bool facingRight;
		public bool isIdle;
		public bool isHopToHero;
		public bool isAttacking;
		public bool isHopBack;
	}
	class security
	{
		public int x;
		public int y;
		public int startX;
		public int targetX;
		public int state;
		public int stateCt;
		public bool facingLeft;
		public int currentIdleFrameSec;
		public int currentWalkFrameSecLeft;
		public int currentWalkFrameSecRight;
		public List<Bitmap> idleImgsSec;
		public List<Bitmap> walkImgsSecLeft;
		public List<Bitmap> walkImgsSecRight;

		public int seenCt;
		public string hmmStr;
		public int hmmCt;
		public bool paintingWasStolen;
		public bool reached;
	}
	class tile
	{
		public int x;
		public int y;
		public Bitmap img;
	}
	class ladder
	{
		public int x;
		public int y;
		public Bitmap img;
	}
	class key
	{
		public int x;
		public int y;
		public List<Bitmap> imgs;
		public int currentKeyFrame;
	}
	class sewer
	{
		public int x;
		public int y;
		public Bitmap img;
	}
	class box
	{
		public int x;
		public int y;
		public Bitmap img;
	}
	class brick
	{
		public int x;
		public int y;
		public Bitmap img;
	}
	class chain
	{
		public int x;
		public int y;
		public Bitmap img;
	}
	class water
	{
		public int x;
		public int y;
		public List<Bitmap> imgs;
		public int on;
		public int currentWaterFrame;
	}
	class chick
	{
		public int x;
		public int y;
		public List<Bitmap> imgs;

		public int currentChickFrame;
	}
	class stairs
	{
		public int x;
		public int y;
		public Bitmap img;
	}
	class painting
	{
		public int x;
		public int y;
		public int width;
		public int height;
		public Bitmap img;
	}
	class items
	{
		public int x;
		public int y;
		public int width;
		public int height;
		public Bitmap img;
	}
	class elevator
	{
		public int x;
		public int y;
		public Bitmap img;
		public int moveDir = 1; // 1 = down, -1 = up
		public int speed = 2;
		public int topY;
		public int bottomY;
		public tile floor;
	}
}
