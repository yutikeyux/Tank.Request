using System;
using System.Reflection;
using System.Web;
using log4net;

namespace Tank.Request
{
	// Token: 0x02000028 RID: 40
	public class CreatShortCut : IHttpHandler
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00006DDC File Offset: 0x00004FDC
		public void ProcessRequest(HttpContext context)
		{
			string gameurl = context.Request["gameurl"];
			context.Response.Write("Not support right now");
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000024 RID: 36
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
