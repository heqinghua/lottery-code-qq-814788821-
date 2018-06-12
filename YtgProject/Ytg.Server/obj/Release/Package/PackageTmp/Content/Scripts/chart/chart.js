var ws;
var url = "ws://127.0.0.1:4534";
var loadcontactData;
Ytg.Chart = {
    Connection: function () {
        return;
        if ("WebSocket" in window) {
            ws = new WebSocket(url);
        }
        else if ("MozWebSocket" in window) {
            ws = new MozWebSocket(url);
        }
        else {
            //alert("浏览器版本过低，请升级您的浏览器。\r\n浏览器要求：IE10+/Chrome14+/FireFox7+/Opera11+");
            return false;
        }
        var tmsIns;
        //注册各类回调
        ws.onopen = function () {
            //  $("#msg").append("连接服务器成功<br/>");
            tmsIns=setTimeout(function () {
                Ytg.Chart.getContact();//获取联系人
                clearInterval(tmsIns);
            }, 2000);
        }
        ws.onclose = function () {
            //$("#msg").append("与服务器断开连接<br/>");
        }
        ws.onerror = function () {
            //$("#msg").append("数据传输发生错误<br/>");
        }
        ws.onmessage = function (receiveMsg) {
           
            console.info(receiveMsg);
            var jsonData = JSON.parse(receiveMsg.data);
            switch (jsonData.Type) {
                case "CONTACT":
                    contact(jsonData.Data);
                    break;
                case "CHART":
                    chartMsg(jsonData.Data);
                    disp_opening();//播放提示音
                    break;
                case "STCHANGE"://更新用户状态
                    updateUserState(jsonData.Data);
                    break;
            }
        }
        return true;
    },
    chartSend: function (id, msg) {
        var sendMsg = { "Content": msg, "TUid": id };
        ws.send("CHART:" +JSON.stringify(sendMsg));
    },
    getContact: function () {
        ws.send("CONTACT:" + Ytg.common.user.info.user_id);//获取联系人
    }
};

function contact(contactData) {
   // $("#customer").children().remove();
    //添加在线客服
    loadcontactData = contactData;//联系人
    var cusCount=1;
    var parCount = 0;
    var childLine = 0;
    var childrenCount = 0;
    var jsonArray = JSON.parse(contactData);
    for (var i = 0; i < jsonArray.length; i++) {
        var item = jsonArray[i];
        if (item.NikeName == "")
            continue;
        var url = "http://cfapu.img48.wal8.com/img48/545266_20160510012323/146281905938.png";
        if (item.IsLogin == true)
            url = "http://cfapu.img48.wal8.com/img48/545266_20160510012323/146281906004.png";
        var norreadclss = "";
        var noreadstr = "";
        
        for (var x = 0; x < noReadMessageArray.length; x++) {
            var noReadMessage = noReadMessageArray[x];
            if (noReadMessage.TUid == item.id) {
                item.noread++;
            }
        }

        if (item.noread > 0) {
            norreadclss = "noread";
            noreadstr = "&nbsp;(" + item.noread + ")";
        }

        if (item.ParentId == Ytg.common.user.info.user_id) {//子
          
            childrenCount++;
            var cdHtml = "<dd style='display:none;' code='" + item.NikeName + "' tag='" + item.id + "'><img src=\"" + url + "\" id='img_" + item.id + "'> <a href='javascript:openChart(" + item.id + ",\"" + item.NikeName + "\"," + item.IsLogin + ")'>" + item.NikeName + "</a><span class='" + norreadclss + "' id='noread_count_spane_" + item.id + "'>" + noreadstr + "</span></dd>";
            if (!item.IsLogin ) {
                $("#mychildrens").append(cdHtml);
            }
            else {
                childLine++;
                if ($("#mychildrens").children().filter("dd").length < 1) {
                    $("#mychildrens").append(cdHtml);
                }
                else {
                    var fsChildre = $("#mychildrens").children().eq(1);
                    $(cdHtml).insertBefore(fsChildre);
                }
                //$(cdHtml).insertbefore(fsChildre);
            }
        } else if (item.ParentId == -1) {//ke
            cusCount++;
            
        } else {
            $("#myparent").children().filter("dd").remove();
            //par
            if (item.NikeName == "在线主管")
                $("#mupardlabel").html("在线主管");
            parCount++;
            $("#myparent").append("<dd style='display:none;' tag='" + item.id + "'><img src=\"" + url + "\" id='img_" + item.id + "'><a href='javascript:openChart(" + item.id + ",\"" + item.NikeName + "\"," + item.IsLogin + ")'>&nbsp;" + item.NikeName + "</a><span class='" + norreadclss + "'>" + noreadstr + "</span></dd>");
        }
    }
    $("#mychildrens").find("span").eq(0).html(childLine+"/"+childrenCount);
    //$("#customer").find("span").eq(0).html(cusCount);
   // $("#myparent").find("span").eq(0).html(parCount);

    //初始化未读消息列表
    setLstReadCount();
}

