using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Частный_детектив
{
    /// <summary>
    /// Логика взаимодействия для KlientSms.xaml
    /// </summary>
    public partial class KlientSms : Window
    {
        Client Client;
        int DetId;
        Sms Sms;
        TextBox body;
        EntityModelContainer db = new EntityModelContainer();
        string text;
        public KlientSms(Client client)
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;
            Client = client;
        }
		private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
            Klient form = new Klient(Client);
            form.textblock.Text = "Добро пожаловать в Личный кабинет, " + Client.Name + "\n" + "Здесь можете просматривать входящие заявки на консультацию, писать и отвечать на сообщения.";
            form.Show();
            this.Hide();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Klient form = new Klient(Client);
            form.textblock.Text = "Добро пожаловать в Личный кабинет, " + Client.Name + "\n" + "Здесь можете просматривать входящие заявки на консультацию, писать и отвечать на сообщения.";
            form.Show();
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                text = "";
                int ClId = Client.Id;
                var query = from n in db.Clients
                            join m in db.Detectives
                            on n.Detective equals m
                            where n.Id == Client.Id
                            orderby n.Id
                            select new { m.Id };

                foreach (var w in query)
                    text = $"{w}";
                text = text.Replace("Id", "");
                text = text.Replace("=", "");
                text = text.Replace(" ", "");
                text = text.Replace("{", "");
                text = text.Replace("}", "");
                DetId = Convert.ToInt32(text);
                text = "";
                query = from n in db.Smss
                        where n.DetectiveId == DetId && n.ClientId == ClId
                        orderby n.Id
                        select new { n.Id };

                foreach (var w in query)
                    text = text + "," + $"{w}";
                if (text.Contains("Id"))
                {

                    text = text.Replace("Id", "");
                    text = text.Replace("=", "");
                    text = text.Replace(" ", "");
                    text = text.Replace("{", "");
                    text = text.Replace("}", "");
                    text = text.Substring(1);
                    string[] m = text.Split(new char[] { ',' }); //[1, 2, 3]
                    int dl = m.Length;
                    int sch = 0;
                    while (dl != 0)
                    {
                        int id = Convert.ToInt32(m[sch]);
                        Sms = db.Smss.Find(id);
                        body = new TextBox();
                        body.Style = (Style)Resources["Text"];
                        body.Text = Sms.Sender + "        |    " + Sms.Text;
                        stack.Children.Add(body);
                        dl = dl - 1;
                        sch = sch + 1;
                        scroll.ScrollToEnd();
                    }
                }
                else { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBoxMessage.Text != "")
                {
                    if (textBoxMessage.Text.Length < 90)
                    {
                        Sms sms = new Sms();
                        sms.Text = textBoxMessage.Text;
                        sms.Sender = "Клиент";
                        sms.DetectiveId = DetId;
                        sms.ClientId = Client.Id;
                        db.Smss.Add(sms);
                        db.SaveChanges();

                        body = new TextBox();
                        body.Style = (Style)Resources["Text"];
                        body.Text = "Клиент" + "  |    " + textBoxMessage.Text;
                        stack.Children.Add(body);
                        scroll.ScrollToEnd();
                        textBoxMessage.Text = "";
                    }
                    else { MessageBox.Show("Сообщение длинное, ограничение - 90 символов"); }
                }
                else { MessageBox.Show("Сообщение пустое!"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
