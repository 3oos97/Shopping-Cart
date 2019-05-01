using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using DataAccessLayer;

namespace BusinessLayer
{
   public class OrderProduct : ObjectBase
    {
        public static string OBJECT_NAME = "OrderProduct";

        public int OrderID = NullHelper.Integer;
        public int ProductID = NullHelper.Integer;
        public float UnitPrice = 0;
        public float Quantity = 1;

        public OrderProduct()
        { SetObjectName(OBJECT_NAME); }
        public OrderProduct(int objectID) : this()
        { ID = objectID; }

        protected override bool Reset()
        {
             OrderID = NullHelper.Integer;
             ProductID = NullHelper.Integer;
             UnitPrice = 0;
             Quantity = 1;
             return base.Reset();
        }

        protected override bool Read(DataRow dataRow)
        {
            OrderID = Read<int>(dataRow, "OrderID", NullHelper.Integer);
            ProductID = Read<int>(dataRow, "ProductID", NullHelper.Integer);
            UnitPrice = Read<float>(dataRow, "UnitPrice", 0);
            Quantity = Read<int>(dataRow, "Quantity", 1);
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

            int paramSize = 7;
            EnumProcType procType = EnumProcType.Insert;
            ParameterDirection paramDir = ParameterDirection.Output;
            AdaptForSave(ref procType, ref paramDir, ref paramSize);

            SqlParameter[] procParams = new SqlParameter[paramSize]; int i = 0;
            procParams[i] = DataAccess.CreateParameter("@ID", SqlDbType.Int, ID, paramDir);
            if (EnumProcType.Insert == procType)
                procParams[++i] = DataAccess.CreateParameter("@CreateDate", SqlDbType.DateTime, CreateDate, ParameterDirection.Output);
            procParams[++i] = DataAccess.CreateParameter("@ModifyDate", SqlDbType.DateTime, ModifyDate, ParameterDirection.Output);
            procParams[++i] = DataAccess.CreateParameter("@OrderID", SqlDbType.Int, OrderID);
            procParams[++i] = DataAccess.CreateParameter("@ProductID", SqlDbType.Int, ProductID);
            procParams[++i] = DataAccess.CreateParameter("@UnitPrice", SqlDbType.Float, UnitPrice);
            procParams[++i] = DataAccess.CreateParameter("@Quantity", SqlDbType.Int, Quantity);

            Ret = Save(procType, ref procParams);

            return Ret;
        }


        public override void Dispose()
        { base.Dispose(); }

    }
}
