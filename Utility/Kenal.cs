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


namespace Toefl.Utility
{
     static class Kenal
    {
        //return Chinese meaning 
         public static string findword(string word)
        {
            string _worde = "";
            string _wordc = "";
            DataTable dt = null;

            try
            {
                string sql = "select * from word_list";
                sql = "select word,chinese from toefl_wm where word ='" + word.Trim() + "'";
                dt = SQLiteHelper.ExecuteDataSet(sql, CommandType.Text).Tables[0];


                if (dt.Rows.Count > 0)
                {
                    _worde = dt.Rows[0]["word"].ToString();
                    _wordc = dt.Rows[0]["chinese"].ToString();

                }

            }
            catch (Exception)
            {

            }
            return _wordc;


        }

        //return if is a Toefl word 
         public static string isToefl(string word)
        {

            string sql = "select word,chinese from toefl_wm where word ='" + word.Trim() + "'";
            string wordc = "";
            try
            {
                DataTable dt = SQLiteHelper.ExecuteDataSet(sql, CommandType.Text).Tables[0];
                {
                    if (dt.Rows.Count > 0)
                    {

                        wordc = " : 托福单词 :" + wordc + dt.Rows[0]["chinese"].ToString();


                    }

                    sql = "update toefl_wm set count = count+1 where word ='" + word.Trim() + "'";
                    SQLiteHelper.ExecuteNonQuery(sql, CommandType.Text);
                }
            }
            catch (Exception ec)
            {
                ec.ToString();
            }



            return wordc;
        }

         [System.Runtime.InteropServices.DllImport("kernel32.dll")]
         public static extern void Sleep(int Milliseconds);


         public static void showword(ComboBox cbo , ListBox lst,string stype)
        {
            DataTable dt = null; 
            
            string sql = "select worde,wordc from word_list ";

            if (stype == "general")
            {
                sql = "select worde,wordc from word_list where substr(worde,1,1) ='" + cbo.SelectedItem.ToString() + "'";
                sql = "select worde,wordc from word_list order by wordc";
            }

            if (stype == "toefl")
            {
                sql = "select word,chinese from toefl_wm where substr(word,1,1) ='" + cbo.SelectedItem.ToString() + "'";

                sql = "select word,chinese from toefl_wm order by chinese ";  //where substr(word,1,1) ='" + cbo.SelectedItem.ToString() + "'";
            }

            if (stype == "sat")
            {
                sql = "select word,chinese from toefl_wm";
            }


            if (stype == "toefl7")
            {
                sql = "select worde,wordc from word_list where substr(worde,1,1) ='" + cbo.SelectedItem.ToString() + "'";
                sql = "select worde,wordc from tbl_toefl_sevendays ";
            }

            if (stype == "grade4")
            {
                sql = "select worde,wordc from word_list where substr(worde,1,1) ='" + cbo.SelectedItem.ToString() + "'";
                sql = "select worde,wordc from tbl_grade4 order by wordc ";
            }


            lst.Items.Clear();
            dt = SQLiteHelper.ExecuteDataSet(sql, CommandType.Text).Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                lst.Items.Add(dt.Rows[i][0].ToString()+ "@" + dt.Rows[i][1].ToString());

            }






            //sql = "select word,chinese from toefl_wm ";

            //dt = SQLiteHelper.ExecuteDataSet(sql, CommandType.Text).Tables[0];

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    lst.Items.Add(dt.Rows[i][0].ToString() + " " + dt.Rows[i][1].ToString());
            //}

        }

         public static void displayArticle(RichTextBox rchbx, string title)
         {
             try
             {

                 string sql = "select title,content,type,words from article_list where title='" + title + "'";
                 DataTable dt = SQLiteHelper.ExecuteDataSet(sql, CommandType.Text).Tables[0];
                 if (dt.Rows.Count > 0)
                 {
                     string rtf = dt.Rows[0][1].ToString();
                     rchbx.Rtf = rtf;
                 }

             }
             catch

             { }
         }

         public static void addtowordList(string worde, string wordc)
        {
            string word = worde;
            string means = wordc;
            try
            {
                string sql = "insert into word_list (worde, wordc) values('" + word + "','" + means + "')";
                SQLiteHelper.ExecuteNonQuery(sql, CommandType.Text);
                
            }
            catch (Exception ec)
            {
                //ec.ToString();
            }

        }

         public static void ShowSelectedWord(WebBrowser web,string selectedword)
         {


             try
             {
                 string word = selectedword;
                 
                 getMp3url(web,word);
                 

             }
             catch
             { }
         }








         public static void getMp3url(WebBrowser web ,string mp3URL)
         {
             string Myurl = "http://www.iciba.com/" + mp3URL;
                         
             web.Navigate(Myurl);

         }



        public static void play_mp3(string filename)
        {
            string spath = System.IO.Directory.GetCurrentDirectory();

            try
            {
                clsMCI mp3player = new clsMCI();
                mp3player.FileName = spath + "\\Data\\" + filename + ".mp3";

                mp3player.play();
            }
            catch (Exception ex)
            { }
        }



         public static void save_mp3(string filename, string mp3url)
         {
             try
             {
                 string spath = System.IO.Directory.GetCurrentDirectory() + "\\Data\\";

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

             catch
             { }
         }

         public static bool existfile(string filename)
         {

             string spath = System.IO.Directory.GetCurrentDirectory();

             string FileName = spath + "\\Data\\" + filename + ".mp3";

             return File.Exists(FileName);
         }


         public static void ExportToExcel(System.Data.DataSet ds, string path)
         {

             StreamWriter sw = null;
             try
             {
                 long totalCount = ds.Tables[0].Rows.Count;
                 sw = new StreamWriter(path, false, Encoding.Unicode);
                 StringBuilder sb = new StringBuilder();
                 for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                 {
                     sb.Append(ds.Tables[0].Columns[i].ColumnName + "\t");
                 }
                 sb.Append(Environment.NewLine);
                 for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                 {
                     for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                     {
                         sb.Append(ds.Tables[0].Rows[i][j].ToString() + "\t");
                     }
                     sb.Append(Environment.NewLine);
                 }
                 sw.Write(sb.ToString());
                 sw.Flush();
             }
             catch (IOException ioe)
             {
                 throw ioe;
             }
             finally
             {
                 if (sw != null)
                 {
                     sw.Close();
                 }
             }
         }

         public static void ExportToExcel(DataGridView dv, string path)
         {

             StreamWriter sw = null;
             try
             {
                 long totalCount = dv.RowCount-1;
                 sw = new StreamWriter(path, false, Encoding.Unicode);
                 StringBuilder sb = new StringBuilder();
                 for (int i = 0; i < dv.ColumnCount; i++)
                 {
                     sb.Append(dv.Columns[i].Name+ "\t");
                 }
                 sb.Append(Environment.NewLine);
                 for (int i = 0; i < dv.RowCount-2; i++)
                 {
                     for (int j = 0; j < dv.Columns.Count; j++)
                     {
                         sb.Append(dv.Rows[i].Cells[j].Value.ToString() + "\t");
                     }
                     sb.Append(Environment.NewLine);
                 }
                 sw.Write(sb.ToString());

                 sw.Flush();
             }
             catch (IOException ioe)
             {
                 throw ioe;
             }
             finally
             {
                 if (sw != null)
                 {
                     sw.Close();
                 }
             }
         }

    }
}
