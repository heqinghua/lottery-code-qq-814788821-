
//琛ㄦ牸鐐瑰嚮璋冪敤鎺掑簭鏂规硶锛�
function orderby(orderby, order) {
    $("#pageform input[name='order']").remove();
    var orderby = '<input type="hidden" name="orderby" value="' + orderby + '"/>';
    var order = '<input type="hidden" name="order" value="' + order + '"/>';
    $("#pageform").append(orderby + order).submit();
    common.setCookie('asc_desc', 1);
}
/**
 * 鍏敤
 */
var common = {};

//璁剧疆cookie
common.setCookie = function (c_name, value, expiredays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + expiredays);
    document.cookie = c_name + "=" + escape(value) + ((expiredays == null) ? "" : ";expires=" + exdate.toGMTString());
};

//鑾峰彇cookie
common.getCookie = function (name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
    if (arr = document.cookie.match(reg)) {
        return (arr[2]);
    } else {
        return null;
    }
};

//娓呴櫎cookie  
common.clearCookie = function (name) {
    common.setCookie(name, "", -1);
};


function changball(i, t) {
    window.location.href = "index.php?a=caipiao&m=lm&type=" + t + "&c=" + i;
}

/*
* @des 鍊掕鏃舵彃浠�
*/
$.fn.countdown = function (opt) {
    var opts = $.extend({}, {
        val: +this.data('val'),
        d: this.find('.day'),
        h: this.find('.hour'),
        m: this.find('.minute'),
        s: this.find('.second'),
        ms: this.find('.millisecond'),
        callback: function () { }
    }, opt);
    return this.each(function () {
        var target = $(this);
        var _time = opts.val;
        var _tC = function (_time) {
            var __d = parseInt((_time / 3600) / 24);
            var __h = parseInt((_time / 3600) % 24);
            var __m = parseInt((_time % 3600) / 60);
            var __s = parseInt(_time % 60);
            __d < 10 ? __d = '0' + __d : __d;
            __h < 10 ? __h = '0' + __h : __h;
            __m < 10 ? __m = '0' + __m : __m;
            __s < 10 ? __s = '0' + __s : __s;
            opts.d.html(__d.toString());
            opts.h.html(__h.toString());
            opts.m.html(__m.toString());
            opts.s.html(__s.toString());
            if (_time <= 0) {
                clearInterval(_timeCount);
                opts.callback(target);
            }
        };
        var _nn = 10;
        var msFn = function (n) {
            opts.ms.html((n - 1).toString());
        };
        if (opts.ms.length) {
            setTimeout(function () {
                setInterval(function () { msFn(_nn); _nn--; if (_nn <= 0) { _nn = 10 } }, 100);
            }, 1000);
        }
        var _timeCount = setInterval(function () { _tC(_time); _time-- }, 1000);
    });
};


