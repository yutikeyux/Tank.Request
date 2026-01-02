using System;
using System.Collections;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Xml.Linq;
using Bussiness;
using log4net;

namespace Tank.Request
{
	// Token: 0x02000041 RID: 65
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class IMFriendsGood : IHttpHandler
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00009F1C File Offset: 0x0000811C
		public void ProcessRequest(HttpContext context)
		{
			bool value = false;
			string message = "Fail!";
			XElement result = new XElement("Result");
			try
			{
				string UserName = context.Request["UserName"];
				using (PlayerBussiness db = new PlayerBussiness())
				{
					ArrayList friends = db.GetFriendsGood(UserName);
					for (int i = 0; i < friends.Count; i++)
					{
						XElement node = new XElement("Item", new XAttribute("UserName", friends[i].ToString()));
						result.Add(node);
					}
				}
				value = true;
				message = "Success!";
			}
			catch (Exception ex)
			{
				IMFriendsGood.log.Error("IMFriendsGood", ex);
			}
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			context.Response.ContentType = "text/plain";
			context.Response.Write(result.ToString(false));
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000046 RID: 70
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
