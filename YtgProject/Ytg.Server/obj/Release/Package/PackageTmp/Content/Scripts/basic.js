/// <reference path="jquery-1.3.2-vsdoc2.js" />
//基础框架
Ytg = $.extend(Ytg, {
    module: {
        "core.template": "template-min",
        "core.flash": "swfobject-min",
        "core.widget": "widget",
        "core.form": "formValidator",
        "core.overlay": "jquery.colorbox",
        "tools.addthis": "addthis-jq"
    },
    _loadModule: [],
    _loadedModule: [],
    init: function (module, fn) {
        if (arguments.length == 1 && module.constructor == Function) {
            fn = module;
            module = "";
        }
        var flag = [],
                timer = 0,
                overtime = 10, //超时秒数
                arr_module = module.split(",");
        var self = this;
        if (module) {
            $.each(arr_module, function (n, item) {
                if (self._loadModule.unique().include(item))
                    return;
                self._loadModule.push(item);
                if (!self._loadedModule.include(item)) {
                    self.include(self.RESOURCEURL + "/js/" + self.module[item] + ".js", function () {
                        switch (item) {
                            case "core.form":
                                $.extend(true, $.formValidator.settings, {
                                    alertmessage: false,
                                    autotip: true,
                                    errorfocus: false,
                                    submitonce: false
                                });
                                self._loadedModule.push(item);
                                self.include(self.RESOURCEURL + "/style/common/validatorAuto.css");
                                break;
                            case "tools.addthis":
                                self.include(self.RESOURCEURL + "/style/common/addthis.css");
                                self._loadedModule.push(item);
                                break;
                            case "tools.imgareaselect":
                                self.include(self.RESOURCEURL + "/style/common/imgareaselect-default.css");
                                self._loadedModule.push(item);
                                break;
                            default:
                                self._loadedModule.push(item);
                                break;
                        }
                    });
                }
            });
        }
        var checkInterval = setInterval(function () {
            if (this._loadedModule.length == $.merge(this._loadModule, this._loadedModule).unique().length || timer / 10 > overtime) {
                clearInterval(checkInterval);
                typeof fn == "function" ? fn() : eval(fn);
            }
            timer++;
        }.bind(this), 100);
    },
    include: function (url, callback) {
        var afile = url.toLowerCase().replace(/^\s|\s$/g, "").match(/([^\/\\]+)\.(\w+)$/);
        if (!afile)
            return false;
        switch (afile[2]) {
            case "css":
                var el = $('<link rel="stylesheet" id="' + afile[1] + '" type="text/css" />').appendTo("head").attr("href", url);
                if ($.browser.msie) {
                    el.load(function () {
                        if (typeof callback == 'function')
                            callback();
                    });
                } else {
                    var i = 0;
                    var checkInterval = setInterval(function () {
                        if ($("head>link").index(el) != -1) {
                            if (i < 10)
                                clearInterval(checkInterval)
                            if (typeof callback == 'function')
                                callback();
                            i++;
                        }
                    }, 200);
                }
                break;
            case "js":
                $.ajax({
                    global: false,
                    cache: true,
                    ifModified: false,
                    dataType: "script",
                    url: url,
                    success: callback
                });
                break;
            default:
                break;
        }
    },
    namespace: function (module) {
        var space = module.split('.');
        var s = '';
        for (var i in space) {
            if (space[i].constructor == String) {
                if (0 == s.length)
                    s = space[i];
                else
                    s += '.' + space[i];
                eval("if ((typeof(" + s + ")) == 'undefined') " + s + " = {};");
            }
        }
    },
    register: function (module) {
        var _this = this;
        var _func = function (module) {
            if (module && _this[module]) {
                for (var i in _this[module]) {
                    this[i] = _this[module][i];
                }
            }
            return this;
        };
        return new _func(module);
    },
    tools: {
        reverse: function (srcString) {
            var temp = [];
            for (var i = srcString.length - 1; i > -1; i--) {
                temp.push(srcString.charAt(i));
            }
            return temp.join("").toString();
        },
        getUrlPar: function (strName) {
            var svalue = location.search.match(new RegExp("[\?\&]" + strName + "=([^\&]*)(\&?)", "i"));
            return svalue ? svalue[1] : svalue;
        },
        copy: function (txt) {
            if (window.clipboardData) {
                window.clipboardData.clearData();
                window.clipboardData.setData("Text", txt);
            } else if (navigator.userAgent.indexOf("Opera") != -1) {
                window.location = txt;
            } else if (window.netscape) {
                try {
                    netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
                } catch (e) {
                    alert("您的firefox安全限制限制您进行剪贴板操作，请打开'about:config'将signed.applets.codebase_principal_support'设置为true'之后重试");
                    return false;
                }
                var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard);
                if (!clip)
                    return false;
                var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable);
                if (!trans)
                    return false;
                trans.addDataFlavor('text/unicode');
                var str = new Object();
                var len = new Object();
                var str = Components.classes['@mozilla.org/supports-string;1'].createInstance(Components.interfaces.nsISupportsString);
                var copytext = txt;
                str.data = copytext;
                trans.setTransferData("text/unicode", str, copytext.length * 2);
                var clipid = Components.interfaces.nsIClipboard;
                if (!clip)
                    return false;
                clip.setData(trans, null, clipid.kGlobalClipboard);
            }
            return true;
        },
        string: {
            format: function () {
                if (arguments.length == 0)
                    return "";
                var args = arguments;
                var str = args[0];
                return str.replace(/\{(\d+)\}/gm, function () {
                    return args[parseInt(arguments[1]) + 1];
                });
            },
            length: function (str) {
                var len = 0;
                for (var i = 0; i < str.length; i++) {
                    if (str.charCodeAt(i) >= 0x4e00 && str.charCodeAt(i) <= 0x9fa5) {
                        len += 2;
                    } else {
                        len++;
                    }
                }
                return len;
            }
        },
        hashString: function (item) {
            if (!item)
                return location.hash.substring(1);
            var sValue = location.hash.match(new RegExp("[\#\&]" + item + "=([^\&]*)(\&?)", "i"));
            sValue = sValue ? sValue[1] : "";
            return sValue == location.hash.substring(1) ? "" : sValue == undefined ? location.hash.substring(1) : decodeURIComponent(sValue);
        },
        cookie: function (name, value, options) {
            if (typeof value != 'undefined') {
                options = options || {};
                if (value === null) {
                    value = '';
                    $.extend({}, options);
                    options.expires = -1;
                }
                var expires = '';
                if (options.expires && (typeof options.expires == 'number' || options.expires.toUTCString)) {
                    var date;
                    if (typeof options.expires == 'number') {
                        date = new Date();
                        date.setTime(date.getTime() + (options.expires * 24 * 60 * 60 * 1000));
                    } else {
                        date = options.expires;
                    }
                    expires = '; expires=' + date.toUTCString(); // use expires attribute, max-age is not supported by IE
                }
                var path = options.path ? '; path=' + (options.path) : '';
                var domain = options.domain ? '; domain=' + (options.domain) : '';
                var secure = options.secure ? '; secure' : '';
                document.cookie = [name, '=', encodeURIComponent(value), expires, path, domain, secure].join('');
            } else {
                var cookieValue = '';
                if (document.cookie && document.cookie != '') {
                    var cookies = document.cookie.split(';');
                    for (var i = 0; i < cookies.length; i++) {
                        var cookie = cookies[i].tirm();
                        if (cookie.substring(0, name.length + 1) == (name + '=')) {
                            cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                            break;
                        }
                    }
                }
                return cookieValue;
            }
        },
        checkAll: function (obj, elName) {
            $(obj).closest("form").find("input:checkbox[name=" + elName + "]").attr("checked", $(obj).attr("checked"));
        },
        checkCount: function (obj, maxNum) {
            var chks = document.getElementsByName(obj.name);
            var count = 0;
            for (var i = 0; i < chks.length; i++) {
                if (chks[i].checked == true) {
                    count++;
                }
                if (count > maxNum) {
                    obj.checked = false;
                    alert('最多只能选择' + maxNum + '项');
                    return false;
                }
            }
        },
        insertSelection: function (obj, str) {
            var tc = obj;
            var tclen = tc.value.length;
            tc.focus();
            if (typeof document.selection != "undefined") {
                document.selection.createRange().text = str;
                obj.createTextRange().duplicate().moveStart("character", -str.length);
            } else {
                var m = tc.selectionStart;
                tc.value = tc.value.substr(0, tc.selectionStart) + str + tc.value.substring(tc.selectionStart, tclen);
                tc.selectionStart = m + str.length;
                tc.setSelectionRange(m + str.length, m + str.length);
            }
        },
        msglen: function (text) { // 微博字数计算规则 汉字 1 英文 0.5 网址 11 除去首尾空白
            text = text.replace(new RegExp("((news|telnet|nttp|file|http|ftp|https)://){1}(([-A-Za-z0-9]+(\\.[-A-Za-z0-9]+)*(\\.[-A-Za-z]{2,5}))|([0-9]{1,3}(\\.[0-9]{1,3}){3}))(:[0-9]*)?(/[-A-Za-z0-9_\\$\\.\\+\\!\\*\\(\\),;:@&=\\?/~\\#\\%]*)*", "gi"), '填充填充填充填充填充填');
            return Math.ceil(($.trim(text.replace(/[^\u0000-\u00ff]/g, "aa")).length) / 2);
        },
        json_encode_js: function (aaa) {
            function je(str) {
                var a = [], i = 0;
                var pcs = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                for (; i < str.length; i++) {
                    if (pcs.indexOf(str[i]) == -1)
                        //a[i]="\\u"+("0000"+str.charCodeAt(i).toString(16)).slice(-4);
                        a[i] = str[i];
                    else
                        a[i] = str[i];
                }
                return a.join("");
            }
            var i, s, a, aa = [];
            if (typeof (aaa) != "object") {
                alert("ERROR json");
                return;
            }
            for (i in aaa) {
                s = aaa[i];
                a = '"' + je(i) + '":';
                if (typeof (s) == 'object') {
                    a += json_encode_js(s);
                } else {
                    if (typeof (s) == 'string')
                        a += '"' + je(s) + '"';
                    else if (typeof (s) == 'number')
                        a += s;
                }
                aa[aa.length] = a;
            }
            return "{" + aa.join(",") + "}";
        },
        moneyFormat: function (num) {
            if (num == null || num == undefined)
                return "0.00";
            var sign = Number(num) < 0 ? "-" : "";
            num = num.toString();
            if (num.indexOf(".") == -1) {
                num = "" + num + ".0000";
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
        },
        /*
         中国电信：133、153、180、189
         中国联通：130、131、132、155、156、185、186
         中国移动：134、135、136、137、138、139、147、150、151、152、157、158、159、182、187、188
         */
        isPhone: function (phone) {
            if (phone == '' || phone.length != 11) {
                return false;
            }
            if (!phone.match(/^1[3|4|5|8]\d{9}$/)) {
                return false;
            } else {
                return true;
            }
        },
        validateInputDate: function (str) {
            str = $.trim(str);
            if (str == "" || str == null) {
                return true;
            }
            var tempArr = str.split(" ");
            var dateArr = new Array();
            var timeArr = new Array();
            if (tempArr[0].indexOf("-") != -1) {
                //2009-06-12
                dateArr = tempArr[0].split("-");
            } else if (tempArr[0].indexOf("/") != -1) {
                //2009/06/12
                dateArr = tempArr[0].split("/");
            } else {
                // 20090612
                if (tempArr[0].toString().length < 8) {
                    return false;
                }
                dateArr[0] = tempArr[0].substring(0, 4);
                dateArr[1] = tempArr[0].substring(4, 6);
                dateArr[2] = tempArr[0].substring(6, 8);
            }

            if (tempArr[1] == undefined || tempArr[1] == null) {
                tempArr[1] = "00:00:00";
            }

            if (tempArr[1].indexOf(":") != -1) {
                timeArr = tempArr[1].split(":");
            }

            if (dateArr[2] != undefined && (dateArr[0] == "" || dateArr[1] == "")) {
                return false;
            }

            if (dateArr[1] != undefined && dateArr[0] == "") {
                return false;
            }

            if (timeArr[2] != undefined && (timeArr[0] == "" || timeArr[1] == "")) {
                return false;
            }

            if (timeArr[1] != undefined && timeArr[0] == "") {
                return false;
            }
            dateArr[0] = (dateArr[0] == undefined || dateArr[0] == "") ? 1970 : dateArr[0];
            dateArr[1] = (dateArr[1] == undefined || dateArr[1] == "") ? 0 : (dateArr[1] - 1);
            dateArr[2] = (dateArr[2] == undefined || dateArr[2] == "") ? 0 : dateArr[2];
            timeArr[0] = (timeArr[0] == undefined || timeArr[0] == "") ? 0 : timeArr[0];
            timeArr[1] = (timeArr[1] == undefined || timeArr[1] == "") ? 0 : timeArr[1];
            timeArr[2] = (timeArr[2] == undefined || timeArr[2] == "") ? 0 : timeArr[2];
            var newDate = new Date(dateArr[0], dateArr[1], dateArr[2], timeArr[0], timeArr[1], timeArr[2]);
            if (
                    newDate.getFullYear() == dateArr[0] && newDate.getMonth() == dateArr[1] && newDate.getDate() == dateArr[2]
                    && newDate.getHours() == timeArr[0] && newDate.getMinutes() == timeArr[1] && newDate.getSeconds() == timeArr[2]
                    ) {
                return true;
            } else {
                return false;
            }
            return true;
        },
        isEmail: function (email) {
            if (email == '' || email.length < 6) {
                return false;
            }
            if (!email.match(/^\w+((-+\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/)) {
                return false;
            } else {
                return true;
            }
        }
    },
    sha1: function (str) {
        var rotate_left = function (n, s) {
            var t4 = (n << s) | (n >>> (32 - s));
            return t4;
        };
        var cvt_hex = function (val) {
            var str = '';
            var i;
            var v;
            for (i = 7; i >= 0; i--) {
                v = (val >>> (i * 4)) & 0x0f;
                str += v.toString(16);
            }
            return str;
        };
        var blockstart;
        var i, j;
        var W = new Array(80);
        var H0 = 0x67452301;
        var H1 = 0xEFCDAB89;
        var H2 = 0x98BADCFE;
        var H3 = 0x10325476;
        var H4 = 0xC3D2E1F0;
        var A, B, C, D, E;
        var temp;
        //str = this.utf8_encode(str);
        var str_len = str.length;
        var word_array = [];
        for (i = 0; i < str_len - 3; i += 4) {
            j = str.charCodeAt(i) << 24 | str.charCodeAt(i + 1) << 16 | str.charCodeAt(i + 2) << 8 | str.charCodeAt(i + 3);
            word_array.push(j);
        }

        switch (str_len % 4) {
            case 0:
                i = 0x080000000;
                break;
            case 1:
                i = str.charCodeAt(str_len - 1) << 24 | 0x0800000;
                break;
            case 2:
                i = str.charCodeAt(str_len - 2) << 24 | str.charCodeAt(str_len - 1) << 16 | 0x08000;
                break;
            case 3:
                i = str.charCodeAt(str_len - 3) << 24 | str.charCodeAt(str_len - 2) << 16 | str.charCodeAt(str_len - 1) <<
                        8 | 0x80;
                break;
        }

        word_array.push(i);
        while ((word_array.length % 16) != 14) {
            word_array.push(0);
        }

        word_array.push(str_len >>> 29);
        word_array.push((str_len << 3) & 0x0ffffffff);
        for (blockstart = 0; blockstart < word_array.length; blockstart += 16) {
            for (i = 0; i < 16; i++) {
                W[i] = word_array[blockstart + i];
            }
            for (i = 16; i <= 79; i++) {
                W[i] = rotate_left(W[i - 3] ^ W[i - 8] ^ W[i - 14] ^ W[i - 16], 1);
            }

            A = H0;
            B = H1;
            C = H2;
            D = H3;
            E = H4;
            for (i = 0; i <= 19; i++) {
                temp = (rotate_left(A, 5) + ((B & C) | (~B & D)) + E + W[i] + 0x5A827999) & 0x0ffffffff;
                E = D;
                D = C;
                C = rotate_left(B, 30);
                B = A;
                A = temp;
            }

            for (i = 20; i <= 39; i++) {
                temp = (rotate_left(A, 5) + (B ^ C ^ D) + E + W[i] + 0x6ED9EBA1) & 0x0ffffffff;
                E = D;
                D = C;
                C = rotate_left(B, 30);
                B = A;
                A = temp;
            }

            for (i = 40; i <= 59; i++) {
                temp = (rotate_left(A, 5) + ((B & C) | (B & D) | (C & D)) + E + W[i] + 0x8F1BBCDC) & 0x0ffffffff;
                E = D;
                D = C;
                C = rotate_left(B, 30);
                B = A;
                A = temp;
            }

            for (i = 60; i <= 79; i++) {
                temp = (rotate_left(A, 5) + (B ^ C ^ D) + E + W[i] + 0xCA62C1D6) & 0x0ffffffff;
                E = D;
                D = C;
                C = rotate_left(B, 30);
                B = A;
                A = temp;
            }

            H0 = (H0 + A) & 0x0ffffffff;
            H1 = (H1 + B) & 0x0ffffffff;
            H2 = (H2 + C) & 0x0ffffffff;
            H3 = (H3 + D) & 0x0ffffffff;
            H4 = (H4 + E) & 0x0ffffffff;
        }

        temp = cvt_hex(H0) + cvt_hex(H1) + cvt_hex(H2) + cvt_hex(H3) + cvt_hex(H4);
        return temp.toLowerCase();
    },
    widget: {},
    api: {//接口调用方法
        ajax: function (type, action, data, callback, dataType, cache, async, options) {
            if (action != undefined)
                var url = (Suke.BASEURL == '/' ? '' : Suke.BASEURL) + Suke.SERVICEURL + "/" + action;
            else
                var url = location.pathname;
            $.ajax($.extend({
                url: url,
                data: data,
                async: typeof async != "undefined" ? async : true,
                type: typeof type != "undefined" ? type : "GET",
                dataType: typeof dataType != "undefined" ? dataType : "html",
                //contentTypeString:"appliction/json; charset=UTF-8",
                ifModified: false,
                timeout: 8000,
                traditional: true,
                cache: typeof cache != "undefined" ? cache : true,
                success: callback,
                error: function () {
                    if (async == false) {
                        Suke.common.alert(Suke.LANG.syserror);
                    }
                    $("#dialog_loading").remove();
                    return false;
                },
                beforeSend: function (XMLHttpRequest) {
                }
            }, options || {}));
        },
        get: function (action, data, callback, cache, async, options) {
            this.ajax("GET", action, data, callback, cache, async, options);
        },
        post: function (action, data, callback, cache, async, options) {
            this.ajax("POST", action, data, callback, cache, async, options);
        }
    },
    template: function (tplname, data) {	//模板
        return $("#_" + tplname.toUpperCase() + "_TPL_").html().process(data);
    },
    tabs: {//页签
        bind: function (obj, cobj) {
            $("dd>ul:eq(0)>li", obj).click(function () {
                var _class = this.className.split(" ");
                if ($.inArray("none", _class) == -1 && $.inArray("more", _class) == -1) {
                    var _index = $(this).parent().children(":not(.more,.none)").index($(this).addClass("curr").siblings().removeClass("curr").end()[0]);
                    var tab_content = $(obj).siblings(cobj);
                    if (tab_content.length > 0)
                        tab_content.hide().eq(_index).show();
                }
            });
            $("dd>ul.tabs_sub>li>a", obj).click(function () {
                var _this = $(this).parent();
                var _class = _this[0].className.split(" ");
                if ($.inArray("none", _class) == -1) {
                    _this.addClass("curr").siblings().removeClass("curr");
                }
            });
            if ($(".more_list li", obj).length > 0) {
                $("li.more>a:eq(0)", obj).click(function () {
                    var _this = $(".more_list", obj);
                    _this.toggle().out("click", function (e) {
                        var found = $(e.target).closest(_this).length || e.target == this;
                        if (found == 0)
                            _this.hide();
                    }.bind(this), true);
                });
            }
        }
    },
    form: {
        bindDefault: function (obj) {
            $(obj || "input.default,textarea.default").live("focus", function () {
                if (this.value == this.defaultValue) {
                    this.value = '';
                    this.style.color = "#000";
                }
            }).live("blur", function () {
                if (this.value == '') {
                    this.value = this.defaultValue;
                    this.style.color = '#ccc';
                }
            });
        },
        bindFocus: function (obj) {
            $(obj || "input.text,textarea.textarea").live("focus", function () {
                $(this).addClass("focus");
            }).live("blur", function () {
                $(this).removeClass("focus");
            });
        },
        isInputNull: function (obj) {
            obj = $(obj);
            if (obj.length == 0)
                return false;
            var _value = obj.val().trim();
            if (_value == "" || _value == obj[0].defaultValue) {
                return true;
            }
            return false;
        },
        bindSelect: function (obj) {
            var self = $(obj);
            var box = $('<div class="dropselectbox" />').appendTo(self.hide().wrap('<div class="dropdown" />').parent());
            $('<h4><span class="symbol arrow">▼</span><strong>' + self.children("option:selected").text() + '</strong></h4>').hover(function () {
                $(this).toggleClass("hover", option.is(":visible") ? true : null);
            }).appendTo(box).one("click", function () {
                self.children("option").each(function (i, item) {
                    $('<li><a href="javascript:void(0)">' + $(item).text() + '</a></li>').appendTo(option).click(function () {
                        option.prev().children("strong").html($(item).text());
                        self.val(item.value).change();
                        option.hide();
                    });
                });
            }).click(function () {
                option.toggle();
            }).out("click", function () {
                $(this).removeClass("hover");
                option.hide();
            }, true);
            var option = $('<ul />').appendTo(box);
        }
    },
    pager: {//分页控件
        init: function (obj, options) {
            this.element = $(obj);
            this.opt = $.extend({
                pageindex: 1,
                pagesize: 10,
                totalcount: -1,
                type: "numeric", //text
                total: false,
                skip: false,
                breakpage: 3,
                ajaxload: false
            }, options || {});
            return Ajzhan.register("pager");
        },
        bind: function (obj, options) {
            var _this = this.init(obj, options);
            if (_this.opt.pageindex < 1)
                _this.opt.pageindex = 1;
            if (_this.opt.totalcount > -1) {
                _this.opt.pagecount = Math.ceil(_this.opt.totalcount / _this.opt.pagesize);
                if (_this.opt.pageindex > _this.opt.pagecount)
                    _this.opt.pageindex = _this.opt.pagecount;
            } else {
                _this.opt.pagecount = 99999;
            }
            if (_this.opt.breakpage > _this.opt.pagecount - 2) {
                _this.opt.breakpage = _this.opt.pagecount - 2;
                _ellipsis = [true, true];
            } else {
                _ellipsis = [false, false];
            }
            var _html = [];
            if (_this.opt.pagecount > 1 || _this.opt.total)
                _html.push('<div class="pager ' + (_this.opt.type == "numeric" ? "pager_numeric" : "") + '">\n');
            if (_this.opt.total) {
                _html.push('<div class="p_options">');
                _html.push('<span class="p_ptotal">' + _this.opt.pageindex + '页/' + _this.opt.pagecount + '页</span>\n');
                _html.push('<span class="p_total">共' + _this.opt.totalcount + '条</span>\n');
                _html.push('</div>');
            }
            if (_this.opt.pagecount > 1) {
                if (_this.opt.type == "text") {
                    if (_this.opt.pageindex > 1) {
                        if (_this.opt.pagecount < 9999)
                            _html.push('<a class="p_start" href="' + _this._getUrl(1) + '">首页</a>\n');
                        _html.push('<a class="p_prev" href="' + _this._getUrl(_this.opt.pageindex - 1) + '">上一页</a>\n');
                    }
                    if (_this.opt.pageindex != _this.opt.pagecount) {
                        _html.push('<a class="p_next" href="' + _this._getUrl(_this.opt.pageindex + 1) + '">下一页</a>\n');
                        if (_this.opt.pagecount < 9999)
                            _html.push('<a class="p_end" href="' + _this._getUrl(_this.opt.pagecount) + '">尾页</a>\n');
                    }
                }
                if (_this.opt.type == "numeric") {
                    if (_this.opt.pageindex > 1) {	//第一页
                        _html.push('<a class="p_prev" href="' + _this._getUrl(_this.opt.pageindex - 1) + '">上页</a>\n');
                    }
                    var _page = [1, _this.opt.pagecount, _this.opt.pageindex, _this.opt.pageindex - 1, _this.opt.pageindex + 1];
                    _page = $.grep(_page, function (item, i) {
                        return item > 0 && item <= _this.opt.pagecount;
                    }).unique();
                    var _count = _page.length;
                    for (var i = 1; i <= _this.opt.breakpage + 2 - _count; i++) {
                        _page.push(_this.opt.pageindex + (_this.opt.pageindex + i < _this.opt.pagecount ? i + 1 : -i - 1));
                    }
                    _page = _page.sort(function sortNumber(a, b) {
                        return a - b;
                    }).unique();
                    var title = "";
                    $.each(_page, function (i, item) {
                        if (this.opt.pageindex == item) {
                            _html.push('<strong>' + item + '</strong>\n');
                        } else {
                            if (item == 1) {
                                title = "首页";
                            } else if (item == _this.opt.pagecount) {
                                title = "尾页";
                            } else {
                                title = "第" + item + "页";
                            }
                            if (_ellipsis[1] == false && this.opt.pageindex <= this.opt.pagecount - this.opt.breakpage && this.opt.pagecount == item) {
                                _html.push('<span>...</span>\n');
                                _ellipsis[1] = true;
                            }
                            _html.push('<a class="p_start" href="' + this._getUrl(item) + '" title="' + title + '">' + item + '</a>\n');
                            if (_ellipsis[0] == false && this.opt.pageindex > this.opt.breakpage) {
                                _html.push('<span>...</span>\n');
                                _ellipsis[0] = true;
                            }
                        }
                    }.bind(_this));
                    if (_this.opt.pageindex < _this.opt.pagecount) {
                        _html.push('<a class="p_next" href="' + _this._getUrl(_this.opt.pageindex + 1) + '">下页</a>\n');
                    }
                }
                if (_this.opt.skip) {
                    _html.push('<div class="p_skip">跳转到:');
                    _html.push('<input type="text" class="p_text" maxlength="8" onclick="this.select()" size="3" name="page" value="' + _this.opt.pageindex + '" />');
                    _html.push('<button class="p_btn" onclick="location.href=\'' + _this._getUrl() + '\'">GO</button>');
                    _html.push('</div>');
                }
            }
            if (_this.opt.pagecount > 1 || _this.opt.total)
                _html.push('</div>');
            _this.element.html(_html.join(""));
        },
        _getUrl: function (page) {
            if (this.opt.ajaxload) {
                return "javascript:goPageIndex(" + page + ");";
            }
            var _url = location.pathname + "?";
            if (page && page.constructor == Number) {
                if (page <= 0)
                    page = 1;
                return _url + Ajzhan.params.set({
                    page: page
                }).serialize() + location.hash;
            }
            return _url + location.hash;
        },
        goPageIndex: function (page) {
            this.element.html("数据加载中...");
            this.bind(this.element, $.extend(this.opt, {
                pageindex: page
            }));
        }
    },
    params: {//参数操作
        init: function (url) {
            this.list = {};
            $.each(location.search.match(/(?:[\?|\&])[^\=]+=[^\&|#|$]*/gi) || [], function (n, item) {
                var _item = item.substring(1);
                var _key = _item.split("=", 1)[0];
                var _value = _item.replace(eval("/" + _key + "=/i"), "");
                this.list[_key.toLowerCase()] = _value;
            }.bind(this));
            return this;
        },
        get: function (item) {
            if (typeof this.list == "undefined")
                this.init();
            var _item = this.list[item.toLowerCase()];
            return _item ? _item : "";
        },
        set: function (options) {
            if (typeof this.list == "undefined")
                this.init();
            this.list = $.extend(true, this.list, options || {});
            return this;
        },
        serialize: function () {
            if (typeof this.list == "undefined")
                this.init();
            return $.param(this.list);
        }
    }
});
jQuery.extend({
    out: function (el, name, func, canMore) {
        var callback = function (e) {
            var src = e.target || e.srcElement;
            var isIn = false;
            while (src) {
                if (src == el) {
                    isIn = true;
                    break;
                }
                src = src.parentNode;
            }
            if (!isIn) {
                func.call(el, e);
                if (!canMore) {
                    jQuery.event.remove(document.body, name, c);
                    if (el._EVENT && el._EVENT.out && el._EVENT.out.length) {
                        var arr = el._EVENT.out;
                        for (var i = 0, il = arr.length; i < il; i++) {
                            if (arr[i].efunc == c && arr[i].name == name) {
                                arr.splice(i, 1);
                                return;
                            }
                        }
                    }
                }
            }
        }
        var c = callback.bindEvent(window);
        if (!el._EVENT) {
            el._EVENT = {
                out: []
            }
        }
        el._EVENT.out.push({
            name: name,
            func: func,
            efunc: c
        });
        jQuery.event.add(document.body, name, c);
    },
    unout: function (el, name, func) {
        if (el._EVENT && el._EVENT.out && el._EVENT.out.length) {
            var arr = el._EVENT.out;
            for (var i = 0, il = arr.length; i < il; i++) {
                if ((func == undefined || arr[i].func == func) && arr[i].name == name) {
                    jQuery.event.remove(document.body, name, arr[i].efunc);
                    arr.splice(i, 1);
                    return;
                }
            }
        }
    }
});
$.browser.msie6 = $.browser.msie && /MSIE 6\.0/i.test(window.navigator.userAgent) && !/MSIE 7\.0/i.test(window.navigator.userAgent);
//函数扩展
Function.prototype.bind = function () {	//绑定域
    var method = this,
            _this = arguments[0],
            args = [];
    for (var i = 1,
            il = arguments.length; i < il; i++) {
        args.push(arguments[i]);
    }
    return function () {
        var thisArgs = args.concat();
        for (var i = 0,
                il = arguments.length; i < il; i++) {
            thisArgs.push(arguments[i]);
        }
        return method.apply(_this, thisArgs);
    };
};
Function.prototype.timeout = function (time) {	//延时执行
    if ($.browser.mozilla) {
        var f = this;
        return setTimeout(function () {
            f();
        },
                time * 1000);
    }
    return setTimeout(this, time * 1000);
};
Function.prototype.interval = function (time) {	//循环执行
    return setInterval(this, time * 1000);
};
Function.prototype.bindEvent = function () {	//绑定Event
    var method = this,
            _this = arguments[0],
            args = [];
    for (var i = 1,
            il = arguments.length; i < il; i++) {
        args.push(arguments[i]);
    }
    return function (e) {
        var thisArgs = args.concat();
        thisArgs.unshift(e || window.event);
        return method.apply(_this, thisArgs);
    };
};
var SetCookie = function (name, value, expire, path) {
    //expire=expire||30*24*60*60*1000;
    var curdate = new Date();
    var cookie = name + "=" + encodeURIComponent(value) + "; ";
    if (expire != undefined || expire == 0) {
        if (expire == -1) {
            expire = 366 * 86400 * 1000;//保存一年
        } else {
            expire = parseInt(expire);
        }
        curdate.setTime(curdate.getTime() + expire);
        cookie += "expires=" + curdate.toUTCString() + "; ";
    }
    path = path || "/";
    cookie += "path=" + path;
    document.cookie = cookie;
};
var setCookie = function (name, value, expire, path) {
    //expire=expire||30*24*60*60*1000;
    var curdate = new Date();
    var cookie = name + "=" + encodeURIComponent(value) + "; ";
    if (expire != undefined || expire == 0) {
        if (expire == -1) {
            expire = 366 * 86400 * 1000;//保存一年
        } else {
            expire = parseInt(expire);
        }
        curdate.setTime(curdate.getTime() + expire);
        cookie += "expires=" + curdate.toUTCString() + "; ";
    }
    path = path || "/";
    cookie += "path=" + path;
    document.cookie = cookie;
};
function getCookie(name) {
    var re = "(?:; )?" + encodeURIComponent(name) + "=([^;]*);?";
    re = new RegExp(re);
    if (re.test(document.cookie)) {
        return decodeURIComponent(RegExp.$1);
    }
    return '';
}
