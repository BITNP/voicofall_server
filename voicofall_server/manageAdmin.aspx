<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="manageAdmin.aspx.cs" Inherits="voicofall_server.manageAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>后台账号管理</title>
<link href="css_style/bootstrap.css" rel="Stylesheet" />
<link href="css_style/flat-ui.css" rel="Stylesheet" />
<link href="css_style/my_style.css" rel="Stylesheet" />
</head>
<body class="container">
    <form id="form1" runat="server">
    <div>
        <h3>管理后台账户</h3>
    </div>
    <div>
        <asp:Label ID="wrongmsglabel" runat="server" Text="" ForeColor="Red"></asp:Label>
    </div>
    <div class="mtop10">
        <asp:TextBox CssClass="tbgreen right10 " ID="newpassportTextBox" runat="server" ValidationGroup="1"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="用户名必须填写" Display="Dynamic" ControlToValidate="newpassportTextBox" ValidationGroup="1"></asp:RequiredFieldValidator>
        <asp:Button CssClass="btn btn-large btn-primary btnbox" ID="addButton" runat="server" Text="增加新账户" OnClick="addButton_Click" ValidationGroup="1" />
        <asp:Button CssClass="btn btn-large btn-info btnbox" ID="cancel" runat="server" Text="返回" OnClick="cancel_Click" />
    </div>
    <div class="mtop10">
        <p class="bold">注意！编辑authority的项时，其值只能为以下三项中的一项</p>
        <ol>
            <li>low : 访客权限，只能查看，不能修改</li>
            <li>high : 高级权限，可以发放新票，修改订票时间，修改剩余票数。不能修改订票记录，不能修改后台账号，不能修改演出时间</li>
            <li>super : 管理员权限，具有全部权限，包括修改订票记录，修改后台账号，修改演出时间</li>
        </ol>
        <p class="blue bold">管理员不能修改管理员账户</p>
        <p>默认密码为 <strong>123456</strong> ，默认权限为<strong>low</strong></p>
    </div>
    <div class="mtop10" style="padding-bottom: 50px">

        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="账号" DataSourceID="AccessDataSource1" GridLines="Horizontal" OnRowDeleting="GridView1_RowDeleting" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" OnRowUpdating="GridView1_RowUpdating">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                <asp:BoundField DataField="账号" HeaderText="账号" ReadOnly="True" SortExpression="账号" />
                <asp:BoundField DataField="密码" HeaderText="密码" SortExpression="密码" />
                <asp:BoundField DataField="权限" HeaderText="权限" SortExpression="权限" />
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#487575" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#275353" />
        </asp:GridView>
        <asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="~/App_Data/tickets.mdb" DeleteCommand="DELETE FROM [adminsTable] WHERE [账号] = ?" InsertCommand="INSERT INTO [adminsTable] ([账号], [密码], [权限]) VALUES (?, ?, ?)" SelectCommand="SELECT 账号, 密码, 权限 FROM adminsTable WHERE (权限 &lt;&gt; 'admin')" UpdateCommand="UPDATE [adminsTable] SET [密码] = ?, [权限] = ? WHERE [账号] = ?" OnDeleting="AccessDataSource1_Deleting" OnUpdating="AccessDataSource1_Updating">
            <DeleteParameters>
                <asp:Parameter Name="账号" Type="String" />
            </DeleteParameters>
            <InsertParameters>
                <asp:ControlParameter ControlID="newpassportTextBox" Name="账号" PropertyName="Text" Type="String" />
                <asp:Parameter Name="密码" Type="String" DefaultValue="123456" />
                <asp:Parameter Name="权限" Type="String" DefaultValue="low" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="密码" Type="String" />
                <asp:Parameter Name="权限" Type="String" />
                <asp:Parameter Name="账号" Type="String" />
            </UpdateParameters>
        </asp:AccessDataSource>

    </div>
    </form>
</body>
</html>
