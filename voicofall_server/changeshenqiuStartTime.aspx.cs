using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace voicofall_server
{
    public partial class changeshenqiuStartTime : System.Web.UI.Page
    {
        public static readonly string connStr1 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath("~/App_Data/tickets.mdb");
        OleDbDataAdapter myAdapter;
        OleDbConnection conn;
        DataSet myDataSet;
        DataTable ticketsStateTable;
        string strSQL;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] == null)
            {
                Response.Redirect("unloginpage.aspx");
            }
            conn = new OleDbConnection(connStr1);
            try
            {
                conn.Open();
                strSQL = "select * from ticketsStateTable";
                myAdapter = new OleDbDataAdapter(strSQL, conn);
                //加了这句话就不会出现：当传递具有已修改行的 DataRow 集合时，更新要求有效的 UpdateCommand。
                OleDbCommandBuilder sqlBulider = new OleDbCommandBuilder(myAdapter);
                myDataSet = new DataSet();
                myAdapter.Fill(myDataSet, "ticketsStateTable");
                ticketsStateTable = myDataSet.Tables["ticketsStateTable"];
                ticketsStateTable.PrimaryKey = new DataColumn[] { ticketsStateTable.Columns["state"] };
                ShowTime();
            }
            catch (Exception ee)
            {
                Response.Write(ee.ToString());
            }
        }

        protected void changeButton_Click(object sender, EventArgs e)
        {
            string message = "修改成功！";
            if (Page.IsValid)
            {
                DataRow drow = ticketsStateTable.Rows.Find("shenqiuStartTime");
                if (this.newTimeLable.Text != "")
                {
                    drow["scontent"] = this.newTimeLable.Text;
                    myAdapter.Update(ticketsStateTable);
                }
                else
                {
                    message = "未填写时间\n返回修改前时间";
                }
                conn.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ok", "<script>alert(\"" + message + "\");self.location('admin.aspx');</script>");
            }
        }

        private void ShowTime()
        {
            string timeStr = ticketsStateTable.Rows.Find("shenqiuStartTime")["scontent"] as string;
            Regex reg;
            reg = new Regex(@"^\d{4}");
            int bookyear = Convert.ToInt32(reg.Match(timeStr).Value);
            reg = new Regex(@"(?<=-)\d+(?=-)");
            int bookmonth = Convert.ToInt32(reg.Match(timeStr).Value);
            reg = new Regex(@"(?<=-)\d+(?=\s)");
            int bookday = Convert.ToInt32(reg.Match(timeStr).Value);
            reg = new Regex(@"\d+(?=:)");
            int bookhour = Convert.ToInt32(reg.Match(timeStr).Value);
            reg = new Regex(@"\d+$");
            int bookmin = Convert.ToInt32(reg.Match(timeStr).Value);
            int booksecond = 0;
            this.nowTimeLable.Text = String.Format("{0}年{1}月{2}日 {3:00}:{4:00}:{5:00}", bookyear, bookmonth, bookday, bookhour, bookmin, booksecond);
        }

        protected void cancleButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx", false);
        }
    }
}