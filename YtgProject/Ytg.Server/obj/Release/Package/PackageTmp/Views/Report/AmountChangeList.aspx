<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AmountChangeList.aspx.cs" Inherits="Ytg.ServerWeb.Views.Report.AmountChangeList" MasterPageFile="~/Views/Report/Report.master" %>

<%@ Register Src="~/Views/Report/AmountChangeListControl.ascx" TagPrefix="uc1" TagName="AmountChangeListControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="usersHead" runat="server">
    <script src="/Content/Scripts/datepicker/WdatePicker.js"></script>
    <link href="/Content/Scripts/Pager/kkpager_orange.css" rel="stylesheet" />
    <script src="/Content/Scripts/Pager/kkpager.min.js" type="text/javascript"></script>
    <style type="text/css">
        .l-list table {
            width: 100%;
        }

            .l-list table tr td {
                height: 40px;
                line-height: 40px;
                
            }

        .btn {
            background: #fff;
            border: 1px solid #e1e1e1;
            color: #000;
            height: 28px;
            line-height: 24px;
            width: 70px;
            padding-left: 10px;
        }

            .btn:hover {
                background: #fff;
                color: #cd0228;
                border: 1px solid #cd0228;
            }

        .selectedBtn {
            background: #fff;
            color: #cd0228;
            border: 1px solid #cd0228;
        }

        .ordertypeS {
            width: 120px;
            height:150px;
            border: 1px solid #e1e1e1;
            padding-left: 5px;
            padding-top: 5px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#zhangbian").addClass("title_active");
        });
    </script>
</asp:Content>
<asp:Content ID="ContentUsers" ContentPlaceHolderID="ContentUsers" runat="server">
    <form runat="server" id="form1">
        <div>
            <uc1:AmountChangeListControl runat="server" ID="AmountChangeListControl" />
        </div>
    </form>
</asp:Content>
