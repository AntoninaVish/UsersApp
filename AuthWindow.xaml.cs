﻿using System;
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
using System.Windows.Shapes;

namespace UsersApp
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string pass = passBox.Password.Trim();
           
            //Проверки длины символов которые вводит пользователь
            if (login.Length < 5)
            {
                textBoxLogin.ToolTip = "Это поле введено не корректно!";
                textBoxLogin.Background = Brushes.DarkRed;
            }
            else if (pass.Length < 5)
            {
                passBox.ToolTip = "Это поле введено не корректно!";
                passBox.Background = Brushes.DarkRed;

            }
            else
            {
                textBoxLogin.ToolTip = ""; //если введено все корректно тогда нету подсказки
                textBoxLogin.Background = Brushes.Transparent;
                passBox.ToolTip = ""; //если введено все корректно тогда нету подсказки
                passBox.Background = Brushes.Transparent;

                User authUser = null; /*создаем объект authUser на основе класса User, изначально память не выделяем 
                                       указываем что этот объект будет полностью пустым*/

                using (ApplicationContext db = new ApplicationContext())/*позволяет создать закрытое окружение, внутри которого
                                         выполним код подключения к базе данных и выполним получение самого пользователя*/
                {
                    authUser = db.Users.Where(b => b.Login == login && b.Pass == pass).FirstOrDefault();/*FirstOrDefault()
                                                                                                         этот метод находит первую найденую запись*/
                }

                /*проверяем*/
                if (authUser != null)
                {
                    //всплывающее окно
                    MessageBox.Show("Все хорошо!");
                    UserPageWindow userPageWindow = new UserPageWindow();
                    userPageWindow.Show();
                    Hide();
                }

                else
                    MessageBox.Show("Вы ввели что-то некорректно!");
            }
        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Hide();
        }
    }
}
