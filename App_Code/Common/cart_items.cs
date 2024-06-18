using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 购物车实体类
/// </summary>
[Serializable]
public partial class cart_items
{
    public cart_items()
    { }
    #region Model
    
    /// <summary>
    /// 商品ID
    /// </summary>
    public long id { set; get; }

    /// <summary>
    /// 商品No
    /// </summary>
    public string product_no { set; get; }

    /// <summary>
    /// 商品名称
    /// </summary>
    public string title { set; get; }


    public string specification { set; get; }


    public string commercialStyle { set; get; }
 
    public string commercialcolor { set; get; }

    /// <summary>
    /// 图片路径
    /// </summary>
    public string img_url { set; get; }
    /// <summary>
    /// 单位
    /// </summary>
    public string dw { set; get; }
    /// <summary>
    /// 销售单价
    /// </summary> 
    public decimal price { set; get; }


    /// <summary>
    /// 购买数量
    /// </summary>
    public int quantity { set; get; }

    /// <summary>
    /// 库存数量
    /// </summary>
    public int stock_quantity { set; get; }
    /// <summary>
    /// 商品种类
    /// </summary>
    public int product_category_id { set; get; }

    /// <summary>
    /// 描述
    /// </summary>
    public string remark { set; get; }


    public string kit_num { set; get; }


    public string kit_desc { set; get; }


    public decimal discount { set; get; }


    public bool is_cust { set; get; }

    


    #endregion
}

/// <summary>
/// 购物车属性类
/// </summary>
[Serializable]
public partial class cart_total
{
    public cart_total()
    { }
    #region Model
    private int _total_num = 0;
    private int _total_quantity = 0;
    private decimal _payable_amount = 0M;
    private decimal _real_amount = 0M;

    /// <summary>
    /// 商品种数
    /// </summary>
    public int total_num
    {
        set { _total_num = value; }
        get { return _total_num; }
    }
    /// <summary>
    /// 商品总数量
    /// </summary>
    public int total_quantity
    {
        set { _total_quantity = value; }
        get { return _total_quantity; }
    }
    /// <summary>
    /// 预定商品总金额
    /// </summary>
    public decimal payable_amount
    {
        set { _payable_amount = value; }
        get { return _payable_amount; }
    }
    /// <summary>
    /// 实付商品总金额
    /// </summary>
    public decimal real_amount
    {
        set { _real_amount = value; }
        get { return _real_amount; }
    }

    #endregion
}