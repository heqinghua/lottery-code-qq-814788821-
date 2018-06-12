
var socket = null; //socket对象
var showChatPannel = false;         //聊天面板是否展开
var curContact = null;//当前联系人
var chatRecordArr = {};//保存用户的聊天记录
var userQueue = new Array();        //用户消息队列
var userUnReadMsgObj = {};                //每个用户的未读消息对象
var userHistoryMsgObj = {};               //每个用户的历史消息对象
var userjson = null;                      //用户列表json数据
var currentUserID = null;                   //当前用户id
var currentUserName = null;                 //当前用户名称
var host = "http://110.80.136.205:3000";
var domainName ="http://36.250.66.248:90";     //域名

/* 客户端socket对象 */
var clientSocket = (function () {
    var clientobj = {};
    
    clientobj.connet = function () {
        
        socket = io.connect(host);
        
        //监听连接事件
        socket.on("connection", function (data) {
            if (data.sucess) {
                $(".state_1 #serverstate").text("（已连接到服务器）");

                var socketid = data.socketid; //服务器socketid

                //获取登录用户ID
                var userid = userjson["self"];
                if (!userid) {
                    alert("未获取到当前用户id");
                    return;
                }
                
                //发送用户登录标志
                socket.emit("onLoginFlag", { userid: currentUserID, svrsocketid: socketid });
            }
            
            //在线用户
            var users = data.onlineUsers;
            if (users != null && users != undefined) {
                for (var i in users) {
                    var uid = users[i];
                    $(".tpl_list ul li a[uid='" + uid + "']").addClass("online");
                }

                //对用户列表排序
                sortUserList();
            }
        });

        //断开连接事件
        socket.on("disconnect", function () {
            $(".state_1 #serverstate").text("（与服务器断开连接）");
        });
        
        //重新连接事件
        socket.on("reconnect", function () {
            $(".state_1 #serverstate").text("（已连接到服务器）");
        });
        
        
        //监听自定义错误信息
        socket.on("err", function (data) {
            //alert(data.error);
        });
        
        //监听系统错误信息
        socket.on("error", function (data) {
            //alert("system error:"+data);
        });
        
        //监听接收服务器的消息
        socket.on('message', function (data) {
            var from = data.from;
            
            //记录历史消息
            if (userHistoryMsgObj[from] == undefined || userHistoryMsgObj[from] == null) {
                userHistoryMsgObj[from] = new Array();
            }
            userHistoryMsgObj[from].push(data);
            
            //如果接收到消息的发送人和当前的聊天人不一致，则将消息放入未读消息面板中
            if (!showChatPannel || from != curContact) {
                
                //如果此用户的消息队列不存在
                if (userUnReadMsgObj[from] == undefined || userUnReadMsgObj[from] == null) {
                    userQueue.push(from);               //将用户名称压入队列末尾
                    userUnReadMsgObj[from] = new Array();
                }
                userUnReadMsgObj[from].push(data);
                
                updateUnReadPannel();                   //更新未读消息面板
            }
            else {
                showMessage(from);
            }
            
            $(".tpa_t span").html("用户 " + data.fromname + "在线");

            setChatStatus(data);        //设置聊天状态
        });
        
        //监听接收服务器的消息
        socket.on('clickcontact', function (data) {
            var to = $("#hideInfo #to").val();
            var toname = $("#hideInfo #toname").val();
            if (data) {
                $(".tpa_t span").html("用户 " + toname + "在线");
            } else {
                $(".tpa_t span").html("用户 " + toname + "处于离线状态");
            }
        });
        
        //用户上线通知
        socket.on("notifyonline", function (data) {
            if (data.online) {
                $(".tpl_list ul li a[uid='" + data.userid + "']").addClass("online");
            }
            else {
                $(".tpl_list ul li a[uid='" + data.userid + "']").removeClass("online");
            }

            //对用户列表排序
            sortUserList();
        });

        //对用户列表排序
        var sortUserList = function(){
            var onlineUsers = "";
            $("#downgrade li a[class='online']").each(function () {
                $("#downgrade").prepend($(this).parent().clone(true));
                $(this).parent().remove();
            });

        };

    };

    return clientobj;
}());

/*****************************************************************************/




