using System;
using System.Reflection;
using System.Web;
using System.Xml.Linq;
using Bussiness;
using log4net;
using Road.Flash;
using SqlDataProvider.Data;

namespace Tank.Request
{
	// Token: 0x02000053 RID: 83
	public class LoginAwardItemTemplate : IHttpHandler
	{
		// Token: 0x0600017B RID: 379 RVA: 0x0000C678 File Offset: 0x0000A878
		public void ProcessRequest(HttpContext context)
		{
			bool flag = csFunction.ValidAdminIP(context.Request.UserHostAddress);
			if (flag)
			{
				context.Response.Write(LoginAwardItemTemplate.Bulid(context));
			}
			else
			{
				context.Response.Write("IP is not valid!");
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000C6C4 File Offset: 0x0000A8C4
		public static string Bulid(HttpContext context)
		{
			bool value = false;
			string message = "Fail!";
			XElement result = new XElement("Result");
			try
			{
				using (ProduceBussiness db = new ProduceBussiness())
				{
					AccumulAtiveLoginAwardInfo[] infos = db.GetAccumulAtiveLoginAwardInfos();
					foreach (AccumulAtiveLoginAwardInfo info in infos)
					{
						result.Add(FlashUtils.CreateAccumulAtiveLoginAwards(info));
					}
					value = true;
					message = "Success!";
				}
			}
			catch (Exception ex)
			{
				LoginAwardItemTemplate.log.Error("Load loginawarditemtemplate is fail!", ex);
			}
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			return csFunction.CreateCompressXml(context, result, "loginawarditemtemplate", true);
		}

		// Token: 0x04000055 RID: 85
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
