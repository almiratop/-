using System;
using System.Windows;

namespace Частный_детектив
{
    /// <summary>
    /// Логика взаимодействия для Detectiv.xaml
    /// </summary>
    public partial class Detectiv : Window
    {
        Detective Detective;
        public Detectiv(Detective detective)
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;
            Detective = detective;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DetectivZayav form = new DetectivZayav(Detective);
            form.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow form = new MainWindow();
            form.Show();
            this.Hide();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DetectiveSmses form = new DetectiveSmses(Detective);
            form.Show();
            this.Hide();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
