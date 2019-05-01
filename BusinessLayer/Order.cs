using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using DataAccessLayer;

namespace BusinessLayer
{
   public class Order : ObjectBase
    {
        public static string OBJECT_NAME = "Order";

        public string ShippingAddress = "";
        public DateTime ShippingDate = DateTime.Now;
        public int CustomerID = NullHelper.Integer;

        public Order()
        { SetObjectName(OBJECT_NAME); }
        public Order(int objectID) : this()
        { ID = objectID; }

        protected override bool Reset()
        {
            ShippingAddress = "";
            ShippingDate = DateTime.Now;
            CustomerID = NullHelper.Integer;
            return base.Reset();
        }

        protected override bool Read(DataRow dataRow)
        {
            ShippingAddress = Read<string>(dataRow, "ShippingAddress", NullHelper.String);
            ShippingDate = Read<DateTime>(dataRow, "ShippingDate", NullHelper.DateTime);
            CustomerID = Read<int>(dataRow, "CustomerID", NullHelper.Integer);
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
            procParams[++i] = DataAccess.CreateParameter("@ShippingAddress", SqlDbType.NVarChar, ShippingAddress);
            procParams[++i] = DataAccess.CreateParameter("@ShippingDate", SqlDbType.DateTime, ShippingDate);
            procParams[++i] = DataAccess.CreateParameter("@CustomerID", SqlDbType.Int, CustomerID);

            Ret = Save(procType, ref procParams);

            return Ret;
        }

        public override void Dispose()
        { base.Dispose(); }
    }
}
