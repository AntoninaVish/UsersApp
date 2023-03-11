using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;// подключаем пространство имен

namespace UsersApp
{
    class ApplicationContext : DbContext //наследование от класса DbContext
    {

        public DbSet<User> Users { get; set; }//название таблицы нашей Users 

        /* внутрь списка Users будут помещены различные значения,
         которые будут вытянуты из самой таблички Users*/
        public ApplicationContext() : base("DefaultConnection") { } /*конструктор ни чего не принимает в качестве параметров,
                                      но зато в родительский конструктор передаем название того подключения
                                      которая работает с локальной нашей базой dataBase*/
    }
    //через этот класс DbContext мы сможем подключаться к базе данных
}
