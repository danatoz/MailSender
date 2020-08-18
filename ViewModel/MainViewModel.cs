using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MailSender.Services;

namespace MailSender.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataAccessService _serviceProxy;
        private ObservableCollection<Email> _Emails;

        public ObservableCollection<Email> Emails
        {
            get { return _Emails; }
            set
            {
                _Emails = value;
                RaisePropertyChanged(nameof(Emails));
            }
        }

        private Email _EmailInfo;

        public Email EmailInfo
        {
            get => _EmailInfo;
            set
            {
                _EmailInfo = value;
                RaisePropertyChanged(nameof(EmailInfo));
            }
        }

        public RelayCommand ReadAllMailsComand { get; set; }
        public RelayCommand<Email> SaveCommand { get; set; }

        public MainViewModel(IDataAccessService servProxy)
        {
            _serviceProxy = servProxy;
            Emails = new ObservableCollection<Email>();
            EmailInfo = new Email();

            ReadAllMailsComand = new RelayCommand(GetEmails);
            SaveCommand = new RelayCommand<Email>(SaveEmail);
        }
        private void GetEmails()
        {
            Emails.Clear();
            foreach (var item in _serviceProxy.GetEmails())
            {
                Emails.Add(item);
            }
        }

        void SaveEmail(Email email)
        {
            EmailInfo.Id = _serviceProxy.CreateEmail(email);
            if (EmailInfo.Id != 0)
            {
                Emails.Add(EmailInfo);
                RaisePropertyChanged(nameof(EmailInfo));
            }
        }
    }
}