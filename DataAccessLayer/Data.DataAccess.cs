using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;


namespace DataAccessLayer
{
	public class DataAccess
	{
		protected SqlConnection _Connection = null;
		protected SqlConnection Connection
		{ get { ValidateConnection(); return _Connection; } }
		
		private string ConnectionString
		{
			get { return Settings.ConnectionString; }
		}
		
		public DataAccess() 
		{}
		public void Dispose()
		{
			if (null != _Connection)
			{
				CloseConnection();
				_Connection.Dispose();
			}
		}

		private void ValidateConnection()
		{
			if (null == _Connection)
				_Connection = new SqlConnection(ConnectionString);
			if (_Connection.State == ConnectionState.Closed)
				_Connection.Open();
		}
		private void CloseConnection()
		{
			if (null != _Connection)
			{
				if (_Connection.State == ConnectionState.Open)
					_Connection.Close();
			}
		}

		protected SqlCommand CreateCommand(CommandType cmdType, string cmdText)
		{
			ValidateConnection();
			SqlCommand Ret = Connection.CreateCommand();
			Ret.CommandType = cmdType;
			Ret.CommandText = cmdText;
			Ret.CommandTimeout = 360;

			return Ret;
		}
		protected SqlCommand CreateCommand(CommandType cmdType, string cmdText, SqlParameter[] cmdParams)
		{
			SqlCommand Ret = CreateCommand(cmdType, cmdText);
			for (int index = 0; index < cmdParams.Length; index++)
			{
				Ret.Parameters.Add(cmdParams[index]);
			}

			return Ret;
		}

		public SqlParameter CreateParameter(string paramName, SqlDbType paramType)
		{
			SqlParameter Ret = new SqlParameter(paramName, paramType);
				
			return Ret;
		}
		public SqlParameter CreateParameter(string paramName, SqlDbType paramType, ParameterDirection direction)
		{
			SqlParameter Ret = CreateParameter(paramName, paramType, DBNull.Value);
			Ret.Direction = direction;

			return Ret;
		}
		public SqlParameter CreateParameter(string paramName, SqlDbType paramType, object paramValue)
		{
			SqlParameter Ret = CreateParameter(paramName, paramType);

			if (paramValue != DBNull.Value)
			{
				switch (paramType)
				{
					case SqlDbType.VarChar:
					case SqlDbType.NVarChar:
					case SqlDbType.Char:
					case SqlDbType.NChar:
					case SqlDbType.Text:
						paramValue = CheckValue((string)paramValue);	break;
					case SqlDbType.DateTime:
						paramValue = CheckValue(Convert.ToDateTime(paramValue));	break;
					case SqlDbType.Int:
						paramValue = CheckValue(Convert.ToInt32(paramValue));	break;
					case SqlDbType.Float:
						paramValue = CheckValue(Convert.ToSingle(paramValue));	break;
					case SqlDbType.Decimal:
						paramValue = CheckValue(Convert.ToDecimal(paramValue));	break;
				}
			}
			Ret.Value = paramValue;

			return Ret;
		}
		public SqlParameter CreateParameter(string paramName, SqlDbType paramType, object paramValue, ParameterDirection direction)
		{
			SqlParameter Ret = CreateParameter(paramName, paramType, paramValue);
			Ret.Direction = direction;

			return Ret;
		}

		protected object CheckValue(string paramValue)
		{
			return (string.IsNullOrEmpty(paramValue)) ? (object)DBNull.Value : paramValue;
		}
		protected object CheckValue(DateTime paramValue)
		{
			return (paramValue.Equals(NullHelper.DateTime)) ? (object)DBNull.Value : paramValue;
		}
		protected object CheckValue(int paramValue)
		{
			//return paramValue;
			return (paramValue.Equals(NullHelper.Integer)) ? (object)DBNull.Value : paramValue;
		}
		protected object CheckValue(float paramValue)
		{
			return (paramValue.Equals(NullHelper.Float)) ? (object)DBNull.Value : paramValue;
		}
		protected object CheckValue(decimal paramValue)
		{
			return (paramValue.Equals(NullHelper.Decimal)) ? (object)DBNull.Value : paramValue;
		}
		
