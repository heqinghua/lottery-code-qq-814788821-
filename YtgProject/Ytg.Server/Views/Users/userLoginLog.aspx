<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userLoginLog.aspx.cs" Inherits="Ytg.ServerWeb.Views.Users.userLoginLog" MasterPageFile="/Views/Users/Users.master"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="usersHead" runat="server">
    <script src="/Content/Scripts/datepicker/WdatePicker.js"></script>
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
       <link href="/Content/Scripts/Pager/kkpager_orange.css" rel="stylesheet" />
    <script src="/Content/Scripts/Pager/kkpager.min.js"></script>
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            $("#loginLog").addClass("title_active");
            $("#lbtnSearch").click(function () {
                loaddata();//加载数据
            });
            loaddata();//加载数据
        });

        var pageIndex = 1;
        function loaddata() {
            Ytg.common.loading();
            var beginDate = $("#txtstart").val();
            var endDate = $("#txtend").val();
           
            $.ajax({
                url: "/Page/Users.aspx",
                type: 'post',
                data: "action=loginlog&beginDate=" + beginDate + "&endDate=" + endDate + "&pageIndex=" + pageIndex,
                success: function (data) {
                    Ytg.common.cloading();
                    var jsonData = JSON.parse(data);
                    inintpager(pageIndex, jsonData.Total, function (n) {
                        pageIndex = n;
                        loaddata();
                    });
                    //清除
                    $(".ltbody").children().remove();
                    if (jsonData.Code == 0 && jsonData.Data.length > 0) {
                        for (var c = 0; c < jsonData.Data.length; c++) {
                            var item = jsonData.Data[c];
                            var htm = "<tr><td>" + item.Code + "</td><td>" + item.Ip + "</td><td>" + item.OccDate + "</td><td>" + item.ServerSystem + "</td></tr>"
                            $(".ltbody").append(htm);
                        }
                    } else {
                        $(".ltbody").Empty(4);
                    }
                }
            });
        }
        function showDetails(title, content) {
            $.alert(content);

        }
    </script>
</asp:Content>
<asp:Content ID="ContentUsers" ContentPlaceHolderID="ContentUsers" runat="server">
    <div>
        <!--工具栏-->
        <div class="toolbar-wrap" >
            <table  border="0" cellspacing="0" cellpadding="0" >
                <tbody>
                    <tr>
                        <td align="right"> 登录时间：</td>
                       <td><input id="txtstart" type="text" class="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" />
                                <span>至 </span>
                                <input id="txtend" type="text" class="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" /></td>
                        <td><input name="" id="lbtnSearch" type="button" value="查询" class="formCheck"></td>
                    </tr>
                    
                </tbody>
            </table>

           
        </div>
        <!--/工具栏-->

        <!--列表-->
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="grayTable">
            <thead>
                <tr>
                    <th >登录名</th>
                    <th >登陆IP</th>
                    <th>登陆时间</th>
                    <th >登陆系统</th>
                </tr>
            </thead>
            <tbody class="ltbody">
                <tr>
                    <td align="center" colspan="10">暂无记录</td>
                </tr>
            </tbody>
        </table>
        <!--/列表-->
        <div id="kkpager"></div>
    </div>
</asp:Content>
