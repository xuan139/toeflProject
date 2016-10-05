using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Data.OleDb;
using System.Web;


/// <summary>
/// Excel������
/// </summary>
/// Microsoft Excel 11.0 Object Library
public class ExcelHelper
{
    #region ���ݵ�����Excel�ļ�
    /// </summary> 
    /// ����Excel�ļ����Զ����ؿ����ص��ļ��� 
    /// </summary> 
    public static void DataTable1Excel(System.Data.DataTable dtData)
    {
        GridView gvExport = null;
        HttpContext curContext = HttpContext.Current;
        StringWriter strWriter = null;
        HtmlTextWriter htmlWriter = null;
        if (dtData != null)
        {
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            curContext.Response.Charset = "utf-8";
            strWriter = new StringWriter();
            htmlWriter = new HtmlTextWriter(strWriter);
            gvExport = new GridView();
            gvExport.DataSource = dtData.DefaultView;
            gvExport.AllowPaging = false;
            gvExport.DataBind();
            gvExport.RenderControl(htmlWriter);
            curContext.Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html;charset=gb2312\"/>" + strWriter.ToString());
            curContext.Response.End();
        }
    }

    /// <summary>
    /// ����Excel�ļ���ת��Ϊ�ɶ�ģʽ
    /// </summary>
    public static void DataTable2Excel(System.Data.DataTable dtData)
    {
        DataGrid dgExport = null;
        HttpContext curContext = HttpContext.Current;
        StringWriter strWriter = null;
        HtmlTextWriter htmlWriter = null;

        if (dtData != null)
        {
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            curContext.Response.Charset = "";
            strWriter = new StringWriter();
            htmlWriter = new HtmlTextWriter(strWriter);
            dgExport = new DataGrid();
            dgExport.DataSource = dtData.DefaultView;
            dgExport.AllowPaging = false;
            dgExport.DataBind();
            dgExport.RenderControl(htmlWriter);
            curContext.Response.Write(strWriter.ToString());
            curContext.Response.End();
        }
    }

    /// <summary>
    /// ����Excel�ļ������Զ����ļ���
    /// </summary>
    public static void DataTable3Excel(System.Data.DataTable dtData, String FileName)
    {
        GridView dgExport = null;
        HttpContext curContext = HttpContext.Current;
        StringWriter strWriter = null;
        HtmlTextWriter htmlWriter = null;

        if (dtData != null)
        {
            HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8);
            curContext.Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls");
            curContext.Response.ContentType = "application nd.ms-excel";
            curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            curContext.Response.Charset = "GB2312";
            strWriter = new StringWriter();
            htmlWriter = new HtmlTextWriter(strWriter);
            dgExport = new GridView();
            dgExport.DataSource = dtData.DefaultView;
            dgExport.AllowPaging = false;
            dgExport.DataBind();
            dgExport.RenderControl(htmlWriter);
            curContext.Response.Write(strWriter.ToString());
            curContext.Response.End();
        }
    }

    /// <summary>
    /// �����ݵ�����Excel�ļ�
    /// </summary>
    /// <param name="Table">DataTable����</param>
    /// <param name="ExcelFilePath">Excel�ļ�·��</param>
    public static bool OutputToExcel(DataTable Table, string ExcelFilePath)
    {
        if (File.Exists(ExcelFilePath))
        {
            throw new Exception("���ļ��Ѿ����ڣ�");
        }

        if ((Table.TableName.Trim().Length == 0) || (Table.TableName.ToLower() == "table"))
        {
            Table.TableName = "Sheet1";
        }

        //���ݱ������
        int ColCount = Table.Columns.Count;

        //���ڼ�����ʵ��������ʱ�����
        int i = 0;

        //��������
        OleDbParameter[] para = new OleDbParameter[ColCount];

        //������ṹ��SQL���
        string TableStructStr = @"Create Table " + Table.TableName + "(";

        //�����ַ���
        string connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelFilePath + ";Extended Properties=Excel 8.0;";
        OleDbConnection objConn = new OleDbConnection(connString);

        //������ṹ
        OleDbCommand objCmd = new OleDbCommand();

        //�������ͼ���
        ArrayList DataTypeList = new ArrayList();
        DataTypeList.Add("System.Decimal");
        DataTypeList.Add("System.Double");
        DataTypeList.Add("System.Int16");
        DataTypeList.Add("System.Int32");
        DataTypeList.Add("System.Int64");
        DataTypeList.Add("System.Single");

        //�������ݱ�������У����ڴ�����ṹ
        foreach (DataColumn col in Table.Columns)
        {
            //��������������У������ø��е���������Ϊdouble
            if (DataTypeList.IndexOf(col.DataType.ToString()) >= 0)
            {
                para[i] = new OleDbParameter("@" + col.ColumnName, OleDbType.Double);
                objCmd.Parameters.Add(para[i]);

                //��������һ��
                if (i + 1 == ColCount)
                {
                    TableStructStr += col.ColumnName + " double)";
                }
                else
                {
                    TableStructStr += col.ColumnName + " double,";
                }
            }
            else
            {
                para[i] = new OleDbParameter("@" + col.ColumnName, OleDbType.VarChar);
                objCmd.Parameters.Add(para[i]);

                //��������һ��
                if (i + 1 == ColCount)
                {
                    TableStructStr += col.ColumnName + " varchar)";
                }
                else
                {
                    TableStructStr += col.ColumnName + " varchar,";
                }
            }
            i++;
        }

        //����Excel�ļ����ļ��ṹ
        try
        {
            objCmd.Connection = objConn;
            objCmd.CommandText = TableStructStr;

            if (objConn.State == ConnectionState.Closed)
            {
                objConn.Open();
            }
            objCmd.ExecuteNonQuery();
        }
        catch (Exception exp)
        {
            throw exp;
        }

        //�����¼��SQL���
        string InsertSql_1 = "Insert into " + Table.TableName + " (";
        string InsertSql_2 = " Values (";
        string InsertSql = "";

        //���������У����ڲ����¼���ڴ˴��������¼��SQL���
        for (int colID = 0; colID < ColCount; colID++)
        {
            if (colID + 1 == ColCount)  //���һ��
            {
                InsertSql_1 += Table.Columns[colID].ColumnName + ")";
                InsertSql_2 += "@" + Table.Columns[colID].ColumnName + ")";
            }
            else
            {
                InsertSql_1 += Table.Columns[colID].ColumnName + ",";
                InsertSql_2 += "@" + Table.Columns[colID].ColumnName + ",";
            }
        }

        InsertSql = InsertSql_1 + InsertSql_2;

        //�������ݱ������������
        for (int rowID = 0; rowID < Table.Rows.Count; rowID++)
        {
            for (int colID = 0; colID < ColCount; colID++)
            {
                if (para[colID].DbType == DbType.Double && Table.Rows[rowID][colID].ToString().Trim() == "")
                {
                    para[colID].Value = 0;
                }
                else
                {
                    para[colID].Value = Table.Rows[rowID][colID].ToString().Trim();
                }
            }
            try
            {
                objCmd.CommandText = InsertSql;
                objCmd.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                string str = exp.Message;
            }
        }
        try
        {
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
        }
        catch (Exception exp)
        {
            throw exp;
        }
        return true;
    }

    /// <summary>
    /// �����ݵ�����Excel�ļ�
    /// </summary>
    /// <param name="Table">DataTable����</param>
    /// <param name="Columns">Ҫ�����������м���</param>
    /// <param name="ExcelFilePath">Excel�ļ�·��</param>
    public static bool OutputToExcel(DataTable Table, ArrayList Columns, string ExcelFilePath)
    {
        if (File.Exists(ExcelFilePath))
        {
            throw new Exception("���ļ��Ѿ����ڣ�");
        }

        //��������������ڱ��������ȡ���ݱ��������
        if (Columns.Count > Table.Columns.Count)
        {
            for (int s = Table.Columns.Count + 1; s <= Columns.Count; s++)
            {
                Columns.RemoveAt(s);   //�Ƴ����ݱ��������������
            }
        }

        //�������е������У�����������е��������Ͳ��� DataColumn�������Ƴ�
        DataColumn column = new DataColumn();
        for (int j = 0; j < Columns.Count; j++)
        {
            try
            {
                column = (DataColumn)Columns[j];
            }
            catch (Exception)
            {
                Columns.RemoveAt(j);
            }
        }
        if ((Table.TableName.Trim().Length == 0) || (Table.TableName.ToLower() == "table"))
        {
            Table.TableName = "Sheet1";
        }

        //���ݱ������
        int ColCount = Columns.Count;

        //��������
        OleDbParameter[] para = new OleDbParameter[ColCount];

        //������ṹ��SQL���
        string TableStructStr = @"Create Table " + Table.TableName + "(";

        //�����ַ���
        string connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelFilePath + ";Extended Properties=Excel 8.0;";
        OleDbConnection objConn = new OleDbConnection(connString);

        //������ṹ
        OleDbCommand objCmd = new OleDbCommand();

        //�������ͼ���
        ArrayList DataTypeList = new ArrayList();
        DataTypeList.Add("System.Decimal");
        DataTypeList.Add("System.Double");
        DataTypeList.Add("System.Int16");
        DataTypeList.Add("System.Int32");
        DataTypeList.Add("System.Int64");
        DataTypeList.Add("System.Single");

        DataColumn col = new DataColumn();

        //�������ݱ�������У����ڴ�����ṹ
        for (int k = 0; k < ColCount; k++)
        {
            col = (DataColumn)Columns[k];

            //�е�����������������
            if (DataTypeList.IndexOf(col.DataType.ToString().Trim()) >= 0)
            {
                para[k] = new OleDbParameter("@" + col.Caption.Trim(), OleDbType.Double);
                objCmd.Parameters.Add(para[k]);

                //��������һ��
                if (k + 1 == ColCount)
                {
                    TableStructStr += col.Caption.Trim() + " Double)";
                }
                else
                {
                    TableStructStr += col.Caption.Trim() + " Double,";
                }
            }
            else
            {
                para[k] = new OleDbParameter("@" + col.Caption.Trim(), OleDbType.VarChar);
                objCmd.Parameters.Add(para[k]);

                //��������һ��
                if (k + 1 == ColCount)
                {
                    TableStructStr += col.Caption.Trim() + " VarChar)";
                }
                else
                {
                    TableStructStr += col.Caption.Trim() + " VarChar,";
                }
            }
        }

        //����Excel�ļ����ļ��ṹ
        try
        {
            objCmd.Connection = objConn;
            objCmd.CommandText = TableStructStr;

            if (objConn.State == ConnectionState.Closed)
            {
                objConn.Open();
            }
            objCmd.ExecuteNonQuery();
        }
        catch (Exception exp)
        {
            throw exp;
        }

        //�����¼��SQL���
        string InsertSql_1 = "Insert into " + Table.TableName + " (";
        string InsertSql_2 = " Values (";
        string InsertSql = "";

        //���������У����ڲ����¼���ڴ˴��������¼��SQL���
        for (int colID = 0; colID < ColCount; colID++)
        {
            if (colID + 1 == ColCount)  //���һ��
            {
                InsertSql_1 += Columns[colID].ToString().Trim() + ")";
                InsertSql_2 += "@" + Columns[colID].ToString().Trim() + ")";
            }
            else
            {
                InsertSql_1 += Columns[colID].ToString().Trim() + ",";
                InsertSql_2 += "@" + Columns[colID].ToString().Trim() + ",";
            }
        }

        InsertSql = InsertSql_1 + InsertSql_2;

        //�������ݱ������������
        DataColumn DataCol = new DataColumn();
        for (int rowID = 0; rowID < Table.Rows.Count; rowID++)
        {
            for (int colID = 0; colID < ColCount; colID++)
            {
                //��Ϊ�в�������������ȡ�õ�Ԫ��ʱ���������б�ţ���������е�����
                DataCol = (DataColumn)Columns[colID];
                if (para[colID].DbType == DbType.Double && Table.Rows[rowID][DataCol.Caption].ToString().Trim() == "")
                {
                    para[colID].Value = 0;
                }
                else
                {
                    para[colID].Value = Table.Rows[rowID][DataCol.Caption].ToString().Trim();
                }
            }
            try
            {
                objCmd.CommandText = InsertSql;
                objCmd.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                string str = exp.Message;
            }
        }
        try
        {
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
        }
        catch (Exception exp)
        {
            throw exp;
        }
        return true;
    }
    #endregion

    /// <summary>
    /// ��ȡExcel�ļ����ݱ��б�
    /// </summary>
    public static ArrayList GetExcelTables(string ExcelFileName)
    {
        DataTable dt = new DataTable();
        ArrayList TablesList = new ArrayList();
        if (File.Exists(ExcelFileName))
        {
            using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" + ExcelFileName))
            {
                try
                {
                    conn.Open();
                    dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                }
                catch (Exception exp)
                {
                    throw exp;
                }

                //��ȡ���ݱ����
                int tablecount = dt.Rows.Count;
                for (int i = 0; i < tablecount; i++)
                {
                    string tablename = dt.Rows[i][2].ToString().Trim().TrimEnd('$');
                    if (TablesList.IndexOf(tablename) < 0)
                    {
                        TablesList.Add(tablename);
                    }
                }
            }
        }
        return TablesList;
    }

    /// <summary>
    /// ��Excel�ļ�������DataTable(��һ����Ϊ��ͷ)
    /// </summary>
    /// <param name="ExcelFilePath">Excel�ļ�·��</param>
    /// <param name="TableName">���ݱ�����������ݱ�������Ĭ��Ϊ��һ�����ݱ���</param>
    public static DataTable InputFromExcel(string ExcelFilePath, string TableName)
    {
        if (!File.Exists(ExcelFilePath))
        {
            throw new Exception("Excel�ļ������ڣ�");
        }

        //������ݱ��������ڣ������ݱ���ΪExcel�ļ��ĵ�һ�����ݱ�
        ArrayList TableList = new ArrayList();
        TableList = GetExcelTables(ExcelFilePath);

        if (TableName.IndexOf(TableName) < 0)
        {
            TableName = TableList[0].ToString().Trim();
        }

        DataTable table = new DataTable();
        OleDbConnection dbcon = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelFilePath + ";Extended Properties=Excel 8.0");
        OleDbCommand cmd = new OleDbCommand("select * from [" + TableName + "$]", dbcon);
        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

        try
        {
            if (dbcon.State == ConnectionState.Closed)
            {
                dbcon.Open();
            }
            adapter.Fill(table);
        }
        catch (Exception exp)
        {
            throw exp;
        }
        finally
        {
            if (dbcon.State == ConnectionState.Open)
            {
                dbcon.Close();
            }
        }
        return table;
    }

    /// <summary>
    /// ��ȡExcel�ļ�ָ�����ݱ�������б�
    /// </summary>
    /// <param name="ExcelFileName">Excel�ļ���</param>
    /// <param name="TableName">���ݱ���</param>
    public static ArrayList GetExcelTableColumns(string ExcelFileName, string TableName)
    {
        DataTable dt = new DataTable();
        ArrayList ColsList = new ArrayList();
        if (File.Exists(ExcelFileName))
        {
            using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" + ExcelFileName))
            {
                conn.Open();
                dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, TableName, null });

                //��ȡ�и���
                int colcount = dt.Rows.Count;
                for (int i = 0; i < colcount; i++)
                {
                    string colname = dt.Rows[i]["Column_Name"].ToString().Trim();
                    ColsList.Add(colname);
                }
            }
        }
        return ColsList;
    }
}
