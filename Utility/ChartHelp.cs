using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using UnileverCAS.UnileverFun;
using System.Data.SqlClient;
using System.Drawing;

namespace UnileverCAS.UnileverFun

{
    public class ChartHelp 
    {

        private SqlConnection Conn = new SqlConnection();
        public void initial_chart(Chart ch)
        {
            ch.Visible = true;
            ch.Width = 760;
            ch.Height = 480;
            ch.BackGradientEndColor = Color.Cyan;
            ch.BorderLineColor = Color.Green;
            ch.BorderLineWidth = 0;
            ch.Palette = ChartColorPalette.Dundas;
            ch.ImageType = ChartImageType.Jpeg;


            ch.ChartAreas["Default"].Area3DStyle.Enable3D = true;
            ch.ChartAreas["Default"].AxisX.Interval = 1;
            ch.ChartAreas["Default"].AxisX.LabelStyle.OffsetLabels = false;
            ch.ChartAreas["Default"].Position.X = 4;
            ch.ChartAreas["Default"].Position.Y = 4;
            ch.ChartAreas["Default"].Position.Width = 70;
            ch.ChartAreas["Default"].Position.Height = 44;


            ch.Series.Clear();
            ch.Series.Add("Default");
            ch.Titles.Clear();
            //ch.Titles.Add("test");

            ch.Series["Default"].Type = SeriesChartType.Pie;
            ch.Series["Default"].SmartLabels.Enabled = true;
            ch.Series["Default"]["PieLabelStyle"] = "OutSide";
            ch.Series["Default"]["PieLineArrowType"] = "SharpTriangle";
            ch.Series["Default"]["PieLineArrowSize"] = "1";
            //ch.Series["Deafult"].ChartArea = "Default";

            ch.Series["Default"].ChartArea = "Default";
        }



        public void Bindinfo(Chart ch)
        {



            foreach (DataPoint point in ch.Series["Default"].Points)
            {
                if (point.AxisLabel.ToString() != "")
                {
                    point.Label = point.AxisLabel.ToString() + ",  " + point.YValues[0].ToString() + "(个)" + ",  " + "#PERCENT{P1}";
                }

            }



        }




        public void ini_ch_chartarea(Chart ch, string strArea)
        {

            ch.Visible = true;
            if (ch.ChartAreas.Count != 0)
            {
                ch.ChartAreas.RemoveAt(0);
                ch.ChartAreas.Add(strArea);

            }
            else
            { ch.ChartAreas.Add(strArea); }

            ch.ChartAreas[strArea].Area3DStyle.Enable3D = false;

        }

        public void ini_ch_series(Chart ch, string str_series)
        {
            ch.Series[str_series].Type = SeriesChartType.StackedColumn;
            ch.Series[str_series].EmptyPointStyle.BorderWidth = 1;
            ch.Series[str_series].EmptyPointStyle.BorderStyle = ChartDashStyle.DashDotDot;
            ch.Series[str_series].EmptyPointStyle.MarkerColor = Color.FromArgb(64, 64, 64);
            ch.Series[str_series].SmartLabels.Enabled = true;
        }


        public void ini_ch_line(Chart ch, string seriesName)
        {


            ch.Visible = true;
            //ch.Width = int.Parse(tbWidth.Trim());
            //ch.Height = int.Parse(tbHeight.Trim());

            //ch.ChartAreas.Add("Default");
            ch.Series[seriesName].Type = SeriesChartType.Line;
            ch.Series[seriesName].EmptyPointStyle.BorderWidth = 1;
            ch.Series[seriesName].EmptyPointStyle.BorderStyle = ChartDashStyle.DashDotDot;
            ch.Series[seriesName].EmptyPointStyle.MarkerColor = Color.FromArgb(64, 64, 64);
            ch.Series[seriesName].SmartLabels.Enabled = true;
            ch.ChartAreas["Default"].Area3DStyle.Enable3D = false;
            ch.Series[seriesName].ShowLabelAsValue = true;
        }

