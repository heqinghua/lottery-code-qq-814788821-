<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsersList.aspx.cs" Inherits="Ytg.ServerWeb.Views.UserGroup.UsersList" MasterPageFile="~/Views/UserGroup/Group.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="usersHead" runat="server">
    <script src="/Content/Scripts/datepicker/WdatePicker.js"></script>
    <link href="/Content/Scripts/Pager/kkpager_orange.css" rel="stylesheet" />
    <script src="/Content/Scripts/Pager/kkpager.min.js" type="text/javascript"></script>
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <style type="text/css">
        .toolbar table {
            width: 100%;
        }

            .toolbar table tr td {
                height: 40px;
                line-height: 40px;
            }

        .sortTh {
            cursor: pointer;
            text-decoration: underline;
        }

            .sortTh span {
                display: block;
                height: 16px;
            }

            .sortTh .desc {
                background: url('/Content/Images/desc.png') no-repeat;
                margin: auto;
            }

            .sortTh .asc {
                background: url('/Content/Images/asc.png') no-repeat;
                margin: auto;
            }

        .tooltip {
            height: 30px;
            line-height: 30px;
            background: #f3f3f3;
            border: 1px solid #E1E1E1;
            border-bottom: none;
            text-align: left;
            padding-left: 20px;
            font-size: 13px;
        }

            .tooltip .hrefSpan {
                cursor: pointer;
            }

                .tooltip .hrefSpan:hover {
                    color: #cd0228;
                    text-decoration: underline;
                }

        .noneColor {
            color: #444;
        }
    </style>

    <script type="text/javascript">
        var hasHide='<%=Ytg.Comm.Utils.HasMinRemo(CookUserInfo)%>';
        $(function () {
            if(hasHide=="True"){
                $("#sphead").remove();
                $("#userfandian").remove();
            }
            $("#users").addClass("title_active");
            $("#lbtnSearch").click(function () {
                loaddata();//加载数据
            });

            loadGroupMonery();
        
            var reid='<%=Request.QueryString["id"]%>';
           var recode='<%=Request.QueryString["name"]%>';
           var htmlCookie=getCookie("back_title_cookie");
           if(reid!="" && recode!="" && htmlCookie!="" && htmlCookie!=null && htmlCookie!=undefined){
               loadTitleInint(reid,recode);
           }
           else{
               loaddata();//加载数据
               clearCoolie();
           }
           $(".sortTh").each(function () {
               $(this).click(function () {
                   var tag = $(this).attr("tag");
                   var isdesc = $(this).children().eq(0).hasClass("desc") ? 1 : 0;
                   if (isdesc == 1) {
                       $(this).children().eq(0).removeClass("desc");
                       $(this).children().eq(0).addClass("asc");
                   }
                   else {
                       $(this).children().eq(0).removeClass("asc");
                       $(this).children().eq(0).addClass("desc");
                   }
                   
                   loaddata(tag, isdesc);
               });
           });
           //顶部导航
           
        });

        function validate(tag){
            var reg = new RegExp("^[0-9]*$");
            var obj =$(tag).val();
            if(!reg.test(obj)){
                $(tag).val("");
            }
            if(!/^[0-9]*$/.test(obj)){
                $(tag).val("");
            }
        }
       
       var _sort = undefined;
       var _order = undefined;
       var pageIndex=1;
       var curpid;
       var ids="";
       var codes="";
       var curMaxReba=<%=MaxRemb%>;
      
       var childrenIndex=1;
       function loaddata(sort,order,codeValue) {
           Ytg.common.loading();
           
           _sort = sort;
           _order = order;
           curpid=(codeValue==undefined?Ytg.common.user.info.user_id:codeValue);

           var paramData = "code=" +$("#userCode").val();
           paramData += "&uid=" +curpid ;
           paramData += "&startmonery=" + $("#txtStart").val();
           paramData += "&endmonery=" + $("#txtend").val();
           paramData += "&order=" + (sort==undefined?"":sort);
           paramData += "&orderType="+(order==undefined?0:order);
           paramData += "&level=" + $("#selLevel").find("option:selected").val();
           paramData += "&startRemb=" + $("#txtSBackNum").val();
           paramData += "&endRemb=" + $("#txtEBackNum").val();
           paramData += "&isSelf=" + ($("#chk").attr("checked") == undefined ? false : $("#chk").attr("checked"));
           paramData += "&pageIndex=" + pageIndex;
           paramData+=  "&playType="+$("#selPlayType").find("option:selected").val();
           paramData += "&action=Childrenusers";
           $.ajax({
               url: "/Page/Users.aspx",
               type: 'post',
               data: paramData,
               success: function (data) {
            
                   Ytg.common.cloading();
                   var jsonData = JSON.parse(data);
                   //清除
                   $(".ltbody").children().remove();
                   if (jsonData.Code == 0 && jsonData.Data.length > 0) {
                       //分页
                       inintpager(pageIndex, jsonData.Total, function (n) {
                           pageIndex = n;
                           //loaddata();
                           loaddata(_sort,_order,curpid);
                       });

                       for (var c = 0; c < jsonData.Data.length; c++) {
                           var item = jsonData.Data[c];
                           if(item.Id==Ytg.common.user.info.user_id)
                               continue;
                    
                          
                         
                           var actionHtml = "";
                           var spActionHtml="";
                           if (<%=LoginUserType%> != 0 && Ytg.common.user.info.user_id!=item.Id)
                               actionHtml+="<a href='javascript:recharge(\"" + item.Id + "\",\""+item.Code+"\");'>充值</a>"
                           actionHtml+="<a href='javascript:showDetail(\"" + item.Code + "\");' style='margin-left:10px;'>账变</a>"
                           actionHtml+="<a href='/Views/UserGroup/UserBonus.aspx?id=" + item.Id + "' target='_blank' style='margin-left:10px;'>详情</a>"
                           if(item.Rebate<0.7 && Ytg.common.user.info.user_id!=item.Id)
                               actionHtml+="<a href='javascript:userpoins(\"" + item.Id + "\");' style='margin-left:10px;color:red;'>开户额</a>"
                           actionHtml+="<a href='javascript:groupMonery(\"" + item.Id + "\");' style='margin-left:10px;'>团队余额</a>"
                           
                           //if(Ytg.common.user.info.user_id!=item.Id)
                           //    actionHtml+="<a href='javascript:EditUserRemo(\"" + item.Id + "\");' style='margin-left:10px;'>返点</a>"
                           
                           if(hasHide!="True"){
                              // if(Ytg.common.user.info.user_id!=item.Id && JsRound(curMaxReba-item.Rebate,2)>=(curMaxReba-1.5)){
                                   spActionHtml+="<a href=\"javascript:recovery("+item.Id+",'"+item.Code+"',"+item.PlayType+","+item.Rebate+")\" style='margin-left:10px;'>回收</a>"
                              // }
                              // if(Ytg.common.user.info.user_id!=item.Id && JsRound(curMaxReba-item.Rebate,2)<curMaxReba){
                                   spActionHtml+="<a href=\"javascript:add_points("+item.Id+",'"+item.Code+"',"+item.Rebate+");\" style='margin-left:10px;'>升点</a>"
                               //}
                           }

                           
                           var daiLIidx=childrenIndex;
                           var htm = "<tr>";
                           if(curpid!=item.Id){
                               
                               htm += "<td><a href=\"javascript:loadChildrens('"+item.Id+"','"+item.Code+"');\">" + item.Code + "</a></td>";
                           }
                           else{
                               daiLIidx--;
                               htm += "<td>" + item.Code + "</td>";
                           }

                           var userTypeStr = "";
                           switch(item.UserType){
                               case 0:
                                   userTypeStr="普通会员";
                                   break;
                               case 1:
                                   var strdl="代理用户";
                                   switch(daiLIidx){
                                       case 1:
                                           strdl="一级代理";
                                           break;
                                       case 2:
                                           strdl="二级代理";
                                           break;
                                       case 3:
                                           strdl="三级代理";
                                           break;
                                   }
                                   userTypeStr=strdl;
                                   break;
                               case 3:
                                   userTypeStr="总代理";
                                   break;
                           }
                           
                           htm += "<td>" + userTypeStr + "</td>";
                           htm += "<td>" + decimalCt(Ytg.tools.moneyFormat(item.UserAmt)) + "</td>";
                          // alert(item.Status);
                           if(hasHide!="True")
                               htm += "<td>" +  getuserRebate(item.Rebate,item.PlayType);+ "</td>";
                           htm += "<td>" + item.OccDate.substring(0,10) + "</td>";
                           htm += "<td>" + (item.Status==0?"正常":"<span style='color:red;'>冻结</span>") + "</td>";
                           //   htm += "<td><input onclick='isRecharge(this,\"" + item.Id+ "\");' type='checkbox' " + (item.IsRecharge == true ? "checked='checked'" : "") + "/></td>";
                           htm += "<td>" + actionHtml + "</td>";
                           if(hasHide!="True")
                               htm+="<td>"+spActionHtml+"</td>";
                           htm += "</tr>";
                           $(".ltbody").append(htm);
                       }
                   } else {
                       $(".ltbody").Empty(9);
                   }
               }
           });
       }
       function loadChildrens(id,usercode,isappend){
           loaddata(_sort,_order,id);
           //加载
           if(isappend==undefined ||ids==""){
               ids+=id+",";
               codes+=usercode+",";
           }

         //  alert(codes);
           var htm="当前位置：<a id='allUsers' class='orange'  href='javascript:;'>用户列表</a>";
           var idarray=ids.split(',');
           var codearray=codes.split(',');

           for(var i=0;i<idarray.length;i++){
               if(idarray[i]== undefined ||idarray[i]=='')
                   continue;
               var hclass="";
               var st="";
               if(i<idarray.length-2){
                   hclass="hrefSpan ";
               }
               else{
                   st="style='color:#444;'";
               }
               
               htm+="<span id='fs_"+idarray[i]+"' name='_"+idarray[i]+"' class='user_title_name'>></span><a id='ls_"+idarray[i]+"' class=' orange "+hclass+"' "+st+">"+codearray[i]+"</a>";
           }
           $(".form-div").html(htm);
           childrenIndex=$(".orange").length;//设置代理级数

           $("#allUsers").click(function(){
               childrenIndex=1;
               codes="";ids="";
               var htm="当前位置：<a id='allUsers' class='orange' href='javascript:;'>用户列表</a>";
               $(".form-div").html(htm);
               loaddata(_sort,_order,Ytg.common.user.info.user_id);
           });

           ///////////////////////////////////////////////////////////////////////

           $(".hrefSpan").click(function(){
               var id= $(this).attr("id");
               var cd=$(this).html();

               $(this).removeClass("tooltip");
               $(this).unbind("click");
               //重置
               ids="";
               codes="";
               var index = $(this).parent().children("a").index(this);
               $(this).parent().children("a").each(function(idx){
                   if(idx<=index)
                   {
                       //移除
                       if($(this).html()!="用户列表"){
                           var idar=$(this).attr("id").split('_')[1];
                           ids+=idar+",";
                           codes+=$(this).html()+",";
                       }
                   }
                   else
                   {
                       $(this).prev().prev().removeClass("hrefSpan").css("color","#444");
                       $(this).prev().remove(); //移除上一个节点
                       $(this).remove()         //移除当前节点
                   }
               });
               childrenIndex=$(".orange").length;//设置代理级数
               //加载数据
               loaddata(_sort,_order,id.split('_')[1]);
           });

           ///////////////////////////////////////////////////////////////////////
       }

       function loadGroupMonery() {
           $.ajax({
               url: "/Page/Users.aspx",
               type: 'post',
               data: "action=groupuseramt",
               success: function (data) {
                   var jsonData = JSON.parse(data);
                   //清除
                   if (jsonData.Code == 0 ) {
                       $("#groupAMT").html("团队余额：" + decimalCt(Ytg.tools.moneyFormat(jsonData.Data)));
                   }
               }
           });
       }
       function isRecharge(obj,uid){
           var value = $(obj).attr("checked") == undefined ? 0 : 1;
           $.ajax({
               url: "/Page/Users.aspx",
               type: 'post',
               data: "action=updateRecharge&iRecharge=" + value+ "&uid=" + uid,
               success: function (data) {
                   var jsonData = JSON.parse(data);
                   //清除
                   if (jsonData.Code == 0) {
                       //成功
                   }
               }
           });
       }
            
       function recharge(uid,name) {
           //$.dialog({
           //    id: 'recharge',
           //    fixed: true,
           //    lock: true,
           //    max: false,
           //    min: false,
           //    title: "用户充值",
           //    content: "url:/Views/UserGroup/Recharge.aspx?id="+uid,
           //    width: 600,
           //    height:330,
           //    close: function () {
           //        //loaddata(_sort,_order);
           //    }
           //});
           saveTitleCookie();
           window.location="/Views/UserGroup/Recharge.aspx?id="+uid+"&name="+name;
       }
       function rechargeClose(){
           $.dialog({ id: 'recharge'}).close();
       }
      

       function editUser() {
           $.dialog({
               id: 'editUser',
               fixed: true,
               lock: true,
               max: false,
               min: false,
               title:"新增用户",
               content: "url:/Views/UserGroup/EditUser.aspx",
               width: 700,
               close: function () {
                  // loaddata(_sort,_order);
               }
           });
       }
       function userClose(){
           $.dialog({ id: 'editUser'}).close()
       }

       function EditUserRemo(uid) {
           $.dialog({
               id: 'EditUserRemo',
               fixed: true,
               lock: true,
               max: false,
               min: false,
               title:"编辑返点",
               content: "url:/Views/UserGroup/EditUserRemo.aspx?id="+uid,
               width: 700,
               close: function () {
                   loaddata(_sort,_order);
               }
           });
       }
       function UserRemoClose(){
           $.dialog({ id: 'EditUserRemo'}).close();
       }
       function groupMonery(uid){
           //获取团队余额
           $.ajax({
               url: "/Page/Users.aspx",
               type: 'post',
               data: "action=groupuseramt&uid="+uid+"&dt="+new Date() ,
               success: function (data) {
               
                   var jsonData = JSON.parse(data);
                   //清除
                   if (jsonData.Code == 0) {
                       //成功
                       $.dialog({
                           id: 'open_userGroup_monery',
                           fixed: true,
                           lock: true,
                           max: false,
                           min: false,
                           width:300,
                           height:150,
                           title: "团队余额",
                           content: "团队余额：<span style='font-size:14px;font-weight:bold;color:red;'>"+decimalCt(Ytg.tools.moneyFormat(jsonData.Data))+"&nbsp;&nbsp;&nbsp;</span>"
                       });
                   }else{
                       $.dialog.tips("获取团队余额失败，请稍后重试!", 1.5, '32X32/succ.png', function () { });
                   }
               }
           });
       }
       /**账变*/
       function showDetail(account) {
           //var winHeight = $(window).height();
           //var openHeight = winHeight - 100;
           //$.dialog({
           //    id: 'showDetail',
           //    fixed: true,
           //    lock: true,
           //    max: false,
           //    min: false,
           //    title: "账变明细",
           //    content: "url:/Views/Report/OpenAmountChangeList.aspx?account=" + account,
           //    width: 1200,
           //    height:openHeight,
           //    close: function () {
           //       // loaddata(_sort,_order);
           //    }
           //});
           window.location="/Views/Report/AmountChangeList.aspx?account=" + account;
       }

       function getuserRebate(userRebate,playType){
           
           var max1800=<%=Ytg.Comm.Utils.MaxRemo%>;
           var max1700=<%=Ytg.Comm.Utils.MaxRemo1700%>;
           if(playType==0){
               return JsRound(max1800-userRebate,1) ;
           }else{
               return JsRound(max1700-userRebate,1) ;
           }
           
       }
       /**回收*/
       function recovery(uid,name,playType,bate_) {
           //var winHeight = $(window).height();
           //var openHeight = winHeight - 100;
           //$.dialog({
           //    id: 'recovery',
           //    fixed: true,
           //    lock: true,
           //    max: false,
           //    min: false,
           //    title: "回收用户",
           //    content: "url:/Views/UserGroup/Recovery.aspx?uid=" + uid,
           //    width: 900,
           //    height:openHeight,
           //    close: function () {
           //        loaddata(_sort,_order);
           //    }
           //});
          
           if(bate_>=<%=Ytg.Comm.Utils.MinRemo1800_1700%>){
               $.alert("返点已降到最低，不能再降点!");
               return;
           }
           //加载数据
           saveTitleCookie();
           window.location="/Views/UserGroup/Recovery.aspx?uid="+uid+"&name="+name;
       }
       function closerecovery(){
           $.dialog({ id: 'recovery'}).close();
       }


       function closeuserpoins_up(){
           $.dialog({ id: 'userpoins_up'}).close();
       }

       
       /**开户额*/
       function userpoins(uid) {
           $.dialog({
               id: 'openQp',
               fixed: true,
               lock: true,
               max: false,
               min: false,
               title: "开户额",
               content: "url:/Views/UserGroup/OpenQuota.aspx?uid=" + uid,
               width: 1000
              
           });
       }
       function quoclose(){
           $.dialog({ id: 'openQp'}).close();
       }

       function add_points(uid,name,bate_){
           if(bate_<=0){
               $.alert("返点已升到最高，不能再升点!");
               return;
           }

           saveTitleCookie();
           window.location="/Views/UserGroup/UserPoints.aspx?uid="+uid+"&name="+name;
       }

       function saveTitleCookie(){
           //alert($(".form-div").html());
           var encodeHtmlStr=encodeURI($(".form-div").html());
           setCookie("back_title_cookie",encodeHtmlStr);
           setCookie("back_title_ids",ids);
           setCookie("back_title_codes",codes);
       }
       function loadTitleInint(reid,recode){
           var reid='<%=Request.QueryString["id"]%>';
           var recode='<%=Request.QueryString["name"]%>';
           if(reid!="" && recode!=""){
               var htmlCookie=getCookie("back_title_cookie");
               var html_ids=getCookie("back_title_ids");
               var html_codes=getCookie("back_title_codes");
               ids=html_ids;
               codes=html_codes;
               $(".form-div").html(decodeURI(htmlCookie));
               loadChildrens(reid,recode,false)
               setCookie("back_title_cookie","");
               setCookie("back_title_ids","");
               setCookie("back_title_codes","");
           }
       }

        function clearCoolie(){
            setCookie("back_title_cookie","");
            setCookie("back_title_ids","");
            setCookie("back_title_codes","");
        }
     
    </script>
