using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace voicofall_server
{
    public partial class addtickets : System.Web.UI.Page
    {
        public static readonly string connStr1 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath("~/App_Data/tickets.mdb");
        OleDbDataAdapter myAdapter;
        OleDbConnection conn;
        DataSet myDataSet;
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
                

            }
            catch (Exception ee)
            {
                Response.Write(ee.ToString());
            }
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            int newTicketsCount;
            string newTicketsStartTime;
            string newZone;
            if (Page.IsValid == true)
            {
                newTicketsCount = Convert.ToInt32(this.numberTextBox.Text);
                newTicketsStartTime = this.timeTextBox.Text;
                newZone = this.zoneTextBox.Text;
                //获取dataTable
                DataTable ticketsStateTable = myDataSet.Tables["ticketsStateTable"];
                DataRow drow;
                ticketsStateTable.PrimaryKey = new DataColumn[] { ticketsStateTable.Columns["state"] };
                drow = ticketsStateTable.Rows.Find("all");
                drow["content"] = Convert.ToInt32(drow["content"]) + newTicketsCount;
                drow = ticketsStateTable.Rows.Find("unbooked");
                drow["content"] = Convert.ToInt32(drow["content"]) + newTicketsCount;
                drow = ticketsStateTable.Rows.Find("nextBookTime");
                drow["scontent"] = newTicketsStartTime;
                drow = ticketsStateTable.Rows.Find("nextBookZone");
                drow["scontent"] = newZone;
                try
                {
                    myAdapter.Update(ticketsStateTable);
                    conn.Close();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ok", "<script>alert(\"" + "添加成功" + "\");self.location('admin.aspx');</script>");

                }
                catch (Exception ee)
                {
                    Response.Write("添加失败！请重试！");
                    //Response.Write(ee.ToString());
                }
            }   
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx");
        }

    }
}