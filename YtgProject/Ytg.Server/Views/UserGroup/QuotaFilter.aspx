<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotaFilter.aspx.cs" Inherits="Ytg.ServerWeb.Views.UserGroup.QuotaFilter" MasterPageFile="~/Views/UserGroup/Group.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="usersHead" runat="server">
    <script src="/Content/Scripts/datepicker/WdatePicker.js"></script>
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <style type="text/css">
        .l-list table {
            width: 100%;
        }

            .l-list table tr td {
                height: 40px;
                line-height: 40px;
            }

        .ltbody tr th {
            font-weight: bold;
        }

        .meTr td {
            color: #cd0228;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
        var rebate = <%=UserRemo%>;
        var maxRebate=<%=Ytg.Comm.Utils.MaxRemo%>;
        var playType=<%=UserPlayType%>;
        var rowsArray;
        $(function () {
            $("#peierfilter").addClass("title_active");
            //
            var rows="<%=rowsStr%>";
            $("#cmbSection").append("<%=OptionHtm%>");
            $("#lsttHead").append("<%=ThHtm%>");
            //添加rows
            if(rows!="")
                rowsArray=rows.split(',');
            $(".rule-single-select").ruleSingleSelect();
            $("#lbtnSearch").click(function () {
                loaddata();//加载数据
            });

            loaddata();//加载数据
        });

        function loaddata(account) {
            if (account != undefined)
                parUserId = account;

            Ytg.common.loading();
            var quoType=$("#cmbSection").find("option:selected").val();
            var paramData="action=qupotafilter";
            paramData+="&quotaWhere="+$("#cmbsy").find("option:selected").val();
            paramData+="&quotaType="+(quoType==-1?"":quoType);
            paramData+="&quotaValue="+$("#txtLastQuota").val();
            paramData+="&code="+$("#txtCode").val();
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
                        
                        for (var c = 0; c < jsonData.Data.length; c++) {//组织数据
                            var item = jsonData.Data[c];
                            if(rowsArray!="" && (item.MaxNum==null || item.MaxNum=="null"))
                                continue;
                           
                            var userRebate=(maxRebate - item.Rebate).toFixed(1);
                            //console.info(item.Rebate+"  "+userRebate+" "+maxRebate);
                            var iscontinue=false;
                            $(".ltbody").children().each(function(){
                                var trcode= $(this).children().first().html();
                                if(trcode==item.Code){
                                    //已经存在，
                                    iscontinue=true;
                                }
                            });
                            if(iscontinue)
                                continue;
                            var htm="<tr>";
                            htm += "<td>" + item.Code + "</td>";
                            htm += "<td>" + xcFindArray(userRebate) + "</td>";
                            for(var v=0;v<rowsArray.length;v++){
                                if(rowsArray[v]=="")
                                    continue;
                                htm += "<td id='"+item.Code+v+"'>0</td>";
                            }
                            htm += "</tr>";
                            var len=$(".ltbody").children().length;
                            if(item.Code!=Ytg.common.user.info.username ||len==0){
                                $(".ltbody").append(htm);
                            }
                            else{
                                var bef=$(htm);
                                bef.insertBefore($(".ltbody").children().first());
                            }
                        }
                        $(".ltbody").children().first().find("td").css({"font-weight":"bold","color":"red"});
                      
                        if (jsonData.Code == 0 && jsonData.Data.length > 0) {
                            for (var c = 0; c < jsonData.Data.length; c++) {//组织数据

                                var item = jsonData.Data[c];
                                if(rowsArray!="" && (item.MaxNum==null || item.MaxNum=="null")){}
                                else{
                                    $(".ltbody").children().each(function(){

                                        var trcode= $(this).children().first().html();
                                        if(trcode==item.Code){
                                            //已经存在，
                                            for(var x=0;x<rowsArray.length;x++){
                                                if(rowsArray[x]==item.QuotaType){
                                                    $("#"+item.Code+x).html(item.MaxNum);
                                                }
                                            }
                                        }
                                    });
                                }

                            }
                        }
                    } else {
                        $(".ltbody").Empty(9);
                    }
                }
            });
        }
       

        function xcFindArray(key){
            if(playType==0)
                return key;
            var spStr="<%=Ytg.Comm.Utils.SysQuotas_key_value%>";
            var spStrArray=spStr.split(',');
            for(var i=0;i<spStrArray.length;i++){
                var xfv=spStrArray[i];
                if(xfv=="")
                    continue;
                var xfvArray=xfv.split('|');
                if(xfvArray.length!=2)
                    continue;
                if(xfvArray[0]==key.toString())
                    return xfvArray[1];
            }
           
        }
    </script>
</asp:Content>
<asp:Content ID="ct" ContentPlaceHolderID="ContentUsers" runat="server">
    <div class="control">

        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar" style="padding: 0px;">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tbody>
                        <tr>
                            <td style="text-align: right;">用户名：</td>
                            <td>
                                <input class="xk_dl_input" type="text" name="txtCode" id="txtCode" value="" >&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td style="text-align: right;">配额区间：</td>
                            <td>
                                <select id="cmbSection">
                                    <option selected="selected" value="-1">所有区间</option>
                                </select>&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td style="text-align: right;">剩余配额：</td>
                            <td>
                                <select id="cmbsy">
                                    <option selected="selected" value="-1">所有条件</option>
                                    <option value="0">等于</option>
                                    <option value="1">大于</option>
                                    <option value="2">大于或等于</option>
                                    <option value="3">小于</option>
                                    <option value="4">小于或等于</option>
                                </select>
                                <input type="text" id="txtMaxValue" class="input normal" style="width: 50px;" />
                            </td>
                            <td>
                                <input name="" id="lbtnSearch" type="button" value="查询" class="formCheck">
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <!--/工具栏-->
        <!--列表-->
        <div>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="grayTable">
                <thead>
                    <tr id="lsttHead">
                        <th>用户</th>
                        <th align="left" width="10%">返点级别</th>
                    </tr>
                </thead>
                <tbody class="ltbody">
                    <tr>
                        <td colspan="2">暂无记录!</td>
                    </tr>
                </tbody>
            </table>
        </div>
        <!--/列表-->
    </div>
</asp:Content>
