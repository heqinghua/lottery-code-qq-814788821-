;(function($){//start
	//追号区
    $.fn.lt_trace = function(){
        var t_type  = 'margin';//追号形式[利润率:margin,同倍:same,翻倍:diff]
		if(lotterytype==3){
			t_type = 'same';//北京快乐吧默认同倍追号
		}
        $.extend({
            lt_trace_issue: 0,//总追号期数
            lt_trace_money: 0//总追号金额
            });
        var t_count = $.lt_issues.tomorrow.length;//可追号期数
        
        var t_nowpos = 0;//当前起始期在追号列表的位置[超过该位置的就不在处理,优化等待时间]
       
        //装载可追号期数
        $($.lt_id_data.id_tra_alct).html(t_count);
        //装载同倍、翻倍标签
        
        var htmllabel = '<div id="button111" class="current">'+lot_lang.dec_s13+'</div><div id="button12">'+lot_lang.dec_s10+'</div><div id="button13">'+lot_lang.dec_s11+'</div>';
        if(lotterytype==0||lotterytype==2){
        	var htmllabel='<ul class="threeTab clearfix"><li class="one"><a class="current" id="button111">利润率追号</a></li><li class="two"><a id="button12">同倍追号</a></li><li class="three"><a id="button13">翻倍追号</a></li></ul>';
        }else if(lotterytype==3){
        	var htmllabel='<ul class="twoTab clearfix"><li class="one"><a class="current" id="button12">同倍追号</a></li><li class="two"><a id="button13">翻倍追号</a></li></ul>';
        }
        //$(htmllabel).appendTo($.lt_id_data.id_tra_label);
        var htmltext  = '追号计划：&nbsp;<span id="lt_margin_html">'+lot_lang.dec_s14+'&nbsp;<input size="3" class="input02" name="lt_trace_times_margin" type="text" id="lt_trace_times_margin" value="1" />&nbsp;'+lot_lang.dec_s29+'&nbsp;<input size="3" class="input02" name="lt_trace_margin" type="text" id="lt_trace_margin" value="50" />&nbsp;%&nbsp;</span>';
        htmltext += '<span id="lt_sametime_html" style="display:none;">'+lot_lang.dec_s14+'&nbsp;<input size="3" name="lt_trace_times_same" type="text" class="input022" id="lt_trace_times_same" value="1" />&nbsp;</span>';
        htmltext += '<span id="lt_difftime_html" style="display:none;">'+lot_lang.dec_s17+'&nbsp;<input size="3" name="lt_trace_diff" type="text"  value="1" class="input02" id="lt_trace_diff" />&nbsp;'+lot_lang.dec_s18+'　'+lot_lang.dec_s2+''+lot_lang.dec_s19+' <input size="3" name="lt_trace_times_diff" type="text" id="lt_trace_times_diff" value="2" class="input02" />&nbsp;</span>';
        htmltext += ''+lot_lang.dec_s15+':&nbsp;<input size="3" name="lt_trace_count_input" type="text" id="lt_trace_count_input" style="width:24px" value="10" /><input size="3" type="hidden" id="lt_trace_money" name="lt_trace_money" value="0" />';
        $(htmllabel).appendTo($.lt_id_data.id_tra_label);
        $(htmltext).appendTo($.lt_id_data.id_tra_lhtml);
		//alert(htmltext);
        $('#button111').click(function(){//利润率
			//alert('ss');
            if( $(this).attr("class") != "temp" ){
                //$(this).attr("id","button111");
                //$('#button121').attr("id","button12");
                //$('#button131').attr("id","button13");
                $('#lt_margin_html').show();
                $('#lt_sametime_html').hide();
                $('#lt_difftime_html').hide();
                t_type = 'margin';
				$(this).attr("class","current");
				$('#button12').attr("class","");
				$('#button13').attr("class","");
            }
        });
        $('#button12').click(function(){//同倍
            if( $(this).attr("class") != "temp" ){
                $('#lt_margin_html').hide();
                $('#lt_sametime_html').show();
                $('#lt_difftime_html').hide();
                t_type = 'same';
				$(this).attr("class","current");
				$('#button111').attr("class","");
				$('#button13').attr("class","");
            }
        });
		if(t_type == 'same'){
                $('#lt_margin_html').hide();
                $('#lt_sametime_html').show();
                $('#lt_difftime_html').hide();
		}
        $('#button13').click(function(){//翻倍
			//alert('ss');
            if( $(this).attr("class") != "temp" ){
                $('#lt_margin_html').hide();
                $('#lt_sametime_html').hide();
                $('#lt_difftime_html').show();
                t_type = 'diff';
				$(this).attr("class","current");
				$('#button111').attr("class","");
				$('#button12').attr("class","");
            }
        });
        function upTraceCount(){//更新追号总期数和总金额
            $('#lt_trace_count').html($.lt_trace_issue);
            $('#lt_trace_hmoney').html(JsRound($.lt_trace_money,2,true));
            $('#lt_trace_money').val($.lt_trace_money);
        }
        
        //标签内的输入框键盘事件
        $("input",$($.lt_id_data.id_tra_lhtml)).keyup(function(){
            $(this).val( Number($(this).val().replace(/[^0-9]/g,"0")) );
        });
        //追号期快捷选择事件
        $("#lt_trace_qissueno").change(function(){
            var t=0;
            if($(this).val() == 'all' ){//全部可追号奖期
                t = parseInt($($.lt_id_data.id_tra_alct).html(),10);
            }else{
                t = parseInt($(this).val(),10);
            }
            t = isNaN(t) ? 0 : t;
            $("#lt_trace_count_input").val(t);
        });
        
        
        //装载追号的期号列表
        var issueshtml = '<table border="0" cellspacing="0" cellpadding="0" id="lt_trace_issues_today" class="formTable" width="100%">';
        var ii = 0;
        var xx = 0;
        var maxXX = 20;
        var arrayObj = new Array();

        $.each($.lt_issues.today, function (i, n) {
            ii++;
            if (xx <= maxXX) {
                xx++;
                arrayObj[ii] = n.issue;
                issueshtml += '<tr id="tr_trace_' + n.issue + '" ><td align="center"><input type="checkbox" name="lt_trace_issues[]" value="' + n.issue + '" /></td><td align="center"  style="display:none;">' + ii + '</td><td align="center">' + n.issue + '</td><td align="center"><input style="width:100px" name="lt_trace_times_' + n.issue + '" type="text" class="input03" value="0" disabled/>' + lot_lang.dec_s2 + '</td><td align="center">' + lot_lang.dec_s20 + '<span id="lt_trace_money_' + n.issue + '">0.00</span></td><td align="center">' + n.endtime + '</td></tr>';
            }
        });
        var t = $.lt_issues.tomorrow.length - $.lt_issues.today.length;
        if (lotterytype == 1)
            t = t + 24;
		if($.lt_issues.today.length==0){
			$("#lottery_today_current").html('-');
			$("#lottery_today_surplus").html('-');
		}else{
			$("#lottery_today_current").html(t+1);
			$("#lottery_today_surplus").html($.lt_issues.today.length-1);
		}
		//alert($.lt_issues.today.length);
        $.each($.lt_issues.tomorrow,function(i,n){
            if (i < t && xx <= maxXX) {
                ii++;
                var isf = false;
                for (var x = 0; x < arrayObj.length; x++) {
                    if (arrayObj[x] == n.issue) {
                        isf = true;
                        continue;
                    }
                }
                if (isf == false)
                    issueshtml += '<tr id="tr_trace_' + n.issue + '"><td align="center"><input type="checkbox" name="lt_trace_issues[]" value="' + n.issue + '" /></td><td align="center" style="display:none;">' + ii + '</td><td align="center">' + n.issue + '</td><td align="center" ><input style="width:100px" name="lt_trace_times_' + n.issue + '" type="text" class="input03" value="0" disabled/>' + lot_lang.dec_s2 + '</td><td align="center">' + lot_lang.dec_s20 + '<span id="lt_trace_money_' + n.issue + '">0.00</span></td><td align="center">' + n.endtime + '</td></tr>';
            }
        });
        issueshtml += '</table>';
        
        $(issueshtml).appendTo($.lt_id_data.id_tra_issues);
        function changeIssueCheck(obj){//选中或者取消某期
            var money = $.lt_trace_base;
            var $j = $(obj).closest("tr");
			//alert(obj);
			//alert($(obj).attr("checked"));
            if( $(obj).attr("checked") == 'checked' ){//选中
                $j.find("input[name^='lt_trace_times_']").val(1).attr("disabled",false).data("times",1);
                $j.find("span[id^='lt_trace_money_']").html(JsRound(money,2,true));
                $.lt_trace_issue++;
                $.lt_trace_money += money;
            }else{  //取消选中
                var t =$j.find("input[name^='lt_trace_times_']").val();
                $j.find("input[name^='lt_trace_times_']").val(0).attr("disabled",true).data("times",0);
                $j.find("span[id^='lt_trace_money_']").html('0.00');
                $.lt_trace_issue--;
                $.lt_trace_money -= money*parseInt(t,10);
            }
            $.lt_trace_money = JsRound($.lt_trace_money,2);
            upTraceCount();
        };
        $("input[name^='lt_trace_times_']",$($.lt_id_data.id_tra_issues)).keyup(function(){//每期的倍数变动
            var v = Number($(this).val().replace(/[^0-9]/g,"0"));
            $.lt_trace_money += $.lt_trace_base*(v-$(this).data('times'));
            upTraceCount();
            $(this).val(v).data("times",v);
            $(this).closest("tr").find("span[id^='lt_trace_money_']").html(JsRound($.lt_trace_base*v,2,true));
        });
        //$(":checkbox",$.lt_id_data.id_tra_issues).click(function(){//取消与选择某期
		$("input[type='checkbox'][name^='lt_trace_issues']").click(function(){//取消与选择某期
            changeIssueCheck(this);
        });
        $("tr",$($.lt_id_data.id_tra_issues)).live("mouseover",function(){
            $(this).children().addClass("temp");
        }).live("mouseout",function(){
            if( $(this).find(":checkbox").attr("checked") == false ){
                $(this).children().removeClass("temp");
            }
        });
        var _initTraceByIssue = function(){//根据起始期的不同重新加载追号区
            var st_issue = $("#lt_issue_start").val();
            cleanTraceIssue();//清空追号方案
            var isshow   = false;//是否已经开始显示
            var acount   = 0;//不可追号期统计
            var loop     = 0;//循环次数
            var mins     = t_nowpos;//开始处理的位置[初始为上次变更的位置]
            var maxe = t_nowpos;//结束处理的位置[初始为上次变更的位置]
            var appendNow = 0;
            var maxAppendnow = 20;//大于200不在追加
          
            $.each($.lt_issues.today,function(i,n){
                loop++;
                appendNow++;
                var issue = n.issue;
                if (i == 0)
                    issue=$.lt_curissue.issue;
                if (isshow == false && st_issue == issue) {//如果选择的期数为当前期，则开始显示
                    isshow = true;
                    $($.lt_id_data.id_tra_today).click();
                }
                if( isshow == false ){
                    acount++;
                    maxe = Math.max(maxe,acount);//取最大的位置
                }else{
                    mins = Math.min(mins,acount);//取最小位置
                }
                if( loop >= mins && loop <= maxe ){//如果没有超过要处理的最大数，则继续处理
                    if( isshow == true ){//显示
                        $("#tr_trace_"+n.issue,$($.lt_id_data.id_tra_issues)).show();
                    } else {//隐藏

                        $("#tr_trace_"+n.issue,$($.lt_id_data.id_tra_issues)).hide();
                    }
                }
                if( loop > maxe ){//超过则退出不再处理
                    return false;
                }
                if (appendNow > maxAppendnow)
                    return false;
            });
            $.each($.lt_issues.tomorrow,function(i,n){
                loop++;
                appendNow++;
               // console.info(appendNow + "  " + maxAppendnow);
                if( isshow == false && st_issue == n.issue ){//如果选择的期数为当前期，则开始显示
                    isshow = true;
                    $($.lt_id_data.id_tra_tom).click();
                }
                if( isshow == false ){
                    acount++;
                    maxe = Math.max(maxe,acount);
                }else{
                    mins = Math.min(mins,acount);//取最小位置
                }
                if( loop >= mins && loop <= maxe ){//如果没有超过要处理的最大数，则继续处理
                    if( isshow == true ){//显示
                        $("#tr_trace_"+n.issue,$($.lt_id_data.id_tra_issues)).show();
                    } else {//隐藏
                        $("#tr_trace_"+n.issue,$($.lt_id_data.id_tra_issues)).hide();
                    }
                }
                if( loop > maxe ){//超过则退出不再处理
                    return false;
                }

                if (appendNow > maxAppendnow)
                    return false;
            });
            //更新可追号期数

            t_count = $.lt_issues.tomorrow.length - acount;
            t_count = t_count > 20 ? 20 : t_count;///
            if($("#lt_trace_qissueno").val()=='all'){
                $("#lt_trace_count_input").val(t_count);
            }
            t_nowpos = acount;
            $($.lt_id_data.id_tra_alct).html(t_count);
            //更新追号总期数和总金额
            $.lt_trace_issue = 0;
            $.lt_trace_money = 0;
            upTraceCount();
        };
        //起始期变动对追号区的影响
        $("#lt_issue_start").change(function(){
            if( $($.lt_id_data.id_tra_if).hasClass("clicked") == true ){//如果是选择了追号的情况才更新追号区
                _initTraceByIssue();
            }
        });
        //是否追号选择处理
        $("#lt_trace_if_button").click(function(){
            if( $("#lt_trace_if_button").attr("checked")){
                //检测是否有投注内容
                if( $.lt_total_nums <= 0 ){
                    parent.$.alert(lot_lang.am_s7);
					$("#lt_trace_if_button").attr("checked",false)
                    return;
                }
                $($.lt_id_data.id_tra_stop).attr("disabled",false).attr("checked",true);
                //$(this).addClass("clicked");
                $($.lt_id_data.id_tra_box1).show();
                $($.lt_id_data.id_tra_box2).show();
                $($.lt_id_data.id_tra_ifb).val('yes');
                _initTraceByIssue();
                $('#main',parent.document).height($('#main',parent.document).contents().find("body").height());//重设 iframe 高度
            }else{
                $($.lt_id_data.id_tra_stop).attr("disabled",true).attr("checked",false);
                $($.lt_id_data.id_tra_box1).hide();
                $($.lt_id_data.id_tra_box2).hide();
                $($.lt_id_data.id_tra_ifb).val('no');
                //$(this).removeClass("clicked");
                $('#main',parent.document).height($('#main',parent.document).contents().find("body").height());//重设 iframe 高度
            }
        });
/*
		$("#lt_trace_stop_a").click(function(){
			if($("#lt_trace_stop_a").hasClass('clicked')){
				$(this).removeClass("clicked");
				$($.lt_id_data.id_tra_stop).attr("checked",true);
			}else{
				$(this).addClass("clicked");
				$($.lt_id_data.id_tra_stop).attr("checked",false);
			}
		}
*/
        //今天明天标签切换
        $($.lt_id_data.id_tra_today).click(function(){//今天
            if( $(this).attr("class") != "selected" ){
                $(this).attr("class","selected");
                $($.lt_id_data.id_tra_tom).attr("class","");
                $("#lt_trace_issues_today").show();
                $("#lt_trace_issues_tom").hide();
            }
        });
        $($.lt_id_data.id_tra_tom).click(function(){//明天
            if( $(this).attr("class") != "selected" ){
                $(this).attr("class","selected");
                $($.lt_id_data.id_tra_today).attr("class","");
                $("#lt_trace_issues_today").hide();
                $("#lt_trace_issues_tom").show();
            }
        });
        //根据利润率计算当期需要的倍数[起始倍数，利润率，单倍购买金额，历史购买金额，单倍奖金],返回倍数
        var computeByMargin = function(s,m,b,o,p){
            s = s ? parseInt(s,10) : 0;
            m = m ? parseInt(m,10) : 0;
            b = b ? Number(b) : 0;
            o = o ? Number(o) : 0;
            p = p ? Number(p) : 0;
            var t = 0;
            if( b > 0 ){
                if( m > 0 ){
                    t = Math.ceil( ((m/100+1)*o)/(p-(b*(m/100+1))) );
                }else{//无利润率
                    t = 1;
                }
                if( t < s ){//如果最小倍数小于起始倍数，则使用起始倍数
                    t = s;
                }
            }
            return t;
        };
        //立即生成按钮
        $($.lt_id_data.id_tra_ok).click(function(){
            var c = parseInt($.lt_total_nums,10);//总投注注数
            if( c <= 0 ){//无投注内容
                parent.$.alert(lot_lang.am_s7);
                return false;
            }
            var p = 0;//奖金
            if( t_type == 'margin' ){//如果为利润率追号则首先检测是否支持
                var marmt = 0;
                var marmd = 0;
                var martype =0;//利润率支持情况，0:支持,1:玩法不支持，2:多玩法，3:多圆角模式
                $.each($('tr',$($.lt_id_data.id_cf_content)),function(i,n){
                    if( marmt != 0 && marmt != $(n).data('data').methodid ){
                        martype = 2;
                        return false;
                    }else{
                        marmt = $(n).data('data').methodid;
                    }
                    if( marmd != 0 && marmd != $(n).data('data').modes ){
                        martype = 3;
                        return false;
                    }else{
                        marmd = $(n).data('data').modes;
                    }
					//北京快乐吧：不支持利润率追号！
                    if( $(n).data('data').prize <= 0 || $.inArray($(n).data('data').methodid, [371,373,375,377,379,381,383]) > -1 ){
                        martype = 1;
                        return false;
                    }else{
                        p = $(n).data('data').prize;
                    }
                });
                if( martype == 1 ){
                    parent.$.alert(lot_lang.am_s32);
                    return false;
                }else if( martype == 2 ){
                    parent.$.alert(lot_lang.am_s31);
                    return false;
                }else if( martype == 3 ){
                    parent.$.alert(lot_lang.am_s33);
                    return false;
                }
            }
            var ic = parseInt($("#lt_trace_count_input").val(),10);//追号总期数
            ic = isNaN(ic) ? 0 : ic;
            if( ic <= 0 ){//期数没有填
                parent.$.alert(lot_lang.am_s8);
                return false;
            }
            if( ic > t_count ){//超过可追号期数 
                parent.$.alert(lot_lang.am_s9);
                $("#lt_trace_count_input").val(t_count);
                return false;
            }
            var times = parseInt($("#lt_trace_times_"+t_type).val(),10);//倍数，当前追号方式里的倍数
            times = isNaN(times) ? 0 : times;
            if( times <= 0 ){
                parent.$.alert(lot_lang.am_s10);
                return false;
            }
            times = isNaN(times) ? 0 : times;
            var td = [];//根据用户填写的条件生成的每期数据
            var tm = 0;//生成后的总金额
            var msg='';//提示信息
            if( t_type == 'same' ){
                var m = $.lt_trace_base*times;//每期金额
                tm = m*ic;//总金额=每期金额*期数
                for( var i=0; i<ic; i++ ){
                    td.push({times:times,money:m});
                }
                msg = lot_lang.am_s12.replace("[times]",times);
            }else if( t_type == 'diff' ){
                var d = parseInt($("#lt_trace_diff").val(),10);//相隔期数
                d = isNaN(d) ? 0 : d;
                if( d <= 0 ){
                    parent.$.alert(lot_lang.am_s11);
                    return false;
                }
                var m = $.lt_trace_base;//每期金额的初始值
                var t = 1;//起始倍数为1
                for( var i=0; i<ic; i++ ){
                    if( i!= 0 && (i%d) == 0  ){
                        t *= times;
                    }
                    td.push({times:t,money:m*t});
                    tm += m*t;
                }
                msg = lot_lang.am_s13.replace("[step]",d).replace("[times]",times);
            }else if( t_type == 'margin' ){//利润追号
                var e = parseInt($("#lt_trace_margin").val(),10);//最低利润率
                e = isNaN(e) ? 0 : e;
                if( e <= 0 ){
                    parent.$.alert(lot_lang.am_s30);
                    return false;
                }
                var m = $.lt_trace_base;//每期金额的初始值
                if( e >= ((p*100/m)-100) ){
                    parent.$.alert(lot_lang.am_s30);
                    return false;
                }
                var t = 0;//返回的倍数
                for( var i=0; i<ic; i++ ){
                    t = computeByMargin(times,e,m,tm,p);
                    td.push({times:t,money:m*t});
                    tm += m*t;
                }
                msg = lot_lang.am_s34.replace("[margin]",e).replace("[times]",times);
            }
            msg += lot_lang.am_s14.replace("[count]",ic);
            parent.$.confirm(msg,function(){
                cleanTraceIssue();//清空以前的追号方案
                var $s = $("tr:visible",$($.lt_id_data.id_tra_issues));
                for( i=0; i<ic; i++ ){
                    $($s[i]).find(":checkbox").attr("checked",true);
                    $($s[i]).find("input[name^='lt_trace_times_']").val(td[i].times).attr("disabled",false).data("times",td[i].times);
                    $($s[i]).find("span[id^='lt_trace_money_']").html(JsRound(td[i].money,2,true));
                    $($s[i]).children().addClass("tmp");
                }
                $.lt_trace_issue = ic;
                $.lt_trace_money = tm;
                upTraceCount();
            },'','',300);
        });
    }
})(jQuery);