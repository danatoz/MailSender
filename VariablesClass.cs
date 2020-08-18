using System.Collections.Generic;
using CodePasswordDLL;

namespace MailSender
{
    class VariablesClass
    {
        public static Dictionary<string, string> Senders
        {
            get => dicSenders;
        }
        private static Dictionary<string,string> dicSenders = new Dictionary<string, string>()
        {
            { "huhehi@yandex.ru" , CodePassword.getPassword ( "4qiYQMTonV1F" ) },
            { "sok74@yandex.ru" , CodePassword.getPassword ( ";liq34tjk" ) }
        };
        public static Dictionary<string, int> SmtpServers = new Dictionary<string, int>()
        {
            { "smtp.yandex.ru", 25 }
        };
    }
}
