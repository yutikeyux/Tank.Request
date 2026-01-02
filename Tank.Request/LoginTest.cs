using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Bussiness.Interface;

namespace Tank.Request
{
	// Token: 0x02000055 RID: 85
	public class LoginTest : Page
	{
		// Token: 0x06000184 RID: 388 RVA: 0x0000C934 File Offset: 0x0000AB34
		protected void Page_Load(object sender, EventArgs e)
		{
			string name = "onelife";
			string pass = "733789";
			int time = 1255165271;
			string loginKey = "yk-MotL-qhpAo88-7road-mtl55dantang-login-logddt777";
			string key = BaseInterface.md5(name + pass + time.ToString() + loginKey);
			string content = "content=" + HttpUtility.UrlEncode(string.Concat(new string[]
			{
				name,
				"|",
				pass,
				"|",
				time.ToString(),
				"|",
				key
			}));
			string str = "http://localhost:728/CreateLogin.aspx?content=" + HttpUtility.UrlEncode(string.Concat(new string[]
			{
				name,
				"|",
				pass,
				"|",
				time.ToString(),
				"|",
				key
			}));
			string url = BaseInterface.RequestContent(str);
			base.Response.Write(url);
		}

		// Token: 0x04000057 RID: 87
		protected HtmlForm form1;
	}
}
