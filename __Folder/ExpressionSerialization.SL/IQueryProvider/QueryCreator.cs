using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Reflection;


namespace ExpressionSerialization
{

	public class QueryCreator
	{
		public QueryCreator() 
			: this(new Func<Type, dynamic>(GetIEnumerableOf)) { }
		public QueryCreator(Func<Type, dynamic> fngetobjects)
		{
			this.fnGetObjects = fngetobjects;
		}

		Func<Type, dynamic> fnGetObjects;
		//Func<Type, IEnumerable<object>> fnGetObjects;

		#region CreateQuery
		public dynamic CreateQuery(Type elementType)
		{
			dynamic ienumerable = this.fnGetObjects(elementType);
			if (!typeof(IEnumerable<>).MakeGenericType(elementType).IsAssignableFrom(ienumerable.GetType()))
				throw new InvalidOperationException(typeof(IEnumerable<>).MakeGenericType(elementType) + " should be assignable from return value of function.");

			IQueryable queryable = Queryable.AsQueryable(ienumerable);
			IQueryProvider provider = (IQueryProvider)queryable.Provider;
			Type queryType = typeof(Query<>).MakeGenericType(elementType);
			ConstructorInfo ctor = queryType.GetConstructors()[1];//Query(IQueryProvider provider, Expression expression)
			ParameterExpression[] parameters = new ParameterExpression[] 
				{ 
					Expression.Parameter(typeof(IQueryProvider)),
					Expression.Parameter(typeof(Expression))
				};

			NewExpression newexpr = Expression.New(ctor, parameters);
			LambdaExpression lambda = Expression.Lambda(newexpr, parameters);
			var newFn = lambda.Compile();
			dynamic query = newFn.DynamicInvoke(new object[] { provider, Expression.Constant(queryable) });
			return query;
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

		#endregion

	}
}