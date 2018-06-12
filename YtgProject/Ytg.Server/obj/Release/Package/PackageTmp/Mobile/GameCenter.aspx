<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameCenter.aspx.cs" Inherits="Ytg.ServerWeb.Mobile.GameCenter" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <meta name="format-detection" content="telephone=no">
    <meta name="format-detection" content="email=no">
    <link href="/Content/Images/skin/favicon.ico" rel="shortcut icon" type="images/x-icon" />
    <title></title>
    <script src="/Content/Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <script src="statics/js/lazyload-min.js" type="text/javascript"></script>

    <!--[if lte IE 6]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <!--[if lte IE 7]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <!--[if IE 8]><script src="/Content/Scripts/json2.js"></script><![endif]-->

    <%--    <script src="/Content/Scripts/playname.js"></script>
    <script src="/Content/Scripts/config.js" type="text/javascript"></script>
    <script src="/Content/Scripts/basic.js" type="text/javascript"></script>
    <script src="/Content/Scripts/comm.js" type="text/javascript"></script>
    <link href="/Content/Css/home.css" rel="stylesheet" />
    <link href="/Mobile/Css/layout.css" rel="stylesheet" />
    <link href="/Content/Css/comn.css" rel="stylesheet" />
    <link href="/Content/Css/feile/main.css" rel="stylesheet" />
    <link href="/Content/Css/hengcai_11to5.css" rel="stylesheet" />
    <script src="/Content/Scripts/jszip.js" type="text/javascript"></script>--%>

    <script type="text/javascript">

        function loadComplete(){
            Ytg = $.extend(Ytg, {
                SITENAME: "美亚娱乐", 
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
        }

        function loadscript()

        {

            LazyLoad.loadOnce([
             '/Content/Scripts/playname.js',
             '/Content/Scripts/config.js',  
             '/Content/Scripts/comm.js',
             '/Content/Scripts/jszip.js',
             '/Content/Scripts/jquery.ezpz_tooltip.min.js',
             '/Content/Scripts/common.js',
             '/Content/Scripts/Lottery/Lottery_lang_zh.js',
             '/Mobile/Css/jquery.lottery.main.js',
             '/Mobile/Css//jquery.lottery.selectarea.js',
             '/Content/Scripts/Dialog/jquery.dialogUI.js',
             '/Content/Scripts/Dialog/jquery.dragdrop.js',
             '/Content/Scripts/Lottery/jquery.lottery.trace.js',
             '/Mobile/css/bootstrap.css',
             '/Content/Css/home.css',
             '/Mobile/Css/layout.css',
             '/Content/Css/comn.css',
             '/Content/Css/feile/main.css',
             '/Content/Css/hengcai_11to5.css',
             '/Mobile/css/dialogUI.css',
            ], loadComplete);
        }

        setTimeout(loadscript,10);
        
        function inintUser_ban() {
            $.ajax({
                type: 'POST',
                url: '/page/Initial.aspx?action=userbalance',
                data: { "uid": Ytg.common.user.info.user_id},
                timeout: 10000, success: function (data) {
                    var jsonData = JSON.parse(data);                    
                    if (jsonData.Code == 0) {
                        //获取成功
                        $("#user_span_monery_title").html(Ytg.tools.moneyFormat(jsonData.Data.UserAmt));
                    } else if (jsonData.Code == 1009) {
                        //$.alert("由于您长时间未操作,为确保安全,请重新登录！", 1, function () {
                        //    window.location = "/login.html";
                        //});
                    } else {
                        return false;
                    }
                },
                error: function () {

                }
            });
        }

        
    </script>
    <%-- <link href="/Mobile/css/bootstrap.css" rel="stylesheet" />
    <script src="/Content/Scripts/jquery.ezpz_tooltip.min.js"></script>
    <script src="/Content/Scripts/common.js" type="text/javascript"></script>
    <script src="/Content/Scripts/Lottery/Lottery_lang_zh.js" type="text/javascript"></script>
    <script src="/Mobile/Css/jquery.lottery.main.js" type="text/javascript"></script>
    <script src="/Mobile/Css//jquery.lottery.selectarea.js" type="text/javascript"></script>
    <script src="/Content/Scripts/Lottery/jquery.lottery.trace.js" type="text/javascript"></script>--%>
    <style>
        #lottery-number-logs-contents {
            font-size: 12px;
        }

            #lottery-number-logs-contents a {
                color: #ff6600;
            }

        #lottery-number-logs-title span {
            background: url("http://cfapu.img48.wal8.com/img48/545266_20160510012323/146281533461.png") no-repeat left center;
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
            background: url("/Content/images/skin/tz_balls_bg.png") no-repeat scroll -61px -300px rgba(0, 0, 0, 0);
        }
    </style>
    <script type="text/javascript">
        var openPlayState=true;//开奖声音播放状态
        var lotterytype =<%=LotteryId%>;//彩种
        var lottertCode='<%=LotteryCode%>';
        var lottery_methods=<%=lottery_methods%>;
        var open_state=0;
        var open_trade_state=0;
        var open_setting_state=0;//
       
        $(document).ready(function () {
            if (lotterytype == 6
                ||lotterytype == 17
                ||lotterytype == 18
                ||lotterytype == 19
                ||lotterytype == 20){
                $("#t_1_q").html("单双：");
                $("#t_1_p").html("中位：");
            }
            inintmonerymodle();
            inintPlayStat();//获取声音播放状态
            $.gameInit({
                data_label: [<%=JsonData%>],//
                data_methods:lottery_methods,
                cur_issue: <%=NowIssue%>,
                issues: {//所有的可追号期数集合
                    today: [<%=NextIssues%>],
                    tomorrow: [<%=NextDayIssues%>]
                },
                servertime:'<%=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")%>',
                lotteryid: lotterytype,
                lotteryCode:lottertCode,
                ontimeout: function () {
                    // $("#ad").show();
                    parent.$.alert("当前销售已截止，请进入下一期购买！");
                },
                onend:function(){ //结束当期，准备开奖
                    refold();
                    // alert("dd");
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
                            if(refCount<60){
                                reftinterval=setInterval(refold,1500);//继续请求开奖;
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
                        //同步更新期号
                        // $('#current_issue').html($.lt_preissue.issue);//
                        processCode($.lt_preissue.issue,jsonData.Data.Result,2);
                    }
                } );
            }
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
            //隐藏、显示玩法
            $('#J-method-select').click(function(){
                if(open_trade_state==1){
                    $('#postion_content_item').slideUp(100);
                    open_trade_state=0;
                }else{
                    $('#postion_content_item').slideDown(100);
                    open_trade_state=1;
                }
            });

            
            $("#xd_clost,#lst_btn_no").click(function(){
                if(open_state==1){
                    $('#lottery_added_lst').slideUp(100);
                    open_state=0;
                }
            });
           
            $('#select_nums').click(function(){
                if(open_state==1){
                    $('#lottery_added_lst').slideUp(100);
                    open_state=0;
                }else{
                    $('#lottery_added_lst').slideDown(100);
                    open_state=1;
                    show_his_lst();
                    close_result_record();
                }
            });
            
            $("#J-result-record").click(function(){
                if(open_setting_state==0){
                    $(".right_setting").animate({right:"0px"});
                    open_setting_state=1;
                    inintUser_ban();
                }else
                {
                    $(".right_setting").animate({right:"-200px"});
                    open_setting_state=0;
                }
            });
            //close/show
            $("#close_his_btn,#his_btt_lottery").click(function(){
                if($('#sty1-number').css("display")=="none"){
                    close_his_lst();
                }
                else{
                    getCurProjects();
                    show_his_lst();
                }
                close_added_Details();
                close_result_record();
            });
        });
        
        //隐藏历史记录
        function close_his_lst(){
            ; $('#sty1-number').slideDown(100);
        }
        //显示历史记录
        function show_his_lst(){
            $('#sty1-number').slideUp(100)
        }

        function close_result_record(){
            if(open_setting_state=1){
                $(".right_setting").animate({right:"-200px"});
                open_setting_state=0;
            }
        }
        //打开选号栏
        function show_added_Details(){
            if(open_state==0){
                $('#lottery_added_lst').slideDown(100);
                open_state=1;
            }
        }
        //关闭选号栏
        function close_added_Details(){
            if(open_state==1){
                $('#lottery_added_lst').slideUp(100);
                open_state=0;
            }
        }
        function close_trad_detaClose(setName){
            if(open_trade_state==1){
                $('#postion_content_item').slideUp(100);
                open_trade_state=0;
            }
            if(setName!=undefined && setName!="")
                $("#J-method-name").html(setName);
        }
        function set_check_title(setName){
            if(setName!=undefined && setName!="")
                $("#J-method-name").html(setName);
        }

        

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
            close_added_Details();
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
                            var detailSowTitle="查看详情";
                            if(item.BetContent.length<=50)
                                detailSowTitle=item.BetContent;
                            betContent="<a class='betdetail' style='color:#ff7272;' href='javascript:parent.showDetail(\""+item.BetCode+"\","+item.tp+",\""+item.IssueCode+"\");'>"+detailSowTitle+"</a>";
                            var model={0:"元",1:"角",2:"分",3:"厘"};
                            var occTime=getdateTime(item.OccDate);
                            var posName=item.PostionName==null?"":item.PostionName;
                            
                            var contentItem="<tr onclick='showDetail(\""+item.BetCode+"\","+item.tp+",\""+item.IssueCode+"\");' name='his_reftr'><td style='display:none;'>"+occTime+"</td><td>"+posName+changePalyName(item.PlayTypeName + "" + item.PlayTypeRadioName, '')+"</td><td>"+item.IssueCode+"</td><td style='display:none;'>"+betContent+"</td><td>"+decimalCt(Ytg.tools.moneyFormat(item.TotalAmt))+"</td>";
                            //contentItem+="<td>"+ item.Multiple+"</td>";//倍数
                            //contentItem+="<td>"+ item.BetCount+"</td>";//注数
                            //contentItem+="<td>"+model[item.Model] +"</td>";//模式
                            contentItem+="<td>"+decimalCt(Ytg.tools.moneyFormat(item.WinMoney))+"</td>";//奖金
                            contentItem+="<td>"+stauts+"</td>"
                            contentItem+="<tr>"
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
        function showDetail(code, issueCode, betType) {
          
            var ul = "/Mobile/userCenter/BettingDetail.aspx?betcode=" + code;
            if (betType == 1 && issueCode != -1) {
                ul = "/Mobile/userCenter/BettingDetail.aspx?catchCode=" + code + "&issueCode=" + issueCode;
            }
            if (betType == 2) {
                openHeight = winHeight - 100;
                ul = "/Mobile/userCenter/CatchDetail.aspx?catchCode=" + code;
            }
            window.location = ul;
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
                //animateStart();
                djs.boolean = true;
                if (djs.boolean){
                    djs.dataStr = aTmp.join("-");
                    djs.dataHtml = sInsertHtml;
                    // setTimeout(animateStop, djs.keep * 1000);
                    var auct=0;
                    $(".qihao1").each(function(){
                        if($(this).html()==issue)
                        {auct++;
                        }
                    });
                    if(auct==0){
                        $(".cai_co").prepend(djs.dataHtml);
                        if ($(".cai_co ul").length > 5){
                            $(".cai_co ul:last").remove();
                        }
                    }
                }
            }
            var num_single = 0, num_double = 0;
            var num_codesort = aTmp; //排序

            if (times){
                $('.num_open ul li span span').html("");
            }

            var lstSp="";
            for (var ci = 0; ci < aTmp.length; ci++){
                var curNum = aTmp[ci];
                lstSp+="<span>"+curNum+"</span>";
                if (times){
                    (function(i, num){
                        window.setTimeout(function(){
                            $('#last_code_num' + i).html(num);
                            $('.num_open ul li').eq(i).css({"left" : 67 * i + "px"}).fadeIn();
                        }, 500 * (aTmp.length - i), i, num);
                    })(ci, curNum);
                }

                var code_word = '';
                if (curNum >= 5){
                    code_word += '大';
                } else{
                    code_word += '小';
                }
                if (curNum % 2 == 0){
                    code_word += '双'; num_double++;
                } else{
                    code_word += '单'; num_single++;
                }
            }
    
            times = false;
            if (lotterytype == 1 || 
                lotterytype == 2 ||
                lotterytype == 3 || 
                lotterytype == 4||
                lotterytype == 5||
                lotterytype == 11||
                lotterytype == 12||
                lotterytype == 15||
                lotterytype == 13||
                lotterytype == 14||
                lotterytype == 23||
                lotterytype == 24||
                lotterytype == 25){//时时彩
                var last_code_desc1 = "组三";
                if (aTmp[0] == aTmp[1] && aTmp[0] == aTmp[2]){
                    last_code_desc1 = "豹子";
                } else if (aTmp[0] != aTmp[1] && aTmp[0] != aTmp[2] && aTmp[1] != aTmp[2]){
                    last_code_desc1 = "组六";
                }
                $("#last_code_desc1").html(last_code_desc1);
                var last_code_desc2 = "组三";
                if (aTmp[2] == aTmp[3] && aTmp[2] == aTmp[4]){
                    last_code_desc2 = "豹子";
                } else if (aTmp[2] != aTmp[3] && aTmp[2] != aTmp[4] && aTmp[3] != aTmp[4]){
                    last_code_desc2 = "组六";
                }
                $("#last_code_desc2").html(last_code_desc2);
            } else if (lotterytype == 6
                ||lotterytype == 17
                ||lotterytype == 18
                ||lotterytype == 19
                ||lotterytype == 20){
                num_codesort = num_codesort.sort();
                var last_code_desc1 = num_single + "单" + num_double + "双";
                $("#last_code_desc1").html(last_code_desc1);
                var last_code_desc2 = num_codesort[2];
                $("#last_code_desc2").html(last_code_desc2);
            } else if (lotterytype == 3){
                var total = ups = downs = odds = evens = 0;
                for (var ci = 0; ci < aTmp.length; ci++){
                    total += intval(aTmp[ci]);
                    if (intval(aTmp[ci]) < 41)	{
                        ups += 1;
                    } else{
                        downs += 1;
                    }
                    if (intval(aTmp[ci]) % 2 == 0){
                        evens += 1;
                    } else{
                        odds += 1;
                    }
                }
                var cheziStr = '和值：' + total + '(';
                if (total > 810){
                    cheziStr += '大';
                } else if (total < 810){
                    cheziStr += '小';
                } else{
                    cheziStr += '和';
                }
                cheziStr += ',' + (total % 2 == 0 ? '双' : '单') + ')';
                var cpanStr = '盘面：(';
                if (ups > downs){
                    cpanStr += '上';
                } else if (ups == downs){
                    cpanStr += '中';
                } else{
                    cpanStr += '下';
                }
                cpanStr += ',';
                if (odds > evens){
                    cpanStr += '奇';
                } else if (odds == evens){
                    cpanStr += '和';
                } else{
                    cpanStr += '偶';
                }
                cpanStr += ')';
                $('#last_code_desc1').html(cheziStr);
                $('#last_code_desc2').html(cpanStr);
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
                //  play_click('/Content/audio/playsound.mp3');/*开奖倒数*/
            }catch(ex){
            }
        }
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
</head>
<body>
    <div id="sound" style="position: absolute;"></div>
    <%-- <link href="/Mobile/css/dialogUI.css" rel="stylesheet" type="text/css" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js" type="text/javascript"></script>
    <script src="/Content/Scripts/Dialog/jquery.dragdrop.js" type="text/javascript"></script>--%>

    <div class="lottery_frame" style="display: none;">
        <div class="lott_ssc_icon">
            <div>
                <div class="ssc_icon" style="background: url(/Content/Images/lt/<%=Request.QueryString["ico"] %>) no-repeat center top;"></div>
                <div class="ssc_type_con"><%=Request.Params["ln"] %></div>
            </div>
        </div>
        <div class="hc_current">
            <%--  <div class="hccurdiv_1"><span class="periods">第<span id="current_issue" style="padding-left: 5px; padding-right: 5px;"></span>期</span></div>
            <div class="clock"><span id="count_down"></span></div>
            <div class="hccurdiv_3">投注截止时间</div>--%>
        </div>
        <div class="hc_open">
            <div class="lo_title">
                <h2 class="dlh2">
                    <dl>
                        <dd class="title"><%=Request.Params["ln"] %></dd>
                        <dd>
                            <span class="periods periods_2">第&nbsp;<span class="no_old"><strong style="color: #947026;" id="openIssue"></strong></span>&nbsp;期开奖结果</span>
                        </dd>
                    </dl>
                </h2>
            </div>
            <div class="num_open">
                <ul class="ssc_open">
                    <li><span class="each"><span id="last_code_num0">★</span></span></li>
                    <li><span class="each"><span id="last_code_num1">★</span></span></li>
                    <li><span class="each"><span id="last_code_num2">★</span></span></li>
                    <li><span class="each"><span id="last_code_num3">★</span></span></li>
                    <li><span class="each"><span id="last_code_num4">★</span></span></li>
                </ul>
                <div class="ad" id="ad"></div>
            </div>
            <div class="num_type">
                <span id="t_1_q" style="margin: 0px;">前三：</span><span id="last_code_desc1">----</span>&nbsp;&nbsp;<span id="t_1_p" style="margin: 0px;">后三：</span><span id="last_code_desc2">----</span>
                <span id="soundCtl" class="soundoff" style="display: inline-block"></span>
            </div>
        </div>

    </div>
    <div id="subContent_bet">
        <!--投注页面头部可视代码开始-->
        <div class="bg clearfix">
            <!--投注页面头部可视代码结束-->
            <div class="box">
                <div class="Menubox title clearfix">
                    <nav class="col-xs-12 lottery_title" style="text-align: left; position: fixed; z-index: 999999;">
                        <a id="J-goback" href="/Mobile/LotteryMain.aspx" class="go-back">返回</a>
                        <span class="desc"><%=Request.Params["ln"] %>
                            <span class="periods">第<span id="current_issue" style="padding-left: 5px; padding-right: 5px; color: #fff000;"></span>期</span>
                            截止时间&nbsp;&nbsp;<span id="count_down" style="color: #fff000;"></span>

                        </span>
                    </nav>
                    <div style="height: 40px;"></div>
                    <div class="select-panel clearfix">
                        <div id="J-method-name" class="method-name">后三直选复式</div>
                        <div id="J-method-select" class="method-list-button">展开玩法</div>
                    </div>
                    <div id="postion_content_item" style="height: 240px; position: absolute; overflow-y: auto; background: #fff; border-bottom: 1px solid rgb(251, 213, 220); z-index: 9999;">
                        <ul id="lotteryType" style="float: left; border-right: 1px solid #FBD5DC; background: #fff;"></ul>
                        <%--<div class="ren" style="padding: 0px; padding-left: 20px; display: none;opacity:0;"><%=RenXuanHref %></div>--%>
                        <div class="minitype p15" id="lt_samll_label" style="min-height: 540px;"></div>
                    </div>
                </div>
                <div class="Contentbox" id="lotteryDetail">
                    <%-- <div class="minitype minitypecd" style="display:none;">
                        <span>玩法说明：</span><span id="lt_desc" class="help_con"></span>&nbsp;<span id="lt_example" class="help_exp"><i></i>示例</span>
                        <div id="lt_example_div" style="display: none; z-index: 800;"></div>
                        &nbsp;&nbsp;<span id="lt_help" class="help_help"><i></i>帮助</span><div id="lt_help_div" style="display: none; z-index: 800;"></div>
                    </div>--%>
                    <div class="minitype" style="background: #fff;" id="poschoose"></div>
                    <!--选号区 -->
                    <div id="lt_selector"></div>
                    <!--选号区结束 -->
                    <form method="post" name="buyform" action="">
                        <input type="hidden" name="play_source" id="play_source" />
                        <div class="clearfix grayBet" style="clear: both;">
                            <span class="floatR" style="float: none;">
                                <%-- <input id="lt_sel_insert" type="button" value="添加投注" class="formOk" /></span>--%>
                                <div class="" style="text-align: center;">
                                    您选择了 <span class="red" id="lt_sel_nums">0</span> 注，共 <span class="red" id="lt_sel_money">0</span> 元　
                                <!--参数面板-->
                                    <div class="right_setting">
                                        <div id="J-result-record" class="record-button">收起面板</div>
                                        <div style="height: 10px;"></div>
                                        <div>
                                            <div>
                                                <span class="right_title">余额：</span>
                                                <span id="user_span_monery_title" class="right_title" style="color: red; margin-left: 0px;">0.0000</span>
                                            </div>
                                            <div style="height: 10px;"></div>
                                            <span class="right_title">倍数：</span><label class="com_btn sel_times" value="0" style="display: none;"><span style="height: 22px; line-height: 22px; padding: 0px 5px;" id="times_sub">-</span></label>
                                            <input id="lt_sel_times" type="text" style="width: 70px; height: 23px;" class="num_input" value="1" border="0" />
                                            <label class="com_btn sel_times" value="0" style="display: none;"><span style="height: 22px; line-height: 22px; padding: 0px 5px;" id="times_add">+</span></label>
                                        </div>
                                        <div style="height: 8px;"></div>
                                        <span id="moneyModle"><span class="right_title">模式：</span>
                                            <label value="1" class="com_btn"><span style="padding: 0px 5px;">元</span></label>
                                            <label value="2" class="com_btn_h"><span style="padding: 0px 5px;">角</span></label>
                                            <label value="3" class="com_btn_h"><span style="padding: 0px 5px;">分</span></label>
                                            <label value="4" class="com_btn_h"><span style="padding: 0px 5px;">厘</span></label>
                                        </span>
                                        <div style="height: 10px;"></div>
                                        <span class="right_title">返点：</span>
                                        <div class="" style="position: relative; display: inline-block; *display: inline;"><span id="nfdprize"></span></div>
                                        <div style="height: 10px;"></div>
                                        <div>
                                            <span class="right_title">最近开奖结果：</span>
                                            <div class="hc_history">
                                                <div class="cai_co">
                                                    <%=Top5OpenResult %>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                        </div>

                        <div class="lottery_list" id="lottery_added_lst" style="display: none;">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <span class="tspan">&nbsp;&nbsp;已选择 <span id="lt_cf_count" class="red">0</span> 单</span>
                                    </td>
                                    <td>
                                        <a class="ui_close" id="xd_clost" href="javascript:void(0);" title="关闭(esc键)" style="display: inline-block; font-size: 25px; font-weight: bold; color: #cd0228; float: right; margin-right: 10px;">×</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="whiteDetail">
                                            <table cellspacing="1" cellpadding="0" id="lt_cf_content">
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tspan">
                                        <div>&nbsp;&nbsp;总注数 <span class="red" id="lt_cf_nums">0</span> 注</div>
                                        <div>&nbsp;&nbsp;总金额 <span class="red" id="lt_cf_money">0</span> 元</div>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="height: 10px;"></td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center;">
                                        <input type="button" value="再选一注" id="lst_btn_no" class="mobilecancel" />
                                        <input type="button" value="投注" id="lt_sendok" class="mobilesubmit" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px;"></td>
                                </tr>
                            </table>
                        </div>
                        <div class="tjBox clearfix" style="display: none;">
                            <%-- <div class="floatR"><a href="#_" class="tjBt" onclick="return(false);" id="lt_sendok">立即投注</a></div>--%>
                            <div class="floatL">
                                <div class="choose">
                                    <span style="display: none;">， 起始期：<span id="lt_issues">
                                        <input type="hidden" name="lt_total_nums" id="lt_total_nums" value="0" />
                                        <input type="hidden" name="lt_total_money" id="lt_total_money" value="0" />
                                    </span></span>
                                </div>
                                <div class="zuihao_box" style="*display: inline;">
                                    <label>
                                        <span style="background: none; border: none; display: inline;">
                                            <input type="checkbox" name="lt_trace_if" id="lt_trace_if_button" value="no" />发起追号</span></label>&nbsp;&nbsp;
                                    <label>
                                        <span style="background: none; border: none; display: inline;">
                                            <input name="lt_trace_stop" type="checkbox" value="yes" id="lt_trace_stop" disabled="disabled" />中奖后停止追号</span></label>
                                </div>
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
                                        <span style="padding-left: 20px; padding-right: 20px;">总期数： <span id="lt_trace_count" class="redSpan">0</span> 期</span>追号总金额：<span id="lt_trace_hmoney" class="redSpan">0.00</span> 元
                                    </p>
                                    <div style="height: 10px;"></div>
                                    <p>
                                        <span id="lt_trace_labelhtml"></span><span class="red" id="lt_trace_alcount" style="display: none;">全部追号期数</span>
                                        <input name="" type="button" value="生成" class="formSC" id="lt_trace_ok" />
                                    </p>
                                </div>
                            </div>
                            <div class="zhlist" id="lt_trace_issues"></div>
                        </div>
                        <div id="sty1-number" class="lottery-number-logs" style="padding: 1px 4px; position: fixed; bottom: 60px; left: 0px; background: #e2e2e2; width: 100%; display: none;">
                            <%--  <div id="lottery-number-logs-title" style="padding: 0 4px">
                                <span>投注记录</span>
                                <a href="javascript:getCurProjects(1);">刷新</a>
                                <div class="clear"></div>
                            </div>--%>
                            <div id="projectloading" style="text-align: center; display: none; height: 100px; vertical-align: middle;">
                                <img style="vertical-align: middle; margin-top: 40px" src="/Content/images/skin/loading.gif" />
                            </div>
                            <div>
                                <a class="ui_close" id="close_his_btn" href="javascript:void(0);" title="关闭(esc键)" style="display: inline-block; font-size: 25px; font-weight: bold; color: #cd0228; float: right; margin-right: 10px;">×</a>
                            </div>
                            <div id="lottery-number-logs-contents">
                                <table width="100%" cellspacing="0" cellpadding="0" border="0" class="grayTable" style="margin-top: 10px;">
                                    <tr>
                                        <%--<th style="color: #000;">投注时间</th>--%>
                                        <th>玩法</th>
                                        <th>期号</th>
                                        <%--   <th style="color: #000;">投注内容</th>--%>
                                        <th>投注金额</th>
                                        <%--             <th style="color: #000;">倍数</th>
                                        <th style="color: #000;">注数</th>--%>
                                        <%--<th style="color: #000;">模式</th>--%>
                                        <th>奖金</th>
                                        <th>状态</th>
                                        <%--<th style="color: #000;">操作</th>--%>
                                    </tr>

                                </table>
                            </div>

                        </div>

                        <!--底部导航按钮-->
                        <nav class="navbar navbar-default navbar-fixed-bottom">
                            <div class="container">
                                <ul class="nav navbar-nav chose-ball col-xs-12">
                                    <li class="col-xs-4"><a href="javascript:;" id="his_btt_lottery">历史投注</a></li>
                                    <li class="col-xs-4"><a href="javascript:;" id="select_nums">选号篮</a></li>
                                    <li class="col-xs-4"><a href="#_" onclick="return(false);" id="lt_sel_insert">立即投注</a></li>
                                </ul>
                            </div>
                        </nav>
                    </form>
                </div>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>

</body>
</html>
<script type="text/javascript">
    $(function () {
        //标识客户端投注来源
        if (getCookie('isclient') == 1) {
            $("#play_source").val(4);
        }
    })
    Ytg.common.init();
</script>
