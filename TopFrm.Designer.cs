namespace Toefl
{
    partial class TopFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopFrm));
            this.btnWords = new System.Windows.Forms.Button();
            this.btnReading = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnWords
            // 
            this.btnWords.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnWords.BackgroundImage")));
            this.btnWords.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnWords.Location = new System.Drawing.Point(65, 76);
            this.btnWords.Name = "btnWords";
            this.btnWords.Size = new System.Drawing.Size(100, 98);
            this.btnWords.TabIndex = 0;
            this.btnWords.Text = "单词训练";
            this.btnWords.UseVisualStyleBackColor = true;
            this.btnWords.Click += new System.EventHandler(this.btnWords_Click);
            // 
            // btnReading
            // 
            this.btnReading.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReading.BackgroundImage")));
            this.btnReading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReading.Location = new System.Drawing.Point(278, 76);
            this.btnReading.Name = "btnReading";
            this.btnReading.Size = new System.Drawing.Size(96, 98);
            this.btnReading.TabIndex = 1;
            this.btnReading.Text = "模拟阅读 ";
            this.btnReading.UseVisualStyleBackColor = true;
            this.btnReading.Click += new System.EventHandler(this.btnReading_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(65, 257);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 89);
            this.button1.TabIndex = 2;
            this.button1.Text = "编辑";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TopFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 517);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnReading);
            this.Controls.Add(this.btnWords);
            this.Name = "TopFrm";
            this.Text = "TopFrm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnWords;
        private System.Windows.Forms.Button btnReading;
        private System.Windows.Forms.Button button1;
    }
}