using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZXing.QrCode;
using ZXing.Common;
using System.Drawing;
using ZXing.QrCode.Internal;
using ZXing;
using System.IO;
using System.Drawing.Imaging;
using System.Data;
using System.Data.OleDb;

namespace voicofall_server.ResponsePages
{
    /// <summary>
    /// reto_ticket 的摘要说明
    /// </summary>
    public class reto_ticket : IHttpHandler
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

        Graphics gs;

        string uid;
        string username;
        string studentid;
        string phonenumber;
        string zonename;
        string shenqiuName;
        string shenqiutime;

        string codeString;

        static int width = 512;
        static int height = 512;

        static int codeWidth = 470;
        static int codeHeight = 420;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/bmp";
            InitDB();
            this.uid = context.Request.Params["uid"];

            LoadTicketInfo(uid, context);
            codeString = String.Format("{0}&{1}&{2}&{3}&{4}&{5}&{6}",uid,username,studentid,phonenumber,zonename,shenqiutime,shenqiuName);
            IDictionary<EncodeHintType, object> hints = new Dictionary<EncodeHintType,object>();
            hints.Add(EncodeHintType.CHARACTER_SET,"UTF-8");
            MemoryStream ms = new MemoryStream();
            BitMatrix bitMatrix = new MultiFormatWriter().encode(codeString, BarcodeFormat.QR_CODE, width, height, hints);
            Bitmap bitmap = toBitmap(bitMatrix);
            bitmap.Save(ms,ImageFormat.Png);
            context.Response.BinaryWrite(ms.ToArray());
        }


        private Bitmap toBitmap(BitMatrix matrix)
        {
            int width = matrix.Width;
            int height = matrix.Height;
            Bitmap bmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            for (int x = 0; x < width; x++)
            {

                for (int y = 0; y < height; y++)
                {
                    bmap.SetPixel(x, y, matrix[x, y] != false ? ColorTranslator.FromHtml("0xFF000000") : ColorTranslator.FromHtml("0xFFedeac1"));
                }
            }
            gs = Graphics.FromImage(bmap);
            SizeF shenqiuNameSize = gs.MeasureString(shenqiuName, new Font("隶书", 24));
            SizeF zoneSize = gs.MeasureString(zonename, new Font("隶书", 24));
            SizeF uidSize = gs.MeasureString(uid, new Font("隶书", 16));
            DrawText(bmap, "深秋歌会", 45, 10, 32);
            DrawText(bmap, shenqiuName, codeWidth - (int)shenqiuNameSize.Width, 18, 24);
            DrawText(bmap, "ID:"+uid, 45, codeHeight + (int)uidSize.Height + 22, 16);
            DrawText(bmap, zonename, codeWidth - (int)zoneSize.Width, codeHeight + (int)zoneSize.Height, 24);
            return bmap;
        }

        private void DrawText(Bitmap img,string s, int x, int y, int fontsize)
        {
            gs = Graphics.FromImage(img);
            Font font = new Font("隶书", fontsize);
            Brush br = new SolidBrush(Color.Black);
            gs.DrawString(s, font, br, x, y);
            gs.Dispose();
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
            conn.Close();
        }

        private void LoadTicketInfo(string uid, HttpContext context)
        {
            try
            {
                DataRow ticketRow = ticketsTable.Rows.Find(uid);
                username = ticketRow["username"] as string;
                studentid = ticketRow["studentid"] as string;
                phonenumber = ticketRow["phonenumber"] as string;
                zonename = ticketRow["zonename"] as string;
                shenqiutime = (ticketsStateTable.Rows.Find("shenqiuStartTime"))["scontent"] as string;
                shenqiuName = (ticketsStateTable.Rows.Find("shenqiuName"))["scontent"] as string;
            }
            catch (Exception ee)
            {
                Bitmap bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                DrawText(bitmap, "生成失败", 165, 10, 32);
                MemoryStream ms = new MemoryStream();
                bitmap.Save(ms, ImageFormat.Png);
                context.Response.BinaryWrite(ms.ToArray());
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