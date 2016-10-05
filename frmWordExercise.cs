using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections;
using System.IO;
using System.Configuration;
using System.Web;
using System.Threading;
using UnileverCAS.UnileverFun;
using System.Data.SqlClient;
using System.Data.OleDb;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using PdfMakerHelper;
using System.Drawing.Text;
using Toefl.Utility;


namespace Toefl
{
    public partial class frmWordExercise : Form
    {
        public frmWordExercise()
        {
            InitializeComponent();
            Setsize();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kenal.showword(this.comboBox1, this.listBox1,"general");
            label1.Text = listBox1.Items.Count.ToString();
        }
        


        private void btnPlay_Click(object sender, EventArgs e)
        {
            var source = new AutoCompleteStringCollection();

            DataTable dt = null;

            string sql = "select worde,wordc from word_list ";

            dt = SQLiteHelper.ExecuteDataSet(sql, CommandType.Text).Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                source.Add(dt.Rows[i][0].ToString() + " " + dt.Rows[i][1].ToString());
            }




            sql = "select word,chinese from toefl_wm ";

            dt = SQLiteHelper.ExecuteDataSet(sql, CommandType.Text).Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                source.Add(dt.Rows[i][0].ToString() + " " + dt.Rows[i][1].ToString());
            }






            textBox3.AutoCompleteCustomSource = source;
            textBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }



        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (Web1.ReadyState != WebBrowserReadyState.Complete)
                return;

            if (e.Url.ToString() != Web1.Url.ToString())
                return;

            string content = this.Web1.DocumentText.ToString();

            clsMCI mp3player = new clsMCI();


            try
            {

                this.Web1.ScriptErrorsSuppressed = true;
                

                int iChineseStart = content.IndexOf("label_list");
                int iChineseEnd = content.IndexOf("</label>", iChineseStart);

                string wordc = content.Substring(iChineseStart + 17, iChineseEnd - iChineseStart);


                wordc = Regex.Replace(wordc, @"^[ \t]*|[ \t]*$ ", " ");
                iChineseEnd = wordc.IndexOf("</label>", 0);


                textBox2.Text = wordc.Substring(8, iChineseEnd - 8);

                int iTableStart = content.IndexOf("asplay", 0);
                int iTableEnd = content.IndexOf(".mp3", 0);

                content = content.Substring(iTableStart + 8, iTableEnd - iTableStart - 4);
                string filename = textBox1.Text.ToString();

                textBox1.BackColor = Color.Red;

                listBox1.Items.Add(textBox1.Text.ToString().Trim() + "@" + textBox2.Text.ToString().Trim());

                if (!Kenal.existfile(filename))
                {
                    Kenal.save_mp3(filename, content);

                }
                Kenal.play_mp3(filename);

                

            }

            catch (Exception ec)
            { ec.ToString(); }


        }

        private void frmWordExercise_Load(object sender, EventArgs e)
        {
            this.Web1.ScriptErrorsSuppressed = true;
        }

        private void btnPlaySingle_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //listBox1.SelectedItem = this.listBox1.Items[i];
            try
            {
                string[] filenames = this.listBox1.SelectedItem.ToString().Split('@');
                string filename = filenames[0].ToString();

                textBox1.Text = filename;
                textBox2.Text = filenames[1].ToString();
                textBox3.Text = filename;



                if (Kenal.existfile(textBox1.Text.ToString()))
                {
                    textBox1.BackColor = Color.Aqua;
                    Kenal.play_mp3(textBox1.Text.ToString());

                }
                else
                {

                    string Myurl = "http://www.iciba.com/" + filename;

                    //this.Web1 .Navigate(Myurl);

                }
            }

            catch
            { }


        }

        private void btnToefl_Click(object sender, EventArgs e)
        {
            Kenal.showword(this.comboBox1, this.listBox1, "toefl");
            label1.Text = listBox1.Items.Count.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
  //          string sql = "insert into article_list (title, content,type,words) values('" + title + "','" + rtf + "','" + type + "','" + words + "')";
            try
            {
                DataTable dt = null;

                this.listBox2.Items.Clear();

                string sql = "";


                sql = "select title,content from article_list where content like '%" + textBox1.Text.ToString() + "%'";

                dt = SQLiteHelper.ExecuteDataSet(sql, CommandType.Text).Tables[0];
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        this.listBox2.Items.Add(dt.Rows[i][0].ToString());

                        this.richTextBox1.Rtf = dt.Rows[i][1].ToString();

                    }
                }
                else
                {
                    MessageBox.Show("Find None");
                    Kenal.getMp3url(Web1, textBox1.Text.ToString());
                }
            }

            catch
            { }






        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Kenal.displayArticle(this.richTextBox1, listBox2.SelectedItem.ToString());

                int index = 0;

                string text = richTextBox1.Text;

                string keyword = textBox1.Text.ToString();

                index = text.IndexOf(keyword) + 1;

                //MessageBox.Show("目标位置：" + index + ",文本长度：" + text.Length);

                richTextBox1.SelectionStart = index - 1;
                richTextBox1.SelectionLength = 100;

                richTextBox1.SelectionColor = Color.Red;
            }

            catch
            { }

        }

        private void bnalltpo_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = null;

                this.listBox2.Items.Clear();

                string sql = "";


                sql = "select title,content from article_list where title like '%tpo%'";

                dt = SQLiteHelper.ExecuteDataSet(sql, CommandType.Text).Tables[0];
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        this.listBox2.Items.Add(dt.Rows[i][0].ToString());

                        this.richTextBox1.Rtf = dt.Rows[i][1].ToString();

                    }
                }
                else
                {
                    MessageBox.Show("Find None");
                    Kenal.getMp3url(Web1, textBox1.Text.ToString());
                }

                this.listBox1.Items.Clear();
            }
            catch
            { }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void richTextBox1_DoubleClick(object sender, EventArgs e)
        {
            //双击时执行。获取双击时抓获的单词
            textBox1.Text  = richTextBox1.SelectedText.ToString().Trim();

            textBox3.Text = richTextBox1.SelectedText.ToString().Trim();
            this.richTextBox1.SelectionColor = Color.Red;
            this.richTextBox1.SelectionFont = new Font("Georgia ", 12, FontStyle.Bold);


            
            string findch = Kenal.findword(textBox1.Text.ToString());

            if(findch=="")
            {
               
                Kenal.getMp3url(Web1, textBox1.Text.ToString());
            }
            else
            {

                textBox2.Text = findch;
                Kenal.play_mp3(findch);
                listBox1.Items.Add(textBox1.Text.ToString().Trim() + "@" + textBox2.Text.ToString().Trim());
            }


            



            

        }


        public void Setsize()//Button btn)
        {
            int height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            int width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            //来获取屏幕的大小或者说分辨率是没有问题的。


            double wi = (double)width;
            double hi = (double)height;

            this.Height = (int)hi;
            this.Width = (int)wi;

            this.Top = 0;
            this.Left = 10;


            this.richTextBox1.Width = this.Width*3/4;
            this.richTextBox1.Height = this.Height / 6;

            this.Web1.Top = this.richTextBox1.Bottom; ;
            this.Web1.Left = this.richTextBox1.Left;
            this.Web1.Width = this.richTextBox1.Width;
            this.Web1.Height = this.richTextBox1.Height*3;
            this.Web1.Visible = true;

            textBox3.Top = this.Web1.Bottom;

            this.listBox1.Height = this.Height * 2 / 4;

            this.bnalltpo.Top = this.listBox1.Bottom;
            this.btnFind.Top = this.listBox1.Bottom;
            this.btnReview.Top = this.listBox1.Bottom;

            this.listBox2.Top = this.btnFind.Bottom+10;
            this.listBox2.Width = this.listBox1.Width;
            this.listBox2.Height = this.Height * 2 / 5;


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                this.listBox1.SelectedIndex = this.listBox1.SelectedIndex + 1;
                
                string[] filenames = this.listBox1.SelectedItem.ToString().Split('@');
                string filename = filenames[0].ToString();

                textBox1.Text = filename;

                if (Kenal.existfile(textBox1.Text.ToString()))
                {
                    textBox1.BackColor = Color.Aqua;
                    Kenal.play_mp3(textBox1.Text.ToString());

                }
                else
                {
                    this.timer1.Enabled = false;
                    Kenal.getMp3url(Web1, textBox1.Text.ToString());
                    this.timer1.Enabled = true;

                }
                                                
            }

            catch
            { }
        }

        private void btnReview_Click(object sender, EventArgs e)
        {
            if (this.timer1.Enabled)
            {
                this.timer1.Enabled = false;
            }
            else
            {
                this.timer1.Enabled = true;
            }
        }

        private void btnSeven_Click(object sender, EventArgs e)
        {
            Kenal.showword(this.comboBox1, this.listBox1, "toefl7");
            label1.Text = listBox1.Items.Count.ToString();
        }

        private void btnG4_Click(object sender, EventArgs e)
        {
            Kenal.showword(this.comboBox1, this.listBox1, "grade4");
            label1.Text = listBox1.Items.Count.ToString();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

                string Myurl = "http://www.iciba.com/" + textBox1.Text .ToString();

                this.Web1 .Navigate(Myurl);

            
        }



    }
}
