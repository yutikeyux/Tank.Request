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
	// Token: 0x0200004B RID: 75
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class loadpetmoeproperty : IHttpHandler
	{
		// Token: 0x06000157 RID: 343 RVA: 0x0000ACB0 File Offset: 0x00008EB0
		public void ProcessRequest(HttpContext context)
		{
			bool flag = csFunction.ValidAdminIP(context.Request.UserHostAddress);
			if (flag)
			{
				context.Response.Write(loadpetmoeproperty.Bulid(context));
			}
			else
			{
				context.Response.Write("Tabi Efendim!");
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000ACF8 File Offset: 0x00008EF8
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
					foreach (PetMoePropertyInfo petm in produceBussiness.GetAllPetMoeProperty())
					{
						xelement.Add(FlashUtils.CreatePetMoePropertyItems(petm));
					}
					result.Add(xelement);
					flag = true;
					str = "Success!";
				}
			}
			catch (Exception ex)
			{
				loadpetmoeproperty.log.Error("loadpetmoeproperty", ex);
			}
			result.Add(new XAttribute("value", flag));
			result.Add(new XAttribute("message", str));
			csFunction.CreateCompressXml(context, result, "loadpetmoeproperty_out", false);
			return csFunction.CreateCompressXml(context, result, "loadpetmoeproperty", true);
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400004E RID: 78
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
