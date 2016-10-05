namespace Toefl
{
    partial class frmWordExercise
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
            this.components = new System.ComponentModel.Container();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPlay = new System.Windows.Forms.Button();
            this.Web1 = new System.Windows.Forms.WebBrowser();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnToefl = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.bnalltpo = new System.Windows.Forms.Button();
            this.btnReview = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnSeven = new System.Windows.Forms.Button();
            this.btnG4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 19;
            this.listBox1.Location = new System.Drawing.Point(12, 63);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(273, 346);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "a",
            "b",
            "c",
            "d",
            "e",
            "f",
            "g",
            "h",
            "i",
            "j",
            "k",
            "l",
            "m",
            "n",
            "o",
            "p",
            "q",
            "r",
            "s",
            "t",
            "u",
            "v",
            "w",
            "x",
            "y",
            "z"});
            this.comboBox1.Location = new System.Drawing.Point(13, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(30, 20);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.Text = "a";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 21);
            this.button1.TabIndex = 2;
            this.button1.Text = "general";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(220, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(175, 26);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 21);
            this.btnPlay.TabIndex = 4;
            this.btnPlay.Text = "Sat";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // Web1
            // 
            this.Web1.Location = new System.Drawing.Point(294, 78);
            this.Web1.MinimumSize = new System.Drawing.Size(20, 20);
            this.Web1.Name = "Web1";
            this.Web1.ScriptErrorsSuppressed = true;
            this.Web1.Size = new System.Drawing.Size(763, 283);
            this.Web1.TabIndex = 36;
            this.Web1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(294, 11);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(283, 40);
            this.textBox1.TabIndex = 37;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(583, 11);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(405, 40);
            this.textBox2.TabIndex = 38;
            // 
            // btnToefl
            // 
            this.btnToefl.Location = new System.Drawing.Point(94, 25);
            this.btnToefl.Name = "btnToefl";
            this.btnToefl.Size = new System.Drawing.Size(75, 21);
            this.btnToefl.TabIndex = 39;
            this.btnToefl.Text = "toefl";
            this.btnToefl.UseVisualStyleBackColor = true;
            this.btnToefl.Click += new System.EventHandler(this.btnToefl_Click);
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(294, 376);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(479, 33);
            this.textBox3.TabIndex = 52;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(294, 63);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(10, 136);
            this.richTextBox1.TabIndex = 53;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            this.richTextBox1.DoubleClick += new System.EventHandler(this.richTextBox1_DoubleClick);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(13, 476);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(89, 21);
            this.btnFind.TabIndex = 54;
            this.btnFind.Text = "Find in article";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(13, 503);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(203, 76);
            this.listBox2.TabIndex = 55;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // bnalltpo
            // 
            this.bnalltpo.Location = new System.Drawing.Point(108, 477);
            this.bnalltpo.Name = "bnalltpo";
            this.bnalltpo.Size = new System.Drawing.Size(82, 23);
            this.bnalltpo.TabIndex = 56;
            this.bnalltpo.Text = "tpo";
            this.bnalltpo.UseVisualStyleBackColor = true;
            this.bnalltpo.Click += new System.EventHandler(this.bnalltpo_Click);
            // 
            // btnReview
            // 
            this.btnReview.Location = new System.Drawing.Point(196, 479);
            this.btnReview.Name = "btnReview";
            this.btnReview.Size = new System.Drawing.Size(75, 21);
            this.btnReview.TabIndex = 57;
            this.btnReview.Text = "Review";
            this.btnReview.UseVisualStyleBackColor = true;
            this.btnReview.Click += new System.EventHandler(this.btnReview_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnSeven
            // 
            this.btnSeven.Location = new System.Drawing.Point(13, 450);
            this.btnSeven.Name = "btnSeven";
            this.btnSeven.Size = new System.Drawing.Size(75, 21);
            this.btnSeven.TabIndex = 58;
            this.btnSeven.Text = "托福7天";
            this.btnSeven.UseVisualStyleBackColor = true;
            this.btnSeven.Click += new System.EventHandler(this.btnSeven_Click);
            // 
            // btnG4
            // 
            this.btnG4.Location = new System.Drawing.Point(108, 450);
            this.btnG4.Name = "btnG4";
            this.btnG4.Size = new System.Drawing.Size(75, 21);
            this.btnG4.TabIndex = 59;
            this.btnG4.Text = "四级单词";
            this.btnG4.UseVisualStyleBackColor = true;
            this.btnG4.Click += new System.EventHandler(this.btnG4_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(995, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(47, 23);
            this.button2.TabIndex = 60;
            this.button2.Text = "l";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // frmWordExercise
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 607);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnG4);
            this.Controls.Add(this.btnSeven);
            this.Controls.Add(this.btnReview);
            this.Controls.Add(this.bnalltpo);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.btnToefl);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Web1);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.listBox1);
            this.Name = "frmWordExercise";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmWordExercise";
            this.Load += new System.EventHandler(this.frmWordExercise_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.WebBrowser Web1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnToefl;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button bnalltpo;
        private System.Windows.Forms.Button btnReview;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnSeven;
        private System.Windows.Forms.Button btnG4;
        private System.Windows.Forms.Button button2;
    }
}