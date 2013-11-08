﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace voicofall_server
{
    public partial class decreseTickets : System.Web.UI.Page
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
                ShowUnbooked();
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
                DataRow unbookedrow = ticketsStateTable.Rows.Find("unbooked");
                DataRow allrow = ticketsStateTable.Rows.Find("all");
                if (this.newunbooked.Text != "")
                {
                    allrow["content"] = Convert.ToInt32(this.newunbooked.Text) - (int)unbookedrow["content"] + (int)allrow["content"];
                    unbookedrow["content"] = Convert.ToInt32(this.newunbooked.Text);
                    myAdapter.Update(ticketsStateTable);
                }
                else
                {
                    message = "未填写\n返回修改前状态";
                }
                conn.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ok", "<script>alert(\"" + message + "\");self.location('admin.aspx');</script>");
            }
        }

        private void ShowUnbooked()
        {
            int unbooked = (int)(ticketsStateTable.Rows.Find("unbooked")["content"]);
            this.nowunbookedLable.Text = unbooked.ToString();
        }

        protected void cancleButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx", false);
        }
        
    }
}