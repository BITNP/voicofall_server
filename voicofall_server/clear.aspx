<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="clear.aspx.cs" Inherits="voicofall_server.clear" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
<link href="css_style/bootstrap.css" rel="Stylesheet" />
<link href="css_style/flat-ui.css" rel="Stylesheet" />
<link href="css_style/my_style.css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
    <h3>结束本次演出？</h3>
        <div>
            <p class="red">
                结束本次演出：
            </p>
            <ul>
                <li>清空订票记录</li>
                <li>设置订票时间为9999-12-31 00:00</li>
                <li>设置演出时间为9999-12-31 00:00</li>
                <li>清零总票量，剩余票量，已定票量</li>
                <li>设置演出场次名称为“无”</li>
                <li>设置订票类型为“普通票”</li>
            </ul>
        </div>
        <div>
            <asp:Button CssClass="btn btn-large btn-danger btnbox" ID="confirm" runat="server" Text="确认结束" OnClick="confirm_Click" />
            <asp:Button CssClass="btn btn-large btn-info btnbox" ID="cancel" runat="server" Text="取消" OnClick="cancel_Click" />
        </div>
    </div>
    </form>
</body>
</html>
