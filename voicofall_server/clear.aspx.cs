using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;


namespace voicofall_server
{
    public partial class clear : System.Web.UI.Page
    {
        public static readonly string connStr1 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath("~/App_Data/tickets.mdb");
        OleDbDataAdapter adapter1;
        DataSet dataSet1;
        OleDbDataAdapter adapter2;
        DataSet dataSet2;
        OleDbConnection conn;
        string strSQL;
        DataTable ticketsStateTable;
        DataTable ticketsTable;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] == null)
            {
                Response.Redirect("unloginpage.aspx");
            }
            InitDB();
        }

        protected void confirm_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in ticketsTable.Rows)
            {
                row.Delete();
            }
            (ticketsStateTable.Rows.Find("all"))["content"] = 0;
            (ticketsStateTable.Rows.Find("booked"))["content"] = 0;
            (ticketsStateTable.Rows.Find("unbooked"))["content"] = 0;
            (ticketsStateTable.Rows.Find("nextBookTime"))["scontent"] = "9999-12-31 00:00";
            (ticketsStateTable.Rows.Find("shenqiuStartTime"))["scontent"] = "9999-12-31 00:00";
            (ticketsStateTable.Rows.Find("shenqiuName"))["scontent"] = "无";
            (ticketsStateTable.Rows.Find("nextBookZone"))["scontent"] = "普通票";
            adapter1.Update(ticketsTable);
            adapter2.Update(ticketsStateTable);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ok", "<script>alert(\"" + "本次演出已结束,所有信息已清空" + "\");self.location('admin.aspx');</script>");
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx");
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

            strSQL = "select * from ticketsStateTable";
            adapter2 = new OleDbDataAdapter(strSQL, conn);
            //加了这句话就不会出现：当传递具有已修改行的 DataRow 集合时，更新要求有效的 UpdateCommand。
            OleDbCommandBuilder sqlBulider2 = new OleDbCommandBuilder(adapter2);
            dataSet2 = new DataSet();
            adapter2.Fill(dataSet2, "ticketsStateTable");
            ticketsStateTable = dataSet2.Tables["ticketsStateTable"];
            ticketsStateTable.PrimaryKey = new DataColumn[] { ticketsStateTable.Columns["state"] };
        }
    }
}