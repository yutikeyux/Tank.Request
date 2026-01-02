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
	// Token: 0x02000020 RID: 32
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class ConsortiaIMList : IHttpHandler
	{
		// Token: 0x06000081 RID: 129 RVA: 0x000060CC File Offset: 0x000042CC
		public void ProcessRequest(HttpContext context)
		{
			bool value = false;
			string message = "Fail!";
			int total = 0;
			XElement result = new XElement("Result");
			try
			{
				int id = int.Parse(context.Request["id"]);
				using (ConsortiaBussiness db = new ConsortiaBussiness())
				{
					ConsortiaInfo info = db.GetConsortiaSingle(id);
					bool flag = info != null;
					if (flag)
					{
						result.Add(new XAttribute("Level", info.Level));
						result.Add(new XAttribute("Repute", info.Repute));
					}
				}
				using (ConsortiaBussiness db2 = new ConsortiaBussiness())
				{
					ConsortiaUserInfo[] infos = db2.GetConsortiaUsersPage(1, 1000, ref total, -1, id, -1, -1);
					foreach (ConsortiaUserInfo info2 in infos)
					{
						result.Add(FlashUtils.CreateConsortiaIMInfo(info2));
					}
					value = true;
					message = "Success!";
				}
			}
			catch (Exception ex)
			{
				ConsortiaIMList.log.Error("ConsortiaIMList", ex);
			}
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			context.Response.Write(result.ToString(false));
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400001D RID: 29
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
