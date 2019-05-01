using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer 
{
		
	public enum EnumProcType				
	{ Unknown, Insert, Update, Delete };

	public enum EnumPopulateSource	
	{ Unknown, Reset, Request, Database };
	
	

	public class EnumHelper
	{
		public static string Enum2String(string[] array, int Enum, string Default)
		{
			string Ret = Default;
			if (Enum >= 0 && Enum < array.Length)
				Ret = array[Enum];

			return Ret;
		}
		public static int String2Enum(string[] array, string Value, int Default)
		{
			int Ret = Default;
			char[] spliter = { ',', '|' };
			string[] Values = Value.Split(spliter);

			for (int i = 0; i < Values.Length; i++)
			{
				for (int j = 0; j < array.Length; j++)
				{
					if (Values[i].Equals(array[j]))
					{
						Ret |= j;
					}
				}
			}
			return Ret;
		}
		public static EnumType String2Enum<EnumType>(string name)
		{
			return (EnumType)Enum.Parse(typeof(EnumType), name);
		}

		public static EnumType MapEnum<EnumType>(string val)
		{ return (EnumType)Enum.Parse(typeof(EnumType), val); }
		public static string MapEnum<EnumType>(EnumType val)
		{ return Enum.GetName(typeof(EnumType), val); }

	
		private static string[] _ProcTypes		=	{ "UNK", "INS", "UPD", "DEL"};
		public static string MapProcName(EnumProcType ProcType)
		{ 
			return Enum2String(_ProcTypes, (int)ProcType, _ProcTypes[0]); 
		}
		

	}
	
}
