//using System; 
//using System.Collections; 
//using System.Collections.Specialized; 
//using System.Data; 
//using MySql.Data.MySqlClient; 
//using System.Configuration; 
//using System.Data.Common; 
//using System.Collections.Generic; 
//using System.Text.RegularExpressions;

//namespace UnileverCAS.UnileverFun
//{ 
//public class MysqlHelper 
//{ 
//    //���ݿ������ַ���(web.config������)�����Զ�̬����connectionString֧�ֶ����ݿ�. 
//    // public static string connectionString = ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString; 
//    public static string connectionString = ConfigurationManager.AppSettings["MySQL"]; 
//    //public string m = ConfigurationManager.AppSettings["MySQL"]; 
//    public MysqlHelper() { } 
//    #region ExecuteNonQuery 
//    //ִ��SQL��䣬����Ӱ��ļ�¼�� 
//    /// <summary> 
//    /// ִ��SQL��䣬����Ӱ��ļ�¼�� 
//    /// </summary> 
//    /// <param name="SQLString">SQL���</param> 
//    /// <returns>Ӱ��ļ�¼��</returns> 
//    public static int ExecuteNonQuery(string SQLString) 
//    { 
//        using (MySqlConnection connection = new MySqlConnection(connectionString)) 
//    { 
//        using (MySqlCommand cmd = new MySqlCommand(SQLString, connection)) 
//    { 
//    try 
//    { 
//    connection.Open(); 
//    int rows = cmd.ExecuteNonQuery(); 
//    return rows; 
//    } 
//    catch (MySql.Data.MySqlClient.MySqlException e) 
//    { 
//    connection.Close(); 
//    throw e; 
//    } 
//    } 
//    } 
//    } 
///// <summary> 
///// ִ��SQL��䣬����Ӱ��ļ�¼�� 
///// </summary> 
///// <param name="SQLString">SQL���</param> 
///// <returns>Ӱ��ļ�¼��</returns> 
//public static int ExecuteNonQuery(string SQLString, params MySqlParameter[] cmdParms) 
//{ 
//using (MySqlConnection connection = new MySqlConnection(connectionString)) 
//{ 
//using (MySqlCommand cmd = new MySqlCommand()) 
//{ 
//try 
//{ 
//PrepareCommand(cmd, connection, null, SQLString, cmdParms); 
//int rows = cmd.ExecuteNonQuery(); 
//cmd.Parameters.Clear(); 
//return rows; 
//} 
//catch (MySql.Data.MySqlClient.MySqlException e) 
//{ 
//throw e; 
//} 
//} 
//} 
//} 
////ִ�ж���SQL��䣬ʵ�����ݿ����� 
///// <summary> 
///// ִ�ж���SQL��䣬ʵ�����ݿ����� 
///// </summary> 
///// <param name="SQLStringList">����SQL���</param> 
//public static bool ExecuteNoQueryTran(List<String> SQLStringList) 
//{ 
//using (MySqlConnection conn = new MySqlConnection(connectionString)) 
//{ 
//conn.Open(); 
//MySqlCommand cmd = new MySqlCommand(); 
//cmd.Connection = conn; 
//MySqlTransaction tx = conn.BeginTransaction(); 
//cmd.Transaction = tx; 
//try 
//{ 
//for (int n = 0; n < SQLStringList.Count; n++) 
//{ 
//string strsql = SQLStringList[n]; 
//if (strsql.Trim().Length > 1) 
//{ 
//cmd.CommandText = strsql; 
//PrepareCommand(cmd, conn, tx, strsql, null); 
//cmd.ExecuteNonQuery(); 
//} 
//} 
//cmd.ExecuteNonQuery(); 
//tx.Commit(); 
//return true; 
//} 
//catch 
//{ 
//tx.Rollback(); 
//return false; 
//} 
//} 
//} 
//#endregion 
//#region ExecuteScalar 
///// <summary> 
///// ִ��һ�������ѯ�����䣬���ز�ѯ�����object���� 
///// </summary> 
///// <param name="SQLString">�����ѯ������</param> 
///// <returns>��ѯ�����object��</returns> 
//public static object ExecuteScalar(string SQLString) 
//{ 
//using (MySqlConnection connection = new MySqlConnection(connectionString)) 
//{ 
//using (MySqlCommand cmd = new MySqlCommand(SQLString, connection)) 
//{ 
//try 
//{ 
//connection.Open(); 
//object obj = cmd.ExecuteScalar(); 
//if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value))) 
//{ 
//return null; 
//} 
//else 
//{ 
//return obj; 
//} 
//} 
//catch (MySql.Data.MySqlClient.MySqlException e) 
//{ 
//connection.Close(); 
//throw e; 
//} 
//} 
//} 
//} 
///// <summary> 
///// ִ��һ�������ѯ�����䣬���ز�ѯ�����object���� 
///// </summary> 
///// <param name="SQLString">�����ѯ������</param> 
///// <returns>��ѯ�����object��</returns> 
//public static object ExecuteScalar(string SQLString, params MySqlParameter[] cmdParms) 
//{ 
//using (MySqlConnection connection = new MySqlConnection(connectionString)) 
//{ 
//using (MySqlCommand cmd = new MySqlCommand()) 
//{ 
//try 
//{ 
//PrepareCommand(cmd, connection, null, SQLString, cmdParms); 
//object obj = cmd.ExecuteScalar(); 
//cmd.Parameters.Clear(); 
//if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value))) 
//{ 
//return null; 
//} 
//else 
//{ 
//return obj; 
//} 
//} 
//catch (MySql.Data.MySqlClient.MySqlException e) 
//{ 
//throw e; 
//} 
//} 
//} 
//} 
//#endregion 
//#region ExecuteReader 
///// <summary> 
///// ִ�в�ѯ��䣬����MySqlDataReader ( ע�⣺���ø÷�����һ��Ҫ��MySqlDataReader����Close ) 
///// </summary> 
///// <param name="strSQL">��ѯ���</param> 
///// <returns>MySqlDataReader</returns> 
//public static MySqlDataReader ExecuteReader(string strSQL) 
//{ 
//MySqlConnection connection = new MySqlConnection(connectionString); 
//MySqlCommand cmd = new MySqlCommand(strSQL, connection); 
//try 
//{ 
//connection.Open(); 
//MySqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection); 
//return myReader; 
//} 
//catch (MySql.Data.MySqlClient.MySqlException e) 
//{ 
//throw e; 
//} 
//} 
///// <summary> 
///// ִ�в�ѯ��䣬����MySqlDataReader ( ע�⣺���ø÷�����һ��Ҫ��MySqlDataReader����Close ) 
///// </summary> 
///// <param name="strSQL">��ѯ���</param> 
///// <returns>MySqlDataReader</returns> 
//public static MySqlDataReader ExecuteReader(string SQLString, params MySqlParameter[] cmdParms) 
//{ 
//MySqlConnection connection = new MySqlConnection(connectionString); 
//MySqlCommand cmd = new MySqlCommand(); 
//try 
//{ 
//PrepareCommand(cmd, connection, null, SQLString, cmdParms); 
//MySqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection); 
//cmd.Parameters.Clear(); 
//return myReader; 
//} 
//catch (MySql.Data.MySqlClient.MySqlException e) 
//{ 
//throw e; 
//} 
//// finally 
//// { 
//// cmd.Dispose(); 
//// connection.Close(); 
//// } 
//} 
//#endregion 
//#region ExecuteDataTable 
///// <summary> 
///// ִ�в�ѯ��䣬����DataTable 
///// </summary> 
///// <param name="SQLString">��ѯ���</param> 
///// <returns>DataTable</returns> 
//public static DataTable ExecuteDataTable(string SQLString) 
//{ 
//using (MySqlConnection connection = new MySqlConnection(connectionString)) 
//{ 
//DataSet ds = new DataSet(); 
//try 
//{ 
//connection.Open(); 
//MySqlDataAdapter command = new MySqlDataAdapter(SQLString, connection); 
//command.Fill(ds, "ds"); 
//} 
//catch (MySql.Data.MySqlClient.MySqlException ex) 
//{ 
//throw new Exception(ex.Message); 
//} 
//return ds.Tables[0]; 
//} 
//} 
///// <summary> 
///// ִ�в�ѯ��䣬����DataSet 
///// </summary> 
///// <param name="SQLString">��ѯ���</param> 
///// <returns>DataTable</returns> 
//public static DataTable ExecuteDataTable(string SQLString, params MySqlParameter[] cmdParms) 
//{ 
//using (MySqlConnection connection = new MySqlConnection(connectionString)) 
//{ 
//MySqlCommand cmd = new MySqlCommand(); 
//PrepareCommand(cmd, connection, null, SQLString, cmdParms); 
//using (MySqlDataAdapter da = new MySqlDataAdapter(cmd)) 
//{ 
//DataSet ds = new DataSet(); 
//try 
//{ 
//da.Fill(ds, "ds"); 
//cmd.Parameters.Clear(); 
//} 
//catch (MySql.Data.MySqlClient.MySqlException ex) 
//{ 
//throw new Exception(ex.Message); 
//} 
//return ds.Tables[0]; 
//} 
//} 
//} 
////��ȡ��ʼҳ��ͽ���ҳ�� 
//public static DataTable ExecuteDataTable(string cmdText, int startResord, int maxRecord) 
//{ 
//using (MySqlConnection connection = new MySqlConnection(connectionString)) 
//{ 
//DataSet ds = new DataSet(); 
//try 
//{ 
//connection.Open(); 
//MySqlDataAdapter command = new MySqlDataAdapter(cmdText, connection); 
//command.Fill(ds, startResord, maxRecord, "ds"); 
//} 
//catch (MySql.Data.MySqlClient.MySqlException ex) 
//{ 
//throw new Exception(ex.Message); 
//} 
//return ds.Tables[0]; 
//} 
//} 
//#endregion 
///// <summary> 
///// ��ȡ��ҳ���� �ڲ��ô洢��������� 
///// </summary> 
///// <param name="recordCount">�ܼ�¼����</param> 
///// <param name="selectList">ѡ����ж��Ÿ���,֧��top num</param> 
///// <param name="tableName">������</param> 
///// <param name="whereStr">�����ַ� ����ǰ�� and</param> 
///// <param name="orderExpression">���� ���� ID</param> 
///// <param name="pageIdex">��ǰ����ҳ</param> 
///// <param name="pageSize">ÿҳ��¼��</param> 
///// <returns></returns> 
//public static DataTable getPager(out int recordCount, string selectList, string tableName, string whereStr, string orderExpression, int pageIdex, int pageSize) 
//{ 
//int rows = 0; 
//DataTable dt = new DataTable(); 
//MatchCollection matchs = Regex.Matches(selectList, @"top\s+\d{1,}", RegexOptions.IgnoreCase);//����top 
//string sqlStr = sqlStr = string.Format("select {0} from {1} where 1=1 {2}", selectList, tableName, whereStr); 
//if (!string.IsNullOrEmpty(orderExpression)) { sqlStr += string.Format(" Order by {0}", orderExpression); } 
//if (matchs.Count > 0) //����top��ʱ�� 
//{ 
//DataTable dtTemp = ExecuteDataTable(sqlStr); 
//rows = dtTemp.Rows.Count; 
//} 
//else //������top��ʱ�� 
//{ 
//string sqlCount = string.Format("select count(*) from {0} where 1=1 {1} ", tableName, whereStr); 
////��ȡ���� 
//object obj = ExecuteScalar(sqlCount); 
//if (obj != null) 
//{ 
//rows = Convert.ToInt32(obj); 
//} 
//} 
//dt = ExecuteDataTable(sqlStr, (pageIdex-1)*pageSize, pageSize); 
//recordCount = rows; 
//return dt; 
//} 
//#region ����command 
//private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, string cmdText, MySqlParameter[] cmdParms) 
//{ 
//if (conn.State != ConnectionState.Open) 
//conn.Open(); 
//cmd.Connection = conn; 
//cmd.CommandText = cmdText; 
//if (trans != null) 
//cmd.Transaction = trans; 
//cmd.CommandType = CommandType.Text;//cmdType; 
//if (cmdParms != null) 
//{ 
//foreach (MySqlParameter parameter in cmdParms) 
//{ 
//if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) && 
//(parameter.Value == null)) 
//{ 
//parameter.Value = DBNull.Value; 
//} 
//cmd.Parameters.Add(parameter); 
//} 
//} 
//} 
//#endregion 
//} 
//}

