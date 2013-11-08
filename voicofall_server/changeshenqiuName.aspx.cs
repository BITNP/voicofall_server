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
    public partial class changeshenqiuName : System.Web.UI.Page
    {
        public static readonly string connStr1 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath("~/App_Data/tickets.mdb");
        OleDbDataAdapter adapter1;
        DataSet dataSet1;
        OleDbConnection conn;
        string strSQL;
        DataTable ticketsStateTable;
        DataRow modifyRow;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] == null)
            {
                Response.Redirect("unloginpage.aspx");
            }
            InitDB();
            ShowShenqiuNameNow();
        }

        private void InitDB()
        {
            conn = new OleDbConnection(connStr1);
            conn.Open();
            strSQL = "select * from ticketsStateTable";
            adapter1 = new OleDbDataAdapter(strSQL, conn);
            //加了这句话就不会出现：当传递具有已修改行的 DataRow 集合时，更新要求有效的 UpdateCommand。
            OleDbCommandBuilder sqlBulider = new OleDbCommandBuilder(adapter1);
            dataSet1 = new DataSet();
            adapter1.Fill(dataSet1, "ticketsStateTable");
            ticketsStateTable = dataSet1.Tables["ticketsStateTable"];
            ticketsStateTable.PrimaryKey = new DataColumn[] { ticketsStateTable.Columns["state"] };
        }

        protected void changeButton_Click(object sender, EventArgs e)
        {
            modifyRow["scontent"] = newshenqiuName.Text;
            adapter1.Update(ticketsStateTable);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ok", "<script>alert(\"" + "修改成功" + "\");self.location('admin.aspx');</script>");
        }

        protected void cancleButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx");
        }

        private void ShowShenqiuNameNow()
        {
            modifyRow = ticketsStateTable.Rows.Find("shenqiuName");
            string zoneNow = (string)(modifyRow["scontent"]);
            this.nowshenqiuNameLabel.Text = zoneNow;
        }
    }
}