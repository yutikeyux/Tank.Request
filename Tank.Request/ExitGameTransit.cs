using System;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Web.Services;
using log4net;

namespace Tank.Request
{
	// Token: 0x02000033 RID: 51
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class ExitGameTransit : IHttpHandler
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000EE RID: 238 RVA: 0x000088F0 File Offset: 0x00006AF0
		public string LoginURL
		{
			get
			{
				string login = "ExitURL_" + this.site;
				return ConfigurationSettings.AppSettings[login];
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00008920 File Offset: 0x00006B20
		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "text/plain";
			string username = "";
			string url = string.Empty;
			try
			{
				bool flag = !string.IsNullOrEmpty(context.Request["username"]);
				if (flag)
				{
					username = HttpUtility.UrlDecode(context.Request["username"]).Trim();
				}
				this.site = ((context.Request["site"] == null) ? "" : HttpUtility.UrlDecode(context.Request["site"]).ToLower());
				bool flag2 = !string.IsNullOrEmpty(this.site);
				if (flag2)
				{
					url = this.LoginURL;
					int place = username.IndexOf('_');
					bool flag3 = place != -1;
					if (flag3)
					{
						username = username.Substring(place + 1, username.Length - place - 1);
					}
				}
				bool flag4 = string.IsNullOrEmpty(url);
				if (flag4)
				{
					url = ConfigurationSettings.AppSettings["ExitURL"];
				}
				context.Response.Redirect(string.Format(url, username, this.site), false);
			}
			catch (Exception ex)
			{
				ExitGameTransit.log.Error("ExitGameTransit:", ex);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000039 RID: 57
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		// Token: 0x0400003A RID: 58
		private string site = "";
	}
}
