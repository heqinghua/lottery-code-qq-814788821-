<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserBonus.aspx.cs" Inherits="Ytg.ServerWeb.Views.UserGroup.UserBonus" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=Ytg.ServerWeb.BootStrapper.SiteHelper.GetSiteName() %> - 奖金详情</title>
    <script src="/Content/Scripts/jquery-1.7.min.js"></script>
    <link href="/Content/Css/style.css" rel="stylesheet" />
    <link href="/Content/Css/subpage.css" rel="stylesheet">
   <style type="text/css">
       td {text-align:center;font-size:14px;}
      
       .checkBtn { border: 1px solid #cd0228;color: #cd0228;height: 28px;line-height: 24px;width: auto;margin: 5px;width: 100px;background:#fff;}
      .btn:hover {color:#cd0228;border:1px solid #cd0228;background:#fff;}
      .btn {border: 1px solid #d3cfcf;background:#fff; color: #000;height: 28px;line-height: 24px;width: auto;margin: 5px;width: 100px;}
   </style>
</head>
<body>
    <form id="form2" runat="server">
        <div class="control">
            <div class="bindDiv">
                <div style="height:20px;" runat="server" id="dspan"></div>
                <p style="margin-left:-20px;">
                    <asp:Literal ID="liyer" runat="server" Text="级别："  Visible="false"></asp:Literal> <asp:Label ID="lbUserType" runat="server" Text="总代理" Visible="false"></asp:Label><span></span>
                    用户名：<asp:Label ID="lbCode" runat="server"></asp:Label><span></span>
                    昵称：<asp:Label ID="lbNickName" runat="server"></asp:Label>
                    <span></span>
                    奖金限额&nbsp;&nbsp;高频彩：<span>400000元</span>低频彩：<span>100000元</span>
                </p>
            </div>
            <div class="l-list" >
                <input  type="hidden"  id="hdlotteryType" name="hdlotteryType"/>
                <asp:HiddenField  ID="userrebate" runat="server"/>
                <asp:HiddenField  ID="hidUserPlayType" runat="server"/>
                <asp:Literal ID="ltActions" runat="server"></asp:Literal>
            </div>
            <div style="height:10px;"></div>
            <div >
                <div>
                   
                    <!--列表-->
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="grayTable"   id="otherList" style="<%=isLhc?"display:none":""%>" >
                        <thead>
                            <tr>
                                <th width="20%" >玩法</th>
                                <th width="20%">奖级</th>
                                <th width="20%">奖金</th>
                                <th width="10%" <%=hideJj%>>返点</th>
                                <th width="10%">状态</th>
                            </tr>
                        </thead>
                        <tbody class="ltbody">
                          <asp:Literal ID="ltTBody" runat="server"></asp:Literal>
                        </tbody>
                    </table>
                    <!--/列表-->
                    <!--列表-->
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="grayTable"  style="<%=!isLhc?"display:none":""%>">
                        <thead>
                            <tr>
                                <th width="20%" >玩法类型</th>
                                <th width="20%">玩法分类</th>
                                <th width="20%">赔率</th>
                                <th width="10%" <%=lhcBackNum<=0?"display:none;":"" %>>返点</th>
                                <th width="10%">模式</th>
                            </tr>
                        </thead>
                        <tbody class="ltbody">
                          <tr>
                              <td>特码01-49</td>
                              <td>一等奖：所选号码与开奖号码相同</td>
                              <td>42.00</td>
                              <td <%=lhcBackNum<=0?"display:none;":"" %>><%=lhcBackNum %>%</td>
                              <td>使用中</td>
                          </tr>
                        </tbody>
                    </table>
                    <!--/列表-->
                </div>
            </div>
         </div>
    </form>
</body>
</html>
<script type="text/javascript">
    function setHidden(code) {
        $("#hdlotteryType").val(code);
    }

    $(function () {
        $('#main', parent.document).height($('#main', parent.document).contents().find("body").height()); //重设 iframe 高度
        
        if ('<%=isLhc%>' == "True") {
            $(".checkBtn").removeClass("checkBtn").addClass("btn");
            $("#hk6").removeClass("btn")
            $("#hk6").addClass("checkBtn");
        }
    })
</script>