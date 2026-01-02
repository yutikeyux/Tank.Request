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
	// Token: 0x0200004F RID: 79
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class LoadUserItems : IHttpHandler
	{
		// Token: 0x06000169 RID: 361 RVA: 0x0000B4AC File Offset: 0x000096AC
		public void ProcessRequest(HttpContext context)
		{
			bool value = false;
			string message = "Fail!";
			XElement result = new XElement("Result");
			try
			{
				int userid = int.Parse(context.Request.Params["ID"]);
				using (PlayerBussiness db = new PlayerBussiness())
				{
					ItemInfo[] items = db.GetUserItem(userid);
					foreach (ItemInfo item in items)
					{
						result.Add(FlashUtils.CreateGoodsInfo(item));
					}
				}
				value = true;
				message = "Success!";
			}
			catch (Exception ex)
			{
				LoadUserItems.log.Error("LoadUserItems", ex);
			}
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			context.Response.ContentType = "text/plain";
			context.Response.Write(result.ToString(false));
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000051 RID: 81
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
