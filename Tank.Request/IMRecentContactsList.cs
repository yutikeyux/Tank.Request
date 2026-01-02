using System;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Xml.Linq;
using Bussiness;
using log4net;

namespace Tank.Request
{
	// Token: 0x02000043 RID: 67
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class IMRecentContactsList : IHttpHandler
	{
		// Token: 0x06000133 RID: 307 RVA: 0x0000A440 File Offset: 0x00008640
		public void ProcessRequest(HttpContext context)
		{
			XElement result = new XElement("Result");
			bool value = true;
			string message = "Success!";
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			context.Response.ContentType = "text/plain";
			context.Response.Write(result.ToString(false));
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000048 RID: 72
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
