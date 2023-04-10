using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Частный_детектив
{
	/// <summary>
	/// Логика взаимодействия для UslugPage.xaml
	/// </summary>
	public partial class UslugPage : Page
	{
		EntityModelContainer db = new EntityModelContainer();
		public UslugPage()
		{
			InitializeComponent();
			db.Uslugas.Load(); // загружаем данные
			dataGridUslugi1.ItemsSource = db.Uslugas.Local.ToBindingList();
		}
		private void BtnPoisk_Click(object sender, RoutedEventArgs e)
		{
			var query = from n in db.Uslugas
						where n.Name == PoiskTB.Text
						orderby n.Id
						select n;
			dataGridUslugi1.ItemsSource = query.ToList();
		}
		private void TbPoisk(object sender, RoutedEventArgs e)
		{
			var query = from n in db.Uslugas
						where n.Name.Contains(PoiskTB.Text)
						orderby n.Id
						select n;
			dataGridUslugi1.ItemsSource = query.ToList();
		}
	}
}
