using System;
using System.Reflection;
using System.Web;
using System.Web.Services;
using log4net;

namespace Tank.Request
{
	// Token: 0x02000030 RID: 48
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class elitematchplayerlist : IHttpHandler
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00002581 File Offset: 0x00000781
		public void ProcessRequest(HttpContext context)
		{
			context.Response.Write(elitematchplayerlist.Build(context));
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00008358 File Offset: 0x00006558
		public static string Build(HttpContext context)
		{
			bool flag = !csFunction.ValidAdminIP(context.Request.UserHostAddress);
			string result;
			if (flag)
			{
				result = "elitematchplayerlist Fail!";
			}
			else
			{
				result = elitematchplayerlist.Build();
			}
			return result;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00008390 File Offset: 0x00006590
		public static string Build()
		{
			return csFunction.BuildEliteMatchPlayerList("elitematchplayerlist");
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000036 RID: 54
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