function updateUserState(jsonData) {
    jsonData = JSON.parse(jsonData);
    var uid = jsonData.Uid;
    var stat = jsonData.State;//状态 0为上线 1为下线
   
    var url = "http://cfapu.img48.wal8.com/img48/545266_20160510012323/146281905938.png";
    if (stat==0)
        url = "http://cfapu.img48.wal8.com/img48/545266_20160510012323/146281906004.png";
    $("#img_" + uid).attr("src", url);
    var imgParent = $("#img_" + uid).parent();
    var len = imgParent.parent().children();
   
    if (len > 2) {
        var fsChildre = imgParent.parent().children().eq(1);
        imgParent.insertBefore(fsChildre);
    }
  
}

/**
保存联系人至cookie
*/
//联系人key[{}]{"id":value,id:value}
function putOpenLinks(userid, nickName) {
    nickName = encodeURI(nickName);
    var cooksStr = getCookie("line_key");
   //alert(cooksStr);
    var appendJson = "{\"user_id\":\"" + userid + "\",\"nickName\":\"" + nickName + "\"}";
    if (cooksStr=="" || cooksStr == null || cooksStr == undefined) {
        cooksStr = "[{\"user_id\":\"" + userid + "\",\"nickName\":\"" + nickName + "\"}]";
        //"{"Type":"CHART","Data":"{\"TUid\":268,\"Content\":\"发发发\"}"}"
        setCookie("line_key", cooksStr);
        return;
    }
    var jsonData = JSON.parse(cooksStr);
    for (var i = 0; i < jsonData.length; i++) {
        var item = jsonData[i];
        var cls = "";
        if (item.user_id == userid) {
            return;
        }
      
    }

    var objx = JSON.parse(appendJson);
  
    jsonData.push(objx);
    setCookie("line_key", JSON.stringify(jsonData));
    //alert(JSON.stringify(jsonData));
}

//移除指定联系人
function removeOpenLinks(userid) {
   // alert(userid);
    var cooksStr = getCookie("line_key");
   
    if (cooksStr == undefined || cooksStr == null)
        return;
    var cooksStr = getCookie("line_key");
    var jsonData = JSON.parse(cooksStr);
    alert(jsonData.length);
    for (var i = 0; i < jsonData.length; i++) {
        var item = jsonData[i];
        if (item.user_id == userid)
            jsonData.splice(i, 1);
    }
    setCookie("line_key", JSON.stringify(jsonData));
}

