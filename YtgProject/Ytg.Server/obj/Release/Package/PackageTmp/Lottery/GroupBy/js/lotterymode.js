var Right = {};
Right.LotNews = function(s) { //彩种页面右边新闻
    $.get('/news/lot', { type: s, t: local.TimeLong() }, function(data) {
        if (data.msg != 'no') {
            if (data.msg.length > 0) {
                html = '<div class="all_title gray_title"><div class="title_m f14">热门公告</div></div><div><ul class="right_list">';
                for (var i = 0; i < data.msg.length; i++) {
                    html += '<li><a target="_blank" href="/news/detail?id=' + data.msg[i].news_id + '">' + data.msg[i].news_title + '</a></li>';
                }
                html += '</ul></div>';
                $('#r_news').html(html).addClass('block gray_border');
            }
        }
    }, 'json');
}
Right.DayOpen = function(s) { //彩种当天开奖号码
    $.get('/mode/dayopen', { lot: s, t: local.TimeLong() }, function(data) {
        if (data.msg != 'no' && data.msg != 'err') {
            var l = data.msg.length;
            var len = Math.ceil(l / 4);
            var tbody = $('#kjhmDiv tbody');
            for (var i = 0; i < l; i++) {
                var haoma = data.msg[i].lot_haoma;
                var qihao = data.msg[i].lot_qihao;
                if (i < l) {
                    var tr = $('<tr/>').appendTo(tbody);
                }
                if (haoma.length > 0) {
                    var xt1 = '<font color="#FA6705">组三</font>';
                    var xt2 = '<font color="#FA6705">组三</font>';
                    var xt3 = '<font color="#FA6705">组三</font>';

                    var hms = haoma.split(',');
                    var hms0 = hms[0];
                    var hms1 = hms[1];
                    var hms2 = hms[2];
                    var hms3 = hms[3];
                    var hms4 = hms[4];
                    if (hms2 == hms3 && hms2 == hms4) {
                        xt3 = '<font color="#24A724">豹子</font>';
                    } else if (hms2 != hms3 && hms2 != hms4 && hms4 != hms3) {
                        xt3 = '组六';
                    }

                    if (hms0 == hms1 && hms0 == hms2) {
                        xt1 = '<font color="#24A724">豹子</font>';
                    } else if (hms0 != hms1 && hms0 != hms2 && hms2 != hms1) {
                        xt1 = '组六';
                    }

                    if (hms1 == hms2 && hms1 == hms3) {
                        xt2 = '<font color="#24A724">豹子</font>';
                    } else if (hms1 != hms2 && hms1 != hms3 && hms3 != hms2) {
                        xt2 = '组六';
                    }
                    // xt1 += hms3 < 5 ? '小' : '大';
                    // xt1 += hms3 % 2 == 0 ? '双' : '单';
                    // xt2 += hms4 < 5 ? '小' : '大';
                    // xt2 += hms4 % 2 == 0 ? '双' : '单';
                    tbody.find('tr:eq(' + (i % len) + ')').append('<td><span class="period">' + qihao.substr(8) + '</span><span class="awardNum">' + haoma.replaceAll(',', '') + '</span>' + xt1 + ' ' + xt2 + ' ' + xt3 + '</td>');
                } else {
                    tbody.find('tr:eq(' + (i % len) + ')').append('<td><span class="period">' + qihao.substr(8) + '</span>- -</td>');
                }
            }
        }
    }, 'json');
}
Right.DayOpen2 = function(s) { //彩种当天开奖号码(11选5)
    $.get('/mode/dayopen', { lot: s, t: local.TimeLong() }, function(data) {
        if (data.msg != 'no' && data.msg != 'err') {
            var l = data.msg.length;
            var len = Math.ceil(l / 4);
            var tbody = $('#kjhmDiv tbody');
            for (var i = 0; i < l; i++) {
                var haoma = data.msg[i].lot_haoma;
                var qihao = data.msg[i].lot_qihao;
                if (i < l) {
                    var tr = $('<tr/>').appendTo(tbody);
                }
                if (haoma.length > 0) {
                    tbody.find('tr:eq(' + (i % len) + ')').append('<td><span class="period">' + qihao.substr(8) + '</span><span class="awardNum" style="width:85px">' + haoma.replaceAll(',', ' ') + '</span></td>');
                } else {
                    tbody.find('tr:eq(' + (i % len) + ')').append('<td><span class="period">' + qihao.substr(8) + '</span>- -</td>');
                }
            }
        }
    }, 'json');
}

