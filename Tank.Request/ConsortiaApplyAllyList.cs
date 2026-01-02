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
	// Token: 0x02000019 RID: 25
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class ConsortiaApplyAllyList : IHttpHandler
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00005520 File Offset: 0x00003720
		public void ProcessRequest(HttpContext context)
		{
			bool value = false;
			string message = "Fail!";
			XElement result = new XElement("Result");
			int total = 0;
			try
			{
				int page = int.Parse(context.Request["page"]);
				int size = int.Parse(context.Request["size"]);
				int order = int.Parse(context.Request["order"]);
				int consortiaID = int.Parse(context.Request["consortiaID"]);
				int applyID = int.Parse(context.Request["applyID"]);
				int state = int.Parse(context.Request["state"]);
				using (ConsortiaBussiness db = new ConsortiaBussiness())
				{
					ConsortiaApplyAllyInfo[] infos = db.GetConsortiaApplyAllyPage(page, size, ref total, order, consortiaID, applyID, state);
					foreach (ConsortiaApplyAllyInfo info in infos)
					{
						result.Add(FlashUtils.CreateConsortiaApplyAllyInfo(info));
					}
					value = true;
					message = "Success!";
				}
			}
			catch (Exception ex)
			{
				ConsortiaApplyAllyList.log.Error("ConsortiaApplyAllyList", ex);
			}
			result.Add(new XAttribute("total", total));
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			context.Response.ContentType = "text/plain";
			context.Response.Write(result.ToString(false));
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000016 RID: 22
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
