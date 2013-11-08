<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addtickets.aspx.cs" Inherits="voicofall_server.addtickets" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>发放新票</title>
<link href="css_style/bootstrap.css" rel="Stylesheet" />
<link href="css_style/flat-ui.css" rel="Stylesheet" />
<link href="css_style/my_style.css" rel="Stylesheet" />
</head>
<body class="container">
    <div>
        <h3>发放新票</h3>
    </div>
    <form id="form1" runat="server">
    <div class="mtop10">
        <span class="right10 big">数量</span>
        <asp:TextBox CssClass="tbgreen right10 " ID="numberTextBox" runat="server" TextMode="SingleLine" ValidationGroup="1" ></asp:TextBox>
        <asp:RegularExpressionValidator ID="numberTextBoxValidator" runat="server"
             ValidationExpression="\d+" ControlToValidate="numberTextBox" Display="Dynamic" ValidationGroup="1" ForeColor="Red" >请输入数字！</asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="numberTextBoxValidator2" runat="server"
             ControlToValidate="numberTextBox" Display="Dynamic" ValidationGroup="1" ForeColor="Red">请填写数字</asp:RequiredFieldValidator>
        
    </div>
    <div class="mtop10">
        <span class="right10 big">时间</span>
        <asp:TextBox CssClass="tbgreen right10 top20" ID="timeTextBox" runat="server" TextMode="SingleLine" ValidationGroup="1" ></asp:TextBox>
        <asp:RegularExpressionValidator ID="timeTextBoxValidator" runat="server"
             ValidationExpression="^(\d{4})-(0\d|1[0-2])-(0[1-9]|[1-2]\d|3[0-1])\s([0-1]\d|2[0-4]):([0-5]\d|)$" ControlToValidate="timeTextBox" Display="Dynamic" ValidationGroup="1" ForeColor="Red">请填写有效时间！请确认格式为yyyy-mm-dd hh:mm。中间有空格</asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="timeTextBoxValidator2" runat="server"
             ControlToValidate="timeTextBox" Display="Dynamic" ValidationGroup="1" ForeColor="Red">请填写时间 格式为yyyy-mm-dd hh:mm</asp:RequiredFieldValidator>
    </div>
    <div class="mtop10">
        <span class="right10 big">类型</span>
        <asp:TextBox CssClass="tbgreen right10 top20" ID="zoneTextBox" runat="server" TextMode="SingleLine" ValidationGroup="1" ></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
             ValidationExpression="^.{1,8}$" ControlToValidate="zoneTextBox" Display="Dynamic" ValidationGroup="1" ForeColor="Red">请填写最多8个汉字</asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
             ControlToValidate="zoneTextBox" Display="Dynamic" ValidationGroup="1" ForeColor="Red">请填写订票类型</asp:RequiredFieldValidator>
    </div>
    <div class="mtop10">
        <asp:Button CssClass="btn btn-large btn-primary btnbox" ID="addButton" runat="server" Text="确定" OnClick="addButton_Click" ValidationGroup="1"/>
        <asp:Button CssClass="btn btn-large btn-primary btnbox" ID="cancelButton" runat="server" Text="取消" ValidationGroup="2" OnClick="cancelButton_Click"/>
    </div>
    <div>
        <asp:Label ID="testLabel" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
