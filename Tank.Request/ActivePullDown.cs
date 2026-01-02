using System;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml.Linq;
using Bussiness;
using Bussiness.CenterService;
using log4net;
using Road.Flash;

namespace Tank.Request
{
	// Token: 0x02000009 RID: 9
	public class ActivePullDown : IHttpHandler
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000215A File Offset: 0x0000035A
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003C1C File Offset: 0x00001E1C
		public void ProcessRequest(HttpContext context)
		{
			string path = HttpContext.Current.Server.MapPath(".");
			path += "\\";
			LanguageMgr.Setup(path);
			int selfid = Convert.ToInt32(context.Request["selfid"]);
			int activeID = Convert.ToInt32(context.Request["activeID"]);
			string key = context.Request["key"];
			string activeKey = context.Request["activeKey"];
			bool value = false;
			string message = "ActivePullDownHandler.Fail";
			string awardID = "";
			XElement result = new XElement("Result");
			bool flag = activeKey != "";
			if (flag)
			{
				byte[] src = CryptoHelper.RsaDecryt2(StaticFunction.RsaCryptor, activeKey);
				awardID = Encoding.UTF8.GetString(src, 0, src.Length);
			}
			try
			{
				using (PlayerBussiness pb = new PlayerBussiness())
				{
					bool flag2 = pb.PullDown(activeID, awardID, selfid, ref message) == 0;
					if (flag2)
					{
						using (CenterServiceClient client = new CenterServiceClient())
						{
							client.MailNotice(selfid);
						}
					}
				}
				value = true;
				message = LanguageMgr.GetTranslation(message, Array.Empty<object>());
			}
			catch (Exception ex)
			{
				ActivePullDown.log.Error("ActivePullDown", ex);
			}
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			context.Response.ContentType = "text/plain";
			context.Response.Write(result.ToString(false));
		}

		// Token: 0x04000008 RID: 8
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
