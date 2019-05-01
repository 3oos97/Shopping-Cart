using System;
using System.Configuration;
using System.Web;


namespace DataAccessLayer
{
	public static class Settings
	{
		static Settings()
		{
			_ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
		}

		public static string ReadAppSettings(string key)
		{ return System.Configuration.ConfigurationManager.AppSettings[key].ToString();	}

		private static string _ConnectionString;
		public static string ConnectionString
		{ get { return _ConnectionString; } }

	
		private static string _ServerUrl = ReadAppSettings("serverUrl");
		public static string ServerUrl
		{	get { return _ServerUrl; }	}

		private static string _ServerRootPath = ReadAppSettings("serverRootPath");
		public static string ServerRootPath
		{	get {	return _ServerRootPath; }	}

	
		private static string _VirtualDirectory = ReadAppSettings("virtualDirectory");
		public static string VirtualDirectory
		{ get { return _VirtualDirectory; } }

		private static string _SchemaPrefix = ReadAppSettings("schemaPrefix");
		public static string SchemaPrefix
		{ get { return _SchemaPrefix; } }

	
    
	}
}
