using System;
using System.Text;
using System.Web;
using System.Web.Services;

namespace Tank.Request.CelebList
{
	// Token: 0x02000097 RID: 151
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class CreateAllCeleb : IHttpHandler
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x00011FA8 File Offset: 0x000101A8
		public void ProcessRequest(HttpContext context)
		{
			bool flag = csFunction.ValidAdminIP(context.Request.UserHostAddress);
			if (flag)
			{
				StringBuilder bulid = new StringBuilder();
				bulid.Append(CelebByGpList.Build());
				bulid.Append(CelebByDayGPList.Build());
				bulid.Append(CelebByWeekGPList.Build());
				bulid.Append(CelebByOfferList.Build());
				bulid.Append(CelebByDayOfferList.Build());
				bulid.Append(CelebByWeekOfferList.Build());
				bulid.Append(CelebByDayFightPowerList.Build());
				bulid.Append(CelebByConsortiaRiches.Build());
				bulid.Append(CelebByConsortiaDayRiches.Build());
				bulid.Append(CelebByConsortiaWeekRiches.Build());
				bulid.Append(CelebByConsortiaHonor.Build());
				bulid.Append(CelebByConsortiaDayHonor.Build());
				bulid.Append(CelebByConsortiaWeekHonor.Build());
				bulid.Append(CelebByConsortiaLevel.Build());
				bulid.Append(CelebByDayBestEquip.Build());
				bulid.Append(celebbyconsortiafightpower.Build());
				context.Response.ContentType = "text/plain";
				context.Response.Write(bulid.ToString());
			}
			else
			{
				context.Response.Write("Tabi Efendim!" + context.Request.UserHostAddress);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}
