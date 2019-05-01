using System;
using System.Collections; 
using System.Web;
using System.Web.UI;
using System.Reflection;
using System.Web.SessionState;
/// <summary>
/// SessionHandler
/// </summary>
namespace DataAccessLayer
{
	public static class SessionHandler
	{
		public static HttpSessionState Session
		{ get { return HttpContext.Current.Session; } }

		public static VarType Load<VarType>(string Key, VarType def)
		{ return ConvertHelper.Convert<VarType>(Session[Key], def); }

		public static void Save<VarType>(string Key, VarType val)
		{ Session[Key] = val; }
		
		public static HttpServerUtility Server
		{ get { return HttpContext.Current.Server; } }

		public static void RenewSessionID()
		{
			HttpContext context = HttpContext.Current;
			SessionIDManager manager = new SessionIDManager();
			string newID = manager.CreateSessionID(context);
			bool redirected = false;
			bool isAdded = false;
			manager.SaveSessionID(context, newID, out redirected, out isAdded);

			if (null != HttpContext.Current.Request.Cookies["ASP.NET_SessionId"])
				HttpContext.Current.Response.Cookies["ASP.NET_SessionId"].Value = newID;
		}
		public static void ResetSessionID()
		{
			Session.Clear();
			Session.Abandon();
			Session.RemoveAll();

			HttpContext context = HttpContext.Current;
			SessionIDManager manager = new SessionIDManager();
			string newID = manager.CreateSessionID(context);
			bool redirected = false;
			bool isAdded = false;
			manager.SaveSessionID(context, newID, out redirected, out isAdded);

			if (null != HttpContext.Current.Request.Cookies["ASP.NET_SessionId"])
				HttpContext.Current.Response.Cookies["ASP.NET_SessionId"].Value = newID;
		}
		public static void Reset()
		{
			Session.Clear();
			Session.Abandon();
			Session.RemoveAll();
			if (null != HttpContext.Current.Request.Cookies["ASP.NET_SessionId"])
			{
				HttpContext.Current.Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
				HttpContext.Current.Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now;
			}
			ResetSessionID();
		}
	}

	public static class ContextHandler
	{
		public static IDictionary Items
		{ get { return HttpContext.Current.Items; } }

		public static VarType Load<VarType>(string Key, VarType def)
		{ return ConvertHelper.Convert<VarType>(Items[Key], def); }

		public static void Save<VarType>(string Key, VarType val)
		{ Items[Key] = val; }
	}

	public class ViewStateHandler
	{
		private StateBag _ViewState = null;

		public ViewStateHandler(StateBag ViewState)
		{
			_ViewState = ViewState;
		}

		public VarType Load<VarType>(string Key, VarType def)
		{ return ConvertHelper.Convert<VarType>(_ViewState[Key], def); }

		public void Save<VarType>(string Key, VarType val)
		{ _ViewState[Key] = val; }
	}
}