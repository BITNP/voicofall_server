using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace voicofall_server.ResponsePages
{
    /// <summary>
    /// ResponseTicketsInfo 的摘要说明
    /// </summary>
    public class ResponseTicketsInfo : IHttpHandler
    {
        public static readonly string connStr1 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath("~/App_Data/tickets.mdb");
        OleDbConnection conn;
        OleDbDataAdapter Adapter2;
        DataSet dataSet2;
        string strSQL;

        private int unbooked;
        private string shenqiustarttime;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            GetTicketsInfoFromDB(context);
            context.Response.Write(unbooked.ToString() +"&"+shenqiustarttime);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void GetTicketsInfoFromDB(HttpContext context)
        {
            conn = new OleDbConnection(connStr1);
            try
            {
                conn.Open();
                strSQL = "select * from ticketsStateTable";
                Adapter2 = new OleDbDataAdapter(strSQL, conn);
                //加了这句话就不会出现：当传递具有已修改行的 DataRow 集合时，更新要求有效的 UpdateCommand。
                OleDbCommandBuilder sqlBulider2 = new OleDbCommandBuilder(Adapter2);
                dataSet2 = new DataSet();
                Adapter2.Fill(dataSet2, "ticketsStateTable");
                DataTable ticketsStateTable = dataSet2.Tables["ticketsStateTable"];

                //检验是否还有空余票
                ticketsStateTable.PrimaryKey = new DataColumn[] { ticketsStateTable.Columns["state"] };
                //读取下次订票时间
                DataRow tempRow = ticketsStateTable.Rows.Find("unbooked");
                unbooked = (int)tempRow["content"];
                tempRow = ticketsStateTable.Rows.Find("shenqiuStartTime");
                shenqiustarttime = tempRow["scontent"] as string;
                conn.Close();
            }
            catch (Exception ee)
            {
                context.Response.Write("state=no&wrongcode=3"); //初始化数据库出错！
            }
        }
    }
}