Right.DayOpen3 = function(s) { //彩种当天开奖号码(11选5)
    $.get('/mode/dayopen', { lot: s, t: local.TimeLong() }, function(data) {
        if (data.msg != 'no' && data.msg != 'err') {
            var l = data.msg.length;
            var len = Math.ceil(l / 4);
            var tbody = $('#kjhmDiv tbody');
            for (var i = 0; i < l; i++) {
                var haoma = data.msg[i].lot_haoma;
                var qihao = data.msg[i].lot_qihao;
                if (i < l) {
                    var tr = $('<tr/>').appendTo(tbody);
                }
                if (haoma.length > 0) {
                    tbody.find('tr:eq(' + (i % len) + ')').append('<td><span class="period" style="width: 48px;">' + qihao + '</span><span class="awardNum" style="width:85px;display:initial">' + haoma.replaceAll(',', ' ').substr(0,12) + '</span></td>');
                } else {
                    tbody.find('tr:eq(' + (i % len) + ')').append('<td><span class="period" style="width: 48px;">' + qihao + '</span ><span class="awardNum" style="width:85px;display:initial">- -</span></td>');
                }
            }
        }
    }, 'json');
}

Right.NowOpen = function(s) {
    if (globe_style == 'ssc') {
        Right.LotOpen(globe_mark, globe_qishu);
    } else if (globe_style == '11x5') {
        Right.LotOpen4(globe_mark, globe_qishu);
    } else if (globe_style == 'pk10') {
        Right.LotOpen5(globe_mark, globe_qishu);
    } else if (globe_style == "k3") {
        Right.LotOpen2(globe_mark, globe_qishu);
    }

}
Right.LotOpen = function(s, n) { //彩种页面右边开奖（高频）
    $.get('/mode/lotopen', { lot: s, t: local.TimeLong() }, function(data) {
        if (data.msg != 'no' && data.msg != 'err') {
            var html = '<div class="all_title gray_title"><div class="title_m f14">最新开奖</div></div>';
            for (var i = 0; i < data.msg.length; i++) {
                var qihao = data.msg[i].lot_qihao;
                var qihaos = qihao.substr(8);
                var haoma = data.msg[i].lot_haoma;
                if (i == 0) {
                    var haomatemp = haoma.replaceAll(',', '</span>&nbsp;<span class="iconBred">');
                    html += '<div class="n_kjgg t_c"> ' + Show.LotName(s) + '  第 <em class="c_ba2636">' + qihao + '</em> 期 开奖<br><span class="iconBred">' + haomatemp + '</span><br>今天已售&nbsp;' + Number(qihaos) + '&nbsp;期，还剩&nbsp;' + (n - Number(qihaos)) + '&nbsp;期</div>' +
                        '<table width="90%" border="0" align="center" cellspacing="0" cellpadding="0" class="index_table right_t_kj"><thead><tr><th width="20%">期号</th><th width="30%">开奖号码</th><th>前三</th><th width="">中三</th><th width="14%">后三</th></tr></thead><tbody id="kj_list" class="ssckj_star1">';
                }
                var xt1 = '<font color="#FA6705">组三</font>';
                var xt2 = '<font color="#FA6705">组三</font>';
                var xt3 = '<font color="#FA6705">组三</font>';
                var hms = haoma.split(',');
                var hms0 = hms[0];
                var hms1 = hms[1];
                var hms2 = hms[2];
                var hms3 = hms[3];
                var hms4 = hms[4];
                if (hms2 == hms3 && hms2 == hms4) {
                    xt3 = '<font color="#24A724">豹子</font>';
                } else if (hms2 != hms3 && hms2 != hms4 && hms4 != hms3) {
                    xt3 = '组六';
                }

                if (hms0 == hms1 && hms0 == hms2) {
                    xt1 = '<font color="#24A724">豹子</font>';
                } else if (hms0 != hms1 && hms0 != hms2 && hms2 != hms1) {
                    xt1 = '组六';
                }

                if (hms1 == hms2 && hms1 == hms3) {
                    xt2 = '<font color="#24A724">豹子</font>';
                } else if (hms1 != hms2 && hms1 != hms3 && hms3 != hms2) {
                    xt2 = '组六';
                }
                
                //xt1 += hms3 < 5 ? '小' : '大';
                // xt1 += hms3 % 2 == 0 ? '双' : '单';
                // xt2 += hms4 < 5 ? '小' : '大';
                // xt2 += hms4 % 2 == 0 ? '双' : '单';
                var haomatemp = haoma.replaceAll(',', '</span> <span class="ball_1">');
                html += '<tr><td>&nbsp;' + qihaos + '</td><td><em class="c_ba2636"><span class="ball_5">' + haomatemp + '</span></em></td><td>' + xt1 + '</td><td>' + xt2 + '</td><td>' + xt3 + '</td></tr>';
            }
            html += '</tbody></table><p class="trendLink"><a href="#awardDetail">今日开奖</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a target="_blank" href="javascript:">历史查询</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a target="_blank" href="/Trend.jzh">走势图表</a></p>';
            $('#r_open').html(html);
        }
    }, 'json');
}
Right.LotOpen2 = function(s) { //彩种页面右边开奖（福彩和排三）
    $.get('/mode/lotopen', { lot: s, t: local.TimeLong() }, function(data) {
        if (data.msg != 'no' && data.msg != 'err') {
            var html = '<div class="all_title gray_title"><div class="title_m f14">最新开奖</div></div>';
            for (var i = 0; i < data.msg.length; i++) {
                var qihao = data.msg[i].lot_qihao;
                var haoma = data.msg[i].lot_haoma;
                if (i == 0) {
                    var haomatemp = haoma.replaceAll(',', '</span>&nbsp;<span class="iconBred">');
                    html += '<div class="n_kjgg t_c"> ' + Show.LotName(s) + '  第 <em class="c_ba2636">' + qihao + '</em> 期 开奖<br><span class="iconBred">' + haomatemp + '</span></div>' +
                        '<table width="90%" border="0" align="center" cellspacing="0" cellpadding="0" class="index_table right_t_kj"><thead><tr><th width="20%">期号</th><th width="32%">开奖号码</th><th>后三</th><th width="14%">十位</th><th width="14%">个位</th></tr></thead><tbody id="kj_list" class="ssckj_star1">';
                }
                var xt1 = '';
                var xt2 = '';
                var xt3 = '<font color="#FA6705">组三</font>';
                var hms = haoma.split(',');
                var hms0 = hms[0];
                var hms1 = hms[1];
                var hms2 = hms[2];
                if (hms0 == hms1 && hms0 == hms2) {
                    xt3 = '<font color="#24A724">豹子</font>';
                } else if (hms0 != hms1 && hms0 != hms2 && hms2 != hms1) {
                    xt3 = '组六';
                }
                xt1 += hms1 < 5 ? '小' : '大';
                xt1 += hms1 % 2 == 0 ? '双' : '单';
                xt2 += hms2 < 5 ? '小' : '大';
                xt2 += hms2 % 2 == 0 ? '双' : '单';
                var haomatemp = haoma.replaceAll(',', '</span> <span class="ball_1">');
                html += '<tr><td>&nbsp;' + qihao + '</td><td><em class="c_ba2636"><span class="ball_5">' + haomatemp + '</span></em></td><td>' + xt3 + '</td><td>' + xt1 + '</td><td>' + xt2 + '</td></tr>';
            }
            html += '</tbody></table><p class="trendLink"><a target="_blank" href="javascript:">历史查询</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a target="_blank" href="/Trend.jzh">走势图表</a></p>';
            $('#r_open').html(html);
        }
    }, 'json');
}
Right.LotOpen3 = function(s) { //彩种页面右边开奖（数字）
    $.get('/mode/lotopen', { lot: s, t: local.TimeLong() }, function(data) {
        if (data.msg != 'no' && data.msg != 'err') {
            var html = '<div class="all_title gray_title"><div class="title_m f14">最新开奖</div></div>';
            for (var i = 0; i < data.msg.length; i++) {
                var qihao = data.msg[i].lot_qihao;
                var haoma = data.msg[i].lot_haoma;
                var hms = haoma.split('+');
                var haomatemp = hms[0].replaceAll(',', '</em>&nbsp;<em class="red_ball">');
                if (hms.length > 1) {
                    haomatemp += '</em>&nbsp;<em class="blue_ball">' + hms[1].replaceAll(',', '</em>&nbsp;<em class="blue_ball">');
                }
                if (i == 0) {
                    html += '<div class="n_kjgg t_c"> ' + Show.LotName(s) + '  第 <em class="c_ba2636">' + qihao + '</em> 期 开奖<br><em class="red_ball">' + haomatemp + '</em></div>' +
                        '<table width="90%" border="0" align="center" cellspacing="0" cellpadding="0" class="index_table right_t_kj"><thead><tr><th width="20%">期号</th><th width="32%">开奖号码</th></tr></thead><tbody id="kj_list" class="ssckj_star1">';
                }
                html += '<tr><td>&nbsp;' + qihao.substr(4) + '</td><td><em class="red_ball">' + haomatemp + '</td></tr>';
            }
            html += '</tbody></table><p class="trendLink"><a target="_blank" href="javascript:">历史查询</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a target="_blank" href="/Trend.jzh">走势图表</a></p>';
            $('#r_open').html(html);
        }
    }, 'json');
}
Right.LotOpen4 = function(s, n) { //彩种页面右边开奖（高频11选5）
    $.get('/mode/lotopen', { lot: s, t: local.TimeLong() }, function(data) {
        if (data.msg != 'no' && data.msg != 'err') {
            var html = '<div class="all_title gray_title"><div class="title_m f14">最新开奖</div></div>';
            for (var i = 0; i < data.msg.length; i++) {
                var qihao = data.msg[i].lot_qihao;
                var qihaos = qihao.substr(8);
                var haoma = data.msg[i].lot_haoma;
                if (i == 0) {
                    var haomatemp = haoma.replaceAll(',', '</span>&nbsp;<span class="iconBred">');
                    html += '<div class="n_kjgg t_c"> ' + Show.LotName(s) + '  第 <em class="c_ba2636">' + qihao + '</em> 期 开奖<br><span class="iconBred">' + haomatemp + '</span><br>今天已售&nbsp;' + Number(qihaos) + '&nbsp;期，还剩&nbsp;' + (n - Number(qihaos)) + '&nbsp;期</div>' +
                        '<table width="90%" border="0" align="center" cellspacing="0" cellpadding="0" class="index_table right_t_kj"><thead><tr><th width="20%">期号</th><th width="32%">开奖号码</th><th>大小比</th><th>奇偶比</th></tr></thead><tbody id="kj_list" class="ssckj_star1">';
                }
                var da_num = 0;
                var ji_num = 0;
                var dxjohm = haoma.split(',');
                for (var n_f in dxjohm) {
                    var htemp = Number(dxjohm[n_f]);
                    if (htemp > 5) { da_num += 1; }
                    if (htemp % 2 == 1) { ji_num += 1; }
                }
                var haomatemp = haoma.replaceAll(',', '</span> <span class="ball_1">');
                html += '<tr><td>&nbsp;' + qihaos + '</td><td><em class="c_ba2636"><span class="ball_5">' + haomatemp + '</span></em></td><td>' + da_num + ':' + (5 - da_num) + '</td><td>' + ji_num + ':' + (5 - ji_num) + '</td></tr>';
            }
            html += '</tbody></table><p class="trendLink"><a href="#awardDetail">今日开奖</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a target="_blank" href="javascript:">历史查询</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a target="_blank" href="/Trend.jzh">走势图表</a></p>';
            $('#r_open').html(html);
        }
    }, 'json');
}

