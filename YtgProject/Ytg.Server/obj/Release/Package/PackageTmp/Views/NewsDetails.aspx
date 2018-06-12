<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsDetails.aspx.cs" Inherits="Ytg.ServerWeb.Views.NewsDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Content/Css/style.css" rel="stylesheet" />
    <link href="/Content/Css/layout.css" rel="stylesheet" />
    <script src="/Content/Scripts/jquery-1.7.min.js"></script>
    <style>
        p {margin:0px;}
    </style>
</head>
<body>
    <div style="display: block;overflow-x:hidden;overflow-y:auto;height:250px;padding-left:5px;">
            <div><%=news%></div>
    </div>
    <div style="text-align: center;">
            <input type="button" value="确 定" id="closeDialog" class="closeButton" />
    </div>
</body>
</html>
<script type="text/javascript">
    $("#closeDialog").click(function () {
        parent.parent.closeDialog();
    });
</script>
