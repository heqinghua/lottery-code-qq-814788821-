<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProfitLossList.aspx.cs" Inherits="Ytg.ServerWeb.Mobile.userCenter.ProfitLossList"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
    <head>
        <title>团队盈亏</title>
        <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <meta name="format-detection" content="telephone=no">
    <meta name="format-detection" content="email=no">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <link href="/Content/Css/feile/comn.css" rel="stylesheet" />
    <link href="/Content/Css/feile/keyframes.css" rel="stylesheet" />
    <link href="/Content/Css/feile/main.css" rel="stylesheet" />
    <link href="/Content/Css/feile/homepg.css" rel="stylesheet" />
    <link href="/Content/Css/base.css" rel="stylesheet" type="text/css" media="all" />
   <link href="/Mobile/Css/layout.css" rel="stylesheet" />
    <link href="/Content/Css/home.css" rel="stylesheet" type="text/css" media="all" />
    <script src="/Content/Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <!--[if lte IE 6]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <!--[if lte IE 7]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <!--[if IE 8]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <script src="/Content/Scripts/jquery.sortable.js" type="text/javascript"></script>
    <script src="/Content/Scripts/config.js" type="text/javascript"></script>
    <script src="/Content/Scripts/basic.js" type="text/javascript"></script>
    <script src="/Content/Scripts/common.js" type="text/javascript"></script>
    <script src="/Content/Scripts/comm.js" type="text/javascript"></script>
     <!--头部结束-->
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" type="text/css" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js" type="text/javascript"></script>
    <script src="/Content/Scripts/Dialog/jquery.dragdrop.js" type="text/javascript"></script>
    <link href="/Mobile/css/subpage1.css" rel="stylesheet" />
     <script src="/Content/Scripts/layout.js" type="text/javascript"></script>
    <link href="/Mobile/css/style1.css" rel="stylesheet" />
      <link href="/Mobile/css/bootstrap1.css" rel="stylesheet" />
    <link href="/Mobile/css/list.css" rel="stylesheet" />
    <link href="/Content/Scripts/Pager/kkpager_orange.css" rel="stylesheet" />
    <script src="/Content/Scripts/Pager/kkpager.min.js"></script>
    <!--消息框代码开始-->
    <script src="/Content/Scripts/dialog-min.js" type="text/javascript"></script>
    <link href="/Content/Css/ui-dialog.css" rel="stylesheet" />
    <script src="/Content/Scripts/dialog-plus-min.js" type="text/javascript"></script>
    <link href="/Content/Css/jslider.css" rel="stylesheet" />
    <link href="/Content/Css/jslider.blue.css" rel="stylesheet" />
    <link href="/Content/Css/jslider.plastic.css" rel="stylesheet" />
    <link href="/Content/Css/jslider.round.css" rel="stylesheet" />
    <link href="/Content/Css/jslider.round.plastic.css" rel="stylesheet" />
    <script src="/Content/Scripts/jslider/jshashtable-2.1_src.js" type="text/javascript"></script>
    <script src="/Content/Scripts/jslider/jquery.numberformatter-1.2.3.js" type="text/javascript"></script>
    <script src="/Content/Scripts/jslider/tmpl.js" type="text/javascript"></script>
    <script src="/Content/Scripts/jslider/jquery.dependClass-0.1.js" type="text/javascript"></script>
    <script src="/Content/Scripts/jslider/draggable-0.1.js" type="text/javascript"></script>
    <script src="/Content/Scripts/jslider/jquery.slider.js" type="text/javascript"></script>
        <script src="/Content/Scripts/jquery.zclip.min.js" type="text/javascript"></script>  
        <script src="/Content/Scripts/datepicker/WdatePicker.js"></script>
    <style type="text/css">
        .l-list table {
            width: 100%;
        }

            .l-list table tr td {
                height: 40px;
                line-height: 40px;
            }

        .ltbody tr th {
            font-weight: bold;
        }

        .meTr td {
            /*color: #cd0228;*/
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
          Ytg = $.extend(Ytg, {
            SITENAME: "<%=Ytg.ServerWeb.BootStrapper.SiteHelper.GetSiteName() %>",
            SITEURL: window.location.host,
            RESOURCEURL: "/Content",
            BASEURL: "/",
            SERVICEURL: "",
            NOTEFREQUENCY: 10000
        });
        // Ytg.namespace("Ytg.Lottery.user");
        Ytg.common.user.info = {
            user_id: '<%=CookUserInfo.Id%>',
            username: '<%=CookUserInfo.Code%>',
            nickname: '<%=CookUserInfo.NikeName%>'
        };

        var parUserId = Ytg.common.user.info.username;
        $(function () {
            $("#yingkui").addClass("title_active");

            $("#lbtnSearch").click(function () {
                parUserId = Ytg.common.user.info.username;
                loaddata();//加载数据
            });
            //var today = new Date(); // 获取今天时间
            //var today = today.getDate() - 1;
            //today.setDate(today.getDate() + -1); // 系统会自动转换
           // $("#txtstart").val("<%=Ytg.Comm.Utils.GetNowBeginDate().ToString("yyyy/MM/dd HH:mm:ss")%>" );


            loaddata();//加载数据
        });

        function loaddata(account) {
            if (account != undefined)
                parUserId = account;

            Ytg.common.loading();
            var st = $("#txtstart").val();
            var ed = $("#txtend").val();
            $.ajax({
                url: "/Page/Repot/AmtChange.aspx",
                type: 'post',
                data: "action=selectprofitlossslist&startTime=" + st + "&endTime=" + ed + "&account=" + (account == undefined ? $("#userCode").val() : account),
                success: function (data) {
                  
                    Ytg.common.cloading();
                    var jsonData = JSON.parse(data);
                    //清除
                    $(".ltbody").children().remove();
                    if (jsonData.Code == 0 && jsonData.Data.length > 0) {
                        var sumChongzhi = 0;
                        var sumTixian = 0;
                        var sumTouzhu = 0;
                        var sumYouxifandian = 0;
                        var sumTotalJiangjin = 0;
                        var sumTotalYingkui = 0;
                        var sumCheDan = 0;

                        var othersumChongzhi = 0;
                        var othersumTixian = 0;
                        var othersumTouzhu = 0;
                        var othersumYouxifandian = 0;
                        var othersumTotalJiangjin = 0;
                        var othersumTotalYingkui = 0;
                        var othersumCheDan = 0;
                        for (var c = 0; c < jsonData.Data.length; c++) {
                            var item = jsonData.Data[c];
                            //alert(item.Fenhong);
                         //   alert(item.Youxifandian);
                            //item.Chedanfankuan item.Zhuihaofankuan
                           // alert((item.Touzhu + item.Zhuihaokoukuan) + "  " + (item.Chedanfankuan + item.Zhuihaofankuan));
                            item.Touzhu = (item.Touzhu + item.Zhuihaokoukuan) + (item.Chedanfankuan + item.Zhuihaofankuan);
                            item.Youxifandian = item.Youxifandian ;
                            item.TotalJiangjin = item.Jiangjinpaisong ;
                            item.TotalYingkui = item.Touzhu + item.Youxifandian + item.TotalJiangjin + item.Fenhong+item.Huodonglijin;
                            var htm = "<tr>";
                           
                            var actionHtm = "";
                            if (item.Id == Ytg.common.user.info.user_id) {
                                actionHtm = "<a href=\"javascript:showDetail('" + item.Code + "');\">查看明细</a>";
                                sumChongzhi = parseFloat(item.Chongzhi);
                                sumTixian = parseFloat(item.Tixian);
                                sumTouzhu = parseFloat(Math.abs(item.Touzhu ));
                                sumYouxifandian = parseFloat(item.Youxifandian);
                                sumTotalJiangjin = parseFloat(item.TotalJiangjin);
                                sumTotalYingkui = parseFloat(item.TotalYingkui);
                            }
                            else {
                                actionHtm = "<a href=\"javascript:loaddata('" + item.Code + "');\">查看下级</a>";
                                if (parUserId != item.Code) {
                                    othersumChongzhi += parseFloat(item.Chongzhi);
                                    othersumTixian += parseFloat(item.Tixian);
                                    othersumTouzhu += parseFloat(Math.abs(item.Touzhu ));
                                    othersumYouxifandian += parseFloat(item.Youxifandian);
                                    othersumTotalJiangjin += parseFloat(item.TotalJiangjin);
                                    othersumTotalYingkui += parseFloat(item.TotalYingkui);
                                }
                            }
                            if (parUserId == item.Code) {
                                htm = "<tr class='meTr'>";
                                actionHtm = "<a href=\"javascript:showDetail('" + item.Code + "');\">查看明细</a>";

                                sumChongzhi = parseFloat(item.Chongzhi);
                                sumTixian = parseFloat(item.Tixian);
                                sumTouzhu = parseFloat(Math.abs(item.Touzhu ));
                                sumYouxifandian = parseFloat(item.Youxifandian);
                                sumTotalJiangjin = parseFloat(item.TotalJiangjin);
                                sumTotalYingkui = parseFloat(item.TotalYingkui);
                            }
                            var rebates = (<%=Ytg.Comm.Utils.MaxRemo%> -item.Rebate).toFixed(1);
                            if (rebates < 0)
                                rebates = 0;
                            htm += "<td>" + item.Code + "</td>";
                           // htm += "<td>" + rebates + "</td>";
                            //htm += "<td>" + decimalCt(Ytg.tools.moneyFormat(Math.abs(item.Chongzhi))) + "</td>";
                            //htm += "<td>" + decimalCt(Ytg.tools.moneyFormat(Math.abs(item.Tixian))) + "</td>";
                            htm += "<td >" + decimalCt(Ytg.tools.moneyFormat(Math.abs(item.Touzhu))) + "</td>";
                           // htm += "<td>" + decimalCt(Ytg.tools.moneyFormat(item.Youxifandian)) + "</td>";
                           // htm += "<td>" + decimalCt(Ytg.tools.moneyFormat(item.TotalJiangjin)) + "</td>";
                            htm += "<td>" + decimalCt(Ytg.tools.moneyFormat(item.TotalYingkui)) + "</td>";
                            //htm += "<td>" + actionHtm + "</td>"
                            htm += "</tr>";
                            $(".ltbody").append(htm);
                        }
                        //合计
                        var ismin = sumTotalYingkui < 0;
                        var sumHtm = "<tr>";
                        sumHtm += "<td colspan='0' style='text-align:right;color:'><span style='color:#000;'>合计：</span></td>";
                       // sumHtm += "<td>" + decimalCt(Ytg.tools.moneyFormat((sumChongzhi))) + "</td>";
                        //sumHtm += "<td>" + decimalCt(Ytg.tools.moneyFormat((sumTixian))) + "</td>";
                        sumHtm += "<td  >" + decimalCt(Ytg.tools.moneyFormat(sumTouzhu)) + "</td>";
                        //sumHtm += "<td>" + decimalCt(Ytg.tools.moneyFormat((sumYouxifandian))) + "</td>";
                       // sumHtm += "<td> " + decimalCt(Ytg.tools.moneyFormat(sumTotalJiangjin)) + "</td>";
                        sumHtm += "<td>" + decimalCt(Ytg.tools.moneyFormat((sumTotalYingkui))) + "</td>";
                        sumHtm += "<td></td>";
                        sumHtm += "</tr>";
                        $(".ltbody").append(sumHtm);

                        //修改第一行值
                        if ($(".ltbody").children().length > 0) {
                            
                            var fstr = $(".ltbody").children().first().children("td");
                           // fstr.eq(2).html(decimalCt(Ytg.tools.moneyFormat((sumChongzhi- othersumChongzhi))));
                            //fstr.eq(3).html(decimalCt(Ytg.tools.moneyFormat((sumTixian-othersumTixian))));
                            fstr.eq(2).html(decimalCt(Ytg.tools.moneyFormat((sumTouzhu-othersumTouzhu))));
                            //fstr.eq(5).html(decimalCt(Ytg.tools.moneyFormat((sumYouxifandian - othersumYouxifandian).toFixed(3))));
                            //fstr.eq(6).html(decimalCt(Ytg.tools.moneyFormat((sumTotalJiangjin-othersumTotalJiangjin))));
                            fstr.eq(3).html(decimalCt(Ytg.tools.moneyFormat(((sumTotalYingkui - othersumTotalYingkui).toFixed(3)))));
                        }
                        $(".ltbody").children().last().children().css("color", "#cc0228");
                    } else {
                        $(".ltbody").Empty(4);
                    }
                }
            });
        }
        function showDetail(account) {
            window.location = "/Views/Report/AmountChangeList.aspx?account=" + account;
        }
    </script>
</head>
    <body>
         <nav class="col-xs-12 title" style="position:fixed;z-index:999;left:0px;top:0px;background:#ec2829;">
         <a id="J-goback" href="/wap/users/Main.aspx/Opens.aspx" class="go-back">返回</a>
         盈亏报表</nav>
        <div class="ctParent">
    <div class="control">
        <!--工具栏-->
        <div class="toolbar-wrap">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="formTable xk_input_head" style="display:none;">
                <tbody>
                    <tr>
                        <td style="text-align:right;width:20%;">时间：</td><td >
                            <input id="txtstart" type="text" class="input date"  value="<%=Ytg.Comm.Utils.ToGetNowBeginDateStr()%>" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"  style="width:100%;" />&nbsp;至&nbsp;
                            <input id="txtend" type="text" class="input date" value="<%=Ytg.Comm.Utils.ToGetNowEndDateStr()%>" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" style="width:100%;"/>
                        </td>
                        
                    </tr>
                    <tr style="display:none;">
                        <td style="text-align:right;">用户：</td><td><input type="text" id="userCode" class="input normal"  /></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <input name="" id="lbtnSearch" type="button" value="查询" class="formCheck"></td>
                    </tr>
                </tbody>
            </table>
          
        </div>
        <!--/工具栏-->
        <!--列表-->
        <div>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="grayTable">
                <thead>
                    <tr>
                        <th >用户</th>
                        <%--<th align="left" >级别</th>--%>
                        <%--<th align="left" >充值总额</th>--%>
                        <%--<th align="left" >提现总额</th>--%>
                        <th align="left" >投注总额</th>
                       <%-- <th align="left" >返点总额</th>--%>
                        <%--<th align="left">中奖总额</th>--%>
                        <th align="left" >总盈亏</th>
                        <%--<th align="left">操作</th>--%>
                    </tr>
                </thead>
                <tbody class="ltbody">
                    <tr>
                        <td colspan="9">暂无记录!</td>
                    </tr>
                </tbody>
            </table>
        </div>
        <!--/列表-->
    </div>
            </div>
</body>
    </html>
