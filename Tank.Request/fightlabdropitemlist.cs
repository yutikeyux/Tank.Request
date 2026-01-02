using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml.Linq;
using Bussiness;
using log4net;
using SqlDataProvider.Data;

namespace Tank.Request
{
	// Token: 0x02000038 RID: 56
	public class fightlabdropitemlist : IHttpHandler
	{
		// Token: 0x06000107 RID: 263 RVA: 0x00009070 File Offset: 0x00007270
		public void ProcessRequest(HttpContext context)
		{
			bool flag = csFunction.ValidAdminIP(context.Request.UserHostAddress);
			if (flag)
			{
				context.Response.Write(fightlabdropitemlist.Bulid(context));
			}
			else
			{
				context.Response.Write("IP is not valid!");
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000090BC File Offset: 0x000072BC
		public static string Bulid(HttpContext context)
		{
			bool value = false;
			string message = "Fail!";
			XElement result = new XElement("Result");
			try
			{
				int[] copyids = new int[]
				{
					10000,
					10001,
					10002,
					10010,
					10011,
					10012,
					10020,
					10021,
					10022,
					10030,
					10031,
					10032,
					10040,
					10041,
					10042
				};
				using (ProduceBussiness db = new ProduceBussiness())
				{
					DropItem[] infos = db.GetAllDropItems();
					foreach (DropItem info in infos)
					{
						bool flag = copyids.Contains(info.DropId);
						if (flag)
						{
							result.Add(new XElement("Item", new object[]
							{
								new XAttribute("ID", info.DropId.ToString().Substring(0, 4)),
								new XAttribute("Easy", info.DropId.ToString().Substring(4, 1)),
								new XAttribute("AwardItem", info.ItemId),
								new XAttribute("Count", info.BeginData)
							}));
						}
					}
				}
				value = true;
				message = "Success!";
			}
			catch (Exception ex)
			{
				fightlabdropitemlist.log.Error("fightlabdropitemlist", ex);
			}
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			return csFunction.CreateCompressXml(context, result, "fightlabdropitemlist_out", false);
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000040 RID: 64
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
