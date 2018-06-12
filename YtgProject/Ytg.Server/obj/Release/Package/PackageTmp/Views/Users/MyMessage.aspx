<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyMessage.aspx.cs" Inherits="Ytg.ServerWeb.Views.Users.MyMessage" MasterPageFile="/Views/Users/Users.master" %>

<asp:Content ContentPlaceHolderID="usersHead" runat="server">
        <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <link href="/Content/Scripts/Pager/kkpager_orange.css" rel="stylesheet" />
    <script src="/Content/Scripts/Pager/kkpager.min.js"></script>
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            $("#msg").addClass("title_active");
            $("#lbtnSearch").click(function () {
                loaddata();//加载数据
            });
            loaddata();//加载数据
        });

        var pageIndex = 1;
        function loaddata() {
            Ytg.common.loading();
            var state = $("#ddlStatus").find("option:selected").val();
            var tp = $("#type").find("option:selected").val();

            $.ajax({
                url: "/Page/Messages.aspx",
                type: 'post',
                data: "action=selectmessages&status=" + state + "&messageType=" + tp + "&pageIndex=" + pageIndex,
                success: function (data)
                {
                    Ytg.common.cloading();
                    var jsonData = JSON.parse(data);
                    //清除
                    $(".ltbody").children().remove();
                    if (jsonData.Code == 0 && jsonData.Data.length > 0)
                    {
                        inintpager(pageIndex, jsonData.Total, function (n)
                        {
                            pageIndex = n;
                            loaddata();
                        });
                        for (var c = 0; c < jsonData.Data.length; c++) {
                            
                            var item = jsonData.Data[c];
                            var tdTitle = item.Title.length > 15 ? item.Title.substring(0, 15) : item.Title;//40
                            var tdMessage = item.MessageContent.length > 25 ? item.MessageContent.substring(0, 24) : item.MessageContent;

                            var statusMsg = item.Status == 0 ? "<span style='color:#cd0228;'>未读</span>" : "已读";
                            
                            var htm = "<tr id='tr_id_" + item.Id + "'><td>" + item.OccDate + "</td><td><a href=\"javascript:showDetails('" + " " + "','" + html_encode(item.MessageContent) + "'," + item.Id + "," + item.Status + ")\">" + tdTitle + "</a></td><td>" + tdMessage + "</td><td>" + statusMsg + "</td><td><a href='javascript:del(" + item.Id + ")'>删除</a></td></tr>"
                            $(".ltbody").append(htm);
                        }
                    }
                    else
                    {
                        $(".ltbody").Empty(5);
                    }
                }
            });
        }
        function showDetails(title, content, id, state) {
           
            $.alert(html_encode(content));
            //标记为已读消息
            if (state == 0) {
                $.ajax({
                    url: "/Page/Messages.aspx",
                    type: 'post',
                    data: "action=setread&mid=" + id,
                    success: function (data) {

                    }
                });
            }
        }
        function del(id) {
            //删除消息
            Ytg.common.loading();
            $.ajax({
                url: "/Page/Messages.aspx",
                type: 'post',
                data: "action=delMessage&id=" + id,
                success: function (data) {
                    Ytg.common.cloading();
                    var jsonData = JSON.parse(data);
                    if (jsonData.Code == 0) {
                        $.alert("删除成功！", 1, function () {
                            $("#tr_id_" + id).remove();
                        });
                    } else {
                        $.alert("删除失败！");
                    }
                }
            });
        }

        function html_encode(str)
        {
            var s = "";
            if (str.length == 0) return "";
            s = str.replace(/&/g, ">");
            s = s.replace(/</g, "<");
            s = s.replace(/>/g, ">");
            s = s.replace(/ /g, " ");
            s = s.replace(/\'/g, "'");
            s = s.replace(/\"/g, "");
            s = s.replace(/\n/g, "<br>");
            return s;
        }

        //解码
        function html_decode(str)
        {
            var s = "";
            if (str.length == 0) return "";
            s = str.replace(/>/g, "&");
            s = s.replace(/</g, "<");
            s = s.replace(/>/g, ">");
            s = s.replace(/ /g, " ");
            s = s.replace(/'/g, "\'");
            s = s.replace(/"/g, "\"");
            s = s.replace(/<br>/g, "\n");
            return s;
        }
    </script>
</asp:Content>
<asp:Content ID="ContentUsers" ContentPlaceHolderID="ContentUsers" runat="server">
    <div>
        <!--工具栏-->
        <div class="toolbar-wrap">
            <table border="0" cellspacing="0" cellpadding="0" >
                <tbody>
                    <tr>
                        <td align="center"> 消息类型：
                        <select id="type">
                            <option selected="selected" value="-1">所有类型</option>
                            <option value="1">系统消息</option>
                            <option value="2">私人消息</option>
                            <option value="4">中奖消息</option>
                            <option value="8">充提信息</option>
                        </select>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                        <td align="center">消息状态：
                        <select id="ddlStatus">
                            <option selected="selected" value="-1">所有状态</option>
                            <option value="1">已读</option>
                            <option value="0">未读</option>
                        </select></td>
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
                    <th width="20%">时间</th>
                    <th width="25%">标题</th>
                    <th>消息内容</th>
                    <th width="8%">消息状态</th>
                    <th width="8%">操作</th>
                </tr>
            </thead>
            <tbody class="ltbody">
                <tr>
                    <td align="center" colspan="10">暂无记录</td>
                </tr>
            </tbody>
        </table>
        <!--/列表-->
        <div id="kkpager" style=""></div>
    </div>
</asp:Content>
