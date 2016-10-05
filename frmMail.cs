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


namespace MailAssistant
{
    public partial class frmMail : Form
    {


        public string alp = "a";
        
        public int i = 0;

        string selected = "";
        //AccessHelper ac = new AccessHelper();

        public int iStartofSelection = 0;

        int ipage = 0;

        public frmMail()
        {
            //初始化

            InitializeComponent();
            Setsize();
        }


        private void frmMail_Load(object sender, EventArgs e)
        {


            try
            {
                    //readfrom("a");
                //禁止浏览器 弹出script
                    this.Web1.ScriptErrorsSuppressed = true;
                    this.web2.ScriptErrorsSuppressed = true;
                    this.web3.ScriptErrorsSuppressed = true;
                    this.Web1.Navigate("http://baidu.com");
                    readfrom("a");

            }
            catch(Exception ex)
            {
                ex.ToString();

            }


        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            //窗体SIZE发生变化 时执行下面函数 
            this.Setsize();
 
        }


        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // record current status
            //窗体关闭时执行下面代码

            if (MessageBox.Show("Quit?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            textBox2.Text =  findword(textBox1.Text.ToString());
            
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            i = i + 1;
        }

        

        public string  findword(string word)
        {
            string _worde = "";
            string _wordc = "";
            DataTable dt = null;

            try
            {
                string sql = "select * from word_list";
                sql = "select worde,wordc from word_list where worde like'%" + word.Trim() + "%'";
                dt = SQLiteHelper.ExecuteDataSet(sql, CommandType.Text).Tables[0];

                dataGridView1.DataSource = dt;
                if (dt.Rows.Count > 0)
                {
                    _worde = NewData(dt.Rows[0]["worde"].ToString());
                    _wordc = NewData(dt.Rows[0]["wordc"].ToString());

                }

            }
            catch(Exception )
            {
                
            }
            return _wordc;

 
        }

        public string isToefl(string word)
        {

            string sql = "select word,chinese from toefl_wm where word ='" + word.Trim() + "'";
            string wordc = "";
            try
            {
                DataTable dt = SQLiteHelper.ExecuteDataSet(sql, CommandType.Text).Tables[0];
                {
                    if (dt.Rows.Count > 0)
                    {

                        wordc = " : 托福单词 :" + wordc + NewData(dt.Rows[0]["chinese"].ToString());


                    }

                    sql = "update toefl_wm set count = count+1 where word ='" + word.Trim() + "'";
                    SQLiteHelper.ExecuteNonQuery(sql, CommandType.Text);
                }
            }
            catch(Exception ec)
            {
                ec.ToString();
            }



            return wordc;
        }

        public void showword(string al)
        {
            i = i + 1;
            label1.Text = i.ToString();
            try
            {

                string word = dataGridView1.Rows[i].Cells[0].Value.ToString();
                string means = dataGridView1.Rows[i].Cells[1].Value.ToString();

                textBox1.Text = word;
                textBox2.Text = means;

                getMp3url(word);
                                   
            }
            catch(Exception )
            {

            }
        }


        public void ShowSelectedWord(string selectedword)
        {
            i = i + 1;
            label1.Text = i.ToString();

            try
            {
                string word = selectedword;
                string means = findword(word);

                textBox1.Text = word;
                textBox2.Text = means;


               int ilen = richTextBox1.SelectionStart + this.richTextBox1.SelectionLength;
               //richTextBox4.AppendText(word + means + this.isToefl(word) + "\n");

               getMp3url(word); 
                    //play_mp3(word); 
    
               this.getFromBing(word);


            }
            catch
            { }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //时钟
            showword(alp);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            i = 1;
        }


        private void btnBegin_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            alp = this.comboBox1.Text.ToString();
            readfrom(alp);
                   
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
        }


