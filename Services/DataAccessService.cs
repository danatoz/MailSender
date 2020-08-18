using System;
using System.Collections.ObjectModel;

namespace MailSender.Services
{
    public interface IDataAccessService
    {
        ObservableCollection<Email> GetEmails();
        int CreateEmail(Email email);
    }
    class DataAccessService : IDataAccessService
    {
        private EmailsDataContext context;

        public DataAccessService()
        {
            context = new EmailsDataContext();
        }

        public int CreateEmail(Email email)
        {
            context.Email.InsertOnSubmit(email);
            context.SubmitChanges();
            return email.Id;
        }

        public ObservableCollection<Email> GetEmails()
        {
            ObservableCollection<Email> Emails = new ObservableCollection<Email>();
            foreach (var item in context.Email)
            {
                Emails.Add(item);
            }

            return Emails;
        }
    }
}
