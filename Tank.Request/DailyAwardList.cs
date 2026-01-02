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
	// Token: 0x0200002A RID: 42
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class DailyAwardList : IHttpHandler
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00002474 File Offset: 0x00000674
		public void ProcessRequest(HttpContext context)
		{
			context.Response.Write(DailyAwardList.Bulid(context));
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000785C File Offset: 0x00005A5C
		public static string Bulid(HttpContext context)
		{
			bool value = false;
			string message = "Fail!";
			XElement result = new XElement("Result");
			try
			{
				using (ProduceBussiness db = new ProduceBussiness())
				{
					DailyAwardInfo[] infos = db.GetAllDailyAward();
					foreach (DailyAwardInfo info in infos)
					{
						result.Add(FlashUtils.CreateActiveInfo(info));
					}
					value = true;
					message = "Success!";
				}
			}
			catch (Exception ex)
			{
				DailyAwardList.log.Error("Load DailyAwardList is fail!", ex);
			}
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			return csFunction.CreateCompressXml(context, result, "DailyAwardList", true);
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000027 RID: 39
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
