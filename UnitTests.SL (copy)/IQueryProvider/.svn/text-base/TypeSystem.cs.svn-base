using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace ExpressionSerialization
{
	public static class TypeSystem
	{
		public static Type FindType(string typeName, IEnumerable<Type> genericArgumentTypes)
		{
			return FindType(typeName).MakeGenericType(genericArgumentTypes.ToArray());
		}

		public static Type FindType(string typeName)
		{
			Type type;
			if (string.IsNullOrEmpty(typeName))
				throw new ArgumentNullException("typeName");

			// First - try all replacers
			type = ResolveTypeFromString(typeName);
			//type = typeReplacers.Select(f => f(typeName)).FirstOrDefault();
			if (type != null)
				return type;

			// If it's an array name - get the element type and wrap in the array type.
			if (typeName.EndsWith("[]"))
				return FindType(typeName.Substring(0, typeName.Length - 2)).MakeArrayType();

			IEnumerable<Assembly> assemblies;

#if SILVERLIGHT
			 assemblies = new Assembly[] { typeof(ExpressionType).Assembly, 
				 typeof(string).Assembly, 
				 typeof(System.ServiceModel.Channels.Binding).Assembly,
 				 typeof(System.ServiceModel.Description.WebHttpBehavior).Assembly,
				 Assembly.GetExecutingAssembly(),
				 typeof(XElement).Assembly,
			 };
#else
			assemblies = AppDomain.CurrentDomain.GetAssemblies();
#endif
			// try all loaded types
			foreach (Assembly assembly in assemblies)
			{
				type = assembly.GetType(typeName);
				if (type != null)
					return type;
			}

			// Second - try just plain old Type.GetType()
			type = Type.GetType(typeName, false, true);
			if (type != null)
				return type;

			throw new ArgumentException("Could not find a matching type", typeName);
		}

		public static MethodInfo GetMethod(Type declaringType, string name, Type[] parameterTypes, Type[] genArgTypes)
		{
			var methods = from mi in declaringType.GetMethods()
						  where mi.Name == name
						  select mi;
			foreach (var method in methods)
			{
				// Would be nice to remvoe the try/catch
				try
				{
					MethodInfo realMethod = method;
					if (method.IsGenericMethod)
					{
						realMethod = method.MakeGenericMethod(genArgTypes);
					}
					var methodParameterTypes = realMethod.GetParameters().Select(p => p.ParameterType);
					if (MatchPiecewise(parameterTypes, methodParameterTypes))
					{
						return realMethod;
					}
				}
				catch (ArgumentException)
				{
					continue;
				}
			}
			return null;
		}
	
		public static Type GetElementType(Type seqType)
		{
			Type ienum = FindIEnumerable(seqType);
			if (ienum == null) return seqType;
			return ienum.GetGenericArguments()[0];
		}
		private static Type FindIEnumerable(Type seqType)
		{
			if (seqType == null || seqType == typeof(string))
				return null;
			if (seqType.IsArray)
				return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType());
			if (seqType.IsGenericType)
			{
				foreach (Type arg in seqType.GetGenericArguments())
				{
					Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);
					if (ienum.IsAssignableFrom(seqType))
					{
						return ienum;
					}
				}
			}
			Type[] ifaces = seqType.GetInterfaces();
			if (ifaces != null && ifaces.Length > 0)
			{
				foreach (Type iface in ifaces)
				{
					Type ienum = FindIEnumerable(iface);
					if (ienum != null) return ienum;
				}
			}
			if (seqType.BaseType != null && seqType.BaseType != typeof(object))
			{
				return FindIEnumerable(seqType.BaseType);
			}
			return null;
		}
		public static bool HasBaseType(this Type thisType, Type baseType)
		{
			while (thisType.BaseType != null && thisType.BaseType != typeof(System.Object))
			{
				if (thisType.BaseType == baseType)
					return true;
				thisType = thisType.BaseType;
			}

			return false;
		}
		/// <summary>
		/// for finding KnownTypes when using DataContractSerializer during send/receive of HTTP request.
		/// </summary>
		/// <param name="expectedType"></param>
		/// <returns></returns>
		public static IEnumerable<Type> GetBaseTypes(this Type expectedType)
		{
			List<Type> list = new List<Type>();
			list.Add(expectedType);
			if (expectedType.IsArray)
			{
				expectedType = expectedType.GetElementType();
				list.Add(expectedType);
			}
			else
				list.Add(expectedType.MakeArrayType());

			while (expectedType.BaseType != null && expectedType.BaseType != typeof(System.Object))
			{
				expectedType = expectedType.BaseType;
				list.Add(expectedType);
			}
			return list;
		}
		/// <summary>
		/// for finding KnownType(s)
		/// </summary>
		/// <param name="baseType"></param>
		/// <returns></returns>
		public static List<Type> FindDerivedTypes(this Type baseType)
		{
			Assembly a = baseType.Assembly;
			var derived = from anyType in a.GetTypes()
						  where HasBaseType(anyType, baseType)
						  select anyType;
			var list = derived.ToList();
			return list;
		}

		static bool IsNullableType(this Type t)
		{
			return t.IsValueType && t.Name == "Nullable`1";
		}
		static bool HasInheritedProperty(this Type declaringType, PropertyInfo pInfo)
		{
			if (pInfo.DeclaringType != declaringType)
				return true;

			while (declaringType.BaseType != null && declaringType.BaseType != typeof(System.Object))
			{
				foreach (var baseP in declaringType.BaseType.GetProperties())
				{
					if (baseP.Name == pInfo.Name && baseP.PropertyType == pInfo.PropertyType)
						return true;
				}
				declaringType = declaringType.BaseType;
			}
			return false;
		}

		public static string ToGenericTypeFullNameString(this Type t)
		{
			if (t.FullName == null && t.IsGenericParameter)
				return t.GenericParameterPosition == 0 ? "T" : "T" + t.GenericParameterPosition;

			if (!t.IsGenericType)
				return t.FullName;

			string value = t.FullName.Substring(0, t.FullName.IndexOf('`')) + "<";
			Type[] genericArgs = t.GetGenericArguments();
			List<string> list = new List<string>();
			for (int i = 0; i < genericArgs.Length; i++)
			{
				value += "{" + i + "},";
				string s = ToGenericTypeFullNameString(genericArgs[i]);
				list.Add(s);
			}
			value = value.TrimEnd(',');
			value += ">";
			value = string.Format(value, list.ToArray<string>());
			return value;

		}

		public static string ToGenericTypeNameString(this Type t)
		{
			string fullname = ToGenericTypeFullNameString(t);
			fullname = fullname.Substring(fullname.LastIndexOf('.') + 1).TrimEnd('>');
			return fullname;
		}

		static bool MatchPiecewise<T>(IEnumerable<T> first, IEnumerable<T> second)
		{
			T[] firstArray = first.ToArray();
			T[] secondArray = second.ToArray();
			if (firstArray.Length != secondArray.Length)
				return false;
			for (int i = 0; i < firstArray.Length; i++)
				if (!firstArray[i].Equals(secondArray[i]))
					return false;
			return true;
		}
		static Type ResolveTypeFromString(string typeString) { return null; }
		static string ResolveStringFromType(Type type) { return null; }

	}
}
