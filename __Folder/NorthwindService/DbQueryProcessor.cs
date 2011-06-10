using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Reflection;

namespace Northwind
{
	public class DbQueryProcessor
	{
		DbConnection connection;

		public DbQueryProcessor(DbConnection connection)
		{
			this.connection = connection;
		}
		public IEnumerable<T> Execute<T>()
		{
			Type elementType = typeof(T);
			DbCommand cmd = this.connection.CreateCommand();
			cmd.CommandText = string.Format("SELECT * FROM dbo.[{0}]", elementType.Name);
			DbDataReader reader = cmd.ExecuteReader();			
			var instance = Activator.CreateInstance(
				typeof(ObjectReader<>).MakeGenericType(elementType),
				BindingFlags.Instance | BindingFlags.NonPublic, null,
				new object[] { reader },
				null);
			return (IEnumerable<T>)instance;
		}

	}
}
