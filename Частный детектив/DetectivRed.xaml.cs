using System.Windows;

namespace Частный_детектив
{
    /// <summary>
    /// Логика взаимодействия для DetectivRed.xaml
    /// </summary>
    public partial class DetectivRed : Window
    {
        Detective Detective;
        User User;
        EntityModelContainer db = new EntityModelContainer();
        public DetectivRed(Detective detective, User user)
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;
            Detective = detective;
            User = user;
        }
        private void ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
