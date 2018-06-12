<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditBankBaseSetting.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.bank.EditBankBaseSetting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑银行基础信息</title>
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
                            <label>银行名称</label>
                            <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control" required data-bv-notempty-message="银行名称不能为空" ></asp:TextBox>
                            <label id="pwdLab">银行简写</label>
                            <asp:TextBox ID="txtBankDesc" runat="server"  CssClass="form-control" required data-bv-notempty-message="银行简写不能为空"></asp:TextBox>
                            <label>银行LOGO</label>
                             <table>
                                 <tr><td><input  type="radio" value="0" id="bk_0" name="bkrad" checked="checked"/> <img  src="BankLogo/1.jpg" /></td>
                                     <td><input  type="radio" value="1" id="bk_1" name="bkrad"/><img  src="BankLogo/2.jpg"/></td>
                                     <td><input  type="radio" value="2" id="bk_2" name="bkrad"/><img  src="BankLogo/3.jpg"/></td>
                                 </tr>
                                 <tr>
                                     <td><input  type="radio" value="3" id="bk_3" name="bkrad"/><img  src="BankLogo/4.jpg"/></td>
                                     <td><input  type="radio" value="4" id="bk_4" name="bkrad"/><img  src="BankLogo/5.jpg"/></td>
                                     <td>
                                         <table>
                                             <tr>
                                                 <td><input  type="radio" id="bk_5" value="5" name="bkrad"/></td>
                                                 <td><asp:TextBox ID="other" runat="server"  CssClass="form-control" ></asp:TextBox></td>
                                             </tr>
                                         </table>
                                     </td>
                                 </tr>
                             </table>
                             <label>银行官网</label>
                            <asp:TextBox ID="txtBankWebUrl" runat="server"  CssClass="form-control" required data-bv-notempty-message="银行官网不能为空"></asp:TextBox>
                            <label>必须填写支行</label>
                             <asp:DropDownList ID="drpZhiHang" runat="server" CssClass="form-control">
                                 <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                 <asp:ListItem Text="否" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <label>开通自动充值</label>
                             <asp:DropDownList ID="drpAuto" runat="server" CssClass="form-control">
                                 <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                 <asp:ListItem Text="否" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <label>支持跨行充值</label>
                             <asp:DropDownList ID="drpKuaiHang" runat="server" CssClass="form-control">
                                 <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                 <asp:ListItem Text="否" Value="0"></asp:ListItem>
                            </asp:DropDownList>
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
    var checkImg="<%=checkedText%>";
    $(document).ready(function () {   
        $('#defaultForm').bootstrapValidator();
        if(checkImg=="0"||checkImg=="1"||checkImg=="2"||checkImg=="3"||checkImg=="4")
        {$("#bk_"+checkImg).attr("checked","checked");
        }
        else{
            $("#bk_5").attr("checked","checked");
            $("#other").val(checkImg);
        }
    });

    
</script>
