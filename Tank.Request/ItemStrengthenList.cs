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
	// Token: 0x02000044 RID: 68
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class ItemStrengthenList : IHttpHandler
	{
		// Token: 0x06000137 RID: 311 RVA: 0x000026F3 File Offset: 0x000008F3
		public void ProcessRequest(HttpContext context)
		{
			context.Response.Write(ItemStrengthenList.Bulid(context));
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000A4C8 File Offset: 0x000086C8
		public static string Bulid(HttpContext context)
		{
			bool value = false;
			string message = "Fail";
			XElement result = new XElement("Result");
			try
			{
				using (ProduceBussiness db = new ProduceBussiness())
				{
					StrengthenInfo[] infos = db.GetAllStrengthen();
					foreach (StrengthenInfo info in infos)
					{
						result.Add(FlashUtils.CreateStrengthenInfo(info));
					}
				}
				value = true;
				message = "Success!";
			}
			catch (Exception ex)
			{
				ItemStrengthenList.log.Error("ItemStrengthenList", ex);
			}
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			return csFunction.CreateCompressXml(context, result, "ItemStrengthenList", true);
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000049 RID: 73
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
