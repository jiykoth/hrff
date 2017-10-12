using System;
using System.Data;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace Hrff { 
  public class Common {
    
    public static string GetTable(string sql) {
      using (SqlConnection conn = Common.GetConn()) {
        using (SqlCommand cmd = new SqlCommand(sql, conn)) {
          using (SqlDataAdapter da = new SqlDataAdapter(cmd)) {
            using (DataSet ds = new DataSet()) {
              da.Fill(ds);
              return Common.GetDataTableToJSONString(ds.Tables[0]);
            }
          }
        }
      }
    }

    private static SqlConnection GetConn() {
      string connString = WebConfigurationManager.ConnectionStrings["localConnectionString"].ConnectionString;
      SqlConnection conn = new SqlConnection(connString);
      conn.Open();
      return conn;
    }

    private static string GetDataTableToJSONString(DataTable table) {
      List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
      foreach (DataRow row in table.Rows) {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        foreach (DataColumn col in table.Columns) {
          dict[col.ColumnName] = row[col];
        }
        list.Add(dict);
      }
      JavaScriptSerializer serializer = new JavaScriptSerializer();
      serializer.MaxJsonLength = Int32.MaxValue;
      return serializer.Serialize(list);
    }
  }
}