Right.LotOpen5 = function(s, n) { //彩种页面右边开奖（高频pk10）
    $.get('/mode/lotopen', { lot: s, t: local.TimeLong() }, function(data) {
        if (data.msg != 'no' && data.msg != 'err') {
            var html = '<div class="all_title gray_title"><div class="title_m f14">最新开奖</div></div>';
            var yijing = 0
            for (var j = 0; j < data.msg.length; j++) {
                yijing++
            }
            for (var i = 0; i < data.msg.length; i++) {
                var qihao = data.msg[i].lot_qihao;
                var qihaos = qihao
                var haoma = data.msg[i].lot_haoma;
                if (i == 0) {
                    var haomatemp = haoma.replaceAll(',', '</span>&nbsp;<span class="iconBred">');
                    html += '<div class="n_kjgg t_c"> ' + Show.LotName(s) + '  第 <em class="c_ba2636">' + qihao + '</em> 期 开奖<br><span class="iconBred">' + haomatemp + '</span></div>' +
                        '<table width="90%" border="0" align="center" cellspacing="0" cellpadding="0" class="index_table right_t_kj"><thead><tr><th width="30%">期号</th><th width="52%">开奖号码</th></tr></thead><tbody id="kj_list" class="ssckj_star1">';
                }
                var da_num = 0;
                var ji_num = 0;
                var dxjohm = haoma.split(',');
                for (var n_f in dxjohm) {
                    var htemp = Number(dxjohm[n_f]);
                    if (htemp > 5) { da_num += 1; }
                    if (htemp % 2 == 1) { ji_num += 1; }
                }
                var haomatemp = haoma.substr(0,12).replaceAll(',', '</span> <span class="ball_1">');
                html += '<tr><td>&nbsp;' + qihaos + '</td><td><em class="c_ba2636"><span class="ball_5">' + haomatemp + '</span></em></td></tr>';
            }
            html += '</tbody></table><p class="trendLink"><a href="#awardDetail">今日开奖</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a target="_blank" href="javascript:">历史查询</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a target="_blank" href="/Trend.jzh">走势图表</a></p>';
            $('#r_open').html(html);
        }
    }, 'json');
}

