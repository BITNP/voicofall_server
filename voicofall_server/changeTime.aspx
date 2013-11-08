<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="changeTime.aspx.cs" Inherits="voicofall_server.changeTime" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>修改订票时间</title>
<link href="css_style/bootstrap.css" rel="Stylesheet" />
<link href="css_style/flat-ui.css" rel="Stylesheet" />
<link href="css_style/my_style.css" rel="Stylesheet" />
</head>
<body class="container">
    <h3>修改订票时间</h3>
    <form id="form1" runat="server">
    <div class="green bold">
       <span>当前开放订票时间：</span><asp:Label ID="nowTimeLable" runat="server" Text=""></asp:Label>
    </div>
    <div class="top10">
       <span class="big">新的开放时间：</span><asp:TextBox CssClass="tbgreen right10" ID="newTimeLable" runat="server" Text="" ValidationGroup="1"></asp:TextBox>
        <asp:RegularExpressionValidator ID="newTimeLableValidator" runat="server"
             ValidationExpression="^(\d{4})-(0\d|1[0-2])-(0[1-9]|[1-2]\d|3[0-1])\s([0-1]\d|2[0-4]):([0-5]\d|)$" Display="Dynamic"
             ErrorMessage="请填写有效时间！请确认格式为yyyy-mm-dd hh:mm。中间有空格" ControlToValidate="newTimeLable" ValidationGroup="1" ForeColor="Red"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="不能为空" Display="Dynamic" ControlToValidate="newTimeLable" ValidationGroup="1" ForeColor="Red"></asp:RequiredFieldValidator>
    </div>
    <div class="top10">
       <span class="big">确定开放时间：</span><asp:TextBox CssClass="tbgreen right10" ID="comparetoNewTimeLable" runat="server" Text="" ValidationGroup="1"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="不能为空" Display="Dynamic" ControlToValidate="comparetoNewTimeLable" ValidationGroup="1" ForeColor="Red"></asp:RequiredFieldValidator>

        <asp:CompareValidator ID="comparetoNewTimeLableCompareValidator" runat="server"
             ErrorMessage="输入时间与上面不符合" ControlToCompare="newTimeLable" ControlToValidate="comparetoNewTimeLable" ValidationGroup="1" ForeColor="Red"></asp:CompareValidator>
    </div>
    <div class="top10">
        <asp:Button CssClass="btn btn-large btn-primary btnbox" ID="changeButton" runat="server" Text="提交修改" OnClick="changeButton_Click" ValidationGroup="1" />
        <asp:Button CssClass="btn btn-large btn-primary btnbox" ID="cancleButton" runat="server" Text="取消" OnClick="cancleButton_Click" ValidationGroup="2" />
    </div>
    </form>
</body>
</html>
