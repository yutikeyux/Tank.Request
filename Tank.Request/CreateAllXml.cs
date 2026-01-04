using System;
using System.Text;
using System.Web;
using System.Web.Services;

namespace Tank.Request
{
	// Token: 0x02000026 RID: 38
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class CreateAllXml : IHttpHandler
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00006A84 File Offset: 0x00004C84
		public void ProcessRequest(HttpContext context)
		{
			bool flag = csFunction.ValidAdminIP(context.Request.UserHostAddress);
			if (flag)
			{
				StringBuilder build = new StringBuilder();
				build.Append(ActiveList.Bulid(context));
				build.Append(BallList.Bulid(context));
				build.Append(LoadMapsItems.Bulid(context));
				build.Append(LoadPVEItems.Build(context));
				build.Append(QuestList.Bulid(context));
				build.Append(TemplateAllList.Bulid(context));
				build.Append(ShopItemList.Bulid(context));
				build.Append(LoadItemsCategory.Bulid(context));
				build.Append(ItemStrengthenList.Bulid(context));
				build.Append(MapServerList.Bulid(context));
				build.Append(ConsortiaLevelList.Bulid(context));
				build.Append(DailyAwardList.Bulid(context));
				build.Append(NPCInfoList.Bulid(context));
				build.Append(LoginAwardItemTemplate.Bulid(context));
				build.Append(eventrewarditemlist.Bulid(context));
				build.Append(serverconfig.Bulid(context));
				build.Append(ShopGoodsShowList.Bulid(context));
				build.Append(newtitle.Bulid(context));
				build.Append(petskillelementinfo.Bulid(context));
				build.Append(petskillinfo.Bulid(context));
				build.Append(petskilltemplateinfo.Bulid(context));
				build.Append(pettemplateinfo.Bulid(context));
				build.Append(CardUpdateCondition.Build(context));
				build.Append(CardUpdateInfo.Build(context));
				build.Append(activitysystemitems.Build(context));
				build.Append(suittemplateinfolist.Build(context));
				build.Append(DailyLeagueLevelList.Build(context));
				build.Append(DailyLeagueAwardList.Build(context));
				context.Response.ContentType = "text/plain";
				context.Response.Write(build.ToString());
			}
			else
			{
				context.Response.Write("Tabi Efendim!");
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}
