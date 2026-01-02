using System;
using System.Reflection;
using System.Web;
using System.Xml.Linq;
using Bussiness;
using log4net;
using Road.Flash;
using SqlDataProvider.Data;

namespace Tank.Request
{
	// Token: 0x0200000A RID: 10
	public class activitysystemitems : IHttpHandler
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00003DFC File Offset: 0x00001FFC
		public void ProcessRequest(HttpContext context)
		{
			bool flag = csFunction.ValidAdminIP(context.Request.UserHostAddress);
			if (flag)
			{
				context.Response.Write(activitysystemitems.Build(context));
			}
			else
			{
				context.Response.Write("IP is not valid!");
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003E48 File Offset: 0x00002048
		public static string Build(HttpContext context)
		{
			bool value = false;
			string message = "Fail!";
			XElement result = new XElement("Result");
			try
			{
				using (ProduceBussiness db = new ProduceBussiness())
				{
					ActivitySystemItemInfo[] infos = db.GetAllActivitySystemItem();
					foreach (ActivitySystemItemInfo info in infos)
					{
						result.Add(FlashUtils.CreateActivitySystemItems(info));
					}
					value = true;
					message = "Success!";
				}
			}
			catch (Exception ex)
			{
				activitysystemitems.log.Error("activitysystemitems", ex);
			}
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			csFunction.CreateCompressXml(context, result, "activitysystemitems_out", false);
			return csFunction.CreateCompressXml(context, result, "activitysystemitems", true);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000009 RID: 9
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
