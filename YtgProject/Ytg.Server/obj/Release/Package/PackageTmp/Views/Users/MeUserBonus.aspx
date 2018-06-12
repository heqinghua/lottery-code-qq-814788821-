<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeUserBonus.aspx.cs" Inherits="Ytg.ServerWeb.Views.Users.MeUserBonus"  MasterPageFile="~/Views/Users/Users.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="usersHead" runat="server">
   <script type="text/javascript">
       $(function () {
           $("#userBonus_Filter").addClass("title_active");
           $.ajax("UserBonus.aspx?id=<%=LoginUserId %>", function (data) {
               alert(data);
           });
       });
    </script>
</asp:Content>
<asp:Content ID="ContentUsers" ContentPlaceHolderID="ContentUsers" runat="server">
   <div class="control">
        <iframe id="main" name="main" allowtransparency="true" width="100%" scrolling="no"  style="min-height:900px;" frameborder="0" src="/Views/UserGroup/UserBonus.aspx?id=<%=LoginUserId %>&from=in" border="0" noresize="noresize" framespacing="0" ></iframe>
    </div>
</asp:Content>
