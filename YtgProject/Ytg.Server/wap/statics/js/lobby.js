
$(function () {
    //getLastResult();
   // setLeaveTimer();
    //getNextPeriod();
});

function getLastResult() {
    $.ajax({
        url: '/Page/Lott/lottery.aspx?action=opens',
        type: 'POST',
        dataType: 'json',
        data: {
        },
        timeout: 30000,
        success: function (data) {

           // console.info(JSON.stringify(data));
            if (data.Code!=0) {
                return;
            }
            if (data.Data == null || data.Data.length<1) {
                return;
            }
            var txtHtml = '';
            for (var i = 0; i < data.Data.length; i++) {
                var gid = data.Data[i].LotteryId;
                if (nowIds.indexOf(',' + gid + ',') == -1) {
                    continue;
                }
                var period = data.Data[i].IssueCode;
                var opennum = data.Data[i].Result;
                /*if (',41,42,'.indexOf(',' + gid + ',') > -1) {
                    if (opennum != undefined && opennum != '') {
                        opennum = opennum.split('|');
                        opennum = opennum[0] + ' + ' + opennum[1] + ' + ' + opennum[2] + ' = ' + opennum[3];
                    } else {
                        opennum = '正在开奖';
                    }
                } else {*/
                    opennum = (opennum == undefined || opennum == '') ? '正在开奖' : opennum.replace(/\|/g, ' ');
               // }
                if ('第' + period + '期' == $('span.last_period_' + gid).text()) {
                    $('p.last_open_' + gid).text(opennum);
                }
                //$('span.last_period_'+gid).text('第'+period+'期');
                //$('p.last_open_'+gid).text(opennum);
            }
        }
    });
}

//获取下一期相关数据
/*lefttimes = {
    1: '0', 2: '0', 3: '0', 4: '0', 5: '0', 7: '0', 9: '0',
    10: '0', 11: '0', 12: '0', 13: '0', 14: '0', 15: '0', 16: '0', 17: '0', 18: '0', 28: '0', 41: '0', 42: '0', 51: '0', 52: '0'
};*/
lefttimes = {
    1: '0', 4: '0', 5: '0', 6: '0', 7: '0', 8: '0', 9: '0', 11: '0', 12: '0', 13: '0', 14: '0', 17: '0', 18: '0', 20: '0', 22: '0', 23: '0', 24: '0', 25: '0', 26: '0'
};