		public object ExecuteScalar(string procName, params SqlParameter[] procParams)
		{
			object Ret = null;
			SqlCommand cmd = null;
			try
			{
				cmd = CreateCommand(CommandType.StoredProcedure, procName, procParams);
				Ret = cmd.ExecuteScalar();
			}
			catch (Exception e)
			{
				throw (new DataAccessException(e.Message, e, "ExecuteScalar"));
			}
			finally
			{
				if (null != cmd) cmd.Dispose();
				CloseConnection();
			}

			return Ret;
		}
		public object ExecuteScalar(string sql)
		{
			object Ret = null;
			SqlCommand cmd = null;
			try
			{
				cmd = CreateCommand(CommandType.Text, sql);
				Ret = cmd.ExecuteScalar();
			}
			catch (Exception e)
			{
				throw (new DataAccessException(e.Message, e, "ExecuteScalar"));
			}
			finally
			{
				if (null != cmd) cmd.Dispose();
				CloseConnection();
			}

			return Ret;
		}
				
		public SqlDataReader ExecuteReader(string procName, params SqlParameter[] procParams)
		{
			SqlDataReader Ret = null;
			SqlCommand cmd = null;
			try
			{
				cmd = CreateCommand(CommandType.StoredProcedure, procName, procParams);
				Ret = cmd.ExecuteReader();
			}
			catch (Exception e)	{	
				throw(new DataAccessException(e.Message, e, "ExecuteReader"));	
			}
			finally	{
				if (null != cmd)
					cmd.Dispose();
			}

			return Ret;
		}
		public SqlDataReader ExecuteReader(string sql)
		{
			SqlDataReader Ret = null;
			SqlCommand cmd = null;
			try
			{
				cmd = CreateCommand(CommandType.Text, sql);
				Ret = cmd.ExecuteReader();
			}
			catch (Exception e)
			{
				throw (new DataAccessException(e.Message, e, "ExecuteReader"));
			}
			finally
			{
				if (null != cmd)
					cmd.Dispose();
			}

			return Ret;
		}
		
		public int ExecuteNonQuery(string procName, params SqlParameter[] procParams)
		{
			SqlCommand procCommand;
			int Ret = ExecuteNonQuery(out procCommand, procName, procParams);
			if (null != procCommand)
				procCommand.Dispose(); 

			return Ret;
		}
		public int ExecuteNonQuery(out SqlCommand procCommand, string procName, params SqlParameter[] procParams)
		{
			int Ret = -1;
			
			procCommand = null;
			try
			{
				procCommand = CreateCommand(CommandType.StoredProcedure, procName, procParams);
				Ret = procCommand.ExecuteNonQuery(); 
			}
			catch (Exception e)	{
				throw(new DataAccessException(e.Message, e, "ExecuteNonQuery"));
			}
			finally	{
				CloseConnection();
			}

			return Ret;
		}
		public int ExecuteNonQuery(string sql)
		{
			SqlCommand txtCommand;
			int Ret = ExecuteNonQuery(out txtCommand, sql);
			if (null != txtCommand)
				txtCommand.Dispose();

			return Ret;
		}
		public int ExecuteNonQuery(out SqlCommand txtCommand, string sql)
		{
			int Ret = -1;

			txtCommand = null;
			try
			{
				txtCommand = CreateCommand(CommandType.Text, sql);
				Ret = txtCommand.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				throw (new DataAccessException(e.Message, e, "ExecuteNonQuery"));
			}
			finally
			{
				CloseConnection();
			}

			return Ret;
		}
		
