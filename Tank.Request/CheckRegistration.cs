using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using System.Xml.Linq;
using log4net;
using zlib;

namespace Tank.Request
{
	// Token: 0x02000017 RID: 23
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class CheckRegistration : IHttpHandler, IRequiresSessionState
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00005244 File Offset: 0x00003444
		public void ProcessRequest(HttpContext context)
		{
			bool value = true;
			string message = "Registered!";
			XElement result = new XElement("Result");
			int status = 1;
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			result.Add(new XAttribute("status", status));
			context.Response.ContentType = "text/plain";
			context.Response.BinaryWrite(StaticFunction.Compress(result.ToString()));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000052E4 File Offset: 0x000034E4
		public static byte[] Compress(byte[] data)
		{
			MemoryStream ms = new MemoryStream();
			ZOutputStream ds = new ZOutputStream(ms, 3);
			ds.Write(data, 0, data.Length);
			ds.Flush();
			ds.Close();
			return ms.ToArray();
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000014 RID: 20
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
