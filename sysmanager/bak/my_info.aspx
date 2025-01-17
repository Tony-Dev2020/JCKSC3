﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="my_info.aspx.cs" Inherits="sysmanager_my_info" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <title>我的信息</title>
<script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../js/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../js/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" src="../js/layout.js"></script>
<script type="text/javascript" src="../js/pinyin.js"></script>
<link href="../css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
    });
</script>
</head>
<body>
    <form id="form1" runat="server">
	<div class="place">
    <span>位置：</span>
    <ul class="placeul">
    <li><a href="../home.aspx">首页</a></li>
    <li><a href="#">我的信息</a></li>
    </ul>
    </div>

    <div class="formbody">   
    <div class="formtitle"><span>我的信息</span></div>

<!--/我的信息-->
<div class="tab-content">
 <dl>  
  <dl>  
    <dt>真实姓名</dt>
    <dd><asp:Literal ID="Litreal_name" runat="server"></asp:Literal></dd>
  </dl> 

    <dl>
    <dt>登录账号</dt>
    <dd><asp:Literal ID="Lituser_name" runat="server"></asp:Literal> </dd>
  </dl>
    <asp:Panel ID="plPassword" Visible="false" runat="server">
        <dl>
        <dt>旧登录密码</dt>
        <dd><asp:TextBox ID="txtOldPassword" runat="server" CssClass="input normal" TextMode="Password" datatype="*4-20" nullmsg="请输入旧密码" errormsg="密码范围在6-20位之间" sucmsg=" "></asp:TextBox> <span class="Validform_checktip">*</span></dd>
      </dl>

      <dl>
        <dt>新登录密码</dt>
        <dd><asp:TextBox ID="txtPassword" runat="server" CssClass="input normal" TextMode="Password" datatype="*6-20" nullmsg="请输入新密码" errormsg="密码范围在6-20位之间" sucmsg=" "></asp:TextBox> <span class="Validform_checktip">*</span></dd>
      </dl>

      <dl>
        <dt>新确认密码</dt>
        <dd><asp:TextBox ID="txtPassword1" runat="server" CssClass="input normal" TextMode="Password" datatype="*" recheck="txtPassword" nullmsg="请再输入一次新密码" errormsg="两次输入的密码不一致" sucmsg=" "></asp:TextBox> <span class="Validform_checktip">*</span></dd>
      </dl>

   </asp:Panel>
   
    <dl id="bmxx" runat="server" visible="false">
    <dt>所属公司</dt>
    <dd><asp:Literal ID="Litdepot_category_name" runat="server"></asp:Literal></dd>
  </dl>
  <div id="mdxx" runat="server" visible="false">

    <dl>
    <dt>所属经销商</dt>
    <dd><asp:Literal ID="Litdepotname" runat="server"></asp:Literal></dd>
  </dl> 

    <dl>
    <dt>联系人姓名</dt>
    <dd><asp:TextBox ID="txtcontact_name" runat="server" CssClass="input normal" ></asp:TextBox><asp:Literal ID="Litcontact_name" Visible="false" runat="server"></asp:Literal></dd>
  </dl>
    <dl>
    <dt>联系手机</dt>
    <dd><asp:TextBox ID="txtmobile" runat="server" CssClass="input normal" ></asp:TextBox></dd>
  </dl>
    <dl id="tel" runat="server" visible="false">
    <dt>联系电话</dt>
    <dd><asp:TextBox ID="txtcontact_tel" runat="server" CssClass="input normal" ></asp:TextBox><asp:Literal ID="Litcontact_tel" runat="server"></asp:Literal></dd>
  </dl>

    <dl>
    <dt>经销商地址</dt>
    <dd><asp:TextBox ID="txtcontact_address" runat="server" CssClass="input normal" ></asp:TextBox><asp:Literal ID="Litdepot_id" runat="server" Visible="false"></asp:Literal></dd>
  </dl>
 </div>
</div>
<!--/我的信息-->  
</div>

    <!--工具栏-->
<div class="page-footer">
  <div class="btn-list">
    
    <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" onclick="btnSubmit_Click"  />
    <asp:Button ID="btnChangePassword" runat="server" Text="修改密码" CssClass="btn green" onclick="btnChangePassword_Click"  />
    <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript:history.back(-1);" />
  </div>
  <div class="clear"></div>
</div>
<!--/工具栏-->

    </form>
</body>
</html>
