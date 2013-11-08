using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Drawing;

namespace voicofall_server
{
    public partial class manageAdmin : System.Web.UI.Page
    {
        public static readonly string connStr1 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath("~/App_Data/tickets.mdb");
        OleDbDataAdapter adapter1;
        DataSet dataSet1;
        OleDbConnection conn;
        string strSQL;
        DataTable adminsTable;
        bool cancelDelete = false;
        bool cancel = false;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["passport"] == null || (string)Session["authority"] == "low" || (string)Session["authority"] == "high")
            {
                Response.Redirect("unloginpage.aspx");
            }
            InitDB();
            
        }

        private void InitDB()
        {
            conn = new OleDbConnection(connStr1);
            conn.Open();
            strSQL = "select * from adminsTable";
            adapter1 = new OleDbDataAdapter(strSQL, conn);
            //加了这句话就不会出现：当传递具有已修改行的 DataRow 集合时，更新要求有效的 UpdateCommand。
            OleDbCommandBuilder sqlBulider = new OleDbCommandBuilder(adapter1);
            dataSet1 = new DataSet();
            adapter1.Fill(dataSet1, "adminsTable");
            adminsTable = dataSet1.Tables["adminsTable"];
            adminsTable.PrimaryKey = new DataColumn[] { adminsTable.Columns["账号"] };
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.AccessDataSource1.Insert();
            }
            catch (Exception ee)
            {
                this.wrongmsglabel.Text = "添加失败！请检查账号是否已存在！";
            }
            
        }

        protected void AccessDataSource1_Deleting(object sender, SqlDataSourceCommandEventArgs e)
        {
            if (cancelDelete)
                e.Cancel = true;
            cancelDelete = false;
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if ((string)Session["authority"] == "admin")
                return;
            DataRow row = adminsTable.Rows[e.RowIndex];
            if ((string)row["权限"] == "super")
            {
                e.Cancel = true;
                cancelDelete = true;
                this.wrongmsglabel.Text = "不能删除权限为管理员的账号！";
            }
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if ((string)Session["authority"] == "admin")
                return;
            DataRow row = adminsTable.Rows[e.RowIndex];
            if ((string)e.NewValues["权限"] == "super")
            {
                e.Cancel = true;
                cancel = true;
                this.wrongmsglabel.Text = "不能将其他账户的权限设为管理员！";
            }
        }

        protected void AccessDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
            if (cancel)
                e.Cancel = true;
            cancel = false;
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx");
        }
    }
}