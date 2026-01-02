using System;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Xml.Linq;
using Bussiness;
using log4net;
using SqlDataProvider.Data;

namespace Tank.Request
{
	// Token: 0x0200003B RID: 59
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class giftsendlog : IHttpHandler
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000113 RID: 275 RVA: 0x0000215A File Offset: 0x0000035A
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00009548 File Offset: 0x00007748
		public void ProcessRequest(HttpContext context)
		{
			bool flag = false;
			string str = "Fail!";
			XElement xelement = new XElement("Result");
			try
			{
				string text = context.Request["key"];
				int.Parse(context.Request["selfid"]);
				int num = int.Parse(context.Request["userID"]);
				using (PlayerBussiness playerBussiness = new PlayerBussiness())
				{
					UserGiftInfo[] allUserGifts = playerBussiness.GetAllUserGifts(num, false);
					bool flag2 = allUserGifts != null;
					if (flag2)
					{
						UserGiftInfo[] array = allUserGifts;
						foreach (UserGiftInfo userGiftInfo in array)
						{
							XElement xelement2 = new XElement("Item", new object[]
							{
								new XAttribute("playerID", userGiftInfo.ReceiverID),
								new XAttribute("TemplateID", userGiftInfo.TemplateID),
								new XAttribute("count", userGiftInfo.Count)
							});
							xelement.Add(xelement2);
						}
					}
				}
				flag = true;
				str = "Success!";
			}
			catch (Exception ex)
			{
				giftsendlog.log.Error("giftsendlog", ex);
			}
			xelement.Add(new XAttribute("value", flag));
			xelement.Add(new XAttribute("message", str));
			context.Response.ContentType = "text/plain";
			context.Response.BinaryWrite(StaticFunction.Compress(xelement.ToString(false)));
		}

		// Token: 0x04000042 RID: 66
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