        public void draw_Chart_line(Chart ch, string strQuestion, string strQ_id, string datestart, string dateend)
        {
            string seriesName = "";
            string ym = "";
            int j = 1;



            long ldiff = CMFun.DateTimeManger.DateDiff(CMFun.DateInterval.Month, Convert.ToDateTime(datestart), Convert.ToDateTime(dateend));

            //ch.Series.Clear();

            seriesName = strQuestion;
            ch.Series.Add(seriesName);
            ch.Visible = true;
            //ch.Width = int.Parse(tbWidth.Trim());
            //ch.Height = int.Parse(tbHeight.Trim());

            //ch.ChartAreas.Add("Default");
            ch.Series[seriesName].Type = SeriesChartType.Line;
            ch.Series[seriesName].EmptyPointStyle.BorderWidth = 1;
            ch.Series[seriesName].EmptyPointStyle.BorderStyle = ChartDashStyle.DashDotDot;
            ch.Series[seriesName].EmptyPointStyle.MarkerColor = Color.FromArgb(64, 64, 64);
            ch.Series[seriesName].SmartLabels.Enabled = true;

            ch.ChartAreas.Add("ChartArea1");
            ch.ChartAreas["ChartArea1"].Position.X = 4;
            ch.ChartAreas["ChartArea1"].Position.Y = 44;
            ch.ChartAreas["ChartArea1"].Position.Width = 74;
            ch.ChartAreas["ChartArea1"].Position.Height = 40;
            //ch.ChartAreas["ChartArea1"].Position.Auto = true;
            ch.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            ch.ChartAreas["ChartArea1"].AxisX.LabelsAutoFit = true;
            ch.ChartAreas["ChartArea1"].AxisY.LabelsAutoFit = true;
            ch.ChartAreas["ChartArea1"].AxisX.LabelsAutoFitStyle = Dundas.Charting.WebControl.LabelsAutoFitStyle.WordWrap;

            string sqlt = "";

            for (int jj = 0; jj < ldiff; jj++)
            {
                ym = Convert.ToDateTime(datestart).AddMonths(jj).Year.ToString() + "-" + Convert.ToDateTime(datestart).AddMonths(jj).Month.ToString();

                if (strQ_id == "103")
                {
                    sqlt = "select num,date1,answer from survey_visit_result where date1 in ('" + ym + "')";

                }
                else
                {
                    sqlt = "select num,question,q_id,date1 from survey_score_1 where q_id = " + "'" + strQ_id + "'" + " and date1 in ('" + ym + "')";
                }
                DataTable dt = CMFun.GetSqlDataTable(Conn, sqlt);

                if (dt.Rows.Count == 0)
                {
                    ch.Series[seriesName].Points.AddXY(ym, 0);
                }
                else
                {
                    ch.Series[seriesName].Points.AddXY(dt.Rows[0]["date1"].ToString(), float.Parse(dt.Rows[0]["num"].ToString()));
                }



            }

            ch.Series[seriesName].ChartArea = "ChartArea1";

            //foreach (DataPoint dp in ch.Series[seriesName].Points)
            //{

            //    if (dp.YValues[0] == 0)
            //    {
            //        dp.Empty = true;
            //    }
            //}
            //TableHelper = new ChartDataTableHelper();
            //TableHelper.Initialize(ch);
        }

