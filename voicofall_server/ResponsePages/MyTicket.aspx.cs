using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace voicofall_server.ResponsePages
{
    public partial class MyTicket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            string uid = Request.Params["uid"];
            if (uid == null)
            {
                this.msgBox.Text = "请不要直接访问此网页！否则会造成生成失败！";
                return;
            }
            this.qrcodeImage.ImageUrl = "reto_ticket.ashx?uid=" + uid;
        }
    }
}