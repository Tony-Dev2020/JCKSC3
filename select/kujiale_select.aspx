﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="kujiale_select.aspx.cs" Inherits="select_kujiale_select" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>商家管理</title>
<link href="../css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../js/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../js/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../js/layout.js"></script>
<script type="text/javascript">
    function selKuJiaLe(id, name, url) {

        window.parent.$("#txtKujiaLe").val(url);

    var api = frameElement.api, W = api.opener;
    api.close();
}
</script> 
</head>
<body>
    <form id="form1" runat="server">
	
    <div class="rightinfo">    

    
   <dl class="seachform"> 
    <dd><label>查询关键字</label><span class="single-select"><asp:TextBox ID="txtKeywords" runat="server" CssClass="scinput"></asp:TextBox></span></dd>
    <%--<dd><label>所属公司</label>  
    <span class="rule-single-select">
    <asp:DropDownList ID="ddlCategoryId"  runat="server" Enabled="false" AutoPostBack="True" onselectedindexchanged="ddlCategoryId_SelectedIndexChanged">
    </asp:DropDownList>
    </span>
    </dd>
    <dd><label>状态</label>  
    <span class="rule-single-select">
      <asp:DropDownList ID="ddlStatus"  runat="server" AutoPostBack="True" onselectedindexchanged="ddlStatus_SelectedIndexChanged">
            <asp:ListItem Value="" Selected="True">==全部==</asp:ListItem>
             <asp:ListItem Value="1">在用</asp:ListItem>
             <asp:ListItem Value="2">注销</asp:ListItem>
          </asp:DropDownList>
    </span>
    </dd>--%>
      <dd class="cx"> <asp:Button ID="lbtnSearch" runat="server" CssClass="scbtn" onclick="btnSearch_Click" Text="查询"></asp:Button></dd> 
    </dl>
    
    <!--列表-->
<asp:Repeater ID="rptList" runat="server">
    <HeaderTemplate>
        <table class="tablelist">
    	    <thead>
    	    <tr>
                <th width="50px;">序号</th>
                <th>方案名称</th>
                <th>图片</th>
                <th>放大</th>
                <th width="90px;">操作</th>
            </tr>
            </thead>
       <tbody>
    </HeaderTemplate>
    <ItemTemplate> 
        <tr>
		<td><%# pageSize * page + Container.ItemIndex + 1 - pageSize%><asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" /></td>	
		<%--<td><%#new ps_depot_category().GetTitle(Convert.ToInt32(Eval("category_id")))%></td>--%>
        <td><%# Eval("Name")%></td>
        <td valign="middle"><a href="<%# Eval("URL")%>"  target="_blank" ><img src="<%# Eval("URL")%>" width="80" height="80" /></td>
        <td ><a href="<%# Eval("URL")%>"  target="_blank" >放大</td>
		<td><input id="btnSelCus" onclick="selKuJiaLe('<%# Eval("id")%>','<%# Eval("name")%>','<%# Eval("URL")%>')" type="button" class="btn green" value="选择" /> </td>
        </tr> 
     </ItemTemplate>
    <FooterTemplate>
      <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"10\"><font color=red>暂无记录</font></td></tr>" : ""%>
       </tbody>
        </table>
    </FooterTemplate>
</asp:Repeater>  
   <!--列表-->
<div class="pagelist">
  <div class="l-btns">
    <span>显示</span><asp:TextBox ID="txtPageNum" runat="server" CssClass="pagenum" onkeydown="return checkNumber(event);" ontextchanged="txtPageNum_TextChanged" AutoPostBack="True"></asp:TextBox><span>条/页</span>
  </div>
  <div id="PageContent" runat="server" class="default"></div>
</div>   
    </div>
    </form>
</body>
</html>

