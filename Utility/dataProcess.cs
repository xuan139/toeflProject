using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using System.Collections.Generic;
using UnileverCAS.UnileverFun;
using System.Windows.Forms;

namespace UnileverCAS.DLL
{
    public  class dataProcess
    {
        private SqlConnection Conn = new SqlConnection();

        private List<Control> txtinfo = new List<Control>();

        public DataTable ProduceNewDataTable(SqlConnection Conn, string table)
        {

            string sql1 = "SELECT * FROM SysColumns WHERE id=Object_Id('" + table + "')";
            DataTable dt = new DataTable(table);
            DataTable dtFields = CMFun.GetSqlDataTable(Conn, sql1);

            int iNum = dtFields.Rows.Count;
            if (iNum > 0)
            {
                for (int j = 0; j <= iNum - 1; j++)
                {
                    dt.Columns.Add(dtFields.Rows[j][0].ToString());
                }

            }


            return dt;

        }

        public List<string> Get_table_info(SqlConnection Conn, string table)
        {

            string sql = "SELECT * FROM SysColumns WHERE id=Object_Id('" + table + "')";



            DataTable dtFields = CMFun.GetSqlDataTable(Conn, sql);
            List<string> tablelist = new List<string>();

            int iNum = dtFields.Rows.Count;
            if (iNum > 0)
            {
                for (int j = 0; j <= iNum - 1; j++)
                { tablelist.Add(dtFields.Rows[j][0].ToString()); }

            }
            else
            {

            }
            Conn.Close();
            return tablelist;
        }


        public DataTable Get_all_DT(SqlConnection Conn, string table, string sql)
        {

            DataTable dtSheet = CMFun.GetSqlDataTable(Conn, sql);

            Conn.Close();
            return dtSheet;

        }



        public List<string> Get_all_record(SqlConnection Conn, string table,string sql)
        {
            DataTable dt = new DataTable();



            
            List<string> resultlist = new List<string>();
            List<string> fieldlist = new List<string>();

            //string sql = "select * from " + table;

            fieldlist = Get_table_info(Conn,table);

            DataTable dtSheet = CMFun.GetSqlDataTable(Conn, sql);

            int iNum = dtSheet.Rows.Count;
            if (iNum > 0)
            {
                for (int j = 0; j <= iNum - 1; j++)
                {

                    for (int jj = 0; jj <= fieldlist.Count -1; jj++)
                    {
                        
                        resultlist.Add(fieldlist[jj].ToString() + "=" + dtSheet.Rows[j][jj].ToString());
                    }
                
                }

            }
            else
            {

            }


            Conn.Close();
            return resultlist;
 
        }






        public  List<string> Get_info(SqlConnection Conn ,string table, string sID, string idx)
        {
            List<string> lsTable = new List<string>();

            List<string> resultlist = new List<string>();

            lsTable = this.Get_table_info(Conn,table);
            int total = lsTable.Count;

            string sql = "select * from " + table + " where " + sID + " = " + idx;
            DataTable dtSheet = CMFun.GetSqlDataTable(Conn, sql);

            int iNum = dtSheet.Rows.Count;
            if (iNum > 0)
            {
                for (int j = 0; j <= total - 1; j++)
                { resultlist.Add(lsTable[j].ToString() + "=" + dtSheet.Rows[0][j].ToString()); }

            }
            else
            {

            }


            Conn.Close();
            return resultlist;

        }


        
    }
}