using System;

namespace DataAccessLayer
{
	public class DataAccessException : ApplicationException
	{
		private string _SQL;
		public string SQL
		{ get { return _SQL; } }

		public override string Message
		{
			get
			{
				string Ret = base.Message;
				Ret += "Executing SQL: " + this._SQL;
				Ret += "[Inner Exception: " + base.InnerException.ToString() + "]";

				return Ret;
			}
		}
		public override string ToString()
		{
			return "DataAccessLayer.DataTierException: " + this.Message;
		}

		public DataAccessException(string Message, Exception e, string Sql)	: base(Message, e)
		{
			_SQL = Sql;
		}
	}
}
