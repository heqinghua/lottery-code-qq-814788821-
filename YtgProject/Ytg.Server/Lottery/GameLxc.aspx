<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameLxc.aspx.cs" Inherits="Ytg.ServerWeb.Lottery.GameLxc" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, minimum-scale=1, user-scalable=no, minimal-ui" />
    <meta name="mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <link href="/Content/Images/skin/favicon.ico" rel="shortcut icon" type="images/x-icon" />
    <title></title>
     <script src="/Content/Scripts/playname.js"></script>
    <script src="/Content/Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <!--[if lte IE 6]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <!--[if lte IE 7]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <!--[if IE 8]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <script src="/Content/Scripts/config.js" type="text/javascript"></script>
    <script src="/Content/Scripts/basic.js" type="text/javascript"></script>
    <script src="/Content/Scripts/comm.js" type="text/javascript"></script>
    <link href="/Content/Css/home.css" rel="stylesheet" />
    <link href="/Content/Css/layout.css" rel="stylesheet" />
    <link href="/Content/Css/comn.css" rel="stylesheet" />
    <link href="/Content/Css/feile/main.css" rel="stylesheet" />
    <link href="/Content/Scripts/Lottery/lhc/liuhecai.css" rel="stylesheet" />
    <script type="text/javascript">
        Ytg = $.extend(Ytg, {
            SITENAME: "乐诚网",
            SITEURL: window.location.host,
            RESOURCEURL: "/Content",
            BASEURL: "/",
            SERVICEURL: "",
            NOTEFREQUENCY: 10000
        });
        Ytg.namespace("Ytg.Lottery.user");
        Ytg.common.user.info = {
            user_id: '<%=CookUserInfo.Id%>',
            username: '<%=CookUserInfo.Code%>',
            nickname: '<%=CookUserInfo.NikeName%>'
        };

    </script>
    <script src="/Content/Scripts/common.js" type="text/javascript"></script>
    <script src="/Content/Scripts/Lottery/Lottery_lang_zh.js" type="text/javascript"></script>
    <script src="/Content/Scripts/Lottery/lhc/jquery.youxi.main.lhc.js"></script>
    <script src="/Content/Scripts/Lottery/lhc/jquery.youxi.trace.lhc.js"></script>

    <style>
        #lottery-number-logs-contents {
            font-size: 12px;
        }

            #lottery-number-logs-contents a {
                color: #ff6600;
            }

        #lottery-number-logs-title span {
            background: url("/content/images/skin/rec_tzicon_bg.png") no-repeat left center;
            display: inline-block;
            height: 28px;
            line-height: 26px;
            padding-left: 26px;
        }

        .lottery-number-logs #lottery-number-logs-title a {
            display: inline-block;
            float: right;
            width: 64px;
            height: 26px;
            text-indent: -3000px;
            background: url("/content/images/skin/tz_balls_bg.png") no-repeat scroll -61px -300px rgba(0, 0, 0, 0);
        }
    </style>
    <script type="text/javascript">
        var openPlayState=true;//开奖声音播放状态
        var lotterytype =<%=LotteryId%>;//彩种
        var lottertCode='<%=LotteryCode%>';

        (function ($) {
           
            $(document).ready(function () {
                inintPlayStat();//获取声音播放状态
                //$.gameInit开始
                $.gameInit({
                    data_label: [<%=JsonData%>],
                    cur_issue: <%=NowIssue%>,
                    issues: {//所有的可追号期数集合
                        today: [<%=NextIssues%>],
                        tomorrow: [<%=NextDayIssues%>]
                    },
                    servertime:'<%=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")%>',
                    lotteryid: lotterytype,
                    lotteryCode:lottertCode,
                    ajaxurl: '',
                    ontimeout: function () {
                        //$("#ad").show();
                    },
                    onend:function(){ //结束当期，准备开奖
                        refold();
                    },
                    onfinishbuy: function () {//刷新投注记录
                        getCurProjects();
                    }
                });
                //$.gameInit结束

                $('span[class="project_more"]', "#history_project").live("click", function () {
                    var mme = this;
                    var $h = $('<span style="display: block;" id="task_div">号码详情[<span class="close">关闭</span>]<br><textarea readonly="readonly" class="code1">' + $(mme).next().html() + '</textarea></span>');
                    $(this).openFloat($h, "projects_more");
                    $("span", $(this).parent()).filter(".close").click(function () {
                        $(mme).closeFloat();
                    });
                });

                var iLid = parseInt(20, 10);
                var position = -(iLid - 1) * 75;
                $("#right_top_01_02").css("background-position", "0px " + position + "px");

                /*开奖*/
                var refCount=0;
                var reftinterval;
                var refoldTime;
                function refold(){
                    $.ajax({
                        type: 'POST',
                        url : '/Page/Lott/LotteryBasic.aspx',
                        data: "lotteryid="+lotterytype+"&lotterycode="+lottertCode+"&action=OpenResult&issue=" +$.lt_preissue.issue+"&dt="+new Date(),
                        success: function(data){
                            if(reftinterval!=undefined)
                                clearInterval(reftinterval);
                            if(data==""){
                                refCount++;
                                if(refCount<10){
                                    reftinterval=setInterval(refold(),3000);//继续请求开奖;
                                }else{
                                    refCount=0;//获取开奖号码失败
                                    parent.$.alert("获取开奖号码失败，请刷新页面！");
                                }
                                return;
                            }
                            var jsonData= JSON.parse(data);
                            if (jsonData.Code == 1009) {
                                alert("由于您长时间未操作,为确保安全,请重新登录！");
                                parent.window.location = "/login.html";
                            }else if (jsonData.Code!=0){
                                processCode($.lt_preissue.issue,"",true);
                                reftinterval=setInterval(refold,3000);//继续请求开奖;
                                return;
                            }
                            $("#ad").hide();
                            //继续请求
                            times=true;
                            processCode($.lt_preissue.issue,jsonData.Data.Result,2);
                        }
                    } );
                }
                //设置开奖号码
                var openres="<%=OpenResult%>".replace(/\,/g,"").replace("+","");
                processCode('<%=OpenIssueCode%>',openres,2);
            });
        })(jQuery);
        /***开奖声音控制*/
        function inintPlayStat(){
            var playState= getCookie("openresult_state");
            if(playState==undefined || playState=="" || playState=="true" ){
                openPlayState=true;                
            }
            else{
                openPlayState=false;
            }
            //
            setPlayState(openPlayState);
            $("#soundCtl").click(function(){
                openPlayState=$("#soundCtl").hasClass("soundoff");
                setPlayState(openPlayState);
                setCookie("openresult_state",openPlayState==true?"true":"false");
            });
        }
        function setPlayState(isOpen){
            if(isOpen)
            {
                if($("#soundCtl").hasClass("soundoff"))
                    $("#soundCtl").removeClass("soundoff");
                $("#soundCtl").addClass("soundon");
            }else
            {
                if($("#soundCtl").hasClass("soundon"))
                    $("#soundCtl").removeClass("soundon");
                $("#soundCtl").addClass("soundoff");
            }
        }
        /****/
        function processCode(issue, code, iscurent) {
            $('.no_old').html(issue);
            //alert(issue);
            var codeArr = [];
            if (code.length == 14) {
                var str = "";
                $.each(code.split(""), function (i, n) {
                    str += n;
                    if (i % 2 == 1) {
                        codeArr.push(str);
                        str = "";
                    }
                });
            } else {
                return false;
            }

            var ball = {
                "blue": ['03', '04', '09', '10', '14', '15', '20', '25', '26', '31', '36', '37', '41', '42', '47', '48'],
                "green": ['05', '06', '11', '16', '17', '21', '22', '27', '28', '32', '33', '38', '39', '43', '44', '49'],
                "red": ['01', '02', '07', '08', '12', '13', '18', '19', '23', '24', '29', '30', '34', '35', '40', '45', '46']
            };

            if (iscurent == 1) {
                reDraw(codeArr, ball, ".c_ball");
            }

            if (iscurent == 2) {
                reDraw(codeArr, ball, ".c_ball");
            }

            //重绘界面方法
            function reDraw(arr, obj, node) {
                var _html = "<ul>";
                $.each(arr, function (i, n) {
                    if (arr.length - 1 == i) {
                        _html += '<li><div class="sign"></div></li>';
                    }
                    $.each(obj, function (ii, nn) {
                        if ($.inArray(n, nn) != -1) {
                            _html += '<li><div class="' + ii + '-ball">' + n + '</div></li>';
                        }
                    });
                });
                _html += "</ul>";
                $(node).empty().html(_html);
            }
        }
        //清空追号方案
        var cleanTraceIssue = function () {
            $("input[name^='lt_trace_issues']", $($.lt_id_data.id_tra_issues)).attr("checked", false);
            $("input[name^='lt_trace_times_']", $($.lt_id_data.id_tra_issues)).val(0).attr("disabled", true);
            $("span[id^='lt_trace_money_']", $($.lt_id_data.id_tra_issues)).html('0.00');
            $("td", $($.lt_id_data.id_tra_issues)).removeClass("selected");
            $('#lt_trace_hmoney').html(0);
            $('#lt_trace_money').val(0);
            $('#lt_trace_count').html(0);
            $.lt_trace_issue = 0;
            $.lt_trace_money = 0;
        };

        function isIE()
        {
            if(!!window.ActiveXObject || "ActiveXObject" in window)
                return true;
            else
                return false;
        }

        function play_click(url){    
            if(isIE()){

                var div = document.getElementById('sound');   
                if(div.innerHTML==""){
                    div.innerHTML = '<embed id="sliod" src="'+url+'" loop="0" autostart="true" hidden="true"></embed>';   
                    var emb = document.getElementsByTagName('EMBED')[0];    
                }else{
                    if(document.getElementById("sliod"))
                        document.getElementById("sliod").play();
                }
            }else{
                var div = document.getElementById('sound');   
                if(div.innerHTML==""){
                    div.innerHTML = '<audio id="sliod" src="'+url+'" ></audio>';  
                }else{
                    if(document.getElementById("sliod")!=undefined)
                        document.getElementById("sliod").play();
                }
            }
           
        }
        /**获取投注记录*/
        function getCurProjects(){
            $("#projectloading").show();
            $.ajax({
                url: '/Page/Lott/LotteryBetDetail.aspx?lotterycode='+ lottertCode +'&lotteryid=' + lotterytype,
                type:'post',
                data: 'action=notopenbetlist',
                error:function(){
                    $("[name=his_reftr]").remove();//移除投注记录内容
                    $("#projectloading").hide();
                },
                success: function(data){
                    $("[name=his_reftr]").remove();//移除投注记录内容
                    //组织数据
                    $("#projectloading").hide();
                    var jsonData=JSON.parse(data);
                    if(jsonData.Code==0 && jsonData.Data.length>0){                        
                        for(var i=0;i<jsonData.Data.length;i++){
                            var item=jsonData.Data[i];
                            var st=item.Stauts+"_";//Ytg.common.LottTool.GetState(item);
                            var stauts= Ytg.common.LottTool.GetStateContent(st);
                            var betContent=Ytg.common.LottTool.ShowBetContent(item.BetContent);
                            var exitNums="撤单";
                           
                            if(stauts.indexOf("正在进行")!=-1 || stauts.indexOf("未开奖")!=-1){
                                exitNums="<a id='"+item.BetCode+"' href=\"javascript:cannelOrder('"+item.BetCode+"',this,"+item.tp+");\">撤单</a>"
                            }

                            betContent="<a class='betdetail' style='color:#ff7272;' href='javascript:parent.showDetail(\""+item.BetCode+"\","+item.tp+");'>查看详情</a>";
                            var occTime=getdateTime(item.OccDate);
                            var model={0:"元",1:"角",2:"分"};
                            var contentItem="<tr name='his_reftr'><td>"+occTime+"</td><td>"+changePalyName("特码"+item.PlayTypeRadioName,'')+"</td><td>"+item.IssueCode+"</td><td >"+betContent+"</td><td>"+decimalCt(Ytg.tools.moneyFormat(item.TotalAmt))+"</td>";
                            contentItem+="<td>"+ item.Multiple+"</td>";//倍数
                            contentItem+="<td>"+model[item.Model] +"</td>";//模式
                            contentItem+="<td>"+decimalCt(Ytg.tools.moneyFormat(item.WinMoney))+"</td>";//奖金
                            contentItem+="<td>"+stauts+"</td>"
                            //contentItem+="<td>"+exitNums+"</td></tr>";//操作
                            $(".grayTable").append(contentItem);
                        }
                        $('#main', parent.document).height($('#main', parent.document).contents().find("body").height()); //重设 iframe 高度
                       
                    }else{
                        $(".grayTable").append("<tr name='his_reftr'><td colspan='9'>没有投注记录</td></tr>");
                        $('#main', parent.document).height($('#main', parent.document).contents().find("body").height()+30); //重设 iframe 高度
                    }
                }});
        }
        getCurProjects();//获取投注历史记录

        //撤单
        function cannelOrder(betcode,obj,betType){
            
            if (confirm("确定要撤单吗？")) {
                Ytg.common.loading();
                $.ajax({
                    url: "/Page/Lott/LotteryBetDetail.aspx",
                    type: 'post',
                    data: "action=cannelbethnum&bettCode="+betcode+"&lotteryid=<%=Request.Params["ltid"]%>&betType="+betType,
                    success: function (data) {
                        Ytg.common.cloading();
                        var jsonData = JSON.parse(data);
                        //清除
                        if (jsonData.Code == 0) {
                            var p= $("#"+betcode).parent();
                            $("#"+betcode).remove();
                            p.prev().html("<span style='color:#1ea600'>已撤单</span>");
                            p.html("撤单");
                            alert("撤单成功！");
                        }else if(jsonData.Code==1002){
                            alert("对不起，当期投注时间已截止，撤单失败！");
                        }else if (jsonData.Code == 1009) {
                            alert("由于您长时间未操作,为确保安全,请重新登录！");
                            parent.window.location = "/login.html";
                        }
                        else {
                            alert("撤单失败，请刷新后重试！");
                        }
                    }
                });
            }
        }

        function opening(){  try{
            play_click('/Content/audio/playsound.mp3');/*开奖倒数*/
        }catch(ex){
        }/*开奖倒数*/
        }

    </script>
