using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Частный_детектив
{
    /// <summary>
    /// Логика взаимодействия для AdminDetec.xaml
    /// </summary>
    public partial class AdminDetec : Window
    {
        EntityModelContainer db = new EntityModelContainer();
        string id2;
        public AdminDetec()
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Admin form = new Admin();
            form.Show();
            this.Hide();
        }

		private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Admin form = new Admin();
			form.Show();
			this.Hide();
		}

		private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DetectivRed form = new DetectivRed(new Detective(), new User());
                if (form.ShowDialog() == true)
                {
                    if (form.textBoxName.Text != "" && form.textBoxNumber.Text != "" && form.textBoxMail.Text != "" && form.textBoxLogin.Text != "" && form.textBoxPassword.Text != "")
                    {
                        int test = 0;
                        int us = db.Users.Count();
                        using (EntityModelContainer db = new EntityModelContainer())
                        {
                            foreach (User user in db.Users)
                            {
                                if (user.Login == form.textBoxLogin.Text)
                                {
                                    test = test + 1;
                                    MessageBox.Show("Пользователь с таким логином существует, придумайте другой");
                                    break;
                                }
                            }
                            if (test == 0)
                            {
                                User user = new User();
                                user.Login = form.textBoxLogin.Text;
                                user.Password = form.textBoxPassword.Text;
                                user.Role = "Детектив";
                                db.Users.Add(user);
                                db.SaveChanges();
                                Detective detective = new Detective();
                                detective.Name = form.textBoxName.Text;
                                detective.Number = form.textBoxNumber.Text;
                                detective.Mail = form.textBoxMail.Text;
                                detective.User = user;
                                db.Detectives.Add(detective);
                                db.SaveChanges();

                                string namedoc = detective.Name + ".docx";
                                string pathDocument = AppDomain.CurrentDomain.BaseDirectory + namedoc;
                                DocX document = DocX.Create(pathDocument);
                                document.MarginLeft = 60.0f;
                                document.MarginRight = 60.0f;
                                document.MarginTop = 60.0f;
                                document.MarginBottom = 60.0f;
                                document.InsertParagraph("Трудовой договор с детективом").
                                Bold().
                                Font("Times New Roman").
                                FontSize(14).
                                Alignment = Alignment.center;
                                DateTime thisDay = DateTime.Today;

                                document.InsertParagraph("\n" + "        Организация Детективное агентство «Частный детектив» в лице " +
                                    "Владельца Николая Анатольевича именуемое в дальнейшем 'Организация', и гражданин " + detective.Name +
                                    " именуемый в дальнейшем 'Работник', заключили настоящий   договор о нижеследующем:" + "\n\n" +
                                    "      1. Работник " + detective.Name + " принимается на работу Детективного агентства по должности детектив." + "\n\n" +
                                    "      2. Договор является договором по основной работе." + "\n\n" +
                                    "      3. Вид договора:" + "\n" + "на неопределенный срок (бессрочный)" + "\n\n" +
                                    "      4. Срок действия договора. Начало работы – " + thisDay.ToString("d") + "\n\n" +
                                    "      5. Срок испытания:" + "\n" + "а) без испытания" + "\n\n" +
                                    "      6. Работник должен выполнять следующие обязанности:" + "\n\n" +
                                    "            1. Сбор сведений по гражданским делам на договорной основе с участниками процесса." + "\n\n" +
                                    "            2. Изучение рынка, сбор информации для деловых переговоров, выявление некредитоспособных или ненадежных деловых партнеров." + "\n\n" +
                                    "            3. Установление обстоятельств неправомерного использования в предпринимательской деятельности фирменных знаков и наименований, недобросовестной конкуренции, а также разглашения сведений, составляющих коммерческую тайну." + "\n\n" +
                                    "            4. Выяснение биографических и иных характеризующих личность данных об отдельных гражданах (с их письменного согласия) при заключении ими трудовых и иных контрактов." + "\n\n" +
                                    "            5. Поиск без вести пропавших граждан." + "\n\n" +
                                    "            6. Поиск утраченного гражданами или предприятиями, учреждениями, организациями имущества." + "\n\n" +
                                    "            7. Сбор сведений по уголовным делам на договорной основе с участниками процесса (с обязательным письменным уведомлением в течении суток с момента заключения контракта с клиентом лица, производящего дознание, следователя, прокурора или суда)." + "\n\n" +
                                    "      7. Работнику устанавливается:" + "\n" + "должностной оклад (тарифная ставка) 50000 руб. в месяц и другие выплаты 20 % от каждого заказа." + "\n\n" +
                                    " Организация                                                         Работник" + "\n\n" +
                                    " (Работодатель)                                                     Детектив" + "\n\n" +
                                    " Николай Анатольевич                                         " + detective.Name + "\n\n" +
                                    " Дата заключения: " + thisDay.ToString("d") + "\n\n").
                                Font("Times New Roman").
                                FontSize(14).
                                Alignment = Alignment.left;
                                document.Save();

                                var query = from n in db.Detectives
                                            join m in db.Users
                                            on n.User equals m
                                            orderby n.Id
                                            select new { n.Id, n.Name, n.Number, n.Mail, m.Login, m.Password };
                                dataGridDet.ItemsSource = query.ToList();
                                MessageBox.Show("Новый детектив добавлен");
                            }
                        }
                    }
                    else { MessageBox.Show("Не все поля заполнены"); }
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }


        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridDet.SelectedItems.Count == 1)
                {
                    string text = Convert.ToString(dataGridDet.SelectedItem);
                    text = text.Replace("Id", "");
                    text = text.Replace("=", "");
                    text = text.Replace(" ", "");
                    text = text.Replace("Name", "");
                    text = text.Replace("Number", "");
                    text = text.Replace("Mail", "");
                    text = text.Replace("Login", "");
                    text = text.Replace("Password", "");
                    text = text.Replace("{", "");
                    text = text.Replace("}", "");
                    string[] word = text.Split(new char[] { ',' });
                    int id = Convert.ToInt32(word[0]);
                    var query2 = from n in db.Detectives 
                                 join m in db.Users 
                                 on n.User equals m 
                                 where n.Id == id orderby n.Id 
                                 select m.Id;
                    foreach (var x in query2)
                        id2 = $"{x}";

                    int id3 = Convert.ToInt32(id2);

                    Detective detective = db.Detectives.Find(id);
                    User user = db.Users.Find(id3);
                    DetectivRed form = new DetectivRed(detective, user);
                    form.textBoxName.Text = detective.Name;
                    form.textBoxNumber.Text = detective.Number;
                    form.textBoxMail.Text = detective.Mail;
                    form.textBoxLogin.Text = user.Login;
                    form.textBoxPassword.Text = user.Password;

                    if (form.ShowDialog() == true)
                    {
                        if (form.textBoxName.Text != "" && form.textBoxNumber.Text != "" && form.textBoxMail.Text != "" && form.textBoxLogin.Text != "" && form.textBoxPassword.Text != "")
                        {
                            detective.Name = form.textBoxName.Text;
                            detective.Number = form.textBoxNumber.Text;
                            detective.Mail = form.textBoxMail.Text;
                            user.Login = form.textBoxLogin.Text;
                            user.Password = form.textBoxPassword.Text;
                            db.SaveChanges();
                            var query = from n in db.Detectives
                                        join m in db.Users
                                        on n.User equals m
                                        orderby n.Id
                                        select new { n.Id, n.Name, n.Number, n.Mail, m.Login, m.Password };
                            dataGridDet.ItemsSource = query.ToList();
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

                if (dataGridDet.SelectedItems.Count == 1)
                {
                    string text = Convert.ToString(dataGridDet.SelectedItem);
                    text = text.Replace("Id", "");
                    text = text.Replace("=", "");
                    text = text.Replace(" ", "");
                    text = text.Replace("Name", "");
                    text = text.Replace("Number", "");
                    text = text.Replace("Mail", "");
                    text = text.Replace("Login", "");
                    text = text.Replace("Password", "");
                    text = text.Replace("{", "");
                    text = text.Replace("}", "");
                    string[] word = text.Split(new char[] { ',' });
                    int id = Convert.ToInt32(word[0]);
                    var query2 = from n in db.Detectives join m in db.Users on n.User equals m where n.Id == id orderby n.Id select m.Id;
                    foreach (var x in query2)
                        id2 = $"{x}";

                    int id3 = Convert.ToInt32(id2);

                    Detective detective = db.Detectives.Find(id);
                    User user = db.Users.Find(id3);
                    db.Detectives.Remove(detective);
                    db.Users.Remove(user);
                    db.SaveChanges();
                    var query = from n in db.Detectives
                                join m in db.Users
                                on n.User equals m
                                orderby n.Id
                                select new { n.Id, n.Name, n.Number, n.Mail, m.Login, m.Password };
                    dataGridDet.ItemsSource = query.ToList();
                }
                else { MessageBox.Show("Необходимо выделить только одну строку"); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query = from n in db.Detectives
                        join m in db.Users
                        on n.User equals m
                        orderby n.Id
                        select new { n.Id, n.Name, n.Number, n.Mail, m.Login, m.Password };
            dataGridDet.ItemsSource = query.ToList();
        }
		private void BtnPoisk_Click(object sender, RoutedEventArgs e)
		{
			var query = from n in db.Detectives
						join m in db.Users
						on n.User equals m
						where n.Name == PoiskTB.Text
						orderby n.Id
						select new { n.Id, n.Name, n.Number, n.Mail, m.Login, m.Password }; ;
			dataGridDet.ItemsSource = query.ToList();
		}
		private void TbPoisk(object sender, RoutedEventArgs e)
		{
			var query = from n in db.Detectives
						join m in db.Users
						on n.User equals m
						where n.Name.Contains(PoiskTB.Text)
						orderby n.Id
						select new { n.Id, n.Name, n.Number, n.Mail, m.Login, m.Password }; ;
			dataGridDet.ItemsSource = query.ToList();
		}

	}
}