function getNextPeriodNew(all) {
    //{"Code":0,"Data":[{"LotteryId":9,"LotteryCode":"排列三、五","IssueCode":"2017305","EndSaleTime":"2017-11-08 20:20:00","EndTime":"2017-11-08 20:34:00","Result":null,"SubTime":-9000000000},{"LotteryId":20,"LotteryCode":"江西11选5","IssueCode":"2017110702","EndSaleTime":"2017-11-07 09:18:40","EndTime":"2017-11-07 09:20:40","Result":null,"SubTime":5200000000},{"LotteryId":14,"LotteryCode":"河内时时彩","IssueCode":"20171107017","EndSaleTime":"2017-11-07 01:23:40","EndTime":"2017-11-07 01:25:40","Result":"9,8,5,9,2","SubTime":2200000000},{"LotteryId":1,"LotteryCode":"重庆时时彩","IssueCode":"20171107017","EndSaleTime":"2017-11-07 01:24:40","EndTime":"2017-11-07 01:25:40","Result":"6,8,3,9,5","SubTime":2800000000},{"LotteryId":12,"LotteryCode":"幸运三分彩","IssueCode":"1107030","EndSaleTime":"2017-11-07 01:29:55","EndTime":"2017-11-07 01:30:00","Result":null,"SubTime":1750000000},{"LotteryId":17,"LotteryCode":"三分11选5","IssueCode":"1107030","EndSaleTime":"2017-11-07 01:29:55","EndTime":"2017-11-07 01:30:00","Result":"07,06,03,10,09","SubTime":1750000000},{"LotteryId":26,"LotteryCode":"北京PK10","IssueCode":"649380","EndSaleTime":"2017-11-07 09:10:50","EndTime":"2017-11-07 09:12:00","Result":null,"SubTime":2300000000},{"LotteryId":7,"LotteryCode":"福彩3D","IssueCode":"2017305","EndSaleTime":"2017-11-08 21:00:00","EndTime":"2017-11-08 21:14:00","Result":null,"SubTime":-9000000000},{"LotteryId":8,"LotteryCode":"上海时时乐","IssueCode":"2017110702","EndSaleTime":"2017-11-07 10:58:00","EndTime":"2017-11-07 11:01:00","Result":null,"SubTime":16600000000},{"LotteryId":22,"LotteryCode":"江苏快三","IssueCode":"20171107002","EndSaleTime":"2017-11-07 08:47:00","EndTime":"2017-11-07 08:50:40","Result":null,"SubTime":4200000000},{"LotteryId":25,"LotteryCode":"埃及五分彩","IssueCode":"20171107019","EndSaleTime":"2017-11-07 01:29:00","EndTime":"2017-11-07 01:30:01","Result":null,"SubTime":2400000000},{"LotteryId":5,"LotteryCode":"天津时时彩","IssueCode":"20171107002","EndSaleTime":"2017-11-07 09:18:00","EndTime":"2017-11-07 09:20:40","Result":null,"SubTime":4800000000},{"LotteryId":6,"LotteryCode":"广东11选5","IssueCode":"2017110702","EndSaleTime":"2017-11-07 09:18:00","EndTime":"2017-11-07 09:21:40","Result":null,"SubTime":4200000000},{"LotteryId":23,"LotteryCode":"印尼时时彩","IssueCode":"20171107017","EndSaleTime":"2017-11-07 01:23:40","EndTime":"2017-11-07 01:25:40","Result":"9,5,8,8,1","SubTime":2200000000},{"LotteryId":24,"LotteryCode":"埃及二分彩","IssueCode":"20171107046","EndSaleTime":"2017-11-07 01:29:40","EndTime":"2017-11-07 01:30:00","Result":null,"SubTime":1000000000},{"LotteryId":13,"LotteryCode":"埃及分分彩","IssueCode":"201711070091","EndSaleTime":"2017-11-07 01:29:50","EndTime":"2017-11-07 01:30:00","Result":null,"SubTime":500000000},{"LotteryId":4,"LotteryCode":"新疆时时彩","IssueCode":"20171106092","EndSaleTime":"2017-11-07 01:18:00","EndTime":"2017-11-07 01:20:40","Result":"1,4,1,5,7","SubTime":4800000000},{"LotteryId":11,"LotteryCode":"幸运分分彩","IssueCode":"11070091","EndSaleTime":"2017-11-07 01:29:50","EndTime":"2017-11-07 01:30:00","Result":null,"SubTime":500000000},{"LotteryId":18,"LotteryCode":"五分11选5","IssueCode":"1107018","EndSaleTime":"2017-11-07 01:29:55","EndTime":"2017-11-07 01:30:00","Result":"07,06,03,10,09","SubTime":2950000000}],"Page":0,"Total":0,"ErrMsg":"","ResponseTime":"2017-11-07 01:30:11"}
    var param = '';
    for (var k in lefttimes) {
        if (all != 1 && lefttimes[k] > 1) {
            // console.info('continue');
            continue;
        }
        if (param != '') {
            param += ',';
        }
        param += k;
    }
    $.ajax({
        url: '/Page/Lott/LotteryBetDetail.aspx?action=nextperiodnew',
        type: 'POST',
        dataType: 'json',
        data: {
            'gid': param
        },
        timeout: 30000,
        success: function (data) {
            if (data.Code != 0) {
                return;
            }
            if (data.Data.length > 0) {
               
                for (var j = 0; j < data.Data.length; j++) {
                    var item = data.Data[j];

                    var gid = item.LotteryId;
                    if (nowIds.indexOf(',' + gid + ',') == -1) {
                        continue;
                    }
                    if (item.Result == null) {
                        $('p#hot_start_' + gid).css("visibility", "visible");
                        $('span.last_period_' + gid).text('即将开奖...');
                        $('span.last_period_' + gid).addClass("start_period");
                        $('span.last_period_' + gid).addClass("last_per_iod_" + gid);
                        $('span.last_per_iod_' + gid).removeClass('last_period_' + gid);
                        $('span#period_show_status_' + gid).text("开奖");
                        $('p.last_open_' + gid).text("正在开奖");
                    } else {
                        $('p#hot_start_' + gid).css("visibility", "hidden");
                        if ($('span.last_period_' + gid).length == 1) {
                            $('span.last_period_' + gid).text('第' + item.IssueCode + '期');
                        } else {
                            $('span.last_per_iod_' + gid).removeClass("start_period");
                            $('span.last_per_iod_' + gid).addClass("last_period_" + gid);
                            $("span.last_period_" + gid).removeClass('last_per_iod_' + gid);
                            $('span.last_period_' + gid).text('第' + item.IssueCode + '期');
                        }
                        $('span#period_show_status_' + gid).text("截止");
                        $('p.last_open_' + gid).text(item.Result);
                    }
                    $('span.period_show_' + gid).text((item.IssueCode+1));
                }

                setLeaveTimer_new();
            }
        }
    });
}

