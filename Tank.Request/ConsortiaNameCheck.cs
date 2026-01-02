using System;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml.Linq;
using Bussiness;
using log4net;

namespace Tank.Request
{
	// Token: 0x02000024 RID: 36
	public class ConsortiaNameCheck : IHttpHandler
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00006714 File Offset: 0x00004914
		public void ProcessRequest(HttpContext context)
		{
			string path = HttpContext.Current.Server.MapPath(".");
			path += "\\";
			LanguageMgr.Setup(path);
			bool value = false;
			string message = LanguageMgr.GetTranslation("Tank.Request.ConsortiaCheck.Exist", Array.Empty<object>());
			XElement result = new XElement("Result");
			try
			{
				string ConsortiaName = csFunction.ConvertSql(HttpUtility.UrlDecode(context.Request["NickName"]));
				bool flag = Encoding.Default.GetByteCount(ConsortiaName) <= 14;
				if (flag)
				{
					bool flag2 = !string.IsNullOrEmpty(ConsortiaName);
					if (flag2)
					{
						using (ConsortiaBussiness db = new ConsortiaBussiness())
						{
							bool flag3 = db.GetConsortiaSingleByName(ConsortiaName) == null;
							if (flag3)
							{
								value = true;
								message = LanguageMgr.GetTranslation("Tank.Request.ConsortiaCheck.Right", Array.Empty<object>());
							}
						}
					}
				}
				else
				{
					message = LanguageMgr.GetTranslation("Tank.Request.ConsortiaCheck.Long", Array.Empty<object>());
				}
			}
			catch (Exception ex)
			{
				ConsortiaNameCheck.log.Error("ConsortiaCheck", ex);
				value = false;
			}
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			context.Response.ContentType = "text/plain";
			context.Response.Write(result.ToString(false));
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000021 RID: 33
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
