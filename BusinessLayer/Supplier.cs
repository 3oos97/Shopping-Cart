using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using DataAccessLayer;

namespace BusinessLayer
{
  public class Supplier : ObjectBase
    {
        public static string OBJECT_NAME = "Supplier";

        public string Name = "";
        public string Description = "";
        public string Email = "";
        public string Homepage = "";
        public int Active = 1;

        public Supplier()
        { SetObjectName(OBJECT_NAME); }
        public Supplier(int objectID) : this()
        { ID = objectID; }

        protected override bool Reset()
        {
              Name = "";
              Description = "";
              Email = "";
              Homepage = "";
              Active = 1;

            return base.Reset();
        }

        protected override bool Read(DataRow dataRow)
        {
            Name = Read<string>(dataRow, "Name", NullHelper.String);
            Description = Read<string>(dataRow, "Description", NullHelper.String);
            Email = Read<string>(dataRow, "Email", NullHelper.String);
            Homepage = Read<string>(dataRow, "Homepage", NullHelper.String);
            Active = Read<int>(dataRow,"Active", 1);
            return base.Read(dataRow);
        }

        protected override bool Populate()
        {
            bool Ret = false;
            DataRow row = Populate("");
            if (null != row && row.ItemArray.Length > 0)
                Ret = Read(row);

            return Ret;
        }

        public override int Save()
        {
            int Ret = 0;

            int paramSize = 8;
            EnumProcType procType = EnumProcType.Insert;
            ParameterDirection paramDir = ParameterDirection.Output;
            AdaptForSave(ref procType, ref paramDir, ref paramSize);

            SqlParameter[] procParams = new SqlParameter[paramSize]; int i = 0;
            procParams[i] = DataAccess.CreateParameter("@ID", SqlDbType.Int, ID, paramDir);
            if (EnumProcType.Insert == procType)
                procParams[++i] = DataAccess.CreateParameter("@CreateDate", SqlDbType.DateTime, CreateDate, ParameterDirection.Output);
            procParams[++i] = DataAccess.CreateParameter("@ModifyDate", SqlDbType.DateTime, ModifyDate, ParameterDirection.Output);
            procParams[++i] = DataAccess.CreateParameter("@Name", SqlDbType.NVarChar, Name);
            procParams[++i] = DataAccess.CreateParameter("@Description", SqlDbType.NVarChar, Description);
            procParams[++i] = DataAccess.CreateParameter("@Email", SqlDbType.NVarChar, Email);
            procParams[++i] = DataAccess.CreateParameter("@Homepage", SqlDbType.NVarChar, Homepage);
            procParams[++i] = DataAccess.CreateParameter("@Active", SqlDbType.Int, Active);

            Ret = Save(procType, ref procParams);

            return Ret;
        }

        public override void Dispose()
        { base.Dispose(); }
    }

}
