<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditBankSetting.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.bank.EditBankSetting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑公司账号</title>
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
                            <label>银行</label>
                            <asp:DropDownList ID="drpBanks" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <p class="help-block"></p>
                             <label>银行帐号</label>
                            <asp:TextBox ID="txtBankNo" runat="server" CssClass="form-control" required data-bv-notempty-message="银行账号不能为空"  data-bv-stringlength="true" data-bv-stringlength-min="16" data-bv-stringlength-max="22" data-bv-stringlength-message="银行账号长度为16-18位"></asp:TextBox>
                            <p class="help-block"></p>
                            <label id="pwdLab">开户省份</label>
                            <asp:TextBox ID="txtProvince" runat="server"  CssClass="form-control" required data-bv-notempty-message="开户省份不能为空"></asp:TextBox>
                            <p class="help-block" id="pwd_block"></p>
                            <label>开户支行</label>
                            <asp:TextBox ID="txtBranch" runat="server"  CssClass="form-control" ></asp:TextBox>
                            <p class="help-block"></p>
                             <label  >开户人</label>
                            <asp:TextBox ID="txtBankOwner" runat="server"  CssClass="form-control" required data-bv-notempty-message="开户人不能为空"></asp:TextBox>
                            <p class="help-block" ></p>
                            <label>状态</label>
                             <asp:DropDownList ID="cmbStatus" runat="server" CssClass="form-control">
                                 <asp:ListItem Text="启用" Value="1"></asp:ListItem>
                                 <asp:ListItem Text="禁用" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <p class="help-block"></p>
                        </div>
                        <div class="modal-footer" style="text-align:center;">
                            <asp:Button ID="btnSave" runat="server" class="btn btn-primary" Text="保存" OnClick="btnSave_Click"  />
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
    var isunque = false;
    var editId=<%=Request.Params["id"]%>;
    
    $(document).ready(function () {   
        $('#defaultForm').bootstrapValidator();

        $("#txtCode").focusout(function () {
            if(editId!="")
                return;
            //检查
            var valx=$(this).val();
            $.ajax({
                url: "EditUser.aspx",
                type: 'post',
                data: "action=unique&code=" + valx,
                success: function (data) {
                    if (data == "-1" && valx!="") {
                        isunque = false;
                        $("#txtCode").val("");
                        $("#txtCode").focus();
                        alert("登录名已经存在！");
                    } else {
                        isunque = true;
                    }
                }
            });
        });
    });

    
</script>
