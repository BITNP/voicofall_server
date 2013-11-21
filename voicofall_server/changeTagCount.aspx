<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="changeTagCount.aspx.cs" Inherits="voicofall_server.changeTagCount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>修改检票口数量</title>
<link href="css_style/bootstrap.css" rel="Stylesheet" />
<link href="css_style/flat-ui.css" rel="Stylesheet" />
<link href="css_style/my_style.css" rel="Stylesheet" />
</head>
<body class="container">
    <h3>修改检票口数量</h3>
    <form id="form1" runat="server">
    <div>
        <div class="mtop10 bold green">
           <span>当前检票口数量：</span><asp:Label ID="nowTagCountLable" runat="server" Text=""></asp:Label>
        </div>
        <div class="mtop10">
           <span class="big">新的检票口数量：</span><asp:TextBox CssClass="tbgreen right10 " ID="newTagCount" runat="server" Text="" ValidationGroup="1"></asp:TextBox>
            <asp:RegularExpressionValidator ID="newTagCountValidator" runat="server"
                 ValidationExpression="^\d+$" Display="Dynamic"
                 ErrorMessage="请输入有效数字！" ControlToValidate="newTagCount" ValidationGroup="1" ForeColor="Red"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="不能为空" Display="Dynamic" ControlToValidate="newTagCount" ValidationGroup="1" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
        <div class="mtop10">
           <span class="big">确定检票口数量：</span><asp:TextBox CssClass="tbgreen right10 " ID="comparetonewTagCount" runat="server" Text="" ValidationGroup="1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="不能为空" Display="Dynamic" ControlToValidate="comparetonewTagCount" ValidationGroup="1" ForeColor="Red"></asp:RequiredFieldValidator>

            <asp:CompareValidator ID="comparetonewTagCountCompareValidator" runat="server"
                 ErrorMessage="输入数字和上面不符" ControlToCompare="newTagCount" ControlToValidate="comparetonewTagCount" ValidationGroup="1" ForeColor="Red"></asp:CompareValidator>
        </div>
        <div class="mtop10">
            <asp:Button CssClass="btn btn-large btn-primary btnbox" ID="changeButton" runat="server" Text="提交修改" OnClick="changeButton_Click" ValidationGroup="1" />
            <asp:Button CssClass="btn btn-large btn-primary btnbox" ID="cancleButton" runat="server" Text="取消" OnClick="cancleButton_Click" ValidationGroup="2" />
        </div>
    </div>
    </form>
</body>
</html>
