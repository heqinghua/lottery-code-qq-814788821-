
//应用扩展
Ytg.common = {
    loading: function () {
        $("#basediv").append("<div class=\"confirmbc\" style='display:block;width:" + ($(window).width()) + "px;height:" + ($(document).height()) + "px;'></div>");
        var tp = $(window).height() / 2;
        var ld = "<div class=\"zeng_msgbox_layer_wrap\" id=\"q_Msgbox\" style=\"top:" + tp + "px; display: block;\"><span class=\"zeng_msgbox_layer\" id=\"mode_tips_v2\" style=\"z-index: 10000;\"><span class=\"gtl_ico_clear\"></span><span class=\"gtl_ico_loading\"></span>正在加载中，请稍后...<span class=\"gtl_end\"></span></span></div>";
        $("#basediv").append(ld);
    },
    cloading: function () {
        $(".confirmbc").remove();
        $("#q_Msgbox").remove();
    },
    confirm: function (msg, yes, no) {
        dialog({
            title: '提示',
            id: 'Confirm',
            content: msg,
            okValue: '确定',
            ok: function (here) {
                return yes.call(this, here);
            },
            cancelValue: '取消',
            cancel: function (here) {
                return no && no.call(this, here);
            }
        }).show();
    },
    alert: function (content, yes) {
        dialog({
            id: 'Alert',
            title: '提示',
            content: content,
            width: 280,
            fixed: true,
            okValue: '确定',
            ok: function (here) {
                return yes && yes.call(this, here);
            }
        }).showModal();

    },
    tips: function (content, time) {
    },
    write: function (id, fn) {
        if (!id)
            return;
        if (!$(document).data("write" + id)) {
            Ytg.api.get("write", {
                id: id
            }, function (json) {
                if (json.code == 1) {
                    $(document).data("write" + id, json.result);
                    fn(json.result);
                }
            });
        } else {
            fn($(document).data("write" + id));
        }
    },
    addFavorite: function (sURL, sTitle) {
        try {
            window.external.addFavorite(sURL, sTitle);
        }
        catch (e) {
            try {
                window.sidebar.addPanel(sTitle, sURL, "");
            }
            catch (e) {
                alert("加入收藏失败，请使用Ctrl+D进行添加");
            }
        }
    },
    setHome: function (obj, vrl) {
        try {
            obj.style.behavior = 'url(#default#homepage)';
            obj.setHomePage(vrl);
        }
        catch (e) {
            if (window.netscape) {
                try {
                    netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
                }
                catch (e) {
                    alert("此操作被浏览器拒绝！\n请在浏览器地址栏输入“about:config”并回车\n然后将 [signed.applets.codebase_principal_support]的值设置为'true',双击即可。");
                }
                var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch);
                prefs.setCharPref('browser.startup.homepage', vrl);
            }
        }
    },
    refreshCaptcha: function (ob) {
        var url = "/?captcha&rd=" + Math.random();
        if (typeof (ob) == 'object') {
            $(ob).attr("src", url);
        } else {
            $("#captchaimg").attr("src", url);
        }
    },
    user: {
        init: function () {
            try {
                window.setInterval(Ytg.common.user.refreshBalance, 40000);
            } catch (e) {
            }
        },
        validateNickName: function (str) {
            var patrn = /^.{2,10}$/g;
            if (patrn.exec(str)) {
                return true;
            } else {
                return false;
            }
        },
        validateUserPss: function (str) {
            var patrn = /^[0-9a-zA-Z]{6,16}$/;
            if (!patrn.exec(str)) {
                return false;
            }
            patrn = /^\d+$/;
            if (patrn.exec(str)) {
                return false;
            }
            patrn = /^[a-zA-Z]+$/;
            if (patrn.exec(str)) {
                return false;
            }
            patrn = /(.)\1{2,}/;
            if (patrn.exec(str)) {
                return false;
            }
            return true;
        },
        getPassword: function () {
            Ytg.common.layer.dialog = dialog({
                id: 'getPassword',
                title: "找回密码",
                url: "/?forgotpassword",
                padding: 4,
                fixed: true
            }).showModal();
        },
        login: function () {
            if (Ytg.common.user.info.user_id !== "") {
                Ytg.common.alert('您已登录！先请退出登录');
                return false;
            }
            var loginuser = $("#userName").val();
            var typepw = $("#password").val();
            var randnum = $("#code").val().toUpperCase();
            randnum = randnum.toUpperCase();
            if (loginuser == '' || loginuser == '用户名') {
                Ytg.common.alert('请填写用户名称');
                return false;
            }
            if (typepw == '') {
                Ytg.common.alert('请填写登录密码');
                return false;
            }
            if (randnum == '验证码' || randnum == '') {
                Ytg.common.alert('请填写数字验证码');
                return false;
            }
            var d = dialog({
                title: "协议与条款",
                fixed: true,
                content: document.getElementById('xieyi'),
                button: [
                    {
                        value: '同意',
                        callback: function () {
                            d.close();
                            var submitvc = $.md5(randnum);
                            var submitpw = $.md5(submitvc + Ytg.tools.reverse(Ytg.sha1($.md5(typepw))).substr(0, 32));
                            var url = '';
                            Ytg.api.post(url, "flag=login&username=" + loginuser + "&loginpass=" + submitpw + "&validcode=" + randnum + "&Submit=json", function (data) {
                                if (data.sMsg > '') {
                                    Ytg.common.alert(data.sMsg, function () {
                                        if (data.sError == 0) {
                                            if (typeof (data.aLinks[0]) != "undefined" && data.aLinks[0].url > '') {
                                                location.href = data.aLinks[0].url;
                                            }
                                        } else {
                                            Ytg.common.refreshCaptcha();
                                        }
                                    });

                                } else {
                                    if (typeof (data.aLinks[0]) != "undefined" && data.aLinks[0].url > '') {
                                        location.href = data.aLinks[0].url;
                                    }
                                }

                            }, 'json');
                        }
                    },
                    {
                        value: '不同意',
                        callback: function () {
                        }
                    }
                ]
            }).showModal();
        },
        refreshBalance: function () {
            
            if (Ytg.common.user.info.username == '') {
                return;
            }
            $.ajax({
                type: 'POST',
                url: '/page/Initial.aspx?action=userbalance',
                data: { "uid": Ytg.common.user.info.user_id},
                timeout: 10000, success: function (data) {
                    var jsonData = JSON.parse(data);                    
                    if (jsonData.Code == 0) {
                        //获取成功
                       parent.$("#refff").text(Ytg.tools.moneyFormat(jsonData.Data.UserAmt));
                    } else if (jsonData.Code == 1009) {
                        //$.alert("你的帐号已在别处登陆，你被强迫下线！", 1, function () {
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
    },
    notice: {
        filter: function (id) {
            Ytg.api.get("readnotice", {
                id: id, status: 2
            }, function (json) {
                if (json.code == 1) {
                    $(this).closest("tbody").fadeOut(function () {
                        if ($(this).siblings().length == 0)
                            $(".index_notice2").remove();
                        else
                            $(this).remove();
                    });
                } else {
                    // Ytg.dialog.notice(json.result);
                }
            }.bind(this));
        },
        check: function () {
            return;
            var nid = getCookie("alertnotice");
            $.ajax({
                type: 'POST',
                url: '',
                data: 'flag=notice',
                dataType: "json",
                timeout: 10000, success: function (data) {
                    if (data.length <= 0) {
                        return;
                    }
                    if (data.id == nid) {
                        return;
                    }
                    dialog({
                        id: 'notice',
                        title: data.subject,
                        content: data.content,
                        width: 660,
                        height: 400,
                        padding: 18,
                        okValue: '关闭',
                        ok: function () {
                            setCookie("alertnotice", data.id)
                        },
                        cancelDisplay: false,
                        cancel: function () {
                            setCookie("alertnotice", data.id)
                        }
                    }).show();
                },
                error: function () {

                }
            });
            setTimeout(arguments.callee.bind(this), 30000);
        }
    },
    layer: {
        dialog: null,
        init: function () {
            $(".show").bind("click", function () {
                flagShow = $(this).parents("li").index();
                var _this = this;
                Ytg.common.layer.dialog = dialog({
                    id: 'layer',
                    title: $(_this).attr("title"),
                    url: $(_this).attr("url-data"),
                    width: 960,
                    height: 560,
                    padding: 4,
                    fixed: true
                }).showModal();
            });
        },
        show: function (title, url) {
            Ytg.common.layer.dialog = dialog({
                id: 'layer',
                title: title,
                url: url,
                width: 960,
                height: 560,
                padding: 4,
                fixed: true
            }).showModal();
        },
        setTitle: function (title) {
            if (parent.Ytg.common.layer.dialog == null) {
                return;
            }
            parent.Ytg.common.layer.dialog.title(title);
        },
        SetWinHeight: function (obj) {
            var win = obj;
            if (document.getElementById) {
                if (win && !window.opera) {
                    if (win.contentDocument && win.contentDocument.body.offsetHeight)
                        win.height = win.contentDocument.body.offsetHeight;
                    else if (win.Document && win.Document.body.scrollHeight)
                        win.height = win.Document.body.scrollHeight;
                }
            }
        }, SetIframeHeight: function (flag) {
            if (flag == 1) {
                var h = $("#xubox_iframe").contents().find("#subContent_bet_re").contents().find("#mainFrame").contents().height();
                $("#xubox_iframe").contents().find("#subContent_bet_re").contents().find("#mainFrame").height(h);
            } else if (flag == 2) {
                $("#xubox_iframe").contents().find("#subContent_bet_re").contents().find("#mainFrame").height("400");
            } else {
                $("#xubox_iframe").contents().find("#subContent_bet_re").contents().find("#mainFrame").height("");
            }
            Ytg.common.layer.SetWinHeight(document.getElementById('mainFrame'));
        }
    },
    goBack: function () {
        window.history.back();
    },
    //通用初始化
    init: function () {
        //初始引导
        Ytg.common.user.init();
        Ytg.common.layer.init();
        Ytg.common.notice.check();

    },
   LottTool: {
        GetState: function (item) {
     
            if (item.BetCode.indexOf("c") != -1) {
                if (item.Stauts == 0)
                    return "10_";//正在进行
                else if (item.Stauts == 1)
                    return "11_";//已完成
                else if (item.Stauts == 2)
                    return "4_";
            }
            return item.Stauts + "_";
        },
        GetStateContent: function (text) {
            if (text.toString().indexOf("_") != -1)
            {

                //1 已中奖、2 未中奖、3 未开奖、4 已撤单
                var color="";
                switch (text)
                {
                    case "0_":
                        text = "正在进行";
                        color = "#000";
                        break;
                    case "1_":
                        text = "已中奖";
                        color = "#cd0228";
                        break;
                    case "2_":
                        text = "未中奖";
                        color = "#0032b8";
                        break;
                        case "5_":
                    text = "系统撤单";
                        color = "#0032b8";
                        break;
                    case "3_":
                        text = "未开奖";
                        color="#000";
                        break;
                    case "4_":
                        text = "本人撤单";
                        color = "#1ea600";
                        break;
                    case "10_":
                        text = "正在进行";
                        color="#000";
                        break;
                    case "11_":
                        text = "已完成";
                        color = "#cd0228";
                        break;
                }
            }

            return '<span style="color:' + color + '">' + text + '</span>';
        },
        ShowBetContent: function (betContent,showAll) {//内容格式化
            if (betContent == "")
                return;
            if (betContent.indexOf('_') != -1) {
                betContent = betContent.split('_')[0];
            }
            var newContent = "";
            if (betContent.indexOf("-") != -1) {
                var array = betContent.split(',');
                for (var i = 0; i < array.length; i++) {
                    var s = array[i];
                    var sArray = s.split('-');
                    for (var j = 0; j < sArray.length; j++) {
                        var sv = sArray[j];
                        if (sv=="" || sv==undefined) continue;
                        newContent += Ytg.common.SpecialConvert.ConvertTo(parseInt("-" + sv));
                       // if(j+1!=sArray.length)
                    }
                    newContent += ",";
                }
                //if (newContent[newContent.length-1]==",")
                //    newContent = newContent.substring(0, newContent.Length - 1);
            }
            else {
                newContent = betContent;
            }
            if (showAll != undefined) {
                return newContent;
            }
            else {
                var maxLength = newContent.length > 80 ? 80 : newContent.length;
                if (newContent.length > 80)
                    return newContent.substring(0, maxLength) + "......";
                else
                    return newContent;
            }
            return "";
        }
    },
    SpecialConvert: {
        ConvertTo: function (value) {
            var numValue = "";
            switch (value)
            {
                case Global.BaoZi:
                    numValue = "豹子";
                    break;
                case Global.DuiZi:
                    numValue = "对子";
                    break;
                case Global.ShunZi:
                    numValue = "顺子";
                    break;
                case Global.Da:
                    numValue = "大";
                    break;
                case Global.Xiao:
                    numValue = "小";
                    break;
                case Global.Dan:
                    numValue = "单";
                    break;
                case Global.Shuang:
                    numValue = "双";
                    break;
                case Global.WuDanLinShuang:
                    numValue = "5单0双";
                    break;
                case Global.SiDanYiShuang:
                    numValue = "4单1双";
                    break;
                case Global.SanDanErShuang:
                    numValue = "3单2双";
                    break;
                case Global.ErDanSanShuang:
                    numValue = "2单3双";
                    break;
                case Global.YiDanSiShuang:
                    numValue = "1单4双";
                    break;
                case Global.LinDanWuShuang:
                    numValue = "0单5双";
                    break;
                case Global.Shang :
                    numValue = "上";
                    break;
                case Global.Zhong:
                    numValue = "中";
                    break;
                case Global.Xia:
                    numValue = "下";
                    break;
                case Global.Ji:
                    numValue = "奇";
                    break;
                case Global.He:
                    numValue = "和";
                    break;
                case Global.Or:
                    numValue = "偶";
                    break;
                case Global.DaDan :
                    numValue = "大.单";
                    break;
                case Global.DaShuang:
                    numValue = "大.双";
                    break;
                case Global.XiaoDan:
                    numValue = "小.单";
                    break;
                case Global.XiaoShuang:
                    numValue = "小.双";
                    break;
            }
            return numValue;
        },
        AmtTradeType: function (value) {
            var strValue = "其他";
            switch (value) {
                case 1: strValue = "用户充值"; break;
                case 2: strValue = "用户提现"; break;
                case 3: strValue = "投注扣款"; break;
                case 4: strValue = "追号扣款"; break;
                case 5: strValue = "追号返款"; break;
                case 6: strValue = "游戏返点"; break;
                case 7: strValue = "奖金派送"; break;
                case 8: strValue = "撤单返款"; break;
                case 9: strValue = "系统撤单"; break;
                case 10: strValue = "撤销返点"; break;
                case 11: strValue = "撤销派奖"; break;
                case 12: strValue = "充值扣费"; break;
                case 13: strValue = "上级充值"; break;
                case 14: strValue = "活动礼金"; break;
                case 15: strValue = "分红"; break;
                case 16: strValue = "提现失败"; break;
                case 17: strValue = "撤销提现"; break;
                case 18: strValue = "满赠活动"; break;
                case 19: strValue = "签到有你"; break;
                case 20: strValue = "注册活动"; break;
                case 21: strValue = "充值活动"; break;
                case 22: strValue = "佣金大返利"; break;
                case 23: strValue = "幸运大转盘"; break;
                case 24: strValue = "系统充值"; break;
                case 25: strValue = "投注送礼包"; break;
                case 26: strValue = "分红扣费"; break;
                    
            }
            return strValue;
        }
    }
};
function checkWithdraw(obj, chineseid, maxnum) {
    obj.value = formatFloat(obj.value);
    if (parseFloat(obj.value) > parseFloat(maxnum)) {
        alert("输入金额超出了可用余额");
        obj.value = maxnum;
    }
    $("#" + chineseid).html(changeMoneyToChinese(obj.value));
}

function postdata(data, contr, act, formobj) {
    var acount, postdata = "";
    acount = data.length;

    for (var i = 0; i < acount; i++) {
        postdata += data[i] + "=" + formobj.elements[data[i]].value + "&";
    }

    $.ajax({
        type: "POST",
        url: "?controller=" + contr + "&action=" + act + "&ajax=1",
        processData: false, data: postdata,
        success: function (data) {
            $("#contentBox").html(data);
        }
    });
}

function checkemailWithdraw(obj, chineseid, maxnum) {
    obj.value = formatFloat(obj.value);
    if (parseFloat(obj.value) > parseFloat(maxnum)) {
        $.alert("您的充值金额已经超出规定限额");
        obj.value = maxnum;
    }
    $("#" + chineseid).html(changeMoneyToChinese(obj.value));
}
function formatFloat(num) {
    num = num.replace(/^[^\d]/g, '');
    num = num.replace(/[^\d.]/g, '');
    num = num.replace(/\.{2,}/g, '.');
    num = num.replace(/^[0].*/g, '');
    num = num.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
    if (num.indexOf(".") != -1) {
        var data = num.split('.');
        num = (data[0].substr(0, 15)) + '.' + (data[1].substr(0, 2));
    } else {
        num = num.substr(0, 15);
    }
    return num;
}
//自动转换数字金额为大小写中文字符,返回大小写中文字符串，最大处理到999兆
function changeMoneyToChinese(money) {
    var cnNums = new Array("零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖");	//汉字的数字     
    var cnIntRadice = new Array("", "拾", "佰", "仟");	//基本单位
    var cnIntUnits = new Array("", "万", "亿", "兆");	//对应整数部分扩展单位     
    var cnDecUnits = new Array("角", "分", "毫", "厘");	//对应小数部分单位
    var cnInteger = "整";	//整数金额时后面跟的字符
    var cnIntLast = "元";	//整型完以后的单位
    var maxNum = 999999999999999.9999;	//最大处理的数字

    var IntegerNum;		//金额整数部分
    var DecimalNum;		//金额小数部分
    var ChineseStr = "";	//输出的中文金额字符串
    var parts;		//分离金额后用的数组，预定义

    if (money == "") {
        return "";
    }

    money = parseFloat(money);
    //alert(money);
    if (money >= maxNum) {
        $.alert('超出最大处理数字');
        return "";
    }
    if (money == 0) {
        ChineseStr = cnNums[0] + cnIntLast + cnInteger;
        //document.getElementById("show").value=ChineseStr;
        return ChineseStr;
    }
    money = money.toString(); //转换为字符串
    if (money.indexOf(".") == -1) {
        IntegerNum = money;
        DecimalNum = '';
    } else {
        parts = money.split(".");
        IntegerNum = parts[0];
        DecimalNum = parts[1].substr(0, 4);
    }
    if (parseInt(IntegerNum, 10) > 0) {//获取整型部分转换
        zeroCount = 0;
        IntLen = IntegerNum.length;
        for (i = 0; i < IntLen; i++) {
            n = IntegerNum.substr(i, 1);
            p = IntLen - i - 1;
            q = p / 4;
            m = p % 4;
            if (n == "0") {
                zeroCount++;
            } else {
                if (zeroCount > 0) {
                    ChineseStr += cnNums[0];
                }
                zeroCount = 0;	//归零
                ChineseStr += cnNums[parseInt(n)] + cnIntRadice[m];
            }
            if (m == 0 && zeroCount < 4) {
                ChineseStr += cnIntUnits[q];
            }
        }
        ChineseStr += cnIntLast;
        //整型部分处理完毕
    }
    if (DecimalNum != '') {//小数部分
        decLen = DecimalNum.length;
        for (i = 0; i < decLen; i++) {
            n = DecimalNum.substr(i, 1);
            if (n != '0') {
                ChineseStr += cnNums[Number(n)] + cnDecUnits[i];
            }
        }
    }
    if (ChineseStr == '') {
        ChineseStr += cnNums[0] + cnIntLast + cnInteger;
    } else if (DecimalNum == '') {
        ChineseStr += cnInteger;
    }
    return ChineseStr;
}
function show_no(id) {
    $("#code_" + id).show("slow");
}

function show_nocode(id) {
    $("#ncode_" + id).show("slow");
}
function close_no(id) {
    $("#code_" + id).hide("slow");
}
function nclose_no(id) {
    $("#ncode_" + id).hide("slow");
}
function clearNoNum(obj) {
    var myregexp = /\d+\.?\d?/;
    var match = myregexp.exec(obj.value);
    if (match != null) {
        obj.value = match[0];
    } else {
        obj.value = "";
    }
}

function checkNum(obj) {
    //为了去除最后一个.
    obj.value = obj.value.replace(/\.$/g, ".0");
}

/*
start
数字组合
@param nu 数组
@param groupl 需要组合几位数字
var arr = group([[1,2],3,4,5],4);
var arr2 = group([1,2,3,4],3);
*/
function group(nu, groupl, result) {

    var result = result ? result : [];
    var nul = nu.length;
    var outloopl = nul - groupl;

    var nuc = nu.slice(0);
    var item = nuc.shift();
    item = item.constructor === Array ? item : [item];

    (function func(item, nuc) {
        var itemc;
        var nucc = nuc.slice(0);
        var margin = groupl - item.length

        if (margin == 0) {
            result.push(item);
            return;
        }
        if (margin == 1) {
            for (var j in nuc) {
                itemc = item.slice(0);
                itemc.push(nuc[j]);
                result.push(itemc);
            }
        }
        if (margin > 1) {
            itemc = item.slice(0);
            itemc.push(nucc.shift());
            func(itemc, nucc);

            if (item.length + nucc.length >= groupl) {
                func(item, nucc);
            }
        }

    })(item, nuc);

    if (nuc.length >= groupl) {
        return group(nuc, groupl, result);
    } else {
        return result;
    }
}
/*end*/
$(function () {
    $(".ltable tr:nth-child(odd)").addClass("odd_bg"); //隔行变色
    //$(".rule-single-checkbox").ruleSingleCheckbox();
    //$(".rule-multi-checkbox").ruleMultiCheckbox();
    //$(".rule-multi-radio").ruleMultiRadio();
    $(".rule-single-select").ruleSingleSelect();
    //$(".rule-multi-porp").ruleMultiPorp();
});
//全选取消按钮函数
function checkAll(chkobj) {
    if ($(chkobj).text() == "全选") {
        $(chkobj).children("span").text("取消");
        $(".checkall input:enabled").prop("checked", true);
    } else {
        $(chkobj).children("span").text("全选");
        $(".checkall input:enabled").prop("checked", false);
    }
}
//Tab控制函数
function tabs(tabObj) {
    var tabNum = $(tabObj).parent().index("li")
    //设置点击后的切换样式
    $(tabObj).parent().parent().find("li a").removeClass("selected");
    $(tabObj).addClass("selected");
    //根据参数决定显示内容
    $(".tab-content").hide();
    $(".tab-content").eq(tabNum).show();
}

//===========================工具类函数============================
//只允许输入数字
function checkNumber(e) {
    if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) {  //FF 
        if (!((e.which >= 48 && e.which <= 57) || (e.which >= 96 && e.which <= 105) || (e.which == 8) || (e.which == 46)))
            return false;
    } else {
        if (!((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 96 && event.keyCode <= 105) || (event.keyCode == 8) || (event.keyCode == 46)))
            event.returnValue = false;
    }
}

//单选下拉框
$.fn.ruleSingleSelect = function () {
    var singleSelect = function (parentObj) {
        parentObj.addClass("single-select"); //添加样式
        parentObj.children().hide(); //隐藏内容
        var divObj = $('<div class="boxwrap"></div>').prependTo(parentObj); //前插入一个DIV
        //创建元素
        var titObj = $('<a class="select-tit" href="javascript:;"><span></span><i></i></a>').appendTo(divObj);
        var itemObj = $('<div class="select-items"><ul></ul></div>').appendTo(divObj);
        var arrowObj = $('<i class="arrow"></i>').appendTo(divObj);
        var selectObj = parentObj.find("select").eq(0); //取得select对象
        //遍历option选项
        selectObj.find("option").each(function (i) {
            var indexNum = selectObj.find("option").index(this); //当前索引
            var liObj = $('<li>' + $(this).text() + '</li>').appendTo(itemObj.find("ul")); //创建LI
            if ($(this).prop("selected") == true) {
                liObj.addClass("selected");
                titObj.find("span").text($(this).text());
            }
            //检查控件是否启用
            if ($(this).prop("disabled") == true) {
                liObj.css("cursor", "default");
                return;
            }
            //绑定事件
            liObj.click(function () {
                $(this).siblings().removeClass("selected");
                $(this).addClass("selected"); //添加选中样式
                selectObj.find("option").prop("selected", false);
                selectObj.find("option").eq(indexNum).prop("selected", true); //赋值给对应的option
                titObj.find("span").text($(this).text()); //赋值选中值
                arrowObj.hide();
                itemObj.hide(); //隐藏下拉框
                selectObj.trigger("change"); //触发select的onchange事件
                //alert(selectObj.find("option:selected").text());
            });
        });
        //设置样式
        //检查控件是否启用
        if (selectObj.prop("disabled") == true) {
            titObj.css("cursor", "default");
            return;
        }
        //绑定单击事件
        titObj.click(function (e) {
            e.stopPropagation();
            if (itemObj.is(":hidden")) {
                //隐藏其它的下位框菜单
                $(".single-select .select-items").hide();
                $(".single-select .arrow").hide();
                //位于其它无素的上面
                arrowObj.css("z-index", "1");
                itemObj.css("z-index", "1");
                //显示下拉框
                arrowObj.show();
                itemObj.show();
            } else {
                //位于其它无素的上面
                arrowObj.css("z-index", "");
                itemObj.css("z-index", "");
                //隐藏下拉框
                arrowObj.hide();
                itemObj.hide();
            }
        });
        //绑定页面点击事件
        $(document).click(function (e) {
            selectObj.trigger("blur"); //触发select的onblure事件
            arrowObj.hide();
            itemObj.hide(); //隐藏下拉框
        });
    };
    return $(this).each(function () {
        singleSelect($(this));
    });
}
//
$.fn.Empty = function (colspan) {
    $(this).append("<tr><td align=\"center\" colspan=\"" + colspan + "\">暂无记录</td></tr>");
}

function changePalyName(typeName,radioName) {
    var key = typeName + radioName;
    
    if (key == "" || key == "null")
        return "";
    try{
        key = key.replace("）", "").replace("（", "");
        var result = eval("playTypeData." + key);
        if (result == undefined)
            return typeName + radioName;
        return result;
    } catch (ex) {
    }
    return key;
}
/**转换11选5显示效果*/
function parseDingWeiDanShow(content) {
    if (content == '')
        return;
    var array = content.split('_');
    return array[0];
}
function getPostionName(postion,content) {
    var is11x5=false;
    if (content != undefined) {
        var sd = content.split(',')[0];
        if (sd != undefined && sd.length == 2)
            is11x5 = true;
    }
    var posStr="定位胆";
    switch (postion) {
        case "1":
            posStr = is11x5?"第一位":"万位";
            break;
        case "2":
            posStr = is11x5 ? "第二位" : "千位";
            break;
        case "3":
            posStr = is11x5 ? "第三位" : "百位";
            break;
        case "4":
            posStr = "十位";
            break;
        case "5":
            posStr = "个位";
            break;
    }
    return posStr;
}