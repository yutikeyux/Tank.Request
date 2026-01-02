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
	// Token: 0x02000012 RID: 18
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class CardUpdateInfo : IHttpHandler
	{
		// Token: 0x06000047 RID: 71 RVA: 0x0000222A File Offset: 0x0000042A
		public void ProcessRequest(HttpContext context)
		{
			context.Response.Write(CardUpdateInfo.Bulid(context));
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000049F8 File Offset: 0x00002BF8
		    public static string Bulid(HttpContext context)
        {
            bool value = false;
            string message = "Fail!";
            XElement result = new XElement("Result");
            try
            {
                using (ProduceBussiness db = new ProduceBussiness())
                {
                    SqlDataProvider.Data.CardUpdateInfo[] infos = db.GetAllCardUpdateInfo();
                    foreach (SqlDataProvider.Data.CardUpdateInfo cardInfo in infos)
                    {
                        result.Add(FlashUtils.CreateCardUpdateInfo(cardInfo));
                    }
                    value = true;
                    message = "Success!";
                }
            }
            catch (Exception ex)
            {
                CardUpdateInfo.log.Error("Load CardUpdateInfo is fail!", ex);
            }
            result.Add(new XAttribute("value", value));
            result.Add(new XAttribute("message", message));
            return csFunction.CreateCompressXml(context, result, "CardUpdateInfo", true);
        }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000010 RID: 16
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
