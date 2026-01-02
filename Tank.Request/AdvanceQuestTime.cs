using System;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Xml.Linq;
using Bussiness;
using log4net;

namespace Tank.Request
{
	// Token: 0x0200000C RID: 12
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class AdvanceQuestTime : IHttpHandler
	{
		// Token: 0x0600002D RID: 45 RVA: 0x0000403C File Offset: 0x0000223C
		public void ProcessRequest(HttpContext context)
		{
			bool flag = false;
			string value = "Fail!";
			XElement xelement = new XElement("Result");
			try
			{
				int.Parse(context.Request["userid"]);
				using (new PlayerBussiness())
				{
				}
				flag = true;
				value = "Success!";
			}
			catch (Exception exception)
			{
				AdvanceQuestTime.log.Error("IMListLoad", exception);
			}
			xelement.Add(new XAttribute("value", flag));
			xelement.Add(new XAttribute("message", value));
			context.Response.ContentType = "text/plain";
			context.Response.Write(string.Format("0,{0},0", DateTime.Now));
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000215A File Offset: 0x0000035A
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400000B RID: 11
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
