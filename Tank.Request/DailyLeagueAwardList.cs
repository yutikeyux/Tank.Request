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
	// Token: 0x0200002B RID: 43
	public class DailyLeagueAwardList : IHttpHandler
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00007954 File Offset: 0x00005B54
		public void ProcessRequest(HttpContext context)
		{
			bool flag = csFunction.ValidAdminIP(context.Request.UserHostAddress);
			if (flag)
			{
				context.Response.Write(DailyLeagueAwardList.Build(context));
			}
			else
			{
				context.Response.Write("IP is not valid!");
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000079A0 File Offset: 0x00005BA0
		public static string Build(HttpContext context)
		{
			bool value = false;
			string message = "Fail!";
			XElement result = new XElement("Result");
			try
			{
				using (ProduceBussiness db = new ProduceBussiness())
				{
					DailyLeagueAwardInfo[] infos = db.GetAllDailyLeagueAward();
					foreach (DailyLeagueAwardInfo info in infos)
					{
						result.Add(FlashUtils.CreateDailyLeagueAward(info));
					}
				}
				value = true;
				message = "Success!";
			}
			catch (Exception ex)
			{
				DailyLeagueAwardList.log.Error("dailyleagueaward", ex);
			}
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			csFunction.CreateCompressXml(context, result, "dailyleagueaward_out", false);
			return csFunction.CreateCompressXml(context, result, "dailyleagueaward", true);
		}

		// Token: 0x04000028 RID: 40
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
