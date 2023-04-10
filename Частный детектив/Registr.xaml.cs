using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Частный_детектив
{
    /// <summary>
    /// Логика взаимодействия для Registr.xaml
    /// </summary>
    public partial class Registr : Window
    {
        EntityModelContainer db = new EntityModelContainer();
        public Registr()
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBoxName.Text != "" && textBoxNumber.Text != "" && textBoxMail.Text != "" && textBoxLogin.Text != "" && textBoxPassword.Text != "") 
                {
                    int test = 0;
                    int us = db.Users.Count();
                    using (EntityModelContainer db = new EntityModelContainer())
                    {
                        foreach (User user in db.Users)
                        {
                            if (user.Login == textBoxLogin.Text)
                            {
                                test = test + 1;
                                MessageBox.Show("Пользователь с таким логином существует, придумайте другой");
                                break;
                            }
                        }
                        if (test == 0) 
                        {
                            int CountDetectives = db.Detectives.Count();
                            Random rnd = new Random();
                            int id = rnd.Next(1, CountDetectives + 1);
                            Detective detective = db.Detectives.Find(id);
                            User newuser = new User() { Login = textBoxLogin.Text, Password = textBoxPassword.Text, Role = "Клиент" };
                            db.Users.Add(newuser);
                            db.SaveChanges();
                            Client client = new Client() { User = newuser, Detective = detective, Name = textBoxName.Text, Number = textBoxNumber.Text, Mail = textBoxMail.Text };
                            db.Clients.Add(client);
                            db.SaveChanges();
                            Sms sms = new Sms();
                            sms.Text = "Добрый день! Меня зовут " + detective.Name + ". Чем могу помочь?";
                            sms.Sender = "Детектив";
                            sms.DetectiveId = detective.Id;
                            sms.ClientId = client.Id;
                            db.Smss.Add(sms);
                            db.SaveChanges();

                            MessageBox.Show("Успешная регистрация");
                            Vhod form = new Vhod();
                            form.Show();
                            this.Hide();
                        }
                    }
                }
                else { MessageBox.Show("Заполните все поля!"); }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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
