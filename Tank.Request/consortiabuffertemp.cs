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
	// Token: 0x0200001B RID: 27
	public class consortiabuffertemp : IHttpHandler
	{
		// Token: 0x0600006C RID: 108 RVA: 0x000058A8 File Offset: 0x00003AA8
		public void ProcessRequest(HttpContext context)
		{
			bool flag = csFunction.ValidAdminIP(context.Request.UserHostAddress);
			if (flag)
			{
				context.Response.Write(consortiabuffertemp.Bulid(context));
			}
			else
			{
				context.Response.Write("IP is not valid!");
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000058F4 File Offset: 0x00003AF4
		public static string Bulid(HttpContext context)
		{
			bool value = false;
			string message = "Fail!";
			XElement result = new XElement("Result");
			try
			{
				using (ProduceBussiness db = new ProduceBussiness())
				{
					ConsortiaBuffTempInfo[] infos = db.GetAllConsortiaBuffTemp();
					foreach (ConsortiaBuffTempInfo info in infos)
					{
						result.Add(FlashUtils.CreateConsortiaBuffer(info));
					}
				}
				value = true;
				message = "Success!";
			}
			catch (Exception ex)
			{
				consortiabuffertemp.log.Error("consortiabuffertemp", ex);
			}
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			csFunction.CreateCompressXml(context, result, "consortiabuffertemp_out", false);
			return csFunction.CreateCompressXml(context, result, "consortiabuffertemp", true);
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000018 RID: 24
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
