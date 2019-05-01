using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Web;


namespace DataAccessLayer
{
	
	public abstract class ObjectBase : ISDObject
	{
        private static string _Prefix = Settings.SchemaPrefix;
        private static string _Object_Lookup = "Lookup";

        protected DataAccess _DataAccess = null;
        protected DataAccess DataAccess
        {
            get
            {
                if (null == _DataAccess)
                    _DataAccess = new DataAccess();
                return _DataAccess;
            }
        }

        protected string _ObjectName = ""; 

        protected virtual void SetObjectName(string objectName)
		{
			_ObjectName = objectName;
		}
        protected bool _IsLookup = false; 
        protected bool IsLookup
		{ 
			get { return _IsLookup; }
			set { _IsLookup = value; }
		}
		
		protected int _ID = 0; // ObjectID
		public int ID
		{
			get { return _ID; }
			set { _ID = value; }
		}
        
        

		protected DateTime _CreateDate = DateTime.Now;	// common data item
		public DateTime CreateDate
		{
			get { return _CreateDate; }
			set { _CreateDate = value; }
            
		}

		protected DateTime _ModifyDate = DateTime.Now;	// common data item
		public DateTime ModifyDate
		{
			get { return _ModifyDate; }
			set { _ModifyDate = value; }
		}

        protected string GetProcName(EnumProcType procType)
        {
            string Ret = string.Format("{0}_{1}_{2}", _Prefix, (!_IsLookup) ? _ObjectName : _Object_Lookup, EnumHelper.MapProcName(procType)); 

            return Ret;
        }

        // it is generics method to retrieve column values from a data row
        // it is recommended to use this method when populating an object from the database
        protected static VarType Read<VarType>(DataRow dataRow, string columnName, VarType defVal)
		{
			VarType Ret = defVal;

			if (DBNull.Value != dataRow[columnName])
					Ret = (VarType)Convert.ChangeType(dataRow[columnName], typeof(VarType));
			
			return Ret;
		}  // i recommend to use this method

		protected virtual bool Read(DataRow dataRow)
		{
			//	common data mappings (for example: id, create date, modified date)
			ID = Read<int>(dataRow, "ID", NullHelper.Integer);	// ID = GetInteger(dataRow, "ID"); 
			if (!IsLookup)
			{
				CreateDate = Read<DateTime>(dataRow, "CreateDate", NullHelper.DateTime); //	CreateDate = GetDateTime(dataRow, "CreateDate");
				ModifyDate = Read<DateTime>(dataRow, "ModifyDate", NullHelper.DateTime); //	ModifyDate = GetDateTime(dataRow, "ModifyDate"); 
			}

			return true;
		}
        		
		protected void AdaptForSave(ref EnumProcType procType, ref ParameterDirection paramDir, ref int paramSize)
		{
			if (0 != ID)
			{
				procType = EnumProcType.Update;
				paramDir = ParameterDirection.Input;
				if (!_IsLookup)
					paramSize -= 1;
			}
		}
		protected int Save(EnumProcType procType, ref SqlParameter[] procParams)
		{
			int Ret = 0;
			if (EnumProcType.Update == procType)
				DataAccess.ExecuteNonQuery(GetProcName(procType), procParams);
			else
			{
				SqlCommand procCommand;
				DataAccess.ExecuteNonQuery(out procCommand, GetProcName(procType), procParams);
				ID = (int)procCommand.Parameters["@ID"].Value;
				procCommand.Dispose();
			}
			Ret = ID;

			return Ret;
		}

		protected virtual bool Reset()
		{
			ID = 0;
			CreateDate = DateTime.Now;
			ModifyDate = DateTime.Now;
		
			return true;
		}

        protected DataRow GetDataRow(string Where)
        {
            DataRow Ret = null;
            string sql = string.Format("SELECT * FROM {0}_{1}_VIW WHERE {2}", Settings.SchemaPrefix, _ObjectName, Where);
            DataTable dt = DataAccess.ExecuteDataTable(sql);
            if (null != dt && dt.Rows.Count > 0)
                Ret = dt.Rows[0];

            return Ret;
        }
        protected virtual DataRow Populate(string Dummy)
        {
            DataRow Ret = null;

            string where = string.Format(" ID = {0}", ID.ToString()); 
            Ret = GetDataRow(where);

            return Ret;
        }


        protected abstract bool Populate();
				
		public virtual bool Populate(EnumPopulateSource populateSource)
		{
			bool Ret = false;
			switch (populateSource)
			{
				case EnumPopulateSource.Database:
					Ret = Populate(); break;
				case EnumPopulateSource.Reset:
					Ret = Reset(); break;
				case EnumPopulateSource.Request:
					break;
			}

			return Ret;
		}
		public abstract int Save();

		public virtual void Dispose()
		{
            if (null != _DataAccess)
                _DataAccess.Dispose();

        }

    }
}
