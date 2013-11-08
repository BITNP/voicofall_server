<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="unloginpage.aspx.cs" Inherits="voicofall_server.unloginpage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css_style/flat-ui.css" rel="Stylesheet" />
    <title>未登陆</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>您未登陆或登陆已过期！</p>
        <a href="login.aspx">点此</a>返回登陆界面
    </div>
    </form>
</body>
</html>
