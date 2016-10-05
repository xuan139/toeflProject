using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Data.SQLite;

namespace UnileverCAS.UnileverFun
{
    /// <summary>
    /// 本类为SQLite数据库帮助静态类,使用时只需直接调用即可,无需实例化
    /// </summary>
    public static class SQLiteHelper
    {

        //数据库连接字符串
        public static readonly string Conn = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + @"DataBase\mydic.s3DB;Version=3;";

        //轻量级数据库SQLite的连接字符串写法："Data Source=D:\database\test.s3db"
        //轻量级数据库SQLite的加密后的连接字符串写法："Data Source=Maximus.db;Version=3;Password=myPassword;"\

        #region ExecuteNonQuery
        /// <summary>
        /// 执行数据库操作(新增、更新或删除)
        /// </summary>
        /// <param name="cmd">SqlCommand对象</param>
        /// <returns>所受影响的行数</returns>
        public static int ExecuteNonQuery(SQLiteCommand cmd)
        {
            int result = 0;
            if (Conn == null || Conn.Length == 0)
                throw new ArgumentNullException("Conn");
            using (SQLiteConnection con = new SQLiteConnection(Conn))
            {
                SQLiteTransaction trans = null;
                PrepareCommand(cmd, con, ref trans, true, cmd.CommandType, cmd.CommandText);
                try
                {
                    result = cmd.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// 执行数据库操作(新增、更新或删除)
        /// </summary>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <returns>所受影响的行数</returns>
        public static int ExecuteNonQuery(string commandText, CommandType commandType)
        {
            int result = 0;
            if (Conn == null || Conn.Length == 0)
                throw new ArgumentNullException("Conn");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");
            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection con = new SQLiteConnection(Conn))
            {
                SQLiteTransaction trans = null;
                PrepareCommand(cmd, con, ref trans, true, commandType, commandText);
                try
                {
                    result = cmd.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// 执行数据库操作(新增、更新或删除)
        /// </summary>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <param name="cmdParms">SQL参数对象</param>
        /// <returns>所受影响的行数</returns>
        public static int ExecuteNonQuery(string commandText, CommandType commandType, params SQLiteParameter[] cmdParms)
        {
            int result = 0;
            if (Conn == null || Conn.Length == 0)
                throw new ArgumentNullException("Conn");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");

            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection con = new SQLiteConnection(Conn))
            {
                SQLiteTransaction trans = null;
                PrepareCommand(cmd, con, ref trans, true, commandType, commandText);
                try
                {
                    result = cmd.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            return result;
        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// 执行数据库操作(新增、更新或删除)同时返回执行后查询所得的第1行第1列数据
        /// </summary>
        /// <param name="cmd">SqlCommand对象</param>
        /// <returns>查询所得的第1行第1列数据</returns>
        public static object ExecuteScalar(SQLiteCommand cmd)
        {
            object result = 0;
            if (Conn == null || Conn.Length == 0)
                throw new ArgumentNullException("Conn");
            using (SQLiteConnection con = new SQLiteConnection(Conn))
            {
                SQLiteTransaction trans = null;
                PrepareCommand(cmd, con, ref trans, true, cmd.CommandType, cmd.CommandText);
                try
                {
                    result = cmd.ExecuteScalar();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// 执行数据库操作(新增、更新或删除)同时返回执行后查询所得的第1行第1列数据
        /// </summary>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <returns>查询所得的第1行第1列数据</returns>
        public static object ExecuteScalar(string commandText, CommandType commandType)
        {
            object result = 0;
            if (Conn == null || Conn.Length == 0)
                throw new ArgumentNullException("Conn");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");
            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection con = new SQLiteConnection(Conn))
            {
                SQLiteTransaction trans = null;
                PrepareCommand(cmd, con, ref trans, true, commandType, commandText);
                try
                {
                    result = cmd.ExecuteScalar();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// 执行数据库操作(新增、更新或删除)同时返回执行后查询所得的第1行第1列数据
        /// </summary>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <param name="cmdParms">SQL参数对象</param>
        /// <returns>查询所得的第1行第1列数据</returns>
        public static object ExecuteScalar(string commandText, CommandType commandType, params SQLiteParameter[] cmdParms)
        {
            object result = 0;
            if (Conn == null || Conn.Length == 0)
                throw new ArgumentNullException("Conn");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");

            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection con = new SQLiteConnection(Conn))
            {
                SQLiteTransaction trans = null;
                PrepareCommand(cmd, con, ref trans, true, commandType, commandText);
                try
                {
                    result = cmd.ExecuteScalar();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            return result;
        }
        #endregion

        #region ExecuteReader
        /// <summary>
        /// 执行数据库查询，返回SqlDataReader对象
        /// </summary>
        /// <param name="cmd">SqlCommand对象</param>
        /// <returns>SqlDataReader对象</returns>
        public static DbDataReader ExecuteReader(SQLiteCommand cmd)
        {
            DbDataReader reader = null;
            if (Conn == null || Conn.Length == 0)
                throw new ArgumentNullException("Conn");

            SQLiteConnection con = new SQLiteConnection(Conn);
            SQLiteTransaction trans = null;
            PrepareCommand(cmd, con, ref trans, false, cmd.CommandType, cmd.CommandText);
            try
            {
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reader;
        }

        /// <summary>
        /// 执行数据库查询，返回SqlDataReader对象
        /// </summary>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <returns>SqlDataReader对象</returns>
        public static DbDataReader ExecuteReader(string commandText, CommandType commandType)
        {
            DbDataReader reader = null;
            if (Conn == null || Conn.Length == 0)
                throw new ArgumentNullException("Conn");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");

            SQLiteConnection con = new SQLiteConnection(Conn);
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteTransaction trans = null;
            PrepareCommand(cmd, con, ref trans, false, commandType, commandText);
            try
            {
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reader;
        }

        /// <summary>
        /// 执行数据库查询，返回SqlDataReader对象
        /// </summary>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <param name="cmdParms">SQL参数对象</param>
        /// <returns>SqlDataReader对象</returns>
        public static DbDataReader ExecuteReader(string commandText, CommandType commandType, params SQLiteParameter[] cmdParms)
        {
            DbDataReader reader = null;
            if (Conn == null || Conn.Length == 0)
                throw new ArgumentNullException("Conn");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");

            SQLiteConnection con = new SQLiteConnection(Conn);
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteTransaction trans = null;
            PrepareCommand(cmd, con, ref trans, false, commandType, commandText, cmdParms);
            try
            {
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reader;
        }
        #endregion

        #region ExecuteDataSet
        /// <summary>
        /// 执行数据库查询，返回DataSet对象
        /// </summary>
        /// <param name="cmd">SqlCommand对象</param>
        /// <returns>DataSet对象</returns>
        public static DataSet ExecuteDataSet(SQLiteCommand cmd)
        {
            DataSet ds = new DataSet();
            SQLiteConnection con = new SQLiteConnection(Conn);
            SQLiteTransaction trans = null;
            PrepareCommand(cmd, con, ref trans, false, cmd.CommandType, cmd.CommandText);
            try
            {
                SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd.Connection != null)
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                }
            }
            return ds;
        }

        /// <summary>
        /// 执行数据库查询，返回DataSet对象
        /// </summary>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <returns>DataSet对象</returns>
        public static DataSet ExecuteDataSet(string commandText, CommandType commandType)
        {
            if (Conn == null || Conn.Length == 0)
                throw new ArgumentNullException("Conn");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");
            DataSet ds = new DataSet();
            SQLiteConnection con = new SQLiteConnection(Conn);
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteTransaction trans = null;
            PrepareCommand(cmd, con, ref trans, false, commandType, commandText);
            try
            {
                SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            return ds;
        }

        /// <summary>
        /// 执行数据库查询，返回DataSet对象
        /// </summary>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <param name="cmdParms">SQL参数对象</param>
        /// <returns>DataSet对象</returns>
        public static DataSet ExecuteDataSet(string commandText, CommandType commandType, params SQLiteParameter[] cmdParms)
        {
            if (Conn == null || Conn.Length == 0)
                throw new ArgumentNullException("Conn");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");
            DataSet ds = new DataSet();
            SQLiteConnection con = new SQLiteConnection(Conn);
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteTransaction trans = null;
            PrepareCommand(cmd, con, ref trans, false, commandType, commandText, cmdParms);
            try
            {
                SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            return ds;
        }
        #endregion

        /// <summary>
        /// 通用分页查询方法
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="strColumns">查询字段名</param>
        /// <param name="strWhere">where条件</param>
        /// <param name="strOrder">排序条件</param>
        /// <param name="pageSize">每页数据数量</param>
        /// <param name="currentIndex">当前页数</param>
        /// <param name="recordOut">数据总量</param>
        /// <returns>DataTable数据表</returns>
        public static DataTable SelectPaging(string tableName, string strColumns, string strWhere, string strOrder, int pageSize, int currentIndex, out int recordOut)
        {
            DataTable dt = new DataTable();
            recordOut = Convert.ToInt32(ExecuteScalar("select count(*) from " + tableName, CommandType.Text));
            string pagingTemplate = "select {0} from {1} where {2} order by {3} limit {4} offset {5} ";
            int offsetCount = (currentIndex - 1) * pageSize;
            string commandText = String.Format(pagingTemplate, strColumns, tableName, strWhere, strOrder, pageSize.ToString(), offsetCount.ToString());
            using (DbDataReader reader = ExecuteReader(commandText, CommandType.Text))
            {
                if (reader != null)
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }

        /// <summary>
        /// 预处理Command对象,数据库链接,事务,需要执行的对象,参数等的初始化
        /// </summary>
        /// <param name="cmd">Command对象</param>
        /// <param name="conn">Connection对象</param>
        /// <param name="trans">Transcation对象</param>
        /// <param name="useTrans">是否使用事务</param>
        /// <param name="cmdType">SQL字符串执行类型</param>
        /// <param name="cmdText">SQL Text</param>
        /// <param name="cmdParms">SQLiteParameters to use in the command</param>
        private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, ref SQLiteTransaction trans, bool useTrans, CommandType cmdType, string cmdText, params SQLiteParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (useTrans)
            {
                trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.Transaction = trans;
            }


            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SQLiteParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
    }
}