//ie placeholder鍏煎
; (function (window, document, $) {

    var isInputSupported = 'placeholder' in document.createElement('input'),
	    isTextareaSupported = 'placeholder' in document.createElement('textarea'),
	    prototype = $.fn,
	    valHooks = $.valHooks,
	    hooks,
	    placeholder;

    if (isInputSupported && isTextareaSupported) {

        placeholder = prototype.placeholder = function () {
            return this;
        };

        placeholder.input = placeholder.textarea = true;

    } else {

        placeholder = prototype.placeholder = function () {
            var $this = this;
            $this
				.filter((isInputSupported ? 'textarea' : ':input') + '[placeholder]')
				.not('.placeholder')
				.bind({
				    'focus.placeholder': clearPlaceholder,
				    'blur.placeholder': setPlaceholder
				})
				.data('placeholder-enabled', true)
				.trigger('blur.placeholder');
            return $this;
        };

        placeholder.input = isInputSupported;
        placeholder.textarea = isTextareaSupported;

        hooks = {
            'get': function (element) {
                var $element = $(element);
                return $element.data('placeholder-enabled') && $element.hasClass('placeholder') ? '' : element.value;
            },
            'set': function (element, value) {
                var $element = $(element);
                if (!$element.data('placeholder-enabled')) {
                    return element.value = value;
                }
                if (value == '') {
                    element.value = value;
                    // Issue #56: Setting the placeholder causes problems if the element continues to have focus.
                    if (element != document.activeElement) {
                        // We can't use `triggerHandler` here because of dummy text/password inputs :(
                        setPlaceholder.call(element);
                    }
                } else if ($element.hasClass('placeholder')) {
                    clearPlaceholder.call(element, true, value) || (element.value = value);
                } else {
                    element.value = value;
                }
                // `set` can not return `undefined`; see http://jsapi.info/jquery/1.7.1/val#L2363
                return $element;
            }
        };

        isInputSupported || (valHooks.input = hooks);
        isTextareaSupported || (valHooks.textarea = hooks);

        $(function () {
            // Look for forms
            $(document).delegate('form', 'submit.placeholder', function () {
                // Clear the placeholder values so they don't get submitted
                var $inputs = $('.placeholder', this).each(clearPlaceholder);
                setTimeout(function () {
                    $inputs.each(setPlaceholder);
                }, 10);
            });
        });

        // Clear placeholder values upon page reload
        $(window).bind('beforeunload.placeholder', function () {
            $('.placeholder').each(function () {
                this.value = '';
            });
        });

    }

    function args(elem) {
        // Return an object of element attributes
        var newAttrs = {},
		    rinlinejQuery = /^jQuery\d+$/;
        $.each(elem.attributes, function (i, attr) {
            if (attr.specified && !rinlinejQuery.test(attr.name)) {
                newAttrs[attr.name] = attr.value;
            }
        });
        return newAttrs;
    }

    function clearPlaceholder(event, value) {
        var input = this,
		    $input = $(input);
        if (input.value == $input.attr('placeholder') && $input.hasClass('placeholder')) {
            if ($input.data('placeholder-password')) {
                $input = $input.hide().next().show().attr('id', $input.removeAttr('id').data('placeholder-id'));
                // If `clearPlaceholder` was called from `$.valHooks.input.set`
                if (event === true) {
                    return $input[0].value = value;
                }
                $input.focus();
            } else {
                input.value = '';
                $input.removeClass('placeholder');
                input == document.activeElement && input.select();
            }
        }
    }

    function setPlaceholder() {
        var $replacement,
		    input = this,
		    $input = $(input),
		    $origInput = $input,
		    id = this.id;
        if (input.value == '') {
            if (input.type == 'password') {
                if (!$input.data('placeholder-textinput')) {
                    try {
                        $replacement = $input.clone().attr({ 'type': 'text' });
                    } catch (e) {
                        $replacement = $('<input>').attr($.extend(args(this), { 'type': 'text' }));
                    }
                    $replacement
						.removeAttr('name')
						.data({
						    'placeholder-password': true,
						    'placeholder-id': id
						})
						.bind('focus.placeholder', clearPlaceholder);
                    $input
						.data({
						    'placeholder-textinput': $replacement,
						    'placeholder-id': id
						})
						.before($replacement);
                }
                $input = $input.removeAttr('id').hide().prev().attr('id', id).show();
                // Note: `$input[0] != input` now!
            }
            $input.addClass('placeholder');
            $input[0].value = $input.attr('placeholder');
        } else {
            $input.removeClass('placeholder');
        }
    }

}(this, document, jQuery));


/*
鍏憡婊氬姩鎻掍欢
*/
$.fn.noticeRoller = function (opt) {
    var o = $.extend(opt, {
        space: 3000, //闂撮殧鏃堕棿
        speed: 200, //鍔ㄧ敾鏃堕棿
        mouseStop: true //榧犳爣鏀句笂鍘绘槸鍚﹀仠姝�
    });
    return this.each(function () {
        var self = $(this);
        var ul = self.find('>ul');
        if (self.find('li').length <= 1) {
            return false;
        }
        var doing = true;
        var timer;
        var todo = function () {
            if (!doing) { return; }
            var itemHeight = self.find('li').outerHeight(true);
            ul.animate({
                'margin-top': -itemHeight
            }, o.speed, function () {
                ul.css({
                    'margin-top': 0
                }).find('>li:first').appendTo(ul);
                timer = setTimeout(todo, o.space);
            });
        };
        timer = setTimeout(todo, o.space);
        o.mouseStop && self.on('mouseenter', function () {
            doing = false;
            clearTimeout(timer);
        }).on('mouseleave', function () {
            doing = true;
            timer = setTimeout(todo, o.space);
        });
    });
};


