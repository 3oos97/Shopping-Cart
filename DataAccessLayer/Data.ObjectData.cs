using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Web;


namespace DataAccessLayer
{
	public abstract class ObjectData : ISDObject
	{
		private static string _Prefix = Settings.SchemaPrefix;
		private static string _Filter_Select = "__FILTER_SELECT";
		private static string _Filter_Count = "__FILTER_COUNT";
		private static string _Filter_Index = "__FILTER_INDEX";
		private static string _Object_Toggle = "__OBJECT_TOGGLE";
		private static string _Object_Lookup = "Lookup";

		protected string _ObjectName = "";
		public string ObjectName
		{	get { return _ObjectName; }	}
		protected abstract void SetObjectName(string objectName);
		
		protected DataAccess _DataAccess = null;
		protected DataAccess DataAccess
		{ get 
			{	
				if (null == _DataAccess)
					_DataAccess = new DataAccess(); 
				return _DataAccess; 
			} 
		}

		protected bool _IsLookup = false;
										
		protected string GetTableName()
		{
			string Ret = "";
			Ret = (_IsLookup) ? "_LKP" : "";
			Ret = string.Format("{0}{1}_{2}", _Prefix, Ret, _ObjectName);
			return Ret;
		}
		protected string GetViewName()
		{
			return string.Format("{0}_{1}_{2}", _Prefix, _ObjectName, "VIW");
		}
		protected string GetProcName(EnumProcType procType)
		{
			string Ret = "";
			switch (procType)
			{ 
				case EnumProcType.Select:
					Ret = string.Format("{0}{1}", _Prefix, _Filter_Select); break;

				case EnumProcType.Count:
					Ret = string.Format("{0}{1}", _Prefix, _Filter_Count); break;

				case EnumProcType.Index:
					Ret = string.Format("{0}{1}", _Prefix, _Filter_Index); break;

				case EnumProcType.Toggle:
					Ret = string.Format("{0}{1}", _Prefix, _Object_Toggle); break;
				
				default:
					Ret = string.Format("{0}_{1}_{2}", _Prefix, (!_IsLookup) ? _ObjectName : _Object_Lookup, EnumHelper.MapProcName(procType)); break;
			}
			
			return Ret;
		}
		
		protected DataTable GetDataTable(ObjectFilter objectFilter)
		{
			SqlParameter[] procParams = new SqlParameter[5];
			procParams[0] = DataAccess.CreateParameter("@View", SqlDbType.NVarChar, GetViewName());
			procParams[1] = DataAccess.CreateParameter("@HitFrom", SqlDbType.Int, (null != objectFilter.HitFrom) ? objectFilter.HitFrom : NullHelper.Integer);
			procParams[2] = DataAccess.CreateParameter("@HitTo", SqlDbType.Int, (null != objectFilter.HitTo) ? objectFilter.HitTo : NullHelper.Integer);
			procParams[3] = DataAccess.CreateParameter("@Where", SqlDbType.NVarChar, objectFilter.Where);
			procParams[4] = DataAccess.CreateParameter("@OrderBy", SqlDbType.NVarChar, objectFilter.OrderBy);

			return DataAccess.ExecuteDataTable(GetProcName(EnumProcType.Select), procParams);
		}
		protected DataRow GetDataRow(ObjectFilter objectFilter)
		{
			DataRow Ret = null;
			DataTable dt = GetDataTable(objectFilter);
			if (null != dt && dt.Rows.Count > 0)
				Ret = dt.Rows[0];

			return Ret;
		}
		protected int CountFromDatabase(ObjectFilter objectFilter)
		{
			SqlParameter[] procParams = new SqlParameter[2];
			procParams[0] = DataAccess.CreateParameter("@View", SqlDbType.NVarChar, GetViewName());
			procParams[1] = DataAccess.CreateParameter("@Where", SqlDbType.NVarChar, objectFilter.Where);
			
			return ConvertHelper.Convert<int>(DataAccess.ExecuteScalar(GetProcName(EnumProcType.Count), procParams), 0);
		}
		protected int IndexFromDatabase(ObjectFilter objectFilter, int objectID)
		{
			SqlParameter[] procParams = new SqlParameter[4];
			procParams[0] = DataAccess.CreateParameter("@View", SqlDbType.NVarChar, GetViewName());
			procParams[1] = DataAccess.CreateParameter("@ID", SqlDbType.Int, objectID);
			procParams[2] = DataAccess.CreateParameter("@Where", SqlDbType.NVarChar, objectFilter.Where);
			procParams[3] = DataAccess.CreateParameter("@OrderBy", SqlDbType.NVarChar, objectFilter.OrderBy);

			return ConvertHelper.Convert<int>(DataAccess.ExecuteScalar(GetProcName(EnumProcType.Index), procParams), -1);
		}

