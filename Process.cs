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
    public partial class Process : Form
    {
        public Process()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {

            try
            {
                string[] TotalLines = this.richTextBox1.Lines;

                for (int j = 0; j < TotalLines.Length; j++)
                {
                    if (TotalLines[j] != "" && TotalLines[j].IndexOf("/")>0)
                    {
                        string[] words = TotalLines[j].Split('/');

                        if (words.Length > 0)

                        {
                            int k = words.Length;
                            string worde = words[0];
                            string wordc = words[2];

                            if ((worde != "" && wordc != ""))
                            {

                                string sql = "insert into tbl_grade4(worde,wordc) values('" + worde + "','" + wordc + "')";
                                SQLiteHelper.ExecuteNonQuery(sql, CommandType.Text);
                            }
                        }
                    }
                }

                Kenal.showword(this.comboBox1, this.listBox1, "grade4");
                label1.Text = listBox1.Items.Count.ToString();
            }
                catch (Exception ec)
                {
                    MessageBox.Show(ec.ToString());
                }
            


        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    this.listBox1.SelectedIndex = i;
                    string[] words = this.listBox1.SelectedItem.ToString().Split('@');
                    string word = words[0].ToString();

                    textBox1.Text = words[0].ToString();

                    string sql = "select word,chinese from toefl_wm where substr(word,1,length(word)-1) like '%" + words[0].ToString() + "%'";
                    DataTable dt = null;
                    dt = SQLiteHelper.ExecuteDataSet(sql, CommandType.Text).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        textBox2.Text = dt.Rows[0]["chinese"].ToString();
                    }
                    else
                    { textBox2.Text = ""; }

                    this.richTextBox1.Text = this.richTextBox1.Text + textBox2.Text + "\n";
                }
            }

            catch
            { }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            

        }

        private void Process_Load(object sender, EventArgs e)
        {
            this.web1.ScriptErrorsSuppressed = true;
        }


        private void web1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (web1.ReadyState != WebBrowserReadyState.Complete)
                return;

            if (e.Url.ToString() != web1.Url.ToString())
                return;

            string content = this.web1.DocumentText.ToString();



            this.timer1.Enabled = false;
            try
            {

                this.web1.ScriptErrorsSuppressed = true;


                int iChineseStart = content.IndexOf("label_list");
                int iChineseEnd = content.IndexOf("</label>", iChineseStart);

                string wordc = content.Substring(iChineseStart + 17, iChineseEnd - iChineseStart);


                wordc = Regex.Replace(wordc, @"^[ \t]*|[ \t]*$ ", " ");
                iChineseEnd = wordc.IndexOf("</label>", 0);


                this.textBox2.Text = wordc.Substring(8, iChineseEnd - 8);


                textBox1.BackColor = Color.Red;

                richTextBox1.Text = richTextBox1.Text + textBox1.Text + "@" + textBox1.Text + "\n";


            }

            catch (Exception ec)
            { ec.ToString(); }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