        public void draw_chart_visit(Chart ch, string strQuestion, string strQ_id)
        {
            string seriesName = "";
            string ym = "";
            int j = 1;

            string datestart = "";
            string dateend = "";


            long ldiff = CMFun.DateTimeManger.DateDiff(CMFun.DateInterval.Month, Convert.ToDateTime(datestart), Convert.ToDateTime(dateend));

            //ch.Series.Clear();

            seriesName = strQuestion;
            ch.Series.Add(seriesName);
            ch.Visible = true;
            //ch.Width = int.Parse(tbWidth.Trim());
            //ch.Height = int.Parse(tbHeight.Trim());

            //ch.ChartAreas.Add("Default");
            ch.Series[seriesName].Type = SeriesChartType.Line;
            ch.Series[seriesName].EmptyPointStyle.BorderWidth = 1;
            ch.Series[seriesName].EmptyPointStyle.BorderStyle = ChartDashStyle.DashDotDot;
            ch.Series[seriesName].EmptyPointStyle.MarkerColor = Color.FromArgb(64, 64, 64);
            ch.Series[seriesName].SmartLabels.Enabled = true;

            ch.ChartAreas.Add("ChartArea1");
            ch.ChartAreas["ChartArea1"].Position.X = 4;
            ch.ChartAreas["ChartArea1"].Position.Y = 44;
            ch.ChartAreas["ChartArea1"].Position.Width = 74;
            ch.ChartAreas["ChartArea1"].Position.Height = 40;
            //ch.ChartAreas["ChartArea1"].Position.Auto = true;
            ch.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            ch.ChartAreas["ChartArea1"].AxisX.LabelsAutoFit = true;
            ch.ChartAreas["ChartArea1"].AxisY.LabelsAutoFit = true;
            ch.ChartAreas["ChartArea1"].AxisX.LabelsAutoFitStyle = Dundas.Charting.WebControl.LabelsAutoFitStyle.WordWrap;

            string sqlt1 = "";
            string sqlt2 = "";

            for (int jj = 0; jj < ldiff; jj++)
            {
                ym = Convert.ToDateTime(datestart).AddMonths(jj).Year.ToString() + "-" + Convert.ToDateTime(datestart).AddMonths(jj).Month.ToString();


                if (strQ_id == "184")
                {
                    sqlt1 = "select num,date1 from survey_lady_score1 where o_id = 1003  and  date1 in ('" + ym + "')";
                    sqlt2 = "select num from survey_lady_score1 where o_id in (1139,1140,1141,1142,1143,1144 ) and date1 in ('" + ym + "')";
                }
                //if (strQ_id == "184")
                //{
                //    sqlt1 = "select num,date1 from survey_sales_score1 where o_id = 1004  and  date1 in ('" + ym + "')";
                //    sqlt2 = "select num from survey_sales_score1 where o_id in (1139,1140,1141,1142,1143,1144  ) and date1 in ('" + ym + "')";
                //}
                //if (strQ_id == "184")
                //{
                //    sqlt1 = "select num,date1 from survey_shvisit_score1 where o_id = 1005  and  date1 in ('" + ym + "')";
                //    sqlt2 = "select num from survey_shvisit_score1 where o_id in (1139,1140,1141,1142,1143,1144 ) and date1 in ('" + ym + "')";
                //}


                DataTable dt1 = CMFun.GetSqlDataTable(Conn, sqlt1);
                DataTable dt2 = CMFun.GetSqlDataTable(Conn, sqlt2);
                float fscore = 0;

                if (dt1.Rows.Count == 0)
                {
                    ch.Series[seriesName].Points.AddXY(ym, 0);
                }
                else
                {

                    if (dt2.Rows.Count != 0)
                    {
                        for (j = 0; j < dt2.Rows.Count; j++)
                        {
                            if (j == 0)
                            { fscore = float.Parse(dt2.Rows[j]["num"].ToString()) * 500; }
                            if (j == 1)
                            { fscore += float.Parse(dt2.Rows[j]["num"].ToString()) * 400; }
                            if (j == 2)
                            { fscore += float.Parse(dt2.Rows[j]["num"].ToString()) * 300; }
                            if (j == 3)
                            { fscore += float.Parse(dt2.Rows[j]["num"].ToString()) * 200; }
                            if (j == 4)
                            { fscore += float.Parse(dt2.Rows[j]["num"].ToString()) * 100; }
                        }
                    }
                    ch.Series[seriesName].Points.AddXY(dt1.Rows[0]["date1"].ToString(), fscore / float.Parse(dt1.Rows[0]["num"].ToString()));
                }



            }

            ch.Series[seriesName].ChartArea = "ChartArea1";

            //foreach (DataPoint dp in ch.Series[seriesName].Points)
            //{

            //    if (dp.YValues[0] == 0)
            //    {
            //        dp.Empty = true;
            //    }
            //}
            //TableHelper = new ChartDataTableHelper();
            //TableHelper.Initialize(ch);
        }