$(function () {
    
    //当前用户信息
    var userinfo = $("#onsuserlist").html();
    userjson = eval("(" + userinfo + ")"); //转换为json
    
    var downgrade = userjson["downgrade"];  //下级列表
    var upgrade = userjson["upgrade"];      //上级列表

    //解析用户列表
    parseUserList(downgrade, upgrade);
    
    //页面加载完成后显示聊天面板
    //$(".talk_pop_list").show();
    $(".tbp_show_bt").addClass("tbp_hide_bt");
    
    //设置登录用户ID
    currentUserID = userjson["self"];

    //获取，并设置用户名
    currentUserName = $(".head_account").html();

    $(".state_1 #loginname").text(currentUserName);
    $(".state_p a span").html(currentUserName);
    
    if (currentUserID) {
        clientSocket.connet();
    }
    
    //选择聊天对象
    $(".tpl_list ul li a").each(function (i) {
            $(this).live("click",function () {
            
                var selectuserid = $(this).attr("uid");     //消息接收人

                socket.emit("clickcontact", selectuserid); //判断当前联系人是否在线

                var from = currentUserID;                 //消息发送人
                var to = $(this).attr("uid");             //消息接收人
                var toname = $(this).attr("uname");       //消息接收人名称
                //var leader = $(this).attr("leader");    //是否为上级代理
                var chat = new Chat(from, to, currentUserName, toname, "", 0, 0);  //创建聊天对象
                setChatStatus(chat);                       //设置聊天状态
                showMessage(selectuserid);
            });
    });
    
    //发送内容
    $("#sendMsgBtn").click(function () {
        if (curContact == null) {
            alert("请选择联系人。");
            return;
        }
        
        var chat = getChat();                  //生成消息对象
        socket.emit('message', chat);
        updateChatPannelBeforeSend(chat);       //更新聊天记录面板
        $(".textarea_con #content").val("");    //清空输入框
        
        //将发送的消息放入历史消息队列
        if (userHistoryMsgObj[chat.to] == null || userHistoryMsgObj[chat.to] == undefined) {
            userHistoryMsgObj[chat.to] = new Array();
        }
        
        userHistoryMsgObj[chat.to].push(chat);

    });
    
    
    //表情点击事件
    $(".exp_list table img").each(function () {
        $(this).click(function () {
            var src = $(this).attr("src");
            var chat = getChat();               //生成消息对象
            chat.chatType = 1;                  //设置消息类型为图片
            chat.message = src;
            socket.emit('message', chat);
            //showMessage(chat);   //更新聊天记录面板
            $(".exp_list").hide();
            
            //显示消息            
            var msg = combineMsgList(chat);
            $(".chat_record").append(msg);
            
            userHistoryMsgObj[chat.to].push(chat);
        });
    });
    
    //点击未读消息
    $(".infor_show a").click(function () {
        
        var userid = userQueue.shift();          //返回用户队列的第一个元素并从队列中删除。
        if (userid != null && userid != undefined) {
            var msglist = userUnReadMsgObj[userid];    //用户消息列表
            
            var firstchat = msglist[0];          //获取第一未读条消息
            
            setChatStatus(firstchat);                 //设置联系人状态
            
            showMessage(userid);                //将消息显示在聊天面板中
            
            updateUnReadPannel();               //更新未读消息面板
        }
    });
    
    //隐藏聊天面板
    $(".tpa_t a").click(function () {
        $(".tp_alert").hide();               //显示聊天面板
        curContact = null;
        showChatPannel = false;               //将显示聊天面板标记设为false
    });
    
    /********************************************************************/
    //获取推广连接地址
    $(".tpll_gen").click(function () {
        var tuiguangurl = "/tuiguang/link?u="+currentUserID+"&r=" + Math.random();
        $.get(tuiguangurl, function (data) {
            if(data =="false")
            {
                alert("推广链接错误");
                return;
            }

            var tuiguangurl =domainName+"/tuiguang/client/" + data;
            $(".prom_con p").html(tuiguangurl);
            $(".promotion_code").show();
        });
    });
    
    //复制推广连接
    $(".prom_copy_bt a").click(function () {
        var link = $(".prom_con p").html();
        copyUrl(link);
    });
    /**************************************************************************/
    
    //点击上传按钮
    $("#uploadBtn").click(function () {
        
     var uploadurl = "/upload";
     $.ajaxFileUpload({
            url: uploadurl,//处理图片脚本
            secureuri : false,
            fileElementId : 'imgupload',//file控件id
            dataType : 'json',
            type	: 'post',
            data    : null,
            success : function (data, status) {
                
                var from = $("#hideInfo #from").val();
                var fromname = $("#hideInfo #fromname").val();

                var to = $("#hideInfo #to").val();
                var toname = $("#hideInfo #toname").val();

                var chat = new Chat(from, to, fromname,toname, data.filename, 1, 0);
                $(".upload_pop").hide();
                
                socket.emit('message', chat);
                updateChatPannelBeforeSend(chat);       //更新聊天记录面板
                
                //将发送的消息放入历史消息队列
                if (userHistoryMsgObj[chat.to] == null || userHistoryMsgObj[chat.to] == undefined) {
                    userHistoryMsgObj[chat.to] = new Array();
                }
                userHistoryMsgObj[chat.to].push(chat);

            },
            error: function (data, status, e) {
                alert("error:" + JSON.stringify(status));
            }
        });
    });

});

