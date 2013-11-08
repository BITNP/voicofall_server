<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="changeshenqiuName.aspx.cs" Inherits="voicofall_server.changeshenqiuName" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>修改场次名称</title>
<link href="css_style/bootstrap.css" rel="Stylesheet" />
<link href="css_style/flat-ui.css" rel="Stylesheet" />
<link href="css_style/my_style.css" rel="Stylesheet" />
</head>
<body class="container">
    <h3>修改场次名称</h3>
    <form id="form1" runat="server">
    <div>
        <div class="mtop10 bold green">
           <span>当前场次名称：</span><asp:Label ID="nowshenqiuNameLabel" runat="server" Text=""></asp:Label>
        </div>
        <div class="mtop10">
           <span class="big">新的场次名称：</span><asp:TextBox CssClass="tbgreen right10 " ID="newshenqiuName" runat="server" Text="" ValidationGroup="1"></asp:TextBox>
            <asp:RegularExpressionValidator ID="newshenqiuNameValidator" runat="server"
                 ValidationExpression=".{1,8}" Display="Dynamic"
                 ErrorMessage="最多输入8个字符" ControlToValidate="newshenqiuName" ValidationGroup="1" ForeColor="Red"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="不能为空" Display="Dynamic" ControlToValidate="newshenqiuName" ValidationGroup="1" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
        <div class="mtop10">
           <span class="big">确认场次名称：</span><asp:TextBox CssClass="tbgreen right10 " ID="comparetonewshenqiuName" runat="server" Text="" ValidationGroup="1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="不能为空" Display="Dynamic" ControlToValidate="comparetonewshenqiuName" ValidationGroup="1" ForeColor="Red"></asp:RequiredFieldValidator>

            <asp:CompareValidator ID="comparetonewshenqiuNameCompareValidator" runat="server"
                 ErrorMessage="确认类型和上面不符" ControlToCompare="newshenqiuName" ControlToValidate="comparetonewshenqiuName" ValidationGroup="1" ForeColor="Red"></asp:CompareValidator>
        </div>
        <div class="mtop10">
            <asp:Button CssClass="btn btn-large btn-primary btnbox" ID="changeButton" runat="server" Text="提交修改" OnClick="changeButton_Click" ValidationGroup="1" />
            <asp:Button CssClass="btn btn-large btn-primary btnbox" ID="cancleButton" runat="server" Text="取消" OnClick="cancleButton_Click" ValidationGroup="2" />
        </div>
    </div>
    </form>
</body>
</html>