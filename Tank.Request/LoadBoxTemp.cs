using System;
using System.Web;
using System.Xml.Linq;
using Bussiness;
using Road.Flash;
using SqlDataProvider.Data;

namespace Tank.Request
{
	// Token: 0x02000047 RID: 71
	public class LoadBoxTemp : IHttpHandler
	{
		// Token: 0x06000144 RID: 324 RVA: 0x0000A7DC File Offset: 0x000089DC
		public void ProcessRequest(HttpContext context)
		{
			bool flag = csFunction.ValidAdminIP(context.Request.UserHostAddress);
			if (flag)
			{
				context.Response.Write(LoadBoxTemp.Bulid(context));
			}
			else
			{
				context.Response.Write("Tabi Efendim!");
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000A828 File Offset: 0x00008A28
		public static string Bulid(HttpContext context)
		{
			bool value = false;
			string message = "Fail!";
			XElement result = new XElement("Result");
			try
			{
				using (ProduceBussiness db = new ProduceBussiness())
				{
					ItemBoxInfo[] itemBox = db.GetItemBoxInfos();
					foreach (ItemBoxInfo s in itemBox)
					{
						result.Add(FlashUtils.CreateItemBoxInfo(s));
					}
					value = true;
					message = "Success!";
				}
			}
			catch
			{
			}
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			return csFunction.CreateCompressXml(context, result, "LoadBoxTemp", true);
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}
