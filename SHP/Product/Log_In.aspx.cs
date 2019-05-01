using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;

public partial class Product_Log_In : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void _LogIn_Click(object sender, EventArgs e)
    {
        DataAccess da = new DataAccess();
        object ret = da.ExecuteScalar(string.Format("SELECT ID FROM {0}_Customer WHERE Username = '{1}' AND Password = '{2}'", Settings.SchemaPrefix, _UserName.Text, _Password.Text));

        if(null != ret)
        {
            HttpCookie UserData = new HttpCookie("Data1");
            UserData["ID"] = ret.ToString();
            UserData.Expires = System.DateTime.Now.AddDays(1);
            Response.Cookies.Add(UserData);

            
            Response.Redirect(string.Format("{0}/Defualt/Home.aspx", Settings.ServerUrl));
        }

        da.Dispose();
    }
}