		public int Delete(int objectID, bool bypassSecurity)
		{
			if (!bypassSecurity)
			{
				FunctionSecurityContext fsc = new FunctionSecurityContext(Functions.FunctionName_Delete, _ObjectName, objectID);
				PermissionsHelper.VerifyPermission(fsc);
			}

			int size = (_IsLookup) ? 3 : 2;
			SqlParameter[] procParams = new SqlParameter[size];
			procParams[0] = DataAccess.CreateParameter("@UserID", SqlDbType.Int, LoginToken.UserID);
			procParams[1] = DataAccess.CreateParameter("@ID", SqlDbType.Int, objectID);
			if (_IsLookup)
				procParams[2] = DataAccess.CreateParameter("@ObjectName", SqlDbType.NVarChar, _ObjectName);

			return DataAccess.ExecuteNonQuery(GetProcName(EnumProcType.Delete), procParams);
		}
		public int Delete(int objectID)
		{
			return Delete(objectID, false);
		}
		public int Delete(string objectID, bool bypassSecurity)
		{
			string[] ObjectID = objectID.Split(',');
			int[] ID = Array.ConvertAll<string, int>(ObjectID, ConvertHelper.StringToInt);
			return Delete(ID, bypassSecurity);
		}
		public int Delete(string objectID)
		{
			return Delete(objectID, false);
		}
		public int Delete(int[] objectID, bool bypassSecurity)
		{
			int Ret = 0;
			for (int i = 0; i < objectID.Length; i++)
				Ret += Delete(objectID[i], bypassSecurity);

			return Ret;
		}
		public int Delete(int[] objectID)
		{
			return Delete(objectID, false);
		}
				
		public int Toggle(int objectID, string fieldName)
		{
			SqlParameter[] procParams = new SqlParameter[4];
			procParams[0] = DataAccess.CreateParameter("@UserID", SqlDbType.Int, LoginToken.UserID);
			procParams[1] = DataAccess.CreateParameter("@ID", SqlDbType.Int, objectID);
			procParams[2] = DataAccess.CreateParameter("@Table", SqlDbType.NVarChar, GetTableName());
			procParams[3] = DataAccess.CreateParameter("@Column", SqlDbType.NVarChar, fieldName);
			return DataAccess.ExecuteNonQuery(GetProcName(EnumProcType.Toggle), procParams);
		}
		public int Toggle(string objectID, string fieldName)
		{
			string[] ObjectID = objectID.Split(',');
			int[] ID = Array.ConvertAll<string, int>(ObjectID, ConvertHelper.StringToInt);
			return Toggle(ID, fieldName);
		}
		public int Toggle(int[] objectID, string fieldName)
		{
			int Ret = 0;
			for (int i = 0; i < objectID.Length; i++)
				Ret += Toggle(objectID[i], fieldName);

			return Ret;
		}
			
		public virtual void Dispose()
		{
			if (null != _DataAccess)
				_DataAccess.Dispose();
		}
	}
}
