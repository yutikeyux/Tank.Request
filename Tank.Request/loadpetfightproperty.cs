using System;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Xml.Linq;
using Bussiness;
using log4net;
using Road.Flash;
using SqlDataProvider.Data;

namespace Tank.Request
{
	// Token: 0x0200004A RID: 74
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class loadpetfightproperty : IHttpHandler
	{
		// Token: 0x06000152 RID: 338 RVA: 0x0000AB4C File Offset: 0x00008D4C
		public void ProcessRequest(HttpContext context)
		{
			bool flag = csFunction.ValidAdminIP(context.Request.UserHostAddress);
			if (flag)
			{
				context.Response.Write(loadpetfightproperty.Bulid(context));
			}
			else
			{
				context.Response.Write("IP is not valid!");
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000AB94 File Offset: 0x00008D94
		public static string Bulid(HttpContext context)
		{
			bool flag = false;
			string str = "Fail!";
			XElement result = new XElement("Result");
			try
			{
				using (ProduceBussiness produceBussiness = new ProduceBussiness())
				{
					XElement xelement = new XElement("ItemTemplate");
					foreach (PetFightPropertyInfo petf in produceBussiness.GetAllPetFightProperty())
					{
						xelement.Add(FlashUtils.CreatePetFightProterpy(petf));
					}
					result.Add(xelement);
					flag = true;
					str = "Success!";
				}
			}
			catch (Exception ex)
			{
				loadpetfightproperty.log.Error("loadpetfightproperty", ex);
			}
			result.Add(new XAttribute("value", flag));
			result.Add(new XAttribute("message", str));
			csFunction.CreateCompressXml(context, result, "loadpetfightproperty_out", false);
			return csFunction.CreateCompressXml(context, result, "loadpetfightproperty", true);
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400004D RID: 77
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
