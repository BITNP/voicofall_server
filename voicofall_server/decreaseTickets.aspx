<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="decreaseTickets.aspx.cs" Inherits="voicofall_server.decreseTickets" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>修改剩余票数</title>
<link href="css_style/bootstrap.css" rel="Stylesheet" />
<link href="css_style/flat-ui.css" rel="Stylesheet" />
<link href="css_style/my_style.css" rel="Stylesheet" />
</head>
<body class="container">
    <h3>修改剩余票数</h3>
    <form id="form1" runat="server">
    <div>
        <div class="mtop10 bold green">
           <span>当前剩余票数：</span><asp:Label ID="nowunbookedLable" runat="server" Text=""></asp:Label>
        </div>
        <div class="mtop10">
           <span class="big">新的剩余票数：</span><asp:TextBox CssClass="tbgreen right10 " ID="newunbooked" runat="server" Text="" ValidationGroup="1"></asp:TextBox>
            <asp:RegularExpressionValidator ID="newunbookedValidator" runat="server"
                 ValidationExpression="^\d+$" Display="Dynamic"
                 ErrorMessage="请输入有效数字！" ControlToValidate="newunbooked" ValidationGroup="1" ForeColor="Red"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="不能为空" Display="Dynamic" ControlToValidate="newunbooked" ValidationGroup="1" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
        <div class="mtop10">
           <span class="big">确定剩余票数：</span><asp:TextBox CssClass="tbgreen right10 " ID="comparetonewunbooked" runat="server" Text="" ValidationGroup="1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="不能为空" Display="Dynamic" ControlToValidate="comparetonewunbooked" ValidationGroup="1" ForeColor="Red"></asp:RequiredFieldValidator>

            <asp:CompareValidator ID="comparetonewunbookedCompareValidator" runat="server"
                 ErrorMessage="输入数字和上面不符" ControlToCompare="newunbooked" ControlToValidate="comparetonewunbooked" ValidationGroup="1" ForeColor="Red"></asp:CompareValidator>
        </div>
        <div class="mtop10">
            <asp:Button CssClass="btn btn-large btn-primary btnbox" ID="changeButton" runat="server" Text="提交修改" OnClick="changeButton_Click" ValidationGroup="1" />
            <asp:Button CssClass="btn btn-large btn-primary btnbox" ID="cancleButton" runat="server" Text="取消" OnClick="cancleButton_Click" ValidationGroup="2" />
        </div>
    </div>
    </form>
</body>
</html>
