using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

// Ajax kütüphanesini dahil ediyoruz (Muhtemelen Ajax.NET Pro kullanılıyor)
using Ajax;

// İş mantığı katmanını dahil ediyoruz
using Bussiness;

namespace Count
{
    // Token: 0x02000002 RID: 2
    public class clickhandler : Page
    {
        // Sayfa formu
        protected HtmlForm form1;

        // Token: 0x06000001 RID: 1 RVA: 0x0000208C File Offset: 0x0000028C
        // Sayfa ilk yüklendiğinde çalışır
        protected void Page_Load(object sender, EventArgs e)
        {
            // Bu sınıfın Ajax istemlerine cevap verebilmesi için tür kaydı yapılır
            Utility.RegisterTypeForAjax(typeof(click));
        }

        // Token: 0x06000002 RID: 2 RVA: 0x00002FB4 File Offset: 0x000011B4
        // Bu metot JavaScript tarafından (örn: onclick veya onunload olayında) asenkron çağrılır
        [AjaxMethod]
        public string Logoff(string App_Id, string Direct_Url, string Referry_Url, string Begin_time, string ScreenW, string ScreenH, string Color, string Flash)
        {
            HttpContext current = HttpContext.Current;
            Dictionary<string, string> clientInfos = new Dictionary<string, string>();

            try
            {
                // Uygulama Kimliği
                clientInfos.Add("Application_Id", App_Id);

                // --- IP ADRESİ TESPİTİ (GÜVENLİĞİ ARTIRILDI) ---
                string ip = string.Empty;

                // Eğer sunucu bir Load Balancer veya Proxy (Cloudflare vb.) arkasındaysa, 
                // gerçek IP genellikle HTTP_X_FORWARDED_FOR başlığındadır.
                if (current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    // Virgülle ayrılmış olabilir, ilk adresi alıyoruz
                    ip = current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(',')[0].Trim();
                }
                else
                {
                    // Proxy yoksa doğrudan uzak IP adresini al
                    ip = current.Request.UserHostAddress;
                }
                clientInfos.Add("IP", ip);
                clientInfos.Add("IPAddress", ip);

                // --- TARAYICI VE CİHAZ BİLGİLERİ ---
                string userAgent = (current.Request.UserAgent == null) ? "Bilinmiyor" : current.Request.UserAgent;
                clientInfos.Add("UserAgent", userAgent);

                // İşlemci Bilgisi (Eski kafa ama tutuyoruz)
                bool flag = current.Request.ServerVariables["HTTP_UA_CPU"] == null;
                clientInfos.Add("CPU", flag ? "Bilinmiyor" : current.Request.ServerVariables["HTTP_UA_CPU"]);

                // İşletim Sistemi (Aşağıda güncellenmiş metod kullanılıyor)
                clientInfos.Add("OperSystem", GetOSNameByUserAgent(userAgent));

                // .NET Desteği
                bool flag2 = current.Request.Browser.ClrVersion == null;
                clientInfos.Add(".NETCLR", flag2 ? "Yok" : current.Request.Browser.ClrVersion.ToString());

                // Tarayıcı Adı ve Versiyonu (Eski .NET yapısı bazen "Default" döner, biz bunu manuel güncelleyeceğiz)
                string browserName = current.Request.Browser.Browser;
                string browserVersion = current.Request.Browser.Version;

                // Eski BrowserCaps dosyaları Chrome/Edge'i tanımayabilir, UserAgent stringinden manuel kontrol ekliyoruz
                if (userAgent.Contains("Edg/") && userAgent.Contains("Chrome"))
                {
                    browserName = "Edge (Chromium)";
                }
                else if (userAgent.Contains("Chrome") && !userAgent.Contains("Edg/"))
                {
                    browserName = "Chrome";
                }
                else if (userAgent.Contains("Firefox"))
                {
                    browserName = "Firefox";
                }
                else if (userAgent.Contains("Safari") && !userAgent.Contains("Chrome"))
                {
                    browserName = "Safari";
                }
                else if (userAgent.Contains("Opera") || userAgent.Contains("OPR"))
                {
                    browserName = "Opera";
                }

                clientInfos.Add("Browser", browserName + " " + browserVersion);

                // --- TARAYICI ÖZELLİKLERİ (Legacy bilgiler korunuyor) ---
                clientInfos.Add("ActiveX", current.Request.Browser.ActiveXControls ? "True" : "False"); // Artık hemen hemen hep False döner
                clientInfos.Add("Cookies", current.Request.Browser.Cookies ? "True" : "False");
                clientInfos.Add("CSS", current.Request.Browser.SupportsCss ? "True" : "False");

                // Dil
                if (current.Request.UserLanguages != null && current.Request.UserLanguages.Length > 0)
                {
                    clientInfos.Add("Language", current.Request.UserLanguages[0]);
                }
                else
                {
                    clientInfos.Add("Language", "tr-TR"); // Varsayılan
                }

                // --- CİHAZ TÜRÜ (Bilgisayar / Mobil) ---
                // Eski metod: HTTP_ACCEPT içinde 'wap' arıyordu (Artık güvensiz).
                // Yeni metod: UserAgent veya Request.Browser özelliklerini kullanıyoruz.
                bool isMobile = current.Request.Browser.IsMobileDevice || userAgent.Contains("Mobile") || userAgent.Contains("Android") || userAgent.Contains("iPhone");
                clientInfos.Add("Computer", isMobile ? "False" : "True"); // Computer = Masaüstü mü?

                // Platform (Win32 vs Win64 vb.)
                clientInfos.Add("Platform", current.Request.Browser.Platform);

                // Eski windows versiyonları için kontroller
                clientInfos.Add("Win16", current.Request.Browser.Win16 ? "True" : "False");
                clientInfos.Add("Win32", current.Request.Browser.Win32 ? "True" : "False");

                // --- BAŞKA HEADER BİLGİLERİ ---
                bool flag5 = current.Request.ServerVariables["HTTP_ACCEPT_ENCODING"] == null;
                clientInfos.Add("AcceptEncoding", flag5 ? "Yok" : current.Request.ServerVariables["HTTP_ACCEPT_ENCODING"]);

                // --- İSTEMCİDEN GELEN PARAMETRELER ---
                // Referrer: Hangi sayfadan gelindi?
                clientInfos.Add("Referry", Referry_Url);

                // Redirect: Tıklanacak gidilecek sayfa
                clientInfos.Add("Redirect", Direct_Url);

                // Zaman bilgisi
                clientInfos.Add("TimeSpan", Begin_time.ToString());

                // Ekran Bilgileri
                clientInfos.Add("ScreenWidth", ScreenW);
                clientInfos.Add("ScreenHeight", ScreenH);
                clientInfos.Add("Color", Color);

                // Flash (Artık teknoloji öldü ama parametre bekliyor olabilir)
                clientInfos.Add("Flash", Flash);

                // --- VERİTABANI KAYDI ---
                // Hazırlanan sözlük (dictionary) iş katmanına gönderilir
                CountBussiness.InsertContentCount(clientInfos);
            }
            catch (Exception ex)
            {
                // Hata olursa hata mesajını istemciye geri gönder
                return ex.ToString();
            }

            // İşlem başarılı
            return "ok";
        }

