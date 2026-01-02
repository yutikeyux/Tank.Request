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
	// Token: 0x02000021 RID: 33
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class ConsortiaInviteUsersList : IHttpHandler
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00006270 File Offset: 0x00004470
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
				int userID = int.Parse(context.Request["userID"]);
				int inviteID = int.Parse(context.Request["inviteID"]);
				using (ConsortiaBussiness db = new ConsortiaBussiness())
				{
					ConsortiaInviteUserInfo[] infos = db.GetConsortiaInviteUserPage(page, size, ref total, order, userID, inviteID);
					foreach (ConsortiaInviteUserInfo info in infos)
					{
						result.Add(FlashUtils.CreateConsortiaInviteUserInfo(info));
					}
					value = true;
					message = "Success!";
				}
			}
			catch (Exception ex)
			{
				ConsortiaInviteUsersList.log.Error("ConsortiaInviteUsersList", ex);
			}
			result.Add(new XAttribute("total", total));
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			context.Response.ContentType = "text/plain";
			context.Response.Write(result.ToString(false));
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400001E RID: 30
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
