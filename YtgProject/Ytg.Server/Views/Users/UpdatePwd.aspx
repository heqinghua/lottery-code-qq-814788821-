<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdatePwd.aspx.cs" Inherits="Ytg.ServerWeb.Views.Users.UpdatePwd" MasterPageFile="/Views/Users/Users.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="usersHead" runat="server">
    <link href="/Content/Css/keypad.css" rel="stylesheet" />
    <script src="/Content/Scripts/jquery.keypad2.js"></script>
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
     <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
    <style>
      
    </style>
    <script type="text/javascript">
        var isShowAll=<%=showAl%>;
        $(function () {
            
            if(isShowAll==1){
                $.alert("您尚未设定资金密码，请先设定！");
                $("#btnSubmit").val("设定");
            }
            $("#upPwd").addClass("title_active");
            //初始化表单验证
            $("#form1").initValidform();
            $("#btnSubmit").click(function () {
                var index = $(".content-tab-ul-wrap ul li a").index($(".content-tab-ul-wrap ul li a.selected")[0]);

                switch (index) {
                    case 0:
                        uploginpwd();
                        break;
                    case 1:
                        upzijinpwd();
                        
                        break;
                    case 2:
                        upwhy();
                        break;
                }
            });
        });
        function uploginpwd() {
            var oldpwd = $("#oldpwd").val();
            var newpwd = $("#newpwd").val();
            var renewpwd = $("#renewpwd").val();
            if ($.trim(oldpwd) == "") {
                $("#oldpwd").focus();
                $.alert("请输入旧登录密码!");
                return;
            }
            if (!validateUserPss(oldpwd)) {
                $("#oldpwd").select();
                $.alert("请输入正确的旧登录密码!");
                return;
            }

            if ($.trim(newpwd) == "") {
                $("#newpwd").focus();
                $.alert("请输入新登录密码!");
                return;
            }
            if (!validateUserPss(newpwd)) {
                $("#newpwd").select();
                $.alert("请输入正确的新登录密码!");
                return;
            }
            if (renewpwd != newpwd) {
                $("#renewpwd").select();
                $.alert("请输入正确的确认登录密码!");
                return;
            }
            Ytg.common.loading();
            $.ajax({
                url: "/Page/Users.aspx",
                type: 'post',
                data: "action=updadwwdasp&oldpwd=" + oldpwd + "&newpwd=" + newpwd,
                success: function (data) {
                    Ytg.common.cloading();
                    var jsonData = JSON.parse(data);
                    if (jsonData.Code == 0)
                        $.alert("登录密码修改成功！");
                    else
                        $.alert("登录密码修改失败！");
                    $("#oldpwd").val("");
                    $("#newpwd").val("");
                    $("#renewpwd").val("");
                }
            });
        }
        function upzijinpwd() {
            var oldpwd = $("#oldzjpwd").val();
           
            var newpwd = $("#newzjpwd").val();
            var renewpwd = $("#renewzjpwd").val();
            if ($.trim(oldpwd) == "" && oldpwd!=undefined) {
                $("#oldpwd").focus();
                $.alert("请输入旧资金密码!");
                return;
            }
            if (!validateUserPss(oldpwd) && oldpwd != undefined) {
                $("#oldpwd").select();
                $.alert("请输入正确的旧资金密码!");
                return;
            }

            if ($.trim(newpwd) == "") {
                $("#newpwd").focus();
                $.alert("请输入新资金密码!");
                return;
            }
            if (!validateUserPss(newpwd)) {
                $("#newpwd").select();
                $.alert("请输入正确的新资金密码!");
                return;
            }
            if (renewpwd != newpwd) {
                $("#renewpwd").select();
                $.alert("资金密码与确认资金密码不一致!");
                return;
            }
            
            oldpwd = oldpwd == undefined ? "" : oldpwd;
            Ytg.common.loading();
            $.ajax({
                url: "/Page/Users.aspx",
                type: 'post',
                data: "action=updatezjmpwp&oldpwd=" + oldpwd + "&newpwd=" + newpwd,
                success: function (data) {
                    Ytg.common.cloading();
                    var jsonData = JSON.parse(data);
                    if (jsonData.Code == 0) {
                        $.alert("资金密码修改成功！",1,function(){location.reload();});
                    }
                    else if (jsonData.Code == 1011) {
                        $.alert("资金密码不能与登录密码一致！");
                    }
                    else {
                        $.alert("资金密码修改失败！");
                    }
                    $("#oldzjpwd").val("");
                    $("#newzjpwd").val("");
                    $("#renewzjpwd").val("");
                }
            });
        }
        function upwhy() {
            var why = $("#why").val();
            if ($.trim(why) == "") {
                $("#why").focus();
                $.alert("请输入问候语!");
                return;
            }
            Ytg.common.loading();
            $.ajax({
                url: "/Page/Users.aspx",
                type: 'post',
                data: "action=updategreetings&greetings=" + why,
                success: function (data) {
                    Ytg.common.cloading();
                    var jsonData = JSON.parse(data);
                    if (jsonData.Code == 0)
                        $.alert("修改问候语成功！");
                    else
                        $.alert("修改问候语失败，请稍后重试！");
                    $("#why").val("");

                }
            });
        }

        jQuery(function () {

            jQuery("input.password").keypad({
                layout: [
                        $.keypad.SPACE + $.keypad.SPACE + $.keypad.SPACE + '1234567890',
                        'cdefghijklmab',
                                        "stuvwxyznopqr"/*+ $.keypad.CLEAR*/,
                                        $.keypad.SPACE + $.keypad.SPACE + $.keypad.SHIFT + $.keypad.CLEAR + $.keypad.BACK + $.keypad.CLOSE
                ],
                // 软键盘按键布局 
                buttonImage: '/content/images/skin/kb1.png',	// 弹出(关闭)软键盘按钮图片地址
                buttonImageOnly: true,	// True 表示已图片形式显示, false 表示已按钮形式显示
                buttonStatus: '打开/关闭软键盘', // 打开/关闭软键盘按钮说明文字
                showOn: 'button', // 'focus'表示已输入框焦点弹出, 
                // 'button'通过按钮点击弹出,或者 'both' 表示两者都可以弹出 

                keypadOnly: false, // True 表示只接受软件盘输入, false 表示可以通过键盘和软键盘输入  

                randomiseNumeric: true, // True 表示对所以数字位置进行随机排列, false 不随机排列
                randomiseAlphabetic: true, // True 表示对字母进行随机排列, false 不随机排列 

                clearText: '清空', // Display text for clear link 
                clearStatus: '', // Status text for clear l

                shiftText: '大小写', // SHIFT 按键功能的键的显示文字 
                shiftStatus: '转换字母大小写', // SHIFT按键功能的TITLE说明文字 

                closeText: '关闭', // 关闭按键功能的显示文字 
                closeStatus: '关闭软键盘', // 关闭按键功能的TITLE说明文字 

                backText: '退格', // 退格功能键的显示文字 
                backStatus: '退格', // 退格功能键的说明文字

                onClose: null	// 点击软键盘关闭是调用的函数
            });
        });
        
        $(function(){
            Ytg.common.loading();
            var cldt=setInterval(function(){Ytg.common.cloading();
                clearInterval(cldt);
            },1000)
        })
        function setButtonVal(idx){
            switch(idx){
                case 0:
                    $("#btnSubmit").val("修改");
                    break;
                case 1:
                    if(isShowAll==1)
                        $("#btnSubmit").val("设定");
                    else
                        $("#btnSubmit").val("修改");
                    break;
                case 2:
                    $("#btnSubmit").val("修改");
                    break;
            }
           
        }
    </script>
