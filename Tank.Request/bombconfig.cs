using System;
using System.Web;
using System.Web.Services;
using System.Xml.Linq;
using Bussiness;
using Road.Flash;
using SqlDataProvider.Data;

namespace Tank.Request
{
	// Token: 0x02000010 RID: 16
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class bombconfig : IHttpHandler
	{
		// Token: 0x0600003E RID: 62 RVA: 0x000047CC File Offset: 0x000029CC
		public void ProcessRequest(HttpContext context)
		{
			bool flag = csFunction.ValidAdminIP(context.Request.UserHostAddress);
			if (flag)
			{
				context.Response.Write(bombconfig.Bulid(context));
			}
			else
			{
				context.Response.Write("IP is not valid!");
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00004818 File Offset: 0x00002A18
		public static string Bulid(HttpContext context)
		{
			bool value = false;
			string message = "Fail!";
			XElement result = new XElement("Result");
			try
			{
				using (ProduceBussiness db = new ProduceBussiness())
				{
					BallConfigInfo[] infos = db.GetAllBallConfig();
					foreach (BallConfigInfo info in infos)
					{
						result.Add(FlashUtils.CreateBallConfigInfo(info));
					}
				}
				value = true;
				message = "Success!";
			}
			catch (Exception ex)
			{
			}
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			return csFunction.CreateCompressXml(context, result, "bombconfig", true);
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}
