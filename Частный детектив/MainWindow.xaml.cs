using System;
using System.Windows;
using System.Windows.Input;

namespace Частный_детектив
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EntityModelContainer model = new EntityModelContainer();
        public MainWindow()
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;
			main.Navigate(new Main());
			uslug.Navigate(new UslugPage());
			gar.Navigate(new GaranPage());
			con.Navigate(new ContacPage());
		}

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Zayavki form = new Zayavki();
            form.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Vhod form = new Vhod();
            form.Show();
            this.Hide();
        }

        private void up_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            qrcod form = new qrcod();
            form.Show();
        }

		private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			qrcod form = new qrcod();
			form.Show();

		}
    }
}
