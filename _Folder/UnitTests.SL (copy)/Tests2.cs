using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using System.Reflection;
using BusinessObjects.WCF;
using BusinessObjects;
using System.Reflection.Emit;
using System.Linq.Expressions;
using System.IO;
using System.Threading;
using System.ServiceModel.Web;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Dynamic;
using ObjectServices;

namespace UnitTests.SL
{
	[TestClass]
	public class Tests2
	{

		/// <summary>
		/// Very simplified call to IQueryProvider. just returns all results of the IEnumerable.
		/// Does not attempt to evaluate the Where methodcallexpressoin.
		/// </summary>
		[TestMethod]
		public void CallWithProviderTest1()
		{
			
			object[] parameters = new object[] { "Program"};
			Type elementType = typeof(BusinessObjects.Mongoose.Program);
			Type expectedType = elementType.MakeArrayType();
			Uri baseAddress = new Uri("http://localhost:8732");
			QueryProvider provider = new WebHttpServiceProvider(baseAddress);

			ThreadPool.QueueUserWorkItem(state =>
			{
				Query<BusinessObjects.Mongoose.Program> queryable = new Query<BusinessObjects.Mongoose.Program>(provider);
				IQueryable<BusinessObjects.Mongoose.Program> query = from p in queryable
																	 where p.URI != null
																	 select p;				
				var list = query.ToList();
				Assert.IsTrue(list is IEnumerable<BusinessObjects.Mongoose.Program>);
				query.Count();
				
			});
		}

		[TestMethod]
		public void BreakDownExpression()
		{		
			MethodCallExpression mcount = LinqHelper.GetMethodCallExpression((IQueryable<BusinessObjects.Mongoose.Program> query) => query.Count());

			MethodCallExpression mwhere = LinqHelper.GetMethodCallExpression((IQueryable<BusinessObjects.Mongoose.Program> query) => query.Where(x => x.URI == "equals:this:URI"));

			MethodCallExpression mfirst = LinqHelper.GetMethodCallExpression((IQueryable<BusinessObjects.Mongoose.Program> query) => query.First(x => x.ID == 9999));
		}


		/// <summary>
		/// http://blogs.msdn.com/b/carlosfigueira/archive/2010/04/29/consuming-rest-pox-services-in-silverlight-4.aspx
		/// </summary>
		/// <param name="endpointAddress"></param>
		/// <returns></returns>
		IWebHttpService CreateProxy(Uri endpointAddress)
		{
			//var encodingelement = new TextMessageEncodingBindingElement(MessageVersion.None, )
			CustomBinding binding = new CustomBinding(
				new TextMessageEncodingBindingElement(MessageVersion.None, Encoding.UTF8),
				new HttpTransportBindingElement { ManualAddressing = true });
			ChannelFactory<IWebHttpService> factory = new ChannelFactory<IWebHttpService>(binding, new EndpointAddress(endpointAddress));
			factory.Endpoint.Behaviors.Add(new WebHttpBehavior());
			IWebHttpService proxy = factory.CreateChannel();
			((IClientChannel)proxy).Closed += delegate { factory.Close(); };
			return proxy;
		}


		/// <summary>
		/// Very possible to make ExpandoObject implement the methods of an interface
		/// (just define a method on the ExpandoObject), without actually making the 
		/// ExpandoObject an instand of that interface. 
		/// But will get run-time exception of trying to cast the ExpandoObject to an instance of the interface.
		/// </summary>
		[TestMethod]
		public void CreateClientTest()
		{
			Uri baseAddress = new Uri("http://localhost:8732");
			MethodInfo m = typeof(IWebHttpService).GetMethod("GetObjects");
			dynamic expando = new ExpandoObject();
			IWebHttpService client = CreateClient<IWebHttpService>(baseAddress, expando);
			
		}


		static T CreateClient<T>(Uri endpointAddress, dynamic expando)
		{
			Type interfaceType = typeof(T);
			AppDomain domain = Thread.GetDomain();
			AssemblyBuilder assembuilder = domain.DefineDynamicAssembly(
						  new AssemblyName(interfaceType.Name + "_assembly"),
						  AssemblyBuilderAccess.Run);

			ModuleBuilder module = assembuilder.DefineDynamicModule(interfaceType.Name + "_module");
			TypeBuilder proxyType = module.DefineType(interfaceType.Name + "_dynamic", TypeAttributes.Public);
			ConstructorInfo ctor = proxyType.DefineDefaultConstructor(MethodAttributes.Public);			
			ConstructorBuilder constructor;
			ILGenerator constructorIL;
			constructor = proxyType.DefineConstructor(MethodAttributes.Public, CallingConventions.HasThis, Type.EmptyTypes);
			constructorIL = constructor.GetILGenerator();
			constructorIL.Emit(OpCodes.Ret);
			MemberInfo[] members = interfaceType.GetMembers();
			MethodInfo[] methods = interfaceType.GetMethods();
			foreach (MethodInfo methodinfo in methods)
			{
				ImplementMethod(proxyType, null, constructorIL, methodinfo);
			}
			proxyType.AddInterfaceImplementation(interfaceType);
			Type createdType = proxyType.CreateType();
			object instance = CreateTypeInstance(createdType);
			//T instance = CreateTypeInstance(createdType);
			return (T)instance;
		}

		//default ctor:
		static dynamic CreateTypeInstance(Type toCreate)
		{
			//default ctor:
			ConstructorInfo ctor = toCreate.GetConstructors().First(c => c.GetParameters().Count() == 0);
			NewExpression newexpr = Expression.New(ctor);
			LambdaExpression lambda = Expression.Lambda(newexpr);
			var newFn = lambda.Compile();
			return newFn.DynamicInvoke(new object[0]);
		}


