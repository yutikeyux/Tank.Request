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
	// Token: 0x02000048 RID: 72
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class LoadItemsCategory : IHttpHandler
	{
		// Token: 0x06000148 RID: 328 RVA: 0x00002734 File Offset: 0x00000934
		public void ProcessRequest(HttpContext context)
		{
			context.Response.Write(LoadItemsCategory.Bulid(context));
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000A910 File Offset: 0x00008B10
		public static string Bulid(HttpContext context)
		{
			bool value = false;
			string message = "Fail!";
			XElement result = new XElement("Result");
			try
			{
				using (ProduceBussiness db = new ProduceBussiness())
				{
					CategoryInfo[] infos = db.GetAllCategory();
					foreach (CategoryInfo info in infos)
					{
						result.Add(FlashUtils.CreateCategoryInfo(info));
					}
				}
				value = true;
				message = "Success!";
			}
			catch (Exception ex)
			{
				LoadItemsCategory.log.Error("LoadItemsCategory", ex);
			}
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			return csFunction.CreateCompressXml(context, result, "LoadItemsCategory", true);
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400004B RID: 75
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
