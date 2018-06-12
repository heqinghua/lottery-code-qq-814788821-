<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameK3.aspx.cs" Inherits="Ytg.ServerWeb.Lottery.GameK3" %>


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

    <script src="/Content/Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <!--[if lte IE 6]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <!--[if lte IE 7]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <!--[if IE 8]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <script src="/Content/Scripts/playname.js" type="text/javascript"></script>
    <script src="/Content/Scripts/config.js" type="text/javascript"></script>
    <script src="/Content/Scripts/basic.js" type="text/javascript"></script>
    <script src="/Content/Scripts/comm.js" type="text/javascript"></script>
    <link href="/Content/Css/home.css" rel="stylesheet" />
    <link href="/Content/Css/layout.css" rel="stylesheet" />
    <link href="/Content/Css/comn.css" rel="stylesheet" />
    <link href="/Content/Css/feile/main.css" rel="stylesheet" />
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
    <link href="/Content/Css/k3_box.css" rel="stylesheet" />
    <script src="/Content/Scripts/common.js" type="text/javascript"></script>
    <script src="/Content/Scripts/Lottery/Lottery_lang_zh.js" type="text/javascript"></script>
    <script src="/Content/Scripts/jquery.youxi.main.k3.js" type="text/javascript"></script>
    <script src="/Content/Scripts/jquery.youxi.selectarea.k3.js" type="text/javascript"></script>
    <script src="/Content/Scripts/jquery.youxi.trace.k3.js" type="text/javascript"></script>

    <script type="text/javascript">
        var openPlayState=true;//开奖声音播放状态
        var lotterytype =<%=LotteryId%>;//彩种
        var lottertCode='<%=LotteryCode%>';
        var lottery_methods=<%=lottery_methods%>;
        $(function () {
           
            inintPlayStat();//获取声音播放状态
            //$.gameInit开始
            $.gameInit({
                data_label: [<%=JsonData%>],
                data_methods:lottery_methods,
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
                    parent.$.alert("当前销售已截止，请进入下一期购买！");
                    //$("#ad").show();
                },
                onend:function(){ //结束当期，准备开奖
                    refold();
                   
                },
                onfinishbuy: function () {//刷新投注记录
                    getCurProjects();
                }
            });

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
                                reftinterval=setInterval(refold(),1500);//继续请求开奖;
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
                            reftinterval=setInterval(refold,1500);//继续请求开奖;
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
            //设置开奖号码
            var def_openresult="<%=OpenResult%>";
            var def_openIssue='<%=OpenIssueCode%>';
            if(def_openIssue!='')
            {
                processCode(def_openIssue,def_openresult,2);
            }else{
                //准备请求刷新开奖结果
                refold();
            }
           
            //增加倍数、减少倍数
            $("#times_sub").click(function(){
                //减少倍数
                var curBei= $("#lt_sel_times").val();
                if(parseInt(curBei)<=1)
                {
                    $("#lt_sel_times").val("1");
                    return;
                }else{
                    $("#lt_sel_times").val((parseInt(curBei)-1));
                }
                $("#lt_sel_times").keyup();
            });

            $("#times_add").click(function(){
                //增加倍数
                var curBei= $("#lt_sel_times").val();
                if(parseInt(curBei)>=10000)
                {
                    $("#lt_sel_times").val("10000");
                    return;
                }else{
                    $("#lt_sel_times").val((parseInt(curBei)+1));
                }
                $("#lt_sel_times").keyup();
            });
            //增加倍数、减少倍数 end 
            $('#main', parent.document).height($('#main', parent.document).contents().find("body").height()+30); //重设 iframe 高度
        });
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
        //初始化模式
        function inintmonerymodle(){
            var modle= getCookie("moneyModle_selected_value");
            if(modle=="")
                return;
            $("#moneyModle label").each(function(){
                if($(this).attr("value")==modle)//com_btn
                {
                    $(this).addClass("com_btn");
                }else{
                    if(!$(this).hasClass("com_btn_h"))
                        $(this).addClass("com_btn_h");
                    $(this).removeClass("com_btn");
                }
            })
        }
        
        //清空
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
        function intval(mixed_var, base) {
            var tmp;
            var type = typeof (mixed_var);
            if (type === 'boolean') {
                return +mixed_var;
            } else if (type === 'string') {
                tmp = parseInt(mixed_var, base || 10);
                return (isNaN(tmp) || !isFinite(tmp)) ? 0 : tmp;
            } else if (type === 'number' && isFinite(mixed_var)) {
                return mixed_var | 0;
            } else {
                return 0;
            }
        }
        function show_on() {
            $("#lt_help_div").show("slow");
        }
        function close_on() {
            $("#lt_help_div").hide("slow");
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
                            
                            var stauts=Ytg.common.LottTool.GetStateContent(st);
                            var betContent=Ytg.common.LottTool.ShowBetContent(item.BetContent);
                            //var exitNums="撤单";
                            //if(stauts.indexOf("正在进行")!=-1 || stauts.indexOf("未开奖")!=-1){
                            //    exitNums="<a id='"+item.BetCode+"' href=\"javascript:cannelOrder('"+item.BetCode+"',this,"+item.tp+");\">撤单</a>"
                            //}
                            // betContent="<a class='betdetail' style='color:#ff7272;' href='javascript:parent.showDetail(\""+item.BetCode+"\","+item.tp+",\""+item.IssueCode+"\");'>查看详情</a>";
                            var detailSowTitle="查看详情";
                            item.BetContent=Ytg.common.LottTool.ShowBetContent(item.BetContent, 1);
                            if(item.BetContent.length<=50){
                                detailSowTitle=item.BetContent;
                            }
                            betContent="<a class='betdetail' style='color:#ff7272;' href='javascript:parent.showDetail(\""+item.BetCode+"\","+item.tp+",\""+item.IssueCode+"\");'>"+detailSowTitle+"</a>";


                            var model={0:"元",1:"角",2:"分"};
                            var occTime=getdateTime(item.OccDate);
                            var posName=item.PostionName==null?"":item.PostionName;
                            var contentItem="<tr name='his_reftr'><td>"+occTime+"</td><td>"+posName+changePalyName(item.PlayTypeName + "" + item.PlayTypeRadioName, '')+"</td><td>"+item.IssueCode+"</td><td >"+betContent+"</td><td>"+decimalCt(Ytg.tools.moneyFormat(item.TotalAmt))+"</td>";
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
        var times = true;
        var djs=[];
        /**处理开奖*/
        function processCode(issue, code, iscurent){
             
            if (issue.length >= 12){
                $('.no_old').css("left", "-6px");
                $('.no_old').css("right", "0px");
            }
            if (issue.length == 11){
                $('.no_old').css("left", "-4px");
                $('.no_old').css("right", "0px");
            }
            if (issue.length == 7){
                $('.no_old').css("left", "15px");
            }
            if (issue.length == 8){
                $('.no_old').css("left", "12px");
            }
            if(code==''){
                // $('.no_old').html(issue);
                return;
            }
            var cStr = '';
            var aTmp = code.split(',');
            
            if (iscurent == 2){//增加动态抓取
                var latestlot = "<ul><li class=\"qihao1\">" + issue + "<\/li><li class=\"haoma1\">";
                var sCode = "";
                for (var ci = 0; ci < aTmp.length; ci++){
                    sCode += "<span>" + aTmp[ci] + "<\/span>";
                }
                var sInsertHtml = latestlot + sCode + "</li></ul>";
                //倒计时开始 sam
                animateStart();
                djs.boolean = true;
                if (djs.boolean){
                    djs.dataStr = aTmp.join("-");
                    djs.dataHtml = sInsertHtml;
                    setTimeout(animateStop, djs.keep * 1000);
                }
            }
            var num_single = 0, num_double = 0;
            var num_codesort = aTmp; //排序

            for (var ci = 0; ci < aTmp.length; ci++) {
                var curNum = aTmp[ci];
                //curNum=parseInt(curNum,10);
                $("#griddle_" + ci).attr("src", "/Content/images/skin/griddle" + curNum + "_da.png");
            }
            $('.no_old').html(issue);
        }
        /** end */
        function div_slow_show(showslow_id){
            $("#div_slow_id_" + showslow_id).show("slow");
        }
        function div_slow_hide(showslow_id){
            $("#div_slow_id_" + showslow_id).hide("slow");
        }
        var setCookie = function(name, value, expire, path){
            //expire=expire||30*24*60*60*1000;
            var curdate = new Date();
            var cookie = name + "=" + encodeURIComponent(value) + "; ";
            if (expire != undefined || expire == 0){
                if (expire == - 1){
                    expire = 366 * 86400 * 1000; //保存一年
                } else{
                    expire = parseInt(expire);
                }
                curdate.setTime(curdate.getTime() + expire);
                cookie += "expires=" + curdate.toUTCString() + "; ";
            }
            path = path || "/";
            cookie += "path=" + path;
            document.cookie = cookie;
        };
        var getCookie = function(name) {
            var re = "(?:; )?" + encodeURIComponent(name) + "=([^;]*);?";
            re = new RegExp(re);
            if (re.test(document.cookie)) {
                return decodeURIComponent(RegExp.$1);
            }
            return '';
        };
        setCookie('last_lottery_url', top.location.href);
       
        //动画开始
        function animateStart(){
            if (djs.boolean == false){
                $(djs.node).empty();
                $.each($(djs.node), function(i, v){
                    $(v).empty();
                    //动画
                    autoMath(v, djs.dataArr[i]);
                    //}
                });
                djs.boolean = true;
            }
        }
        //拼接span标签
        function joinHtml(num){
            if (num){
                return '<span style="display:none;">' + num + '</span>';
            } else{
                var ra = Math.floor(Math.random() * 10);
                return '<span style="display:none;" class="rotate1">' + ra + '</span>';
            }
        }

        //动画结束
        function animateStop(){
            var len = $(djs.node).length;
            var arr = djs.dataStr.split("-");
            $(djs.node).hide();
            if (djs.boolean){
           
                //chrome css3动画
                $.each($(djs.node), function(k, v){
                    var n = 0;
                    if (djs.fltr){
                        n = k + 1;
                    } else{
                        n = len - k + 1;
                    }
                    window.setTimeout(function(){
                        if (k == 0){
                            //kaijiang._mPlay();
                            autoMath(v, arr[k]);
                        } else{
                            autoMath(v, arr[k]);
                        }
                    }, djs.stopTime * (n));
                });
                //}
                $(".cai_co").prepend(djs.dataHtml);
                if ($(".cai_co ul").length > 5){
                    $(".cai_co ul:last").remove();
                }
                window.setTimeout(function(){djs.boolean = false;}, djs.stopTime * (len + 1));
            }
        }
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
        function opening(){
            if(!openPlayState)
                return;
            try{
                play_click('/Content/audio/playsound.mp3');/*开奖倒数*/
            }catch(ex){
            }
        }
        //撤单
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
         function jjtc() {
             parent.$.alert('即将推出，敬请期待');
         }

    </script>
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
            background: url("/content/images/skin/tz_balls_bg.png") no-repeat scroll -61px -310px rgba(0, 0, 0, 0);
        }
    </style>