</head>
<body>
    <div id="sound" style="position: absolute;"></div>
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" type="text/css" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js" type="text/javascript"></script>
    <script src="/Content/Scripts/Dialog/jquery.dragdrop.js" type="text/javascript"></script>
  <div class="lottery_frame">
    <div class="lott_ssc_icon">
        <div>
            <div class="ssc_icon" style="background: url(/Content/Images/lt/<%=Request.QueryString["ico"] %>) no-repeat center top;"></div>
                <div class="ssc_type_con"><%=Request.Params["ln"] %></div>
        </div>
    </div>
    <div class="hc_current">
        <div class="hccurdiv_1"><span class="periods">第<span id="current_issue"></span>期</span></div>
        <div class="clock" style="width: 220px; padding-left: 0px; background: url(/content/images/skin/lhc_clock_bg.png) no-repeat;"><span id="count_down" style="width: 220px;"></span></div>
        <div class="hccurdiv_3">投注截止时间</div>
    </div>
    <div class="hc_open" style="width:580px">
        <div class="lo_title">
            <h2><span class="periods" style="color: #000; font-size: 14px;">第<span class="no_old" style="left: -6px; right: 0px;"></span>期开奖结果
                    <a  href="/Views/SignalAsyncLhc.aspx" target="_blank" style="font-size:14px; color:#0094fe; float: right; color: #000">漏号分析</a></span>
            </h2>
        </div>
        <div class="c_ball"></div>
    </div>
    <div class="hc_history">
    </div>
