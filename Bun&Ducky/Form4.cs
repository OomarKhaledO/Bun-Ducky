using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Bun_Ducky
{
    public partial class Form4 : Form
    {
        Bitmap off;
        const int m = 5;
        const int n = 3;
        cube[,] cubes = new cube[m, n];
        Timer tt = new Timer();
        public bool maskReleased;
        int moveSensorCt = 0;
        int sensorR = 2;
        int sensorC = 0;
        int sensorStep = 0;
        int tx = 40;
        int ty = 40;
        int xinc = 0;
        int w = 0;
        int h = 0;
        bool caught;
        public Form4()
        {
            InitializeComponent();
            Load += Form4_Load;
            Paint += Form4_Paint;
            MouseDown += Form4_MouseDown;
            tt.Interval = 16;
            tt.Tick += Tt_Tick;
            tt.Start();
        }
        List<Bitmap> sensorImgs = new List<Bitmap>();
        List<bg> bgs = new List<bg>();
        Rectangle src;
        Rectangle dst;
        private void Tt_Tick(object sender, EventArgs e)
        {
            moveSensorCt++;
            if (moveSensorCt == 2)
            {
                moveSensorCt = 0;

                if (sensorR == 2 && sensorC == 0)
                {
                    sensorR = 1; sensorC = 0; 
                }
                else if (sensorR == 1 && sensorC == 0)
                {
                    sensorR = 0; sensorC = 1;
                }
                else if (sensorR == 0 && sensorC == 1)
                {
                    sensorR = 1; sensorC = 2; 
                }
                else if (sensorR == 1 && sensorC == 2) 
                {
                    sensorR = 2; sensorC = 2; 
                }
                else if (sensorR == 2 && sensorC == 2)
                { 
                    sensorR = 3; sensorC = 2; 
                }
                else if (sensorR == 3 && sensorC == 2)
                {
                    sensorR = 4; sensorC = 1; 
                }
                else if (sensorR == 4 && sensorC == 1)
                {
                    sensorR = 3; sensorC = 0; 
                }
                else if (sensorR == 3 && sensorC == 0)
                {
                    sensorR = 2; sensorC = 0; 
                }
            }
            drawDB(CreateGraphics());
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.Size = new Size(400, 600);
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            //Image.FromFile("lvl2\\tutBox\\bg.png");
            for (int i = 0; i < 1; i++)
            {
                bg maskBg = new bg();
                maskBg.X = 0;
                maskBg.Y = 0;
                maskBg.img = new Bitmap("lvl2\\tutBox\\bg2.png");
                 src = new Rectangle(0, 0, maskBg.img.Width, maskBg.img.Height);
                 dst = new Rectangle(0, 0, this.Width, this.Height);
                bgs.Add(maskBg);
            }
            for (int r = 0; r < m; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    cube pnn = new cube();
                    pnn.imgs = new List<Bitmap>();
                    pnn.x = tx + xinc;
                    pnn.y = ty;
                    cubes[r, c] = pnn;

                    // nails
                    if ((r == 0 || r == 4) && (c == 0 || c == 2))
                    {
                        for (int i = 5; i < 10; i++)
                        {
                            Bitmap b = new Bitmap("lvl2\\tutBox\\nail" + i + ".png");
                            b.MakeTransparent();
                            pnn.imgs.Add(b);
                        }
                        pnn.type = "nail";
                    }
                    // clamp
                    else if ((r == 1 || r == 3) && c == 1)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Bitmap b = new Bitmap("lvl2\\tutBox\\clamp" + (i + 1) + ".png");
                            b.MakeTransparent();
                            pnn.imgs.Add(b);
                        }
                        pnn.type = "clamp";
                    }
                    // sensor — loaded here, stays here, we just track sensorR/sensorC
                    else if (r == 2 && c == 0)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            Bitmap b = new Bitmap("lvl2\\tutBox\\sensor" + (i + 1) + ".png");
                            b.MakeTransparent();
                            sensorImgs.Add(b);
                        }
                        // make the cell itself empty so no image stays behind
                        Bitmap empty = new Bitmap("lvl1\\tiles\\nothing.png");
                        empty.MakeTransparent();
                        pnn.imgs.Add(empty);
                        pnn.type = "empty";
                    }
                    // mask
                    else if (r == 2 && c == 1)
                    {
                        Bitmap b = new Bitmap("lvl2\\tutBox\\mask.png");
                        pnn.imgs.Add(b);
                        pnn.type = "mask";
                    }
                    // empty
                    else
                    {
                        Bitmap b = new Bitmap("lvl1\\tiles\\nothing.png");
                        b.MakeTransparent();
                        pnn.imgs.Add(b);
                        pnn.type = "empty";
                    }

                    if (w == 0) { w = 120; h = 100; }
                    xinc += w;
                }
                xinc = 0;
                ty += h;
            }
            ty = 40;
        }

        private void Form4_MouseDown(object sender, MouseEventArgs e)
        {
            int c = (e.X - tx) / w;
            int r = (e.Y - ty) / h;

            if (r < 0 || r >= m || c < 0 || c >= n) return;

            cube clicked = cubes[r, c];

            if (clicked.type == "nail")
            {
                if (clicked.unscrewed) return;

                if (sensorR >= r - 1 && sensorR <= r + 1 && sensorC >= c - 1 && sensorC <= c + 1)
                {
                    caught = true;
                    drawDB(CreateGraphics());

                    tt.Stop();
                    MessageBox.Show("BUSTED");
                    this.Close();
                    return;

                }

                clicked.cF = (clicked.cF + 1) % clicked.imgs.Count;

                if (clicked.cF == clicked.imgs.Count - 1)
                {
                    clicked.unscrewed = true;
                }

                if (r == 0)
                {
                    bool leftDone = cubes[0, 0].unscrewed;
                    bool rightDone = cubes[0, 2].unscrewed;
                    if (leftDone && rightDone)
                    {
                        cubes[1, 1].cF = 3;
                    }
                    else if (leftDone && !rightDone)
                    {
                        cubes[1, 1].cF = 2;
                    }
                    else if (!leftDone && rightDone)
                    {
                        cubes[1, 1].cF = 1;
                    }
                }
                if (r == 4)
                {
                    bool leftDone = cubes[4, 0].unscrewed;
                    bool rightDone = cubes[4, 2].unscrewed;
                    if (leftDone && rightDone)
                    {
                        cubes[3, 1].cF = 3;
                    }
                    else if (leftDone && !rightDone)
                    {
                        cubes[3, 1].cF = 2;
                    }
                    else if (!leftDone && rightDone)
                    {
                        cubes[3, 1].cF = 1;
                    }
                }
            }
            else if (clicked.type == "clamp")
            {
                bool leftDone, rightDone;
                if (r == 1)
                {
                    leftDone = cubes[0, 0].unscrewed;
                    rightDone = cubes[0, 2].unscrewed;
                }
                else
                {
                    leftDone = cubes[4, 0].unscrewed;
                    rightDone = cubes[4, 2].unscrewed;
                }

                if (!leftDone || !rightDone)
                {
                    return;
                }
                if (sensorR >= r - 1 && sensorR <= r + 1 && sensorC >= c - 1 && sensorC <= c + 1)
                {
                    caught = true;
                    drawDB(CreateGraphics());
                    tt.Stop();
                    MessageBox.Show("BUSTED");
                    this.Close();

                }

                clicked.cF = 3;
                clicked.removed = true;

                if (cubes[1, 1].removed && cubes[3, 1].removed)
                {
                    MessageBox.Show("TUT IS YOURS!");
                    tt.Stop();
                    maskReleased = true;
                    this.Close();
                }
            }

            drawDB(CreateGraphics());
        }

        private void Form4_Paint(object sender, PaintEventArgs e)
        {
            drawDB(CreateGraphics());
        }

        void drawDB(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            drawScene(g2);
            g.DrawImage(off, 0, 0);
        }

        void drawScene(Graphics g2)
        {
            g2.Clear(Color.Gray);
            for (int i = 0; i < bgs.Count; i++)
            {
                bg ptrv = bgs[i];
                
                g2.DrawImage(ptrv.img, dst, src, GraphicsUnit.Pixel);
            }
            for (int r = 0; r < m; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    cube ptrv = cubes[r, c];
                    int ww = 80;
                    int hh = 80;
                    if (ptrv.type == "clamp" || ptrv.type == "mask")
                    {
                        ww = 100;
                        hh = 100;
                    }

                        g2.DrawImage(ptrv.imgs[ptrv.cF], ptrv.x, ptrv.y, ww, hh);

                    if (r == sensorR && c == sensorC)
                    {
                        if (caught)
                        {
                            ptrv.cF= 1;
                        }
                        g2.DrawImage(sensorImgs[ptrv.cF], ptrv.x, ptrv.y, 80, 80);
                    }
                }
            }
        }

        class cube
        {
            public int x;
            public int y;
            public List<Bitmap> imgs;
            public string type;
            public int cF;
            public bool unscrewed;
            public bool removed;
            public int rStart;
            public int cStart;
            public bool up;
            public bool down;
            public bool right;
            public bool left;
            
        }
        
    }
}