<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRecharge.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Customer.UserRecharge" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>账号充值</title>
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
                        <div class="form-group">
                            <label>登录名</label>
                            <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" required Enabled="false"></asp:TextBox>
                            <p class="help-block"></p>
                            <label>昵称</label>
                            <asp:TextBox ID="txtNickName" runat="server" CssClass="form-control" required Enabled="false"></asp:TextBox>
                            <p class="help-block"></p>
                            <label>账户余额</label>
                            <asp:TextBox ID="txtMonery" runat="server" CssClass="form-control" required Enabled="false"></asp:TextBox>
                            <p class="help-block"></p>
                             <label style="display:none;">充值类型</label>
                             <div style="display:none;">
                                 <asp:RadioButton  ID="radDefault" runat="server" Text="普通充值" GroupName="cq" />
                                 <asp:RadioButton  ID="radFenHong" runat="server" Text="分红充值" GroupName="cq"/>
                                 <asp:RadioButton  ID="radKouk" runat="server" Text="扣款" GroupName="cq"/>
                             </div>
                            <p class="help-block"></p>
                            <label id="pwdLab">充值金额</label>
                            <asp:TextBox ID="txtinMonery" runat="server"  CssClass="form-control" required data-bv-notempty-message="请填写正确充值金额"
                                pattern="^([1-9][\d]{0,7}|0)(\.[\d]{1,4})?$" data-bv-regexp-message="充值金额格式错误"></asp:TextBox>
                            <p class="help-block" id="pwd_block"></p>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnSave" runat="server" class="btn btn-primary" Text="确认充值" OnClick="btnSave_Click" />
                        </div>
                    </form>
                </div>
            </section>
        </div>
    </div>
</body>
</html>
<link href="/dist/css/font.css" rel="stylesheet" />
<script type="text/javascript">
    
    $(document).ready(function () {   
        $('#defaultForm').bootstrapValidator();
    });

    
</script>