//------------------------------------------------------------------------------------




(function ($) {
    //璁＄畻宸﹁竟鑿滃崟鐨刪eight
    $('.menu_li').hover(function () {
        if ($(this).find(".menu_pop").is(":hidden")) {
            $(this).find(".menu_pop").show();
        } else {
            $(this).find(".menu_pop").hide();
        }
    });

    //鑷畾涔塻elect
    var $customs = $('.custom-select');
    $customs.on('mouseenter', function () {
        $(this).addClass('hover');
    }).on('mouseleave', function () {
        $(this).removeClass('hover');
    });
    $customs.find('.second a').on('click', function () {
        var custom = $(this).parent().parent('.custom-select');
        var val = $(this).html();
        custom.find('.text').val(val);
        custom.find('.init').html(val);
        custom.removeClass('hover');
    });
    //鑱婂ぉ鐣岄潰  
    $('#modMsg').on('click', '.head', function () {
        $(this).parent().toggleClass('zoom');
    }).on('click', '.item .cat', function () {
        $(this).parent().toggleClass('open');
    }).on('click', '.userlist li', function (e) {
        e.preventDefault();
        $('#modMsgChat').addClass('open');
    });
    $('#modMsgChat').on('click', '.icon-close', function () {
        $('#modMsgChat').removeClass('open');
    });
    //user info
    //	$('#userInfo .sec').on('click', function(){
    //		$('#userInfo').toggleClass('open_balance');								 
    //	});
    $('#show_qiandao').on('click', function () {
        $('#userInfo').toggleClass('open_qiandao');
    });
    //ie placeholder 鍏煎
    $('input[placeholder]').placeholder();

    //婊氬姩鍏憡
    $('#notice').noticeRoller();
    $('#txlist').noticeRoller();
    $('#zjlist').noticeRoller();
})(jQuery);


function iFrameHeight(o, h) { //楂樺害鑷€傚簲
    if (o == 'mainframe') {
        var h2 = $(top).scrollTop();
    }
    var iframe = document.getElementById(o);
    if (o == 'mainframe') {
        var x = (iframe.contentWindow.location.href);
        if (x.indexOf('login') > 0) {
            //common.clearCookie('nowurl');
        } else {
            common.setCookie('nowurl', x);
        }
    }
    if (iframe) {
        iframe.height = 0;
    }
    try {
        if (typeof h == "undefined") {
            var bHeight = iframe.contentWindow.document.body.scrollHeight;
            var dHeight = iframe.contentWindow.document.documentElement.scrollHeight;
            h = Math.max(bHeight, dHeight);

        }
        iframe.height = h;
    } catch (ex) { }
    if (o == 'mainframe') {
        $(top).scrollTop(h2);
    }
}
//鏍煎紡鍖栨诞鐐规暟褰㈠紡(鍙兘杈撳叆姝ｆ诞鐐规暟锛屼笖灏忔暟鐐瑰悗鍙兘璺熷洓浣�,鎬讳綋鏁板€间笉鑳藉ぇ浜�999999999999999鍏�15浣�:鏁板€�999鍏�)
function formatFloat(num) {
    num = num.replace(/^[^\d]/g, '');
    num = num.replace(/[^\d.]/g, '');
    num = num.replace(/\.{2,}/g, '.');
    num = num.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
    if (num.indexOf(".") != -1) {
        var data = num.split('.');
        num = (data[0].substr(0, 15)) + '.' + (data[1].substr(0, 4));
    }
    else {
        num = num.substr(0, 15);
    }
    return num;
}

