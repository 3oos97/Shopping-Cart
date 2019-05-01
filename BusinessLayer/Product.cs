using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using DataAccessLayer;


namespace BusinessLayer
{
   public class Product : ObjectBase
    {
		public static string OBJECT_NAME = "Product";

        public string Name = "";
        public string Description = "";
        public float UnitPrice = NullHelper.Float;
        public int SupplierID = NullHelper.Integer;
        public string Category = "";
        public string Supplier = "";
        public int SortIndex = 0;
        public int Active = 1;

        public Product()
        { SetObjectName(OBJECT_NAME); }
        public Product(int objectID) : this()
        { ID = objectID; }

        protected override bool Reset()
        {
              Name = "";
              Description = "";
              UnitPrice = 0;
              SupplierID = NullHelper.Integer;
              Category = "";
              Supplier = "";
              SortIndex = 0;
              Active = 1;
            return base.Reset();
        }

        protected override bool Read(DataRow dataRow)
        {
            Name = Read<string>(dataRow, "Name", NullHelper.String);
            Description = Read<string>(dataRow, "Description", NullHelper.String);
            UnitPrice = Read<float>(dataRow, "UnitPrice", 0);
            SupplierID = Read<int>(dataRow, "SupplierID", NullHelper.Integer);
            Category = Read<string>(dataRow, "Category", NullHelper.String);
            Supplier = Read<string>(dataRow, "Supplier", NullHelper.String);
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
            int Ret = 12;

            int paramSize = 13;
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
            procParams[++i] = DataAccess.CreateParameter("@UnitPrice", SqlDbType.Float, UnitPrice);
            procParams[++i] = DataAccess.CreateParameter("@SupplierID", SqlDbType.Int, SupplierID);
            procParams[++i] = DataAccess.CreateParameter("@Category", SqlDbType.NVarChar, Category);
            procParams[++i] = DataAccess.CreateParameter("@Supplier", SqlDbType.NVarChar, Supplier);
            procParams[++i] = DataAccess.CreateParameter("@SortIndex", SqlDbType.Int, SortIndex);
            procParams[++i] = DataAccess.CreateParameter("@Active", SqlDbType.Int, Active);

            Ret = Save(procType, ref procParams);

            return Ret;
        }


        public override void Dispose()
        { base.Dispose(); }
    }
}
