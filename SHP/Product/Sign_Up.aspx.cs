using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;

public partial class Product_Sign_Up : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void _SignUp_Click(object sender, EventArgs e)
    {
        DataAccess da = new DataAccess();

        int paramSize = 9;
        SqlParameter[] procParams = new SqlParameter[paramSize]; int i = 0;

        procParams[i] = da.CreateParameter("@ID", SqlDbType.Int, ParameterDirection.Output);
        procParams[++i] = da.CreateParameter("@CreateDate", SqlDbType.DateTime, DateTime.Now);
        procParams[++i] = da.CreateParameter("@Name", SqlDbType.NVarChar, _Name.Text);
        procParams[++i] = da.CreateParameter("@Mobile", SqlDbType.NVarChar, _Mobile.Text);
        procParams[++i] = da.CreateParameter("@Email", SqlDbType.NVarChar, _Email.Text);
        procParams[++i] = da.CreateParameter("@UserName", SqlDbType.NVarChar, _UserName.Text);
        if (_Password.Text == _ConfirmPassword.Text)
            procParams[++i] = da.CreateParameter("@Password", SqlDbType.NVarChar, _Password.Text);

        string sql = string.Format("SELECT ID FROM {0}_LKP_Country WHERE Name ='{1}'", Settings.SchemaPrefix, _Country.SelectedItem.Text);
        object id = da.ExecuteScalar(sql);

        procParams[++i] = da.CreateParameter("@CountryID", SqlDbType.Int, id);
        procParams[++i] = da.CreateParameter("@Active", SqlDbType.Int, 1);

        string proc = string.Format("{0}_Customer_INS", Settings.SchemaPrefix);
        int ret= da.ExecuteNonQuery(proc, procParams);
        
        if (-1 != ret)
        {
            HttpCookie UserData = new HttpCookie("Data1");
            UserData["ID"] = procParams[0].Value.ToString();
            UserData.Expires = System.DateTime.Now.AddDays(1);
            Response.Cookies.Add(UserData);

            Response.Redirect(string.Format("{0}/Defualt/Home.aspx", Settings.ServerUrl));
        }

        da.Dispose();
    }

    
}