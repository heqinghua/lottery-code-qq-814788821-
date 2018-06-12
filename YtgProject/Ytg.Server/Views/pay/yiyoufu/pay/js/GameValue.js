var eka = "5|10|15|20|30|50|100";
var sd="10|15|25|30|45|50|100|300|350";
var jw = "10|15|30|50|100";
var qq="5|10|15|30|60|100|200";
var wm = "15|30|50|100";
var zt="10|15|25|20|30|50|60|100|300|480";
var sh ="5|10|15|30|40|100";
var wy ="10|15|30";
var zgdx = "50|100";
var szxzj = "30|50|100";
var jy="5|10|15|20|25|30|50";
var lt="20|30|50|100";
var szx="10|30|50|100|300";
var szxjs="30|50|100";

window.onload=function(){GetValue("sel_card");ChoosePayment();};
function GetValue(id)
{
    var sel_cards = document.getElementById(id);
    var sel_price = document.getElementById("sel_price");
    switch(sel_cards.value)
    {
        case "4":
        sel_price.length=0;
        var ekalst = eka.split('|');
        for(var i = 0; i < ekalst.length;i++)
        {
            sel_price.options.add(new Option(ekalst[i],ekalst[i]));
        }
        break;
        
        case "2":
        sel_price.length=0;
        var ekalst = sd.split('|');
        for(var i = 0; i < ekalst.length;i++)
        {
            sel_price.options.add(new Option(ekalst[i],ekalst[i]));
        }
        break;
        
        case "3":
        sel_price.length=0;
        var ekalst = jw.split('|');
        for(var i = 0; i < ekalst.length;i++)
        {
            sel_price.options.add(new Option(ekalst[i],ekalst[i]));
        }
        break;
        
        case "1":
        sel_price.length=0;
        var ekalst = qq.split('|');
        for(var i = 0; i < ekalst.length;i++)
        {
            sel_price.options.add(new Option(ekalst[i],ekalst[i]));
        }
        break;
        
        case "5":
        sel_price.length=0;
        var ekalst = wm.split('|');
        for(var i = 0; i < ekalst.length;i++)
        {
            sel_price.options.add(new Option(ekalst[i],ekalst[i]));
        }
        break;
        
        case "7":
        sel_price.length=0;
        var ekalst = zt.split('|');
        for(var i = 0; i < ekalst.length;i++)
        {
            sel_price.options.add(new Option(ekalst[i],ekalst[i]));
        }
        break;
        
        case "6":
        sel_price.length=0;
        var ekalst = sh.split('|');
        for(var i = 0; i < ekalst.length;i++)
        {
            sel_price.options.add(new Option(ekalst[i],ekalst[i]));
        }
        break;
        
        case "9":
        sel_price.length=0;
        var ekalst = wy.split('|');
        for(var i = 0; i < ekalst.length;i++)
        {
            sel_price.options.add(new Option(ekalst[i],ekalst[i]));
        }
        break;
        
        case "12":
        sel_price.length=0;
        var ekalst = zgdx.split('|');
        for(var i = 0; i < ekalst.length;i++)
        {
            sel_price.options.add(new Option(ekalst[i],ekalst[i]));
        }
        break;
        
        
        case "13，24":
        sel_price.length=0;
        var ekalst = szxzj.split('|');
        for(var i = 0; i < ekalst.length;i++)
        {
            sel_price.options.add(new Option(ekalst[i],ekalst[i]));
        }
        break;
        
        case "8":
        sel_price.length=0;
        var ekalst = jy.split('|');
        for(var i = 0; i < ekalst.length;i++)
        {
            sel_price.options.add(new Option(ekalst[i],ekalst[i]));
        }
        break;
        
        case "14":
        sel_price.length=0;
        var ekalst = lt.split('|');
        for(var i = 0; i < ekalst.length;i++)
        {
            sel_price.options.add(new Option(ekalst[i],ekalst[i]));
        }
        break;
        
        case "13，0":
        sel_price.length=0;
        var ekalst = szx.split('|');
        for(var i = 0; i < ekalst.length;i++)
        {
            sel_price.options.add(new Option(ekalst[i],ekalst[i]));
        }
        break;
        
        case "13，23":
        sel_price.length=0;
        var ekalst = szxjs.split('|');
        for(var i = 0; i < ekalst.length;i++)
        {
            sel_price.options.add(new Option(ekalst[i],ekalst[i]));
        }
        break;
    }
}
function SumFrms()
{
    if(document.getElementById("txtcardNo").value=="")
    {
        alert("请输入游戏卡号");
        return false;
    }
    if(document.getElementById("txtcardpwd").value=="")
    {
        alert("请输入游戏卡密");
        return false;
    }
    if(document.getElementById("txtUserNameCard").value=="")
    {
        alert("请输入游戏帐号");
        return false;
    }
    if(document.getElementById("txtUserNameCard2").value=="")
    {
        alert("请输入游戏帐号");
        return false;
    }
    if(document.getElementById("txtUserNameCard").value!=document.getElementById("txtUserNameCard2").value)
    {
        alert('两次输入游戏帐号不一致！');
        return false;
    }
    document.getElementById("frm_card").submit();
}
function ChoosePayment()
{
    if(document.getElementById("bank").checked)
    {
        document.getElementById("frm_payment").style.display="block";
        document.getElementById("frm_card").style.display="none";
    }
    else
    {
        document.getElementById("frm_payment").style.display="none";
        document.getElementById("frm_card").style.display="block";
    }
}