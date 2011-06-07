using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using System.Net;
using System.IO;
using System.ServiceModel;
using System.Reflection;
using System.Linq.Expressions;

namespace UnitTests.SL
{

	public static class ClientHelper
	{
		public static C CreateTcpClient<C, TChannel>(string endpointAddress) where C : ClientBase<TChannel>, new() where TChannel : class//"net.tcp://localhost:4505/Test"
		{
			C tcpclient;
			//ClientBase<T> tcpclient;
			TcpTransportBindingElement tcpTransport = new TcpTransportBindingElement();
			BinaryMessageEncodingBindingElement messageEncoding = new BinaryMessageEncodingBindingElement();
#if !SILVERLIGHT
			messageEncoding.ReaderQuotas.MaxStringContentLength = 2147483647;//not available in Silverlight
#endif
			tcpTransport.MaxBufferSize = 2147483647;
			tcpTransport.MaxReceivedMessageSize = 2147483647;
			CustomBinding custombinding = new CustomBinding(messageEncoding, tcpTransport);
			custombinding.CloseTimeout = TimeSpan.FromSeconds(20);
			custombinding.SendTimeout = TimeSpan.FromSeconds(20);
			custombinding.ReceiveTimeout = TimeSpan.FromSeconds(20);
			custombinding.OpenTimeout = TimeSpan.FromSeconds(20);

			ConstructorInfo ctor = typeof(C).GetConstructors()[4];//http://msdn.microsoft.com/en-us/library/ms574927.aspx
			var parameters = new ParameterExpression[]
			{
				Expression.Parameter(typeof(System.ServiceModel.Channels.Binding)),
				Expression.Parameter(typeof(System.ServiceModel.EndpointAddress)),
			};
			NewExpression newexpr = Expression.New(ctor, parameters);
			LambdaExpression lambda = Expression.Lambda(newexpr, parameters);
			var newFn = lambda.Compile();
			EndpointAddress endpointaddress = new EndpointAddress(endpointAddress);
			var constructed = newFn.DynamicInvoke(new object[] { custombinding, endpointaddress });
			tcpclient = (C)constructed;				
#if !SILVERLIGHT
			setMaxItemsInObjectGraph<TChannel>(tcpclient);
#endif
			return tcpclient;
		}

#if !SILVERLIGHT
			static void setMaxItemsInObjectGraph<TChannel>(ClientBase<TChannel> tcpclient) where TChannel : class
			{
				var operations = tcpclient.Endpoint.Contract.Operations;
				foreach (var operation in operations)
				{
					operation.Behaviors.Find<System.ServiceModel.Description.DataContractSerializerOperationBehavior>().MaxItemsInObjectGraph = 2147483647;
				}
			}

#endif



        
	}





}