function moneyFormat(num) {
    var sign = Number(num) < 0 ? "-" : "";
    num = num.toString();
    if (num.indexOf(".") == -1) {
        num = "" + num + ".00";
    }
    var data = num.split('.');
    data[0] = data[0].toString().replace(/[^\d]/g, "").substr(0, 15);
    data[0] = Number(data[0]).toString();
    var newnum = [];
    for (var i = data[0].length; i > 0; i -= 3) {
        newnum.unshift(data[0].substring(i, i - 3));
    }
    data[0] = newnum.join(",");
    data[1] = data[1].toString().substr(0, 4);
    return sign + "" + data[0] + "." + data[1];
}
function moneyFormatB(num) {
    var sign = Number(num) < 0 ? "-" : "";
    num = num.toString();
    if (num.indexOf(".") == -1) {
        num = "" + num + ".00";
    }
    var data = num.split('.');
    data[0] = data[0].toString().replace(/[^\d]/g, "").substr(0, 15);
    data[0] = Number(data[0]).toString();
    var newnum = [];
    for (var i = data[0].length; i > 0; i -= 3) {
        newnum.unshift(data[0].substring(i, i - 3));
    }
    data[0] = newnum.join(",");
    data[1] = data[1].toString().substr(0, 2);
    return sign + "" + data[0] + "." + data[1];
}
//鑷姩杞崲鏁板瓧閲戦涓哄ぇ灏忓啓涓枃瀛楃,杩斿洖澶у皬鍐欎腑鏂囧瓧绗︿覆锛屾渶澶у鐞嗗埌999鍏�
function changeMoneyToChinese(money) {
    var cnNums = new Array("闆�", "澹�", "璐�", "鍙�", "鑲�", "浼�", "闄�", "鏌�", "鎹�", "鐜�");	//姹夊瓧鐨勬暟瀛�
    var cnIntRadice = new Array("", "鎷�", "浣�", "浠�");	//鍩烘湰鍗曚綅
    var cnIntUnits = new Array("", "涓�", "浜�", "鍏�");	//瀵瑰簲鏁存暟閮ㄥ垎鎵╁睍鍗曚綅
    var cnDecUnits = new Array("瑙�", "鍒�", "鍘�", "姣�");	//瀵瑰簲灏忔暟閮ㄥ垎鍗曚綅
    var cnInteger = "鏁�";	//鏁存暟閲戦鏃跺悗闈㈣窡鐨勫瓧绗�
    var cnIntLast = "鍏�";	//鏁村瀷瀹屼互鍚庣殑鍗曚綅
    var maxNum = 999999999999999.9999;	//鏈€澶у鐞嗙殑鏁板瓧

    var IntegerNum;		//閲戦鏁存暟閮ㄥ垎
    var DecimalNum;		//閲戦灏忔暟閮ㄥ垎
    var ChineseStr = "";	//杈撳嚭鐨勪腑鏂囬噾棰濆瓧绗︿覆
    var parts;		//鍒嗙閲戦鍚庣敤鐨勬暟缁勶紝棰勫畾涔�
    var i, m;

    if (money == "") {
        return "";
    }

    money = parseFloat(money);
    //alert(money);
    if (money >= maxNum) {
        alert('瓒呭嚭鏈€澶у鐞嗘暟瀛�');
        return "";
    }
    if (money == 0) {
        ChineseStr = cnNums[0] + cnIntLast + cnInteger;
        //document.getElementById("show").value=ChineseStr;
        return ChineseStr;
    }
    money = money.toString(); //杞崲涓哄瓧绗︿覆
    if (money.indexOf(".") == -1) {
        IntegerNum = money;
        DecimalNum = '';
    }
    else {
        parts = money.split(".");
        IntegerNum = parts[0];
        DecimalNum = parts[1].substr(0, 4);
    }
    if (parseInt(IntegerNum, 10) > 0) {
        //鑾峰彇鏁村瀷閮ㄥ垎杞崲
        zeroCount = 0;
        IntLen = IntegerNum.length;
        for (i = 0; i < IntLen; i++) {
            n = IntegerNum.substr(i, 1);
            p = IntLen - i - 1;
            q = p / 4;
            m = p % 4;
            if (n == "0") {
                zeroCount++;
            }
            else {
                if (zeroCount > 0) {
                    ChineseStr += cnNums[0];
                }
                zeroCount = 0;	//褰掗浂
                ChineseStr += cnNums[parseInt(n)] + cnIntRadice[m];
            }
            if (m == 0 && zeroCount < 4) {
                ChineseStr += cnIntUnits[q];
            }
        }
        ChineseStr += cnIntLast;
        //鏁村瀷閮ㄥ垎澶勭悊瀹屾瘯
    }
    if (DecimalNum != '') {
        //灏忔暟閮ㄥ垎
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
    }
    else if (DecimalNum == '') {
        ChineseStr += cnInteger;
    }
    return ChineseStr;

}
function re_prize_auto(basic_mode, basic_prize, this_mode) {
    var new_prize = (parseFloat(basic_prize, 10) / parseFloat(basic_mode, 10)) * parseFloat(this_mode, 10);
    return new_prize.toFixed(3);
}
//褰╃エB 鏌ヨ璇︽儏
function lookGameBet(url) {
    layer.open({
        type: 2,
        title: 'NC褰╃ - 娉ㄥ崟璇︽儏',
        offset: '100px',
        //	  skin: 'uc-layer',
        shadeClose: true,
        area: ['690px', '508px'],
        content: url
    });
}
//褰╃エA 鏌ヨ璇︽儏
function showBetInfo(mid) {
    layer.open({
        type: 2,
        title: 'BA褰╃ - 娉ㄥ崟璇︽儏',
        offset: '100px',
        //		skin: 'uc-layer',
        shadeClose: true,
        area: ['720px', '380px'],
        content: 'index.php?a=caipiao&m=showBetInfo&mid=' + mid,
        success: function () { }
    });
}
//鍗曚釜鎾ゅ崟
function chedan(uid) {
    var url = 'index.php?a=betRecord&m=gameTask&uid=' + uid + '&active=lot_back';
    layer.confirm('纭畾瑕佹挙娑堟湰娉ㄥ崟鍚楋紵', { icon: 3, title: '鎻愮ず' }, function (index) {
        layer.open({
            type: 2,
            title: 'NC褰╃ - 娉ㄥ崟璇︽儏',
            offset: '100px',
            shadeClose: true,
            content: url,
            success: function () {
                parent.parent.GetNewMoney();
                parent.Ajax_get_buy();
            }
        });
    });
}
//涓€閿叏鎾ゅ崟
function chedanAll(uid) {
    var url = 'index.php?a=betRecord&m=gameTask&uid=' + uid + '&active=lot_back_all';
    layer.confirm('纭畾瑕佹挙娑堝叏閮ㄦ敞鍗曞悧锛�', { icon: 3, title: '鎻愮ず' }, function (index) {
        layer.open({
            type: 2,
            title: 'NC褰╃ - 娉ㄥ崟璇︽儏',
            offset: '100px',
            shadeClose: true,
            content: url,
            success: function () {
                parent.parent.GetNewMoney();
                parent.Ajax_get_buy();
            }
        });
    });
}
function Read_Ballimg(a) {
    $('#shows tr').each(function (i) {
        if (i == a) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
    $("#lmmenu a").removeClass('on');
    $("#lmmenu a:eq(" + a + ")").addClass('on');
    parent.iFrameHeight("lmframe");
}
$(function () {
    $('.game_table a.zoom').on('click', function () {
        if ($(this).hasClass('zhide')) {
            $(this).removeClass('zhide').html('鏀惰捣<i></i>');
        } else {
            $(this).addClass('zhide').html('灞曞紑<i></i>');
        }

        $(".shishi_bot").toggle();
        iFrameHeight("lmframe");
        iFrameHeight("ordersFrame");
    });

    $('#gameZoom').on('click', function () {
        $(this).hide();
        $('.lottery_log').show();
    });
    $('.lottery_log .zoom').on('click', function () {
        $('#gameZoom').show();
        $('.lottery_log').hide();
    });
})