using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using Bussiness.CenterService;
using log4net;

namespace Tank.Request
{
	// Token: 0x02000003 RID: 3
	public class AASGetState : Page
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020A8 File Offset: 0x000002A8
		public static string GetAdminIP
		{
			get
			{
				return ConfigurationManager.AppSettings["AdminIP"];
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000034EC File Offset: 0x000016EC
		public static bool ValidLoginIP(string ip)
		{
			string getAdminIp = AASGetState.GetAdminIP;
			bool flag = !string.IsNullOrEmpty(getAdminIp) && !getAdminIp.Split(new char[]
			{
				'|'
			}).Contains(ip);
			return !flag;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00003534 File Offset: 0x00001734
		protected void Page_Load(object sender, EventArgs e)
		{
			int num = 2;
			try
			{
				bool flag = AASGetState.ValidLoginIP(this.Context.Request.UserHostAddress);
				if (flag)
				{
					using (CenterServiceClient centerServiceClient = new CenterServiceClient())
					{
						num = centerServiceClient.AASGetState();
					}
				}
			}
			catch (Exception ex)
			{
				AASGetState.log.Error("ASSGetState:", ex);
			}
			base.Response.Write(num);
		}

		// Token: 0x04000002 RID: 2
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
