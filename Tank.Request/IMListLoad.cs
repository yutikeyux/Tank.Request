using System;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Xml.Linq;
using Bussiness;
using log4net;
using SqlDataProvider.Data;

namespace Tank.Request
{
	// Token: 0x02000042 RID: 66
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class IMListLoad : IHttpHandler
	{
		// Token: 0x0600012F RID: 303 RVA: 0x0000A064 File Offset: 0x00008264
		public void ProcessRequest(HttpContext context)
		{
			bool flag = false;
			string value = "Fail!";
			XElement xelement = new XElement("Result");
			try
			{
				int userID = int.Parse(context.Request["id"]);
				using (PlayerBussiness playerBussiness = new PlayerBussiness())
				{
					FriendInfo[] friendsAll = playerBussiness.GetFriendsAll(userID);
					XElement content = new XElement("customList", new object[]
					{
						new XAttribute("ID", 0),
						new XAttribute("Name", "Arkadaşlar")
					});
					xelement.Add(content);
					foreach (FriendInfo friendInfo in friendsAll)
					{
						XElement content2 = new XElement("Item", new object[]
						{
							new XAttribute("ID", friendInfo.FriendID),
							new XAttribute("NickName", friendInfo.NickName),
							new XAttribute("Birthday", DateTime.Now),
							new XAttribute("ApprenticeshipState", 0),
							new XAttribute("LoginName", friendInfo.UserName),
							new XAttribute("Style", friendInfo.Style),
							new XAttribute("Sex", friendInfo.Sex == 1),
							new XAttribute("Colors", friendInfo.Colors),
							new XAttribute("Grade", friendInfo.Grade),
							new XAttribute("Hide", friendInfo.Hide),
							new XAttribute("ConsortiaName", friendInfo.ConsortiaName),
							new XAttribute("TotalCount", friendInfo.Total),
							new XAttribute("EscapeCount", friendInfo.Escape),
							new XAttribute("WinCount", friendInfo.Win),
							new XAttribute("Offer", friendInfo.Offer),
							new XAttribute("Relation", friendInfo.Relation),
							new XAttribute("Repute", friendInfo.Repute),
							new XAttribute("State", (friendInfo.State == 1) ? 1 : 0),
							new XAttribute("Nimbus", friendInfo.Nimbus),
							new XAttribute("DutyName", friendInfo.DutyName)
						});
						xelement.Add(content2);
					}
				}
				flag = true;
				value = "Success!";
			}
			catch (Exception exception)
			{
				IMListLoad.log.Error("IMListLoad", exception);
			}
			xelement.Add(new XAttribute("value", flag));
			xelement.Add(new XAttribute("message", value));
			context.Response.ContentType = "text/plain";
			context.Response.Write(xelement.ToString(false));
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000215A File Offset: 0x0000035A
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000047 RID: 71
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
