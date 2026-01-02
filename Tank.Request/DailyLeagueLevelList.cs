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
	// Token: 0x0200002C RID: 44
	public class DailyLeagueLevelList : IHttpHandler
	{
		// Token: 0x060000BF RID: 191 RVA: 0x00007AA8 File Offset: 0x00005CA8
		public void ProcessRequest(HttpContext context)
		{
			bool flag = csFunction.ValidAdminIP(context.Request.UserHostAddress);
			if (flag)
			{
				context.Response.Write(DailyLeagueLevelList.Build(context));
			}
			else
			{
				context.Response.Write("IP is not valid!");
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00007AF4 File Offset: 0x00005CF4
		public static string Build(HttpContext context)
		{
			bool value = false;
			string message = "Fail!";
			XElement result = new XElement("Result");
			try
			{
				using (ProduceBussiness db = new ProduceBussiness())
				{
					FairBattleRewardInfo[] infos = db.GetAllFairBattleReward();
					foreach (FairBattleRewardInfo info in infos)
					{
						result.Add(FlashUtils.CreateFairBattleReward(info));
					}
				}
				value = true;
				message = "Success!";
			}
			catch (Exception ex)
			{
				DailyLeagueLevelList.log.Error("dailyleaguelevel", ex);
			}
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			csFunction.CreateCompressXml(context, result, "dailyleaguelevel_out", false);
			return csFunction.CreateCompressXml(context, result, "dailyleaguelevel", true);
		}

		// Token: 0x04000029 RID: 41
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
