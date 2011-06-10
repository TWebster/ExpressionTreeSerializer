using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using Resolver = ExpressionSerialization.TypeResolver;

namespace ExpressionSerialization
{
	public class MockQueryProvider : QueryProvider
	{
		public MockQueryProvider() : base()
		{
			
		}
		
		public override string GetQueryText(Expression expression)
		{
			return string.Empty;
		}

		public override object Execute(Expression expression)
		{
			throw new NotImplementedException();
		}
	}
}