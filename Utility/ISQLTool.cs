using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using UnileverCAS.UnileverFun;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections.Generic;

namespace UnileverCAS.UnileverFun
{
    interface ISQLTool
    {
        List<string> databaseList(string connection);
        List<string> GetColumnField(SqlConnection Conn, string TableName);
        List<string> GetSqlServerNames();
        List<string> GetTables(string connection);
    }
}
