<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="voicofall_server.admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>深秋歌会后台</title>
<link href="css_style/bootstrap.css" rel="Stylesheet" />
<link href="css_style/flat-ui.css" rel="Stylesheet" />
<link href="css_style/my_style.css" rel="Stylesheet" />
<script type="text/javascript">
    var year = <%=year%>;
    var month = <%=month%>;
    var day = <%=day%>;
    var hour = <%=hour%>;
    var min = <%=min%>;
    var second = <%=second%>;

    function UpdateTime() {
        second++;//秒数+1
        //处理时间上的进位
        if (second >= 60) {
            min++
            second = 0;
            if (min >= 60) {
                min = 0;
                hour++;
                if (hour >= 24) {
                    day++;
                    hour = 0;
                }
            }
        }
        document.getElementById("servertimeLable").innerHTML = + year + "年" + month + "月" + day + "日 " + hour + ":" + (min < 10 ? "0" : "") + min + ":" + (second < 10 ? "0" : "") + second;
        window.setTimeout("UpdateTime()", 1000);
    }
    
</script>
</head>
<body onload="UpdateTime()" class="container">
    <form id="form1" runat="server">
    <div>
        <h3>深秋歌会后台管理系统</h3>
    </div>
    <div>
        <span>当前账号：</span><asp:Label CssClass="right10" ID="passportLabel" runat="server" Text=""></asp:Label>
        <span>权限：</span><asp:Label CssClass="right10" ID="authorityLabel" runat="server" ForeColor="Blue"></asp:Label>
        <span>
            <asp:LinkButton ID="loginoutButton" runat="server" OnClick="loginoutButton_Click">注销</asp:LinkButton>
        </span>
    </div>
    <div runat="server" id="highButtons" style="padding-top: 10px" >
        <span class="span3" style="padding-right: 5px;">
            <asp:Button CssClass="btn btn-large btn-info" ID="addButton" runat="server" Text="发放新票" OnClick="addButton_Click" Width="120px" />
        </span>
        <span class="span3" style="padding-right: 5px;">
            <asp:Button CssClass="btn btn-large btn-info" ID="changetimeButton" runat="server" Text="修改订票时间" OnClick="changetimeButton_Click" Width="120px" />
        </span>
        <span class="span3" style="padding-right: 5px;">
            <asp:Button CssClass="btn btn-large btn-info" ID="decreseButton" runat="server" Text="修改剩余票数" OnClick="decreseButton_Click" Width="120px" />
        </span>
        <asp:Button CssClass="btn btn-large btn-info btnbox" ID="changeZone" runat="server" Text="修改订票类型" OnClick="changeZone_Click" />
        <asp:Button CssClass="btn btn-large btn-primary btnbox" ID="changePasswordButton" runat="server" Text="修改密码" OnClick="changePasswordButton_Click" />
            
    </div>
    <div runat="server" id="superButtons" style="padding-top: 10px">
        <span class="span3" style="padding-right: 5px;">
            <asp:Button CssClass="btn btn-large btn-warning" ID="changeshenqiutimeButton" runat="server" Text="修改演出时间" OnClick="changeshenqiutimeButton_Click" Width="120px" />
        </span>
        <asp:Button  CssClass="btn btn-large btn-warning btnbox" ID="changeshenqiuName" runat="server" Text="修改场次名称" OnClick="changeshenqiuName_Click" />
        <asp:Button CssClass="btn btn-large btn-warning btnbox" ID="changeTagCountButton" runat="server" Text="检票口数量" OnClick="changeTagCountButton_Click" />
        <asp:Button CssClass="btn btn-large btn-danger btnbox" ID="clearButton" runat="server" Text="结束本次演出" OnClick="clearButton_Click" />
        <span class="span3" style="padding-right: 5px;">    
            <asp:Button CssClass="btn btn-large btn-primary"  ID="manageAdminButton" runat="server" Text="管理后台账号" OnClick="manageAdminButton_Click" Width="120px" />
        </span>

    </div>
    <div class="bold purple">
        <span class="purple">演出场次：</span><asp:Label CssClass="purple" ID="shenqiuNameLabel" runat="server" Text=""></asp:Label>
    </div>
    <div class="bold purple">
        <span class="purple">演出时间：</span><asp:Label CssClass="purple" ID="shenqiuStartTimeLabel" runat="server" Text=""></asp:Label>
    </div>
        <div class="bold purple">
            检票口<span class="purple">数量：</span><asp:Label CssClass="purple" ID="tagCountLabel" runat="server" Text=""></asp:Label>
    </div>
    <div class="blue bold">
        <span>订票类型：</span><asp:Label CssClass="right10" ID="zoneLabel" runat="server" Text=""></asp:Label>
        
    </div>
    <div class="blue bold">
        <span>总数：</span><asp:Label CssClass="right10" ID="allLable" runat="server" Text=""></asp:Label>
        <span>已定：</span><asp:Label CssClass="right10" ID="bookedLable" runat="server" Text=""></asp:Label>
        <span>剩余：</span><asp:Label CssClass="right10" ID="unbookedLable" runat="server" Text=""></asp:Label>
    </div>
    <div>
        <span class="green">订票时间：</span><asp:Label CssClass="green" ID="booktimeLable" runat="server" Text=""></asp:Label>
        <br />
        <span>当前时间：</span><asp:Label ID="servertimeLable" runat="server" Text=""></asp:Label>
    </div>
    <div>
        <span class="span3 divbox">  
            <asp:Button CssClass="btn btn-large btn-danger btnbox" ID="modifyButton" runat="server" Text="修改名单" OnClick="modifyButton_Click" />
        </span>
    </div>
    <div class="divbox mbottom20">
        <asp:GridView ID="TicketsGridView" runat="server" CellPadding="4" ForeColor="#333333" GridLines="Vertical" OnRowDeleting="TicketsGridView_RowDeleting" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="UID" DataSourceID="AccessDataSource2" OnPageIndexChanged="TicketsGridView_PageIndexChanged">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:CommandField ShowDeleteButton="True" />
                <asp:BoundField DataField="UID" HeaderText="UID" ReadOnly="True" SortExpression="UID" />
                <asp:BoundField DataField="username" HeaderText="姓名" SortExpression="username" />
                <asp:BoundField DataField="zonename" HeaderText="门票类型" SortExpression="zonename" />
                <asp:BoundField DataField="tickettag" HeaderText="标记" SortExpression="tickettag" />
                <asp:BoundField DataField="studentid" HeaderText="学号" SortExpression="studentid" />
                <asp:BoundField DataField="phonenumber" HeaderText="电话" SortExpression="phonenumber" />
                <asp:BoundField DataField="addtime" HeaderText="添加时间" SortExpression="addtime" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        <asp:AccessDataSource ID="AccessDataSource2" runat="server" DataFile="~/App_Data/tickets.mdb" DeleteCommand="DELETE FROM [ticketsTable] WHERE [UID] = ?" InsertCommand="INSERT INTO [ticketsTable] ([UID], [username], [zonename], [tickettag], [studentid], [phonenumber], [addtime]) VALUES (?, ?, ?, ?, ?, ?, ?)" SelectCommand="SELECT [UID], [username], [zonename], [tickettag], [studentid], [phonenumber], [addtime] FROM [ticketsTable]" UpdateCommand="UPDATE [ticketsTable] SET [username] = ?, [zonename] = ?, [tickettag] = ?, [studentid] = ?, [phonenumber] = ?, [addtime] = ? WHERE [UID] = ?">
            <DeleteParameters>
                <asp:Parameter Name="UID" Type="String" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="UID" Type="String" />
                <asp:Parameter Name="username" Type="String" />
                <asp:Parameter Name="zonename" Type="String" />
                <asp:Parameter Name="tickettag" Type="String" />
                <asp:Parameter Name="studentid" Type="String" />
                <asp:Parameter Name="phonenumber" Type="String" />
                <asp:Parameter Name="addtime" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="username" Type="String" />
                <asp:Parameter Name="zonename" Type="String" />
                <asp:Parameter Name="tickettag" Type="String" />
                <asp:Parameter Name="studentid" Type="String" />
                <asp:Parameter Name="phonenumber" Type="String" />
                <asp:Parameter Name="addtime" Type="String" />
                <asp:Parameter Name="UID" Type="String" />
            </UpdateParameters>
        </asp:AccessDataSource>
    </div>
    </form>
</body>
</html>
