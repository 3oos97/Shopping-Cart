using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;
using System.Collections;

public partial class Product_Product_Details : System.Web.UI.Page
{
    DataAccess _DataAccess;
    protected void Page_Load(object sender, EventArgs e)
    {
        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        var uriBuilder = new UriBuilder(url);
        string proId = HttpUtility.ParseQueryString(uriBuilder.Query).Get("ProductID");

        _Image.ImageUrl = string.Format("{0}/App_Resources/Images/Large/{1}.jpg", Settings.ServerUrl, proId.ToString());


        _DataAccess = new DataAccess();
        string sql = string.Format("SELECT Name,UnitPrice,Supplier FROM {0}_Product_VIW WHERE Id={1}", Settings.SchemaPrefix,proId.ToString());
        SqlDataReader dr = _DataAccess.ExecuteReader(sql);
        
        

        if (dr.Read())
        {
            _Name.Text = dr["Name"].ToString();
            _Price.Text = dr["UnitPrice"].ToString() + "$";
            _Supplier.Text = dr["Supplier"].ToString();
            
        }

        
        _DataAccess.Dispose();
        
    }

    
    protected void _Add_Click(object sender, EventArgs e)
    {
        if (null != Request.Cookies["Data1"])
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            var uriBuilder = new UriBuilder(url);
            string proId = HttpUtility.ParseQueryString(uriBuilder.Query).Get("ProductID");

            Session.Add(proId, proId);
            //HttpCookie UserData = new HttpCookie("Data2");
            //UserData["ID"] = proId;
            //Response.Cookies.Add(UserData);
            //UserData.Expires = System.DateTime.Now.AddDays(1);


            Response.Redirect(string.Format("{0}/Defualt/Home.aspx", Settings.ServerUrl));
        }

       else
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You have to login or signup')", true);
    }

    //protected void _Confirm_Click(object sender, EventArgs e)
    //{
    //   _DataAccess = new DataAccess();

    //    int paramSize = 5;
    //    SqlParameter[] procParams = new SqlParameter[paramSize]; int i = 0;
    //    procParams[i] =  _DataAccess.CreateParameter("@ID", SqlDbType.Int, ParameterDirection.Output);
    //    procParams[++i] = _DataAccess.CreateParameter("@CreateDate", SqlDbType.DateTime, DateTime.Now);
    //    procParams[++i] = _DataAccess.CreateParameter("@ShippingAddress", SqlDbType.NVarChar, _ShippingAddress.Text);
    //    procParams[++i] = _DataAccess.CreateParameter("@ShippingDate", SqlDbType.DateTime, _ShippingDate.Text);

    //    int custID = ConvertHelper.Convert<int>(Request.Cookies["Data1"]["ID"], 0);
    //    if(0 != custID)
    //        procParams[++i] = _DataAccess.CreateParameter("@CustomerID", SqlDbType.Int, custID);

    //    string proc = string.Format("{0}_Order_INS", Settings.SchemaPrefix);
    //    int ret = _DataAccess.ExecuteNonQuery(proc, procParams);

    //    if(-1 != ret)
    //    {
    //        string url = HttpContext.Current.Request.Url.AbsoluteUri;
    //        var uriBuilder = new UriBuilder(url);
    //        string proId = HttpUtility.ParseQueryString(uriBuilder.Query).Get("ProductID");
    //        string[] price = _Price.Text.Split('$');

    //        int size = 5;
    //        SqlParameter[] Params = new SqlParameter[size]; int j = 0;

    //        Params[j] = _DataAccess.CreateParameter("@ID", SqlDbType.Int, ParameterDirection.Output);
    //        Params[++j] = _DataAccess.CreateParameter("@OrderID", SqlDbType.Int, procParams[0].Value.ToString());
    //        Params[++j] = _DataAccess.CreateParameter("@ProductID", SqlDbType.Int, proId);
    //        Params[++j] = _DataAccess.CreateParameter("@UnitPrice", SqlDbType.Decimal, price[0]);
    //        Params[++j] = _DataAccess.CreateParameter("@Quantity", SqlDbType.Int, _Quantity.SelectedItem.Text);

    //        string proc2 = string.Format("{0}_OrderProduct_INS", Settings.SchemaPrefix);
    //        int check = _DataAccess.ExecuteNonQuery(proc2, Params);

    //        if (-1 != check)
    //        {
    //            price = null;
    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You Ordered successfully')", true);
    //        }
    //    }

    //    _DataAccess.Dispose();
    //}
}
