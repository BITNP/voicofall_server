<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyTicket.aspx.cs" Inherits="voicofall_server.ResponsePages.MyTicket" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
<link href="../css_style/bootstrap.css" rel="Stylesheet" />
<link href="../css_style/flat-ui.css" rel="Stylesheet" />
<link href="../css_style/my_style.css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <div  style="text-align:center">
            <h3>我的门票</h3>
        </div >
        <div  id="wrongmessage" runat="server" class="purple"  style="text-align:center">
            <asp:Label ID="msgBox" runat="server" Text="请将二维码图片保存在手机上以验票使用</br>请尽量避免用手机直接拍摄，以免造成二维码不清晰，验票时延误进场"></asp:Label>
        </div>
        <div style="text-align:center;">
            <%--<div style="width:512px;height:512px;">--%>
                <asp:Image ID="qrcodeImage" runat="server" BorderColor="#2ECC71" BorderStyle="Solid" BorderWidth="5px" />
            <%--</div> --%>
        </div>
        
    </div>
    </form>
</body>
</html>
