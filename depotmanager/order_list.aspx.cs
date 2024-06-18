﻿using System;
using System.Text;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class depotmanager_order_list : System.Web.UI.Page
{
    protected int totalCount;
    protected int page;
    protected int pageSize;

    protected int depot_category_id;
    protected int depot_id;
    protected int status;
    protected int type;
    protected string note_no = string.Empty;
    protected string start_time = string.Empty;
    protected string stop_time = string.Empty;

    ManagePage mym = new ManagePage();
    protected void Page_Load(object sender, EventArgs e)
    {
        //判断是否登录
        if (!mym.IsAdminLogin())
        {
            Response.Write("<script>parent.location.href='../index.aspx'</script>");
            Response.End();
        }
        if (Session["DepotCatID"] != null && Session["DepotCatID"].ToString() != "" && Session["DepotCatID"].ToString() != "0")
        {
            this.depot_category_id = Convert.ToInt32(Session["DepotCatID"]);
            this.ddldepot_category_id.Enabled = false;
        }
        else
        {
            this.depot_category_id = AXRequest.GetQueryInt("depot_category_id");
        }
        this.depot_id = AXRequest.GetQueryInt("depot_id");
        if(Session["DepotID"].ToString()!="" && Session["DepotID"].ToString() != "0")
            this.depot_id = Convert.ToInt32(Session["DepotID"]);
        this.status = AXRequest.GetQueryInt("status");
        this.type = AXRequest.GetQueryInt("type");
        this.note_no = AXRequest.GetQueryString("note_no");

        if (AXRequest.GetQueryString("start_time") == "")
        {
            this.start_time = DateTime.Now.ToString("yyyy-MM-01");
        }
        else
        {
            this.start_time = AXRequest.GetQueryString("start_time");
        }
        if (AXRequest.GetQueryString("stop_time") == "")
        {
            this.stop_time = DateTime.Now.ToString("yyyy-MM-dd");
        }
        else
        {
            this.stop_time = AXRequest.GetQueryString("stop_time");
        }
        //判断权限
        ps_manager_role_value myrv = new ps_manager_role_value();
        int role_id = Convert.ToInt32(Session["RoleID"]);
        int nav_id = 38;
        if (this.status == 2)
            nav_id = 49;
        if (this.status == 3)
            nav_id = 50;
        if (this.status == 4)
            nav_id = 51;
        if (this.status == 5)
            nav_id = 52;
        if (this.status == 6)
            nav_id = 53;
        if (this.status == 7)
            nav_id = 54;
        if (!myrv.QXExists(role_id, nav_id))
        {
            Response.Redirect("../error.html");
            Response.End();
        }
       



        lbTile.Text = GetPageTitle(this.status);
        this.pageSize = GetPageSize(10); //每页数量

        if (!Page.IsPostBack)
        {
            DQBind(0); //绑定商家地区
            SJBind(depot_category_id); //绑定下单商家
            RptBind("id>0 " + CombSqlTxt(this.depot_category_id, this.depot_id, this.status, this.note_no, this.start_time, this.stop_time), "add_time desc,id desc");
        }
    }

    #region 绑定商家地区=================================
    private void DQBind(int _category_id)
    {
        ps_depot_category bll = new ps_depot_category();
        DataTable dt = bll.GetList(_category_id);
        this.ddldepot_category_id.Items.Clear();
        this.ddldepot_category_id.Items.Add(new ListItem("==全部==", "0"));
        foreach (DataRow dr in dt.Rows)
        {
            string Id = dr["id"].ToString();
            string Title = dr["name"].ToString().Trim();
            this.ddldepot_category_id.Items.Add(new ListItem(Title, Id));
        }
    }
    #endregion

    #region 绑定下单商家=================================
    private void SJBind(int _category_id)
    {
        ps_manager_customer bllManagerCustomer = new ps_manager_customer();
        string custidlist = "";
        string strWhere = "category_id=" + _category_id + "and status=1 ";
        DataSet dsManagerCustomer = bllManagerCustomer.GetList(" user_id = '" + Session["AID"] + "' ");
        if (dsManagerCustomer.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsManagerCustomer.Tables[0].Rows.Count; i++)
            {
                custidlist = custidlist + dsManagerCustomer.Tables[0].Rows[i]["cust_id"] + ",";
            }
            custidlist = custidlist + "9999";

           strWhere = strWhere + " and id in (" + custidlist + ")";
        }

        ps_depot bll = new ps_depot();
        DataTable dt = bll.GetList(strWhere).Tables[0];
        this.ddldepot_id.Items.Clear();
        this.ddldepot_id.Items.Add(new ListItem("==全部==", ""));
        foreach (DataRow dr in dt.Rows)
        {
            string Id = dr["id"].ToString();
            string Title = dr["title"].ToString().Trim();
            this.ddldepot_id.Items.Add(new ListItem(Title, Id));
        }
    }
    #endregion

    #region 数据绑定=================================
    private void RptBind(string _strWhere, string _orderby)
    {
        this.page = AXRequest.GetQueryInt("page", 1);
       
        if (this.depot_category_id > 0)
        {
            this.ddldepot_category_id.SelectedValue = this.depot_category_id.ToString();
        }
        if (this.depot_id > 0)
        {
            this.ddldepot_id.SelectedValue = this.depot_id.ToString();
        }

        txtNote_no.Text = this.note_no;
        txtstart_time.Value = this.start_time;
        txtstop_time.Value = this.stop_time;

        ps_manager_customer bllManagerCustomer = new ps_manager_customer();
        string custidlist = "";
        DataSet dsManagerCustomer = bllManagerCustomer.GetList(" user_id = '" + Session["AID"] + "' ");
        if (dsManagerCustomer.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsManagerCustomer.Tables[0].Rows.Count; i++)
            {
                custidlist = custidlist + dsManagerCustomer.Tables[0].Rows[i]["cust_id"] + ",";
            }
            custidlist = custidlist + "9999";

            _strWhere = _strWhere + " and depot_id in (" + custidlist + ")";
        }

        if(this.status==1)
            _strWhere = _strWhere + " and IsCust ='"+ this.type + "'";

        ps_orders bll = new ps_orders();
        this.rptList.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
        this.rptList.DataBind();

        //绑定页码
        txtPageNum.Text = this.pageSize.ToString();
        string pageUrl = Utils.CombUrlTxt("order_list.aspx", "depot_category_id={0}&depot_id={1}&status={2}&note_no={3}&page={4}", this.depot_category_id.ToString(), this.depot_id.ToString(), this.status.ToString(), txtNote_no.Text, "__id__");
        PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
    }
    #endregion

    #region 组合SQL查询语句==========================
    protected string CombSqlTxt(int _depot_category_id, int _depot_id, int _status, string _note_no, string _start_time, string _stop_time)
    {
        StringBuilder strTemp = new StringBuilder();

        //strTemp.Append(" and status=1" );
        strTemp.Append(" and status='"+ this.status +"'");
        if (_depot_category_id > 0)
        {
            strTemp.Append(" and depot_category_id=" + _depot_category_id);
        }
        if (_depot_id > 0)
        {
            strTemp.Append(" and depot_id=" + _depot_id);
        }

        if (string.IsNullOrEmpty(_start_time))
        {
            _start_time = "1900-01-01";
        }
        if (string.IsNullOrEmpty(_stop_time))
        {
            _stop_time = "2099-01-01";
        }
        strTemp.Append(" and add_time between  '" + DateTime.Parse(_start_time) + "' and '" + DateTime.Parse(_stop_time + " 23:59:59") + "'");


        //strTemp.Append(" and add_time between  '" + DateTime.Parse(_start_time) + "' and '" + DateTime.Parse(_stop_time + " 23:59:59") + "'");

        _note_no = _note_no.Replace("'", "");
        if (!string.IsNullOrEmpty(_note_no))
        {
            strTemp.Append(" and (order_no like  '%" + _note_no + "%' or  remark like  '%" + _note_no + "%'  or address like  '%" + _note_no + "%'  or message like  '%" + _note_no + "%' or id in (select  order_id from v_OrderGoods where commercialStyle  like'%" + _note_no + "%'  ) )");
        }
        return strTemp.ToString();
    }
    #endregion

    #region 返回每页数量=============================
    private int GetPageSize(int _default_size)
    {
        int _pagesize;
        if (int.TryParse(Utils.GetCookie("d_order_page_size"), out _pagesize))
        {
            if (_pagesize > 0)
            {
                return _pagesize;
            }
        }
        return _default_size;
    }
    #endregion

    #region 返回订单状态=============================
    protected string GetOrderStatus(int _id)
    {
        string _title = string.Empty;

        switch (_id)
        {
            case 1:
                _title = "已生成";
                break;
            case 2:
                _title = "已确认";
                break;
            case 3:
                //_title = "已设计";
                _title = "未确认设计";
                break;
            case 4:
                _title = "已确认设计";
                break;
            case 5:
                _title = "已报价";
                break;
            case 6:
                _title = "已确认报价";
                break;
            case 7:
                _title = "已汇款";
                break;
            case 8:
                _title = "已确认收款";
                break;
            case 9:
                _title = "已排产";
                break;
            case 10:
                _title = "已取消";
                break;
            case 11:
                _title = "已作废";
                break;
        }

        return _title;
    }
    #endregion

    #region 返回订单状态=============================
    protected string GetPageTitle(int _status)
    {
        string _title = string.Empty;

        switch (_status)
        {
            case 1:
                _title = "未确认订单";
                break;
            case 2:
                _title = "未设计订单";
                break;
            case 3:
                _title = "未确认设计订单";
                break;
            case 4:
                _title = "未报价订单";
                break;
            case 5:
                _title = "未确认报价订单";
                break;
            case 6:
                _title = "未汇款订单";
                break;
            case 7:
                _title = "未确认收款订单";
                break;
            case 8:
                _title = "未完成订单";
                break;
        }

        return _title;
    }
    #endregion

    //查询
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "depot_category_id={0}&depot_id={1}&status={2}&note_no={3}&start_time={4}&stop_time={5}", this.depot_category_id.ToString(), this.depot_id.ToString(), this.status.ToString(), txtNote_no.Text, this.txtstart_time.Value, this.txtstop_time.Value));
    }

    //筛选商家地区
    protected void ddldepot_category_id_SelectedIndexChanged(object sender, EventArgs e)
    {
        SJBind(Convert.ToInt32(ddldepot_category_id.SelectedValue));
        Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "depot_category_id={0}&depot_id={1}&status={2}&note_no={3}&start_time={4}&stop_time={5}", this.ddldepot_category_id.SelectedValue, this.depot_id.ToString(), this.status.ToString(), txtNote_no.Text, this.txtstart_time.Value, this.txtstop_time.Value));
    }

    //筛选下单商家
    protected void ddldepot_id_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "depot_category_id={0}&depot_id={1}&status={2}&note_no={3}}&start_time={4}&stop_time={5}", this.depot_category_id.ToString(), this.ddldepot_id.SelectedValue, this.status.ToString(),txtNote_no.Text, this.txtstart_time.Value, this.txtstop_time.Value));
    }


    //设置分页数量
    protected void txtPageNum_TextChanged(object sender, EventArgs e)
    {
        int _pagesize;
        if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
        {
            if (_pagesize > 0)
            {
                Utils.WriteCookie("d_order_page_size", _pagesize.ToString(), 14400);
            }
        }
        Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "depot_category_id={0}&depot_id={1}&status={2}&note_no={3}&start_time={4}&stop_time={5}", this.depot_category_id.ToString(), this.depot_id.ToString(), this.status.ToString(), txtNote_no.Text, this.txtstart_time.Value, this.txtstop_time.Value));

    }

    //小数位是0的不显示
    public string MyConvert(object d)
    {
        string myNum = d.ToString();
        string[] strs = d.ToString().Split('.');
        if (strs.Length > 1)
        {
            if (Convert.ToInt32(strs[1]) == 0)
            {
                myNum = strs[0];
            }
        }
        return myNum;
    }
}