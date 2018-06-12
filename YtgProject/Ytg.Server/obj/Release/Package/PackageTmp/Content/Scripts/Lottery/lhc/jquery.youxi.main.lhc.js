/*
 * 美亚娱乐
 *
 * version: 1.0.0 (2015/06/11)
 * @ jQuery jquery-1.10.2 
 * Copyright 2015 
 */
var is_select=0;
;(function($){//start
    //check the version, need 1.3 or later , suggest use 1.4
    if (/^1.2/.test($.fn.jquery) || /^1.1/.test($.fn.jquery)) {
    	alert('requires jQuery v1.3 or later !  You are using v' + $.fn.jquery);
    	return;
    }
    $.gameInit = function(opts){//整个购彩界面的初始化
        var ps = {//整个JS的初试化默认参数
            data_label : [],
            data_id : {
                        id_cur_issue    : '#current_issue',//装载当前期的ID
                        id_cur_end      : '#current_endtime',//装载当前期结束时间的ID
                        id_count_down   : '#count_down',//装载倒计时的ID
                        id_labelbox     : '#lotteryType', //装载大标签的元素ID(原来的是:lt_big_label)
                        id_methoddesc   : '#lt_desc',//装载玩法描述的ID
                        id_methodhelp   : '#lt_help',//玩法帮助
                        id_helpdiv      : '#lt_help_div',//玩法帮助弹出框
                        id_selector     : '#lt_selector',//装载选号区的ID
                        id_sel_num      : '#lt_sel_nums',//装载选号区投注倍数的ID
                        id_sel_money    : '#lt_sel_money',//装载选号区投注金额的ID
                        id_sel_times    : '#lt_sel_times',//选号区倍数输入框ID
                        id_sel_insert   : '#lt_sel_insert',//添加按钮
                        id_sel_modes    : '#lt_sel_modes',//元角模式选择
                        id_cf_count     : '#lt_cf_count', //统计投注单数
                        id_cf_clear     : '#lt_cf_clear', //清空确认区数据的按钮ID
                        id_cf_content   : '#lt_cf_content',//装载确认区数据的TABLE的ID
                        id_cf_num       : '#lt_cf_nums',//装载确认区总投注注数的ID
                        id_cf_money     : '#lt_cf_money',//装载确认区总投注金额的ID
                        id_issues       : '#lt_issues',//装载起始期的ID
                        id_sendok       : '#lt_sendok',  //立即购买按钮
                        id_tra_if       : '#lt_trace_if',//是否追号的div
                        id_tra_ifb      : '#lt_trace_if_button',//是否追号的hidden input
                        id_tra_stop     : '#lt_trace_stop',//是否追中即停的checkbox ID
                        id_tra_box1     : '#lt_trace_box1',//装载整个追号内容的ID，主要是隐藏和显示
                        id_tra_box2     : '#lt_trace_box2',//装载整个追号内容的ID，主要是隐藏和显示
                        id_tra_today    : '#lt_trace_today',//今天按钮的ID
                        id_tra_tom      : '#lt_trace_tomorrow',//明天按钮的ID
                        id_tra_alct     : '#lt_trace_alcount',//装载可追号期数的ID
                        id_tra_label    : '#lt_trace_label',//装载同倍，翻倍，利润追号等元素的ID
                        id_tra_lhtml    : '#lt_trace_labelhtml',//装载同倍翻倍等标签所表示的内容
                        id_tra_ok       : '#lt_trace_ok',//立即生成按钮
                        id_tra_issues   : '#lt_trace_issues',//装载追号的一系列期数的ID
                        id_beishuselect : '#lt_beishuselect',//jack 增加的下拉框选择倍数ID
                        id_methodexample: '#lt_example',//玩法示例
                        id_examplediv: '#lt_example_div',//玩法示例弹出框
                        id_count: '#lt_count',//玩法示例
                        id_random_area: '#lt_random_area',//随机选号
                        id_countdiv: '#lt_count_div',//玩法示例弹出框  
                        id_poschoose: '#poschoose',//任选玩法万千百十个容器
                        lt_onend: opts.onend,//当期结束
						id_lhc: '#id_lhc'//六合彩容器 
                    },

            cur_issue : {},  //当前期
            issues    : {//所有的可追号期数集合
                         today:[],
                         tomorrow: []
                        },
            servertime : '',//服务器时间[与服务器同步]
            ajaxurl    : '',    //提交的URL地址,获取下一期的地址是后面加上flag=read,提交是后面加上flag=save
            lotteryid  : 1,//彩种ID
            ontimeout  : function(){},//时间结束后执行的函数
            onfinishbuy: function(){},//购买成功后调用的函数
            test : ''
        }
        opts = $.extend( {}, ps, opts || {} ); //根据参数初始化默认配置
        /*************************************全局参数配置 **************************/
		$.extend({
			lt_id_data : opts.data_id,
			lt_method_data : {},//当前所选择的玩法数据
			lt_method : {
		
			},
            lt_issues : opts.issues,//所有的可追号期的初始集合
            lt_ajaxurl: opts.ajaxurl,
            lt_lottid : opts.lotteryid,
            lt_total_nums : 0,//总投注注数
            lt_total_money: 0,//总投注金额[非追号]
			lt_total_bill : 0,//几单
            lt_time_leave : 10, //本期剩余时间
            lt_same_code  : [],//用于限制一个方法里不能投相同单
            lt_ontimeout  : opts.ontimeout,
            lt_onfinishbuy: opts.onfinishbuy,
            lt_trace_base : 0,//追号的基本金额.
            lt_submiting  : false,//是否正在提交表单
            lt_prizes: [], //投注内容的奖金情况
            lt_inint_url: '/Page/Initial.aspx',//读取服务器时间url
            lt_betting_url: '/Page/Lott/LotteryBetDetail.aspx?loginUserId=' + Ytg.common.user.info.user_id + "&lotterycode=" + opts.lotteryCode + "&lotteryid=" + opts.lotteryid,//投注相关url
            lt_lottery_basic_url: '/Page/Lott/LotteryBasic.aspx?lotterycode=' + opts.lotteryCode + '&lotteryid=' + opts.lotteryid,
        });
        ps = null;
        opts.data_id = null;
        opts.issues  = null;
        opts.ajaxurl = null;
        opts.lotteryid = null;
        if( $.browser.msie ){//&& /MSIE 6.0/.test(navigator.userAgent)
            CollectGarbage();//释放内存
        }
        //开始倒计时
        $($.lt_id_data.id_count_down).lt_timer(opts.servertime, opts.cur_issue.scendtime, opts.cur_issue.endtime);
		
        var bhtml = ''; //大标签HTML
        var hasdefault = false;
        $.each(opts.data_label, function(i,n){//生成标签
            if(typeof(n)=='object'){
                if( i == 0 ){//第一个标签自动选择
                    if(n.isnew == 1)
                        {
                            bhtml += '<li><span class="m">'+n.title+'</span><em></em></li>';
                        } else {
                            bhtml += '<li><span class="m">'+n.title+'</span></li>';
                        }
						lt_smalllabel({//生成该标签下的小标签
                            title:n.title,
                            label:n.label 
						});
                }else{
                    if(n.isdefault==1){//选择默认标签
                        hasdefault = true;
                 //     alert(ssss[0])
                        bhtml = bhtml.replace(/(menu_0[1-3])a/gi,"$1");
                        if(n.isnew == 1){
							bhtml += '<li class="hover"><span class="m">'+n.title+'</span><em></em></li>';
                        }else{
							bhtml += '<li class="hover"><span class="m">'+n.title+'</span></li>';
                        }
						lt_smalllabel({//生成该标签下的小标签
                            title:n.title,
                            label:n.label 
						});
                    }else{ 
						if(n.isnew == 1){
							bhtml += '<li><span class="m">'+n.title+'</span><em></em></li>';
						} else {
							bhtml += '<li><span class="m">'+n.title+'</span></li>';
						} 
                    }
                }
    
            }

        });
		$($.lt_id_data.id_labelbox).prepend(bhtml);
		
        //如果没有设置默认玩法，将第一个设置为默认玩法
        if(hasdefault == false){
            $($.lt_id_data.id_labelbox + " li").eq(0).removeClass().addClass("hover");
        }
		
		$($.lt_id_data.id_labelbox + " li").click(function(){
			var index = $($.lt_id_data.id_labelbox + " li").index($(this));
			var url = $(this).attr("url");
			$($.lt_id_data.id_labelbox + " li").removeClass("hover");
			$(this).addClass("hover");
			$(".Contentbox").hide();
			if(url){
				$("#Contentbox_" + index).show().load(url);
			}else{
				$("#Contentbox_" + index).show();
			}
		});
		
        //写入当前期
		$($.lt_id_data.id_cur_issue).html(opts.cur_issue.issue);
        //写入当前期结束时间
		$($.lt_id_data.id_cur_end).html(opts.cur_issue.endtime);
		
		
        //生成并写入起始期内容
		var chtml = '<select name="lt_issue_start" id="lt_issue_start">';
		chtml += '<option value="' + opts.cur_issue.issue + '">' + opts.cur_issue.issue + lot_lang.dec_s7 + '</option>';
		$.each($.lt_issues.today, function (i, n) {
		    chtml += '<option value="' + n.issue + '">' + n.issue + (n.issue == opts.cur_issue.issue ? lot_lang.dec_s7 : '') + '</option>';
		});
		var t = $.lt_issues.tomorrow.length - $.lt_issues.today.length;
		if (t > 0) {//如果当天的期数小于每天的固定期数则继续增加显示
		    for (i = 0; i < t; i++) {
		        chtml += '<option value="' + $.lt_issues.tomorrow[i].issue + '">' + $.lt_issues.tomorrow[i].issue + '</option>';
		    }
		}
		chtml += '</select><input type="hidden" name="lt_total_nums" id="lt_total_nums" value="0"><input type="hidden" name="lt_total_money" id="lt_total_money" value="0">';
        $(chtml).appendTo($.lt_id_data.id_issues);
		
		//追号区
        $($.lt_id_data.id_tra_if).lt_trace({issues:opts.issues});
		
		//添加按钮
		$($.lt_id_data.id_sel_insert).click(function(){
			//{'type':'digital','methodid':200149,'codes':'49','nums':1,'omodel':2,'times':100,'money':100,'mode':1,'desc':'[特码01到49_特码49] 49'}
			var canInsert = true;
			$.each($("input[name='method']"),function(i,n){			
				if($.trim($(n).val()) != ""){
					if(isNaN($.trim($(n).val())) || $(this).val().indexOf(".") != -1 || ($.trim($(n).val()) - 0) <= 0){
						$(n).val("");
						canInsert = false;
						parent.$.alert("只允许输入大于0的整数");
					}
				}
			});
			
			if(canInsert){
				$.each($("input[name='method']"),function(i,n){
					var type,methodid,codes,nums,omodel,bill,times,money,mode,desc,gtitle,serverdata,inputHtml,$inputHtml;
					if($(n).val() != ""){
						type = $(n).attr("tp");
						methodid = $(n).attr("methodid");
						codes = $(n).attr("codes");
						nums = 1;
						omodel = 1;
						bill = 1;
						times = $(n).val();
						money = $(n).val();
						mode = $("input[name='lt_project_modes']").val();
						desc = $(n).attr("desc");
						gtitle = $(n).attr("gtitle");
						desc = "[" + gtitle + "_" + desc + "] " + codes;
						serverdata = "{'type': '" + type + "','methodid': '" + methodid + "','codes': '" + codes + "','nums': '" + nums + "','omodel': '" + omodel + "','times': '" + times + "','money': '" + money + "','mode': '" + mode + "','desc': '" + desc + "'}";
						//serverdata = {'type': type ,'methodid': methodid,'codes': codes ,'nums': nums ,'omodel': omodel ,'times': times ,'money': money ,'mode': mode ,'desc': desc};
						inputHtml = '<tr class="table_line"><th width="70%" style="padding-left:10px;">'+desc+' </th><td width="50">'+nums+lot_lang.dec_s1+'</td><td width="50">1'+lot_lang.dec_s2+'</td><td width="80">'+money+lot_lang.dec_s3+'</td><td class="del" width="30"><span><img src="/content/images/skin/btn_08.jpg"></span><input type="hidden" name="lt_project[]" value="'+serverdata+'" /></td></tr>';
						$inputHtml = $(inputHtml);
						$inputHtml.prependTo($.lt_id_data.id_cf_content);
						//数据绑定
						$inputHtml.data('data',{'type': type ,'methodid': methodid,'codes': codes ,'nums': nums ,'omodel': omodel ,'times': times ,'money': money ,'mode': mode ,'desc': desc ,'bill': bill});
						$(n).val("");
						$.lt_total_nums = parseInt($.lt_total_nums) + parseInt(nums);
						$.lt_total_money = parseInt($.lt_total_money) + parseInt(money);
						$.lt_total_bill = parseInt($.lt_total_bill) + parseInt(bill);
						checkNum();
						showClearAll();
						
						$('td',$inputHtml).filter(".del").find("span").css("cursor",'pointer').attr("title",lot_lang.dec_s24).click(function(){
							var nums = $inputHtml.data('data').nums;
							var money = $inputHtml.data('data').money;
							var bill = $inputHtml.data('data').bill;
							$.lt_total_nums = parseInt($.lt_total_nums) - parseInt(nums);
							$.lt_total_money = parseInt($.lt_total_money) - parseInt(money);
							$.lt_total_bill = parseInt($.lt_total_bill) - parseInt(bill);
							$(this).parent().parent().remove();
							checkNum();
							showClearAll();
						});
					} 
				});
			}
		});
		
		//确认投注按钮事件
		$($.lt_id_data.id_sendok).lt_ajaxSubmit();

    }
	
	function checkNum(){//实时计算投注注数与金额
		//统计投注单数
		$($.lt_id_data.id_cf_count).html($.lt_total_bill);
		//统计总投注数
		$($.lt_id_data.id_cf_num).html($.lt_total_nums);
		//统计总投注金额
		$($.lt_id_data.id_cf_money).html($.lt_total_money);
		lt_selcountback();
	}
	
	//统计归0
    var lt_selcountback = function(){
        $($.lt_id_data.id_sel_money).html(0);
        $($.lt_id_data.id_sel_num).html(0);
    };
	
    var lt_smalllabel = function(opts){//动态载入小标签
        var ps = {};    //标签数据
        opts   = $.extend( {}, ps, opts || {} ); //根据参数初始化默认配置
        var html = '';
        $.each(opts.label, function(i,n){
			var gtitle = n.gtitle;
			$.each(n.label, function(ii,nn){
				if(typeof(nn)=='object'){
					if(ii % 10 == 0){
						html += '<dl><dt><span class="w48">号码</span><span class="w50">赔率</span><span class="w50">金额</span></dt>';
					}
					html += '<dd><div class="w48"><div class="' + nn.color + '-ball_x">' + nn.num + '</div></div><span class="w50">' + nn.prize[1] + '</span><input name="method" gtitle="' + gtitle + '" methodid="'+ nn.methodid + '" codes="'+ nn.num + '" desc="'+ nn.desc + '" tp="' + nn.type + '" type="text" class="w42" /></dd>';
					
					if(ii % 10 == 9 || ii == n.label.length){
						html += '</dl>';
					}
				}
			});
        });
		//填充六合彩容器
        $($.lt_id_data.id_lhc).empty().html(html);
		
		//实时显示选中多少注多少钱
		$("input[name='method']").keyup(function(){
			var canInsert = true;
			var v = $.trim($(this).val());
			if(v != ""){
				if(isNaN(v) || v.indexOf(".") != -1 || (v - 0) <= 0){
					$(this).val("");
					canInsert = false;
					parent.$.alert("只允许输入大于0的整数");
					chooseRecord();
				}
			}
			if(canInsert){
				chooseRecord();
			}
		})
    };
	
	//记录选择的注数
	function chooseRecord(){
		var nums = 0,money = 0;
		$.each($("input[name='method']"),function(i,n){
			if($.trim($(n).val()) != ""){
				nums = nums - 0 + 1;
				money = money - 0 + parseInt($.trim($(n).val()));
				$(n).val(parseInt($.trim($(n).val())));
			}else{
				$(n).val($.trim($(n).val()));
			}
		})
		$($.lt_id_data.id_sel_money).html(money);
		$($.lt_id_data.id_sel_num).html(nums);
	}
	
	/*全清功能*/
	function showClearAll(){
		var numtotal = $($.lt_id_data.id_cf_count).text();
		if(numtotal > 1){
			$(".lottery_list .cleanall").show();
			$(".lottery_list .cleanall").click(function(){
				var num = $($.lt_id_data.id_cf_content + " tr td span[title=" + lot_lang.dec_s24 + "]").length;
				if(num > 0){
					$.each($($.lt_id_data.id_cf_content + " tr td span[title=" + lot_lang.dec_s24 + "]"),function(i,v){
						$(v).trigger('click');
					});
					$(this).unbind().hide();
				}
			});
		}else{
			$(".lottery_list .cleanall").hide();
		}
	}

    //倒计时
	$.fn.lt_timer = function (start, end, betend) {//服务器开始时间，服务器结束时间
	  
	    var me = this;
	    if (start == "" || end==undefined || end == "" || betend == "") {
	        $.lt_time_leave = 0;
	        $.lt_time_end = 0;
	        return;
	    } else {
	        $.lt_time_leave = (format(end).getTime() - format(start).getTime()) / 1000;//总秒数 封单
	        $.lt_time_end = (format(betend).getTime() - format(start).getTime()) / 1000;//总秒数; 
	        $.lt_time_end = $.lt_time_end - $.lt_time_leave;
	        $.lt_time_leave--;
	    }
	    function fftime(n) {
	        return Number(n) < 10 ? "" + 0 + Number(n) : Number(n);
	    }
	    function format(dateStr) {//格式化时间
	        
	        return new Date(dateStr.replace(/[\-\u4e00-\u9fa5]/g, "/"));
	    }
	    function diff(t) {//根据时间差返回相隔时间
	        return t > 0 ? {
	            day: Math.floor(t / 86400),
	            hour: Math.floor(t % 86400 / 3600),
	            minute: Math.floor(t % 3600 / 60),
	            second: Math.floor(t % 60)
	        } : { day: 0, hour: 0, minute: 0, second: 0 };
	    }
	    var timerno = window.setInterval(function () {

	        //if ($.lt_time_leave > 0 && ($.lt_time_leave % 240 == 0 || $.lt_time_leave == 25)) {//每隔4分钟以及最后一分钟重新读取服务器时间
	        //    $.ajax({
	        //        type: 'POST',
	        //        URL: $.lt_inint_url,
	        //        timeout: 30000,
	        //        data: "lotteryid=" + $.lt_lottid + "&issue=" + $($.lt_id_data.id_cur_issue).html() + "&action=nowtime",
	        //        success: function (data) {//成功
	        //            data = parseInt(data, 10);
	        //            data = isNaN(data) ? 0 : data;
	        //            data = data <= 0 ? 0 : data;
	        //            $.lt_time_leave = data;
	        //        }
	        //    });
	        //}

	        /*倒计时倒数还有一分钟的时候播放 =============================================== */
	        if ($.lt_time_leave <= 10) {
	            //minute1._mPlay();
	            opening();
	        }
	        /*倒计时倒数还有一分钟的时候播放 =============================================== */

	        /*封单的时候播放 =============================================== */
	        if ($.lt_time_leave == 0) {
	            //fengdan._mPlay();
	        }
	        /*封单的时候播放 =============================================== */
	       
	        if ($.lt_time_leave <= 0) {//封单结束

	            if ($.lt_submiting == false) {//如果没有正在提交数据则弹出对话框,否则主动权交给提交表单
	                parent.$.unblockUI({ fadeInTime: 0, fadeOutTime: 0 });
	                if ($($.lt_id_data.id_cur_issue).html() > '') {
	                    //parent.$.alert(lot_lang.am_s15);
	                    clearInterval(timerno);//清除time
	                    $.lt_ontimeout();
	                    $.lt_reset(true);//继续下一期
	                } else {
	                    parent.$.alert(lot_lang.am_s15_2);
	                }
	                //play_click('/Content/audio/playsound.mp3');
	                //计时准备开奖
	                var opendTimerno = setInterval(function () {
	                    //opening();
	                    if ($.lt_time_end <= 0) {
	                        clearInterval(opendTimerno);//清除计时
	                        //结束销售，开奖
	                       // $.lt_onend();
	                        // console.log("lt_onend");
	                    }
	                    $.lt_time_end--;
	                }, 1000);
	            }
	        }

	        var oDate = diff($.lt_time_leave--);
	        $(me).html("<div class='day'>" + (oDate.day > 0 ? oDate.day + (lot_lang.dec_s21) + "</div>" : "<div class='day' >&nbsp;&nbsp;&nbsp;&nbsp;</div>") + "<div class=\"hour\">" + fftime(oDate.hour) + ":</div><div class=\"min\">" + fftime(oDate.minute) + ":</div><div class=\"sec\">" + fftime(oDate.second) + "</div>");
	    }, 1000);

	};
	
	
	$.lt_reset = function(iskeep){
	    if( iskeep && iskeep === true ){
            iskeep = true;
        }else{
            iskeep = false;
        }
        if( $.lt_time_leave <= 0 ){//本期结束后的刷新
            //01:刷新选号区
            if( iskeep == false ){
                $(":radio:checked",$($.lt_id_data.id_smalllabel)).removeData("ischecked").click();
            }
            //02:刷新确认区
            if( iskeep == false ){
                $.lt_total_nums  = 0;//总注数清零
                $.lt_total_money = 0;//总金额清零
                $.lt_trace_base  = 0;//追号基础数据
                $.lt_same_code   = [];//已在确认区的数据
                $($.lt_id_data.id_cf_num).html(0);//显示数据清零
                $($.lt_id_data.id_cf_money).html(0);//显示数据清零
                $($.lt_id_data.id_cf_content).children().empty();
                $($.lt_id_data.id_cf_count).html(0);
                $("#times").attr('selected');
            }
        }else{//提交表单成功后的刷新
            //01:刷新选号区
            if( iskeep == false ){
                $(":radio:checked",$($.lt_id_data.id_smalllabel)).removeData("ischecked").click();
            }
            //02:刷新确认区
            if( iskeep == false ){
                $.lt_total_nums  = 0;//总注数清零
                $.lt_total_money = 0;//总金额清零
                $.lt_trace_base  = 0;//追号基数
                $.lt_same_code   = [];//已在确认区的数据
                $($.lt_id_data.id_cf_num).html(0);//显示数据清零
                $($.lt_id_data.id_cf_money).html(0);//显示数据清零
                $($.lt_id_data.id_cf_content).children().empty();
                $($.lt_id_data.id_cf_count).html(0);
            }
            //03:刷新追号区
            if( iskeep == false ){
                cleanTraceIssue();//清空追号区数据
            }
        }
	};
    //提交表单
	var ajaxSubmitAllow = true;
	$.fn.lt_ajaxSubmit = function(){
	    var me = this;

	    $(this).click(function () {
			/*if($.lt_lottid == '4'){
			$.alert('该彩种已经停止销售！敬请留意网站公告！');
			return;
			}*/
	        if( checkTimeOut() == false ){
	            return;
	        }
	        $.lt_submiting = true;//倒计时提示的主动权转移到这里
	        //var istrace = $($.lt_id_data.id_tra_if).hasClass("clicked");//是否追号
			var istrace = $($.lt_id_data.id_tra_ifb).attr("checked")=='checked'?1:0;//是否追号
			//alert(istrace);
            if( $.lt_total_nums <= 0 || $.lt_total_money <= 0 ){//检查是否有投注内容
                $.lt_submiting = false;
                parent.$.alert(lot_lang.am_s6);
                return;
            }
            if( istrace == true ){
	            if( $.lt_trace_issue <= 0 || $.lt_trace_money <= 0 ){//检查是否有追号内容
	                $.lt_submiting = false;
	                parent.$.alert(lot_lang.am_s20);
                    return;
	            }
	            var terr = "";
	            $("input[name^='lt_trace_issues']:checked",$($.lt_id_data.id_tra_issues)).each(function(){
	                if( Number($(this).closest("tr").find("input[name^='lt_trace_times_']").val()) <= 0 ){
	                    terr += $(this).val()+'\n';
	                }
	            });
	            if( terr.length > 0 ){//有错误倍数的奖期
	                $.lt_submiting = false;
	                parent.$.alert(lot_lang.am_s21.replace("[errorIssue]", terr));
                    return;
	            }
	        }
            if( istrace == true ){
                var msg = lot_lang.am_s14.replace("[count]",$.lt_trace_issue);
            }else{
                var msg = lot_lang.dec_s8.replace("[issue]",$("#lt_issue_start").val());
            }
            msg += '<div class="floatarea" style="height:150px;">';
            var modesmsg = [];
            var desc;
            $.each($('tr',$($.lt_id_data.id_cf_content)),function(i,n){
                desc = $(n).data('data').desc;
				msg += "<p><font>" + desc + "</font></p>";
            });
            msg += '</div>';
            $.lt_trace_money = Math.round($.lt_trace_money*1000)/1000;
            msg += lot_lang.dec_s9+': '+(istrace==true ? $.lt_trace_money : $.lt_total_money)+' '+lot_lang.dec_s3;
            parent.$.confirm(msg, function () {//点确定[提交]

                if( checkTimeOut() == false ){//正式提交前再检查1下时间
                    $.lt_submiting = false;
    	            return;
    	        }
    	        $("#lt_total_nums").val($.lt_total_nums);
    	        $("#lt_total_money").val($.lt_total_money);
    	        if (ajaxSubmitAllow) {
    	            ajaxSubmitAllow = false;
    	            ajaxSubmit();
    	        }
            },function(){//点取消
                $.lt_submiting = false;
                return checkTimeOut();
            },'',400);
        });
        //检查时间是否结束，然后做处理
        function checkTimeOut(){
            if( $.lt_time_leave <= 0 ){//结束
				if($($.lt_id_data.id_cur_issue).html()>''){
				    parent.$.confirm(lot_lang.am_s15, function () {//确定
						$.lt_reset(false);
						$.lt_ontimeout();
					},function(){//取消
						$.lt_reset(true);
						$.lt_ontimeout();
					});
				}else{
				    parent.$.alert(lot_lang.am_s15_2);
				}
                return false;
            }else{
                return true;
            }
        };
        


        //ajax提交表单 sean
        function ajaxSubmit(){
            parent.$.blockUI({
                message: lot_lang.am_s22,
                overlayCSS: { backgroundColor: '#FFFFFF', }
            });
            var form = $(me).closest("form");
            var randomNum = Math.floor((Math.random() * 10000) + 1);
            $.ajax({
                type: 'POST',
                url: $.lt_betting_url,
                timeout : 600000,
                data: $(form).serialize() + "&randomNum=" + randomNum + "&action=htdbetdetail",
                success: function (data) {
                    //解决瞬间提交2次的问题
                    ajaxSubmitAllow = true;
                    parent.$.unblockUI({ fadeInTime: 0, fadeOutTime: 0 });
                    $.lt_submiting = false;
                    var jsonData = JSON.parse(data);
                    if (jsonData.Code == 0) {
                        //成功
                        parent.$.alert(lot_lang.am_s24, lot_lang.dec_s25, function () {
                            if (checkTimeOut() == true) {//时间未结束
                                $.lt_reset();
                            }
                            $.lt_onfinishbuy();
                            /*全清功能*/
                            showClearAll();
                        });
                        return false;
                    } else if (jsonData.Code == 1007) {
                        parent.$.alert(lot_lang.am_s37);
                        return false;
                    } else if (jsonData.Code == 1009) {
                        alert("由于您长时间未操作,为确保安全,请重新登录！");
                        parent.window.location = "/login.html";
                        return false;
                    } else if (jsonData.Code == 1002) {
                        //超出最大注数am_s39
                        parent.$.alert(lot_lang.am_s39);
                    } else if (jsonData.Code == 1003) {
                        parent.$.alert(lot_lang.am_s18)
                    } else if (jsonData.Code == 2001) {
                        parent.$.alert(lot_lang.am_s40);
                    }
                    else {
                        parent.$.alert(lot_lang.am_s16);
                        return false;
                    }
                },
                error: function () {
                    ajaxSubmitAllow = true;
                    $.lt_submiting = false;
                    parent.$.unblockUI({ fadeInTime: 0, fadeOutTime: 0 });
                    parent.$.alert(lot_lang.am_s23, '', checkTimeOut);
                    /*全清功能*/
                    showClearAll();
                }
            });
        };

	};

})(jQuery);
