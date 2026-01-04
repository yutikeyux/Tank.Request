using System;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Xml.Linq;
using Bussiness;
using log4net;
using Road.Flash;
using SqlDataProvider.Data;

namespace Tank.Request
{
	// Token: 0x0200004C RID: 76
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class LoadPVEItems : IHttpHandler
	{
		// Token: 0x0600015C RID: 348 RVA: 0x0000AE14 File Offset: 0x00009014
		public void ProcessRequest(HttpContext context)
		{
			bool flag = csFunction.ValidAdminIP(context.Request.UserHostAddress);
			if (flag)
			{
				context.Response.Write(LoadPVEItems.Build(context));
			}
			else
			{
				context.Response.Write("Tabi Efendim!");
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000AE60 File Offset: 0x00009060
		public static string Build(HttpContext context)
		{
			bool value = false;
			string message = "Fail";
			XElement result = new XElement("Result");
			try
			{
				using (PveBussiness db = new PveBussiness())
				{
					PveInfo[] infos = db.GetAllPveInfos();
					foreach (PveInfo info in infos)
					{
						result.Add(FlashUtils.CreatePveInfo(info));
					}
				}
				value = true;
				message = "Success!";
			}
			catch (Exception ex)
			{
				LoadPVEItems.log.Error("LoadPVEItems", ex);
			}
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			return csFunction.CreateCompressXml(context, result, "LoadPVEItems", true);
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400004F RID: 79
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
