using System;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpressionSerialization;
using System.Linq.Expressions;
using System.Xml.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using Northwind;
using System.Linq;
using System.Reflection;
using System.Dynamic;
using System.Runtime.Serialization;
using System.IO;
using System.Threading;
using System.Net;


namespace UnitTests
{
	[TestClass]
	public class ServiceTests : BaseTests
	{
		Uri baseAddress = new Uri("http://localhost:8999/");

		[TestMethod]
		[Tag("WCF")]
		public void BasicCall()
		{
			Uri endpoint;
			WebClient wc;
			endpoint = new Uri(baseAddress, new Uri("/object", UriKind.Relative));
			wc = new WebClient();
			
			wc.DownloadStringCompleted += wc_DownloadStringCompleted;
			wc.UploadStringCompleted += new UploadStringCompletedEventHandler(wc_UploadStringCompleted);
			wc.UploadStringAsync(endpoint, "");
		}

		void wc_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
		{
			string result = e.Result;
		}

		void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
		{
			//string result = e.Result;
		}

		[TestMethod]
		[Tag("WCF")]
		public void BasicCall2()
		{
			Uri endpoint = new Uri(baseAddress, new Uri("/object", UriKind.Relative));
			Action<Stream> callback = (s) =>
				{
					IEnumerable<Type> knownTypes = new Type[] { typeof(Order) };
					DataContractSerializer serializer = new DataContractSerializer(typeof(SequencedObject), knownTypes);
					var order = serializer.ReadObject(s);
				};
			WebHttpRequestClient.CreateHttpWebRequest(endpoint, null, callback, "POST");
		}

		[TestMethod]		
		public void Test1()
		{
			
			ThreadPool.QueueUserWorkItem(state => 
				{
					XElement xml = new XElement("xname", "value") ;
					object[] args = new object[] { xml };
					//var results = WebHttpRequestClient.SynchronousCall<INorthwindService, object[]>
					//    (baseAddress, (svc) => svc.GetObjects());
					var results = WebHttpRequestClient.SynchronousCall<INorthwindService, object>(baseAddress, (svc) => svc.GetObject());
	
				});
			
		}
	}
}
