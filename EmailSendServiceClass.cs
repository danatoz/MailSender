using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MailSender
{
    class EmailSendServiceClass
    {
        private string strLogin;
        private string strPassword;
        private string strSmtp = "smtp.yandex.ru";
        private int iSmtpPort = 25;
        private string strBody;
        private string strSubject;

        public EmailSendServiceClass(string sLogin, string sPassword)
        {
            strLogin = sLogin;
            strPassword = sPassword;
        }

        private void SendMail(string mail, string name)
        {
            using (MailMessage mm = new MailMessage(strLogin, mail))
            {
                mm.Subject = strSubject;
                mm.Body = "Hello world!";
                mm.IsBodyHtml = false;
                SmtpClient sc = new SmtpClient(strSmtp,iSmtpPort);
                sc.EnableSsl = true;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new NetworkCredential(strLogin, strPassword);
                try
                {
                    sc.Send(mm);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Невозможно отправить письмо " + e.ToString());
                }
            }
        }

        public void SendMails(ObservableCollection<Email> emails)
        {
            foreach (Email email in emails)
            {
                SendMail(email.EMail,email.Name);
            }
        }
    }
}
