<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Help_.aspx.cs" Inherits="Ytg.ServerWeb.Views.Help.Help_" MasterPageFile="~/lotterySite.Master"%>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
   <script src="/Content/Scripts/layout.js" type="text/javascript"></script>
    <script src="/Content/Scripts/lhgdialog/lhgdialog.js?skin=chrome"></script>
    <link href="/Content/Css/style.css" rel="stylesheet" />
    <style type="text/css">
        .ltable td {font-size:12px;}
    </style>
    <script type="text/javascript">
        $(function () {
            $("#helper_").addClass("cur");
            var minHeight = $(this).height() - (95 + (80));
            $("#main").css("min-height", minHeight);
        })
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <iframe id="main" class="help_man_css" name="main" allowtransparency="true"  src="../../Helper/help.aspx" width="100%" scrolling="no" frameborder="0"  border="0" noresize="noresize" framespacing="0" style="max-height:700px;"></iframe>
</asp:Content>