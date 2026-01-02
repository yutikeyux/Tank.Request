using System;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Xml.Linq;
using Bussiness;
using log4net;

namespace Tank.Request
{
	// Token: 0x0200005A RID: 90
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class MapWeekList : IHttpHandler
	{
		// Token: 0x06000196 RID: 406 RVA: 0x0000CFA8 File Offset: 0x0000B1A8
		public void ProcessRequest(HttpContext context)
		{
			bool flag = false;
			string str = "获取失败!";
			XElement node = new XElement("Result");
			try
			{
				using (new MapBussiness())
				{
					flag = true;
					str = "获取成功!";
				}
			}
			catch (Exception exception)
			{
				MapWeekList.log.Error("加载地图周期失败", exception);
			}
			node.Add(new XAttribute("value", flag));
			node.Add(new XAttribute("message", str));
			context.Response.ContentType = "text/plain";
			context.Response.Write(node.ToString(false));
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400005B RID: 91
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
