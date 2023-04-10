using System.Windows;

namespace Частный_детектив
{
    /// <summary>
    /// Логика взаимодействия для UslugiRed.xaml
    /// </summary>
    public partial class UslugiRed : Window
    {
        Usluga Usluga;
        EntityModelContainer db = new EntityModelContainer();
        public UslugiRed(Usluga usluga)
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;
            Usluga = usluga;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;           
        }
    }
}