</head>
<body>
    <div id="sound" style="position: absolute;"></div>
    <table style="width: 100%;">
        <tr>
            <td id="leftcontent" valign="top">
                <div class="lottery_frame" style="margin: auto; width: 1000px;">
                    <div class="lott_ssc_icon" style="margin-left: 20px; margin-right: 20px;">
                        <div>
                           <div class="ssc_type_con" style="font-size: 24px; font-weight: bold;"><%=Request.Params["ln"] %></div>

                            <div class="num_type" style="margin-top: 10px; font-size: 12px; color: #000;">
                                游戏提示音：<span id="soundCtl" class="soundoff" style="display: inline-block"></span>
                            </div>
                        </div>
                    </div>
                    <div class="hc_current">
                        <div class="hccurdiv_1" style="font-size: 12px;"><span id="current_issue"></span><span id="current_endtime" style="display: none;"></span></div>
                        <div class="clock"><span id="count_down"></span></div>
                        <div class="hccurdiv_3">投注截止时间</div>
                    </div>
                    <div class="hc_open" style="margin-left: 20px; margin-right: 20px;">
                        <div class="lo_title">
                            <h2><span class="periods">江苏快三第<span class="no_old"></span>期开奖结果</span></h2>
                        </div>
                        <div class="k3_open">
                            <ul>
                                <li><span class="each">
                                    <img id="griddle_0" src="/Content/images/skin/griddle_wen_da.png" /></span></li>
                                <li><span class="each">
                                    <img id="griddle_1" src="/Content/images/skin/griddle_wen_da.png" /></span></li>
                                <li><span class="each">
                                    <img id="griddle_2" src="/Content/images/skin/griddle_wen_da.png" /></span></li>
                            </ul>
                        </div>
                     <%--   <div class="num_type">
                            <!--前三：----  后三：---- -->
                            <span id="soundCtl" class="soundon" style="display: inline-block"></span>
                        </div>--%>
                    </div>
                  <%--  <div class="hc_history">
                        <div class="hc_history_tit"><span class="date">历史开奖号码</span><span class="number"><a style="font-size: 12px; float: right;" href='/Views/SignalAsync.aspx?lotteryid=<%=Request.QueryString["ltid"] %>&lottery=<%=Server.UrlEncode(Request.QueryString["ln"]) %>&issuecount=30' class="lhfx-btn" target="_blank">漏号分析</a></span></div>
                        <div class="cai_co">
                            <%=Top5OpenResult %>
                        </div>
                    </div>--%>
                </div>

                <div id="subContent_bet">
                    <!--投注页面头部可视代码开始-->
                    <div class="bg clearfix">
                        <!--投注页面头部可视代码结束-->
                        <div class="box" id="k3_box">
                            <div class="Menubox title clearfix">
                                <ul id="lotteryType"></ul>
                            </div>
                            <div id="lotteryDetail2" style="position: relative;">
                                <div id="wrapshow"><span id="nfdprize"></span></div>
                            </div>
                            <div class="Contentbox" id="lotteryDetail">
                                <div class="minitype transparent_class" id="lt_samll_label"></div>
                                <div class="minitype transparent_class">
                                    <span>玩法说明：</span><span id="lt_desc" class="help_con"></span>&nbsp;<span id="lt_example" class="help_exp"><i></i>示例</span><div id="lt_example_div" style="display: none;"></div>
                                    &nbsp;&nbsp;<span id="lt_help" class="help_help"><i></i>帮助</span><div id="lt_help_div" style="display: none;"></div>
                                </div>
                                <!--	<div class="minitype" id="poschoose_1" style="margin-top:-2px; display: none;">
       <span class="methodgroupname">选择位置</span>
	   <label><input type="checkbox" name="pos[]" class="posChoose" value="1">万位</label>
       <label><input type="checkbox" name="pos[]" class="posChoose" value="2">千位</label>
       <label><input type="checkbox" name="pos[]" class="posChoose" value="3">百位</label>
       <label><input type="checkbox" name="pos[]" class="posChoose" value="4">十位</label>
       <label><input type="checkbox" name="pos[]" class="posChoose" value="5">个位</label>
	</div>-->
                                <!--<div id="right_03" class="grayTop">
		<div class="right_02_05"></div>
		<div class="right_02_06"></div>
		</div>-->
                                <!--选号区 -->
                                <div id="lt_selector"></div>
                                <!--选号区结束 -->
                                <!--<div id="right_04" class="grayBottom">
      <div class="right_02_07"></div>
      <div class="right_02_08"></div>
    </div>-->
                                <form method="post" name="buyform" action="">
                                    <input type="hidden" name="curmid" id="curmid" value="2440" />
                                    <input type="hidden" name="poschoose" value="" />
                                    <input type="hidden" name="flag" id="flag" value="save" />
                                    <input type="hidden" name="play_source" id="play_source" />
                                    <div class="clearfix grayBet" style="clear: both;">
                                        <span class="floatR">
                                            <input id="lt_sel_insert" type="button" value="选好了" class="formOk" /></span>
                                        <div class="floatL">
                                            您选择了 <span class="yellow" id="lt_sel_nums">2</span> 注，共 <span class="red" id="lt_sel_money">4</span> 元，请输入倍数：
                                <label class="com_btn sel_times" value="0"><span style="height: 22px; line-height: 22px; padding: 0px 5px;" id="times_sub">-</span></label>
                                            <input id="lt_sel_times" type="text" style="width: 45px; height: 22px;" class="num_input" value="1" border="0" />
                                            <label class="com_btn sel_times" value="0"><span style="height: 22px; line-height: 22px; padding: 0px 5px;" id="times_add">+</span></label>倍
                                倍  
      <script>
          $(function () {
              $("#moneyModle label").click(function () {
                  $(this).siblings("label").addClass("com_btn_h").removeClass("com_btn");
                  $(this).addClass("com_btn").removeClass("com_btn_h");
                  $("#moneyModle_hide input").eq($(this).index()).attr("checked", true).click();
              })
          })
      </script>
                                            <span id="moneyModle">
                                                <label value="1" class="com_btn"><span style="padding: 0px 5px;">元</span></label>
                                                <label value="2" class="com_btn_h"><span style="padding: 0px 5px;">角</span></label>
                                                <label value="3" class="com_btn_h"><span style="padding: 0px 5px;">分</span></label>
                                                <label value="4" class="com_btn_h"><span style="padding: 0px 5px;">厘</span></label>
                                            </span>
                                            <span id="moneyModle_hide" style="display: none">
                                                <input type="radio" name="lt_project_modes" value="1" checked="checked">元模式</label>
                                    <label>
                                        <input type="radio" name="lt_project_modes" value="2">角模式</label>
                                                <label>
                                                    <input type="radio" name="lt_project_modes" value="3">分模式</label>
                                                <label>
                                                    <input type="radio" name="lt_project_modes" value="4">厘模式</label></span>
                                        </div>
                                    </div>
                                    <div class="lottery_list">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="100%" height="20">
                                                    <div class="cleanall"></div>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td width="100%">
                                                    <div class="whiteDetail">
                                                        <table cellspacing="1" cellpadding="0" id="lt_cf_content">
                                                        </table>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="layout_jixian" id="lt_random_area"></div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="tjBox clearfix">
                                        <span class="floatR"><a href="#_" class="tjBt" onclick="return(false);" id="lt_sendok">提交</a></span>
                                        <div class="floatL">
                                            <div class="choose">
                                                已选择<span id="lt_cf_count" class="yellow">0</span>单 总注数 <span class="yellow" id="lt_cf_nums">0</span> 注，总金额 <span class="red" id="lt_cf_money">0</span> 元<span style="display: none;">， 起始期：<span id="lt_issues"></span></span>
                                            </div>
                                            <p class="zuihao_box">
                                                <label class="com_btn">
                                                    <span style="width: auto; background: none; color: #666;">
                                                        <input type="checkbox" name="lt_trace_if" id="lt_trace_if_button" value="no" />发起追号</span></label>
                                                <label>
                                                    <span>
                                                        <input name="lt_trace_stop" type="checkbox" value="yes" id="lt_trace_stop" disabled="disabled" />中奖后停止追号</span></label>
                                            </p>
                                        </div>
                                    </div>
                                    <!--我要追号-->
                                    <div id="lt_trace_box1" class="wyzhContent" style="display: none;">
                                        <div id="lt_trace_label"></div>
                                        <div class="head clearfix">
                                            <div class="headContent">
                                                <p>
                                                    追号期数：
                                        <select class="select3" id="lt_trace_qissueno">
                                            <option value="">请选择</option>
                                            <option value="5">5期</option>
                                            <option value="10">10期</option>
                                            <option value="15">15期</option>
                                            <option value="20">20期</option>
                                            <option value="25">25期</option>
                                            <option value="all">全部</option>
                                        </select>
                                                    总期数： <span class="red" id="lt_trace_count">0</span> 期   追号总金额： <span class="red" id="lt_trace_hmoney">0.00</span> 元
            <p>
                <!--<p>追号计划： 起始倍数 <input name="" type="text" value="1" size="3" /> 最低收益率  <input name="" type="text" value="50" size="3" /> % 追号期数: <input name="" type="text" value="10" size="3" /></p>-->
                <span id="lt_trace_labelhtml"></span><span class="red" id="lt_trace_alcount" style="display: none;">全部追号期数</span>
                <input name="" type="button" value="生成" class="formSC" id="lt_trace_ok" />
                                            </div>
                                        </div>
                                        <div class="zhlist" id="lt_trace_issues"></div>
                                    </div>
                                    <div id="sty1-number" class="lottery-number-logs" style="padding: 2px">
                                        <div id="lottery-number-logs-title" style="padding: 0 4px">
                                            <span>投注记录</span>
                                            <a href="javascript:getCurProjects(0);">刷新</a>
                                            <div class="clear"></div>
                                        </div>
                                        <div id="projectloading" style="text-align: center; display: none; height: 100px; vertical-align: middle;">
                                            <img style="vertical-align: middle; margin-top: 40px" src="/content/images/loading.gif" />
                                        </div>

                                        <div id="lottery-number-logs-contents" style="height: auto">
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
                                                    <%--    <th style="color: #000; ">操作</th>--%>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </form>
                            </div>
                        </div>


                    </div>
                </div>
                <!-- bet -->
            </td>
            <td style="width: 288px; border-right: 1px solid #4253a4;" valign="top">
                <div>
                    <div style="text-align: center; line-height: 2em; font-size: 16px;">
                        往期开奖
                    </div>
                    <div style="min-height: 233px; overflow: hidden; margin-top: 12px;">
                        <div class="hc_history">
                            <%--<div class="hc_history_tit"><span class="date">历史开奖号</span><span class="number"><a href='/Views/SignalAsync.aspx?lotteryid=<%=Request.QueryString["ltid"] %>&lottery=<%=Server.UrlEncode(Request.QueryString["ln"]) %>&issuecount=30' target="_blank" class="lhfx-btn">漏号分析</a></span></div>--%>
                            <div class='lottery_log'>
                                <ul id="newhislottery"><%=Top5OpenResult %></ul>
                            </div>
                        </div>
                    </div>
                    <div style="text-align: center; line-height: 2em; font-size: 16px;">
                        中奖播报
                    </div>

                    <div id="win_list_parent" direction="up" class="hd_notice">
                        <div class="marquee" style="position: relative;  overflow: hidden; ">
                            <ul style="position: absolute; width: 100%; padding-left: 1%; padding-right: 2%; box-sizing: border-box; top: -8px;" class="marquee" id="marquee2">
                            </ul>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</body>