        public void addtowordList(string worde, string wordc)
        {
            string word = worde;
            string means = wordc;
            try
            {
                string sql = "insert into word_list (worde, wordc) values('" + word + "','" + means + "')";
                SQLiteHelper.ExecuteNonQuery(sql, CommandType.Text);
                textBox2.Text = findword(textBox1.Text.ToString());

            }
            catch (Exception ec)
            {
                //ec.ToString();
            }
 
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            addtowordList(textBox1.Text.ToString(),textBox2.Text.ToString());

        }

        private void richTextBox1_DoubleClick(object sender, EventArgs e)
        {
            //双击时执行。获取双击时抓获的单词
            selected = richTextBox1.SelectedText.ToString();


            this.richTextBox1.SelectionColor = Color.Red;
            this.richTextBox1.SelectionFont = new Font("Georgia ", 12, FontStyle.Bold);
            
            ShowSelectedWord(selected);

        }


        private void richTextBox3_DoubleClick(object sender, EventArgs e)
        {
            selected = richTextBox3.SelectedText.ToString();

            this.richTextBox3.SelectionColor = Color.Red;    //设置颜色
            this.richTextBox3.SelectionFont = new Font("Georgia ", 12, FontStyle.Italic | FontStyle.Bold);//设置字体
            ShowSelectedWord(selected);
            
        }

        public void SetFont(RichTextBox m_TextBox,FontDialog fontDialog)
         {

            //设置字体
             if (fontDialog.ShowDialog() != DialogResult.Cancel)
             {
                 m_TextBox.SelectionColor = fontDialog.Color;
                 m_TextBox.SelectionFont = fontDialog.Font;
             }
             else
             {

//                 m_TextBox.SelectionFont = font;

             }
            
        }

        public void SetFont(RichTextBox m_TextBox, Font font)
        {
            m_TextBox.SelectionFont = font;
 
        }


        public void readfrom(string alp)
        {

            DataTable dt = null;


            string sql = "select word,chinese from toefl_wm where substr(word,1,1) ='" + alp + "'";

            if (alp == "all")
            {
                sql = "select word,chinese from toefl_wm";
 
            }


            dt = SQLiteHelper.ExecuteDataSet(sql, CommandType.Text).Tables[0];

            dataGridView1.DataSource = dt;
            dataGridView1.Show();

            label5.Text = dataGridView1.RowCount.ToString();
            sql = "select id,title from article_list ";
            string sKeyWord = this.textBox1.Text.ToString();
            if ( sKeyWord!= string.Empty)
            {
                sql = sql + " where content like '%" + sKeyWord + "%'";
            }
            //dt = ac.GetDataSet("select title,type from article_list");
            sql = sql + " order by id desc";
            dt = SQLiteHelper.ExecuteDataSet(sql, CommandType.Text).Tables[0];
            dataGridView2.DataSource = dt;
            dataGridView2.Show();

            comboBox4.DataSource = dt;
            comboBox4.ValueMember="title";
            comboBox4.SelectedValue = "title";
            //comboBox4.Show();
            
            
            label4.Text = dataGridView2.Rows.Count.ToString();

            
            
        }

        public void GetFiles(ComboBox cb, string dir)
        {
            try
            {
                cb.Items.Clear();
                string[] files = Directory.GetFiles(dir);//得到文件
                foreach (string file in files)//循环文件
                {
                    string exname = file.Substring(file.LastIndexOf(".") + 1);//得到后缀名
                    // if (".txt|.aspx".IndexOf(file.Substring(file.LastIndexOf(".") + 1)) > -1)//查找.txt .aspx结尾的文件
                    if (".pdf".IndexOf(file.Substring(file.LastIndexOf(".") + 1)) > -1)//如果后缀名为.txt文件
                    {
                        FileInfo fi = new FileInfo(file);//建立FileInfo对象
                        cb.Items.Add(fi.FullName);//把.txt文件全名加人到FileInfo对象
                    }
                }
            }
            catch
            {

            }
        }

        private string openFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            openFileDialog.Filter = "PDF|*.pdf|DOC|*.doc";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fName = openFileDialog.FileName;
                return fName;
            }

