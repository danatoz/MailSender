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
            { "huhehi@yandex.ru" , CodePassword.getPassword ( "1234l;i" ) },
            { "sok74@yandex.ru" , CodePassword.getPassword ( ";liq34tjk" ) }
        };
    }
}