</div>    
    <div id="subContent_bet">
        <div class="box">
<div class="Menubox title clearfix" style="background-color: #ececec;">
    <ul id="lotteryType">
        
    </ul>
</div>

<div id="lotteryDetail">
    <div class="Contentbox" id="Contentbox_0">
        <div class="six_top_box" id="id_lhc">
        <!--<dl>模板
                <dt><span class="w48">号码</span><span class="w50">赔率</span><span class="w50">金额</span></dt>
                <dd><div class="w48"><div class="red-ball_x">01</div></div><span class="w50">42.00</span><input name="" type="text" class="w42" /></dd>
            </dl>-->
        </div>
    
        <!--<div class="six_foot_box"> 红波蓝波绿波模板
            <ul>
                <li><span class="w48">特码单</span><span class="w50">42.00</span><input name="" type="text" class="w42" /></li>
                <li><span class="w48">特码双</span><span class="w50">42.00</span><input name="" type="text" class="w42" /></li>
                <li><span class="w48">大单</span><span class="w50">42.00</span><input name="" type="text" class="w42" /></li>
                <li><span class="w48">小单</span><span class="w50">42.00</span><input name="" type="text" class="w42" /></li>
            </ul>
            <ul>
                <li><span class="w48">特码单</span><span class="w50">42.00</span><input name="" type="text" class="w42" /></li>
                <li><span class="w48">特码双</span><span class="w50">42.00</span><input name="" type="text" class="w42" /></li>
                <li><span class="w48">大单</span><span class="w50">42.00</span><input name="" type="text" class="w42" /></li>
                <li><span class="w48">小单</span><span class="w50">42.00</span><input name="" type="text" class="w42" /></li>
            </ul>
            <ul>
                <li><span class="w48">特码单</span><span class="w50">42.00</span><input name="" type="text" class="w42" /></li>
                <li><span class="w48">特码双</span><span class="w50">42.00</span><input name="" type="text" class="w42" /></li>
                <li><span class="w48">大单</span><span class="w50">42.00</span><input name="" type="text" class="w42" /></li>
                <li><span class="w48">小单</span><span class="w50">42.00</span><input name="" type="text" class="w42" /></li>
            </ul>
            <ul>
                <li><span class="w48">特码单</span><span class="w50">42.00</span><input name="" type="text" class="w42" /></li>
                <li><span class="w48">特码双</span><span class="w50">42.00</span><input name="" type="text" class="w42" /></li>
                <li><span class="w48">大单</span><span class="w50">42.00</span><input name="" type="text" class="w42" /></li>
                <li><span class="w48">小单</span><span class="w50">42.00</span><input name="" type="text" class="w42" /></li>
            </ul>
            <ul>
                <li><span class="w48 red_color">红波</span><span class="w50">42.00</span><input name="" type="text" class="w42" /></li>
                <li><span class="w48 blue_color">蓝波</span><span class="w50">42.00</span><input name="" type="text" class="w42" /></li>
                <li><span class="w48 green_color">绿波</span><span class="w50">42.00</span><input name="" type="text" class="w42" /></li>
            </ul>
        </div>-->
    
        <div class="gap-line"></div>
        
        <form method="post" name="buyform" action="">
            <div class="clearfix grayBet">
                <div class="fl">
                    您选择了 <font class="hs" id="lt_sel_nums">0</font>  注，共 <font class="hs" id="lt_sel_money">0</font>  元<div style="display:none;">，请输入倍数：<input id="lt_sel_times" type="text" class="num_input"  value="1" border="0"/>　倍　</div>&nbsp;&nbsp;&nbsp;&nbsp;资金模式<span class="curr" num="0">元<input type="radio" name="lt_project_modes" value="1" checked="checked" style="display:none;" /></span>
                    <!--<span class="default" num="1">角<input type="radio" name="lt_project_modes" value="2" style="display:none;" /></span>
                    <span class="default" num="2">分<input type="radio" name="lt_project_modes" value="3" style="display:none;" /></span>-->
                </div>
                <span class="fr"><input id="lt_sel_insert" type="button" value="添加投注" class="formOk" /></span>
            </div>
            <div class="lottery_list clearfix">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="100%" height="22"><!--div class="cleanall"></div--></td>
                    <td></td>
                    <td></td>
                  </tr>
                  <tr>
                    <td width="100%"><div class="whiteDetail"><table cellspacing="0" cellpadding="0" id="lt_cf_content"></table></div></td>
                    <td><div class="layout_jixian" id="lt_random_area" style="width: 0px;"></div></td>
                    <!--td><div class="tjBt_r"><a href="#_" class="tjBt" onclick="return(false);" id="lt_sendok">提交</a></div></td-->
                  </tr>
                </table>    
            </div>
            <div class="tjBox clearfix">
                <div class="floatR"><a href="#_" class="tjBt" onclick="return(false);" id="lt_sendok">立即投注</a></div>
                <div class="floatL">
            已选择 <span id="lt_cf_count" class="hs">0</span>  单 总注数 <span class="hs" id="lt_cf_nums">0</span>  注，总金额 <span class="hs" id="lt_cf_money">0</span>  元 <span style="display:none;">， 起始期：<span id="lt_issues"></span></span>
                <span class="zh_box" style="display:none;">
                <label><input type="checkbox" name="lt_trace_if" id="lt_trace_if_button"  value="no" /><font class="oanger">发起追号</font></label>
                <label><input name="lt_trace_stop" type="checkbox" value="yes" id="lt_trace_stop" disabled="disabled" />中奖后停止追号</label>
                </span>
                </div>
            </div>
            <!--我要追号-->
            <div id="lt_trace_box1" class="wyzhContent" style="display:none;">
                <div id="lt_trace_label"></div>
                <div class="head clearfix">
                      <div class="headContent">
                        <p>追号期数：<select class="select3" id="lt_trace_qissueno">
                            <option value="">请选择</option>
                            <option value="5">5期</option>
                            <option value="10">10期</option>
                            <option value="15">15期</option>
                            <option value="20">20期</option>
                            <option value="25">25期</option>
                            <option value="all">全部</option>
                          </select>　总期数：<span class="hs" id="lt_trace_count">0</span>期　　追号总金额：<span class="hs" id="lt_trace_hmoney">0.00</span>元<p>
                          <!--<p>追号计划： 起始倍数 <input name="" type="text" value="1" size="3" />　最低收益率<input name="" type="text" value="50" size="3" />%　追号期数:<input name="" type="text" value="10" size="3" /></p>-->
                          <span id="lt_trace_labelhtml"></span>　<span class="red" id="lt_trace_alcount" style="display:none;">全部追号期数</span>
                          <input name="" type="button" value="生成" class="formSC" id="lt_trace_ok" />
                      </div>
                </div>
                <div class="zhlist"  id="lt_trace_issues"></div>
            </div>
             <div id="sty1-number" class="lottery-number-logs" style="padding: 5px 12px;">
                            <div id="lottery-number-logs-title" style="padding: 0 4px">
                                <span>投注记录</span>
                                <a href="javascript:getCurProjects(1);">刷新</a>
                                <div class="clear"></div>
                            </div>
                            <div id="projectloading" style="text-align: center; display: none; height: 100px; vertical-align: middle;">
                                <img style="vertical-align: middle; margin-top: 40px" src="/Content/images/skin/loading.gif" />
                            </div>
                            <div id="lottery-number-logs-contents">
                                <table width="100%" cellspacing="0" cellpadding="0" border="0" class="grayTable">
                                    <tr>
                                        <th style="color: #000;">投注时间</th>
                                        <th style="color: #000;">玩法</th>
                                        <th style="color: #000;">期号</th>
                                        <th style="color: #000;">投注内容</th>
                                        <th style="color: #000;">投注金额</th>
                                        <th style="color: #000;">倍数</th>
                                        <th style="color: #000;">模式</th>
                                        <th style="color: #000;">奖金</th>
                                        <th style="color: #000;">状态</th>
                                      <%--  <th style="color: #000;">操作</th>--%>
                                    </tr>
                                </table>
                            </div>
                        </div>
        </form>
    </div>
</div>
</div>
</div>
</body>
</html>
