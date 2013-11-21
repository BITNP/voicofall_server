using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace voicofall_server.ResponsePages
{
    /// <summary>
    /// reto_ticketsTable 的摘要说明
    /// </summary>
    public class reto_ticketsTable : IHttpHandler
    {
        public static readonly string connStr1 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath("~/App_Data/tickets.mdb");
        OleDbDataAdapter adapter1;
        DataSet dataSet1;
        OleDbConnection conn;
        string strSQL;
        DataTable ticketsTable;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            InitDB();
            foreach (DataRow row in ticketsTable.Rows)
            {
                context.Response.Write(row["UID"] as string + ",");
            }
        }

        private void InitDB()
        {
            conn = new OleDbConnection(connStr1);
            conn.Open();
            strSQL = "select * from ticketsTable";
            adapter1 = new OleDbDataAdapter(strSQL, conn);
            //加了这句话就不会出现：当传递具有已修改行的 DataRow 集合时，更新要求有效的 UpdateCommand。
            OleDbCommandBuilder sqlBulider = new OleDbCommandBuilder(adapter1);
            dataSet1 = new DataSet();
            adapter1.Fill(dataSet1, "ticketsTable");
            ticketsTable = dataSet1.Tables["ticketsTable"];
            ticketsTable.PrimaryKey = new DataColumn[] { ticketsTable.Columns["UID"] };
            conn.Close();
        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}