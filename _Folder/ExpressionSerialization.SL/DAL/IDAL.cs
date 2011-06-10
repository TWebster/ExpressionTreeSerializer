using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;


namespace ExpressionSerialization
{
	public interface IDAL<T> : IDAL where T : class
	{
		IEnumerable<T> GetObjects();
		//IList<T> GetObjects(Type elementType);
	}
	public interface IDAL
	{
		//IEnumerable<object> GetObjects();
		IEnumerable<object> GetObjects(Type elementType);
	}
}