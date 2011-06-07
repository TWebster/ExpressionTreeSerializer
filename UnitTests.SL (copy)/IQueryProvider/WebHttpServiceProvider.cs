using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ObjectServices;
using System.Linq.Expressions;
using System.Threading;
using BusinessObjects.WCF;
using BusinessObjects;

namespace UnitTests.SL
{
	public class WebHttpServiceProvider : QueryProvider
	{
		public WebHttpServiceProvider(Uri baseAddress)
		{
			this.baseAddress = baseAddress;
		}
		Uri baseAddress;

		public override string GetQueryText(Expression expression)
		{
			return string.Empty;
		}

		public override object Execute(Expression expression)
		{
			object result = null;
			MethodCallExpression methodcall = (MethodCallExpression)expression;
			Type returnType = methodcall.Method.ReturnType;
			Type elementType = TypeSystem.GetElementType(expression.Type);//get the actual element Type used in the IQueryable<elementType> where Expression.						

			if (returnType.GetInterface("IEnumerable`1", true) != null)
				returnType = elementType.MakeArrayType();			
			
			//it is the responsiblity of this IQueryProvider to handle to calling conventions for
			//calling the WCF service:
			object[] parameters = new object[] { elementType.Name };//GetObjects expects elementType's Name, not the actual return Type
			result = WebHttpRequestClient.SynchronousCall(baseAddress,
					(IWebHttpService isvc) => isvc.GetObjects(parameters));		
			
			//must cast the returned array to the correct IEnumerable<T>:
			dynamic ienumerable1 = LinqHelper.CastToGenericEnumerable((System.Collections.IEnumerable)result, elementType);
			return ienumerable1;
		}
	}
}
