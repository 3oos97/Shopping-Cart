using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
	public static class ConvertHelper
	{
		public static  VarType Convert<VarType>(object Value, VarType def) //where VarType : Type
		{
			VarType Ret = def;
			if (null != Value && !(Value is DBNull))
			{
				try
				{
					Ret = (VarType)System.Convert.ChangeType(Value, typeof(VarType));
				}
				catch
				{
					//this is a specialhandle for casting enum values
					try
					{
						Ret = (VarType)System.Convert.ChangeType(Value, typeof(int));
					}
					catch
					{
						;
					}
				}
			}
			return Ret;
		}
		public static int StringToInt(string val)
		{
			int Ret = 0;
			//Ret = System.Convert.ToInt32(val);
			int.TryParse(val, out Ret);
			return Ret;
		}
	}
}
