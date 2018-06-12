<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetRole.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Manager.SetRole" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" /> 
    <link href="/dist/css/font.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">    
         <div class="dataTable_wrapper">
                <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                <thead>
                    <tr>
                        <td></td>
                        <th>角色名称</th>
                        <th>描述</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repList" runat="server" OnItemDataBound="repList_ItemDataBound">
                        <ItemTemplate>
                            <tr class="odd gradeX">
                                <td><asp:CheckBox ID="cBox" ToolTip='<%#Eval("RoleId") %>' Text="" runat="server" /></td>  
                                <td><%#Eval("RoleName") %></td>
                                <td><%#Eval("Descript") %></td>
                                <%-- <td></td>  --%>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <div class="modal-footer" style="text-align:center;">
                <asp:HiddenField ID="txtUserId" runat="server" />
                <asp:Button ID="btnSave" runat="server" class="btn btn-primary" Text="保存"  OnClick="btnSetRole_Click"/>
            </div>
        </div>
    </form>
</body>
</html>


