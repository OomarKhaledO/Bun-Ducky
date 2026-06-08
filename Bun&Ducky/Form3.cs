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
    public partial class Form3 : Form
    {
        string code = "";
        string correctCode = "2205";
        public  bool Complete = false;
      
        public Form3()
        {
            InitializeComponent();
           
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.Size = new Size(241, 314);
            //pictureBox1.Image = Image.FromFile("lvl2\\tutBox\\2.png");
            //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            message.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            code += "1";
            digits.Text = code;
            CheckCode();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            code += "2";
            digits.Text = code;
            CheckCode();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            code += "3";
            digits.Text = code;
            CheckCode();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            code += "4";
            digits.Text = code;
            CheckCode();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            code += "5";
            digits.Text = code;
            CheckCode();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            code += "6";
            digits.Text = code;
            CheckCode();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            code += "7";
            digits.Text = code;
            CheckCode();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            code += "8";
            digits.Text = code;
            CheckCode();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            code += "9";
            digits.Text = code;
            CheckCode();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            code += "0";
            digits.Text = code;
            CheckCode();

        }

        void CheckCode()
        {
            if (code.Length == 4)
            {
                message.Visible = true;
                if (code == correctCode)
                {
                    message.Text = "Access Granted";
                    message.ForeColor = Color.Green;

                    Complete = true;
                }
                else
                {
                    message.Text = "Access Denied";
                    message.ForeColor = Color.Red;
                }
                Refresh(); 
                System.Threading.Thread.Sleep(1000);
                this.Close();
            }
        }
    }
}