        public void draw_Chart_column(Chart ch, string strQuestion, string strQ_id, string datestart, string dateend)
        {

            //tbHeight = "450";
            //tbWidth = "700";

            string ym = "";
            string seriesName = "";
            string sqldate = "";

            //ch.Width = int.Parse(tbWidth.Trim());
            //ch.Height = int.Parse(tbHeight.Trim());


            ch.ChartAreas.Add("ChartArea1");

            long ldiff = CMFun.DateTimeManger.DateDiff(CMFun.DateInterval.Month, Convert.ToDateTime(datestart), Convert.ToDateTime(dateend));

            string sqlu = "";

            sqlu = "select distinct subject,o_id from question_answer where q_id =  " + "'" + strQ_id + "'" + " order by subject desc";


            DataTable dt1 = CMFun.GetSqlDataTable(Conn, sqlu);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {


                seriesName = dt1.Rows[i]["subject"].ToString();
                ch.Series.Add(seriesName);


                ch.ChartAreas["ChartArea1"].Position.X = 4;
                ch.ChartAreas["ChartArea1"].Position.Y = 44;
                ch.ChartAreas["ChartArea1"].Position.Width = 70;
                ch.ChartAreas["ChartArea1"].Position.Height = 40;
                ch.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                ch.ChartAreas["ChartArea1"].AxisX.LabelsAutoFit = true;
                ch.ChartAreas["ChartArea1"].AxisY.LabelsAutoFit = true;
                ch.ChartAreas["ChartArea1"].AxisX.LabelsAutoFitStyle = Dundas.Charting.WebControl.LabelsAutoFitStyle.WordWrap;
                ch.Series[seriesName].Type = SeriesChartType.StackedColumn100;
                ch.Series[seriesName].EmptyPointStyle.BorderWidth = 1;
                ch.Series[seriesName].EmptyPointStyle.BorderStyle = ChartDashStyle.DashDotDot;
                ch.Series[seriesName].EmptyPointStyle.MarkerColor = Color.FromArgb(64, 64, 64);
                ch.Series[seriesName].SmartLabels.Enabled = true;


                for (int jj = 0; jj < ldiff; jj++)
                {
                    ym = Convert.ToDateTime(datestart).AddMonths(jj).Year.ToString() + "-" + Convert.ToDateTime(datestart).AddMonths(jj).Month.ToString();


                    sqldate = "select num,date1 from survey_score where q_id =  " + "'" + strQ_id + "'" + " and answer  =  " + "'" + seriesName + "' " + " and date1 in ('" + ym + "')" + " order by date1";


                    DataTable dt = CMFun.GetSqlDataTable(Conn, sqldate);


                    if (dt.Rows.Count == 0)
                    {
                        ch.Series[seriesName].Points.AddXY(ym, 0);
                    }
                    else
                    {

                        ch.Series[seriesName].Points.AddXY(dt.Rows[0]["date1"].ToString(), float.Parse(dt.Rows[0]["num"].ToString()));


                    }

                    ch.Series[seriesName].ShowLabelAsValue = true;

                    ch.Series[seriesName].ChartArea = "ChartArea1";


                    //TableHelper = new ChartDataTableHelper();
                    //TableHelper.Initialize(ch);
                    //ch.Series[seriesName].ChartArea = "ChartArea1";

                }





            }
        }

        public void draw_Chart_pie_jiedai(Chart ch, string strQuestion, string strOid, string datestart, string dateend)
        {
            CMFun.SqlConnOpenAgain(Conn);

            //tbHeight = "480";
            //tbWidth = "760";

            DateTime XMin = Convert.ToDateTime(datestart);
            DateTime XMax = Convert.ToDateTime(dateend);

            string strMinYear = XMin.Year.ToString();
            string strMinMonth = XMin.Month.ToString();
            string strMaxYear = XMax.Year.ToString();
            string strMaxMonth = XMax.Month.ToString();


            //int YValTemp = 0;
            DateTime XMin1 = XMax.AddDays(1);
            string sqldateMin = XMin1.AddMonths(-1).ToString("yyyy-MM-dd");
            string sqldateMax = XMax.ToString("yyyy-MM-dd") + " 23:59:59";

            string sql = "select count(o_id) as num,o_id,q_id,credit,answer,question from survey_result  where question like '%工作人员接待过%'  ";
            sql = sql + " and i_id in (select i_id from survey_result where o_id = " + strOid + ")";

            sql = sql + "   and answer_date >= " + "'" + sqldateMin + "'" + " and answer_date<= " + "'" + sqldateMax + "'" + " group by o_id,q_id,credit,answer,question order by q_id ";
            DataTable dt = SqlHelper.ExecuteDataset(CMFun.GetConnectString(), CommandType.Text, sql).Tables[0];


            ch.DataSource = dt;

            DataView dv = new DataView(dt);
            initial_chart(ch);

            ch.Titles.Add(strQuestion);
            for (int j = 0; j < dv.Count; j++)
            {
                string XVal = dv[j]["answer"].ToString();
                string strO_id = dv[j]["o_id"].ToString();
                string strNum = dv[j]["num"].ToString();
                int YVal = Convert.ToInt32(strNum);



                DataPoint dp = new DataPoint();
                ch.Series["Default"].Points.AddXY(XVal, YVal);

            }

            Bindinfo(ch);


        }

