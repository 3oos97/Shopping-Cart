using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
	public static class DataListHelper
	{
		public static object ReadField(DataRowView DRV, string FieldName)
		{
			Object Ret = null;

			int idx = DRV.Row.Table.Columns.IndexOf(FieldName);
			Ret = (-1 != idx) ? DRV.Row.ItemArray[idx] : null;

			return Ret;
		}
		public static VarType ReadField<VarType>(DataRowView DRV, string FieldName, VarType DefVal)
		{
			VarType Ret = DefVal;
			object val = ReadField(DRV, FieldName);
			if (null != val)
				Ret = ConvertHelper.Convert<VarType>(val, DefVal);

			return Ret;
		}

	}
}