</asp:Content>
<asp:Content ID="ContentUsers" ContentPlaceHolderID="ContentUsers" runat="server">
    <form runat="server" id="form1">
        <div>
            <!--工具栏-->
            <div class="toolbar-wrap">
                <table border="0" cellspacing="0" cellpadding="0" >
                    <tbody>
                        <tr>
                            <td style="text-align: right;">用户名：</td>
                            <td>
                                <input class="xk_dl_input" type="text" name="userCode" id="userCode" value="" >&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td style="text-align: right;">用户余额：</td>
                            <td>
                                <input class="xk_zd_input" name="txtStart" id="txtStart" value="" onkeyup="validate(this)" type="text" size="10">
                                至
                                <input class="xk_zd_input" name="txtend" id="txtend" value="" onkeyup="validate(this)" type="text" size="10">
                            </td>
                            <td style="text-align: right;<%=HasFil%>">
                                所属奖金组：
                            </td>
                            <td style="<%=HasFil%>">
                                <select id="selPlayType" name="selPlayType">
                                    <option value="-1">所有奖金组</option>
                                    <option value="0">1800</option>
                                    <option value="1">1700</option>
                                </select>
                            </td>
                            <td><input name="" id="lbtnSearch" type="button" value="查询" class="formCheck"></td></td>
                        </tr>
                      
                    </tbody>
                </table>
                <div id="floatHead" class="toolbar" style="display: none;">
                    <table>
                        <tr>
                            <td>
                                <span>返点级别：</span><input type="text" id="txtSBackNum" class="input normal" style="width: 160px;" />
                                <span style="margin-left: 5px; margin-right: 5px">至</span><input type="text" id="txtEBackNum" class="input normal" style="width: 160px;" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span>用户级别：</span>
                                <div class="rule-single-select">
                                    <select id="selLevel">
                                        <option selected="selected" value="-1">所有</option>
                                        <option value="1">代理用户</option>
                                        <option value="0">会员用户</option>
                                    </select>
                                </div>

                            </td>
                        </tr>
                    </table>
                    <div class="l-list">
                        <ul class="icon-list">
                            <li><a class="add" href="javascript:editUser();"><i></i><span>增加用户</span></a></li>
                            <li><a class="save" id="btnSave" href="javascript:__doPostBack('btnSave','')" style="display: none;"><i></i><span>保存</span></a></li>
                            <li><a class="all" onclick="checkAll(this);" href="javascript:;" style="display: none;"><i></i><span>全选</span></a></li>
                        </ul>
                    </div>

                </div>
                <!--/工具栏-->
                <div class="form-div" style="text-align: left;">当前位置：<a id="allUsers" class="orange" href="javascript:;"> 用户列表</a></div>
                <!--列表-->
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="grayTable">
                    <thead>
                        <tr>
                            <th width="10%" tag="Code">用户名</th>
                            <th width="10%" tag="UserType">用户类型</th>
                            <th width="10%" tag="UserAmt">余额</th>
                            <th width="8%" tag="Rebate" id="userfandian">返点</th>
                            <th width="10%" tag="OccDate">注册时间</th>
                            <th width="8%" tag="IsDelete">状态</th>
                            <%-- <th width="10%" tag="IsRecharge" class="sortTh">是否开通充值</th>--%>
                            <th>用户操作</th>
                            <th id="sphead">特殊调整</th>
                        </tr>
                    </thead>
                    <tbody class="ltbody">
                        <tr>
                            <td align="center" colspan="9">暂无记录</td>
                        </tr>
                    </tbody>
                </table>

                <!--/列表-->
                <div id="kkpager"></div>
                <!--团队余额-->
                <div style="font-size: 16px; font-weight: bold; color: #000; text-align: right;" id="groupAMT"></div>
            </div>
        </div>
    </form>
</asp:Content>