// 列表解析：downgrade 下级 upgrade 上级
var parseUserList = function (downgrade, upgrade) {
    
    var downgradehtml = "";
    if (downgrade) {
        $("#downgradeUserCount").html(downgrade.length);
        for (var i in downgrade) {
            var item = downgrade[i];
            var uid = item["userid"];
            var uname = item["username"];
            //<li><a href="#"   uid="1003" uname="name1003">飞龙在天</a></li>
            downgradehtml += "<li><a href=\"# \"   uid='" + uid + "' uname='" + uname + "' leader='false'>" + uname + "</a></li>";
        }
        
        $("#downgrade").html(downgradehtml);
    }
    
    
    var upgradehtml = "";
    if (upgrade) {
        $("#upgradeUserCount").html(upgrade.length);
        for (var i in upgrade) {
            var item = upgrade[i];
            
            var uid = item["userid"];
            var uname = item["username"];
            var nickname = item["nickname"];
            //upgradehtml += "<li><a href=\"# \"   uid='" + uid + "' uname='" + uname + "'>" + uname + "</a></li>";
            upgradehtml += "<li><a href=\"# \"   uid='" + uid + "' uname='" + nickname + "' leader = 'true'>"+ nickname+"</a></li>";
        }
        
        $("#upgrade").html(upgradehtml);
    }
}

//获取一个聊天对象实例
var getChat = function () {
    var content = $(".textarea_con #content").val();   //消息内容
    
    var from = $("#hideInfo #from").val();             //发送人
    var fromname = $("#hideInfo #fromname").val();             //发送人
    
    var to = $("#hideInfo #to").val();                 //接收人
    var toname = $("#hideInfo #toname").val();     //接收人

    var chat = new Chat(from, to, fromname, toname, content, 0, 0);        //生成消息对象
    
    return chat;
};

// 兼容所有浏览器
var copyUrl = function (url) {
    if (navigator.userAgent.toLowerCase().indexOf('ie') > -1) {
        clipboardData.setData('Text', url);
        alert("该地址已复制到剪切板！");
    } else {
        prompt("非IE内核浏览器，请复制以下地址：", url);
    }
};

//设置聊天状态
var setChatStatus = function (chat) {
    if (chat.direct == 0) {  // 消息发送
        $("#hideInfo #from").val(chat.from);  //消息来自哪里
        $("#hideInfo #fromname").val(chat.fromname);  //消息来自哪里

        $("#hideInfo #to").val(chat.to);      //消息发送给某人
        $("#hideInfo #toname").val(chat.toname);      //消息发送给某人
        
        curContact = chat.to;
    }
    else if (chat.direct == 1) {    //消息接收
        $("#hideInfo #from").val(chat.to);     //消息来自哪里
        $("#hideInfo #fromname").val(chat.toname);     //消息来自哪里

        $("#hideInfo #to").val(chat.from);     //消息发送给某人
        $("#hideInfo #toname").val(chat.fromname);     //消息发送给某人

        curContact = chat.from;
    }
};

//切换联系人,chat聊天对象， direction消息的方向，0：表示发送，1：表示接收
var changeContact = function (chat, direction) {
    
    if (direction == 0) {
        $("#hideInfo #from").val(chat.from);            //消息来自哪里
        $("#hideInfo #to").val(chat.to);                //消息发送给某人
        $("#hideInfo #toname").val(chat.toname);       //消息发送给某人
    }
    else {
        $("#hideInfo #from").val(chat.to);              //消息来自哪里
        $("#hideInfo #to").val(chat.from);              //消息发送给某人
        $("#hideInfo #toname").val(chat.fromname);     //消息发送给某人
    }
};

//显示聊天内容
var showMessage = function (userid) {
    var content = "";
    var chatlist = userHistoryMsgObj[userid]; //获取该用户的消息队列
    if (chatlist != null) {
        for (var i in chatlist) {
            content += combineMsgList(chatlist[i]);
        }
    }
    
    $(".tp_alert").show();               //显示聊天面板
    showChatPannel = true;               //将显示聊天面板标记设为true
    
    $(".chat_record").html(content);
};

