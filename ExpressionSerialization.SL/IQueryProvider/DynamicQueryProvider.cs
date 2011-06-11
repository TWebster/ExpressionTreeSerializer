using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using Resolver = ExpressionSerialization.TypeResolver;

namespace ExpressionSerialization
{
	public class DynamicQueryProvider : QueryProvider
	{

		public override string GetQueryText(Expression expression)
		{
			return string.Empty;
		}

		public override object Execute(Expression e)
		{
			Type elementType = TypeResolver.GetElementType(e.Type);		
			Type TResult;
			if (typeof(System.Collections.IEnumerable).IsAssignableFrom(e.Type))
			{
				TResult = typeof(IEnumerable<>).MakeGenericType(elementType);				
			}				
			else
			{
				TResult = e.Type;
			}
			
			dynamic ienumerable = GetIEnumerableOf(elementType);
			IQueryable queryable = Queryable.AsQueryable(ienumerable);
			IQueryProvider provider = (IQueryProvider)queryable.Provider;
			
			dynamic result = DelegateExpressionCallToProvider(provider, e, TResult);
			return result;			
		}
		

		internal static dynamic DelegateExpressionCallToProvider(IQueryProvider provider, Expression e, Type TResult)
		{			
			MethodInfo method3 = typeof(IQueryProvider).GetMethods()[3];//Execute<TResult>
			MethodInfo generic = method3.MakeGenericMethod(TResult);
			//dynamic enumerable = generic.Invoke(((IQueryable)provider), new object[] { e });			
			MethodCallExpression execCall = Expression.Call(Expression.Constant(provider), generic,
				new Expression[] { Expression.Constant(e) });
			LambdaExpression lambda = Expression.Lambda(execCall, Expression.Parameter(typeof(Expression)));
			dynamic executed = lambda.Compile().DynamicInvoke(new object[] { Expression.Constant(e) });
			return executed;
		}

		/// <summary>
		/// This method is important to how this IQueryProvider works, for returning the IEnumerable from
		/// which we generate the IQueryable and IQueryProvider to delegate the Execute(Expression) call to.
		/// 
		/// In practice this would be a call to the DAL.
		/// </summary>
		/// <param name="elementType"></param>
		/// <returns></returns>
		internal static dynamic GetIEnumerableOf(Type elementType)
		{
			Type listType = typeof(List<>).MakeGenericType(elementType);			
			dynamic list = CreateDefaultInstance(listType);
			for (int i = 0; i < 10; i++)
			{
				dynamic instance = CreateDefaultInstance(elementType);
				list.Add(instance);
			}
			return list;
		}

		/// <summary>
		/// creates instance using default ctor
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		internal static dynamic CreateDefaultInstance(Type type)
		{
			//default ctor:
			ConstructorInfo ctor = type.GetConstructors().First(c => c.GetParameters().Count() == 0);
			NewExpression newexpr = Expression.New(ctor);
			LambdaExpression lambda = Expression.Lambda(newexpr);
			var newFn = lambda.Compile();
			return newFn.DynamicInvoke(new object[0]);
		}
	}
}