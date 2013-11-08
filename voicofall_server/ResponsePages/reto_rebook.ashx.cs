using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace voicofall_server.ResponsePages
{
    /// <summary>
    /// reto_rebook 的摘要说明
    /// </summary>
    public class reto_rebook : IHttpHandler
    {
        public static readonly string connStr1 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath("~/App_Data/tickets.mdb");
        OleDbDataAdapter Adapter1;
        OleDbConnection conn;
        DataSet dataSet1;
        OleDbDataAdapter Adapter2;
        DataSet dataSet2;
        string strSQL;
        DataTable ticketsStateTable;
        DataTable ticketsTable;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string uid = context.Request.Params["uid"];
            InitDB(context);
            foreach (DataRow row in ticketsTable.Rows)
            {
                if ((string)(row["uid"]) == uid)
                {
                    row.Delete();
                    ticketsStateTable.Rows.Find("booked")["content"] = (int)ticketsStateTable.Rows.Find("booked")["content"] - 1;
                    ticketsStateTable.Rows.Find("unbooked")["content"] = (int)ticketsStateTable.Rows.Find("unbooked")["content"] + 1;
                    try
                    {
                        Adapter1.Update(ticketsTable);
                        Adapter2.Update(ticketsStateTable);
                        context.Response.Write("1");
                    }
                    catch (Exception ee)
                    {
                        context.Response.Write("0");
                    }
                    
                    return;
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void InitDB(HttpContext context)
        {
            conn = new OleDbConnection(connStr1);
            try
            {
                conn.Open();
                strSQL = "select * from ticketsTable";
                Adapter1 = new OleDbDataAdapter(strSQL, conn);
                //加了这句话就不会出现：当传递具有已修改行的 DataRow 集合时，更新要求有效的 UpdateCommand。
                OleDbCommandBuilder sqlBulider = new OleDbCommandBuilder(Adapter1);
                dataSet1 = new DataSet();
                Adapter1.Fill(dataSet1, "ticketsTable");
                ticketsTable = dataSet1.Tables["ticketsTable"];

                strSQL = "select * from ticketsStateTable";
                Adapter2 = new OleDbDataAdapter(strSQL, conn);
                //加了这句话就不会出现：当传递具有已修改行的 DataRow 集合时，更新要求有效的 UpdateCommand。
                OleDbCommandBuilder sqlBulider2 = new OleDbCommandBuilder(Adapter2);
                dataSet2 = new DataSet();
                Adapter2.Fill(dataSet2, "ticketsStateTable");
                ticketsStateTable = dataSet2.Tables["ticketsStateTable"];
                ticketsStateTable.PrimaryKey = new DataColumn[] { ticketsStateTable.Columns["state"] };

            }
            catch (Exception ee)
            {
                context.Response.Write("state=no&wrongcode=3"); //链接数据库出错！
            }
        }
    }
}