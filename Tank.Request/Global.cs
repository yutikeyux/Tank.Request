using System;
using System.Web;
using Bussiness;
using log4net.Config;

namespace Tank.Request
{
	// Token: 0x0200003C RID: 60
	public class Global : HttpApplication
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00009738 File Offset: 0x00007938
		protected void Application_Start(object sender, EventArgs e)
		{
			string path = base.Server.MapPath("~");
			LanguageMgr.Setup(path);
			XmlConfigurator.Configure();
			StaticsMgr.Setup();
			PlayerManager.Setup();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00002670 File Offset: 0x00000870
		protected void Session_Start(object sender, EventArgs e)
		{
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00002670 File Offset: 0x00000870
		protected void Application_BeginRequest(object sender, EventArgs e)
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00002670 File Offset: 0x00000870
		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00002670 File Offset: 0x00000870
		protected void Application_Error(object sender, EventArgs e)
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00002670 File Offset: 0x00000870
		protected void Session_End(object sender, EventArgs e)
		{
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00002673 File Offset: 0x00000873
		protected void Application_End(object sender, EventArgs e)
		{
			StaticsMgr.Stop();
		}
	}
}
