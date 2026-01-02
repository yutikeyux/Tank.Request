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
	// Token: 0x02000059 RID: 89
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class MapServerList : IHttpHandler
	{
		// Token: 0x06000191 RID: 401 RVA: 0x0000288E File Offset: 0x00000A8E
		public void ProcessRequest(HttpContext context)
		{
			context.Response.Write(MapServerList.Bulid(context));
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000CEB0 File Offset: 0x0000B0B0
		public static string Bulid(HttpContext context)
		{
			bool value = false;
			string message = "Fail";
			XElement result = new XElement("Result");
			try
			{
				using (MapBussiness db = new MapBussiness())
				{
					ServerMapInfo[] infos = db.GetAllServerMap();
					foreach (ServerMapInfo info in infos)
					{
						result.Add(FlashUtils.CreateMapServer(info));
					}
				}
				value = true;
				message = "Success!";
			}
			catch (Exception ex)
			{
				MapServerList.log.Error("MapServerList", ex);
			}
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			return csFunction.CreateCompressXml(context, result, "MapServerList", true);
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400005A RID: 90
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
