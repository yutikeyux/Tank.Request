using System;
using System.Collections.Generic;
using System.Web;
using System.Xml.Linq;
using Bussiness;
using SqlDataProvider.Data;

namespace Tank.Request
{
	// Token: 0x02000032 RID: 50
	public class eventrewarditemlist : IHttpHandler
	{
		// Token: 0x060000EA RID: 234 RVA: 0x000083AC File Offset: 0x000065AC
		public void ProcessRequest(HttpContext context)
		{
			bool flag = csFunction.ValidAdminIP(context.Request.UserHostAddress);
			if (flag)
			{
				context.Response.Write(eventrewarditemlist.Bulid(context));
			}
			else
			{
				context.Response.Write("Tabi Efendim!");
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000083F8 File Offset: 0x000065F8
		public static string Bulid(HttpContext context)
		{
			bool value = false;
			string message = "Fail!";
			XElement result = new XElement("Result");
			try
			{
				using (ProduceBussiness db = new ProduceBussiness())
				{
					Dictionary<int, Dictionary<int, EventRewardInfo>> EventRewardInfo = new Dictionary<int, Dictionary<int, EventRewardInfo>>();
					EventRewardInfo[] eventInfos = db.GetAllEventRewardInfo();
					EventRewardGoodsInfo[] eventGoods = db.GetAllEventRewardGoods();
					foreach (EventRewardInfo item in eventInfos)
					{
						item.AwardLists = new List<EventRewardGoodsInfo>();
						bool flag = !EventRewardInfo.ContainsKey(item.ActivityType);
						if (flag)
						{
							Dictionary<int, EventRewardInfo> tmp = new Dictionary<int, EventRewardInfo>();
							tmp.Add(item.SubActivityType, item);
							EventRewardInfo.Add(item.ActivityType, tmp);
						}
						else
						{
							bool flag2 = !EventRewardInfo[item.ActivityType].ContainsKey(item.SubActivityType);
							if (flag2)
							{
								EventRewardInfo[item.ActivityType].Add(item.SubActivityType, item);
							}
						}
					}
					foreach (EventRewardGoodsInfo good in eventGoods)
					{
						bool flag3 = EventRewardInfo.ContainsKey(good.ActivityType) && EventRewardInfo[good.ActivityType].ContainsKey(good.SubActivityType);
						if (flag3)
						{
							EventRewardInfo[good.ActivityType][good.SubActivityType].AwardLists.Add(good);
						}
					}
					XElement ActiveType = null;
					foreach (Dictionary<int, EventRewardInfo> eventInActive in EventRewardInfo.Values)
					{
						foreach (EventRewardInfo info in eventInActive.Values)
						{
							bool flag4 = ActiveType == null;
							if (flag4)
							{
								ActiveType = new XElement("ActivityType", new XAttribute("value", info.ActivityType));
							}
							XElement Items = new XElement("Items", new object[]
							{
								new XAttribute("SubActivityType", info.SubActivityType),
								new XAttribute("Condition", info.Condition)
							});
							foreach (EventRewardGoodsInfo awardGood in info.AwardLists)
							{
								XElement Item = new XElement("Item", new object[]
								{
									new XAttribute("TemplateId", awardGood.TemplateId),
									new XAttribute("StrengthLevel", awardGood.StrengthLevel),
									new XAttribute("AttackCompose", awardGood.AttackCompose),
									new XAttribute("DefendCompose", awardGood.DefendCompose),
									new XAttribute("LuckCompose", awardGood.LuckCompose),
									new XAttribute("AgilityCompose", awardGood.AgilityCompose),
									new XAttribute("IsBind", awardGood.IsBind),
									new XAttribute("ValidDate", awardGood.ValidDate),
									new XAttribute("Count", awardGood.Count)
								});
								Items.Add(Item);
							}
							ActiveType.Add(Items);
						}
						result.Add(ActiveType);
						ActiveType = null;
					}
					value = true;
					message = "Success!";
				}
			}
			catch
			{
			}
			result.Add(new XAttribute("value", value));
			result.Add(new XAttribute("message", message));
			csFunction.CreateCompressXml(context, result, "eventrewarditemlist_out", false);
			return csFunction.CreateCompressXml(context, result, "eventrewarditemlist", true);
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}
