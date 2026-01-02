using System;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Xml.Linq;
using Bussiness;
using log4net;
using SqlDataProvider.Data;

namespace Tank.Request
{
	// Token: 0x0200001D RID: 29
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class ConsortiaEquipControl : IHttpHandler
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00005BA8 File Offset: 0x00003DA8
		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "text/plain";
			bool value = false;
			string message = "Fail!";
			XElement result = new XElement("Result");
			int total = 0;
			try
			{
				int consortiaID = int.Parse(context.Request["consortiaID"]);
				using (ConsortiaBussiness db = new ConsortiaBussiness())
				{
					for (int i = 1; i < 3; i++)
					{
						for (int j = 1; j < 11; j++)
						{
							ConsortiaEquipControlInfo cecInfo = db.GetConsortiaEquipRiches(consortiaID, j, i);
							bool flag = cecInfo != null;
							if (flag)
							{
								result.Add(new XElement("Item", new object[]
								{
									new XAttribute("type", cecInfo.Type),
									new XAttribute("level", cecInfo.Level),
									new XAttribute("riches", cecInfo.Riches)
								}));
								total++;
							}
						}
					}
					value = true;
					message = "Success!";
				}
			}
			catch (Exception ex)
			{
				ConsortiaEquipControl.log.Error("ConsortiaEventList", ex);
			}
			result.Add(new XAttribute("total", total));
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			context.Response.ContentType = "text/plain";
			context.Response.Write(result.ToString(false));
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400001A RID: 26
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
