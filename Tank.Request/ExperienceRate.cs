using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Bussiness.CenterService;
using log4net;

namespace Tank.Request
{
	// Token: 0x02000034 RID: 52
	public class ExperienceRate : Page
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000035C4 File Offset: 0x000017C4
		public static string GetAdminIP
		{
			get
			{
				return ConfigurationSettings.AppSettings["AdminIP"];
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00008A6C File Offset: 0x00006C6C
		public static bool ValidLoginIP(string ip)
		{
			string ips = ExperienceRate.GetAdminIP;
			return string.IsNullOrEmpty(ips) || ips.Split(new char[]
			{
				'|'
			}).Contains(ip);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00008AB0 File Offset: 0x00006CB0
		protected void Page_Load(object sender, EventArgs e)
		{
			int result = 2;
			try
			{
				int serverId = int.Parse(this.Context.Request["serverId"]);
				bool flag = ExperienceRate.ValidLoginIP(this.Context.Request.UserHostAddress);
				if (flag)
				{
					using (CenterServiceClient temp = new CenterServiceClient())
					{
						result = temp.ExperienceRateUpdate(serverId);
					}
				}
			}
			catch (Exception ex)
			{
				ExperienceRate.log.Error("ExperienceRateUpdate:", ex);
			}
			base.Response.Write(result);
		}

		// Token: 0x0400003B RID: 59
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		// Token: 0x0400003C RID: 60
		protected HtmlForm form1;
	}
}