</asp:Content>
<asp:Content ID="ContentUsers" ContentPlaceHolderID="ContentUsers" runat="server"><form method="post">
    <div class="control">
        
        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li onclick="setButtonVal(0);"><a href="javascript:;" onclick="tabs(this,0);" class="<%=UpdateUserPwd %>">修改登录密码</a></li>
                        <li onclick="setButtonVal(1);"><a href="javascript:;" onclick="tabs(this,1);" class="<%=UpdateZiJinPwd %>">修改资金密码</a></li>
                        <li onclick="setButtonVal(2);" style="display:none;"><a href="javascript:;" onclick="tabs(this,2);">修改问候语</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="tab-content" style="<%=UpdateUserTable%>">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="formTable" >
                <tbody>
                    <tr>
                        <td class="s_checkpass_td"><span>输入旧登陆密码：</span><input id="oldpwd" name="oldpwd" type="password" class="password  normal" datatype="*1-16" sucmsg=" " text="" nullmsg=" "></td>
                    </tr>

                    <tr>
                        <td class="s_checkpass_td"><span>输入新登陆密码：</span><input id="newpwd" name="newpwd" type="password" class="password normal" datatype="*1-16" sucmsg=" " text="" nullmsg=" "><font class="red" style="line-height:25px;">&nbsp;&nbsp;由字母和数字组成6-16个字符</font></td>
                    </tr>
                    <tr>
                        <td class="s_checkpass_td"><span>确认新登陆密码：</span><input id="renewpwd" name="renewpwd" type="password" class="password normal" datatype="*1-16" sucmsg=" " text="" nullmsg=" "></td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="tab-content" style="<%=UpdateZiJinTable%>">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="formTable" >
                <tbody>
                    <tr id="nonwZiPwd" runat="server" visible="false">
                        <td class="s_checkpass_td"><span style="padding-left:95px;">您还没有设定资金密码，请先设置：</span></td>
                    </tr>
                    <tr id="oldzjPwd" runat="server">
                        <td class="s_checkpass_td"><span>输入旧资金密码：</span>
                            <input type="password" id="oldzjpwd" name="oldzjpwd" class="password normal" datatype="*1-16" sucmsg=" " text="" nullmsg=" " /></td>
                    </tr>
                    <tr>
                        <td class="s_checkpass_td">
                            <span>输入新资金密码：</span>
                            <input type="password" id="newzjpwd" name="newzjpwd" class="password normal" datatype="*1-16" sucmsg=" " text="" nullmsg=" " /><font class="red" style="line-height:25px;">&nbsp;&nbsp;由字母和数字组成6-16个字符</font>
                        </td>
                    </tr>
                    <tr>
                        <td class="s_checkpass_td">
                             <span>确认新资金密码：</span>
                            <input type="password" id="renewzjpwd" name="renewzjpwd" class="password normal" datatype="*1-16" sucmsg=" " text="" nullmsg=" " />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="tab-content" style="display: none;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="formTable">
                <tbody>
                    <tr>
                        <td class="s_checkpass_td">
                            <span>问 候 语：</span>
                            <input type="text" id="why" name="why" class="input normal"  datatype="*1-50" sucmsg=" " text="" nullmsg=" " />
                        </td>
                    </tr>
                    <tr>
                        <td class="s_checkpass_td"><font class="red">在登录界面,输入用户名后,您会看到此处设置的登录问候语,避免仿冒钓鱼网站</font></td>
                    </tr>
                </tbody>
            </table>

        </div>
        <div align="center" class="page-footer">
            <input name="btnSubmit" class="formChange" id="btnSubmit" type="button" value="修改"><input name="" type="reset" value="重置" class="formReset"></div>
        <p class="bz_text">备注：请妥善保管好您的登录密码，如遗忘请使用您的提款密码重置</p>
    </div></form>
</asp:Content>
