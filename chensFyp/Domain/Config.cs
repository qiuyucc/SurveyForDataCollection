using System.Web.Configuration;

namespace chensFyp.Web.Domain
{
    public class Config
    {
        public static string AccountSid => WebConfigurationManager.AppSettings["AccountSid"] ??
                                           "AC92f62d1107e2cf0cae941cc66ba02e59";

        public static string AuthToken => WebConfigurationManager.AppSettings["AuthToken"] ??
                                          "a710604cdbbe17466a14bfa03e3cd610";

        public static string TwilioNumber => WebConfigurationManager.AppSettings["TwilioNumber"] ??
                                             "+16093888767 ";
    }
}