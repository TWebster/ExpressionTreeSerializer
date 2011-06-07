using System;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.ServiceModel.Web;
using System.Threading;

namespace BusinessObjects
{

	/// <summary>
	/// this can be a base class for the evental client that implements IWebHttpService
	/// </summary>		
	public class WebHttpRequestClient
	{
		public static void CreateHttpWebRequest(string url,			
			object instance,
			Action<Stream> callback,
			string method = "POST",
			WebMessageFormat requestFormat = WebMessageFormat.Xml)
		{
			Uri uri;
			Stream postStream;
			string content;//XML or JSON
			HttpWebRequest request;
			HttpWebResponse response;
			dynamic serializer;//DataContractJsonSerializer or DataContractSerializer
			uri = new Uri(url, UriKind.Absolute);
			request = WebRequest.CreateHttp(uri);
			request.Method = method;

			AsyncCallback responseCallback = (ar2) =>
			{
				HttpWebRequest request2 = (HttpWebRequest)ar2.AsyncState;
				response = (HttpWebResponse)request2.EndGetResponse(ar2);
				Stream stream = response.GetResponseStream();
				callback(stream);				
				//stream.Position = 0;//NotSupportedException: Specified method is not supported.
			};

			if (method == "POST")
			{
				Type elementType = instance.GetType();
				IEnumerable<Type> knownTypes = new Type[] { elementType };
				switch (requestFormat)
				{
					case WebMessageFormat.Json:
						request.ContentType = "application/json";
						serializer = new DataContractJsonSerializer(elementType, knownTypes);
						break;
					case WebMessageFormat.Xml:
						request.ContentType = "application/xml";
						serializer = new DataContractSerializer(elementType, knownTypes);
						break;
					default:
						request.ContentType = "application/xml";
						serializer = new DataContractSerializer(elementType, knownTypes); break;

				}
				using (MemoryStream mem = new MemoryStream())
				{
					serializer.WriteObject(mem, instance);
					mem.Position = 0;
					using (StreamReader reader = new StreamReader(mem))
					{
						content = reader.ReadToEnd();
					}
				}
				byte[] byteData = UTF8Encoding.UTF8.GetBytes(content);
				request.ContentLength = byteData.Length;
				AsyncCallback requestCallback = (ar1) =>
				{
					postStream = request.EndGetRequestStream(ar1);
					postStream.Write(byteData, 0, byteData.Length);
					postStream.Close();
					request.BeginGetResponse(responseCallback, request);//GetResponse
				};
				request.BeginGetRequestStream(requestCallback, request);
			}

		}


		public static Stream SynchronousCall(string url			
			, Action<Stream> callback
			, params object[] Parameters
			)
		{
			Stream stream = null;
			ManualResetEvent reset = new ManualResetEvent(false);
			Action<Stream> completedHandler = (s) =>
				{
					stream = s;
					reset.Set();
					callback(stream);
				};
			CreateHttpWebRequest(url, instance: Parameters, callback: completedHandler);
			reset.WaitOne();
			return stream;
		}


		public static object Deserialize(Type type, Stream stream, bool isjson = true)
		{
			dynamic serializer;
			IEnumerable<Type> knownTypes = new Type[] { type };
			if (isjson)
			{
				serializer = new DataContractJsonSerializer(type, knownTypes);
				var jsonserialzire = new DataContractJsonSerializer(type, knownTypes);
				
			}
			else
			{
				serializer = new DataContractSerializer(type, knownTypes);
			}
			return serializer.ReadObject(stream);
		}
		public static T Deserialize<T>(Stream stream, bool isjson = true)
		{
			return (T)Deserialize(typeof(T), stream, isjson);
		}

		public static object Deserialize(Type type, string json, bool isjson = false)
		{
			using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
			{
				return Deserialize(type, stream, isjson);
			}
		}

		/// <summary>
		/// We're not HTTTP POST-ing JSON, so this probably won't be useful to any users/callers.
		/// </summary>		
		static string SerializeToJsonString(object objectToSerialize)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				DataContractJsonSerializer serializer =
						new DataContractJsonSerializer(objectToSerialize.GetType());
				serializer.WriteObject(ms, objectToSerialize);
				ms.Position = 0;

				using (StreamReader reader = new StreamReader(ms))
				{
					return reader.ReadToEnd();
				}
			}
		}
		//public static HttpWebResponse CreateHttpXMLWebRequest(string url, string method, object instance = null)
		//{			
		//    Uri uri;
		//    Stream postStream;
		//    string xml;//UTF8
		//    HttpWebRequest request;
		//    HttpWebResponse response;


		//    uri = new Uri(url, UriKind.Absolute);
		//    request = WebRequest.Create(uri) as HttpWebRequest;
		//    request.Method = method;

		//    if (method == "POST")
		//    {
		//        DataContractSerializer serializer;
		//        Type elementType = instance.GetType();
		//        IEnumerable<Type> knownTypes = new Type[] { elementType };//, typeof(System.Object), typeof(BusinessObjects.Tamarin.DataPoint), typeof(BusinessObjects.Orca.SequencedObject), typeof(BusinessObjects.Orca.DatabaseObject) };
		//        serializer = new DataContractSerializer(elementType, knownTypes);
		//        using (MemoryStream mem = new MemoryStream())
		//        {
		//            serializer.WriteObject(mem, instance);
		//            xml = Encoding.UTF8.GetString(mem.ToArray());
		//        }
		//        request.ContentType = "application/xml";
		//        var deserialized = Deserialize<BusinessObjects.Tamarin.OSCostDataPoint[]>(xml,false);
		//        byte[] byteData = UTF8Encoding.UTF8.GetBytes(xml);
		//        request.ContentLength = byteData.Length;
		//        postStream = request.GetRequestStream();
		//        postStream.Write(byteData, 0, byteData.Length);
		//        //postStream.Close();
		//    }

		//    response = request.GetResponse() as HttpWebResponse;
		//    return response;	
		//}

	}

}
