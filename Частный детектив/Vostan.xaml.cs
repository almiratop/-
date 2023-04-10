using System;
using System.Windows;
using System.Windows.Input;
using System.Net.Mail;
using System.Net;

namespace Частный_детектив
{
    /// <summary>
    /// Логика взаимодействия для Vostan.xaml
    /// </summary>
    public partial class Vostan : Window
    {
        public Vostan()
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MailAddress from = new MailAddress("38_g@inbox.ru", "Восстановление данных");
                MailAddress to = new MailAddress(textBoxMail.Text);
                MailMessage m = new MailMessage(from, to);
                m.Subject = "Частное агентство";
                using (EntityModelContainer db = new EntityModelContainer())
                {
                    foreach (Detective detective in db.Detectives)
                    {
                        if (textBoxMail.Text == detective.Mail)
                        {
                            foreach (User user in db.Users)
                            {
                                if (detective.User.Id == user.Id)
                                {
                                    m.Body = "<h1>Логин: " + detective.User.Login + "</h1>" + "<h1>Пароль: " + detective.User.Password + "</h1>";
                                }
                            }
                        }
                    }
                    foreach (Client client in db.Clients)
                    {
                        if (textBoxMail.Text == client.Mail)
                        {
                            foreach (User user in db.Users)
                            {
                                if (client.User.Id == user.Id)
                                {
                                    m.Body = "<h1>Логин: " + client.User.Login + "</h1>" + "<h1>Пароль: " + client.User.Password + "</h1>";
                                }
                            }
                        }
                    }
                }
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                smtp.Credentials = new NetworkCredential("38_g@inbox.ru", "pPydF1U0ePfCmTXXTaum");
                smtp.EnableSsl = true;
                smtp.Send(m);
                MessageBox.Show("Логин и пароль отправлены на почту");
                Vhod form = new Vhod();
                form.Show();
                this.Hide();
            }
            catch
            {
                MessageBox.Show("Пользователя с такой почтой не существует");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
			Application.Current.Shutdown();
		}
		private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			MainWindow form = new MainWindow();
			form.Show();
			this.Hide();
		}
	}
}