		#region System.Reflection.Emit
		/// <summary>
		/// Implements an interface member in a duck proxy type using a given type builder.
		/// If successful, the implemented member will be added to the given proxy member dictionary.
		/// </summary>
		/// <param name="proxyType">Type builder for the duck proxy type.</param>
		/// <param name="proxyMembers">Dictionary of members of the proxy type.</param>
		/// <param name="duckField">Field that holds a reference to the duck object to forward calls to.</param>
		/// <param name="constructorIL">IL generator to use to add code to the constructor if necessary.</param>
		/// <param name="interfaceMember">The interface member to implement.</param>
		static void ImplementMember(TypeBuilder proxyType, ILGenerator constructorIL, MemberInfo interfaceMember)
		{
			if (interfaceMember.MemberType == MemberTypes.Method)
			{
				//ImplementMethod(proxyType, proxyMembers, duckField, constructorIL, (MethodInfo)interfaceMember);
			}
			//else if (interfaceMember.MemberType == MemberTypes.Property)
			//{
			//    ImplementProperty(proxyType, proxyMembers, duckField, constructorIL, (PropertyInfo)interfaceMember);
			//}
			//else if (interfaceMember.MemberType == MemberTypes.Event)
			//{
			//    ImplementEvent(proxyType, proxyMembers, duckField, constructorIL, (EventInfo)interfaceMember);
			//}
			else
			{
				throw new NotSupportedException("Interface defines a member type that is not supported.");
			}
		}
		/// <summary>
		/// Implements an interface method in a duck proxy type using a given type builder.
		/// If successful, the implemented method will be added to the given proxy member dictionary.
		/// </summary>
		/// <param name="proxyType">Type builder for the duck proxy type.</param>
		/// <param name="proxyMembers">Dictionary of members of the proxy type.</param>
		/// <param name="duckField">Field that holds a reference to the duck object to forward calls to.</param>
		/// <param name="constructorIL">IL generator to use to add code to the constructor if necessary.</param>
		/// <param name="interfaceMethod">The interface method to implement.</param>
		static void ImplementMethod(TypeBuilder proxyType, FieldInfo duckField, ILGenerator constructorIL, MethodInfo interfaceMethod)
		{
			MethodInfo duckMethod = null;// FindDuckMethod(interfaceMethod);
			if (duckMethod == null)
			{
				throw new NotImplementedException("Duck type does not implement a method named \"" + interfaceMethod.Name + "\" with compatible parameters and return type.");
			}

			if (!duckMethod.IsSpecialName || (!duckMethod.Name.StartsWith("add_") && !duckMethod.Name.StartsWith("remove_")))
			{
				ParameterInfo[] interfaceMethodParameters = interfaceMethod.GetParameters();
				ParameterInfo[] duckMethodParameters = duckMethod.GetParameters();

				MethodBuilder proxyMethod = null;
				//proxyType.DefineMethod(interfaceMethod.Name, MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.Final, CallingConventions.HasThis, 
				//interfaceMethod.ReturnType, 
				//GetParameterTypes(interfaceMethodParameters));

				GenericTypeParameterBuilder[] genericParameters;

				if (interfaceMethod.IsGenericMethodDefinition)
				{
					Type[] interfaceMethodGenericArguments = interfaceMethod.GetGenericArguments();
					string[] genericParameterNames = new string[interfaceMethodGenericArguments.Length];
					for (int i = 0; i < interfaceMethodGenericArguments.Length; i++)
					{
						genericParameterNames[i] = interfaceMethodGenericArguments[i].Name;
					}
					genericParameters = proxyMethod.DefineGenericParameters(genericParameterNames);
				}
				else
				{
					genericParameters = new GenericTypeParameterBuilder[0];
				}

				ILGenerator proxyMethodIL = proxyMethod.GetILGenerator();

				// Emit IL to load the duck object if the method is not static
				if (!duckMethod.IsStatic)
				{
					// Emit IL to load the proxy instance, then load the value of its duck field
					proxyMethodIL.Emit(OpCodes.Ldarg_0);
					proxyMethodIL.Emit(OpCodes.Ldfld, duckField);
				}

				// Emit IL to load method arguments
				for (int i = 0; i < interfaceMethodParameters.Length; i++)
				{
					// Emit IL to load the argument
					proxyMethodIL.Emit(OpCodes.Ldarg, 1 + i);

					// Emit IL to cast the argument if necessary
					//DuckTyping.EmitCastIL(proxyMethodIL, duckMethodParameters[i].ParameterType, interfaceMethodParameters[i].ParameterType);
				}

				MethodInfo methodToCall;
				if (!duckMethod.IsGenericMethodDefinition)
				{
					methodToCall = duckMethod;
				}
				else
				{
					methodToCall = duckMethod.MakeGenericMethod((Type[])(genericParameters));
				}

				// Emit IL to call the method
				if (!duckMethod.IsStatic)
				{
					proxyMethodIL.Emit(OpCodes.Callvirt, methodToCall);
				}
				else
				{
					proxyMethodIL.Emit(OpCodes.Call, methodToCall);
				}

				// If we are returning something...
				if (duckMethod.ReturnType != typeof(void))
				{
					// Emit IL to cast the return value if necessary
					//DuckTyping.EmitCastIL(proxyMethodIL, interfaceMethod.ReturnType, duckMethod.ReturnType);
				}

				// Emit IL to return.
				proxyMethodIL.Emit(OpCodes.Ret);

				// Add proxy method to proxy member dictionary
				// (This is so that any associated properties or events can refer to it later)
				//if (proxyMembers != null) proxyMembers[duckMethod] = proxyMethod;
			}
		}
		#endregion

	}
}
