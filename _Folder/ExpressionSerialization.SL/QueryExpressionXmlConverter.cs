using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace ExpressionSerialization
{
	public class QueryExpressionXmlConverter : CustomExpressionXmlConverter
	{
		public QueryExpressionXmlConverter(IDAL dal, TypeResolver resolver)
		{
			this.dal = dal;//client does not need a DAL, only server
			this.resolver = resolver;
		}
		IDAL dal;
		TypeResolver resolver;



		/// <summary>
		/// Re-create the Query, but with a different server-side IQueryProvider.
		/// </summary>
		/// <param name="expressionXml"></param>
		/// <param name="e"></param>
		/// <returns></returns>
		public override bool TryDeserialize(XElement expressionXml, out Expression e)
		{			
			if (expressionXml.Name.LocalName == "Query")
			{
				Type elementType = resolver.GetType(expressionXml.Attribute("elementType").Value);
				IQueryProvider provider = new DynamicQueryProvider();//				
				Type queryType = typeof(Query<>).MakeGenericType(elementType);
				ConstructorInfo ctor = queryType.GetConstructors()[0];
				ParameterExpression p = Expression.Parameter(typeof(IQueryProvider));
				NewExpression newexpr = Expression.New(ctor, p);
				LambdaExpression lambda = Expression.Lambda(newexpr, p);
				var newFn = lambda.Compile();
				dynamic instance = newFn.DynamicInvoke(new object[] { provider });
				e = Expression.Constant(instance);				
				return true;
			}
			e = null;
			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		/// <param name="x"></param>
		/// <returns></returns>
		public override bool TrySerialize(Expression e, out XElement x)
		{
			if (e.NodeType == ExpressionType.Constant 
				&& typeof(IQueryable).IsAssignableFrom(e.Type))
			{
				Type elementType = ((IQueryable)((ConstantExpression)e).Value).ElementType;
				if (typeof(Query<>).MakeGenericType(new Type[] { elementType }) == e.Type)
				{
					x = new XElement("Query",
					new XAttribute("elementType", elementType.FullName));
					return true;
				}				
			}
			x = null;
			return false;
		}
	}
	public abstract class CustomExpressionXmlConverter
	{
		public abstract bool TryDeserialize(XElement expressionXml, out Expression e);
		public abstract bool TrySerialize(Expression expression, out XElement x);
	}
}
