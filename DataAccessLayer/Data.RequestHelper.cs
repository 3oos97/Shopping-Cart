using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Web;
using System.Security;
using System.Collections;


namespace DataAccessLayer
{
	public static class RequestHelper
	{
		public static object Request(string Key)
		{
			// We should serach in the context at first i.e priority is for the context.
			object Ret = HttpContextOnly(Key);
			if (null == Ret)
				Ret = RequestOnly(Key);
			return Ret;
		}
		
		public static VarType Request<VarType>(string Key, VarType def)
		{ return ConvertHelper.Convert<VarType>(Request(Key), def); }

		public static string GetMatchedKey(string Key)
		{
			string Ret = "";
			string[] Keys = HttpContext.Current.Request.Form.AllKeys;
			for (int i = 0; i < Keys.Length; i++)
			{
				if (-1 != Keys[i].IndexOf(Key))
				{
					Ret = Keys[i]; break;
				}
			}
			return Ret;
		}

		public static NameValueCollection ServerVariables
		{ get { return System.Web.HttpContext.Current.Request.ServerVariables; } }

	}
}
