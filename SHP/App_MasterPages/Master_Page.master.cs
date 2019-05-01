using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;

public partial class Home_Page : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HttpCookie cookie = Request.Cookies["Data1"];

            if (null != cookie)
            {
                _LogIn.Visible = false;
                _SignUp.Visible = false;

                _Account.Visible = true;
                _Cart.Visible = true;
                _LogOut.Visible = true;

            }
        }
    }

    protected void _LogIn_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("{0}/Product/Log_In.aspx", Settings.ServerUrl));
    }

    protected void _SignUp_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("{0}/Product/Sign_Up.aspx", Settings.ServerUrl));
    }

    protected void _Home_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("{0}/Defualt/Home.aspx", Settings.ServerUrl));
    }

    protected void _Account_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect(string.Format("{0}/Product/Account_Page.aspx", Settings.ServerUrl));
    }

    protected void _LogOut_Click(object sender, ImageClickEventArgs e)
    {
        HttpCookie cookie = new HttpCookie("Data1");
        cookie.Expires = DateTime.Now.AddDays(-1d);
        Response.Cookies.Add(cookie);

        _LogIn.Visible = true;
        _SignUp.Visible = true;

        _Account.Visible = false;
        _Cart.Visible = false;
        _LogOut.Visible = false;

        Response.Redirect(string.Format("{0}/Defualt/Home.aspx", Settings.ServerUrl));
        
    }

    protected void _Cart_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect(string.Format("{0}/Product/MyCart.aspx", Settings.ServerUrl));
    }
}
