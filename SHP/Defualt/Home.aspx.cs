using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;

public partial class Product_Default : System.Web.UI.Page
{
    DataAccess _DataAccess;
    protected void Page_Load(object sender, EventArgs e)
    {
            this.OnDataBinding();    
            
    }

    protected void OnDataBinding()
    {
        
        if (null != Request.QueryString["CategoryID"])
        {

            DataTable table1 = new DataTable();
            DataTable table2 = new DataTable();
            _DataAccess = new DataAccess();

            table1 = _DataAccess.ExecuteDataTable(string.Format("SELECT DISTINCT pro.ID, pro.Name FROM SHP_Product pro,SHP_LKP_Category cat,SHP_LNK_ProductCategory pc WHERE pc.ProductID=pro.ID AND pc.CategoryID={0}", Request.QueryString["CategoryID"]));
            table2 = _DataAccess.ExecuteDataTable("SELECT Name FROM SHP_LKP_Category");

            _items.DataSource = table1;
            _CategoryList.DataSource = table2;

            _items.DataBind();
            _CategoryList.DataBind();

        }
        else
        {
            DataTable table1 = new DataTable();
            DataTable table2 = new DataTable();
            _DataAccess = new DataAccess();

            table1 = _DataAccess.ExecuteDataTable("SELECT ID,Name FROM SHP_Product WHERE SortIndex = 2");
            table2 = _DataAccess.ExecuteDataTable("SELECT Name FROM SHP_LKP_Category");

            _items.DataSource = table1;
            _CategoryList.DataSource = table2;

            _items.DataBind();
            _CategoryList.DataBind();
        }

        _DataAccess.Dispose();
    }


    protected void _Category_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        HyperLink link = (HyperLink)e.Item.FindControl("_Category");

        _DataAccess = new DataAccess();

        SqlDataReader dr = _DataAccess.ExecuteReader(string.Format("SELECT ID,Name FROM SHP_LKP_Category WHERE Name='{0}'", link.Text));

        string id = "";
        if (dr.Read())
            id = dr["ID"].ToString();

        _DataAccess.Dispose();

        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        var uriBuilder = new UriBuilder(url);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["CategoryID"] = id;
        uriBuilder.Query = query.ToString();
        url = uriBuilder.ToString();
        link.NavigateUrl = url;
    }


    protected void _items_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        HyperLink link = (HyperLink)e.Item.FindControl("_Link");
        _DataAccess = new DataAccess();

        object id = _DataAccess.ExecuteScalar(string.Format("SELECT ID FROM SHP_Product WHERE Name = '{0}'", link.Text));

        _DataAccess.Dispose();

        string url = string.Format("{0}/Product/Product_Details.aspx", Settings.ServerUrl);
        var uriBuilder = new UriBuilder(url);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["ProductID"] = id.ToString();
        uriBuilder.Query = query.ToString();
        url = uriBuilder.ToString();
        link.NavigateUrl = url;
    }
}