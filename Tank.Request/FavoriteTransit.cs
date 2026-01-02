using System;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Web.Services;
using log4net;

namespace Tank.Request
{
	// Token: 0x02000037 RID: 55
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class FavoriteTransit : IHttpHandler
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00008F18 File Offset: 0x00007118
		public static string GetFavoriteUrl
		{
			get
			{
				return ConfigurationSettings.AppSettings["FavoriteUrl"];
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00008F3C File Offset: 0x0000713C
		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "text/plain";
			try
			{
				string username = (context.Request["username"] == null) ? "" : HttpUtility.UrlDecode(context.Request["username"]);
				string site = (context.Request["site"] == null) ? "" : HttpUtility.UrlDecode(context.Request["site"]).ToLower();
				string url = string.Empty;
				bool flag = !string.IsNullOrEmpty(site);
				if (flag)
				{
					url = ConfigurationSettings.AppSettings[string.Format("FavoriteUrl_{0}", site)];
					int place = username.IndexOf('_');
					bool flag2 = place != -1;
					if (flag2)
					{
						username = username.Substring(place + 1, username.Length - place - 1);
					}
				}
				bool flag3 = string.IsNullOrEmpty(url);
				if (flag3)
				{
					url = FavoriteTransit.GetFavoriteUrl;
				}
				context.Response.Redirect(string.Format(url, username, site), false);
			}
			catch (Exception ex)
			{
				FavoriteTransit.log.Error("FavoriteTransit:", ex);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400003F RID: 63
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
