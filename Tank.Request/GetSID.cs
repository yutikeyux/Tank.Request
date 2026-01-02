using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Web;
using System.Web.SessionState;
using System.Xml.Linq;

namespace Tank.Request
{
	// Token: 0x02000039 RID: 57
	public class GetSID : IHttpHandler, IRequiresSessionState
	{
		// Token: 0x0600010C RID: 268 RVA: 0x000092A0 File Offset: 0x000074A0
		public void ProcessRequest(HttpContext context)
		{
			CspParameters csp = new CspParameters();
			csp.Flags = CspProviderFlags.UseMachineKeyStore;
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);
			rsa.FromXmlString(ConfigurationSettings.AppSettings["privateKey"]);
			RSAParameters para = rsa.ExportParameters(false);
			XElement node = new XElement("result", new object[]
			{
				new XAttribute("m1", Convert.ToBase64String(para.Modulus)),
				new XAttribute("m2", Convert.ToBase64String(para.Exponent))
			});
			context.Response.ContentType = "text/plain";
			context.Response.Write(node.ToString());
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00003828 File Offset: 0x00001A28
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}
