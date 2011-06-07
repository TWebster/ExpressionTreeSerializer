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



namespace UnitTests.SL
{

	
	[TestClass]
	public class Tests1
	{

		/// <summary>
		/// Basic example of using compile-time inference to get a MethodCallExpression.
		/// For example, the user creates a lambda expression that has a method call.
		/// Create a MethodCallExpression from that, and then evaluate the arguments, if any.
		/// </summary>
		[TestMethod]
		public void EvaluateArgsInMethodCall()
		{
			
			object[] args = new object[] { "Contact", 20 };
			Expression<Func<IWebHttpService,BusinessObjects.Orca.SequencedObject[]>> fnexpr 
				= ((IWebHttpService c) => c.GetObjects(args));
			MethodCallExpression m = (MethodCallExpression)fnexpr.Body;
			object[] evaldargs = getArgumentValues(m);
		}

		/// <summary>
		/// whichever caller expects synchronous behavior must surround the call with a
		/// ThreadPool.QueueUserWorkItem call, or otherwise call the synchronous-like method on a 
		/// thread separate from the main UI thread.
		/// </summary>
		[TestMethod]
		public void CallTest()
		{
			object[] parameters = new object[] { "Program", 20 };
			Type expectedType = typeof(BusinessObjects.Mongoose.Program[]);
			ThreadPool.QueueUserWorkItem(state =>
			{
				//Call<IWebHttpService, BusinessObjects.Orca.SequencedObject[]>((svc) => svc.GetObjects(parameters));
				Call((IWebHttpService c) => c.GetObjects(parameters));//compiles
			});
		}

		static TResult Call<TChannel, TResult>(Expression<Func<TChannel,TResult>> callexpr)
		{
			object result = null;
			Uri baseAddress = new Uri("http://localhost:8732");
			//MethodCallExpression m = (MethodCallExpression)callexpr.Body;
			//object[] parameters = getArgumentValues(m);
			//ManualResetEvent reset = new ManualResetEvent(false);
			//ThreadPool.QueueUserWorkItem(state =>
			//{
				result = WebHttpRequestClient.SynchronousCall<TChannel, TResult>(
					baseAddress,
					callexpr, 
					typeof(TResult));
				//reset.Set();
			//});
			//reset.WaitOne();
			return (TResult)result;
		}

		/// <summary>
		/// Although Silverlight enforces (forces) asynchronous network requests, we can work around
		/// this limitation and still have the client fake synchronous calls if the client calls are 
		/// queued on a separate thread from the main UI thread.
		/// </summary>
		[TestMethod()]
		public void SynchronousCallTest1()
		{
			Uri baseAddress = new Uri("http://localhost:8732");
			IEnumerable<BusinessObjects.Mongoose.Program> programs;
			Type responseType = typeof(BusinessObjects.Mongoose.Program[]);//get this by reflection on interface
			int count = 3;
			object[] parameters = new object[] { "Program", count };

			ThreadPool.QueueUserWorkItem(state =>
			{
				//compiler cannot infer Type parameters:
				var result = WebHttpRequestClient.SynchronousCall<IWebHttpService, BusinessObjects.Orca.SequencedObject[]>(baseAddress,
					(IWebHttpService isvc) => isvc.GetObjects(parameters));
				programs = result.Cast<BusinessObjects.Mongoose.Program>();
			});
		}

