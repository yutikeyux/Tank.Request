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
	// Token: 0x02000022 RID: 34
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class ConsortiaLevelList : IHttpHandler
	{
		// Token: 0x06000089 RID: 137 RVA: 0x0000239A File Offset: 0x0000059A
		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "text/plain";
			context.Response.Write(ConsortiaLevelList.Bulid(context));
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000641C File Offset: 0x0000461C
		public static string Bulid(HttpContext context)
		{
			bool value = false;
			string message = "Fail!";
			XElement result = new XElement("Result");
			try
			{
				using (ConsortiaBussiness db = new ConsortiaBussiness())
				{
					ConsortiaLevelInfo[] infos = db.GetAllConsortiaLevel();
					foreach (ConsortiaLevelInfo info in infos)
					{
						result.Add(FlashUtils.CreateConsortiLevelInfo(info));
					}
				}
				value = true;
				message = "Success!";
			}
			catch (Exception ex)
			{
				ConsortiaLevelList.log.Error("ConsortiaLevelList", ex);
			}
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			return csFunction.CreateCompressXml(context, result, "ConsortiaLevelList", true);
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400001F RID: 31
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
