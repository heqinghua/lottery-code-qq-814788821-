/*
 * 美亚娱乐
 *
 * version: 1.0.0 (2015/06/11)
 * @ jQuery jquery-1.10.2 
 * Copyright 2015 
 */

var is_select = 0;
;
(function ($) {//start
    //check the version, need 1.3 or later , suggest use 1.4
    if (/^1.2/.test($.fn.jquery) || /^1.1/.test($.fn.jquery)) {
        alert('requires jQuery v1.3 or later !  You are using v' + $.fn.jquery);
        return;
    }
    $.gameInit = function (opts) {//整个购彩界面的初始化
        
        var ps = {//整个JS的初试化默认参数
            data_label: [],
            data_methods:{},
            data_id: {
                id_cur_issue: '#current_issue', //装载当前期的ID
                id_cur_end: '#current_endtime', //装载当前期结束时间的ID
                id_count_down: '#count_down', //装载倒计时的ID
                //id_labelbox     : '#lt_big_label', //装载大标签的元素ID
                id_labelbox: '#lotteryType', //装载大标签的元素ID(原来的是:lt_big_label)
                id_smalllabel: '#lt_samll_label', //装载小标签的元素ID
                id_methoddesc: '#lt_desc', //装载玩法描述的ID
                id_methodhelp: '#lt_help', //玩法帮助
                id_helpdiv: '#lt_help_div', //玩法帮助弹出框
                id_selector: '#lt_selector', //装载选号区的ID
                id_sel_num: '#lt_sel_nums', //装载选号区投注倍数的ID
                id_sel_money: '#lt_sel_money', //装载选号区投注金额的ID
                id_sel_times: '#lt_sel_times', //选号区倍数输入框ID
                id_sel_insert: '#lt_sel_insert', //添加按钮
                id_sel_modes: '#lt_sel_modes', //元角模式选择
                id_cf_count: '#lt_cf_count', //统计投注单数
                id_cf_clear: '#lt_cf_clear', //清空确认区数据的按钮ID
                id_cf_content: '#lt_cf_content', //装载确认区数据的TABLE的ID
                id_cf_num: '#lt_cf_nums', //装载确认区总投注注数的ID
                id_cf_money: '#lt_cf_money', //装载确认区总投注金额的ID
                id_issues: '#lt_issues', //装载起始期的ID
                id_sendok: '#lt_sendok', //立即购买按钮
                id_tra_if: '#lt_trace_if', //是否追号的div
                id_tra_ifb: '#lt_trace_if_button', //是否追号的hidden input
                id_tra_stop: '#lt_trace_stop', //是否追中即停的checkbox ID
                id_tra_box1: '#lt_trace_box1', //装载整个追号内容的ID，主要是隐藏和显示
                id_tra_box2: '#lt_trace_box2', //装载整个追号内容的ID，主要是隐藏和显示
                id_tra_today: '#lt_trace_today', //今天按钮的ID
                id_tra_tom: '#lt_trace_tomorrow', //明天按钮的ID
                id_tra_alct: '#lt_trace_alcount', //装载可追号期数的ID
                id_tra_label: '#lt_trace_label', //装载同倍，翻倍，利润追号等元素的ID
                id_tra_lhtml: '#lt_trace_labelhtml', //装载同倍翻倍等标签所表示的内容
                id_tra_ok: '#lt_trace_ok', //立即生成按钮
                id_tra_issues: '#lt_trace_issues', //装载追号的一系列期数的ID
                id_beishuselect: '#lt_beishuselect', //jack 增加的下拉框选择倍数ID
                id_methodexample: '#lt_example', //玩法示例
                id_examplediv: '#lt_example_div', //玩法示例弹出框
                id_count: '#lt_count', //玩法示例
                id_random_area: '#lt_random_area', //随机选号
                id_countdiv: '#lt_count_div', //玩法示例弹出框  
                id_changetype: '#changeLotteryType',
                id_poschoose: '#poschoose',
            },
            cur_issue: {issue: '20130510001', endtime: '2013-05-10 09:10:00'}, //当前期
            issues: {//所有的可追号期数集合
                today: [],
                tomorrow: []
            },
            servertime: '2013-05-10 09:09:40', //服务器时间[与服务器同步]
            lotteryid: 1, //彩种ID
            ontimeout: function () {
                
            },//时间结束后执行的函数停止销售
            onend: function () {
                
            }, //开奖时间
            onfinishbuy: function () {
               
            }, //购买成功后调用的函数
            test: ''
        }
        opts = $.extend({}, ps, opts || {}); //根据参数初始化默认配置
        /*************************************全局参数配置 **************************/
        $.extend({
            lt_id_data: opts.data_id,
            lt_method_data: {}, //当前所选择的玩法数据
            lt_method: opts.data_methods,
            lt_issues: opts.issues, //所有的可追号期的初始集合
            lt_curissue:opts.cur_issue,//当前期号
            lt_preissue:null,//开奖期数
           // lt_ajaxurl: opts.ajaxurl,
            lt_lottid: opts.lotteryid,//id
            lt_lottCode: opts.lotteryCode,//编号
            lt_total_nums: 0, //总投注注数
            lt_total_money: 0, //总投注金额[非追号]
            lt_time_leave: 0, //本期剩余时间，封单
            lt_time_end:0,//本期结束剩余时间，
            lt_same_code: [], //用于限制一个方法里不能投相同单
            lt_ontimeout: opts.ontimeout,
            lt_onfinishbuy: opts.onfinishbuy,
            lt_onend:opts.onend,//当期结束
            lt_trace_base: 0, //追号的基本金额.
            lt_submiting: false, //是否正在提交表单
            lt_prizes: [], //投注内容的奖金情况
            lt_inint_url: '/page/Initial.aspx?loginUserId=' + Ytg.common.user.info.user_id + "&lotterycode=" + opts.lotteryCode + "&lotteryid=" + opts.lotteryid,//读取服务器时间url
            lt_betting_url: '/Page/Lott/LotteryBetDetail.aspx?loginUserId=' + Ytg.common.user.info.user_id + "&lotterycode=" + opts.lotteryCode + "&lotteryid=" + opts.lotteryid,//投注相关url
            lt_lottery_basic_url: '/Page/Lott/LotteryBasic.aspx?lotterycode=' + opts.lotteryCode + '&lotteryid=' + opts.lotteryid,
        });
        ps = null;
        opts.data_id = null;
        opts.issues = null;
       // opts.ajaxurl = null;
        opts.lotteryid = null;
        if ($.browser.msie) {//&& /MSIE 6.0/.test(navigator.userAgent)
            CollectGarbage();//释放内存
        }
       
        //开始倒计时
        $($.lt_id_data.id_count_down).lt_timer(opts.servertime, opts.cur_issue.scendtime, opts.cur_issue.endtime);
        //装载模式选择
        $('<select name="lt_project_modes" id="lt_project_modes"></select>').appendTo($.lt_id_data.id_sel_modes);//此为装载玩sean
        var bhtml = ''; //大标签HTML
        var data_label_count = opts.data_label.length;

        var hasdefault = false;
        $.each(opts.data_label, function (i, n) {//生成标签
            
            if (typeof (n) == 'object') {
                if (i == 0) {//第一个标签自动选择
                    if (n.isnew == 1)
                    {
                        bhtml += '<li value="' + i + '" id="two' + (i + 1) + '"><span class="m">' + n.title + '</span><em></em></li>';
                    } else {
                        bhtml += '<li value="' + i + '" id="two' + (i + 1) + '"><span class="m">' + n.title + '</span></li>';
                    }
                    lt_smalllabel({//生成该标签下的小标签
                        title: n.title,
                        label: n.label});

                } else {
                    if (n.isdefault == 1) {//选择默认标签
                        hasdefault = true;
                      
                        bhtml = bhtml.replace(/(menu_0[1-3])a/gi, "$1");
                        if (n.isnew == 1)
                        {
                            bhtml += '<li value="' + i + '" id="two' + (i + 1) + '" class="hover"><span class="m">' + n.title + '</span><em></em></li>';
                        } else {
                            bhtml += '<li value="' + i + '" id="two' + (i + 1) + '" class="hover"><span class="m">' + n.title + '</span></li>';
                        }
                        lt_smalllabel({//生成该标签下的小标签
                            title: n.title,
                            label: n.label
                        });
                       
                    } else {
                        if (n.isnew == 1)
                        {
                            bhtml += '<li value="' + i + '" id="two' + (i + 1) + '"><span class="m">' + n.title + '</span><em></em></li>';
                        } else {
                            bhtml += '<li value="' + i + '" id="two' + (i + 1) + '"><span class="m">' + n.title + '</span></li>';
                        }
                    }
                }

            }

        });
        
        if ($.lt_lottid == 1)
        {
            $($.lt_id_data.id_changetype).show();
        }
        //alert(bhtml);
        $bhtml = $(bhtml);
        $(bhtml).appendTo($.lt_id_data.id_labelbox);

        //如果没有设置默认玩法，将第一个设置为默认玩法
        if (hasdefault == false) {
            $($.lt_id_data.id_labelbox + " li").eq(0).removeClass().addClass("hover");
        }

        //*
        //下面是对【小标签】进行切换（例如：前三、后三、二码）
        $($.lt_id_data.id_labelbox).children().click(function () {//切换标签
            
            if ($($(this).children()[0]).attr("class").indexOf('a') >= 0) {//如果已经是当前标签则不切换
                return;
            }
            $(this).addClass("hover").siblings().removeClass("hover");

             //*/
            var index = parseInt($(this).attr("value"), 10);
            if (opts.data_label[index].label.length > 0) {
                lt_smalllabel({
                    title: opts.data_label[index].title,
                    label: opts.data_label[index].label
                });
                //setTab('two',(index+1),data_label_count);
            } else {
              //  jjtc();
            }
            
        });//*/
        //写入当前期
        $($.lt_id_data.id_cur_issue).html(opts.cur_issue.issue);
        //写入当前期结束时间
        $($.lt_id_data.id_cur_end).html(opts.cur_issue.endtime);
        /***/
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
        /***/
        //确认区事件
        $("tr", $($.lt_id_data.id_cf_content)).live("mouseover", function () {//确认区行颜色变化效果
            $(this).children().addClass("temp");
        }).live("mouseout", function () {
            $(this).children().removeClass("temp");
        });
        $($.lt_id_data.id_cf_clear).click(function () {//清空按钮
            parent.$.confirm(lot_lang.am_s5, function () {
                $.lt_total_nums = 0;//总注数清零
                $.lt_total_money = 0;//总金额清零
                $.lt_trace_base = 0;//追号金额基数清零
                $.lt_same_code = [];//已在确认区的数据
                $($.lt_id_data.id_cf_num).html(0);//显示数据清零
                $($.lt_id_data.id_cf_money).html(0);//显示数据清零
                $($.lt_id_data.id_cf_count).html(0);//总投注项清零
                $($.lt_id_data.id_cf_content).children().empty();
                cleanTraceIssue();//清空追号区数据
            },"","");
        });
        //追号区
        $($.lt_id_data.id_tra_if).lt_trace({issues: opts.issues});

        //确认投注按钮事件
        $($.lt_id_data.id_sendok).lt_ajaxSubmit();

        //帮助中心
        $($.lt_id_data.id_methodhelp).hover(function () {
            if ($($.lt_id_data.id_helpdiv).html().length > 2) {
                var offset = $(this).offset();
                var lft = 0;
                var vtp = 0;
                if ($($.lt_id_data.id_helpdiv).html().length > 30) {
                    $($.lt_id_data.id_helpdiv).css({"width": "300px"});
                } else {
                    $($.lt_id_data.id_helpdiv).css({"width": ($.browser.msie ? "300px" : "auto")});
                }
                if (offset.left < $($.lt_id_data.id_helpdiv).width()) {
                    lft = (offset.left + 64);
                    vtp = (offset.top + 10);
                    $($.lt_id_data.id_helpdiv).css({ "left": lft + "px", "top": vtp + "px" }).show();
                } else {
                    lft = (offset.left - $($.lt_id_data.id_helpdiv).width() + 64);
                    vtp = (offset.top + 28);
                    $($.lt_id_data.id_helpdiv).css({ "left": lft + "px", "top": vtp + "px" }).show();
                }
                $(".sanjiao").css({ "left": (lft + 245), "top": vtp + 3 }).show();
            }
        }, function () {
            $($.lt_id_data.id_helpdiv).hide();
            $(".sanjiao").hide();
        });


    }

    var lt_smalllabel = function (opts) {//动态载入小标签
        //alert(opts);
        var ps = {title: '', label: []};    //标签数据
        opts = $.extend({}, ps, opts || {}); //根据参数初始化默认配置
        //alert(opts.title);
        var html = '';
        var modelhtml = '';
        function addItem(o, t, v) {
            var i = new Option(t, v);
            o.options.add(i);
        }
        function SelectItem(obj, value) {
            for (var i = 0; i < obj.options.length; i++) {
                if (obj.options[i].value == value) {
                    obj.options[i].selected = true;
                    return true;
                }
            }
        }
        $.each(opts.label, function (i, n) {
            var isShowDiv = "style='display:none;'";
            if (n.gtitle != "")
                isShowDiv = "";
          
            html += '<div style="text-align:left;padding-left:5px;"><span class="methodgroupname" ' + isShowDiv + '>&nbsp;&nbsp;' + n.gtitle + '：</span><br/>';
            $.each(n.label, function (ii, nn) {
                if (typeof (nn) == 'object') {
                    if (ii > 0 && ii % 10 == 0) {//4个小标签一换行
                        html += '</div><div>';
                    }
                    if (i == 0 && ii == 0) {//第一个标签自动选择
                        set_check_title(n.gtitle + "-" + nn.desc);
                        html += '<label ' + isShowDiv + ' tag=' + n.gtitle + ' for="smalllabel_' + i + '_' + ii + '" id="smalllabel_' + i + '_' + ii + '" value="' + i + '-' + ii + '" class="com_btn"><span>' + nn.desc + '</span></label>&nbsp;';
                        if (nn.methoddesc.length > 0) {
                            $($.lt_id_data.id_methoddesc).html(nn.methoddesc).parent().show();
                        } else {
                            $($.lt_id_data.id_methoddesc).parent().hide();
                        }

                        if (nn.methodexample && nn.methodexample.length > 0) {
                            $($.lt_id_data.id_methodexample).show();
                            $($.lt_id_data.id_examplediv).html(nn.methodexample);
                        } else {
                            $($.lt_id_data.id_methodexample).hide();
                            $($.lt_id_data.id_examplediv).html("");
                        }

                        if (nn.methodhelp && nn.methodhelp.length > 0) {
                            $($.lt_id_data.id_helpdiv).html(nn.methodhelp);
                        } else {
                            $($.lt_id_data.id_helpdiv).html("");
                        }

                        if (nn.methodcount && nn.methodcount.length > 0) {
                            $($.lt_id_data.id_count).show();
                            $($.lt_id_data.id_countdiv).html(nn.methodcount);
                        } else {
                            $($.lt_id_data.id_methodcount).hide();
                            $($.lt_id_data.id_countdiv).html("");
                        }

                        if (nn.ifrandom && nn.ifrandom > 0) {
                            $($.lt_id_data.id_random_area).html('<input class="lt_random_bets_1 jx_button_90x26" title="机选1注" type="button" value="机选1注"  /><input class="lt_random_bets_5 jx_button_90x26" title="机选5注" type="button" value="机选5注"  /><input class="lt_random_bets_10 jx_button_90x26" title="机选10注" type="button" value="机选10注"  /><input type="hidden" id="randomcos" value="' + nn.randomcos + '" ><input type="hidden" id="randomcosvalue" value="' + nn.randomcosvalue + '">');
                            $($.lt_id_data.id_random_area).show();
                        } else {
                            $($.lt_id_data.id_random_area).hide();
                        }

                        lt_selcountback();//选号区的统计归零
                        $.lt_method_data = {
                            methodid: nn.methodid,
                            title: opts.title,
                            name: nn.name,
                            str: nn.show_str,
                            ifrandom: nn.ifrandom,
                            randomcos: nn.randomcos,
                            randomcosvalue: nn.randomcosvalue,
                            prize: nn.prize,
                            nfdprize: nn.nfdprize,
                            modes: $.lt_method_data.modes ? $.lt_method_data.modes : {},
                            sp: nn.code_sp,
                            maxcodecount: nn.maxcodecount,
                            defaultposition: nn.defaultposition
                        };


                        $($.lt_id_data.id_selector).lt_selectarea(nn.selectarea);//生成选号界面
                        //生成模式选择

                        selmodes = getCookie("modes");
                        //*//SELECT框就会用到，单选框就没用到了。
                        if (is_select)
                            $("#lt_project_modes").empty();
                        $.each(nn.modes, function (j, m) {
                            $.lt_method_data.modes[m.modeid] = {name: m.name, rate: Number(m.rate)};
                            if (is_select)
                                addItem($("#lt_project_modes")[0], '' + m.name + '', m.modeid);
                        });
                        if (is_select)
                            SelectItem($("#lt_project_modes")[0], selmodes);
                        if ((typeof (nn.nfdprize) != "undefined") && (nn.nfdprize.levs != "") && (typeof (nn.nfdprize.levs) != "undefined")) {
                            $nfdhtml = '<select name="pmode" id="pmode" style="height:22px; line-height:22px; font-size:12px; border:1px solid #aaaaaa;color:#000;">';
                            $nfdhtml += '<option value ="1" >奖金' + nn.nfdprize.defaultprize + "-" + nn.nfdprize.userdiffpoint + '%</option>';
                            $nfdhtml += '<option value ="2" selected="selected" >奖金' + nn.nfdprize.levs + '-0%</option>';
                            $("#nfdprize").html($nfdhtml);
                            $("#wrapshow").css("display", 'block');
                            //添加绑定记录用户返金模式功能,自动选择已选过的模式。
                            var pmode_value = getCookie("pmode_selected_value");
                            if (pmode_value)
                            {
                                $("#pmode").val(pmode_value);
                            } else {
                                $("#pmode").val("2");
                                setCookie("pmode_selected_value", 2);
                            }
                            $("#pmode").change(function () {
                                setCookie("pmode_selected_value", $(this).val());
                            });
                        
                        }
                        else {
                            $("#wrapshow").css("display", 'none');
                            $("#nfdprize").html("");
                        }
//*/
                    } else {
                        html += '<label for="smalllabel_' + i + '_' + ii + '" id="smalllabel_' + i + '_' + ii + '" class="com_btn_h" value="' + i + '-' + ii + '"  tag=' + n.gtitle + '><span>' + nn.desc + '</span></label>&nbsp;';
                    }
                }
            });
        });
        html += '</div><input type="hidden">';
        $html = $('<div>' + html + '</div>');
        $($.lt_id_data.id_smalllabel).empty();
        $html.appendTo($.lt_id_data.id_smalllabel);
        //==============3 levels by floyd=============
        //if( opts.label.length == 1 ){
        //$($.lt_id_data.id_smalllabel).empty();
        //}
        //==============/3 levels by floyd=============
        //$("input[name='smalllabel']:first").attr("checked",true).data("ischecked",'yes');//第一个标签自动选择[兼容各种浏览器]
        $("#lt_samll_label label").click(function () {
        
            if ($(this).hasClass("com_btn") == true) {//如果已经选择则无任何动作
                return;
            }
            $("#lt_samll_label label").removeClass("com_btn").addClass("com_btn_h");
            $(this).addClass("com_btn").removeClass("com_btn_h");

            var sel_title = $(this).attr("tag") + "-" + $(this).children("span").html();

            close_trad_detaClose(sel_title);//隐藏弹出框

            var index = $(this).attr("value").split('-');
            if (opts.label[index[0]].label[index[1]].methoddesc.length > 0) {
                $($.lt_id_data.id_methoddesc).html(opts.label[index[0]].label[index[1]].methoddesc).parent().show();
            } else {
                $($.lt_id_data.id_methoddesc).parent().hide();
            }
            if (opts.label[index[0]].label[index[1]].methodhelp && opts.label[index[0]].label[index[1]].methodhelp.length > 0) {
                $($.lt_id_data.id_helpdiv).html(opts.label[index[0]].label[index[1]].methodhelp);
            } else {
                $($.lt_id_data.id_helpdiv).html("");
            }

            if (opts.label[index[0]].label[index[1]].methodexample && opts.label[index[0]].label[index[1]].methodexample.length > 0) {
                $($.lt_id_data.id_methodexample).show();
                $($.lt_id_data.id_examplediv).html(opts.label[index[0]].label[index[1]].methodexample);
            } else {
                $($.lt_id_data.id_methodexample).hide();
                $($.lt_id_data.id_examplediv).html("");
            }

            if (opts.label[index[0]].label[index[1]].methodcount && opts.label[index[0]].label[index[1]].methodcount.length > 0) {
                $($.lt_id_data.id_count).show();
                $($.lt_id_data.id_countdiv).html(opts.label[index[0]].label[index[1]].methodcount);
            } else {
                $($.lt_id_data.id_count).hide();
                $($.lt_id_data.id_countdiv).html("");
            }

            if (opts.label[index[0]].label[index[1]].ifrandom && opts.label[index[0]].label[index[1]].ifrandom > 0) {
                $($.lt_id_data.id_random_area).html('<input class="lt_random_bets_1 jx_button_90x26" title="机选1注" type="button" value="机选1注"  /><input class="lt_random_bets_5 jx_button_90x26" title="机选5注" type="button" value="机选5注"  /><input class="lt_random_bets_10 jx_button_90x26" title="机选10注" type="button" value="机选10注"  /><input type="hidden" id="randomcos" value="' + opts.label[index[0]].label[index[1]].randomcos + '" ><input type="hidden" id="randomcosvalue" value="' + opts.label[index[0]].label[index[1]].randomcosvalue + '">');
                $($.lt_id_data.id_random_area).show();
            } else {
                $($.lt_id_data.id_random_area).hide();
            }

            lt_selcountback();//选号区的统计归零
            $.lt_method_data = {
                methodid: opts.label[index[0]].label[index[1]].methodid,
                title: opts.title,
                name: opts.label[index[0]].label[index[1]].name,
                ifrandom: opts.label[index[0]].label[index[1]].ifrandom,
                randomcos: opts.label[index[0]].label[index[1]].randomcos,
                randomcosvalue: opts.label[index[0]].label[index[1]].randomcosvalue,
                str: opts.label[index[0]].label[index[1]].show_str,
                prize: opts.label[index[0]].label[index[1]].prize,
                nfdprize: opts.label[index[0]].label[index[1]].nfdprize,
                modes: $.lt_method_data.modes ? $.lt_method_data.modes : {},
                sp: opts.label[index[0]].label[index[1]].code_sp,
                maxcodecount: opts.label[index[0]].label[index[1]].maxcodecount,
                defaultposition: opts.label[index[0]].label[index[1]].defaultposition
            };

            $("input[name='smalllabel']").removeData("ischecked");
            $(this).data("ischecked", 'yes'); //标记为已选择

            if (typeof (opts.label[index[0]].label[index[1]].nfdprize.defaultprize) != 'undefined' && opts.label[index[0]].label[index[1]].nfdprize.levs != '') {
                $nfdhtml = '<select name="pmode" id="pmode" style="height:22px; line-height:22px; font-size:12px; border: 1px double #57114f;color:#760795;">';
                $nfdhtml += '<option value ="1" >奖金' + opts.label[index[0]].label[index[1]].nfdprize.defaultprize
                        + "-" + opts.label[index[0]].label[index[1]].nfdprize.userdiffpoint + '%</option>';
                //	 +opts.label[index[0]].label[index[1]].nfdprize.defaultprize+"-"opts.label[index[0]].label[index[1]].nfdprize.userdiffpoint+'%</option>';
                $nfdhtml += '<option value ="2" selected="selected" >奖金' + opts.label[index[0]].label[index[1]].nfdprize.levs + '-0%</option>';
                $("#nfdprize").html($nfdhtml);
                $("#wrapshow").css("display", 'block');
                //添加绑定记录用户返金模式功能,自动选择已选过的模式。
                var pmode_value = getCookie("pmode_selected_value");
                if (pmode_value != null)
                {
                    $("#pmode").val(pmode_value);
                }
                $("#pmode").change(function () {
                    setCookie("pmode_selected_value", $(this).val());
                });
                //2013-04-12  end-----------------------------------------------
            }
            else {
                $("#wrapshow").css("display", 'none');
                $("#nfdprize").html("");
            }
            $($.lt_id_data.id_selector).lt_selectarea(opts.label[index[0]].label[index[1]].selectarea);//生成选号界面
            //生成模式选择
            
            $("#lt_project_modes").empty();
            //$("#lt_project_modes")[0].options.length ==0;
            selmodes = getCookie("modes");
            $.each(opts.label[index[0]].label[index[1]].modes, function (j, m) {
                $.lt_method_data.modes[m.modeid] = {name: m.name, rate: Number(m.rate)};
                //modelhtml += '<option value="'+m.modeid+'" '+(selmodes==m.modeid ? 'selected="selected"' : '')+' >&nbsp;&nbsp;'+m.name+'&nbsp;&nbsp;</option>';
                if (is_select)
                    addItem($("#lt_project_modes")[0], '' + m.name + '', m.modeid);
            });
            if (is_select)
                SelectItem($("#lt_project_modes")[0], selmodes);
            
            $('#main', parent.document).height($('#main', parent.document).contents().find("body").height());//重设 iframe 高度
        });
        //---xy---//
        $('#main', parent.document).height($('#main', parent.document).contents().find("body").height());//重设 iframe 高度
    };

    var lt_selcountback = function () {

        //$($.lt_id_data.id_sel_times).val(1);
        $($.lt_id_data.id_sel_money).html(0);
        $($.lt_id_data.id_sel_num).html(0);
    };


    /*
     //清空追号方案
     //*/

    //倒计时
    $.fn.lt_timer = function (start, end, betend) {//服务器开始时间，服务器结束时间
        //scendtime
       
        var me = this;
        if ( start == "" || end==undefined || end == "" || betend=="") {
            $.lt_time_leave = 0;
            $.lt_time_end = 0;
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
            } : {day: 0, hour: 0, minute: 0, second: 0};
        }
        
        

        var timerno = window.setInterval(function () {

            if ($.lt_time_leave > 0 && ($.lt_time_leave % 20 == 0 || $.lt_time_leave == 20)) {//每隔30秒以及最后一分钟重新读取服务器时间
                //
                //alert($($.lt_id_data.id_cur_issue).html());
                console.info("****************"+$($.lt_id_data.id_cur_issue).html());
                $.ajax({
                    type: 'POST',
                    url:$.lt_inint_url, //"/page/Initial.aspx",
                    timeout: 30000,
                    data: "issue=" + $($.lt_id_data.id_cur_issue).html() + "&action=nowtime",
                    success: function (data) {//成功
                        data = parseInt(data, 10);
                        data = isNaN(data) ? 0 : data;
                        data = data <= 0 ? 0 : data;
                        $.lt_time_leave = data;
                    }

                });
            }

            /*倒计时倒数还有一分钟的时候播放 =============================================== */
            if ($.lt_time_leave <= 10) {
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
                    $.unblockUI({fadeInTime: 0, fadeOutTime: 0});
                    if ($($.lt_id_data.id_cur_issue).html() > '') {
                        //parent.$.alert(lot_lang.am_s15);
                        clearInterval(timerno);//清除time
                        $.lt_ontimeout();
                        $.lt_reset(false);//继续下一期
                        
                    } else {
                        parent.$.alert(lot_lang.am_s15_2);
                    }
                    //play_click('/Content/audio/playsound.mp3');
                    //计时准备开奖
                    var opendTimerno = setInterval(function () {
                      
                        if ($.lt_time_end <= 0) {
                            clearInterval(opendTimerno);//清除计时
                            //结束销售，开奖
                            $.lt_onend();
                        }
                        $.lt_time_end--;
                    }, 1000);
                }
            }
            var oDate = diff($.lt_time_leave--);
            $(me).html("" + (oDate.day > 0 ? oDate.day + (lot_lang.dec_s21) + " " : "") + "<span class=\"hour\">" + fftime(oDate.hour) + ":</span><span class=\"min\">" + fftime(oDate.minute) + ":</span><span class=\"sec\">" + fftime(oDate.second) + "</span>");
        }, 1000);

    };
    //根据投单完成和本期销售时间结束，进行重新更新整个投注界面

    $.lt_reset = function (iskeep) {
        if (iskeep && iskeep === true) {
            iskeep = true;
        } else {
            iskeep = false;
        }
        if ($.lt_time_leave <= 0) {//本期结束后的刷新
            //01:刷新选号区
            if (iskeep == false) {
                $(":radio:checked", $($.lt_id_data.id_smalllabel)).removeData("ischecked").click();
            }
            //02:刷新确认区
            if (iskeep == false) {
                $.lt_total_nums = 0;//总注数清零
                $.lt_total_money = 0;//总金额清零
                $.lt_trace_base = 0;//追号基础数据
                $.lt_same_code = [];//已在确认区的数据
                $($.lt_id_data.id_cf_num).html(0);//显示数据清零
                $($.lt_id_data.id_cf_money).html(0);//显示数据清零
                $($.lt_id_data.id_cf_content).children().empty();
                $($.lt_id_data.id_cf_count).html(0);
                $("#times").attr('selected');
            }
           
            /********/
            //重新生成并写入起始期内容
            var chtml = '';
            $.each($.lt_issues.today, function (i, n) {
                chtml += '<option value="' + n.issue + '">' + n.issue + (i ==0? lot_lang.dec_s7 : '') + '</option>';
            });
            var t = $.lt_issues.tomorrow.length - $.lt_issues.today.length;
            if (t > 0) {//如果当天的期数小于每天的固定期数则继续增加显示
                for (i = 0; i < t; i++) {
                    chtml += '<option value="' + $.lt_issues.tomorrow[i].issue + '">' + $.lt_issues.tomorrow[i].issue + '</option>';
                }
            }

            $("#lt_issue_start").empty();
            $(chtml).appendTo("#lt_issue_start");
            /***/
            $.lt_preissue = $.lt_curissue;
            var data = $.lt_issues.today.shift();
            //03:刷新当前期的信息 不刷新，
            $($.lt_id_data.id_cur_issue).html(data.issue);
            $($.lt_id_data.id_cur_end).html(data.scendtime);
            //04:重新开始计时
            $($.lt_id_data.id_count_down).lt_timer($.lt_curissue.scendtime, data.scendtime, data.endtime);

            
            //06:更新可追号期数
            t_count = $.lt_issues.tomorrow.length;
            $($.lt_id_data.id_tra_alct).html(t_count);
            //07:更新追号数据
            cleanTraceIssue();//清空追号区数据
            while (true) {//删除追号列表里已经过期的数据
                $j = $("tr:first", $("#lt_trace_issues_today"));
                if ($j.length <= 0) {
                    break;
                }
                if ($j.find(":checkbox").val() == data.issue) {
                    break;
                }
                $j.remove();
            }
            //更行当前期号
            $.lt_curissue = data;
            /********/
            //读取新数据刷新必须刷新的内容
            //$.ajax({
            //    type: 'POST',
            //    url: $.lt_lottery_basic_url,
            //    data: {"action":"getnowsalesissue"},
            //    success: function (data) {//成功
            //        var jsonData= JSON.parse(data);
            //        if (jsonData.Code != 0) {
            //            parent.$.alert(lot_lang.am_s16);
            //            return false;
            //        }
            //        data = jsonData.Data;
            //        //03:刷新当前期的信息
            //        $($.lt_id_data.id_cur_issue).html(data.IssueCode);
            //        $($.lt_id_data.id_cur_end).html(data.EndSaleTime);
            //        //04:重新开始计时
            //        $($.lt_id_data.id_count_down).lt_timer(data.LotteryTime, data.EndSaleTime);
            //        var l = $.lt_issues.today.length;
            //        //05:更新起始期
            //        while (true) {
            //            if (data.IssueCode == $.lt_issues.today.shift().issue) {
            //                break;
            //            } else {
            //                break;
            //            }
            //        }
            //        $.lt_issues.today.unshift({ issue: data.IssueCode, endtime: data.EndSaleTime });
            //        //重新生成并写入起始期内容
            //        var chtml = '';
            //        $.each($.lt_issues.today, function (i, n) {
            //            chtml += '<option value="' + n.issue + '">' + n.issue + (n.issue == data.IssueCode ? lot_lang.dec_s7 : '') + '</option>';
            //        });
            //        var t = $.lt_issues.tomorrow.length - $.lt_issues.today.length;
            //        if (t > 0) {//如果当天的期数小于每天的固定期数则继续增加显示
            //            for (i = 0; i < t; i++) {
            //                chtml += '<option value="' + $.lt_issues.tomorrow[i].issue + '">' + $.lt_issues.tomorrow[i].issue + '</option>';
            //            }
            //        }
                   
            //        $("#lt_issue_start").empty();
            //        $(chtml).appendTo("#lt_issue_start");
            //        //06:更新可追号期数
            //        t_count = $.lt_issues.tomorrow.length;
            //        $($.lt_id_data.id_tra_alct).html(t_count);
            //        //07:更新追号数据
            //        cleanTraceIssue();//清空追号区数据
            //        while (true) {//删除追号列表里已经过期的数据
            //            $j = $("tr:first", $("#lt_trace_issues_today"));
            //            if ($j.length <= 0) {
            //                break;
            //            }
            //            if ($j.find(":checkbox").val() == data.IssueCode) {
            //                break;
            //            }
            //            $j.remove();
            //        }
            //    },
            //    error: function () {//失败
            //        parent.$.alert(lot_lang.am_s16);
            //        cleanTraceIssue();//清空追号区数据
            //        return false;
            //    }
            //});
        } else {//提交表单成功后的刷新
            //01:刷新选号区
            if (iskeep == false) {
                $(":radio:checked", $($.lt_id_data.id_smalllabel)).removeData("ischecked").click();
            }
            //02:刷新确认区
            if (iskeep == false) {
                $.lt_total_nums = 0;//总注数清零
                $.lt_total_money = 0;//总金额清零
                $.lt_trace_base = 0;//追号基数
                $.lt_same_code = [];//已在确认区的数据
                $($.lt_id_data.id_cf_num).html(0);//显示数据清零
                $($.lt_id_data.id_cf_money).html(0);//显示数据清零
                $($.lt_id_data.id_cf_content).children().empty();
                $($.lt_id_data.id_cf_count).html(0);
            }
            //03:刷新追号区
            if (iskeep == false) {
                cleanTraceIssue();//清空追号区数据
            }
        }
    };
    //提交表单
    var ajaxSubmitAllow = true;
    $.fn.lt_ajaxSubmit = function () {
        
        var me = this;

        $(this).click(function () {
            //$($.lt_id_data.id_sel_insert)[0].click();//添加投注

            $.lt_submiting = true;//倒计时提示的主动权转移到这里
            var istrace = $($.lt_id_data.id_tra_ifb).attr("checked") == 'checked' ? 1 : 0;//是否追号
            //alert(istrace);
            if ($.lt_total_nums <= 0 || $.lt_total_money <= 0) {//检查是否有投注内容
                $.lt_submiting = false;
                parent.$.alert(lot_lang.am_s6);
                return;
            }
            if (istrace == true) {
                if ($.lt_trace_issue <= 0 || $.lt_trace_money <= 0) {//检查是否有追号内容
                    $.lt_submiting = false;
                    parent.$.alert(lot_lang.am_s20);
                    return;
                }
                var terr = "";
                $("input[name^='lt_trace_issues']:checked", $($.lt_id_data.id_tra_issues)).each(function () {
                    if (Number($(this).closest("tr").find("input[name^='lt_trace_times_']").val()) <= 0) {
                        terr += $(this).val() + '\n';
                    }
                });
                if (terr.length > 0) {//有错误倍数的奖期
                    $.lt_submiting = false;
                    parent.$.alert(lot_lang.am_s21.replace("[errorIssue]", terr));
                    return;
                }
            }
            if (istrace == true) {
                var msg = lot_lang.am_s14.replace("[count]", $.lt_trace_issue);
            } else {
                var msg = lot_lang.dec_s8.replace("[issue]", $("#lt_issue_start").val());
            }
            msg += '<div class="floatarea" style="height:150px;">';
            var modesmsg = [];
            var modes = 0;
            $.each($('tr', $($.lt_id_data.id_cf_content)), function (i, n) {
                modes = $(n).data('data').modes;
                if (modesmsg[modes] == undefined) {
                    modesmsg[modes] = [];
                }
                var itemValue = $("th", n).html().replace(lot_lang.dec_s5, "");
                var itemArray = itemValue.split(']');
                var lstct ="<span style='padding-left:20px;'>"+itemArray[0] + "]<span style='padding-left:20px;padding-right:20px;'>" + $.lt_method_data.modes[modes].name + "</span>" + itemArray[1] + "</span>\n";
                modesmsg[modes].push(lstct);
            });
            $.each(modesmsg, function (i, n) {
                if ($.lt_method_data.modes[i] != undefined && n != undefined && n.length > 0) {
                    msg +=n.join("");
                }
            });
            msg += '</div>';
            $.lt_trace_money = Math.round($.lt_trace_money * 1000) / 1000;
            var total = lot_lang.dec_s9 + '：' + (istrace == true ? $.lt_trace_money : $.lt_total_money) + ' ' + lot_lang.dec_s3 + "";
           // parent.$.confirm(msg,total, function () {//点确定[提交]

                if (checkTimeOut() == false) {//正式提交前再检查1下时间
                    $.lt_submiting = false;
                    return;
                }
                $("#lt_total_nums").val($.lt_total_nums);
                $("#lt_total_money").val($.lt_total_money);

                //解决瞬间提交2次的问题

                if (ajaxSubmitAllow) {
                    ajaxSubmitAllow = false;
                    ajaxSubmit();
                }
            //}, function () {//点取消
            //    $.lt_submiting = false;
            //    return checkTimeOut();
            //}, '', 400);
        });
        //检查时间是否结束，然后做处理
        function checkTimeOut() {
            if ($.lt_time_leave <= 0) {//结束
                if ($($.lt_id_data.id_cur_issue).html() > '') {
                    parent.$.confirm(lot_lang.am_s15, function () {//确定
                        $.lt_reset(false);
                        $.lt_ontimeout();
                    }, function () {//取消
                        $.lt_reset(true);
                        $.lt_ontimeout();
                    });
                } else {
                    parent.$.alert(lot_lang.am_s15_2);
                }
                return false;
            } else {
                return true;
            }
        }
        ;

        $($.lt_id_data.id_methodexample).hover(function () {
            if ($($.lt_id_data.id_examplediv).html().length > 2) {
                var offset = $(this).offset();
                var lft = 0;
                var vtp = 0;
                if ($($.lt_id_data.id_examplediv).html().length > 30) {
                    $($.lt_id_data.id_examplediv).css({"width": "300px"});
                } else {
                    $($.lt_id_data.id_examplediv).css({"width": ($.browser.msie ? "300px" : "auto")});
                }
                if (offset.left < $($.lt_id_data.id_examplediv).width()) {
                    lft = (offset.left + 64);
                    vtp = (offset.top + 10);
                    $($.lt_id_data.id_examplediv).css({ "left": lft + "px", "top": vtp + "px" }).show();
                } else {
                    lft = (offset.left - $($.lt_id_data.id_examplediv).width() + 100);
                    vtp = (offset.top + 25);
                    $($.lt_id_data.id_examplediv).css({ "left": lft + "px", "top": vtp + "px" }).show();
                }
                //获取 242
                $(".sanjiao").css({ "left": (lft + 236), "top": vtp + 3 }).show();
            }
        }, function () {
            $($.lt_id_data.id_examplediv).hide();
            $(".sanjiao").hide();
        });


        $($.lt_id_data.id_count).hover(function () {
            if ($($.lt_id_data.id_countdiv).html().length > 2) {
                var offset = $(this).offset();
                if ($($.lt_id_data.id_countdiv).html().length > 30) {
                    $($.lt_id_data.id_countdiv).css({"width": "300px"});
                } else {
                    $($.lt_id_data.id_countdiv).css({"width": ($.browser.msie ? "300px" : "auto")});
                }
                $($.lt_id_data.id_countdiv).css({"left": (offset.left + $(this).outerWidth()) + "px", "top": (offset.top - 20) + "px"}).show();
            }
        }, function () {
            $($.lt_id_data.id_countdiv).hide();
        });

        //ajax提交表单 sean
        function ajaxSubmit() {
            parent.$.blockUI({
                message: lot_lang.am_s22,
                overlayCSS: { backgroundColor: '#FFFFFF',  }
            });
          
           
            var form = $(me).closest("form");
            var dt = $(form).serialize();
       
           // dt = content;
            //alert(dt);
            //console.log($(form).serialize());
            var randomNum = Math.floor((Math.random() * 10000) + 1);
            $.ajax({
                type: 'POST',
                url: $.lt_betting_url,
                timeout: 600000,
                data: dt + "&randomNum=" + randomNum + "&action=htdbetdetail",
                success: function (data) {
                   parent.Ytg.common.user.refreshBalance();//刷新金额
                    //解决瞬间提交2次的问题
                    ajaxSubmitAllow = true;
                    $.unblockUI({ fadeInTime: 0, fadeOutTime: 0 });
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
                    } else if(jsonData.Code==1007){
                        parent.$.alert(lot_lang.am_s37);
                        return false;
                    } else if (jsonData.Code == 1009) {
                        alert("由于您长时间未操作,为确保安全,请重新登录！");
                        parent.window.location = "/login.html";
                    } else if (jsonData.Code == 1011) {
                        parent.$.alert(lot_lang.am_s38);
                        return false; 
                    } else if (jsonData.Code == 1002) {
                        //超出最大注数am_s39
                        parent.$.alert(lot_lang.am_s39);
                    } else if (jsonData.Code == 1003) {
                        parent.$.alert(lot_lang.am_s18);
                    } else if (jsonData.Code == 2001) {
                        parent.$.alert(lot_lang.am_s40);
                    }
                    else {
                        parent.$.alert(lot_lang.am_s16 + "[" + jsonData.Code + "]");
                        return false;
                    }
                },
                error: function () {
                    ajaxSubmitAllow = true;
                    $.lt_submiting = false;
                    $.unblockUI({fadeInTime: 0, fadeOutTime: 0});
                    parent.$.alert(lot_lang.am_s23, '', checkTimeOut);
                    /*全清功能*/
                    showClearAll();
                }

                
            });
        }
        ;

    };

})(jQuery);