//启动定时器
function setLeaveTimer_new() {

}

var isCanGetNext = true;
function getNextPeriod(all) {
  
    if (!isCanGetNext) {
        return;
    }
    
    isCanGetNext = false;
    var param = '';
    for (var k in lefttimes) {
        if (all != 1 && lefttimes[k] > 1) {
           // console.info('continue');
            continue;
        }
        if (param != '') {
            param += ',';
        }
        param += k;
    }
   // console.info(param);
    $.ajax({
        url: '/Page/Lott/LotteryBetDetail.aspx?action=nextperiod',
        type: 'POST',
        dataType: 'json',
        data: {
            'gid': param
        },
        timeout: 30000,
        success: function (data) {
            if (data.Code != 0) {
                return;
            }
            if (data.Data.length > 0) {
                for (var j = 0; j < data.Data.length; j++) {
                    var gid = data.Data[j].iGameId;
                    if (nowIds.indexOf(',' + gid + ',') == -1) {
                        continue;
                    }
                    //$('div.erect-right-'+gid).data('time', data.Records[j].iCloseTime);
                    if (data.Data[j].iStartTime > 0) {
                        lefttimes[gid] = data.Data[j].iStartTime;
                        $('p#hot_start_' + gid).css("visibility", "visible");
                        $('span.last_period_' + gid).text('即将开奖...');
                        $('span.last_period_' + gid).addClass("start_period");
                        $('span.last_period_' + gid).addClass("last_per_iod_" + gid);
                        $('span.last_per_iod_' + gid).removeClass('last_period_' + gid);
                        $('span#period_show_status_' + gid).text("开奖");
                    } else {
                        lefttimes[gid] = data.Data[j].iCloseTime;
                        $('p#hot_start_' + gid).css("visibility", "hidden");
                        if ($('span.last_period_' + gid).length == 1) {
                            $('span.last_period_' + gid).text('第' + data.Data[j].sGamePeriodPrior + '期');
                        } else {
                            $('span.last_per_iod_' + gid).removeClass("start_period");
                            $('span.last_per_iod_' + gid).addClass("last_period_" + gid);
                            $("span.last_period_" + gid).removeClass('last_per_iod_' + gid);
                            $('span.last_period_' + gid).text('第' + data.Data[j].sGamePeriodPrior + '期');
                        }
                        $('span#period_show_status_' + gid).text("截止");
                    }
                    $('span.period_show_' + gid).text(data.Data[j].sGamePeriod);
                    //$('div.erect-right-'+gid+' span.period_show').text(data.Records[j].sGamePeriod);
                    //上一期信息

                    var lastnum = data.Data[j].sOpenNumPrior;
                   /* if (',41,42,'.indexOf(',' + gid + ',') > -1) {
                        if (lastnum != undefined && lastnum != '') {
                            lastnum = lastnum.split('|');
                            lastnum = lastnum[0] + ' + ' + lastnum[1] + ' + ' + lastnum[2] + ' = ' + lastnum[3];
                        } else {
                            lastnum = '正在开奖';
                        }
                    } else {*/
                        lastnum = (lastnum == undefined || lastnum == '') ? '正在开奖' : lastnum.replace(/\|/g, ' ');
                    //}
                    if (data.Data[j].iStartTime > 0) {
                        $('p.last_open_' + gid).html("&nbsp;");
                    } else {
                        $('p.last_open_' + gid).text(lastnum);
                    }
                }
                if (all != 1) {
                    setLeaveTimer();
                }
            } else {
                //alert('错误信息:' + data.Desc);
            }
            isCanGetNext = true;
            if (firstPage != 1) {
                loaded();
            }
            getLastResult();
        }
    });
}

