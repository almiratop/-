using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Частный_детектив
{
    /// <summary>
    /// Логика взаимодействия для DetectiveSmses.xaml
    /// </summary>
    public partial class DetectiveSmses : Window
    {
        Detective Detective;
        Button ok;
        EntityModelContainer db = new EntityModelContainer();
        Client client;
        string text;
        public DetectiveSmses(Detective detective)
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;
            Detective = detective;
        }
		private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
            Detectiv form = new Detectiv(Detective);
            form.Show();
            this.Hide();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Detectiv form = new Detectiv(Detective);
            form.Show();
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = Detective.Id;
                var query = from n in db.Clients
                            join m in db.Detectives
                            on n.Detective equals m
                            where m.Id == id
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
                    string[] zayavk = text.Split(new char[] { ',' });
                    int dl = zayavk.Length;
                    dl = dl - 1;
                    int sch = 1;

                    while (dl != 0)
                    {
                        id = Convert.ToInt32(zayavk[sch]);
                        client = db.Clients.Find(id);
                        
                        ok = new Button();
                        ok.Style = (Style)Resources["People"];
                        ok.Content = client.Name;
                        ok.Click += new RoutedEventHandler(ok_Click);
                        ok.Name = "But" + Convert.ToString(client.Id);

                        stack.Children.Add(ok);

                        dl = dl - 1;
                        sch = sch + 1;
                    }
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
        void ok_Click(object sender, RoutedEventArgs e)
        {
            int id = Detective.Id;
            string text = (sender as FrameworkElement).Name;
            text = text.Replace("But", "");
            int get = Convert.ToInt32(text);
            client = db.Clients.Find(get);
            DetectivSms form = new DetectivSms(Detective, client);
            form.label.Content = client.Name;
            form.Show();
            this.Hide();
        }
    }
}
