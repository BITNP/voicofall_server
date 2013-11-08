<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="voicofall_server.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>登陆</title>
<link href="css_style/bootstrap.css" rel="Stylesheet" />
<link href="css_style/flat-ui.css" rel="Stylesheet" />
<link href="css_style/my_style.css" rel="Stylesheet" />
    
</head>
<body>
    <div class="container">
        <form id="form1" runat="server">
        <div>
            <h4>深秋歌会后台登陆</h4>
            <p>请输入用户名和密码</p>
        </div>
        <div>
            <asp:Label ForeColor="Red" ID="messageLabel" runat="server" Text=""></asp:Label>
        </div>
        <div class="top10">
            <asp:TextBox CssClass="tbgreen right10" ID="passportTextBox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必须填写用户名" ForeColor="Red"
                ControlToValidate="passportTextBox"></asp:RequiredFieldValidator>
        </div>

        <div class="top10">
            <asp:TextBox  CssClass="tbgreen right10" ID="passwordTextBox" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="必须填写密码" ForeColor="Red"
                ControlToValidate="passwordTextBox"></asp:RequiredFieldValidator>
        </div>
        <div class="top10">
            <asp:Button CssClass="btn btn-large btn-block btn-info" ID="loginButton" runat="server" Text="登陆" OnClick="loginButton_Click" Width="167px" />
        </div>
        </form>
    </div>
</body>
</html>