/**
存储用户聊天消息
[{"userid":"1","content":"ccc"}]
*/
function putUserMessage(userid,content,occDate) {
    content = encodeURI(content);
    var cooksStr = getCookie("message_content");
    var appendJson = "{\"user_id\":\"" + userid + "\",\"content\":\"" + content + "\",\"occDate\":\"" + occDate + "\"}";
    if (cooksStr == null || cooksStr == "" || cooksStr == undefined) {
        cooksStr = "[{\"user_id\":\"" + userid + "\",\"content\":\"" + content + "\",\"occDate\":\"" + occDate + "\"}]";
        setCookie("message_content", cooksStr);
        return;
    }
    var jsonData = JSON.parse(cooksStr);
    var objx = JSON.parse(appendJson);
    jsonData.push(objx);
    setCookie("message_content", JSON.stringify(jsonData));
}
/**
搜索下级
*/
function searChildren(obj) {
    var content = $(obj).val();
    if (content == "") {
        $("#customer").show();
        $("#myparent").show();
        $("#mymsr").show();
        $("#mychildrens").children().filter("dd").show();
        return;
    }
    $("#customer").hide();
    $("#myparent").hide();
    $("#mymsr").hide();
    //展开
    $("#mychildrens").children().filter("dt").children().eq(0).attr("src", "/Content/Images/chart/left/poplist_arr_down.png");
    $("#mychildrens").children().filter("dd").each(function () {
        var code = $(this).attr("code");
        //indexOf
        //alert(code);
        if (code == undefined) {
            $(this).hide();
        } else {
            if (code.indexOf(content) != -1) {
                $(this).show();
            } else {
                $(this).hide();
            }
        }
    });
}
/**
初始化时获取未读消息
**/
function getNotReadMessage() {
    $.ajax({
        url: "/Page/Messages.aspx",
        type: 'post',
        data: "action=chartmsg&state=0&pageindex=1",
        success: function (data) {
            var jsonData = JSON.parse(data);
            //清除
            if (jsonData.Code == 0) {
                for (var i = 0; i < jsonData.Data.length; i++) {
                    var ite = jsonData.Data[i];
                    var fid = ite.FormUserId;
                    var content = ite.MessageContent;
                    putUserMessage(fid, content, ite.OccDate);
                }
                //
            } else if (jsonData.Code == 1009) {
                alert("你的帐号已在别处登陆，你被强迫下线！"); window.location = "/login.html";
            }

            updatenoread();//加载未读消息
        }
    });
}

function subCode(code) {
    return code.length > 10 ? code.substring(0, 10) : code;
}

function subCode1(code) {
    return code.length > 15 ? code.substring(0, 15) : code;
}

/*
如有未读消息，点击时，自动打开聊天窗口
**/
function inintOpentChartParams() {
    //从cookie中读取消息，显示完成后移除json
    var cooksStr = parent.getCookie("message_content");

    //alert(cooksStr);
    if (cooksStr == undefined || cooksStr == null)
        return;
    var jsonData = JSON.parse(cooksStr);
    var len = jsonData.length;

    var jsonArray = JSON.parse(loadcontactData);
    var defUserId =-1;
    var defNikeName = "";
    for (var i = 0; i < jsonArray.length; i++) {
        var item = jsonArray[i];

        for (var x = 0; x < len; x++) {
            var message = jsonData[x];
            if (message.user_id == item.id) {
                //加载联系人
                putOpenLinks(item.id, item.NikeName);
                defUserId = item.id;
                defNikeName = item.NikeName;
            }
        }
    }

    if (defUserId != -1) {
        //自动打开聊天窗口
        openChart(defUserId, defNikeName,false);
    }
}


function disp_play_click(url) {
    if (isIE()) {

        var div = document.getElementById('sound');
        if (div.innerHTML == "") {
            div.innerHTML = '<embed id="sliod" src="' + url + '" loop="0" autostart="true" hidden="true"></embed>';
            var emb = document.getElementsByTagName('EMBED')[0];
        } else {
            if (document.getElementById("sliod"))
                document.getElementById("sliod").play();
        }
    } else {
        var div = document.getElementById('sound');
        if (div.innerHTML == "") {
            div.innerHTML = '<audio id="sliod" src="' + url + '" ></audio>';
        } else {
            if (document.getElementById("sliod") != undefined)
                document.getElementById("sliod").play();
        }
    }
}

function disp_opening() {

    try {
        disp_play_click('/Views/Chart/js/chart.mp3');/*开奖倒数*/
    } catch (ex) {
    }
}