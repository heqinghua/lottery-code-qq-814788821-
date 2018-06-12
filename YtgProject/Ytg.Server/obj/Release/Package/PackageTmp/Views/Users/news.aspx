<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="news.aspx.cs" Inherits="Ytg.ServerWeb.Views.Users.news" MasterPageFile="/Views/Users/Users.master"%>


<asp:Content ContentPlaceHolderID="usersHead" runat="server">
        <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <link href="/Content/Scripts/Pager/kkpager_orange.css" rel="stylesheet" />
    <script src="/Content/Scripts/Pager/kkpager.min.js"></script>
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            $("#news_msg").addClass("title_active");
            $("#lbtnSearch").click(function () {
                loaddata();//加载数据
            });
            loaddata();//加载数据
        });

        var pageIndex = 1;
        function loaddata() {
            Ytg.common.loading();
            
            $.ajax({
                url: "/Page/Initial.aspx",
                type: 'post',
                data: "action=news",
                success: function (data)
                {
                    Ytg.common.cloading();
                    var jsonData = JSON.parse(data);
                    //清除
                    $(".ltbody").children().remove();
                    if (jsonData.Code == 0 && jsonData.Data.length > 0)
                    {
                        //inintpager(pageIndex, jsonData.Total, function (n)
                        //{
                        //    pageIndex = n;
                        //    loaddata();
                        //});
                       // console.info(jsonData.Data);
                        for (var c = 0; c < jsonData.Data.length; c++) {
                            //
                            var item = jsonData.Data[c];
                            var tdTitle = item.Title.length > 15 ? item.Title.substring(0, 15) : item.Title;//40
                            var sditem = unescape(item.Content);
                            console.info(sditem);
                            var tdMessage = sditem.length > 25 ? sditem.substring(0, 24) : sditem;
                            
                            
                            
                            var htm = "<tr id='tr_id_" + item.Id + "'><td>" + item.OccDate + "</td><td><a href=\"javascript:showDetails('" + " " + "','" + html_encode(item.Content) + "'," + item.Id + ",0)\">" + tdTitle + "</a></td></tr>"
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
           
            showNewDetails(title,id);
           
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
          
        </div>
        <!--/工具栏-->

        <!--列表-->
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="grayTable">
            <thead>
                <tr>
                    <th width="20%">时间</th>
                    <th width="25%">标题</th>
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
