using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.ServiceModel;

namespace Northwind
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class NorthwindService : INorthwindService
	{
		Customer[] _customers;
		Supplier[] _suppliers;
		Employee[] _employees;
		string connectionString;
		DbConnection dbconnection;
		public NorthwindService()
		{
			var connectionStrings = System.Configuration.ConfigurationManager.ConnectionStrings;
			this.connectionString = connectionStrings["northwind"].ConnectionString;
			this.dbconnection = new System.Data.SqlClient.SqlConnection(this.connectionString);
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

		public object[] ExecuteQueryForObjects(System.Xml.Linq.XElement xml)
		{
			using (this.dbconnection)
			{
				dbconnection.Open();

			}
			throw new NotImplementedException();
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
        
	}
}
