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
    public partial class login : System.Web.UI.Page
    {
        public static readonly string connStr1 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath("~/App_Data/tickets.mdb");
        OleDbDataAdapter adapter;
        OleDbConnection conn;
        DataSet dataSet;
        DataTable adminsTable; 
        string strSQL;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitDB();
        }

        public void InitDB()
        {
            conn = new OleDbConnection(connStr1);
            try
            {
                conn.Open();
                strSQL = "select * from adminsTable";
                adapter = new OleDbDataAdapter(strSQL, conn);
                //加了这句话就不会出现：当传递具有已修改行的 DataRow 集合时，更新要求有效的 UpdateCommand。
                OleDbCommandBuilder sqlBulider = new OleDbCommandBuilder(adapter);
                dataSet = new DataSet();
                adapter.Fill(dataSet, "adminsTable");
                adminsTable = dataSet.Tables["adminsTable"];
                adminsTable.PrimaryKey = new DataColumn[] { adminsTable.Columns["账号"] };


            }
            catch (Exception ee)
            {
                Response.Write(ee.ToString()); //链接数据库出错！
            }
        }

        protected void loginButton_Click(object sender, EventArgs e)
        {
            string passport = null;
            string password = null;
            string authority = null;
            foreach (DataRow row in adminsTable.Rows)
            {
                if ((string)row["账号"] == this.passportTextBox.Text)
                {
                    passport = row["账号"] as string;
                    if ((string)row["密码"] == this.passwordTextBox.Text)
                    {
                        password = row["密码"] as string;
                        passport = row["账号"] as string;
                        authority = row["权限"] as string;
                    }
                    break;
                }
            }
            if (passport == null)
                this.messageLabel.Text = "无效用户名!";
            else
                if (password == null)
                    this.messageLabel.Text = "密码错误!";
                else
                {
                    Session.Clear();
                    Session.Timeout = 10;
                    Session["passport"] = passport;
                    Session["password"] = password;
                    Session["authority"] = authority;
                    Response.Redirect("admin.aspx");
                }
        }
    }
}