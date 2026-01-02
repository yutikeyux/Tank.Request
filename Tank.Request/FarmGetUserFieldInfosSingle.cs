using System;
using System.Reflection;
using System.Web;
using System.Xml.Linq;
using Bussiness;
using log4net;

namespace Tank.Request
{
	// Token: 0x02000036 RID: 54
	public class FarmGetUserFieldInfosSingle : IHttpHandler
	{
		// Token: 0x060000FE RID: 254 RVA: 0x00008E08 File Offset: 0x00007008
		public void ProcessRequest(HttpContext context)
		{
			bool flag = false;
			string str = "fail!";
			XElement node = new XElement("Result");
			string str2 = context.Request["friendID"];
			try
			{
				XElement xelement = new XElement("Item", new object[]
				{
					new XAttribute("UserID", str2),
					new XAttribute("isFeed", false)
				});
				node.Add(xelement);
				flag = true;
				str = "Success!";
			}
			catch (Exception ex)
			{
				FarmGetUserFieldInfosSingle.log.Error("FarmGetUserFieldInfosSingle", ex);
			}
			node.Add(new XAttribute("value", flag));
			node.Add(new XAttribute("message", str));
			context.Response.ContentType = "text/plain";
			context.Response.Write(node.ToString(false));
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400003E RID: 62
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
