<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="changePassword.aspx.cs" Inherits="voicofall_server.changePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>修改密码</title>
<link href="css_style/bootstrap.css" rel="Stylesheet" />
<link href="css_style/flat-ui.css" rel="Stylesheet" />
<link href="css_style/my_style.css" rel="Stylesheet" />
</head>
<body class="container">
    <h3>修改密码</h3>
    <form id="form1" runat="server">
    <div>
        <div class="mtop10 bold green">
           <span>当前密码：</span><asp:Label ID="nowPasswordLabel" runat="server" Text=""></asp:Label>
        </div>
        <div class="mtop10">
           <span class="big">新的密码：</span><asp:TextBox CssClass="tbgreen right10 " ID="newPassword" runat="server" Text="" ValidationGroup="1"></asp:TextBox>
            <asp:RegularExpressionValidator ID="newPasswordValidator" runat="server"
                 ValidationExpression="^[a-z0-9A-Z_]{6,}$" Display="Dynamic"
                 ErrorMessage="请输入六位以上有效数字或字母！" ControlToValidate="newPassword" ValidationGroup="1" ForeColor="Red"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="不能为空" Display="Dynamic" ControlToValidate="newPassword" ValidationGroup="1" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
        <div class="mtop10">
           <span class="big">确认密码：</span><asp:TextBox CssClass="tbgreen right10 " ID="comparetonewPassword" runat="server" Text="" ValidationGroup="1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="不能为空" Display="Dynamic" ControlToValidate="comparetonewPassword" ValidationGroup="1" ForeColor="Red"></asp:RequiredFieldValidator>

            <asp:CompareValidator ID="comparetonewPasswordCompareValidator" runat="server"
                 ErrorMessage="确认密码和上面不符" ControlToCompare="newPassword" ControlToValidate="comparetonewPassword" ValidationGroup="1" ForeColor="Red"></asp:CompareValidator>
        </div>
        <div class="mtop10">
            <asp:Button CssClass="btn btn-large btn-primary btnbox" ID="changeButton" runat="server" Text="提交修改" OnClick="changeButton_Click" ValidationGroup="1" />
            <asp:Button CssClass="btn btn-large btn-primary btnbox" ID="cancleButton" runat="server" Text="取消" OnClick="cancleButton_Click" ValidationGroup="2" />
        </div>
    </div>
    </form>
</body>
</html>