		[TestMethod]
		public void SynchronousCallTest2()
		{
			Uri baseAddress = new Uri("http://localhost:8732", UriKind.Absolute);
			IEnumerable<BusinessObjects.Mongoose.Program> programs;
			Type expectedType = typeof(BusinessObjects.Mongoose.Program[]);
			int count = 3;
			object[] parameters = new object[] { "Program"  };
			ThreadPool.QueueUserWorkItem(state =>
			{
				object result = WebHttpRequestClient.SynchronousCall(baseAddress,
					(IWebHttpService isvc) => isvc.GetObjects(parameters));
				BusinessObjects.Orca.SequencedObject[] objects = (BusinessObjects.Orca.SequencedObject[])result;
				programs = objects.Cast<BusinessObjects.Mongoose.Program>();
			});
		}

	
		/// <summary>
		/// calls UpdateObjects to HTTP POST SequencedObject[]
		/// </summary>
		[TestMethod]
		public void SynchronousCallTest3()
		{
			Uri baseAddress = new Uri("http://localhost:8732", UriKind.Absolute);
			BusinessObjects.Mongoose.Program program = new BusinessObjects.Mongoose.Program { URI = "NAVSTAR:GPS", Name = "NAVSTAR GPS", LastUpdated = DateTime.MinValue };
			Type expectedType = typeof(BusinessObjects.Mongoose.Program[]);
			var parameters = new BusinessObjects.Orca.SequencedObject[] { program } ;
			ThreadPool.QueueUserWorkItem(state =>
			{
				object result = WebHttpRequestClient.SynchronousCall(baseAddress,
					(IWebHttpService isvc) => isvc.UpdateObjects(parameters),
					expectedType);
					//,parameters);
				Type returnedType = result.GetType();								
			});
		}

	
	
		
		internal static Uri GetOperationInfo(MethodInfo operation,
			Uri baseAddress,
			out string method,
			out WebInvokeAttribute webinvoke,
			out OperationContractAttribute operationcontract,
			out WebMessageFormat requestformat,
			out WebMessageFormat responseformat)
		{
			object[] customAttributes = operation.GetCustomAttributes(false);
			webinvoke = customAttributes.Single(a => a is WebInvokeAttribute) as WebInvokeAttribute;
			method = webinvoke.Method;
			operationcontract = customAttributes.Single(a => a is OperationContractAttribute) as OperationContractAttribute;
			requestformat = webinvoke.RequestFormat;
			responseformat = webinvoke.ResponseFormat;
			Uri relative = new Uri(webinvoke.UriTemplate, UriKind.Relative);
			Uri endpoint = new Uri(baseAddress, relative);
			return endpoint;
		}

		/// <summary>
		/// assumes a single params object[] argument.
		/// </summary>
		/// <param name="m"></param>
		/// <returns></returns>
		static object[] getArgumentValues(MethodCallExpression m)
		{
			Expression argexp = m.Arguments[0];
			var evald = (ConstantExpression)ObjectServices.Evaluator.PartialEval(argexp);
			LambdaExpression lambda = Expression.Lambda(evald);
			object[] args = (object[])lambda.Compile().DynamicInvoke(new object[0]);
			return args;
		}


		
		/// <summary>
		/// 1. Run service: wcfsvchost.exe /service:E:\CACI\UnitTests\BusinessObjects.WCF\bin\Debug\BusinessObjects.WCF.dll /config:E:\CACI\UnitTests\BusinessObjects.WCF\app.config	
		/// 
		/// Why bad request error (400) from WCF service?
		/// i. Didn't remember to have WCF service convert src Type to dest (serializable) Type
		/// ii. KnownTypes had to be limited to just those with BaseType of SequencedObject, DatabaseObject, DataPoint
		/// </summary>		 
		[TestMethod]
		public void CreateWebRequestToService()
		{
			Uri baseAddress = new Uri("http://localhost:8732");
			MethodInfo m = typeof(IWebHttpService).GetMethod("GetObjects");
			string method;
			WebInvokeAttribute webinvoke;
			OperationContractAttribute operationcontract;
			WebMessageFormat requestformat, responseformat;
			Uri endpoint = GetOperationInfo(m, baseAddress, out method, out webinvoke, out operationcontract, out requestformat, out responseformat);

			Type elementType = typeof(BusinessObjects.Mongoose.Program);
			Type responseType = elementType.MakeArrayType();//get this by reflection on interface
			int count = 3;
			object[] parameters = new object[] { elementType.Name, count };

			Action<Stream> callback = (s) =>
			{
				var result = WebHttpRequestClient.Deserialize(responseType, s);
				var programs = (IEnumerable<BusinessObjects.Mongoose.Program>)result;
			};
			WebHttpRequestClient.CreateHttpWebRequest(endpoint, instance: parameters, callback: callback);


		}

	}
}