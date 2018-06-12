<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameCenter.aspx.cs" Inherits="Ytg.ServerWeb.GameCenter" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, minimum-scale=1, maximum-scale=1,user-scalable=no" />
    <meta name="format-detection" content="telephone=no" />
    <%-- <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, minimum-scale=1, user-scalable=no, minimal-ui" />
    <meta name="mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />--%>
    <link href="/Content/Images/skin/favicon.ico" rel="shortcut icon" type="images/x-icon" />
    <title></title>
    <script src="/Content/Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <!--[if lte IE 6]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <!--[if lte IE 7]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <!--[if IE 8]><script src="/Content/Scripts/json2.js"></script><![endif]-->
    <script src="/Content/Scripts/playname.js"></script>
    <script src="/Content/Scripts/config.js" type="text/javascript"></script>
    <script src="/Content/Scripts/basic.js" type="text/javascript"></script>
    <script src="/Content/Scripts/comm.js" type="text/javascript"></script>
    <link href="/Content/Css/home.css" rel="stylesheet" />
    <link href="/Content/Css/layout.css" rel="stylesheet" />
    <link href="/Content/Css/comn.css" rel="stylesheet" />
    <link href="/Content/Css/feile/main.css" rel="stylesheet" />
    <link href="/Content/Css/hengcai_11to5.css" rel="stylesheet" />
    <script src="/Content/Scripts/jszip.js" type="text/javascript"></script>

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
    <script src="/Content/Scripts/jquery.ezpz_tooltip.min.js"></script>
    <script src="/Content/Scripts/common.js" type="text/javascript"></script>
    <script src="/Content/Scripts/Lottery/Lottery_lang_zh.js" type="text/javascript"></script>
    <script src="/Content/Scripts/Lottery/jquery.lottery.main.js" type="text/javascript"></script>
    <script src="/Content/Scripts/Lottery/jquery.lottery.selectarea.js" type="text/javascript"></script>
    <script src="/Content/Scripts/Lottery/jquery.lottery.trace.js" type="text/javascript"></script>
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
            background: url("/Content/images/skin/tz_balls_bg.png") no-repeat scroll -61px -300px rgba(0, 0, 0, 0);
        }
        .next-par {float:right;padding-right:15px;}
        .next-par .next-a {font-size:12px;text-align:right;color:#4f63c3;}
    </style>
    <script type="text/javascript">
        var openPlayState=true;//开奖声音播放状态
        var lotterytype =<%=LotteryId%>;//彩种
        var lottertCode='<%=LotteryCode%>';
        var lottery_methods=<%=lottery_methods%>;
       
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
                    //$("#ad").show();
                    parent.$.alert("当前销售已截止，请进入下一期购买！");
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
          
            $('#main', parent.document).height($(document).height()+30); //重设 iframe 高度
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
                            var detailSowTitle="查看详情";
                            if(item.BetContent.length<=50)
                                detailSowTitle=item.BetContent;
                            betContent="<a class='betdetail' style='color:#ff7272;' href='javascript:parent.showDetail(\""+item.BetCode+"\","+item.tp+",\""+item.IssueCode+"\");'>"+detailSowTitle+"</a>";
                            var model={0:"元",1:"角",2:"分",3:"厘"};
                            var occTime=getdateTime(item.OccDate);
                            var posName=item.PostionName==null?"":item.PostionName;
                            
                            var contentItem="<tr name='his_reftr'><td>"+occTime+"</td><td>"+posName+changePalyName(item.PlayTypeName + "" + item.PlayTypeRadioName, '')+"</td><td>"+item.IssueCode+"</td><td >"+betContent+"</td><td>"+decimalCt(Ytg.tools.moneyFormat(item.TotalAmt))+"</td>";
                            contentItem+="<td>"+ item.Multiple+"</td>";//倍数
                            contentItem+="<td>"+ item.BetCount+"</td>";//注数
                            contentItem+="<td>"+model[item.Model] +"</td>";//模式
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
                //<i class="icon icon-num2 kjn0" title="">6</i><i class="icon icon-num2 kjn0" title="">5</i><i class="icon icon-num2 kjn0" title="">8</i><i class="icon icon-num2 kjn0" title="">3</i><i class="icon icon-num2 kjn0" title="">4</i></span></li>
                //var latestlot = "<ul><li class=\"qihao1\">" + issue + "<\/li><li class=\"haoma1\">";
                var latestlot = "<li title=\""+aTmp+"\"><span class=\"left open_item_issue\">"+issue+"</span><span class=\"right\">";
                var sCode = "";
                for (var ci = 0; ci < aTmp.length; ci++){
                    //sCode += "<span>" + aTmp[ci] + "<\/span>";
                    sCode+="<i class=\"icon icon-num2 kjn0\" title=\"\">"+ aTmp[ci]+"</i>";
                }
                var sInsertHtml = latestlot + sCode + "</span></li>";
                //倒计时开始 sam
                //animateStart();
                djs.boolean = true;
                if (djs.boolean){
                    djs.dataStr = aTmp.join("-");
                    djs.dataHtml = sInsertHtml;
                    // setTimeout(animateStop, djs.keep * 1000);
                    var auct=0;
                    $(".open_item_issue").each(function(){
                        if($(this).html()==issue)
                        {auct++;
                        }
                    });
                    
                    if(auct==0){
                        $("#newhislottery").prepend(djs.dataHtml);
                        if ($("#newhislottery").children().length > 15){
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
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" type="text/css" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js" type="text/javascript"></script>
    <script src="/Content/Scripts/Dialog/jquery.dragdrop.js" type="text/javascript"></script>

    <table style="width: 100%;">
        <tr>
            <td id="leftcontent" valign="top">
                <div class="lottery_frame" style="margin: auto; ">

                    <div class="lott_ssc_icon" >
                        <div style="text-align: left;">
                            <%--<div class="ssc_icon" style="background: url(/Content/Images/lt/<%=Request.QueryString["ico"] %>) no-repeat center top;"></div>--%>
                            <div class="ssc_type_con" style="font-size: 24px; font-weight: bold;"><%=Request.Params["ln"] %></div>

                            <div class="num_type" style="margin-top: 10px; font-size: 12px; color: #000;display:none;">
                                游戏提示音：<span id="soundCtl" class="soundoff" style="display: inline-block"></span>
                            </div>
                        </div>
                    </div>

                    <div class="hc_current">
                        <div class="hccurdiv_1"><span class="periods">第<span id="current_issue" style="padding-left: 5px; padding-right: 5px;"></span>期</span></div>
                        <div class="clock"><span id="count_down"></span></div>
                        <div class="hccurdiv_3">投注截止时间</div>
                    </div>

                    <div class="hc_open" >
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

                        </div>
                    </div>
                    <%--<div class="hc_history">
                        <div class="hc_history_tit"><span class="date">历史开奖号</span><span class="number"><a href='/Views/SignalAsync.aspx?lotteryid=<%=Request.QueryString["ltid"] %>&lottery=<%=Server.UrlEncode(Request.QueryString["ln"]) %>&issuecount=30' target="_blank" class="lhfx-btn">漏号分析</a></span></div>
                        <div class="cai_co">
                            <%=Top5OpenResult %>
                        </div>
                    </div>--%>
                </div>
                <div id="subContent_bet">
                    <!--投注页面头部可视代码开始-->
                    <div class="bg clearfix">
                        <!--投注页面头部可视代码结束-->
                        <div class="box">
                            <div class="Menubox title clearfix">
                                <ul id="lotteryType" style="float: left;"></ul>
                                <div class="ren" style="padding: 0px; padding-left: 20px;"><%=RenXuanHref %></div>
                            </div>
                            <div class="Contentbox" id="lotteryDetail">
                                <div class="minitype p15" id="lt_samll_label">
                                </div>
                                <div class="minitype minitypecd">
                                    <span>玩法说明：</span><span id="lt_desc" class="help_con"></span>&nbsp;<span id="lt_example" class="help_exp"><i></i>示例</span>

                                    <div id="lt_example_div" style="display: none; z-index: 800;"></div>
                                    &nbsp;&nbsp;<span id="lt_help" class="help_help"><i></i>帮助</span><div id="lt_help_div" style="display: none; z-index: 800;"></div>
                                </div>
                                <div class="minitype" style="background: #fff;" id="poschoose"></div>
                                <!--选号区 -->
                                <div id="lt_selector"></div>
                                <!--选号区结束 -->
                                <form method="post" name="buyform" action="">
                                    <input type="hidden" name="play_source" id="play_source" />
                                    <div class="clearfix grayBet" style="clear: both;">
                                        <span class="floatR">
                                            <input id="lt_sel_insert" type="button" value="添加投注" class="formOk" /></span>
                                        <div class="floatL">
                                            您选择了 <span class="red" id="lt_sel_nums">0</span> 注，共 <span class="red" id="lt_sel_money">0</span> 元　
                                倍数
                                <label class="com_btn sel_times" value="0"><span style="height: 22px; line-height: 22px; padding: 0px 5px;" id="times_sub">-</span></label>
                                            <input id="lt_sel_times" type="text" style="width: 45px; height: 22px;" class="num_input" value="1" border="0" />
                                            <label class="com_btn sel_times" value="0"><span style="height: 22px; line-height: 22px; padding: 0px 5px;" id="times_add">+</span></label>倍
                                <span id="moneyModle">&nbsp;&nbsp;模式&nbsp;
                                    <label value="1" class="com_btn"><span style="padding: 0px 5px;">元</span></label>
                                    <label value="2" class="com_btn_h" style="display: none;"><span style="padding: 0px 5px;">角</span></label>
                                    <label value="3" class="com_btn_h" style="display: none;"><span style="padding: 0px 5px;">分</span></label>
                                    <label value="4" class="com_btn_h" style="display: none;" ><span style="padding: 0px 5px;">厘</span></label>
                                </span>
                                <span id="nfdprize"></span>
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
                                    <td>
                                        <div class="whiteDetail">
                                            <table cellspacing="1" cellpadding="0" id="lt_cf_content">
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="tjBox clearfix">
                            <div>
                                <div class="choose">
                                    <span>已选择 <span id="lt_cf_count" class="red">0</span> 单</span>
                                    总注数 <span class="red" id="lt_cf_nums">0</span> 注，总金额 <span class="red" id="lt_cf_money">0</span> 元<span style="display: none;">， 起始期：<span id="lt_issues"><input type="hidden" name="lt_total_nums" id="lt_total_nums" value="0"><input type="hidden" name="lt_total_money" id="lt_total_money" value="0"></span></span>
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
                            <!--代购选项-->
                            <div id="hmcontent">
                                <div class="next_con clearfix">
                                    <ul id="daigoutype" class="more_select clearfix">
                                        <li style="padding:3px 0px 3px !important;"><strong  style="font-weight:bold;text-align:left;">代购方式：</strong></li>
                                        <li id="onebuy_box" class="dg active"><label for="onebuy"><input type="radio" name="operate2" id="onebuy" checked="checked" value="0"/>&nbsp;个人代购</label></li>
                                        <li id="hemai_box" class="dg"><label for="hemai"><input type="radio" name="operate2" id="hemai" value="1" />&nbsp;多人合买</label></li>
                                    </ul>
                                    <div id="other_con" >
                                        <div id="c" class="more_operate" style="display: none;">
                                            <div class="fqhm_box">
                                                <table class="table_noborder " style="width:100%;">
                                                    <tbody>
                                                        <tr>
                                                            <th width="25%">投注总额：</th>
                                                            <td width="75%"><strong class="c_ba2534 red" id="game_tzallmon">0</strong>&nbsp;元</td>
                                                        </tr>
                                                        <tr>
                                                            <th valign="top">我要认购：</th>
                                                            <td>
                                                                <input type="hidden" name="hidgame_tzallmon" id="hidgame_tzallmon"/>
                                                                <input name="createrBuyPieces" onpaste="return false" autocomplete="off" id="share_cnt" class="input w70" />&nbsp;元&nbsp;&nbsp;
                                                                <span class="c_727171">最少购买<span>10</span>%，即<em id="game_total_fen">0</em>元</span>
                                                            </td>
                                                        </tr>
                                                        <tr >
                                                            <th valign="top">保密设置：</th>
                                                            <td id="secretBtnBox" class="baomi_box">
                                                                <button tag="0" class="btn_org" type="button">公开</button>
                                                                <button tag="1" class="btn_gray" type="button">参与可见</button>
                                                                <button tag="2" class="btn_gray" type="button">完全保密</button>
                                                                <input  type="hidden" name="baomi_hidden" id="baomi_hidden" value="0"/>
                                                            </td>
                                                            
                                                        </tr>
                                                        <tr style="display: none;">
                                                            <th valign="top">盈利佣金：</th>
                                                            <td class="yongjin_box">
                                                                <ul id="commissionBtnBox" class="yongjin_list">
                                                                    <li v="0" class="active">无</li>
                                                                    <li v="1">1%</li>
                                                                    <li v="2">2%</li>
                                                                    <li v="3">3%</li>
                                                                    <li v="4">4%</li>
                                                                    <li v="5">5%</li>
                                                                    <li v="6">6%</li>
                                                                    <li v="7">7%</li>
                                                                    <li v="8">8%</li>
                                                                    <li v="9">9%</li>
                                                                    <li style="margin-right: 0" v="10">10%</li>
                                                                </ul>
                                                                <p class="group_yjtips">【盈利佣金 =（税后奖金 - 方案金额）× 佣金比例】</p>
                                                            </td>
                                                        </tr>
                                                        <tr style="display:none;">
                                                            <th valign="top">保底：</th>
                                                            <td>
                                                                <input name="guarantee" onpaste="return false" autocomplete="off" id="reserve_share" value="0" class="input w70">&nbsp;元&nbsp;
                                                                <input type="checkbox" name="qbao" id="qbao">&nbsp;全额保底&nbsp;&nbsp;
                                                                <span class="c_727171">不能大于总数， 默认0元</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="padding: 0;text-align:center;">
                                                                <p class="t_c c_727171">注：方案进度+保底&gt;=100%，即可出票</p>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--合买  end -->
                            <div style="text-align: center;"><a href="#_" class="tjBt" onclick="return(false);" id="lt_sendok" style="margin:auto;">立即投注</a></div>
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
                                                    <th style="color: #000;">注数</th>
                                                    <th style="color: #000;">模式</th>
                                                    <th style="color: #000;">奖金</th>
                                                    <th style="color: #000;">状态</th>
                                                    <%--<th style="color: #000;">操作</th>--%>
                                                </tr>

                                            </table>
                                        </div>

                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </td>
            <td class="gamecenter-td" valign="top">
                <div>
                    <div style="text-align: center; line-height: 2em; font-size: 16px;">
                        
                        <div class="hc_history_tit"><span class="date">往期开奖</span><span class="number next-par"><a href='/Views/SignalAsync.aspx?lotteryid=<%=Request.QueryString["ltid"] %>&lottery=<%=Server.UrlEncode(Request.QueryString["ln"]) %>&issuecount=30' target="_blank" class="next-a">漏号分析</a></span></div>
                    </div>
                    <div style="min-height: 233px; overflow: hidden; margin-top: 12px;">
                        <div class="hc_history">
                            
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
        if('<%=Request.Params["ltcode"]%>'=='tenct'){
            $("#hmcontent").hide();
        }
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
                        var msg = showCode + item.LotteryName + "喜中<span style='color:#ff7348;'>" + item.WinMoney + "</span> ";
                        var tmp = "<li style='text-align:center;'><a href=\"javascript:;\" style=\"color:#a9a9a9;\">" + msg + "</a></li>";
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