Right.LotZhong = function(s) { //彩种页面右边最新中奖
    $.get('/mode/newwin', { lot: s, t: local.TimeLong() }, function(data) {
        if (data.msg != 'no' && data.msg != 'err') {
            var html = '<div class="all_title gray_title"><div class="title_m f14">最新中奖</div></div><div class="latestWin_con" id="latestAwardList"><ul class="latestWin_list">';
            for (var i = 0; i < data.msg.length; i++) {
                var name = data.msg[i].user_name;
                html += i % 2 == 0 ? '<li class="even">' : '<li class="odd">';
                html += '<span class="winner">' + name.substr(0, 2) + '***</span><span class="wanfaName"></span><span class="awards">喜中<em class="c_ba2636">' + data.msg[i].buyuser_win + '元</em></span></li>';
            }
            html += '</ul></div>';
            $('#r_zhong').html(html);
            local.scrollMaquee('ul.latestWin_list', 5, 6, 200);
        }
    }, 'json');
}
var Mode = {};
Mode.News = function() { //首页新闻
    $('#topInfoTab li').click(function() {
        $(this).parent().children().removeClass("active");
        $(this).addClass("active");
        $('.promotionText>div').hide();
        var ind = $(this).index();
        if (2 == ind) {
            $('#tophelp').show();
        } else if (1 == ind) {
            if ($('#topForecast ul').text().length == 27) {
                Loaddata("gl");
            }
            $('#topForecast').show();
            Loaddata("xw");
        } else {
            $('#topNotice').show();
        }
    });
    Loaddata("gg");

    function Loaddata(t) {
        $.get('/news/lot', { type: t, t: local.TimeLong() }, function(data) {
            
            if (data.msg != 'no') {
                if (data.msg.length > 0) {
                    var html = '';
                   
                    for (var i = 0; i < data.msg.length; i++) {
                        
                        if (i <= 2) {
                            html += '<li>·<a target="_blank" href="/news/detail?id=' + data.msg[i].news_id + '" class="c_ba2636">' + data.msg[i].news_title + '</a></li>';
                        } else {
                            html += '<li>·<a target="_blank" href="/news/detail?id=' + data.msg[i].news_id + '">' + data.msg[i].news_title + '</a></li>';
                        }
                    }
                    if ('gg' == t) {
                        $('#topNotice>ul').html(html);
                    } else {
                        $('#topForecast>ul').html(html);
                    }
                }
            }
        }, 'json');
    }
}
Mode.PHB = function() { //排行榜
    $('#ranksTab dd').click(function() {
        var i = $(this).index();
        $('#ranksTab dd').removeClass('active');
        $(this).addClass('active');
        $('#ownerRanks,#totalRanks').hide();
        if (1 == i) { //单挑
            if ($('#totalRanks tbody').val().length <= 0) {
                Loaddata(0);
            }
            $('#totalRanks').show();
        } else { //合买
            $('#ownerRanks').show();
        }

    });
    Loaddata(2);

    function Loaddata(t) {
        $.post('/phb', { type: t, t: local.TimeLong() }, function(data) {
            if (data.msg != 'no') {
                var html = '<tr><th></th><th>用户名</th><th class="t_r">中奖金额</th><th>跟单</th></tr>';
                for (var i = 0; i < data.msg.length; i++) {
                    var phb_value = data.msg[i].phb_value > 20000000?'20000000.00': data.msg[i].phb_value ;
                    var cla = i % 2 == 1 ? '' : ' bgcolor="f7f7f7"';
                    var top = i <= 2 ? 'xh_top' : '';
                    html += '<tr' + cla + '><td><span class="' + top + '">' + (i + 1) + '</span></td><td><span class="nickNames"><a rel="nofollow" href="javascript:;" target="_blank">' + data.msg[i].user_nameDis + '</a></span></td><td class="t_r">' + phb_value + '</td><td><a rel="nofollow" target="_blank" href="javascript:;" class="dingzhi">定制</a></td></tr>';
                }
                if (t == 2) {
                    $('#ownerRanks tbody').html(html);
                } else {
                    $('#totalRanks tbody').html(html);
                }
            }
        }, 'json');
    }
}
Mode.Gonglue = function() { //首页攻略
    $.get('/news/lot', { type: "gl", t: local.TimeLong() }, function(data) {
        if (data.msg != 'no') {
            html = '';
            for (var i = 0; i < data.msg.length; i++) {
                html += '<li class="clearfix">·<a target="_blank" href="/news/detail?id=' + data.msg[i].news_id + '">' + data.msg[i].news_title + '</a></li>';
            }
            $('ul#Gonglue').html(html);
        }
    }, 'json');
}
Mode.NewWin = function() { //首页最新中奖
    $.get('/newwin', { t: local.TimeLong() }, function(data) {
        if (data.msg != 'no') {
            var html = '';
            for (var i = 0; i < data.msg.length; i++) {
                var name = data.msg[i].user_name;
                var lot = data.msg[i].buy_item;
                html += i % 2 == 0 ? '<li class="even">' : '<li class="odd">';
                html += '<span class="czname">[' + Show.LotName(lot) + ']</span><span class="winner">' + name.substr(0, 2) + '**</span><span class="awards" style="float:left;  text-align:left">' + Math.round(data.msg[i].buyuser_win * 100) / 100 + '元</span></li>';
            }
            $('ul#NewWin').html(html);
            local.scrollMaquee('ul.latestWin_list', 5, 4, 500);
        }
    }, 'json');
}
Mode.HM = function() { //首页合买
    var showlot = 'all';
    $('ul#groupbuyTab>li').click(function() {
        $('ul#groupbuyTab>li').removeClass('active');
        $(this).addClass('active');
        showlot = $(this).attr('rel');
        LoadData(showlot);
    });
    LoadData(showlot);

    function LoadData(l) {
        $.post('/groupbuy', { lot: l, t: local.TimeLong() }, function(data) {
            if (data.msg != 'no') {
                var html = '<tr><th class="t_c">状态</th><th>发起人</th><th>发起人战绩</th><th>彩种</th><th>进度 </th><th class="t_r"> 方案金额 | </th><th>剩余金额</th><th>认购金额</th><th class="t_c">操作</th></tr>';
                for (var i = 0; i < data.msg.length; i++) {
                    html += i % 2 == 1 ? '<tr bgcolor="f7f7f7">' : '<tr>';
                    var d = data.msg[i];
                    if (d.buy_isopen = '0') {
                        html += '<td class="t_c"><span class="openLock" title="公开方案"></span></td>'
                    } else if (d.buy_isopen = '1') {
                        html += '<td class="t_c"><span class="halfLock" title="参与可见"></span></td>'
                    } else if (d.buy_isopen = '2') {
                        html += '<td class="t_c"><span class="halfLock" title="截止可见"></span></td>'
                    } else {
                        html += '<td class="t_c"><span class="lock" title="永久保密"></span></td>'
                    }
                    html += '<td>' + d.user_nameDis + '</td>';
                    html += '<td>' + Show.UserLevel(d.user_level) + '</td>';
                    html += '<td>' + Show.LotName(d.buy_lot) + '</td>';
                    var bfb = Math.floor((d.buy_money - d.buy_have) * 100 / d.buy_money);
                    var bao = Math.floor(d.buy_baodi * 100 / d.buy_money);
                    html += '<td><p style="margin-top:-3px">' + bfb + '%';
                    if (bao > 0) {
                        html += '+' + bao + '%<span class="img_baodi" title="保底">保</span>';
                    }
                    html += '</p><p class="complete"><em style="width:' + bfb + '%"></em></p></td></td>';
                    html += '<td class="totalmoney"><em>' + d.buy_money + '元</em> |</td>';
                    html += '<td>' + d.buy_have + '元</td>';
                    if (d.buy_status == -1) {
                        html += '<td><input style="color:#000000" autocomplete="off" class="group_input" placeholder="剩余' + d.buy_have + '元" maxnum="' + d.buy_have + '"/></td><td class="t_r"><a class="buy_btn" lot="' + d.buy_lot + '" fqh="' + d.buy_fqihao + '" lotoorder="' + d.buy_item + '" href="javascript:;" rel="nofollow">购买</a>'
                    } else {
                        html += '<td>' + Show.BuyStatus(d.buy_status) + '</td><td class="t_r">';
                    }
                    html += '<a rel="nofollow" target="_blank" class="btn_mini" href="/buylot/detail?spm=' + d.buy_item + '">详情</a></td></tr>';
                }
                $('table.group_buy tbody').html(html);
                $('input.group_input').keyup(function() {
                    $(this).val($(this).val().replace(/[^\d]/g, ''));
                    var num = Number($(this).val() == 0 ? 1 : $(this).val());
                    var max = Number($(this).attr('maxnum'));
                    num = num > max ? max : num;
                    $(this).val(num);
                });
                var hmsta = false;
                $('a.buy_btn').click(function() {
                    if (hmsta) {
                        alert('正在提交数据，请稍等。');
                        return;
                    }
                    var thiz = $(this).parent().parent().find('input.group_input');
                    var item = $(this).attr('lotoorder');
                    var lot = $(this).attr('lot');
                    var fqh = $(this).attr('fqh');
                    var mon = Number(thiz.val());
                    var max = Number(thiz.attr('maxnum'));
                    if (mon == 0) {
                        alert('请输入您要认购的金额。');
                        return;
                    } else if (mon > max) {
                        alert('最多只能认购' + max + '元');
                    }
                    hmsta = true;
                    $.post('/buylot/addhm', { item: item, lot: lot, fqh: fqh, mon: mon, t: local.TimeLong() }, function(data) {
                        switch (data) { //-1期号过期 0成功 1订单剩余金额不够 2用户余额不足 3订单不存在
                            case 'no':
                                alert('您还未登录');
                                break;
                            case '-1':
                                alert('期号已过期。');
                                break;
                            case '0':
                                alert('购买成功。');
                                LoadData(showlot);
                                break;
                            case '1':
                                alert('订单金额不够。');
                                break;
                            case '2':
                                alert('您余额不足。');
                                break;
                            case '3':
                                alert('订单不存在。');
                                break;
                            default:
                                alert('购买失败！');
                                break;
                                hmsta = false;
                        }
                    });
                });
            } else {
                $('table.group_buy tbody').html('<tr><th class="t_c">状态</th><th>发起人</th><th>发起人战绩</th><th>彩种</th><th>进度 </th><th class="t_r"> 方案金额 | </th><th>剩余金额</th><th>认购金额</th><th class="t_c">操作</th></tr><tr bgcolor="f7f7f7"><td colspan="9" style="text-align:center;">暂无合买</td></tr>');
            }
        }, 'json');
    }
}
Mode.Open = function() { //全国开奖
    $('#awardListTab dd:not(end)').click(function() {
        var i = $(this).index();
        $('#awardListTab dd').removeClass('active');
        $(this).addClass('active');
        $('#awardList01,#awardList02').hide();
        if (1 == i) {
            $('#awardList01').show();
        } else if (2 == i) {
            $('#awardList02').show();
        }
    });
    $.post('/award', { t: local.TimeLong() }, function(data) {
        if (data.msg != 'no') {
            var szc = '';
            var gpc = '';
            for (var i = 0; i < data.msg.length; i++) {
                var typ = 0; //0高频 1数字
                var lot = data.msg[i].lot_name;
                var hm = data.msg[i].lot_haoma;
                switch (lot) {
                    case 'ss1':
                        typ = 1;
                        break;
                    case 'dlt':
                        typ = 1;
                        break;
                    case 'fc3d':
                        typ = 1;
                        break;
                    case 'pl3':
                        typ = 1;
                        break;
                    case 'pl5':
                        typ = 1;
                        break;
                    default:
                        break;
                }
                var hms = hm.split('+');
                var md = '<li><p><strong><a href="/lottery/' + lot + '" target="_blank">' + Show.LotName(lot) + '</a></strong>&nbsp;第' + data.msg[i].lot_qihao.substr(2) + '期</p><p class="clearfix"><em class="smallRedball">';
                md += hms[0].replaceAll(',', '</em><em class="smallRedball">');
                if (hms.length >= 2) {
                    md += '</em><em class="smallBlueball">' + hms[1].replaceAll(',', '</em><em class="smallBlueball">');
                }
                md += '';
                if (typ == 1) {
                    szc += md + '</em></p><p class="otherlink">' + data.msg[i].lot_etime.substring(0, 10) + '<a href="/Trend.jzh" target="_blank">走势</a>|	<a href="/lottery/' + lot + '" target="_blank">投注</a></p></li>';
                } else {
                    gpc += md + '</em></p><p class="otherlink">' + data.msg[i].lot_etime.substring(5, 16) + '<a href="/Trend.jzh" target="_blank">走势</a>|	<a href="/lottery/' + lot + '" target="_blank">投注</a></p></li>';
                }
                $('#awardList01').html(gpc);
                $('#awardList02').html(szc);
            }
        }
    }, 'json');
}