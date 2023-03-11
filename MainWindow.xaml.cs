using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;//подключаем для анимации

namespace UsersApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //ссылка на класс ApplicationContext, создаем объкт DB на основе этого класса
        ApplicationContext db; //ApplicationContext -это тип поля 


        public MainWindow()
        {
            InitializeComponent();/*внутри конструктора мы обращаемся к объекту db он уже подключен к базе данных,
                                  устанавливаем значение-это просто выделение памяти*/
            db = new ApplicationContext(); // ни какие параметры передавать не нужно

            DoubleAnimation btnAnimation = new DoubleAnimation();// создаем для анимации
            btnAnimation.From = 0;
            btnAnimation.To = 450;
            btnAnimation.Duration = TimeSpan.FromSeconds(3);
            regButton.BeginAnimation(Button.WidthProperty, btnAnimation);//на кнопку вешаем анимацию

            ///*код при помощи которого будем получать полностью все записи из нашей таблички
            //создаем список в котором каждый объект на основе класса User, список с yазвание user*/
            //List<User> users = db.Users.ToList(); /*метод ToList позволяет вытянуть полностью все записи и далее
            //                                      все эти записи будут преобразованы к классу List (к формату списка)*/
            //string str = ""; //создаем строковую переменную

            //// в эту переменную будем записывать каждую отдельную запись, которую будем вытягивать из базы данных
            //foreach (User user in users)
            //    str += "Login: " + user.Login + " | "; // обращаемся к str и каждый раз будем добавлять новую запись

            //exempleText.Text = str;

        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string pass = passBox.Password.Trim();
            string pass_2 = passBox_2.Password.Trim();
            string email = textBoxEmail.Text.Trim().ToLower();// ToLower-текст приобразуем в нижний регистр (с маленькой буквы)

            //Проверки длины символов которые вводит пользователь
            if(login.Length < 5)
            {
                textBoxLogin.ToolTip = "Это поле введено не корректно!";
                textBoxLogin.Background = Brushes.DarkRed;
            }
            else if (pass.Length < 5)
            {
                passBox.ToolTip = "Это поле введено не корректно!";
                passBox.Background = Brushes.DarkRed;

            }
            else if (pass != pass_2)
            {
                passBox_2.ToolTip = "Это поле введено не корректно!";
                passBox_2.Background = Brushes.DarkRed;

            }
            else if (email.Length < 5 || !email.Contains("@") || !email.Contains("."))
            {
                textBoxEmail.ToolTip = "Это поле введено не корректно!";
                textBoxEmail.Background = Brushes.DarkRed;

            }
            //если нету ошибок тогда сработает оператор else
            else
            {
                textBoxLogin.ToolTip = ""; //если введено все корректно тогда нету подсказки
                textBoxLogin.Background = Brushes.Transparent;
                passBox.ToolTip = ""; //если введено все корректно тогда нету подсказки
                passBox.Background = Brushes.Transparent;
                passBox_2.ToolTip = ""; //если введено все корректно тогда нету подсказки
                passBox_2.Background = Brushes.Transparent;
                textBoxEmail.ToolTip = ""; //если введено все корректно тогда нету подсказки
                textBoxEmail.Background = Brushes.Transparent;

                //всплывающее окно
                MessageBox.Show("Все хорошо!");

                /*создаем объект на основе класса User и прописываем выделение памяти
                 и передаем параментры такие как login, email, pass*/
                User user = new User(login, email, pass);

                /*обращаемся к базе данных, обращаемся к нашей табличке Users и метод Add добавляем какой-либо объект
                либо новую запись внутрь базы данных*/
                db.Users.Add(user);

                //производим синхронизацию с базой данных (выполняем сохранение этого объекта внутри базы данных db)
                db.SaveChanges();

                /*создаем объект в основе окна к которому хотим перейти и выделяем память*/
                AuthWindow authWindow = new AuthWindow();

                //показываем окно
                authWindow.Show();
                //скрываем текущее окно, Hide - этот метод Делает окно невидимым.
                this.Hide();

            }

        }

        private void Button_Window_Auth_Click(object sender, RoutedEventArgs e) //метод к кнопке "Войти"
        {
            /*создаем объект в основе окна к которому хотим перейти и выделяем память*/
            AuthWindow authWindow = new AuthWindow();

            //показываем окно
            authWindow.Show();
            //скрываем текущее окно, Hide - этот метод Делает окно невидимым.
            this.Hide();
        }
    }
}
