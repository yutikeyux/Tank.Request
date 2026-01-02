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
	// Token: 0x0200001A RID: 26
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class ConsortiaApplyUsersList : IHttpHandler
	{
		// Token: 0x06000068 RID: 104 RVA: 0x000056E4 File Offset: 0x000038E4
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
				int userID = int.Parse(context.Request["userID"]);
				using (ConsortiaBussiness db = new ConsortiaBussiness())
				{
					ConsortiaApplyUserInfo[] infos = db.GetConsortiaApplyUserPage(page, size, ref total, order, consortiaID, applyID, userID);
					foreach (ConsortiaApplyUserInfo info in infos)
					{
						result.Add(FlashUtils.CreateConsortiaApplyUserInfo(info));
					}
					value = true;
					message = "Success!";
				}
			}
			catch (Exception ex)
			{
				ConsortiaApplyUsersList.log.Error("ConsortiaApplyUsersList", ex);
			}
			result.Add(new XAttribute("total", total));
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			context.Response.ContentType = "text/plain";
			context.Response.Write(result.ToString(false));
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000017 RID: 23
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
