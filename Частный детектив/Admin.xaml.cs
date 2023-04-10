using System;
using System.Linq;
using System.Windows;
namespace Частный_детектив
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        EntityModelContainer db = new EntityModelContainer();
        public Admin()
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow form = new MainWindow();
            form.Show();
            this.Hide();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            UslugiRed form = new UslugiRed(new Usluga());
            if (form.ShowDialog() == true)
            {
                if (form.textBoxName.Text != "" && form.textBoxPrice.Text != "")
                {
                    Usluga usluga = new Usluga();
                    usluga.Name = form.textBoxName.Text;
                    usluga.Price = form.textBoxPrice.Text;
                    db.Uslugas.Add(usluga);
                    db.SaveChanges();
                    var query = from n in db.Uslugas orderby n.Id select new { n.Id, n.Name, n.Price };
                    dataGridUslugi2.ItemsSource = query.ToList();
                    MessageBox.Show("Новая услуга добавлена");
                }
                else { MessageBox.Show("Не все поля заполнены"); }
            }
            
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridUslugi2.SelectedItems.Count == 1)
                {
                    string text = Convert.ToString(dataGridUslugi2.SelectedItem);
                    text = text.Replace("Id", "");
                    text = text.Replace("=", "");
                    text = text.Replace(" ", "");
                    text = text.Replace("Name", "");
                    text = text.Replace("Price", "");
                    text = text.Replace("{", "");
                    text = text.Replace("}", "");
                    string[] word = text.Split(new char[] { ',' });
                    int id = Convert.ToInt32(word[0]);
                    Usluga usluga = db.Uslugas.Find(id);
                    UslugiRed form = new UslugiRed(usluga);
                    form.textBoxName.Text = usluga.Name;
                    form.textBoxPrice.Text = usluga.Price;
                    if (form.ShowDialog() == true)
                    {
                        if (form.textBoxName.Text != "" && form.textBoxPrice.Text != "")
                        {
                            usluga.Name = form.textBoxName.Text;
                            usluga.Price = form.textBoxPrice.Text;
                            db.SaveChanges();
                            var query = from n in db.Uslugas orderby n.Id select new { n.Id, n.Name, n.Price };
                            dataGridUslugi2.ItemsSource = query.ToList();;
                            MessageBox.Show("Успешно изменено");
                        }
                        else { MessageBox.Show("Не все поля заполнены"); }
                    }
                }
                else { MessageBox.Show("Необходимо выделить только одну строку"); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (dataGridUslugi2.SelectedItems.Count == 1)
                {
                    string text = Convert.ToString(dataGridUslugi2.SelectedItem);
                    text = text.Replace("Id", "");
                    text = text.Replace("=", "");
                    text = text.Replace(" ", "");
                    text = text.Replace("Name", "");
                    text = text.Replace("Price", "");
                    text = text.Replace("{", "");
                    text = text.Replace("}", "");
                    string[] word = text.Split(new char[] { ',' });
                    int id = Convert.ToInt32(word[0]);
                    Usluga usluga = db.Uslugas.Find(id);
                    db.Uslugas.Remove(usluga);
                    db.SaveChanges();
                    var query = from n in db.Uslugas orderby n.Id select new { n.Id, n.Name, n.Price };
                    dataGridUslugi2.ItemsSource = query.ToList();
                }
                else { MessageBox.Show("Необходимо выделить только одну строку"); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void GoToDetectiv_Click(object sender, RoutedEventArgs e)
        {
            AdminDetec form = new AdminDetec();
            form.Show();
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query = from n in db.Uslugas orderby n.Id select new { n.Id, n.Name, n.Price };
            dataGridUslugi2.ItemsSource = query.ToList();
        }

		private void BtnPoisk_Click(object sender, RoutedEventArgs e)
		{
			var query = from n in db.Uslugas
						where n.Name == PoiskTB.Text
						orderby n.Id
						select new { n.Id, n.Name, n.Price};
			dataGridUslugi2.ItemsSource = query.ToList();
		}
		private void TbPoisk(object sender, RoutedEventArgs e)
		{
			var query = from n in db.Uslugas
						where n.Name.Contains(PoiskTB.Text)
						orderby n.Id
						select new { n.Id, n.Name, n.Price };
			dataGridUslugi2.ItemsSource = query.ToList();
		}
	}
}
