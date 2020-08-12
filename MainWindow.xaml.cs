using System;
using System.Linq;
using System.Windows;

namespace MailSender
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            cbSenderSelect.ItemsSource = VariablesClass.Senders;
            cbSenderSelect.DisplayMemberPath = "Key";
            cbSenderSelect.SelectedValuePath = "Value";

            DBClass db = new DBClass();
            dgEmails.ItemsSource = db.Emails;
        }

        private void MiClose_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnClock_OnClick(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedItem = tabPlaner;
        }

        private void BtnSendAtOnce_OnClick(object sender, RoutedEventArgs e)
        {
            string strLogin = cbSenderSelect.Text;
            string strPassword = cbSenderSelect.SelectedValue.ToString();
            if (string.IsNullOrEmpty(strLogin))
            {
                MessageBox.Show("Выберите отправителя");
                return;
            }

            if (string.IsNullOrEmpty(strPassword))
            {
                MessageBox.Show("Укажите пароль отправителя.");
                return;
            }
            EmailSendServiceClass emailSender = new EmailSendServiceClass(strLogin,strPassword);
            emailSender.SendMails((IQueryable<Email>)dgEmails.ItemsSource);
        }

        private void BtnSend_OnClick(object sender, RoutedEventArgs e)
        {
            SchedulerClass sc = new SchedulerClass();
            TimeSpan tsSendTime = sc.GetSendTime(tbTimePicker.Text);
            if (tsSendTime == new TimeSpan())
            {
                MessageBox.Show("Некорректный формат даты");
                return;
            }

            DateTime dtSendDateTime = (cldSchedulDateTimes.SelectedDate ?? DateTime.Today).Add(tsSendTime);
            if (dtSendDateTime < DateTime.Now)
            {
                MessageBox.Show("Дата и время отправки писем не могут быть раньше, чем настоящее время");
                return;
            }
            EmailSendServiceClass emailSender = new EmailSendServiceClass(cbSenderSelect.Text, cbSenderSelect.SelectedValue.ToString());
            sc.SendEmails(dtSendDateTime, emailSender, (IQueryable<Email>)dgEmails.ItemsSource);
        }

        private void TabSwitcherControl_OnBack(object sender, RoutedEventArgs e)
        {
            if (tabControl.SelectedIndex == 0) return;
            tabControl.SelectedIndex--;
        }

        private void TabSwitcherControl_OnForward(object sender, RoutedEventArgs e)
        {
            if (tabControl.SelectedIndex == tabControl.Items.Count - 1) return;
            tabControl.SelectedIndex++;
        }
    }
}
