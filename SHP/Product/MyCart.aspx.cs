using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using DataAccessLayer;

public partial class Product_MyCart : System.Web.UI.Page
{
    DataAccess _DataAccess;
    TextBox address = new TextBox();
    TextBox date = new TextBox();
    Label cost = new Label();
    Label total = new Label();
    DropDownList qty = new DropDownList();
    protected void Page_Load(object sender, EventArgs e)
    {
        DataAccess da = new DataAccess();

        //string sql = string.Format("SELECT ProductID,ProductName,OrderDate,ShippingDate,ShippingAddress,Quantity,UnitPrice FROM {0}_OrderProduct_VIW WHERE OrderID IN (SELECT ID FROM {0}_Order WHERE CustomerID={1})", Settings.SchemaPrefix, Request.Cookies["Data1"]["ID"].ToString());

        if (0 != Session.Count)
        {
            string sql;

            //ArrayList list = new ArrayList();

            //foreach (object i in Session)
            //{ list.Add(i); }
           // string IDs = ConvertHelper.Convert<string>(Session.Contents, NullHelper.String);

            sql = string.Format("SELECT distinct ID,Name,UnitPrice,Supplier FROM {0}_Product_VIW WHERE Id in ({1})", Settings.SchemaPrefix,"1,2,3");

            DataTable table = da.ExecuteDataTable(sql);

            _List.DataSource = table; 
            _List.DataBind();
                                             
            //Session.Clear();
        }
        
        da.Dispose();

    }



  
    protected void _List_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
          

           address = (TextBox)e.Item.FindControl("_ShippingAddress");
            date = (TextBox)e.Item.FindControl("_ShippingDate");
            cost = (Label)e.Item.FindControl("_UnitPrice");
            total = (Label)e.Item.FindControl("_Total");
            qty = (DropDownList)e.Item.FindControl("_Quantity");

            string[] temp = cost.Text.Split('$');
            double total_price = Convert.ToDouble(temp[0]) * Convert.ToDouble(qty.SelectedItem.ToString());
            total.Text = total_price.ToString() + "$";

           
        }
    }

    protected void _Save_Click(object sender, EventArgs e)
    {
        _DataAccess = new DataAccess();
        
        int paramSize = 5;
        SqlParameter[] procParams = new SqlParameter[paramSize]; int i = 0;
        procParams[i] = _DataAccess.CreateParameter("@ID", SqlDbType.Int, ParameterDirection.Output);
        procParams[++i] = _DataAccess.CreateParameter("@CreateDate", SqlDbType.DateTime, DateTime.Now);
        procParams[++i] = _DataAccess.CreateParameter("@ShippingAddress", SqlDbType.NVarChar, address.Text);
        procParams[++i] = _DataAccess.CreateParameter("@ShippingDate", SqlDbType.DateTime, date.Text);

        int custID = ConvertHelper.Convert<int>(Request.Cookies["Data1"]["ID"], 0);
        if (0 != custID)
            procParams[++i] = _DataAccess.CreateParameter("@CustomerID", SqlDbType.Int, custID);

        string proc = string.Format("{0}_Order_INS", Settings.SchemaPrefix);
        int ret = _DataAccess.ExecuteNonQuery(proc, procParams);

        if (-1 != ret)
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            var uriBuilder = new UriBuilder(url);
            string proId = HttpUtility.ParseQueryString(uriBuilder.Query).Get("ProductID");
            string[] price = cost.Text.Split('$');

            int size = 5;
            SqlParameter[] Params = new SqlParameter[size]; int j = 0;

            Params[j] = _DataAccess.CreateParameter("@ID", SqlDbType.Int, ParameterDirection.Output);
            Params[++j] = _DataAccess.CreateParameter("@OrderID", SqlDbType.Int, procParams[0].Value.ToString());
            Params[++j] = _DataAccess.CreateParameter("@ProductID", SqlDbType.Int, proId);
            Params[++j] = _DataAccess.CreateParameter("@UnitPrice", SqlDbType.Decimal, price[0]);
            Params[++j] = _DataAccess.CreateParameter("@Quantity", SqlDbType.Int, qty.SelectedItem);

            string proc2 = string.Format("{0}_OrderProduct_INS", Settings.SchemaPrefix);
            int check = _DataAccess.ExecuteNonQuery(proc2, Params);

            if (-1 != check)
            {
                price = null;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You Ordered successfully')", true);
            }
        }

        _DataAccess.Dispose();
    }

}