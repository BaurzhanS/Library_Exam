using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Library;

namespace WebLibrary
{
    public class Menu
    {
        private Administrator Admin = new Administrator();
        private Reader Reader = new Reader();

        private ServiceAdmin serviceAdmin = new ServiceAdmin();
        private ServiceReaders serviceReader = new ServiceReaders();
        private ServiceBook serviceBook = new ServiceBook();
        private ServiceBookRent serviceBookRent = new ServiceBookRent();

        public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Добро пожаловать в библиотеку!");
                Console.WriteLine("1. Регистрация");
                Console.WriteLine("2. Вход");
                Console.WriteLine("3. Выход");
                int choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                    RegisterMenu();
                else if (choice == 2)
                    LogOnMenu();
                else if (choice == 3)
                    break;
                else
                    continue;
            }
        }

        public void RegisterMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Регистрация пользователей");
                Console.WriteLine();
                Console.WriteLine("1. Читатель");
                Console.WriteLine("2. Администратор");
                Console.WriteLine("3. Выход");
                Console.Write("Выберите пользователя: ");
                int ch = int.Parse(Console.ReadLine());
                if (ch == 1)
                {
                    string message = "";
                    Reader reader = new Reader();
                    Console.WriteLine("Регистрация читателя:");
                    Console.WriteLine();
                    Console.Write("Имя: ");
                    reader.Name = Console.ReadLine();
                    Console.Write("Фамилия: ");
                    reader.Surname = Console.ReadLine();

                    Console.Write("Email: ");
                    reader.Email = Console.ReadLine();

                    Console.Write("Логин: ");
                    reader.Login = Console.ReadLine();

                    Console.Write("Пароль: ");
                    reader.Password = Console.ReadLine();

                    serviceReader.RegisterReader(reader, out message);
                    Console.Clear();
                    Console.WriteLine(message);
                    Thread.Sleep(2000);
                }
                //RegisterReaderMenu();
                else if (ch == 2)
                {
                    string message = "";
                    Administrator admin = new Administrator();
                    Console.WriteLine("Регистрация администратора:");
                    Console.WriteLine();
                    Console.Write("Имя: ");
                    admin.Name = Console.ReadLine();
                    Console.Write("Фамилия: ");
                    admin.Surname = Console.ReadLine();
                    Console.Write("Логин: ");
                    admin.Login = Console.ReadLine();
                    Console.Write("Пароль: ");
                    admin.Password = Console.ReadLine();

                    serviceAdmin.RegisterAdmin(admin, out message);
                    Console.Clear();
                    Console.WriteLine(message);
                    Thread.Sleep(1000);
                }
                //RegisterAdministratorMenu();

                else if (ch == 3)
                    break;
                else
                    continue;
            }
        }

        public void LogOnMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Вход:");
                Console.WriteLine();
                Console.WriteLine("1. Читатель");
                Console.WriteLine("2. Администратор");
                Console.WriteLine("3. Назад");
                Console.Write("Выберите пользователя: ");
                int choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    string message = "";
                    Console.Clear();
                    Console.WriteLine("Вход читателя:");
                    Console.WriteLine("---------------------------------------------\n");
                    Console.Write("Логин: ");
                    string login = Console.ReadLine();
                    Console.Write("Пароль: ");
                    string password = Console.ReadLine();
                    Reader reader = serviceReader.LogOnReader(login, password, out message);
                    Reader = reader;
                }
                else if (choice == 2)
                {
                    string message = "";
                    Console.Clear();
                    Console.WriteLine("Вход администратора:");
                    Console.WriteLine("---------------------------------------------\n");
                    Console.Write("Логин: ");
                    string login = Console.ReadLine();
                    Console.Write("Пароль: ");
                    string password = Console.ReadLine();
                    Administrator admin = serviceAdmin.LogOnAdmin(login, password, out message);

                }
                else if (choice == 3)
                    break;
                else
                    continue;
            }
        }

        public void ReaderMenu()
        {
            Book book = null;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Читатель: ");
                Console.WriteLine();
                Console.WriteLine("1. Найти книгу");
                Console.WriteLine("2. Изменить пароль");
                Console.WriteLine("3. Выход");
                Console.Write("Выберите: ");
                int choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    while (true)
                    {
                        string message = "";
                        Console.Clear();
                        Console.WriteLine("Поиск книги");
                        Console.WriteLine();
                        Console.WriteLine("1. Поиск по номеру");
                        Console.WriteLine("2. Выход");
                        Console.Write("Выберите: ");
                        int choice1 = int.Parse(Console.ReadLine());
                        if (choice1 == 2)
                            break;
                        else if (choice1 == 1)
                        {
                            Console.WriteLine();
                            Console.Write("Введите номер: ");
                            int id = int.Parse(Console.ReadLine());
                            book = serviceBook.FindBookById(id, out message);
                            break;
                        }
                        else
                            continue;
                    }

                }
                else if (choice == 2)
                {
                    string message = "";
                    Console.Clear();
                    Console.WriteLine("Изменение пароля");
                    Console.WriteLine();
                    Console.Write("Новый пароль: ");
                    string password = Console.ReadLine();

                    serviceReader.UpdateReaderPassword(Reader, password, out message);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(message);
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(2000);
                }
                else if (choice == 3)
                    break;
                else
                    continue;
            }
        }

        public void AdministratorMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Администратор:");
                Console.WriteLine();
                Console.WriteLine("1. Заблокировать читателя");
                Console.WriteLine("2. Вывести все книги на руках у читателей");
                Console.WriteLine("3. Вывести все книги возвращенные пользователем");
                Console.WriteLine("4. Изменить пароль");
                Console.WriteLine("5. Выход");
                Console.Write("Выберите: ");
                int ch = Int32.Parse(Console.ReadLine());
                if (ch == 1)
                {
                    Console.WriteLine("Введите номер читателя: ");
                    int readerId = int.Parse(Console.ReadLine());
                    string message = "";
                    Reader=serviceReader.FindReaderById(readerId,out message);
                }
                else if (ch == 2)
                {

                }
                else if (ch == 3)
                {

                }
                else if (ch == 4)
                {

                }
                else if (ch == 5)
                    break;
                else
                    continue;
            }
        }

    }






}
}
