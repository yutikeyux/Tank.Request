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
	// Token: 0x02000011 RID: 17
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class CardUpdateCondition : IHttpHandler
	{
		// Token: 0x06000042 RID: 66 RVA: 0x000021FF File Offset: 0x000003FF
		public void ProcessRequest(HttpContext context)
		{
			context.Response.Write(CardUpdateCondition.Bulid(context));
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004900 File Offset: 0x00002B00
		public static string Bulid(HttpContext context)
		{
			bool value = false;
			string message = "Fail!";
			XElement result = new XElement("Result");
			try
			{
				using (ProduceBussiness db = new ProduceBussiness())
				{
					CardUpdateConditionInfo[] infos = db.GetAllCardUpdateCondition();
					foreach (CardUpdateConditionInfo info in infos)
					{
						result.Add(FlashUtils.CreateCardUpdateCondition(info));
					}
					value = true;
					message = "Success!";
				}
			}
			catch (Exception ex)
			{
				CardUpdateCondition.log.Error("Load CardUpdateCondition is fail!", ex);
			}
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			return csFunction.CreateCompressXml(context, result, "CardUpdateCondition", true);
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400000F RID: 15
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
