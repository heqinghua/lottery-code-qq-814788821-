//Flash调用方法
function addNewSwf(swfUrl, objID, swfID, width, height, ifAlpha, ifScript, varStr) {
    var addcountsflash = new SWFObject(swfUrl, swfID, width, height, "9");

    if (ifAlpha == 1) {
        addcountsflash.addParam("wmode", "transparent");
    }

    if (ifScript == 1) {
        addcountsflash.addParam("allowScriptAccess", "always");
    }

    addcountsflash.addParam("flashvars", varStr);
    addcountsflash.write(objID);
}
/*声音播放插件*/
(function ($) {
    $.fn.soundBox = function (options) {
        var defaults = {
            audioId: "chatAudio",//播放插件id
            bgType: ['ogg', 'mp3', 'wav'],//音频格式
            bgDir: '',//背景音乐路径
            swfDir: '',//flash文件路径
            swfId: '',//
            isAir: '0',
            soundOn: true
        };

        //配置文件
        var opt = {
            soundType: {
                ogg: "audio/ogg",
                mp3: "audio/mpeg",
                wav: "audio/wav"
            }
        };
        var options = $.extend({}, defaults, options);
        var isSafari = navigator.userAgent.indexOf("Safari") > 0 ? true : false;
        var isChrome = navigator.userAgent.indexOf("Chrome") > 0 ? true : false;
        options.isAir = getCookie("isclient") || defaults.isAir;

        this.each(function () {
            //配置项检查
            $.each(options, function (i, v) {
                if (v == '') {
                    alert("请检查defaults." + i + "的配置...");
                    return false;
                }
            });

            var _html = '<audio id="' + options.audioId + '">';
            $.each(options.bgType, function (i, v) {
                _html += '<source src="' + options.bgDir + '.' + v + '" type="' + opt.soundType[v] + '">';
            });
            _html += '</audio>';

            //播放器绑定
            if (options.isAir != "1") {
                //$(this).empty();
                if (!!document.all) {//ie浏览器
                    addNewSwf(options.swfDir, this.id, options.swfId, 1, 1, 1, 1, "");
                } else {//非ie浏览器
                    if (isSafari && !isChrome) {//Safari浏览器
                        addNewSwf(options.swfDir, this.id, options.swfId, 1, 1, 1, 1, "");
                    } else {
                        $(this).prepend(_html);
                        //$(_html).appendTo($(this));		
                    }
                }
            }
        });

        //声音播放
        this._soundCtl = function () {
            if (options.soundOn == true) {
                options.soundOn = false;
            } else {
                options.soundOn = true;
            }
        }

        //音乐播放方法
        this._mPlay = function () {
            //flash绑定
            if (options.soundOn) {
                if (options.isAir != "1") {
                    if (!!document.all) {//ie浏览器
                        document.getElementById(options.swfId).mPlay(options.bgDir + '.mp3');
                    } else {//非ie浏览器
                        if (isSafari && !isChrome) {//Safari浏览器
                            document.getElementById(options.swfId).mPlay(options.bgDir + '.mp3');
                        } else {
                            $('#' + options.audioId)[0].play();
                        }
                    }
                } else {
                    document.getElementById(options.swfId).mPlay(options.bgDir + '.mp3');
                }
            }
        }
        return this;

    };
})(jQuery);