runSec = 1;//定时器运行了多少秒
objList = '';//时间显示jqery对象
timeLeave = 0; //倒计时剩余时间
localTime = 0;//最后一次倒计时的本机时间（毫秒）
isTimerStarted = false;//是否启动了定时器
function setLeaveTimer() { //启动定时，进行倒计时。
    if (isTimerStarted) {
        return;
    }
    isTimerStarted = true;
    function format(dateStr) { //格式化时间
        return new Date(dateStr.replace(/[\-\u4e00-\u9fa5]/g, "/"));
    }

    function fftime(n) {
        return Number(n) < 10 ? "" + 0 + Number(n) : Number(n);
    }

    function diff(t) { //根据时间差返回相隔时间
        return t > 0 ? {
            day: Math.floor(t / 86400),
            hour: Math.floor(t / 3600),
            minute: Math.floor(t % 3600 / 60),
            second: Math.floor(t % 60)
        } : {
            day: 0,
            hour: 0,
            minute: 0,
            second: 0
        };
    }

    //timeLeave = 0//倒计时总秒数
    clearInterval(timerno);//删除定时器
    var timerno = window.setInterval(function () {
        if (firstPage != 1 && runSec < 271) {
            if (runSec % 50 == 0) {
                getLastResult();
            }
            runSec++;
        }
        var tmpLocalTime = new Date().getTime() + 0;//本机毫秒数
       // console.info(localTime + "    (tmpLocalTime - localTime)>3000" + (tmpLocalTime - localTime));
        if (localTime > 0 && (tmpLocalTime - localTime) > 3000) {//倒计时已不准确，重新获取倒计时
           // console.info('ddd');
            getNextPeriod(1);
        }
        localTime = tmpLocalTime;
        //对所有倒计时遍历，重新赋值
        if (objList == undefined || objList == '') {
            objList = (firstPage == 1) ? $('div.hot-list-text') : $('div.erect-right');
        }
        for (var i = 0; i < objList.length; i++) {
            //var tmpTimeLeave = $(objList[i]).data('time');
            var tmpGid = $(objList[i]).data('gid');
            if (nowIds.indexOf(',' + tmpGid + ',') == -1) {
                continue;
            }
            var tmpTimeLeave = lefttimes[tmpGid];
            if (tmpTimeLeave > -1) {
                tmpTimeLeave--;
            }
            lefttimes[tmpGid] = tmpTimeLeave;
            //  console.info("isCanGetNext=" + isCanGetNext + "tmpGid=" + tmpGid + "tmpTimeLeave=" + tmpTimeLeave);
            //console.info(isCanGetNext + "   tmpTimeLeave=" +parseInt(tmpTimeLeave));
            if (isCanGetNext && parseInt(tmpTimeLeave) == 0) {
                
               // console.info("ccc");
                //getNextPeriod();
                getLastResult();
                continue;
            }
            tmpTimeLeave = (tmpTimeLeave == undefined || tmpTimeLeave == '') ? 0 : tmpTimeLeave;
            var oDate = diff(tmpTimeLeave);
            var ahour = fftime(oDate.hour).toString().split("");
            var aminute = fftime(oDate.minute).toString().split("");
            var asecond = fftime(oDate.second).toString().split("");
            var timeShow = ahour[0] + ahour[1] + ':' + aminute[0] + aminute[1] + ':' + asecond[0] + asecond[1];
            //$(objList[i]).data('time', tmpTimeLeave);
            //lefttimes[tmpGid] = tmpTimeLeave;
            if (typeof (showLeaveTime) == "function") {//eval(
                showLeaveTime(tmpGid, timeShow);
                continue;
            }
            if (firstPage == 1 || gameShowType == 1) {//列表显示
                $('span.time_show_1_' + tmpGid).text(timeShow);
                //$('div.erect-right-'+tmpGid+' span.time_show').text(timeShow);
            } else {//宫格显示
                $('span.time_show_2_' + tmpGid).text(timeShow);
            }
        }
    }, 1000);
}