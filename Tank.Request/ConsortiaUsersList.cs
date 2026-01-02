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
	// Token: 0x02000025 RID: 37
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class ConsortiaUsersList : IHttpHandler
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00006898 File Offset: 0x00004A98
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
				int userID = int.Parse(context.Request["userID"]);
				int state = int.Parse(context.Request["state"]);
				using (ConsortiaBussiness db = new ConsortiaBussiness())
				{
					ConsortiaUserInfo[] infos = db.GetConsortiaUsersPage(page, size, ref total, order, consortiaID, userID, state);
					foreach (ConsortiaUserInfo info in infos)
					{
						result.Add(FlashUtils.CreateConsortiaUserInfo(info));
					}
					value = true;
					message = "Success!";
				}
			}
			catch (Exception ex)
			{
				ConsortiaUsersList.log.Error("ConsortiaUsersList", ex);
			}
			result.Add(new XAttribute("total", total));
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			result.Add(new XAttribute("currentDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
			context.Response.ContentType = "text/plain";
			context.Response.Write(result.ToString(false));
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000022 RID: 34
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
