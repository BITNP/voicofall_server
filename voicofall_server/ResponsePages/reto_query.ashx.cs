using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace voicofall_server.ResponsePages
{
    /// <summary>
    /// reto_query 的摘要说明
    /// </summary>
    public class reto_query : IHttpHandler
    {
        public static readonly string connStr1 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath("~/App_Data/tickets.mdb");
        OleDbDataAdapter Adapter1;
        OleDbConnection conn;
        DataSet dataSet1;
        string strSQL;
        OleDbDataAdapter Adapter2;
        DataSet dataSet2;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string username = context.Request.Params["username"];
            string studentid = context.Request.Params["studentid"];
            string phonenumber = context.Request.Params["phonenumber"];
            string shenqiuStartTime;
            string shenqiuName;
            string zone;
            InitDB(context);
            DataTable ticketsTable = dataSet1.Tables["ticketsTable"];
            DataTable ticketsStateTable = dataSet2.Tables["ticketsStateTable"];
            zone = (ticketsStateTable.Rows.Find("nextBookZone"))["scontent"] as string;
            shenqiuStartTime = (ticketsStateTable.Rows.Find("shenqiuStartTime"))["scontent"] as string;
            shenqiuName = (ticketsStateTable.Rows.Find("shenqiuName"))["scontent"] as string;
            foreach (DataRow row in ticketsTable.Rows)
            {
                if ((string)(row["username"]) == username &&
                    (string)(row["studentid"]) == studentid &&
                    (string)(row["phonenumber"]) == phonenumber)
                {
                    context.Response.Write(String.Format("uid={0}&zone={1}&shenqiuStartTime={2}&shenqiuName={3}", row["UID"], zone, shenqiuStartTime, shenqiuName));
                    return;
                }
            }
            context.Response.Write("uid=0");
            
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

                strSQL = "select * from ticketsStateTable";
                Adapter2 = new OleDbDataAdapter(strSQL, conn);
                //加了这句话就不会出现：当传递具有已修改行的 DataRow 集合时，更新要求有效的 UpdateCommand。
                OleDbCommandBuilder sqlBulider2 = new OleDbCommandBuilder(Adapter2);
                dataSet2 = new DataSet();
                Adapter2.Fill(dataSet2, "ticketsStateTable");
                conn.Close();
            }
            catch (Exception ee)
            {
                context.Response.Write("state=no&wrongcode=3"); //链接数据库出错！
            }
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