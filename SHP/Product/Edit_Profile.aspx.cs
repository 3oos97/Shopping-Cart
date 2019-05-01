using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;

public partial class Product_Edit_Profile : System.Web.UI.Page
{
    DataAccess _DataAccess;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            int custID = ConvertHelper.Convert<int>(Request.Cookies["Data1"]["ID"].ToString(), 0);
            if (0 != custID)
            {
                _DataAccess = new DataAccess();
                string sql = string.Format("SELECT Name,Email,UserName,Password,Mobile,Country FROM {0}_Customer_VIW WHERE ID = {1}", Settings.SchemaPrefix, custID);
                SqlDataReader dr = _DataAccess.ExecuteReader(sql);
                if (dr.Read())
                {
                    _Name.Text = dr["Name"].ToString();
                    _Email.Text = dr["Email"].ToString();
                    _UserName.Text = dr["UserName"].ToString();
                    _Password.Text = dr["Password"].ToString();
                    _Mobile.Text = dr["Mobile"].ToString();
                    _Country.Text = dr["Country"].ToString();
                }
                _DataAccess.Dispose();
            }
        }
    }

    protected void _Save_Click(object sender, EventArgs e)
    {
        _DataAccess = new DataAccess();
      
        int ParamSize = 10;
        SqlParameter[] procParams = new SqlParameter[ParamSize]; int i = 0;

        int custID = ConvertHelper.Convert<int>(Request.Cookies["Data1"]["ID"].ToString(), 0);
        if (0 != custID)
        {
            procParams[i] = _DataAccess.CreateParameter("@ID", SqlDbType.Int, custID);

            string sql = string.Format("SELECT CreateDate FROM {0}_Customer WHERE ID={1}", Settings.SchemaPrefix, Request.Cookies["Data1"]["ID"].ToString());
            object date = _DataAccess.ExecuteScalar(sql);

            procParams[++i] = _DataAccess.CreateParameter("@CreateDate", SqlDbType.DateTime, date.ToString());
        }
        procParams[++i] = _DataAccess.CreateParameter("@ModifyDate", SqlDbType.DateTime, DateTime.Now.ToString());
        procParams[++i] = _DataAccess.CreateParameter("@Name", SqlDbType.NVarChar, _Name.Text);
        procParams[++i] = _DataAccess.CreateParameter("@Mobile", SqlDbType.NVarChar, _Mobile.Text);
        procParams[++i] = _DataAccess.CreateParameter("@Email", SqlDbType.NVarChar, _Email.Text);
        procParams[++i] = _DataAccess.CreateParameter("@UserName", SqlDbType.NVarChar, _UserName.Text);
        procParams[++i] = _DataAccess.CreateParameter("@Password", SqlDbType.NVarChar, _Password.Text);

        object id = _DataAccess.ExecuteScalar(string.Format("SELECT ID FROM {0}_LKP_Country WHERE Name='{1}'", Settings.SchemaPrefix, _Country.SelectedItem.ToString()));
        procParams[++i] = _DataAccess.CreateParameter("@CountryID", SqlDbType.Int, id.ToString());
        procParams[++i] = _DataAccess.CreateParameter("@Active", SqlDbType.Int, 1.ToString());

        string proc = string.Format("{0}_Customer_UPD", Settings.SchemaPrefix);
        int _return = _DataAccess.ExecuteNonQuery(proc, procParams);

        if(-1 != _return)
        {
            _DataAccess.Dispose();
            Response.Redirect(string.Format("{0}/Product/Account_Page.aspx", Settings.ServerUrl));
        }
        else
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Error Edit Again!!!!')", true);

        _DataAccess.Dispose();
    }

  
}