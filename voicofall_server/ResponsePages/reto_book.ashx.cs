using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

//finish

namespace voicofall_server.ResponsePages
{
    /// <summary>
    /// reto_html1 的摘要说明
    /// </summary>
    public class reto_html1 : IHttpHandler
    {

        public static readonly string connStr1 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath("~/App_Data/tickets.mdb");
        OleDbDataAdapter Adapter1;
        OleDbConnection conn;
        DataSet dataSet1;
        OleDbDataAdapter Adapter2;
        DataSet dataSet2;
        string strSQL;
        Random rd = new Random();

        public void ProcessRequest(HttpContext context)
        {
            
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache); //无缓存
            Random rd = new Random();
            string username = context.Request.Params["username"];
            string studentid = context.Request.Params["studentid"];
            string phonenumber = context.Request.Params["phonenumber"];
            string ticketid;
            string zonename;
            int unbooked;
            string ticketTag;
            string temp = ((DateTime.Now.Year % 100) < 10 ? "0" : "") + (DateTime.Now.Year % 100).ToString() + studentid.Substring(6) + phonenumber.Substring(9);
            int rdnum = rd.Next(100);
            temp += (rdnum < 10 ? "0" : "") + rdnum.ToString();
            int validCode = 0;
            for (int i = 0; i < temp.Length; i++)
            {
                //context.Response.Write("validcode: "+ validCode.ToString() + "+" + (i+1).ToString() +"*" + temp[i] +"\n");
                validCode += (Convert.ToInt32(temp[i])-48) * (i + 1);
            }
            ticketid = temp + ((validCode % 100) < 10 ? "0" : "") + (validCode % 100).ToString();
            string time = String.Format("{0:D4}-{1:D2}-{2:D2} {3:D2}:{4:D2}:{5:D2}",
                DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute,DateTime.Now.Second);
            InitDB(context);
            DataTable ticketsTable = dataSet1.Tables["ticketsTable"];
            DataTable ticketsStateTable = dataSet2.Tables["ticketsStateTable"];

            
           

            //检验是否还有空余票
            ticketsStateTable.PrimaryKey = new DataColumn[] { ticketsStateTable.Columns["state"] };
            DataRow rowunbooked = ticketsStateTable.Rows.Find("unbooked");
            if ((int)rowunbooked["content"] <= 0)
            {
                context.Response.Write("state=no&wrongcode=4"); //"票已订完
                return;
            }

            //检验是否到订票时间
            DataRow rowbooktime = ticketsStateTable.Rows.Find("nextBookTime");
            string booktime = rowunbooked["scontent"] as string;
            if (time.CompareTo(booktime) < 0)
            {
                context.Response.Write("state=no&wrongcode=5"); //"未到订票时间
                return;
            }

            //检验phonenumber是否存在
            for (int i = 0; i < ticketsTable.Rows.Count; i++)
            {
                DataRow row = ticketsTable.Rows[i];
                if ((string)(row["phonenumber"]) == phonenumber)
                {
                    context.Response.Write("state=no&wrongcode=1"); //"请勿重复订票！
                    return;
                }
            }

            
            //添加新行
            DataRow newRow = ticketsTable.NewRow();
            newRow["UID"] = ticketid;
            newRow["zonename"] = (ticketsStateTable.Rows.Find("nextBookzone"))["scontent"] as string;
            newRow["tickettag"] = (char)('A' + rd.Next((int)(ticketsStateTable.Rows.Find("tagCount"))["content"]));
            ticketTag = (string)newRow["tickettag"];
            zonename = newRow["zonename"] as string;
            newRow["username"] = username;
            newRow["studentid"] = studentid;
            newRow["phonenumber"] = phonenumber;
            newRow["addtime"] = time;
            ticketsTable.Rows.Add(newRow);
            unbooked = (int)(ticketsStateTable.Rows.Find("unbooked"))["content"];
            //修改剩余票数
            DataRow rowbooed = ticketsStateTable.Rows.Find("booked");
            rowbooed["content"] = Convert.ToInt32(rowbooed["content"]) + 1;
            rowunbooked["content"] = Convert.ToInt32(rowunbooked["content"]) - 1;
            try
            {
                Adapter1.Update(ticketsTable);
                Adapter2.Update(ticketsStateTable);
                
                
                conn.Close();
            }
            catch (Exception ee)
            {
                //context.Response.Write(ee.ToString());
                context.Response.Write("state=no&wrongcode=2");  //预订失败！请重试！
                return;
            }


            context.Response.Write(String.Format("state=yes&uid={0}&zonename={1}&tag={2}", ticketid, zonename, ticketTag));
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