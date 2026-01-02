using System;
using System.Web;
using System.Xml.Linq;
using Bussiness;
using Road.Flash;
using SqlDataProvider.Data;

namespace Tank.Request
{
	// Token: 0x0200004D RID: 77
	public class LoadUserBox : IHttpHandler
	{
		// Token: 0x06000161 RID: 353 RVA: 0x0000AF58 File Offset: 0x00009158
		public void ProcessRequest(HttpContext context)
		{
			bool flag = csFunction.ValidAdminIP(context.Request.UserHostAddress);
			if (flag)
			{
				context.Response.Write(LoadUserBox.Bulid(context));
			}
			else
			{
				context.Response.Write("IP is not valid!");
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000AFA4 File Offset: 0x000091A4
		public static string Bulid(HttpContext context)
		{
			bool value = false;
			string message = "Fail!";
			XElement result = new XElement("Result");
			try
			{
				using (ProduceBussiness db = new ProduceBussiness())
				{
					UserBoxInfo[] itemBox = db.GetAllUserBox();
					foreach (UserBoxInfo s in itemBox)
					{
						result.Add(FlashUtils.CreateUserBoxInfo(s));
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
			return csFunction.CreateCompressXml(context, result, "LoadUserBox", true);
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}
