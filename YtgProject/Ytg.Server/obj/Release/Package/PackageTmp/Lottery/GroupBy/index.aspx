<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Ytg.ServerWeb.Lottery.GroupBy.index" MasterPageFile="~/lotterySite.Master" %>


<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>合买大厅</title>
    <link href="css/base.css" rel="stylesheet" />
    <link href="css/core.css" rel="stylesheet" />
    <link href="css/salleAtj.css" rel="stylesheet" />
    <link href="css/index.css" rel="stylesheet" />
    <script src="js/local.js"></script>
    <link href="/Content/Scripts/Pager/kkpager_orange.css" rel="stylesheet" />
    <script src="/Content/Scripts/Pager/kkpager.min.js"></script>
    <script src="../../Content/Scripts/common.js"></script>
    <script src="js/hm.js"></script>
    <style type="text/css">
        th, td {
            font-size: 14px;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#lottery_group").addClass("on");
        });
    </script>
    <div class="UserInfoBox wrap_footerbg">
        <div class="clearfix"></div>
        <div class="wrap_bg wrap" style="width: 100%;">
            <!--个人信息-->
            <div id="content" >
                <div class="left_frame" >
                    <div class="left_content">
                        <div class="sidebar_menu">
                            <asp:Repeater ID="rptMenus" runat="server" OnItemDataBound="rptMenus_ItemDataBound">
                                <ItemTemplate>
                                    <dl class="ff-tow2">
                                        <dt style="display:none;"><%#Eval("ShowTitle") %></dt>
                                        <dd class="on">
                                            <asp:Repeater ID="rptChildren" runat="server">
                                                <ItemTemplate>
                                                    <ul class="con_ul noBorder">
                                                        <li><a href="/Lottery/GroupBy/index.aspx?lotterycode=<%# Eval("LotteryCode") %>">
                                                            <p><span class="<%#Eval("Remark") %>"><%# Eval("LotteryName") %></span></p>
                                                        </a></li>
                                                    </ul>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                    </dl>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="leftsidebotcon"></div>
                    <script>
                        $(".ff-one").click(function () {
                            $(this).addClass("ff-tow").siblings().removeClass("ff-tow")
                        });
                        $(".ff-tow").click(function () {
                            $(this).slideDown();
                        });
                    </script>
                    <div style="clear: both;"></div>
                </div>

                <div id="con_right">
                    <div class="right_box" style="padding: 0px; border: 0px currentColor; border-image: none;">
                        <div class="faWrap" style="margin-top: 0px;float:left;width:80%;">
                            <h3 class="toBuyMenTit"><span title="热门方案" class="hotFan">热门方案</span><span class="hDeng">合买方案进度满100%才可以出票哦！</span></h3>
                            <div class="selected clearfix">
                                <form method="get" action="/">
                                    <span class="ifShaiXuan">状态：
                            <select name="status" id="status">
                                <option value="-2">所有</option>
                                <option value="-1">未满员</option>
                                <option value="0">进行中</option>
                                <option value="1">已中奖</option>
                                <option value="2">未中奖</option>
                                <option value="3">已撤单</option>
                            </select>
                                        &nbsp;|&nbsp;发起人：<input type="text" value="" class="w80" name="name" id="searname" />
                                        <input type="button" value="搜 索" class="btnOrange mlPad" id="btnsear" />&nbsp;&nbsp;
                                    </span>
                                </form>
                            </div>
                            <table width="100%" cellspacing="0" cellpadding="0" border="0" class="group_buy">
                                <thead>
                                    <tr bgcolor="#f6f6f6">
                                        <th width="6%" class="t_c">序号</th>
                                        <th width="11%">发起人</th>
                                        <th width="15%">发起人战绩</th>
                                        <th width="10%">彩种</th>
                                        <th width="12%">进度</th>
                                        <th width="12%" class="t_r">方案金额 |</th>
                                        <th width="12%">剩余</th>
                                        <th width="12%">认购份数</th>
                                        <th width="8%" class="t_c">操作</th>
                                    </tr>
                                </thead>
                                <tbody id="contentbody">
                                </tbody>
                            </table>
                            <div id="kkpager" style=""></div>
                        </div>
                        <div >
                               <div style="text-align: center; line-height: 2em; font-size: 16px;">
                                   <span class="date">当日中奖排行</span>
                                </div>
                                <div style="overflow: hidden; margin-top: 12px;">
                                    <div class="hc_history">
                                        <div class='lottery_log'>
                                           <table width="100%" cellspacing="0" cellpadding="0" border="0" class="group_buy">
                                            <thead>
                                                <tr bgcolor="#f6f6f6">
                                                    <th style="text-align:center;">序号</th>
                                                    <th style="text-align:center;">用户名</th>
                                                    <th style="text-align:center;">中奖金额</th>
                                                </tr>
                                            </thead>
                                            <tbody id="contentbody_winLst">
                                            </tbody>
                                        </table>
                                        </div>
                                    </div>
                                </div>
                        </div>
                        <div style="clear: both;"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        $(function () {

            //加载数据
            loaddata();
            $("#btnsear").click(function () {
                loaddata();
            });
            loadwinlst();
            $("#con_right").css({"width":($(window).width()-175)});
        });

        var pageIndex = 1;
        function loaddata() {

            $.ajax({
                url: "/Page/Lott/LotteryBetDetail.aspx",
                type: 'post',
                data: "&action=hmlst&nickName=" + $("#searname").val() + "&stauts=" + $("#status").find("option:selected").val() + "&pageIndex=" + pageIndex + "&lotterycode=<%=Request.Params["lotterycode"]%>",
                success: function (data) {
                    
                    var jsonData = JSON.parse(data);
                    //清除
                    $("#contentbody").children().remove();

                    if (jsonData.Code == 0 && jsonData.Data.length > 0) {
                        //分页
                        inintpager(pageIndex, jsonData.Total, function (n) {
                            pageIndex = n;
                            loaddata();
                        });

                        //{"Id":2277289,"IssueCode":"20180501008","BetCode":"b4DAE99CBB95BFEED","UserId":12455,"BetCount":125,"TotalAmt":250.0000,"LotteryCode":"cqssc","Multiple":1,"Model":0,"BetContent":"13579,56789,01234","WinMoney":0.0000,"IsMatch":false,"OpenResult":null,"Stauts":3,"PostionName":"","IsBuyTogether":1,"Subscription":50.0000,"Secrecy":0,"PartakeUserCount":0,"PartakeMonery":0.0000,"SurplusMonery":200.0000,"Bili":20.0,"Code":"heladiv2","NikeName":"冰冰"},{"Id":2277288,"IssueCode":"20180501008","BetCode":"bAF4D6BFBA6EBC246","UserId":12455,"BetCount":125,"TotalAmt":250.0000,"LotteryCode":"cqssc","Multiple":1,"Model":0,"BetContent":"56789,56789,56789","WinMoney":0.0000,"IsMatch":false,"OpenResult":null,"Stauts":3,"PostionName":"","IsBuyTogether":1,"Subscription":50.0000,"Secrecy":0,"PartakeUserCount":0,"PartakeMonery":0.0000,"SurplusMonery":200.0000,"Bili":20.0,"Code":"heladiv2","NikeName":"冰冰"},{"Id":2277287,"IssueCode":"20180501008","BetCode":"b357EA29E225334BC","UserId":12455,"BetCount":125,"TotalAmt":250.0000,"LotteryCode":"cqssc","Multiple":1,"Model":0,"BetContent":"01234,56789,01234","WinMoney":0.0000,"IsMatch":false,"OpenResult":null,"Stauts":3,"PostionName":"","IsBuyTogether":1,"Subscription":25.0000,"Secrecy":0,"PartakeUserCount":0,"PartakeMonery":0.0000,"SurplusMonery":225.0000,"Bili":10.0,"Code":"heladiv2","NikeName":"冰冰"}
                        var index = 1;
                        for (var i = 0; i < jsonData.Data.length; i++) {
                            var item = jsonData.Data[i];
                            
                            $("#contentbody").append(createitem(index, item));
                            index++;
                        }

                        

                    } else {
                        inintpager(0, 0);
                        $("#contentbody").append("<tr><td colspan='9' style='text-align:center;color:red;font-size:14px;'>无合买数据</td></tr>");
                    }
                    loadother();
                }
            });
        }

        function loadother() {
            if('<%=Request.Params["lotterycode"]%>'!='')
            {
                inintaction();
                return;
            }
            $.ajax({
                url: "/Page/Lott/LotteryBetDetail.aspx",
                type: 'post',
                data: "&action=hmlstfc",
                success: function (data) {
                    var jsonData = JSON.parse(data);
                    if (jsonData.Code == 0 && jsonData.Data.length > 0) {
                        var index = 1;
                        for (var i = 0; i < jsonData.Data.length; i++) {
                            var item = jsonData.Data[i];
                            $("#contentbody").prepend(createitem(index, item));
                            index++;
                        }

                        inintaction();
                    }
                    
                }
            });

        }


        function createitem(index, item) {
            var bycontent = "<input type='text' maxnum='" + item.SurplusMonery + "' class='group_input' placeholder='剩余" + item.SurplusMonery + "元' autocomplete='off'></td>";
            var byactionHtml = "";
            byactionHtml = "<a class='buy_btn'  tag='" + item.Id + "' href='javascript:;'>购买</a>";
            //console.info(item.GroupByState);

            if (item.Stauts != 3) {
                bycontent = "<font color=\"red\">已完成</font>";
                byactionHtml = "";
            } else if (item.GroupByState == 1) {
                bycontent = "<font color=\"#0091D1\">已满员</font>";
                byactionHtml = "";
            }

            //<td class="tdOne"><em>3</em><span class="public" title="方案公开"></span></td>
            return "<tr ><td class='tdOne'><em>" + index + "</em>" +
                            "<span class=\"" + (item.Secrecy == 0 ? "public" : "img_baodi") + "\" title=\""+(item.Secrecy==0?"方案公开":"方案保密")+"\"></span></td>" +
                            "<td>" + subnikenaem(item.NikeName) + "</td>" +
                            "<td>" + Show.UserLevel(item.UserId) + "</td>" +
                            "<td class='twoLine'>" + Show.LotName(item.LotteryCode) + "</td>" +
                            "<td>" +
                                "<p style='margin-top: -3px'>" + item.Bili + "%</p>" +
                                "<p class='complete'><em style='width: " + item.Bili + "%'></em></p>" +
                            "</td>" +
                            "<td class='totalmoney'><em>" + item.TotalAmt + "元</em> |</td>" +
                            "<td>" + item.SurplusMonery + "</td>" +
                            "<td>" + bycontent + "<td>" + byactionHtml + "<a href='javascript:showDetail(" + item.Id + ",\"" + item.LotteryCode + "\");' class='btn_mini' target='_blank'>详情</a>" +
                            "</td>" +
                       "</tr>";
        }

        function subnikenaem(name) {
          
            if (name == null || name == '')
                return name;
            if (name.length > 3)
                return name.substring(0, 2)+"***";
            return name+"***";
        }

        function showDetail(id, lotteryCode) {
            var winHeight = $(window).height() * 0.8;
            var openHeight = winHeight;
            var ul = "url:details.aspx?id=" + id + "&lotteryCode=" + lotteryCode;

            $.dialog({
                id: 'betdetail_hm',
                fixed: true,
                lock: true,
                max: false,
                min: false,
                title: "查看详情",
                content: ul,
                width: 820,
                height: openHeight
            });
        }

        function inintaction() {

            $('input.group_input').keyup(function () {
                //$(this).val($(this).val().replace(/[^\d]/g, ''));
                //var num = Number($(this).val() == 0 ? 1 : $(this).val());
                //var max = Number($(this).attr('maxnum'));
                //num = num > max ? max : num;
                //$(this).val(num);
            });

            var hmsta = false;
            $('a.buy_btn').click(function () {
                if (hmsta) {
                    $.alert('正在提交数据，请稍等。');
                    return;
                }
                var thiz = $(this).parent().parent().find('input.group_input');
                var mon = Number(thiz.val());
                var max = Number(thiz.attr('maxnum'));
                
                var bettid = $(this).attr("tag");
                if (typeof (bettid) == typeof (undefined)) {
                    $.alert('未知错误，请刷新后重试。');
                    return;
                }
                if (mon == 0) {
                    $.alert('请输入您要认购的金额。');
                    return;
                } else if (mon > max) {
                    $.alert('最多只能认购' + max + '元');
                    thiz.val(max)
                    return;
                } else if (mon < 1) {
                    $.alert('最低需认购1元');
                    thiz.val(1)
                    return;
                }
                hmsta = false;
                subhm(bettid, mon);

            });

        }

        function loadwinlst() {
            $.ajax({
                url: "/Page/Lott/LotteryBasic.aspx",
                type: 'post',
                data: "&action=winlst",
                success: function (data) {
                    var jsonData = JSON.parse(data);
                    //清除
                    $("#contentbody_winLst").children().remove();

                    if (jsonData.Code == 0 && jsonData.Data.length > 0) {
                        
                        var index = 1;
                        for (var i = 0; i < jsonData.Data.length; i++) {
                            var item = jsonData.Data[i];
                            $("#contentbody_winLst").append(createwinitem(index, item));
                            index++;
                        }


                    } else {
                        $("#contentbody_winLst").append("<tr><td colspan='3' style='text-align:center;color:red;font-size:14px;'>无数据</td></tr>");
                    }
                }
            });
        }

        function createwinitem(index,item) {
            //[{"NikeName":"868","WinMonery":2601.6000,"Id":9,"OccDate":"2018-05-17 23:39:59"},{"NikeName":"赚钱包二奶","WinMonery":1755.9000,"Id":15,"OccDate":"2018-05-17 23:39:59"},{"NikeName":"abcd214","WinMonery":1012.2120,"Id":11,"OccDate":"2018-05-17 23:39:59"},{"NikeName":"记忆之痕","WinMonery":801.9600,"Id":10,"OccDate":"2018-05-17 23:39:59"},{"NikeName":"lb081796","WinMonery":390.0000,"Id":12,"OccDate":"2018-05-17 23:39:59"},{"NikeName":"十八骑","WinMonery":146.5500,"Id":14,"OccDate":"2018-05-17 23:39:59"},{"NikeName":"dsc","WinMonery":26.0400,"Id":13,"OccDate":"2018-05-17 23:39:59"}],"Page":0,"Total":0,"ErrMsg":"","ResponseTime":"2018-05-18 00:57:07"}
            return "<tr ><td style='text-align:center;'>" + index + "</td>" +
                          "<td style='text-align:center;'>" + subnikenaem(item.NikeName) + "</td>" +
                          "<td style='text-align:center;'><span style='color:red;'>" + item.WinMonery + "</span>元</td>" +
                     "</tr>";
        }
    </script>

</asp:Content>