</html>

<script type="text/javascript">
    $(function () {
        //标识客户端投注来源
        if (getCookie('isclient') == 1) {
            $("#play_source").val(4);
        }
        refWinMessages();
        //中奖播报
        setInterval(function () {
            refWinMessages();
        }, 600000);
        
    })

    function refWinMessages() {
        $.ajax({
            url: "/Page/Messages.aspx",
            type: 'post',
            data: "action=wintts",
            success: function (data) {
                $("#marquee2").children().remove();
                var jsonData = JSON.parse(data);
                //清除
                if (jsonData.Code == 0) {

                    for (var i = 0; i < jsonData.Data.length; i++) {
                        var item = jsonData.Data[i];
                        var showCode = item.Code;
                        if (showCode == null)
                            continue;
                        if (showCode.length >= 1) {
                            showCode = showCode[0] + "***";
                        }
                        var msg = showCode + item.LotteryName + "喜中 <span style='color:#ffcd55;'>" + item.WinMoney + "</span> ";
                        var tmp = "<li><a href=\"javascript:;\" style=\"color:#a9a9a9;\">" + msg + "</a></li>";
                        $("#marquee2").append(tmp);
                    }
                    pmCarousel();
                    
                } else if (jsonData.Code == 1009) {
                    alert("由于您长时间未操作,为确保安全,请重新登录！");
                    window.location = "/login.html";
                }
            }
        });
    }

    var pmcarinterval=null;
    //循环轮播中奖排名 @  2016-09-23
    function pmCarousel() {
       
        try {
            var length = 300;
            $("#win_list_parent>div").css("height", length + "px");
         
            if ($("#marquee2").height() < length) {
                //数据小于5行的时候不用循环轮播
                return;
            }
            var iCount = 0;
            function goPaly() {
                iCount++;
                if (iCount % 6 > 0) {
                    $("#marquee2 ul").css("top", 0 - (iCount % 6) * 4);
                }
                else {
                    var newTr = $("#marquee2 ul li:eq(0)");
                    $("#marquee2").append("<li>" + newTr.html() + "</li>");
                    $("#marquee2").css("top", 0);
                    $("#marquee2  li:eq(0)").remove();
                    iCount = 0;
                }
            }
            if(pmcarinterval!=null)
                clearInterval(pmcarinterval);
            pmcarinterval=setInterval(goPaly, 500);
           
        } catch (e) { console.log(e); }
    }

    Ytg.common.init();
</script>
