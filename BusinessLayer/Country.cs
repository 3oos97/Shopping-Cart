using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using DataAccessLayer;

namespace BusinessLayer
{
    class Country : ObjectBase
    {
        public static string OBJECT_NAME = "Country";

        public string Name = "";
        public int SortIndex = 0;
        public int Active = 1;

        public Country()
        { SetObjectName(OBJECT_NAME); }
        public Country(int objectID) : this()
        { ID = objectID; }

        protected override bool Reset()
        {
            Name = "";
            SortIndex = 0;
            Active = 1;
            return base.Reset();
        }

        protected override bool Read(DataRow dataRow)
        {
            Name = Read<string>(dataRow, "Name", NullHelper.String);
            SortIndex = Read<int>(dataRow, "SortIndex", 0);
            Active = Read<int>(dataRow, "Active", 1);

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

            int paramSize = 6;
            EnumProcType procType = EnumProcType.Insert;
            ParameterDirection paramDir = ParameterDirection.Output;
            AdaptForSave(ref procType, ref paramDir, ref paramSize);

            SqlParameter[] procParams = new SqlParameter[paramSize]; int i = 0;
            procParams[i] = DataAccess.CreateParameter("@ID", SqlDbType.Int, ID, paramDir);
            if (EnumProcType.Insert == procType)
                procParams[++i] = DataAccess.CreateParameter("@CreateDate", SqlDbType.DateTime, CreateDate, ParameterDirection.Output);
            procParams[++i] = DataAccess.CreateParameter("@ModifyDate", SqlDbType.DateTime, ModifyDate, ParameterDirection.Output);
            procParams[++i] = DataAccess.CreateParameter("@Name", SqlDbType.NVarChar, Name);
            procParams[++i] = DataAccess.CreateParameter("@SortIndex", SqlDbType.Int, SortIndex);
            procParams[++i] = DataAccess.CreateParameter("@Active", SqlDbType.Int, Active);


            Ret = Save(procType, ref procParams);

            return Ret;
        }

        public void Dispose()
        { base.Dispose(); }
    }
}
