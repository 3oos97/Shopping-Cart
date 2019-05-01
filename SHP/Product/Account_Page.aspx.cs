using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;

public partial class Product_Account_Page : System.Web.UI.Page
{
    DataAccess _DataAccess;
    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (null != Request.Cookies["Data1"])
        {
            int custID = ConvertHelper.Convert<int>(Request.Cookies["Data1"]["ID"].ToString(), 0);
            if (0 != custID)
            {
                _DataAccess = new DataAccess();
                string sql = string.Format("SELECT Name,Email,UserName,Mobile,Country FROM {0}_Customer_VIW cus WHERE ID={1}", Settings.SchemaPrefix, custID);
                SqlDataReader dr = _DataAccess.ExecuteReader(sql);


                if (dr.Read())
                {
                    _Name.Text = dr["Name"].ToString();
                    _UserName.Text = dr["UserName"].ToString();
                    _Email.Text = dr["Email"].ToString();
                    _Mobile.Text = dr["Mobile"].ToString();
                    _Country.Text = dr["Country"].ToString();
                }

                _DataAccess.Dispose();
            }
        }
    }

    protected void _Edit_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("/Product/Edit_Profile.aspx", Settings.ServerUrl));
    }
}