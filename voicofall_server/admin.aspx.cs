using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Drawing;

namespace voicofall_server
{
    public partial class admin : System.Web.UI.Page
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

        OleDbDataAdapter adapterHistory;
        DataSet dataSetHistory;
        DataTable history;
        string thisAuthority;

        public int year;
        public int month;
        public int day;
        public int hour;
        public int min;
        public int second;
        private string shenqiuStartTime;

        

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthorityCheck();
            conn = new OleDbConnection(connStr1);
            try
            {
                InitDB();
                if (Session["isLogin"] == null)
                {
                    LogToHistroy("登入", DateTime.Now);
                    Session["isLogin"] = "true";
                }
                //显示名单
                //this.TicketsGridView.DataSource = dataSet1;
                //this.TicketsGridView.DataBind();

                //显示票务信息
                this.allLable.Text = ((int)ticketsStateTable.Rows.Find("all")["content"]).ToString();
                this.bookedLable.Text = ((int)ticketsStateTable.Rows.Find("booked")["content"]).ToString();
                this.unbookedLable.Text = ((int)ticketsStateTable.Rows.Find("unbooked")["content"]).ToString();
                this.zoneLabel.Text = ticketsStateTable.Rows.Find("nextBookZone")["scontent"] as string;
                this.shenqiuNameLabel.Text = ticketsStateTable.Rows.Find("shenqiuName")["scontent"] as string;
                this.tagCountLabel.Text = ((int)ticketsStateTable.Rows.Find("tagCount")["content"]).ToString();
                //显示时间
                ShowTime();
            }
            catch (Exception ee)
            {
                Response.Write(ee.ToString());
            }
            this.TicketsGridView.Columns[0].Visible = false;
        }

        private void AuthorityCheck()
        {
            if (Session["passport"] == null)
            {
                Response.Redirect("unloginpage.aspx");
            }
            this.passportLabel.Text = Session["passport"] as string;
            switch (Session["authority"] as string)
            {
                case "super": 
                    this.authorityLabel.Text = "管理员";
                    thisAuthority = "管理员";
                    this.authorityLabel.ForeColor = Color.Purple;
                    if (this.modifyButton.Text == "完成修改")
                    {
                        this.TicketsGridView.Columns[0].Visible = true;
                    }
                    break;
                case "high": 
                    this.authorityLabel.Text = "高级账户";
                    thisAuthority = "高级账户";
                    this.modifyButton.Visible = false;
                    this.superButtons.Visible = false;
                    break;
                case "low": 
                    this.authorityLabel.Text = "访客";
                    thisAuthority = "访客";
                    this.modifyButton.Visible = false;
                    this.superButtons.Visible = false;
                    this.highButtons.Visible = false;
                    break;
                case "admin":
                    this.authorityLabel.Text = "最高管理员";
                    thisAuthority = "最高管理员";
                    this.authorityLabel.ForeColor = Color.Red;
                    if (this.modifyButton.Text == "完成修改")
                    {
                        this.TicketsGridView.Columns[0].Visible = true;
                    }
                    break;
            }
            
   
        }

        private void ShowTime()
        {
            string timeStr = ticketsStateTable.Rows.Find("nextBookTime")["scontent"] as string;
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
            this.booktimeLable.Text = String.Format("{0}年{1}月{2}日 {3}:{4:00}:{5:00}",bookyear,bookmonth,bookday,bookhour,bookmin,booksecond);
            shenqiuStartTime = ticketsStateTable.Rows.Find("shenqiuStartTime")["scontent"] as string;
            this.shenqiuStartTimeLabel.Text = shenqiuStartTime;
            year = DateTime.Now.Year;
            month = DateTime.Now.Month;
            day = DateTime.Now.Day;
            hour = DateTime.Now.Hour;
            min = DateTime.Now.Minute;
            second = DateTime.Now.Second;
        }

        private void InitDB()
        {
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

            //获取历史记录表
            strSQL = "select * from history";
            adapterHistory = new OleDbDataAdapter(strSQL, conn);
            //加了这句话就不会出现：当传递具有已修改行的 DataRow 集合时，更新要求有效的 UpdateCommand。
            OleDbCommandBuilder sqlBulider3 = new OleDbCommandBuilder(adapterHistory);
            dataSetHistory = new DataSet();
            adapterHistory.Fill(dataSetHistory, "history");
            history = dataSetHistory.Tables["history"];
            history.PrimaryKey = new DataColumn[] { history.Columns["发生时间"] };
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            LogToHistroy("发放新票", DateTime.Now);
            Response.Redirect("addtickets.aspx", false);
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            
        }

        //按下删除按钮时发生
        protected void TicketsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            LogToHistroy("删除记录 " + (ticketsTable.Rows[e.RowIndex])["username"] + " ; " +
                (ticketsTable.Rows[e.RowIndex])["studentid"] + " ; " +
                (ticketsTable.Rows[e.RowIndex])["phonenumber"], DateTime.Now);
            ////删除ticketsTable中选中的那一行
            //ticketsTable.Rows[e.RowIndex].Delete();
            ////修改ticketsStateTable中的信息
            ticketsStateTable.Rows.Find("booked")["content"] = (int)ticketsStateTable.Rows.Find("booked")["content"] - 1;
            ticketsStateTable.Rows.Find("unbooked")["content"] = (int)ticketsStateTable.Rows.Find("unbooked")["content"] + 1;
            try
            {
                //adapter1.Update(ticketsTable);
                adapter2.Update(ticketsStateTable);
                //更新票务名单
                //this.TicketsGridView.DataBind();
                //更新票务lable信息
                this.bookedLable.Text = ((int)ticketsStateTable.Rows.Find("booked")["content"]).ToString();
                this.unbookedLable.Text = ((int)ticketsStateTable.Rows.Find("unbooked")["content"]).ToString();

                this.TicketsGridView.Columns[0].Visible = true;

            }
            catch (Exception ee)
            {
            }
        }

        protected void changetimeButton_Click(object sender, EventArgs e)
        {
            LogToHistroy("修改订票时间", DateTime.Now);
            Response.Redirect("changeTime.aspx", false);
        }

        protected void decreseButton_Click(object sender, EventArgs e)
        {
            LogToHistroy("修改剩余票数", DateTime.Now);
            Response.Redirect("decreaseTickets.aspx", false);
        }

        protected void modifyButton_Click(object sender, EventArgs e)
        {
            if (this.modifyButton.Text == "修改名单")
            {
                this.TicketsGridView.Columns[0].Visible = true;
                this.modifyButton.Text = "完成修改";
                LogToHistroy("开始修改名单", DateTime.Now);
            }
            else
            {
                this.TicketsGridView.Columns[0].Visible = false;
                this.modifyButton.Text = "修改名单";
                LogToHistroy("结束修改名单", DateTime.Now);
            }
        }

        protected void loginoutButton_Click(object sender, EventArgs e)
        {
            LogToHistroy("注销", DateTime.Now);
            Session.Clear();
            Response.Redirect("login.aspx");
        }

        protected void changeshenqiutimeButton_Click(object sender, EventArgs e)
        {
            LogToHistroy("修改深秋歌会演出时间", DateTime.Now);
            Response.Redirect("changeshenqiuStartTime.aspx");
        }

        protected void manageAdminButton_Click(object sender, EventArgs e)
        {
            LogToHistroy("进入后台账号管理", DateTime.Now);
            Response.Redirect("manageAdmin.aspx");
        }

        protected void changePasswordButton_Click(object sender, EventArgs e)
        {
            LogToHistroy("修改密码", DateTime.Now);
            Response.Redirect("changePassword.aspx");
        }

        private void LogToHistroy(string operation, DateTime time)
        {
            DataRow newrow = history.NewRow();
            newrow["操作账户"] = Session["passport"] as string;
            newrow["操作权限"] = thisAuthority;
            newrow["操作"] = operation;
            newrow["发生时间"] = time.ToString("yyyy-MM-dd HH:mm:ss") + " (" + time.Millisecond.ToString() + ")";
            history.Rows.Add(newrow);
            adapterHistory.Update(history);
        }

        protected void changeZone_Click(object sender, EventArgs e)
        {
            LogToHistroy("修改订票类型", DateTime.Now);
            Response.Redirect("changeZone.aspx");
        }

        protected void changeshenqiuName_Click(object sender, EventArgs e)
        {
            LogToHistroy("修改场次名称", DateTime.Now);
            Response.Redirect("changeshenqiuName.aspx");
        }

        protected void clearButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("clear.aspx");
        }

        protected void changeTagCountButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("changeTagCount.aspx");
        }

        protected void TicketsGridView_PageIndexChanged(object sender, EventArgs e)
        {
            if (this.modifyButton.Text == "修改名单")
            {
                this.TicketsGridView.Columns[0].Visible = false;
            }
            else
            {
                this.TicketsGridView.Columns[0].Visible = true;
            }
        }
    }
}