//发送消息前更新聊天面板
var updateChatPannelBeforeSend = function (chat) {
    var mymsg = "<div>";
    mymsg += "<span style='color:#2b8fe5;font-size:12px;'>" + chat.fromname + "</span>  ";
    mymsg += "<span style='color:#c2c2c2;font-size:12px;'>" + chat.time + "</span><br/>";
    if (chat.chatType == 0) {
        mymsg += "<p style='color:#333'>" + chat.message.HTMLEncode() + "</p>";
    }
    else if (chat.chatType == 1) {
        var imgurl = domainName+chat.message;
        mymsg += "<p><img src='" +imgurl + "'/></p>";
    }
    mymsg += "</div>";
    
    $(".chat_record").append(mymsg);
};

//接收消息后更新聊天面板
var updateChatPannelAfterReceive = function (chat) {
    $(".tp_alert").show();               //显示聊天面板
    showChatPannel = true;               //将显示聊天面板标记设为true
    var mymsg = combineMsgList(chat);    //拼接消息列表
    
    $(".chat_record").append(mymsg);
};

//更新未读消息面板
var updateUnReadPannel = function () {
    var usercount = userQueue.length;
    $(".infor_show a").html("您有" + usercount + "个人的未读消息");
};


//拼接消息列表 chat:聊天对象，direct 消息方向 0发送消息  1接收消息
var combineMsgList = function (chat) {
    var mymsg = "<div>";
    if (chat.direct == 0) {
        mymsg += "<span style='color:#2b8fe5;font-size:12px;'>" + chat.fromname + "</span>  ";
    }
    else if (chat.direct == 1) {
        mymsg += "<span style='color:#c7cd19;font-size:12px;'>" + chat.fromname + "</span>  ";
    }
    mymsg += "<span style='color:#c2c2c2;font-size:12px;'>" + chat.time + "</span><br/>";
    
    if (chat.chatType == 0) {
        mymsg += "<p style='color:#333'>" + chat.message.HTMLEncode() + "</p>";
    } else if (chat.chatType == 1) {
        mymsg += "<p><img src='" + chat.message + "'/></p>";
    }
    
    mymsg += "</div>";
    return mymsg;
};

//聊天对象 from:发送人id，接收人id，发送人名称，接收人名称，消息内容，聊天内容，消息方向
var Chat = function (from, to,fromname,toname, message, chatType, direct) {
    this.from = from;           //消息发送人
    this.to = to;               //消息接收人
    this.fromname = fromname;   //消息发送人名称
    this.toname = toname;       //消息接收人名称
    //this.leader = isleader;        //是否为上级代理
    this.message = message;     //消息内容
    this.chatType = chatType;          //消息类型：0表示文本（默认），1表示图片
    this.time = getCurrentTime();       //消息发送时间
    this.direct = direct;               //消息的方向，0表示发送（默认），1表示接受
};


//获取当前的系统时间
var getCurrentTime = function () {
    var myDate = new Date();
    var year = myDate.getYear();      //获取当前年份(2位)  
    var fullYear = myDate.getFullYear(); //获取完整的年份(4位,1970-????)  
    var month = myDate.getMonth();      //获取当前月份(0-11,0代表1月)  
    var date = myDate.getDate();      //获取当前日(1-31)  
    var day = myDate.getDay();        //获取当前星期X(0-6,0代表星期天)  
    var time = myDate.getTime();      //获取当前时间(从1970.1.1开始的毫秒数)  
    var hour = myDate.getHours();      //获取当前小时数(0-23)  
    var minutes = myDate.getMinutes();    //获取当前分钟数(0-59)  
    var seconds = myDate.getSeconds();    //获取当前秒数(0-59)  
    var milliseconds = myDate.getMilliseconds(); //获取当前毫秒数(0-999)  
    var localdate = myDate.toLocaleDateString();    //获取当前日期  
    var mytime = myDate.toLocaleTimeString();    //获取当前时间  
    var local = myDate.toLocaleString();      //获取日期与时间----如果涉及到时分秒，直接使用即可。
    var time = fullYear + "-" + (month + 1) + "-" + date + " " + hour + ":" + minutes + ":" + seconds;
    return time;
};

//转移html字符
String.prototype.HTMLEncode = function () {
    var temp = document.createElement("div");
    (temp.textContent != null) ? (temp.textContent = this) : (temp.innerText = this);
    var output = temp.innerHTML;
    temp = null;
    return output;
};
