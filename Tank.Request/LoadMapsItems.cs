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
	// Token: 0x02000049 RID: 73
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class LoadMapsItems : IHttpHandler
	{
		// Token: 0x0600014D RID: 333 RVA: 0x0000AA08 File Offset: 0x00008C08
		public void ProcessRequest(HttpContext context)
		{
			bool flag = csFunction.ValidAdminIP(context.Request.UserHostAddress);
			if (flag)
			{
				context.Response.Write(LoadMapsItems.Bulid(context));
			}
			else
			{
				context.Response.Write("Tabi Efendim!");
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000AA54 File Offset: 0x00008C54
		public static string Bulid(HttpContext context)
		{
			bool value = false;
			string message = "Fail";
			XElement result = new XElement("Result");
			try
			{
				using (MapBussiness db = new MapBussiness())
				{
					MapInfo[] infos = db.GetAllMap();
					foreach (MapInfo info in infos)
					{
						result.Add(FlashUtils.CreateMapInfo(info));
					}
				}
				value = true;
				message = "Success!";
			}
			catch (Exception ex)
			{
				LoadMapsItems.log.Error("LoadMapsItems", ex);
			}
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			return csFunction.CreateCompressXml(context, result, "LoadMapsItems", true);
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400004C RID: 76
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
