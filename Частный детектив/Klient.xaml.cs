using System;
using System.Windows;

namespace Частный_детектив
{
    /// <summary>
    /// Логика взаимодействия для Klient.xaml
    /// </summary>
    public partial class Klient : Window
    {
        Client Client;
        public Klient(Client client)
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;
            Client = client;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow form = new MainWindow();
            form.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            KlientSms form = new KlientSms(Client);
            form.Show();
            this.Hide();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