		public DataSet ExecuteDataSet(string procName, params SqlParameter[] procParams)
		{
			SqlCommand procCommand;
			DataSet Ret = ExecuteDataSet(out procCommand, procName, procParams);
			if (null != procCommand)
				procCommand.Dispose();

			return Ret;
		}
		public DataSet ExecuteDataSet(out SqlCommand procCommand, string procName, params SqlParameter[] procParams)
		{
			DataSet Ret = new DataSet();
			SqlDataAdapter dataAdapter = new SqlDataAdapter();
			procCommand = null;
			try
			{
				procCommand = CreateCommand(CommandType.StoredProcedure, procName, procParams);
				dataAdapter.SelectCommand = procCommand;
				dataAdapter.Fill(Ret);
			}
			catch (Exception e)	{
				throw(new DataAccessException(e.Message, e, "ExecuteDataSet"));
			}
			finally	{
				if (null != dataAdapter)	dataAdapter.Dispose();
				CloseConnection(); 
			}			
						
			return Ret;
		}
		public DataSet ExecuteDataSet(string sql)
		{
			SqlCommand txtCommand;
			DataSet Ret = ExecuteDataSet(out txtCommand, sql);
			if (null != txtCommand)
				txtCommand.Dispose();

			return Ret;
		}
		public DataSet ExecuteDataSet(out SqlCommand txtCommand, string sql)
		{
			DataSet Ret = new DataSet();
			SqlDataAdapter dataAdapter = new SqlDataAdapter();
			txtCommand = null;
			try
			{
				txtCommand = CreateCommand(CommandType.Text, sql);
				dataAdapter.SelectCommand = txtCommand;
				dataAdapter.Fill(Ret);
			}
			catch (Exception e)
			{
				throw (new DataAccessException(e.Message, e, "ExecuteDataSet"));
			}
			finally
			{
				if (null != dataAdapter) dataAdapter.Dispose();
				CloseConnection();
			}

			return Ret;
		}
		
		public DataTable ExecuteDataTable(string procName, params SqlParameter[] procParams)
		{
			SqlCommand procCommand;
			DataTable Ret = ExecuteDataTable(out procCommand, procName, procParams);
			if (null != procCommand)
				procCommand.Dispose();

			return Ret; 
		}
		public DataTable ExecuteDataTable(out SqlCommand procCommand, string procName, params SqlParameter[] procParams)
		{
			DataTable Ret = null;
			DataSet ds = ExecuteDataSet(out procCommand, procName, procParams);
			if (null != ds && ds.Tables.Count > 0)
				Ret = ds.Tables[0]; 
			
			return Ret;
		}
		public DataTable ExecuteDataTable(string sql)
		{
			SqlCommand txtCommand;
			DataTable Ret = ExecuteDataTable(out txtCommand, sql);
			if (null != txtCommand)
				txtCommand.Dispose();

			return Ret;
		}
		public DataTable ExecuteDataTable(out SqlCommand txtCommand, string sql)
		{
			DataTable Ret = null;
			DataSet ds = ExecuteDataSet(out txtCommand, sql);
			if (null != ds && ds.Tables.Count > 0)
				Ret = ds.Tables[0];

			return Ret;
		}
		
		public DataRow ExecuteDataRow(string procName, params SqlParameter[] procParams)
		{
			SqlCommand procCommand;
			DataRow Ret = ExecuteDataRow(out procCommand, procName, procParams);
			if (null != procCommand)
				procCommand.Dispose();

			return Ret;
		}
		public DataRow ExecuteDataRow(out SqlCommand procCommand, string procName, params SqlParameter[] procParams)
		{
			DataRow Ret = null;
			DataTable dt = ExecuteDataTable(out procCommand, procName, procParams);
			if (null != dt && dt.Rows.Count > 0)
				Ret = dt.Rows[0];

			return Ret;
		}
		public DataRow ExecuteDataRow(string sql)
		{
			SqlCommand txtCommand;
			DataRow Ret = ExecuteDataRow(out txtCommand, sql);
			if (null != txtCommand)
				txtCommand.Dispose();

			return Ret;
		}
		public DataRow ExecuteDataRow(out SqlCommand txtCommand, string sql)
		{
			DataRow Ret = null;
			DataTable dt = ExecuteDataTable(out txtCommand, sql);
			if (null != dt && dt.Rows.Count > 0)
				Ret = dt.Rows[0];
			return Ret;
		}
	}
}