        public void draw_Chart_pie(Chart ch, string strQuestion, string strQ_id, string datestart ,string dateend) 
        {
            CMFun.SqlConnOpenAgain(Conn);
            
            DateTime XMin = Convert.ToDateTime(datestart);
            DateTime XMax = Convert.ToDateTime(dateend);

            string strMinYear = XMin.Year.ToString();
            string strMinMonth = XMin.Month.ToString();
            string strMaxYear = XMax.Year.ToString();
            string strMaxMonth = XMax.Month.ToString();


            //int YValTemp = 0;
            DateTime XMin1 = XMax.AddDays(1);
            string sqldateMin = XMin1.AddMonths(-1).ToString("yyyy-MM-dd");
            string sqldateMax = XMax.ToString("yyyy-MM-dd") + " 23:59:59";

            sqldateMin = "2011-11-31";

            string sql = "select count(o_id) as num,o_id,q_id,credit,answer,question from survey_result  where  q_id in(" + strQ_id + ")" + "  and answer_date >= " + "'" + sqldateMin + "'" + " and answer_date<= " + "'" + sqldateMax + "'" + " group by o_id,q_id,credit,answer,question order by q_id ";
            DataTable dt = SqlHelper.ExecuteDataset(CMFun.GetConnectString(), CommandType.Text, sql).Tables[0];

            DataView dv = new DataView(dt);
            initial_chart(ch);


            for (int j = 0; j < dv.Count; j++)
            {
                string XVal = dv[j]["answer"].ToString();
                string strO_id = dv[j]["o_id"].ToString();
                string strNum = dv[j]["num"].ToString();
                int YVal = Convert.ToInt32(strNum);

                if (ch.Titles.Count != 0)
                {
                    ch.Titles.RemoveAt(0);
                    //ch.Titles.Add(strQ_id);
                    ch.Titles.Add(dv[j]["question"].ToString());
                }
                else
                { ch.Titles.Add(dv[j]["question"].ToString()); }

                DataPoint dp = new DataPoint();
                ch.Series["Default"].Points.AddXY(XVal, YVal);

            }

            Bindinfo(ch);


        }


        public void GridView_Excel(GridView gv, string filetype, string filename)
        {



            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.Buffer = true;
            //HttpContext.Current.Response.Charset = "UTF-8";
            //HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            //HttpContext.Current.Response.AppendHeader("Content-Disposition", String.Format("attachment;filename={0}", HttpUtility.UrlEncode(filename)));
            //HttpContext.Current.Response.ContentType = filetype;
            //Page p = new Page();
            //p.EnableViewState = false;
            //StringWriter tw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(tw);

            //gv.AllowPaging = false;

            //gv.RenderControl(hw);

            //HttpContext.Current.Response.Write("<meta   http-equiv=Content-Type   content=text/html;charset=UTF-8>");
            //string fileName = HttpUtility.UrlEncode("Result" + ".xls", Encoding.GetEncoding("UTF-8"));
            //HttpContext.Current.Response.AddHeader("content-disposition",
            //"attachment;filename=" + fileName);
            //HttpContext.Current.Response.Write(tw.ToString());
            //HttpContext.Current.Response.End();
        }



    }




}
