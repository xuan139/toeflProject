using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;




namespace UnileverCAS.UnileverFun
{
    public static class CMFun
    {


        #region 打开关闭的数据库连接
        public static void SqlConnOpenAgain(SqlConnection Conn)
        {
            if (Conn.State == ConnectionState.Closed)
            {
                Conn.ConnectionString = "Data Source=156.5.173.15;Initial Catalog=uni_crm;User ID=uni_crm Password=uni_crm"; // GetConnectString();
                Conn.Open();
            }
        }
        #endregion
        #region 返回DataTable对象
        public static DataTable GetSqlDataTable(SqlConnection Conn, string strSql)
        {

            SqlConnOpenAgain(Conn);
            DataTable dtDSet = new DataTable();
            SqlCommand Comm = new SqlCommand(strSql, Conn);
            Comm.CommandTimeout = 3600;
            SqlDataAdapter sdAdapter = new SqlDataAdapter(Comm);
            sdAdapter.Fill(dtDSet);
            return dtDSet;
        }
        #endregion
        #region 返回DataSet数据集对象
        public static DataSet GetSqlDataSet(SqlConnection Conn, string strSql)
        {
            DataSet dsDSet = null;
            SqlConnOpenAgain(Conn);
            SqlDataAdapter sdAdapter = new SqlDataAdapter(strSql, Conn);
            sdAdapter.SelectCommand.CommandTimeout = 3600;
            dsDSet = new DataSet();
            sdAdapter.Fill(dsDSet);
            return dsDSet;
        }
        #endregion
        #region 返回SqlDataReader对象
        public static SqlDataReader GetSqlDataReader(SqlConnection Conn, string strSql)
        {
            SqlDataReader DRead = null;
            SqlConnOpenAgain(Conn);
            SqlCommand Comm = new SqlCommand(strSql, Conn);
            Comm.CommandTimeout = 3600;
            DRead = Comm.ExecuteReader();
            return DRead;
        }
        #endregion
        #region 返回Sql查询结果的第一行第一列Object对象
        public static Object GetSqlObject(SqlConnection Conn, string strSql)
        {
            Object obj = null;
            SqlConnOpenAgain(Conn);
            SqlCommand Comm = new SqlCommand(strSql, Conn);
            Comm.CommandTimeout = 3600;
            obj = Comm.ExecuteScalar();
            return obj;
        }
        #endregion
        #region 插入、修改、删除数据并返回影响的行数
        public static int GetSqlExecuteNum(SqlConnection Conn, string strSql)
        {
            int iRowNum = 0;
            SqlConnOpenAgain(Conn);
            SqlCommand Comm = new SqlCommand(strSql, Conn);
            Comm.CommandTimeout = 3600;
            iRowNum = Comm.ExecuteNonQuery();
            Comm.Dispose();
            return iRowNum;
        }
        #endregion
        #region 查询Sql并返回bool值表示是否存在数据
        public static bool GetSqlDataHasRows(SqlConnection Conn, string strSql)
        {
            bool bflag = false;
            SqlDataReader DRead = null;
            DRead = GetSqlDataReader(Conn, strSql);
            if (DRead != null)
            {
                bflag = DRead.HasRows;
            }
            DRead.Close();
            return bflag;
        }
        #endregion
        #region 插入一条数据并返回主键Id
        public static string GetSqlInsertReturnPKID(SqlConnection Conn, string strSql)
        {
            string strPKID = "";
            strSql += " SELECT SCOPE_IDENTITY() AS NEWID";
            SqlConnOpenAgain(Conn);
            SqlCommand Comm = new SqlCommand(strSql, Conn);
            strPKID = Comm.ExecuteScalar().ToString();
            Comm.Dispose();
            return strPKID;
        }
        #endregion

        public static List<string> GetTableField(SqlConnection Conn, string tableName)
        {

            //SqlConnOpenAgain(Conn);
            List<string> myCode = new List<string>();

            string tbName = "Select Name FROM SysColumns Where id=Object_Id('" + tableName + "')";


            DataTable dsName = CMFun.GetSqlDataTable(Conn, tbName);

            for (int i = 0; i < dsName.Rows.Count; i++)
            {
                myCode.Add(dsName.Rows[i][0].ToString());

            }

            return myCode;
        }


        public enum DateInterval
        {
            Second, Minute, Hour, Day, Week, Month, Quarter, Year
        }
        public sealed class DateTimeManger
        {
            private DateTimeManger()
            { }//end of default constructor
            public static long DateDiff(DateInterval Interval, System.DateTime StartDate, System.DateTime EndDate)
            {
                long lngDateDiffValue = 0;
                System.TimeSpan TS = new System.TimeSpan(EndDate.Ticks - StartDate.Ticks);
                switch (Interval)
                {
                    case DateInterval.Second:
                        lngDateDiffValue = (long)TS.TotalSeconds;
                        break;
                    case DateInterval.Minute:
                        lngDateDiffValue = (long)TS.TotalMinutes;
                        break;
                    case DateInterval.Hour:
                        lngDateDiffValue = (long)TS.TotalHours;
                        break;
                    case DateInterval.Day:
                        lngDateDiffValue = (long)TS.Days;
                        break;
                    case DateInterval.Week:
                        lngDateDiffValue = (long)(TS.Days / 7);
                        break;
                    case DateInterval.Month:
                        lngDateDiffValue = (long)(TS.Days / 30);
                        break;
                    case DateInterval.Quarter:
                        lngDateDiffValue = (long)((TS.Days / 30) / 3);
                        break;
                    case DateInterval.Year:
                        lngDateDiffValue = (long)(TS.Days / 365);
                        break;
                }
                return (lngDateDiffValue);
            }//end of DateDiff
        }//

    }
}
