using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bun_Ducky
{
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
		//============================

		public List<Bitmap> walkImgsRabbitRight;
		public List<Bitmap> walkImgsRabbitLeft;

		public List<Bitmap> idleImgsRabbitRight;
		public List<Bitmap> idleImgsRabbitLeft;

		public List<Bitmap> runImgsRabbitRight;
		public List<Bitmap> runImgsRabbitLeft;
		public List<Bitmap> climbImgsRabbitRight;
		public List<Bitmap> climbImgsRabbitLeft;

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
		public Bitmap img;
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
	public partial class Form2 : Form
	{
        List<door> doors = new List<door>();
        List<bg> bgs = new List<bg>();
		List<hero> heros = new List<hero>();
		List<tile> tilesLvl1 = new List<tile>();
		List<tile> tilesPLvl1 = new List<tile>();
		List<tile> tilesWLvl1 = new List<tile>();
		List<ladder> ladders = new List<ladder>();
		List<key> keysLvl1 = new List<key>();
		List<sewer> sewers = new List<sewer>();
		List<box> boxes = new List<box>();
		Bitmap off;
		Timer gameTimer = new Timer();
		int xStart = 0;
		int yStart = 0;
		int tilesXInc = 0;
		int tilesYInc = 0;
		public Form2()
		{
			this.WindowState = FormWindowState.Maximized;
			Load += Form1_Load;
			Paint += Form1_Paint;
			KeyDown += Form1_KeyDown;
			KeyUp += Form1_KeyUp;
			gameTimer.Interval = 16;
			gameTimer.Tick += GameTimer_Tick;
			gameTimer.Start();
			FormClosed += Form2_FormClosed;
		}

		private void Form2_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
            door d = new door();
            d.img = new Bitmap("door.png");
            d.x = 750;
            d.y = 950; ;
            doors.Add(d);
            //======
            //LVL 1
            //======
            bg bb = new bg();
			bb.img = new Bitmap("bg.png");
			bb.X = 0;
			bb.Y = 0;

			bgs.Add(bb);
			//y1380  x1725
			for (int i = 0; i < 1; i++)
			{
				key g = new key();
				g.x = 1725;
				g.y = 1360;
				g.img = new Bitmap("lvl1\\key.png");
				keysLvl1.Add(g);
			}
			for (int i = 0; i < 9; i++)
			{
				tile g = new tile();
				g.x = 740 + tilesXInc;
				g.y = 1670;
				g.img = new Bitmap("lvl1\\tiles\\tile4.png");
				tilesXInc += 70;
				tilesLvl1.Add(g);
			}
			for (int i = 0; i < 1; i++)
			{
				box g = new box();
				g.x = 305;
				g.y = 930;
				g.img = new Bitmap("lvl1\\box.png");
				tilesXInc += 70;
				boxes.Add(g);
			}
			tilesXInc = 0;
			for (int i = 0; i < 1; i++)
			{
				box g = new box();
				g.x = 930;
				g.y = 1600;
				g.img = new Bitmap("lvl1\\box.png");
				tilesXInc += 70;
				boxes.Add(g);
			}
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
			tilesYInc = 0;
			for (int i = 0; i < 10; i++)
			{
				tile g = new tile();
				g.x = 740 + tilesXInc;
				g.y = 400;
				g.img = new Bitmap("lvl1\\tiles\\tile4.png");
				tilesXInc += 70;
				tilesLvl1.Add(g);
			}
			//375
			//KEY ROOM S
			for (int i = 0; i < 5; i++)
			{
				tile l = new tile();
				l.x = 960 + tilesXInc;
				l.y = 1400;
				l.img = new Bitmap("lvl1\\tiles\\tile3.png");
				tilesXInc += 70;
				tilesLvl1.Add(l);
			}
			tilesXInc = 0;
			for (int i = 0; i < 5; i++)
			{
				tile l = new tile();
				l.x = 1600 + tilesXInc;
				l.y = 1260;
				l.img = new Bitmap("lvl1\\tiles\\tile3.png");
				tilesXInc += 70;
				tilesLvl1.Add(l);
			}
			for (int i = 0; i < 1; i++)
			{
				tile l = new tile();
				l.x = 1635;
				l.y = 1260;
				l.img = new Bitmap("lvl1\\tiles\\tile3U.png");
				tilesWLvl1.Add(l);
			}
			for (int i = 0; i < 1; i++)
			{
				tile l = new tile();
				l.x = 2190;
				l.y = 1260;
				l.img = new Bitmap("lvl1\\tiles\\tile3U.png");
				tilesWLvl1.Add(l);
			}
			tilesXInc = 0;
			tilesYInc = 0;
			for (int i = 0; i < 5; i++)
			{
				tile l = new tile();
				l.x = 1855;
				l.y = 550 + tilesYInc;
				l.img = new Bitmap("lvl1\\tiles\\tile3U.png");
				tilesYInc += 70;
				tilesWLvl1.Add(l);
			}
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
			//1166 1875
			tilesXInc = 0;
			for (int i = 0; i < 5; i++)
			{
				tile l = new tile();
				l.x = 1890 + tilesXInc;
				l.y = 1182;
				l.img = new Bitmap("lvl1\\tiles\\tile3.png");
				tilesXInc += 70;
				tilesLvl1.Add(l);
			}
			//2190	x
			//1394 y
			tilesYInc = 0;
			for (int i = 0; i < 5; i++)
			{
				sewer l = new sewer();
				l.x = 2190;
				l.y = 1250 + tilesYInc;
				l.img = new Bitmap("lvl1\\sewer.png");
				tilesYInc += 70;
				sewers.Add(l);
			}
			//KEY ROOM E
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
			for (int i = 0; i < 1; i++)
			{
				tile l = new tile();
				l.x = 1565 + tilesXInc;
				l.y = 1750;
				l.img = new Bitmap("lvl1\\tiles\\tile5-0.5.png");
				tilesXInc += 70;
				tilesLvl1.Add(l);
			}
			tilesXInc = 0;

			//===========
			//LVL 1   END
			//===========
			hero pnn = new hero();
			//   === < DUCK ===

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
			pnn.xDuck = 650;
			pnn.yDuck = 950;
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
			//   === RABBIT /> ===
			heros.Add(pnn);
			off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
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

		
		bool snapDuckToGround(int oldFeetY,int newFeetY)
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
			int rabbitCenterY = heros[0].yRabbit + 50;
			for (int i = 0; i < sewers.Count; i++)
			{
				int sewerRight = sewers[i].x + sewers[i].img.Width;
				int sewerBottom = sewers[i].y + sewers[i].img.Height;
				if (rabbitCenterX >= sewers[i].x && rabbitCenterX <= sewerRight)
				{
					if (rabbitCenterY >= sewers[i].y && rabbitCenterY <= sewerBottom)
					{
						return true;
					}
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
			if (duckOnTile() || duckOnTileP() || duckOnBox())
			{
				return;
			}

			int oldFeetY = heros[0].yDuck + 70;

			heros[0].yDuck += 8;

			int newFeetY = heros[0].yDuck + 70;


			if (duckOnTile() || duckOnTileP() || duckOnBox())
			{
				snapDuckToGround(oldFeetY, newFeetY);
			}
		}
		void checkWall(bool isRat)
		{
			if (isRat)
			{
				int rabbitRight = heros[0].xRabbit+ heros[0].runImgsRabbitRight[0].Width;
				if (rabbitRight >= xStart + this.ClientSize.Width - 50)
				{
					heros[0].xRabbit= heros[0].xRabbit- 20;
				}
				if (heros[0].xRabbit <= 0)
				{
					heros[0].xRabbit = heros[0].xRabbit+ 20;
				}
			}
			else
			{
				int duckRight = heros[0].xDuck + heros[0].walkImgsDuckRight[0].Width;
				
				if (duckRight >= xStart + this.ClientSize.Width)
				{
					heros[0].xDuck = heros[0].xDuck - 20;
				}
				if (heros[0].xDuck <= 0)
				{
					heros[0].xDuck = heros[0].xDuck + 20;
				}
			}
			
		}
		private void GameTimer_Tick(object sender, EventArgs e)
		{
			this.Text = "" + heros[0].yDuck + "|||" + heros[0].xDuck;
			heros[0].canPushRight = false;
			heros[0].canPushLeft = false;
			int box1 = -1;
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
				else if (onSewer)
				{
					// on sewer but not pressing climb -> block left/right movement, just idle
					heros[0].currentIdelFrameRabbitRight = (heros[0].currentIdelFrameRabbitRight + 1) % heros[0].idleImgsRabbitRight.Count;
				}
				else if (heros[0].isWalkRabbit)
				{
					if (heros[0].isRightRabbit)
					{
						checkWall(heros[0].isRat);
						heros[0].currentIdelFrameRabbitLeft = 0;
						heros[0].currentIdelFrameRabbitRight = 0;
						heros[0].currentWalkFrameRabbitRight = (heros[0].currentWalkFrameRabbitRight + 1) % heros[0].walkImgsRabbitRight.Count;
						heros[0].xRabbit += 5;
					}
					else if (heros[0].isLeftRabbit)
					{
						checkWall(heros[0].isRat);
						heros[0].currentRunFrameRabbitRight = 0;
						heros[0].currentIdelFrameRabbitLeft = 0;
						heros[0].currentWalkFrameRabbitLeft = (heros[0].currentWalkFrameRabbitLeft + 1) % heros[0].walkImgsRabbitLeft.Count;
						heros[0].xRabbit -= 5;
					}
				}
				else if (heros[0].isRunRabbit)
				{
					if (heros[0].isRightRabbit)
					{
						checkWall(heros[0].isRat);

						heros[0].currentIdelFrameRabbitRight = 0;
						heros[0].currentRunFrameRabbitRight = (heros[0].currentRunFrameRabbitRight + 1) % heros[0].runImgsRabbitRight.Count;
						heros[0].xRabbit += 15;
					}
					else if (heros[0].isLeftRabbit)
					{
						checkWall(heros[0].isRat);

						heros[0].currentRunFrameRabbitRight = 0;
						heros[0].currentIdelFrameRabbitLeft = 0;
						heros[0].currentRunFrameRabbitLeft = (heros[0].currentRunFrameRabbitLeft + 1) % heros[0].runImgsRabbitLeft.Count;
						heros[0].xRabbit -= 15;
					}
					else
					{
						heros[0].currentIdelFrameRabbitRight = (heros[0].currentIdelFrameRabbitRight + 1) % heros[0].idleImgsRabbitRight.Count;
					}
				}
				else
				{
					heros[0].currentIdelFrameRabbitRight = (heros[0].currentIdelFrameRabbitRight + 1) % heros[0].idleImgsRabbitRight.Count;
				}
			}
			else
			{
				// ===== DUCK MOVEMENT =====
				bool onLadder = duckOnLadder();

				if (heros[0].isClimbDuckUp)
				{
					// only climb if on a ladder
					if (onLadder)
					{
						heros[0].currentIdleFrameDuckRight = 0;
						heros[0].currentClimbFramesDuck = (heros[0].currentClimbFramesDuck + 1) % heros[0].climbImgsDuck.Count;
						heros[0].yDuck -= 8;
					}
					// if not on ladder, just ignore
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
					// move horizontally while in air
					if (heros[0].isJumpDuckRight)
					{
						heros[0].currentJumpFrameDuckRight = (heros[0].currentJumpFrameDuckRight + 1) % heros[0].jumpImgsDuckRight.Count;
						heros[0].xDuck += 5;
					}
					else if (heros[0].isJumpDuckLeft)
					{
						heros[0].currentJumpFrameDuckLeft = (heros[0].currentJumpFrameDuckLeft + 1) % heros[0].jumpImgsDuckLeft.Count;
						heros[0].xDuck -= 5;
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
						// only check landing when falling down (velocity positive)
						if (snapDuckToGround(oldFeetY, newFeetY))
						{
							heros[0].isJumpDuckUp = false;
							heros[0].isJumpDuckRight = false;
							heros[0].isJumpDuckLeft = false;
							heros[0].jumpVelocity = 0;
						}
					}
				}
				// ---- WALK / RUN / ATTACK / IDLE ----
				else if (heros[0].isWalkDuck)
				{
					if (heros[0].isRightDuck)
					{
						heros[0].currentIdleFrameDuckLeft = 0;

						// PUSHING
						if (heros[0].canPushRight)
						{
							heros[0].isPushDuckRight = true;
							heros[0].isPushDuckLeft = false;
							heros[0].currentWalkFramesDuckRight = 0;
							heros[0].xDuck += 3;
							boxes[box1].x += 3;
						}
						else
						{
							heros[0].isPushDuckRight = false;

							heros[0].currentWalkFramesDuckRight =
							(heros[0].currentWalkFramesDuckRight + 1) %
							heros[0].walkImgsDuckRight.Count;

							heros[0].xDuck += 5;
						}
					}
					else if (heros[0].isLeftDuck)
					{
						heros[0].currentIdleFrameDuckRight = 0;

						// PUSHING
						if (heros[0].canPushLeft)
						{
							heros[0].isPushDuckLeft = true;
							heros[0].currentWalkFramesDuckLeft = 0;
							heros[0].isPushDuckRight = false;

							heros[0].xDuck -= 3;
							boxes[box1].x -= 3;
						}
						else
						{
							heros[0].isPushDuckLeft = false;

							heros[0].currentWalkFramesDuckLeft =
							(heros[0].currentWalkFramesDuckLeft + 1) %
							heros[0].walkImgsDuckLeft.Count;

							heros[0].xDuck -= 5;
						}
					}
				}
				else if (heros[0].isRunDuck)
				{
					if (heros[0].isRightDuck)
					{
						heros[0].currentIdleFrameDuckRight = 0;
						heros[0].currentRunFrameDuckRight = (heros[0].currentRunFrameDuckRight + 1) % heros[0].runImgsDuckRight.Count;
						heros[0].xDuck += 15;
					}
					else if (heros[0].isLeftDuck)
					{
						heros[0].currentIdleFrameDuckLeft = 0;
						heros[0].currentRunFrameDuckLeft = (heros[0].currentRunFrameDuckLeft + 1) % heros[0].runImgsDuckLeft.Count;
						heros[0].xDuck -= 15;
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

				// apply gravity every tick for duck (when not climbing or jumping)
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
			if (xStart > maxX)
			{
				xStart = maxX;
			}
			if (yStart > maxY)
			{
				yStart = maxY;
			}
            // key pickup
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
            drawDb(CreateGraphics());
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
            if (e.KeyCode == Keys.E)
            {
                if (heros[0].hasKey)
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

                    for (int i = doors.Count - 1; i >= 0; i--)
                    {
                        if (heroX + heroW >= doors[i].x && heroX <= doors[i].x + doors[i].img.Width)
                        {
                            if (heroY + heroH >= doors[i].y && heroY <= doors[i].y + doors[i].img.Height)
                            {
                                doors.RemoveAt(i);
                                heros[0].hasKey = false;
                            }
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.T)
			{
				if (heros[0].isRat)
				{
					heros[0].xDuck = heros[0].xRabbit;
					heros[0].yDuck = heros[0].yRabbit + 30;
					heros[0].isRat = false;
					heros[0].isRunRabbit = false;
					heros[0].isWalkRabbit = false;
					heros[0].isLeftRabbit = false;
					heros[0].isRightRabbit = false;
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
				}
			}
			if (e.KeyCode == Keys.Right)
			{
				if (heros[0].isRat)
				{
					// only move right if not on a sewer
					if (!rabbitOnSewer())
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
				}
				if (!heros[0].isRat && !heros[0].isJumpDuckRight && !heros[0].isJumpDuckLeft && !heros[0].isJumpDuckUp)
				{
					// only move right if not actively on ladder with climb pressed
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
					// only move left if not on a sewer
					if (!rabbitOnSewer())
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
				}
				if (!heros[0].isRat && !heros[0].isJumpDuckRight && !heros[0].isJumpDuckLeft && !heros[0].isJumpDuckUp)
				{
					// only move left if not actively on ladder with climb pressed
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
					// only allow climb down if on a ladder
					if (duckOnLadder())
					{
						heros[0].isIdelDuck = false;
						heros[0].isClimbDuckDn = true;
						heros[0].isClimbDuckUp = false;
					}
				}
				else
				{
					// only allow climb down if on a sewer
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
					// only allow climb up if on a ladder
					if (duckOnLadder())
					{
						heros[0].isClimbDuckUp = true;
						heros[0].isClimbDuckDn = false;
						heros[0].isIdelDuck = false;
						heros[0].isJumpDuckLeft = false;
						heros[0].isJumpDuckRight = false;
						heros[0].isJumpDuckUp  = false;

					}
				}
				else
				{
					// only allow climb up if on a sewer
					if (rabbitOnSewer())
					{
						heros[0].isClimbRabbitUp = true;
						heros[0].isIdelRabbit = false;
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

		void drawDb(Graphics g)
		{
			Graphics g2 = Graphics.FromImage(off);

			drawScene(g2);

			g.DrawImage(off, 0, 0);
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
			for (int i = 0; i < sewers.Count; i++)
			{
				sewer ptrv = sewers[i];
				g2.DrawImage(ptrv.img, ptrv.x - xStart, ptrv.y - yStart, ptrv.img.Width, ptrv.img.Height);
			}
			for (int i = 0; i < tilesPLvl1.Count; i++)
			{
				tile ptrv = tilesPLvl1[i];
				g2.DrawImage(ptrv.img, ptrv.x - xStart, ptrv.y - yStart, 50, 50);
			}
			for (int i = 0; i < ladders.Count; i++)
			{
				ladder ptrv = ladders[i];
				g2.DrawImage(ptrv.img, ptrv.x - xStart, ptrv.y - yStart, ptrv.img.Width + 20, ptrv.img.Height);
			}
			for (int i = 0; i < keysLvl1.Count; i++)
			{
				key ptrv = keysLvl1[i];
				g2.DrawImage(ptrv.img, ptrv.x - xStart, ptrv.y - yStart, 30, 30);
			}

			for (int i = 0; i < heros.Count; i++)
			{
				hero ptrv = heros[i];
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
					else
					{
						g2.DrawImage(ptrv.idleImgsRabbitRight[ptrv.currentIdelFrameRabbitRight], ptrv.xRabbit - xStart, ptrv.yRabbit - yStart, 100, 100);
					}
				}
				else
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
						g2.DrawImage(ptrv.idelImgsDuckRight[ptrv.currentIdleFrameDuckRight], ptrv.xDuck - xStart, ptrv.yDuck - yStart, 70, 70);
					}
				}
			}
            for (int i = 0; i < doors.Count; i++)
            {
                door ptrv = doors[i];
                g2.DrawImage(ptrv.img, ptrv.x - xStart, ptrv.y - yStart, 100, 150);
            }
            for (int i = 0; i < boxes.Count; i++)
			{
				box ptrv = boxes[i];
				g2.DrawImage(ptrv.img, ptrv.x - xStart, ptrv.y - yStart, 120, 120);
			}
		}
	}
}