        // Token: 0x06000003 RID: 3 RVA: 0x00003380 File Offset: 0x00001580
        // İşletim Sistemi tespit eden yardımcı metod (Güncellendi)
        private static string GetOSNameByUserAgent(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent)) return "Bilinmiyor";

            // Mobil Cihazlar
            if (userAgent.Contains("Windows Phone")) return "Windows Phone";
            if (userAgent.Contains("Android")) return "Android";
            if (userAgent.Contains("iPad")) return "iPad (iOS)";
            if (userAgent.Contains("iPhone") || userAgent.Contains("iPod")) return "iPhone (iOS)";

            // Macintosh
            if (userAgent.Contains("Macintosh") || userAgent.Contains("Mac OS X"))
            {
                // M1/M2 çipleri ayırt etmek için "Intel" kontrolü yapılabilir
                return "macOS";
            }

            // Windows Sürümleri (NT çekirdeği üzerinden)
            if (userAgent.Contains("Windows NT 10.0") || userAgent.Contains("Windows NT 11.0"))
                return "Windows 10 / 11";
            if (userAgent.Contains("Windows NT 6.3"))
                return "Windows 8.1";
            if (userAgent.Contains("Windows NT 6.2"))
                return "Windows 8";
            if (userAgent.Contains("Windows NT 6.1"))
                return "Windows 7";
            if (userAgent.Contains("Windows NT 6.0"))
                return "Windows Vista";
            if (userAgent.Contains("Windows NT 5.2"))
                return "Windows Server 2003 / XP 64-bit";
            if (userAgent.Contains("Windows NT 5.1"))
                return "Windows XP";
            if (userAgent.Contains("Windows NT 5.0"))
                return "Windows 2000";
            if (userAgent.Contains("Windows NT 4"))
                return "Windows NT4";
            if (userAgent.Contains("Windows 98") || userAgent.Contains("Win98"))
                return "Windows 98";
            if (userAgent.Contains("Windows 95"))
                return "Windows 95";

            // Linux ve Diğerleri
            if (userAgent.Contains("Linux"))
                return "Linux";
            if (userAgent.Contains("Ubuntu"))
                return "Ubuntu";
            if (userAgent.Contains("CrOS"))
                return "Chrome OS";
            if (userAgent.Contains("Unix"))
                return "UNIX";
            if (userAgent.Contains("SunOS"))
                return "SunOS";

            return "Bilinmeyen İşletim Sistemi";
        }
    }
}