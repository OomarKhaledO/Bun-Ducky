namespace Bun_Ducky
{
	partial class Form2
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button2
			// 
			this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.button2.BackColor = System.Drawing.Color.DarkSlateGray;
			this.button2.Enabled = false;
			this.button2.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
			this.button2.FlatAppearance.BorderSize = 5;
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.Font = new System.Drawing.Font("Arial Black", 8F);
			this.button2.ForeColor = System.Drawing.Color.Yellow;
			this.button2.Location = new System.Drawing.Point(288, 328);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(232, 56);
			this.button2.TabIndex = 3;
			this.button2.Text = "EXIT";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Visible = false;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.button1.BackColor = System.Drawing.Color.DarkSlateGray;
			this.button1.Enabled = false;
			this.button1.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
			this.button1.FlatAppearance.BorderSize = 5;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Font = new System.Drawing.Font("Arial Black", 8F, System.Drawing.FontStyle.Bold);
			this.button1.ForeColor = System.Drawing.Color.Yellow;
			this.button1.Location = new System.Drawing.Point(288, 136);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(232, 56);
			this.button1.TabIndex = 2;
			this.button1.Text = "CONTINUE";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Visible = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button3
			// 
			this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.button3.BackColor = System.Drawing.Color.DarkSlateGray;
			this.button3.Enabled = false;
			this.button3.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
			this.button3.FlatAppearance.BorderSize = 5;
			this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button3.Font = new System.Drawing.Font("Arial Black", 8F);
			this.button3.ForeColor = System.Drawing.Color.Yellow;
			this.button3.Location = new System.Drawing.Point(288, 200);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(232, 56);
			this.button3.TabIndex = 4;
			this.button3.Text = "NEW GAME";
			this.button3.UseVisualStyleBackColor = false;
			this.button3.Visible = false;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button4
			// 
			this.button4.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.button4.BackColor = System.Drawing.Color.DarkSlateGray;
			this.button4.Enabled = false;
			this.button4.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
			this.button4.FlatAppearance.BorderSize = 5;
			this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button4.Font = new System.Drawing.Font("Arial Black", 8F);
			this.button4.ForeColor = System.Drawing.Color.Yellow;
			this.button4.Location = new System.Drawing.Point(288, 264);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(232, 56);
			this.button4.TabIndex = 5;
			this.button4.Text = "SAVE GAME";
			this.button4.UseVisualStyleBackColor = false;
			this.button4.Visible = false;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// Form2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form2";
			this.Text = "Bun&Ducky";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
	}
}