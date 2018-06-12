<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Content.aspx.cs" Inherits="Ytg.ServerWeb.Content" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100%;height:100%">
        <asp:DataGrid ID="gridData" Width="100%" Height="100%" runat="server" AutoGenerateColumns="False" BorderWidth="1px" Font-Names="Verdana,Arial,sans-serif" Font-Size="12px" BorderColor="#CCCCCC" GridLines="Horizontal" BackColor="White" BorderStyle="None" CellPadding="4" ForeColor="Black">
            <FooterStyle BackColor="#CCCC99" ForeColor="Black"></FooterStyle>
            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#333333"></HeaderStyle>
            <PagerStyle HorizontalAlign="Right" BackColor="White" ForeColor="Black"></PagerStyle>
            <SelectedItemStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White"></SelectedItemStyle>
            <Columns>
                <asp:BoundColumn DataField="CaiZ" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="彩种"></asp:BoundColumn>
                <asp:BoundColumn DataField="WanZ" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="玩法组"></asp:BoundColumn>
                <asp:BoundColumn DataField="JiangJ" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="奖级"></asp:BoundColumn>
                <asp:BoundColumn DataField="JiangJin" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="奖金"></asp:BoundColumn>
                <asp:BoundColumn DataField="FanD" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="返点"></asp:BoundColumn>
                <asp:BoundColumn DataField="Status" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="状态"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
    </div>
    </form>
    
</body>
</html>
