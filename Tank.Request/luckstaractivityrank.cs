using System;
using System.Web;
using System.Xml.Linq;
using Bussiness;
using Road.Flash;
using SqlDataProvider.Data;

namespace Tank.Request
{
	// Token: 0x02000057 RID: 87
	public class luckstaractivityrank : IHttpHandler
	{
		// Token: 0x0600018A RID: 394 RVA: 0x0000CB94 File Offset: 0x0000AD94
		public void ProcessRequest(HttpContext context)
		{
			int selfid = Convert.ToInt32(context.Request["selfid"]);
			string key = context.Request["key"];
			XElement ranks = new XElement("Ranks");
			LuckstarActivityRankInfo myRankInfo = new LuckstarActivityRankInfo();
			myRankInfo.nickName = "";
			using (PlayerBussiness db = new PlayerBussiness())
			{
				LuckstarActivityRankInfo[] LuckstarActivityRanks = db.GetAllLuckstarActivityRank();
				foreach (LuckstarActivityRankInfo r in LuckstarActivityRanks)
				{
					ranks.Add(FlashUtils.LuckstarActivityRank(r));
					bool flag = r.UserID == selfid;
					if (flag)
					{
						myRankInfo = r;
					}
				}
			}
			XElement myRank = new XElement("myRank", new object[]
			{
				new XAttribute("rank", myRankInfo.rank),
				new XAttribute("useStarNum", myRankInfo.useStarNum),
				new XAttribute("nickName", myRankInfo.nickName)
			});
			ranks.Add(myRank);
			bool value = true;
			string message = "Success!";
			ranks.Add(new XAttribute("lastUpdateTime", DateTime.Now.ToString("MM-dd hh:mm")));
			ranks.Add(new XAttribute("value", value));
			ranks.Add(new XAttribute("message", message));
			context.Response.ContentType = "text/plain";
			context.Response.Write(ranks.ToString(false));
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}
