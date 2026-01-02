using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using Bussiness.Interface;
using log4net;

namespace Tank.Request
{
	// Token: 0x02000027 RID: 39
	public class CreateLogin : Page
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00006C58 File Offset: 0x00004E58
		public static string GetLoginIP
		{
			get
			{
				return ConfigurationSettings.AppSettings["LoginIP"];
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00006C7C File Offset: 0x00004E7C
		public static bool ValidLoginIP(string ip)
		{
			string ips = CreateLogin.GetLoginIP;
			return string.IsNullOrEmpty(ips) || ips.Split(new char[]
			{
				'|'
			}).Contains(ip);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00006CC0 File Offset: 0x00004EC0
		protected void Page_Load(object sender, EventArgs e)
		{
			int result = 1;
			try
			{
				string content = HttpUtility.UrlDecode(base.Request["content"]);
				string site = (base.Request["site"] == null) ? "" : HttpUtility.UrlDecode(base.Request["site"]).ToLower();
				BaseInterface inter = BaseInterface.CreateInterface();
				string[] str = inter.UnEncryptLogin(content, ref result, site);
				bool flag = str.Length > 3;
				if (flag)
				{
					string name = str[0].Trim().ToLower();
					string password = str[1].Trim().ToLower();
					bool flag2 = !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(password);
					if (flag2)
					{
						name = BaseInterface.GetNameBySite(name, site);
						PlayerManager.Add(name, password);
						result = 0;
					}
					else
					{
						result = -91010;
					}
				}
			}
			catch (Exception ex)
			{
				CreateLogin.log.Error("CreateLogin:", ex);
			}
			base.Response.Write(result);
		}

		// Token: 0x04000023 RID: 35
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
