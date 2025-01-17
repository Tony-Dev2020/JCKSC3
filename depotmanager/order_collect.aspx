﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="order_collect.aspx.cs" Inherits="depotmanager_order_collect" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <title>未配送商品汇总</title>
<link href="../css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../js/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../js/lhgdialog/lhgdialog.js?skin=idialog"></script>
<script type="text/javascript" src="../js/layout.js"></script>

</head>
<body>
    <form id="form1" runat="server">
	<div class="place">
    <span>位置:</span>
    <ul class="placeul">
    <li><a href="../home.aspx">首页</a></li>
    <li><a href="#">未配送商品汇总</a></li>
    </ul>
    </div>
    <div class="rightinfo">
    <dl class="seachform">  
    <dd><label>商品名称</label><span class="single-select"><asp:TextBox ID="txtNote_no" runat="server" Width="220" CssClass="scinput"></asp:TextBox></span></dd>

     <dd><label>商品类别</label>  
    <span class="rule-single-select">
      <asp:DropDownList ID="ddlproduct_category_id"  runat="server" AutoPostBack="True" onselectedindexchanged="ddlproduct_category_id_SelectedIndexChanged">
          </asp:DropDownList>
    </span>
    </dd>
    
      <dd class="cx"><asp:Button ID="lbtnSearch" runat="server" CssClass="scbtn" onclick="btnSearch_Click" Text="查询"></asp:Button>   
	 </dd>
     <dd class="toolbar1">
        <asp:LinkButton ID="btnExport" runat="server" CssClass="save" onclick="btnExport_Click">   <li><span><img src="../images/t04.png" /></span>导出Execl</li></asp:LinkButton>
        </dd>
    </dl>
	        <!--列表-->
<asp:Repeater ID="rptList" runat="server">
    <HeaderTemplate>
        <table class="imgtable">
    	    <thead>
    	    <tr>
                <th width="50px;">序号</th>
			    <th width="80px;">商品图片</th>
                <th width="100px;">商品编码</th>
		        <th >商品名称</th>
		        <th>商品类别</th>
                <th width="100" align="left">规格型号</th>
                <th width="100" align="left">款式</th>
		        <th width="80px;">订购总量</th>
            </tr>
            </thead>
            <tbody>
    </HeaderTemplate>
    <ItemTemplate> 
            <tr>
                <td><%# pageSize * page + Container.ItemIndex + 1 - pageSize%></td>
                <td class="imgtd"><img src="<%#new ps_here_depot().GetTPurl(Convert.ToInt32(Eval("goods_id")))%>" width="40" height="40"  /></td><%--onMouseOver="toolTip('<img src=<%#new ps_here_depot().GetTPurl(Convert.ToInt32(Eval("goods_id")))%>>')" onMouseOut="toolTip()"--%>
                 <td><%# Eval("product_no")%></td>	
                 <td><%# Eval("goods_title")%></td>	
                 <td><%#new ps_product_category().GetTitle(Convert.ToInt32(Eval("product_category_id")))%></td>	  
                 <td><%# Eval("specification")%></td>
                 <td><%# Eval("commercialStyle")%></td>	
                 <td><%# MyZF(Eval("zongquantity"))%>&nbsp;&nbsp;<%# Eval("dw")%></td>	
            </tr> 
       
    </ItemTemplate>
    <FooterTemplate>
      <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"6\"><font color=red>暂无记录</font></td></tr>" : ""%>
       </tbody>
        </table>
    </FooterTemplate>
</asp:Repeater> 
   
<div class="pagelist">
  <div class="l-btns">
    <span>显示</span><asp:TextBox ID="txtPageNum" runat="server" CssClass="pagenum" onkeydown="return checkNumber(event);" ontextchanged="txtPageNum_TextChanged" AutoPostBack="True"></asp:TextBox><span>条/页</span>
  </div>
  <div id="PageContent" runat="server" class="default"></div>
</div>
    
    </div>

    </form>
</body>
  <script type="text/javascript" src="../js/ToolTip.js"></script>
</html>

