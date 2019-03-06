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
        //private ServiceBookType serviceBookType = new ServiceBookType();
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
                Console.Write("Ваш выбор: ");
                int ch = int.Parse(Console.ReadLine());
                if (ch == 1)
                    RegisterMenu();
                else if (ch == 2)
                    LogOnMenu();
                else if (ch == 3)
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
                Console.WriteLine("Меню регистрации");
                Console.WriteLine("---------------------------------------------\n");
                Console.WriteLine("1. Читатель");
                Console.WriteLine("2. Администратор");
                Console.WriteLine("3. Выход");
                Console.Write("Ваш выбор: ");
                int ch = Int32.Parse(Console.ReadLine());
                if (ch == 1)
                    RegisterReaderMenu();
                else if (ch == 2)
                    RegisterAdministratorMenu();
                else if (ch == 3)
                    break;
                else
                    continue;
            }
        }//MainMenu

        public void LogOnMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Меню входа в систему");
                Console.WriteLine("---------------------------------------------\n");
                Console.WriteLine("1. Читатель");
                Console.WriteLine("2. Администратор");
                Console.WriteLine("3. Выход");
                Console.Write("Ваш выбор: ");
                int ch = Int32.Parse(Console.ReadLine());
                if (ch == 1)
                    LogOnReaderMenu();
                else if (ch == 2)
                    LogOnAdministratorMenu();
                else if (ch == 3)
                    break;
                else
                    continue;
            }
        }//MainMenu

        public void RegisterReaderMenu()
        {
            string msg = "";
            Reader reader = new Reader();
            Console.WriteLine("Форма регистрации для читателя:");
            Console.WriteLine("---------------------------------------------\n");
            Console.Write("Имя: ");
            reader.Name = Console.ReadLine();
            Console.Write("Фамилия: ");
            reader.Surname = Console.ReadLine();
            while (true)
            {
                Console.Write("Email: ");
                string tmp = Console.ReadLine();
                if ((!tmp.Contains("@") && (!tmp.Contains(".kz") || !tmp.Contains(".ru") || !tmp.Contains(".com") || !tmp.Contains(".org"))) || (!tmp.Contains("@") || tmp[0] == '@'))
                    continue;
                else
                {
                    reader.Email = tmp;
                    break;
                }
            }
            while (true)
            {
                Console.Write("Логин: ");
                string tmp = Console.ReadLine();
                var readersList = serviceReader.GetReaders(out msg);
                if (readersList.Find(f => f.Login.Equals(tmp)) != null)
                {
                    Console.WriteLine("Читатель с таким логином уже существует");
                    continue;
                }
                else
                {
                    reader.Login = tmp;
                    break;
                }
            }
            Console.Write("Пароль: ");
            reader.Password = Console.ReadLine();

            serviceReader.RegisterReader(reader, out msg);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(1000);
        }//RegisterMenu

        public void RegisterAdministratorMenu()
        {
            string msg = "";
            Administrator admin = new Administrator();
            Console.WriteLine("Форма регистрации для администратора:");
            Console.WriteLine("---------------------------------------------\n");
            Console.Write("Имя: ");
            admin.Name = Console.ReadLine();
            Console.Write("Фамилия: ");
            admin.Surname = Console.ReadLine();
            //while (true)
            //{
            //    Console.Write("Email: ");
            //    string tmp = Console.ReadLine();
            //    if ((!tmp.Contains("@") && (!tmp.Contains(".kz") || !tmp.Contains(".ru") || !tmp.Contains(".com") || !tmp.Contains(".org"))) || (!tmp.Contains("@") || tmp[0] == '@'))
            //        continue;
            //    else
            //    {
            //        admin.Email = tmp;
            //        break;
            //    }
            //}
            while (true)
            {
                Console.Write("Логин: ");
                string tmp = Console.ReadLine();
                var adminsList = serviceAdmin.GetAdministrators(out msg);
                if (adminsList.Find(f => f.Login.Equals(tmp)) != null)
                {
                    Console.WriteLine("Администратор с таким логином уже существует");
                    continue;
                }
                else
                {
                    admin.Login = tmp;
                    break;
                }
            }
            Console.Write("Пароль: ");
            admin.Password = Console.ReadLine();

            serviceAdmin.RegisterAdmin(admin, out msg);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(1000);
        }//RegisterMenu

        public void LogOnReaderMenu()
        {
            int k = 1;
            while (k != 4)
            {
                string msg = "";
                Console.Clear();
                Console.WriteLine("Проверка входа для читателя:");
                Console.WriteLine("---------------------------------------------\n");
                Console.Write("Логин: ");
                string login = Console.ReadLine();
                Console.Write("Пароль: ");
                string password = Console.ReadLine();
                Reader reader = serviceReader.LogOnReader(login, password, out msg);
                if (reader != null && reader.BlockedByAdmin == false)
                {
                    Reader = reader;
                    List<BookRent> tmp = serviceBookRent.GetRentedBooksByReaderId(Reader.Id, out msg);
                    foreach (BookRent i in tmp.Where(w => w.Book.status.Equals(Status.rented)))
                    {
                        Reader.books.Add(i.Book);
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(msg);
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1000);
                    ReaderMenu();
                    break;
                }
                else if (reader != null && reader.BlockedByAdmin == true)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ваш профиль заблокирован. Обратитесь к администрации");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(2000);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(msg);
                    Console.WriteLine("Попытка {0}", k);
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1000);
                }
                k++;
            }
        }//LogOnMenu

        public void LogOnAdministratorMenu()
        {
            int k = 1;
            while (k != 4)
            {
                string msg = "";
                Console.Clear();
                Console.WriteLine("Проверка входа для администратора:");
                Console.WriteLine("---------------------------------------------\n");
                Console.Write("Логин: ");
                string login = Console.ReadLine();
                Console.Write("Пароль: ");
                string password = Console.ReadLine();
                Administrator admin = serviceAdmin.LogOnAdmin(login, password, out msg);
                if (admin != null)
                {
                    Admin = admin;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(msg);
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1000);
                    AdministratorMenu();
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(msg);
                    Console.WriteLine("Попытка {0}", k);
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1000);
                }
                k++;
            }
        }//LogOnMenu

        public void ReaderMenu()
        {
            Book book = null;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Меню читателя");
                Console.WriteLine("---------------------------------------------\n");
                Console.WriteLine("1. Найти книгу");
                Console.WriteLine("2. Вернуть книгу");
                Console.WriteLine("3. Изменить пароль");
                Console.WriteLine("4. Выход");
                Console.Write("Ваш выбор: ");
                int ch = Int32.Parse(Console.ReadLine());
                if (ch == 1)
                {
                    FindBook(book);
                    IssueBook(book);
                }
                else if (ch == 2)
                    ReturnBook();
                else if (ch == 3)
                    ChangeReaderPassword();
                else if (ch == 4)
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
                Console.WriteLine("Меню администратора");
                Console.WriteLine("---------------------------------------------\n");
                Console.WriteLine("1. Читатели");
                Console.WriteLine("2. Книжный фонд");
                Console.WriteLine("3. Отчеты");
                Console.WriteLine("4. Изменить пароль");
                Console.WriteLine("5. Выход");
                Console.Write("Ваш выбор: ");
                int ch = Int32.Parse(Console.ReadLine());
                if (ch == 1)
                    CreateOrChangeReader();
                else if (ch == 2)
                    CreateOrChangeBook();
                else if (ch == 3)
                    ReportsMenu();
                else if (ch == 4)
                    ChangeAdministratorPassword();
                else if (ch == 4)
                    break;
                else
                    continue;
            }
        }

        public void FindBook(Book book)
        {
            while (true)
            {
                string msg = "";
                Console.Clear();
                Console.WriteLine("Поиск книги");
                Console.WriteLine("---------------------------------------------\n");
                Console.WriteLine("1. По ID");
                //Console.WriteLine("2. По ISDN");
                //Console.WriteLine("3. По названию и автору");
                Console.WriteLine("4. Выход");
                Console.Write("Ваш выбор: ");
                int ch = Int32.Parse(Console.ReadLine());
                if (ch == 4)
                    break;
                else if (ch == 1)
                {
                    Console.WriteLine("---------------------------------------------\n");
                    Console.Write("Введите ID: ");
                    int id = Int32.Parse(Console.ReadLine());
                    book = serviceBook.FindBookById(id, out msg);
                    break;
                }
                //else if (ch == 2)
                //{
                //    Console.WriteLine("---------------------------------------------\n");
                //    Console.Write("Введите ISDN: ");
                //    string isdn = Console.ReadLine();
                //    book = serviceBook.FindBookByISDN(isdn, out msg);
                //    break;
                //}
                //else if (ch == 3)
                //{
                //    Console.WriteLine("---------------------------------------------\n");
                //    Console.Write("Введите название: ");
                //    string name = Console.ReadLine();
                //    Console.Write("Введите автора: ");
                //    string author = Console.ReadLine();
                //    book = serviceBook.FindBookByNameAuthor(name, author, out msg);
                //    break;
                //}
                else
                    continue;
            }
        }//ReaderMenu

        public void IssueBook(Book book)
        {
            string msg = "";
            if (book != null)
            {
                Console.Clear();
                Console.WriteLine("Найдено");
                Console.WriteLine("---------------------------------------------\n");
                Console.WriteLine(book.ToString());
                Console.WriteLine("Заказать книгу?");
                Console.WriteLine("1. да");
                Console.WriteLine("2. нет");
                Console.Write("Ваш ответ: ");
                int ans = Int32.Parse(Console.ReadLine());
                if (ans == 1)
                {
                    if (book.status.BookStatus != Status.rented)
                    {
                        Reader.books.Add(book);
                        serviceBook.UpdateBookStatus(book, Status.rented, out msg);
                        BookRent rent = new BookRent();
                        rent.IssueDate = DateTime.Now;
                        rent.Reader = Reader;
                        rent.Book = book;
                        //rent.TransactionType = TransactionType.issueBook;
                        serviceBookRent.RegisterBookRent(rent, out msg);
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Книга добавлена в ваш лист");
                        Console.ForegroundColor = ConsoleColor.White;
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Книга сейчас занята. Пожалуста, выберите другую книгу");
                        Console.ForegroundColor = ConsoleColor.White;
                        Thread.Sleep(2000);
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Книга с такими параметрами не найдена");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(2000);
            }
        }//ReaderMenu

        public void ReturnBook()
        {
            string msg = "";
            Book book = null;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Список Ваших книг\n");
                Console.WriteLine("ID:\tISDN:\tНазвание:\tАвтор:\tДата публикации:\tЖанр:\tСтатус:");
                Console.WriteLine("---------------------------------------------\n");
                foreach (Book i in Reader.books)
                {
                    Console.WriteLine(i.ToString());
                }
                Console.Write("Введите ID книги, которую необходимо вернуть: ");
                int id = Int32.Parse(Console.ReadLine());
                book = serviceBook.FindBookById(id, out msg);
                if (book != null)
                {
                    Reader.books.Remove(book);
                    serviceBook.UpdateBookStatus(book, Status.returned, out msg);
                    BookRent rent = new BookRent();
                    rent.IssueDate = DateTime.Now;
                    rent.Reader = Reader;
                    rent.Book = book;
                    rent.BookStatus = Status.returned;
                    serviceBookRent.RegisterBookRent(rent, out msg);

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Книга возвращена");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Вернуть еще одну книгу?");
                    Console.WriteLine("1. да");
                    Console.WriteLine("2. нет");
                    Console.Write("Ваш ответ:");
                    int ans = Int32.Parse(Console.ReadLine());
                    if (ans == 1)
                        continue;
                    else
                        break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(msg);
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(2000);
                }
            }
        }//ReaderMenu

        public void ChangeReaderPassword()
        {
            int k = 1;
            while (k != 4)
            {
                string msg = "";
                Console.Clear();
                Console.WriteLine("Изменение пароля");
                Console.WriteLine("---------------------------------------------\n");
                Console.Write("Новый пароль: ");
                string pass1 = Console.ReadLine();
                Console.Write("Повторите еще раз новый пароль: ");
                string pass2 = Console.ReadLine();
                if (pass1.Equals(pass2))
                {
                    serviceReader.UpdateReaderPassword(Reader, pass1, out msg);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(msg);
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(2000);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Пароли не сходятся, введите заново");
                    Console.WriteLine("Попытка {0}", k);
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(2000);
                }
                k++;
            }
        }//ReaderMenu

        public void CreateOrChangeReader()
        {
            Reader reader = new Reader();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Создать/редактировать профиль читателя");
                Console.WriteLine("---------------------------------------------\n");
                Console.WriteLine("1. Зарегистрировать читателя");
                Console.WriteLine("2. Редактировать профиль читателя");
                Console.WriteLine("3. Выход");
                Console.Write("Ваш выбор: ");
                int ch = Int32.Parse(Console.ReadLine());
                if (ch == 1)
                    RegisterReaderMenu();
                else if (ch == 2)
                {
                    FindReader(reader);
                    UpdateReader(reader);
                }
                else if (ch == 3)
                    break;
                else
                    continue;
            }
        }//AdministratorMenu

        public void CreateOrChangeBook()
        {
            Book book = new Book();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Создать/редактировать книжный фонд");
                Console.WriteLine("---------------------------------------------\n");
                Console.WriteLine("1. Зарегистрировать книгу");
                Console.WriteLine("2. Редактировать профиль книги");
                Console.WriteLine("3. Добавить новый жанр");
                Console.WriteLine("4. Выход");
                Console.Write("Ваш выбор: ");
                int ch = Int32.Parse(Console.ReadLine());
                if (ch == 1)
                    RegisterBookMenu();
                else if (ch == 2)
                {
                    FindBook(book);
                    UpdateBook(book);
                }
                else if (ch == 3)
                    Console.WriteLine();
                //AddNewBookType();
                else if (ch == 4)
                    break;
                else
                    continue;
            }
        }//AdministratorMenu

        public void ReportsMenu()
        {

        }//AdministratorMenu

        public void ChangeAdministratorPassword()
        {
            int k = 1;
            while (k != 4)
            {
                string msg = "";
                Console.Clear();
                Console.WriteLine("Изменение пароля");
                Console.WriteLine("---------------------------------------------\n");
                Console.Write("Новый пароль: ");
                string pass1 = Console.ReadLine();
                Console.Write("Повторите еще раз новый пароль: ");
                string pass2 = Console.ReadLine();
                if (pass1.Equals(pass2))
                {
                    serviceAdmin.UpdateAdminPassword(Admin, pass1, out msg);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(msg);
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(2000);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Пароли не сходятся, введите заново");
                    Console.WriteLine("Попытка {0}", k);
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(2000);
                }
                k++;
            }
        }//AdministratorMenu

        public void FindReader(Reader reader)
        {
            while (true)
            {
                string msg = "";
                Console.Clear();
                Console.WriteLine("Поиск читателя");
                Console.WriteLine("---------------------------------------------\n");
                Console.WriteLine("1. По ID");
                //Console.WriteLine("2. По имени и фамилии");
                Console.WriteLine("3. Выход");
                Console.Write("Ваш выбор: ");
                int ch = Int32.Parse(Console.ReadLine());
                if (ch == 3)
                    break;
                else if (ch == 1)
                {
                    Console.WriteLine("---------------------------------------------\n");
                    Console.Write("Введите ID: ");
                    int id = Int32.Parse(Console.ReadLine());
                    reader = serviceReader.FindReaderById(id, out msg);
                    break;
                }
                //else if (ch == 2)
                //{
                //    Console.WriteLine("---------------------------------------------\n");
                //    Console.Write("Введите имя: ");
                //    string name = Console.ReadLine();
                //    Console.Write("Введите фамилию: ");
                //    string surname = Console.ReadLine();
                //    reader = serviceReader.FindReaderByNameSurname(name, surname, out msg);
                //    break;
                //}
                else
                    continue;
            }
        }//CreateOrChangeReader

        public void UpdateReader(Reader reader)
        {
            string msg = "";
            if (reader != null)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Редактировать профиль читателя");
                    Console.WriteLine("---------------------------------------------\n");
                    Console.WriteLine("1. Имя");
                    Console.WriteLine("2. Фамилия");
                    Console.WriteLine("3. Email");
                    Console.WriteLine("4. Адрес");
                    Console.WriteLine("5. Телефон");
                    Console.WriteLine("6. Пароль");
                    Console.WriteLine("7. Доступ");
                    Console.WriteLine("8. Выход");
                    Console.Write("Ваш выбор: ");
                    int ch = Int32.Parse(Console.ReadLine());
                    Console.Clear();
                    if (ch == 1)
                    {
                        Console.Write("Новое имя читателя:");
                        string name = Console.ReadLine();
                        serviceReader.UpdateReaderName(reader, name, out msg);
                    }
                    else if (ch == 2)
                    {
                        Console.Write("Новая фамилия читателя:");
                        string surname = Console.ReadLine();
                        serviceReader.UpdateReaderSurname(reader, surname, out msg);
                    }
                    else if (ch == 3)
                    {
                        while (true)
                        {
                            Console.Write("Новый email читателя:");
                            string tmp = Console.ReadLine();
                            if ((!tmp.Contains("@") && (!tmp.Contains(".kz") || !tmp.Contains(".ru") || !tmp.Contains(".com") || !tmp.Contains(".org"))) || (!tmp.Contains("@") || tmp[0] == '@'))
                                continue;
                            else
                            {
                                serviceReader.UpdateReaderEmail(reader, tmp, out msg);
                                break;
                            }
                        }
                    }
                    else if (ch == 4)
                    {
                        Console.Write("Новый адрес читателя:");
                        string address = Console.ReadLine();
                        serviceReader.UpdateReaderAddress(reader, address, out msg);
                    }
                    else if (ch == 5)
                    {
                        Console.Write("Новый телефон читателя:");
                        string tel = Console.ReadLine();
                        serviceReader.UpdateReaderPhone(reader, tel, out msg);
                    }
                    else if (ch == 6)
                    {
                        Console.Write("Новый пароль читателя:");
                        string pass = Console.ReadLine();
                        serviceReader.UpdateReaderPassword(reader, pass, out msg);
                    }
                    else if (ch == 7)
                    {
                        Console.Write("Доступ читателя (0-свободный, 1-заблокировать):");
                        bool status = Convert.ToBoolean(Int32.Parse(Console.ReadLine()));
                        serviceReader.UpdateReaderStatus(reader, status, out msg);
                    }
                    else if (ch == 8)
                        break;
                    else
                        continue;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(msg);
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(2000);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Читатель с такими параметрами не найден");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(2000);
            }
        }//CreateOrChangeReader

        public void RegisterBookMenu()
        {
            string msg = "";
            Book book = new Book();
            Console.WriteLine("Форма регистрации книги:");
            Console.WriteLine("---------------------------------------------\n");
            Console.Write("Название: ");
            book.Name = Console.ReadLine();
            Console.Write("Автор: ");
            book.Author = Console.ReadLine();
            while (true)
            {
                Console.Write("ISDN: ");
                string isdn = Console.ReadLine();
                var tmp = serviceBook.FindBookByISDN(isdn, out msg);
                if (tmp == null)
                {
                    book.ISDN = isdn;
                    break;
                }
                else
                {
                    Console.WriteLine("Книга с таким ISDN в базе уже есть");
                    continue;
                }
            }

            //while (true)
            //{
            //    Console.Write("Год публикации: ");
            //    int tmp = Int32.Parse(Console.ReadLine());
            //    if (tmp <= 1760 && tmp > DateTime.Now.Year)
            //        continue;
            //    else
            //    {
            //        book.PublishDate = tmp;
            //        break;
            //    }
            //}
            Console.Write("Редакция: ");
            book.Edition = Int32.Parse(Console.ReadLine());
            Console.Write("Внутренний код: ");
            book.Code = int.Parse(Console.ReadLine());
            //var bookTypes = serviceBookType.GetBookTypes(out msg);
            //Console.WriteLine("Жанр: ");
            //foreach (BookType i in bookTypes)
            //{
            //    Console.WriteLine("{0}. {1}", i.Id, i.Name);
            //}
            //while (true)
            //{
            //    Console.Write("Ваш выбор жанра: ");
            //    int t = Int32.Parse(Console.ReadLine());
            //    if (bookTypes.Exists(e => e.Id.Equals(t)))
            //    {
            //        book.BookType = bookTypes.Find(f => f.Id.Equals(t));
            //        break;
            //    }
            //    else
            //        continue;
            //}

            serviceBook.RegisterBook(book, out msg);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(1000);
        }//CreateOrChangeBook

        public void UpdateBook(Book book)
        {
            string msg = "";
            if (book != null)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Редактировать профиль книги");
                    Console.WriteLine("---------------------------------------------\n");
                    Console.WriteLine("1. Внутренний код");
                    Console.WriteLine("2. Жанр");
                    Console.WriteLine("3. Выход");
                    Console.Write("Ваш выбор: ");
                    int ch = Int32.Parse(Console.ReadLine());
                    Console.Clear();
                    if (ch == 1)
                    {
                        Console.Write("Новый внутренний код книги:");
                        int code = int.Parse(Console.ReadLine());
                        serviceBook.UpdateBookCode(book, code, out msg);
                    }
                    //else if (ch == 2)
                    //{
                    //    Console.WriteLine("Новый жанр книги:");
                    //    var tmp = serviceBookType.GetBookTypes(out msg);
                    //    foreach (BookType i in tmp)
                    //    {
                    //        Console.WriteLine("{0}. {1}", i.Id, i.Name);
                    //    }
                    //    while (true)
                    //    {
                    //        Console.Write("Ваш выбор жанра:");
                    //        int btype = Int32.Parse(Console.ReadLine());
                    //        if (tmp.Exists(e => e.Id.Equals(btype)))
                    //        {
                    //            BookType bt = tmp.Find(f => f.Id.Equals(btype));
                    //            serviceBook.UpdateBookBookType(book, bt, out msg);
                    //            break;
                    //        }
                    //        else
                    //            continue;
                    //    }
                    //}
                    else if (ch == 3)
                        break;
                    else
                        continue;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(msg);
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(2000);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Читатель с такими параметрами не найден");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(2000);
            }
        }//CreateOrChangeBook
    }
}