            return "";
        }

        public void save_mp3(string filename, string mp3url)
        {
            string spath = System.IO.Directory.GetCurrentDirectory()+"\\Data\\";

            if (!File.Exists(spath + filename + ".mp3"))
            {
                System.Net.CookieContainer cookie = new System.Net.CookieContainer();
                Stream st = HTMLHelper.GetStream(mp3url, cookie);
                StreamWriter sw = new StreamWriter(spath + filename + ".mp3");

                st.CopyTo(sw.BaseStream);
                sw.Flush();
                sw.Close();
            }
        }

        public void play_mp3(string filename)
        {
            string spath = System.IO.Directory.GetCurrentDirectory() ;

            try
            {
                clsMCI mp3player = new clsMCI();
                mp3player.FileName = spath + "\\Data\\" + filename + ".mp3";
                mp3player.play();
            }
            catch(Exception ex)
            {}
        }

        public void getMp3url(string mp3URL)
        {
            string Myurl = "http://www.iciba.com/" + mp3URL;
            Web1.Navigate(Myurl);

        }


        public void getFromBing(string word)
        {
            string Myurl = "http://baidu.com";
                ;
            this.web3.Navigate(Myurl);
            this.web2.Visible = false;
           
        }


        public void spanish(string word)
        {
            string Myurl = "http://dict.hjenglish.com/es/" + word;
            this.Web1.Navigate(Myurl);
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

                string wordc = content.Substring(iChineseStart +17, iChineseEnd-iChineseStart);


                wordc = Regex.Replace(wordc, @"^[ \t]*|[ \t]*$ ", " ");
                iChineseEnd = wordc.IndexOf("</label>", 0);

                if (textBox2.Text.ToString() == "")
                {
                    textBox2.Text = wordc.Substring(8, iChineseEnd - 8);
                }
                if (dataGridView1.RowCount <= 1)
                {
                    addtowordList(textBox1.Text.ToString(), textBox2.Text.ToString());
                }

                int ilen = richTextBox1.SelectionStart + this.richTextBox1.SelectionLength;
                
                richTextBox2.AppendText(textBox1.Text.ToString() + textBox2.Text.ToString() + this.isToefl(textBox1.Text.ToString()) + "\n");



                int iTableStart = content.IndexOf("asplay", 0);
                int iTableEnd = content.IndexOf(".mp3", 0);

                content = content.Substring(iTableStart + 8, iTableEnd - iTableStart - 4);
                string filename = textBox1.Text.ToString();

                if (!existfile(filename))
                {
                    save_mp3(filename, content);
                    
                }
                play_mp3(filename);

                string tmp = null;

                int iStart = this.richTextBox1.SelectionStart;
                int iEnd =  this.richTextBox1.SelectionLength ;
                int iFrom = iStart + iEnd;
                int itotal = this.richTextBox1.Text.Length;

                //string cc = this.richTextBox1.Text.Substring(iFrom, this.richTextBox1.Text.Length-1);
                string cc = this.richTextBox1.SelectedText;
                string aa = this.richTextBox1.Text.Substring(0, iStart);
                string bb = this.richTextBox1.Text.Substring(iStart, iEnd) + "(" + textBox2.Text.ToString() + ")";
                //string cc = this.richTextBox1.Text.Substring(iStart+iEnd,itotal);
                
                string temp = cc.Replace(cc,bb);


                tmp = aa + bb + cc;

                this.richTextBox1.SelectedText = temp;

            }

            catch(Exception ec)
            { ec.ToString();}


        }

        public bool existfile(string filename)
        {

           string spath = System.IO.Directory.GetCurrentDirectory() ;

           string FileName = spath + "\\Data\\" + filename + ".mp3";

           return File.Exists(FileName);
        }

        public void saveFile()
        {
            string title = this.txtTitle.Text.ToString() + DateTime.Now.ToLocalTime(); ;
            string type = "science";
            string words = this.richTextBox2.Text.ToString();
            string rtf = richTextBox1.Rtf;



            try
            {
                string sql = "insert into article_list (title, content,type,words) values('" + title + "','" + rtf + "','" + type + "','" + words + "')";
                SQLiteHelper.ExecuteNonQuery(sql, CommandType.Text);
                MessageBox.Show("Successful Save" + " " + title);
                readfrom("a");
            }

            catch (Exception ec)
            {
                MessageBox.Show(ec.ToString());
            }
        }


        private void btnAddFile_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            readfrom("a");
        }

        public void FilterString(RichTextBox m_TextBox)
        {
            RichTextBox tempRichTextBox = new RichTextBox();
            string tmp = "";
            string richBox = m_TextBox.SelectedText.ToString();

            int len = m_TextBox.SelectionLength;
            int iStart = m_TextBox.SelectionStart;
            for (int i = 0; i < len; i++)
            {
                if ((int)richBox[i] < 127 )//&& (int)richBox[i]>31)
                    {
                        if (richBox[i].ToString() != "'" )   //&& richBox[i].ToString() != "\n")
                        {
                            tempRichTextBox.Text += richBox[i].ToString();
                        }
                        else if (richBox[i].ToString() == "\n")
                        {
                            //tempRichTextBox.Text += " "; /*richBox[i].ToString()*/;

                            MessageBox.Show(richBox[i].ToString());
                        }
                            
                        else

                        {
                            tmp = tmp + richBox[i].ToString();
                        }
                    }

            }

            m_TextBox.SelectedText = tempRichTextBox.Text;
            m_TextBox.Select(iStart, len);

        }

        public string NewArticle(string oldtxt)
        {
            string content = oldtxt.Replace("\n", "");

            content = content.Replace("'s", "s");
            content = content.Replace("\"", "");
            content = content.Replace("“", "");
            content = content.Replace("”", "");
            return content;
        }


        public string NewData(string oldtxt)
        {

            return oldtxt;
        }


        private void btnIn_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int a = dataGridView1.CurrentRow.Index;
            string word = dataGridView1.Rows[a].Cells[0].Value.ToString();
            string means = dataGridView1.Rows[a].Cells[1].Value.ToString().TrimEnd(); ;

            MessageBox.Show(word + ":" + means);
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

            splitContainer1.Left = this.Left;
            splitContainer1.Top = textBox1.Top+textBox1.Height+5;
            splitContainer1.Height = this.Height;//(int)hi;
            splitContainer1.Width = this.Width;// (int)wi;

            tabControl1.Width = splitContainer1.Panel2.Width;
            tabControl1.Height = splitContainer1.Panel2.Height;


            textBox1.Left = this.btnAddFile.Left + this.btnAddFile.Width+10;
            textBox1.Top = this.Top;
            textBox1.Width = tabControl1.Width /3;

            textBox2.Left = textBox1.Left + textBox1.Width + 5;
            textBox2.Top = textBox1.Top;
            textBox2.Width = tabControl1.Width / 2;






            this.richTextBox1.Width = (splitContainer1.Width -splitContainer1.SplitterDistance)/ 2;
            this.richTextBox1.Height = splitContainer1.Panel2.Height*8/10;
            this.richTextBox1.Top = comboBox4.Top +comboBox4.Height;

            this.dataGridView1.Left = 5;
            this.dataGridView1.Top = this.txtTitle.Top;
            this.dataGridView1.Height = splitContainer1.Panel1.Height / 2;
            this.dataGridView1.Width = splitContainer1.Panel1.Width - 25;

            this.dataGridView2.Left = 5;
            this.dataGridView2.Top = this.dataGridView1.Top + this.dataGridView1.Height+10;
            this.dataGridView2.Height = splitContainer1.Panel1.Height / 3;
            this.dataGridView2.Width = splitContainer1.Panel1.Width - 25;

            this.dataGridView2.Visible = false;
            this.Web1.Top = this.richTextBox1.Top;
            this.Web1.Left = this.richTextBox1.Left + this.richTextBox1.Width;
            this.Web1.Width = splitContainer1.Panel2.Width /2-50;
            this.Web1.Height = this.richTextBox1.Height;
            this.Web1.Visible = true;


            this.web2.Visible = false;
            this.web2.Left = this.richTextBox1.Left + this.richTextBox1.Width;
            this.web2.Width = splitContainer1.Panel2.Width / 2-50;
            this.web2.Top = this.Web1.Top+this.Web1.Height+2;
            this.web2.Height = this.splitContainer1.Panel2.Height / 2;

            this.web3.Left = this.richTextBox1.Left + this.richTextBox1.Width;
            this.web3.Width = splitContainer1.Panel2.Width / 2 - 50;
            this.web3.Top = this.Web1.Top + this.Web1.Height + 2;
            this.web3.Height = this.splitContainer1.Panel2.Height / 2;

            //this.web3.Visible = false;

            this.richTextBox2.Top = this.dataGridView1.Top + this.dataGridView1.Height + 10;
            this.richTextBox2.Left = 5;
            this.richTextBox2.Width = splitContainer1.Panel1.Width - 25;
            this.richTextBox2.Height = splitContainer1.Panel1.Height / 3;
            //MessageBox.Show(wi.ToString()+ " " + hi.ToString());

            this.web2.Width=(int)wi*4/5;
            this.web2.Height = (int)hi * 3 / 5;
            this.btnGo.Left = (int)wi / 4;

            this.dataGridView3.Width = splitContainer1.Panel2.Width * 1 /2;
            this.dataGridView3.Height = (int)hi * 3 / 5;
            this.btnDisplay.Top = this.dataGridView3.Top + this.dataGridView3.Height + 10;

            this.dataGridView4.Left = dataGridView3.Left + dataGridView3.Width + 30;
            this.dataGridView4.Height = (int)hi * 3 / 5;
            this.dataGridView4.Top = this.dataGridView3.Top;
            this.dataGridView4.Width = splitContainer1.Panel2.Width * 1 / 2;

            this.btnPdf.Left = (int)wi / 4;

            this.comboBox3.Width = (int)wi * 3 / 5;
            this.comboBox3.Left = richTextBox1.Left;

            this.richTextBox3.Width = this.splitContainer1.Panel2.Width*2/5;
            this.richTextBox3.Height = this.splitContainer1.Panel2.Height * 3 /4;
            //this.richTextBox4.Width = this.splitContainer1.Panel2.Width /7;

            this.axAcroPDF1.Width = this.splitContainer1.Panel2.Width*3/5;
            this.axAcroPDF1.Top = this.richTextBox3.Top ;
            this.axAcroPDF1.Left = this.richTextBox3.Left + this.richTextBox3.Width;
            this.axAcroPDF1.Height = this.richTextBox3.Height;


            this.richTextBox4.Left = this.axAcroPDF1.Left + this.axAcroPDF1.Width;
            this.richTextBox4.Height = this.richTextBox3.Height;

            this.richTextBox4.Width = 1; // this.splitContainer1.Panel2.Width / 7;



            this.btnPdf.Left = this.comboBox5.Left + this.comboBox5.Width;
            this.btnPdf.Top = this.comboBox5.Top;

            this.btnPrev.Top = this.richTextBox3.Top + this.richTextBox3.Height;
            this.btnNextP.Top = this.richTextBox3.Top + this.richTextBox3.Height;
            this.btnFilter.Top = this.richTextBox3.Top + this.richTextBox3.Height;

            btnCh.Top = this.richTextBox3.Top + this.richTextBox3.Height;
            btnCh.Left = btnFilter.Left + btnFilter.Width;
            txtPDfTitle.Top = btnCh.Top;            
            txtPage.Top = this.richTextBox3.Top + this.richTextBox3.Height;
            txtTotal.Top = this.richTextBox3.Top + this.richTextBox3.Height;
            label7.Top = this.richTextBox3.Top + this.richTextBox3.Height;

        }




        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int a = dataGridView2.CurrentRow.Index;
            string title = dataGridView2.Rows[a].Cells[1].Value.ToString();
            displayArticle(title);

        }


        public void displayArticle(string title)
        {
            try
            {

                string sql = "select title,content,type,words from article_list where title='" + title + "'";
                //dt = ac.GetDataSet("select title,type from article_list");
                DataTable dt = SQLiteHelper.ExecuteDataSet(sql, CommandType.Text).Tables[0];

                txtTitle.Text = dt.Rows[0][0].ToString();
                string rtf = dt.Rows[0][1].ToString();
                this.richTextBox1.Rtf = rtf;
                this.richTextBox2.Text = dt.Rows[0][3].ToString();


                //string[] aa = this.richTextBox1.Text.ToString().Split(' ');
                //MessageBox.Show("This article has " + aa.Length.ToString() + "words");

            }
            catch

            { }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            string content =  this.richTextBox1.Rtf.ToString();


            //content = content.Replace("\\par", "");
            //content = content.Replace("fs19", "fs36");
            //content = content.Replace("fs17", "fs36");
            //richTextBox1.Rtf = content;

            this.richTextBox1.TextChanged -= new System.EventHandler(this.richTextBox1_TextChanged);
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            string content = this.richTextBox1.Text.ToString();
            NewArticle(this.richTextBox1.Text.ToString());

            FilterString(this.richTextBox1);
            //SetFont(this.richTextBox1);

        }


        private void btnGo_Click(object sender, EventArgs e)
        {
            //web2.Navigate(tbURL.Text.ToString());
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            //this.GetFiles(this.comboBox5, AppDomain.CurrentDomain.BaseDirectory + "\\PDF\\");
            //this.openFileDialog();

            this.comboBox5.Text = this.openFileDialog();
            string pdfName = this.comboBox5.Text.ToString();
            this.axAcroPDF1.LoadFile(pdfName);
            this.axAcroPDF1.gotoFirstPage();
            this.richTextBox3.Text = "";
            ipage = 1;
            this.txtPage.Text = ipage.ToString();
            this.gotoPage(this.richTextBox3,pdfName, ipage);

            

        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            string sql = "select id,title from article_list ";
            displayDataGridView(this.dataGridView3, sql);

            sql = "select word, chinese,count from toefl_wm where count <> 0 order by count desc LIMIT 1000";

            displayDataGridView(this.dataGridView4, sql);
            
        }


        public void displayDataGridView(DataGridView dgv, string sql)
        {
            //dt = ac.GetDataSet("select title,type from article_list");
            DataTable dt = null;
            dt = SQLiteHelper.ExecuteDataSet(sql, CommandType.Text).Tables[0];
            dgv.DataSource = dt;
            dgv.Show();
        }
        private void dataGridView3_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int a = dataGridView3.CurrentRow.Index;
            string title = dataGridView3.Rows[a].Cells[0].Value.ToString();
            //string type = dataGridView3.Rows[a].Cells[1].Value.ToString().TrimEnd(); ;

            try
            {
                if (MessageBox.Show("Delete this Article?", "Confirm Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    string sql = "delete from article_list where id = '" + title + "'";

                    SQLiteHelper.ExecuteNonQuery(sql, CommandType.Text);
                }
            }
            catch (Exception ec)

            { }

        }
        private void dataGridView4_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int a = dataGridView4.CurrentRow.Index;
            string title = dataGridView4.Rows[a].Cells[0].Value.ToString();
            //string type = dataGridView3.Rows[a].Cells[1].Value.ToString().TrimEnd(); ;

            try
            {
                this.play_mp3(title);
            }
            catch (Exception ec)

            { }
        }
        public void FindKeyWords(RichTextBox m_Rich, string key)
        {

            string content = m_Rich.Text.ToString();
            iStartofSelection = 0;

                while (iStartofSelection < content.Length)
                {
                    int i = content.IndexOf(key, iStartofSelection);
                    if (i < 0)
                    {
                        return;
                    }
                    else
                    {
                        
                        m_Rich.SelectionStart = i;
                        m_Rich.SelectionLength =key.Length+1;
                        m_Rich.SelectionColor = Color.Red;
                        iStartofSelection = i+ m_Rich.SelectionLength;
                        
                    }
                }
            
                
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            
            FindKeyWords(this.richTextBox1, this.textBox1.Text.ToString());
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string title = comboBox4.Text.ToString();
            this.displayArticle(title);

        }
        public string ExtractTextFromPDFPage(string pdfFile, int pageNumber)
        {
            if (pdfFile == "")
            { return ""; }
            PdfReader reader = new PdfReader(pdfFile); 
            int n = reader.NumberOfPages;
            txtTotal.Text = n.ToString();
            string text = ""; 
            try
            {


                if (pageNumber <= 0)
                {
                    return "没有了";
                }

                else if (pageNumber > n)
                {
                    return "没有了";
                }
                else
                {
                    text = PdfTextExtractor.GetTextFromPage(reader, pageNumber);
                }
            }
            
            
            catch { }
            return text;
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            //ipage = ipage - 1;  // int.Parse(textBox3.Text.ToString());
            this.axAcroPDF1.gotoPreviousPage();

            ipage = int.Parse(txtPage.Text.ToString());
            string ibook = this.comboBox5.Text.ToString();
            string aa = ExtractTextFromPDFPage(ibook, ipage);
            richTextBox3.Text = aa;

            this.txtPage.Text = ipage.ToString();
            this.txtPage.Text = (int.Parse(ipage.ToString()) - 1).ToString();
        }
        private void btnNextP_Click(object sender, EventArgs e)
        {
            //ipage = ipage + 1;  // int.Parse(textBox3.Text.ToString());
            this.axAcroPDF1.gotoNextPage();
            ipage = int.Parse(txtPage.Text.ToString());
            string ibook = this.comboBox5.Text.ToString();
            string aa = ExtractTextFromPDFPage(ibook, ipage);
            richTextBox3.Text = aa;

            this.txtPage.Text = (int.Parse(ipage.ToString())+1).ToString() ;
        }

        public void gotoPage(RichTextBox rtb, string pdfName,int ipage)
        {

            string aa = ExtractTextFromPDFPage(pdfName, ipage);
            rtb.Text = aa;
        }
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.richTextBox3.Text = "";
            ipage = 1;
            string pdfName = this.comboBox5.Text.ToString();
            this.txtPage.Text = ipage.ToString();
            this.gotoPage(this.richTextBox3,pdfName, ipage);
            this.axAcroPDF1.LoadFile(pdfName);
            this.axAcroPDF1.gotoFirstPage();
        }

        public void LoadPdfFile(string pdfFileName)
        {
            
         }
        private void btnFilter_Click(object sender, EventArgs e)
        {

            string content = this.richTextBox3.Text.ToString();
            NewArticle(this.richTextBox3.Text.ToString());

            FilterString(this.richTextBox3);
            //SetFont(this.richTextBox3);
        }
        private void btnCh_Click(object sender, EventArgs e)
        {
            PdfMakerTool pdfhelper = new PdfMakerTool();

            if (txtPDfTitle.Text.ToString() != "")
            {
                string title = AppDomain.CurrentDomain.BaseDirectory + "\\pdf\\" + txtPDfTitle.Text.ToString() + ".pdf"; ;

                pdfhelper.ExportPdfTable(this.richTextBox3, title);
            }
            
            
            //displayToeflWord(this.richTextBox3, this.richTextBox4);

        }
        public void displayToeflWord(RichTextBox rtbin, RichTextBox rtbout)
        {
            string[] aa = new string[] { };
            aa = rtbin.Text.ToString().Split(' ');


            for (int i = 0; i < aa.Length; i++)
            {
                if (aa[i].Trim() != "")
                {
                    if (!isExistString(aa, aa[i]))
                    {
                        string toefl = this.isToefl(aa[i]);
                        if (toefl != "")
                        {
                            rtbout.Text += aa[i] + toefl + "\n";
                        }
                    }

                }
            }

        }
        private bool isExistString(string[] aa ,string word)
        {
            bool b = false;
            int icount = 0;
            for (int i=0;i<aa.Length;i++)
            {
                
                if (aa[i] == word)
                {
                    icount =icount +1;
                }
            }

            if (icount >= 2)
            {
                b = true;
                return b;
            }
            else
            {
                b = false;
                return b;
            }

            return b;
                        

        }
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            web2.Navigate(this.comboBox6.SelectedItem.ToString());
        }

        private void tmSelectFont_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog1 = new FontDialog();
            fontDialog1.ShowColor = true;
            this.SetFont(this.richTextBox1, fontDialog1);
        }

        private void tmFilter_Click(object sender, EventArgs e)
        {
            this.FilterString(this.richTextBox1);
            //Font font = new Font("Couier", 14, FontStyle.Regular | FontStyle.Regular);
            //this.SetFont(this.richTextBox1, font);
        }

        private void tmDefaultFont_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();

                listBox1.Items.Add(this.richTextBox1.Font.Name.ToString());
                listBox2.Items.Add(this.richTextBox1.ForeColor.Name.ToString());
                this.getInstalledFont(this.listBox1);
                this.getColor(this.listBox2);

                listBox1.SelectedIndex = 0;
                listBox2.SelectedIndex = 0;
                listBox3.SelectedIndex = 0;
                this.panel1.Visible = true;
            }

            catch(Exception ec)
            {
                MessageBox.Show(ec.ToString());

            }

        }

        private void tmSaveFile_Click(object sender, EventArgs e)
        {
            this.saveFile();
        }

        public void SelectAll(RichTextBox rtb)
        {
            rtb.Select(0, rtb.Rtf.Length);
        }
        private void tmSelectAll_Click(object sender, EventArgs e)
        {
            this.SelectAll(this.richTextBox1);
        }

        public void getInstalledFont(ListBox listbox)
        {
            InstalledFontCollection MyFont = new InstalledFontCollection();
            FontFamily[] MyFontFamilies = MyFont.Families;
            //listBox1.Items.Clear();
            int Count = MyFontFamilies.Length;
            for (int i = 0; i < Count; i++)
            {
                string FontName =  MyFontFamilies[i].Name ;
                listbox.Items.Add(FontName);
            }

               
        }

        public void getColor(ListBox listbox)
        {
            //listbox.Items.Clear();
          
            Array colors = System.Enum.GetValues( typeof(KnownColor) );
            foreach (object colorName in colors)
            {
                listbox.Items.Add(colorName);
            }
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Font font = new Font(this.listBox1.SelectedItem.ToString(), float.Parse(this.listBox3.SelectedItem.ToString()), FontStyle.Regular);
                this.SetFont(this.richTextBox1, font);
            }
            catch
            { }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            
            Font font = new Font(this.listBox1.SelectedItem.ToString(), float.Parse(this.listBox3.SelectedItem.ToString()), FontStyle.Regular);
            this.SetFont(this.richTextBox1, font);
            this.richTextBox1.SelectionColor = Color.FromName(this.listBox2.SelectedItem.ToString());
            this.panel1.Visible = false;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = false;

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.richTextBox1.SelectionColor = Color.FromName(this.listBox2.SelectedItem.ToString());

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Font font = new Font(this.listBox1.SelectedItem.ToString(), float.Parse(this.listBox3.SelectedItem.ToString()), FontStyle.Regular);
                this.SetFont(this.richTextBox1, font);
            }
            catch
            { }
        }














    }
}
