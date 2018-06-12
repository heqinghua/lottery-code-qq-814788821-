<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditQuotas.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Customer.EditQuotas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>调整配额</title>
    <link rel="stylesheet" href="/resource/bsvd/vendor/bootstrap/css/bootstrap.css" />
    <link rel="stylesheet" href="/resource/bsvd/dist/css/bootstrapValidator.css" />

    <script type="text/javascript" src="/resource/bsvd/vendor/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/resource/bsvd/vendor/bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="/resource/bsvd/dist/js/bootstrapValidator.js"></script>
</head>
<body>
    <div class="container">
        <div id="al_container"></div>
        <div class="row">
            <!-- /.col-lg-12 -->
            <section>
                <div class="col-lg-8 col-lg-offset-2">
                    <form id="defaultForm" runat="server" method="post" data-bv-message="This value is not valid"
                        data-bv-feedbackicons-valid="glyphicon glyphicon-ok"
                        data-bv-feedbackicons-invalid="glyphicon glyphicon-remove"
                        data-bv-feedbackicons-validating="glyphicon glyphicon-refresh">
                        <div class="form-group" style="min-height:360px;">
                             <div style="height:10px;"></div>
                                <p style="padding-left:20px;">
                                    用户名：<asp:Label ID="lbCode" runat="server"></asp:Label><span>&nbsp;&nbsp;</span>
                                    返点：<asp:Label ID="lbBackNum" runat="server"></asp:Label>
                                </p>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0"  class="table table-striped table-bordered table-hover" >
                            <thead>
                                <tr>
                                    <th width="20%" >开户级别</th>
                                    <th  width="20%">剩余开户额</th>
                                    <th>增加开户额</th>
                                </tr>
                            </thead>
                             <tbody>
                                 <asp:Repeater ID="rpt" runat="server">
                                     <ItemTemplate>
                                         <tr>
                                         <td style="font-weight:bold;"><%# Eval("QuoType") %></td>
                                         <td><%# Eval("ChildQuoValue") %></td>
                                         <td>
                                             <input type="text" id='<%# Eval("QuoId") %>' class="input" name="<%# Eval("QuoId") %>"/>
                                         </td>
                                             </tr>
                                     </ItemTemplate>
                                 </asp:Repeater>
                             </tbody>
                        </table>
                        </div>
                        <div class="modal-footer" style="text-align:center;">
                            <asp:Button ID="btnSave" runat="server" class="btn btn-primary" Text="确认分配" OnClick="btnSave_Click" OnClientClick="return onSubmitFun();"/>
                        </div>
                        <asp:HiddenField  ID="hidVal" runat="server"/>
                    </form>
                </div>
            </section>
        </div>
    </div>
</body>
</html>
<script type="text/javascript">
    var partten = /^\d+$/;
    function onSubmitFun() {
        //验证
        var inputLength = $('input[type=text]').length;
       
        var isVd = false;
        var data = "";
        $('input[type=text]').each(function () {
            if ($(this).val() != "") {
                //验证是否为数字
                if (!partten.test($(this).val())) {
                    alert("开户额只能为数字!");
                    $(this).select();
                    return false;
                }
               
                //
                var kh = parseInt($(this).val());
                data += $(this).attr("id") + "_" + $(this).val() + ",";
             
                isVd = true;
            }
        });
        if (data == "")
            return false;
        $("#hidVal").val(data);
        return isVd;
    }
</script>