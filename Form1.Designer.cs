namespace InventorBasics
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.CaptionTB = new System.Windows.Forms.TextBox();
            this.widthTB = new System.Windows.Forms.TextBox();
            this.HeightTB = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.CreatePart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.CalculateHoleB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 64);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "edit caption";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CaptionTB
            // 
            this.CaptionTB.Location = new System.Drawing.Point(12, 38);
            this.CaptionTB.Name = "CaptionTB";
            this.CaptionTB.Size = new System.Drawing.Size(100, 20);
            this.CaptionTB.TabIndex = 1;
            // 
            // widthTB
            // 
            this.widthTB.Location = new System.Drawing.Point(120, 12);
            this.widthTB.Name = "widthTB";
            this.widthTB.Size = new System.Drawing.Size(100, 20);
            this.widthTB.TabIndex = 2;
            // 
            // HeightTB
            // 
            this.HeightTB.Location = new System.Drawing.Point(120, 38);
            this.HeightTB.Name = "HeightTB";
            this.HeightTB.Size = new System.Drawing.Size(100, 20);
            this.HeightTB.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(145, 64);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "edit view size";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // CreatePart
            // 
            this.CreatePart.Location = new System.Drawing.Point(12, 106);
            this.CreatePart.Name = "CreatePart";
            this.CreatePart.Size = new System.Drawing.Size(75, 23);
            this.CreatePart.TabIndex = 5;
            this.CreatePart.Text = "create part";
            this.CreatePart.UseVisualStyleBackColor = true;
            this.CreatePart.Click += new System.EventHandler(this.CreatePart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "caption";
            // 
            // CalculateHoleB
            // 
            this.CalculateHoleB.Location = new System.Drawing.Point(15, 227);
            this.CalculateHoleB.Name = "CalculateHoleB";
            this.CalculateHoleB.Size = new System.Drawing.Size(257, 23);
            this.CalculateHoleB.TabIndex = 9;
            this.CalculateHoleB.Text = "CalculateHole";
            this.CalculateHoleB.UseVisualStyleBackColor = true;
            this.CalculateHoleB.Click += new System.EventHandler(this.CalculateHoleB_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.CalculateHoleB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CreatePart);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.HeightTB);
            this.Controls.Add(this.widthTB);
            this.Controls.Add(this.CaptionTB);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox CaptionTB;
        private System.Windows.Forms.TextBox widthTB;
        private System.Windows.Forms.TextBox HeightTB;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button CreatePart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CalculateHoleB;
    }
}

