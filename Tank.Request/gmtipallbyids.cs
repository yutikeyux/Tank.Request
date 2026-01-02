using System;
using System.Reflection;
using System.Web;
using System.Xml.Linq;
using Bussiness;
using log4net;
using Road.Flash;
using SqlDataProvider.Data;

namespace Tank.Request
{
	// Token: 0x0200003D RID: 61
	public class gmtipallbyids : IHttpHandler
	{
		// Token: 0x0600011F RID: 287 RVA: 0x00009770 File Offset: 0x00007970
		public void ProcessRequest(HttpContext context)
		{
			bool value = false;
			string message = "Fail!";
			XElement result = new XElement("Result");
			try
			{
				string p = context.Request["ids"];
				string[] ids = null;
				bool flag = !string.IsNullOrEmpty(p);
				if (flag)
				{
					ids = p.Split(new char[]
					{
						','
					});
				}
				bool flag2 = ids != null;
				if (flag2)
				{
					using (ProduceBussiness db = new ProduceBussiness())
					{
						EdictumInfo[] edictumInfos = db.GetAllEdictum();
						foreach (EdictumInfo info in edictumInfos)
						{
							info.ID = int.Parse(ids[0]);
							bool flag3 = info.EndDate.Date > DateTime.Now.Date;
							if (flag3)
							{
								result.Add(FlashUtils.CreateEdictum(info));
							}
						}
						value = true;
						message = "Success!";
					}
				}
			}
			catch (Exception ex)
			{
				message = ex.ToString();
			}
			finally
			{
				result.Add(new XAttribute("value", value));
				result.Add(new XAttribute("message", message));
				context.Response.ContentType = "text/plain";
				context.Response.Write(result.ToString(false));
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000043 RID: 67
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
