using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Northwind
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class NorthwindService : INorthwindService
	{
	
		string connectionString;
		public NorthwindService()
		{
			var connectionStrings = System.Configuration.ConfigurationManager.ConnectionStrings;
			this.connectionString = connectionStrings["northwind"].ConnectionString;
			
		}


		#region INorthwindService Members

		public SequencedObject[] UpdateObjects(SequencedObject[] updatedobjects)
		{
			throw new NotImplementedException();
		}

		public SequencedObject[] GetObjects(params object[] args)
		{
			throw new NotImplementedException();
		}

		public SequencedObject[] ExecuteQueryForObjects()//System.Xml.Linq.XElement xml)
		{
			using (DbConnection dbconnection = new System.Data.SqlClient.SqlConnection(this.connectionString))
			{
				dbconnection.Open();
				var processor = new DbQueryProcessor(dbconnection);
				//var orders = processor.Execute<Order>();
				//return orders.First();				
				Customer[] customers = new Customer[] { new Customer { Name = "name1" }, new Customer { Name = "name2" } };
				return customers;// new object[] { customers };
			}			
		}

		public object GetObject()
		{
			Order[] orders = new Order[] 
				{	
					new Order { ShipperId = 123, OrderDate = DateTime.Now, ShipCity = "Huntington Beach", Employee = new Employee { ID = 23, FirstName = "Employee 23" } },
					new Order { ShipperId = 234, Freight = 394.3m, ShipCity = "Chantilly", ShipPostalCode = "20151"}
				};
			return orders[0];
		}
		#endregion


		#region IClientAccessPolicy Members
		[System.ServiceModel.OperationBehavior]
		public System.IO.Stream GetClientAccessPolicy()
		{
			const string result = @"<?xml version=""1.0"" encoding=""utf-8""?>
<access-policy>
    <cross-domain-access>
        <policy>
            <allow-from http-request-headers=""*"">
                <domain uri=""*""/>
            </allow-from>
            <grant-to>
                <resource path=""/"" include-subpaths=""true""/>				
            </grant-to>
        </policy>
    </cross-domain-access>
</access-policy>";
			//<socket-resource port=""4502-4534"" protocol=""tcp"" />
			if (System.ServiceModel.Web.WebOperationContext.Current != null)
				System.ServiceModel.Web.WebOperationContext.Current.OutgoingResponse.ContentType = "application/xml";
			return new System.IO.MemoryStream(Encoding.UTF8.GetBytes(result));
		}
		#endregion
        
		static void Main()
		{
			//Uri baseAddress = new Uri("http://localhost:8999/");
			using (var host = new WebServiceHost(typeof(NorthwindService)))
			{
				host.Open();
				string baseAddress = host.BaseAddresses.FirstOrDefault().AbsoluteUri ?? "not in .config file";
				Console.WriteLine("Service {0} started.\n\tAddress:\n\t{1}", typeof(NorthwindService).FullName, baseAddress);
				Console.ReadLine();
			}
		}
	}
}
