using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Library
{
    public class ServiceReaders
    {
        public bool RegisterReader(Reader reader, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"Library.db"))
                {
                    var books = db.GetCollection<Reader>("Readers");
                    books.Insert(reader);
                }
                message = string.Format("Читатель {0} {1} успешно добавлен", reader.Surname, reader.Name);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public bool DeleteReader(Reader reader, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var readers = db.GetCollection<Reader>("Readers");
                    readers.Delete(d => d.Equals(reader));
                }
                message = string.Format("Читатель {0} {1} удален", reader.Surname, reader.Name);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public Reader LogOnReader(string login, string password, out string message)
        {
            Reader reader = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var readers = db.GetCollection<Reader>("Readers");
                    var results = readers.Find(f => f.Login==login && f.Password==password);
                    if (results.Any())
                    {
                        message = "";
                        return results.FirstOrDefault();
                    }
                    else
                    {
                        message = "Неправильный логин или пароль";
                        return reader;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return reader;
            }
        }

        public Reader FindReaderById(int id, out string message)
        {
            Reader reader = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var readers = db.GetCollection<Reader>("Readers");
                    var results = readers.Find(f => f.Id==id);
                    if (results.Any())
                    {
                        message = "";
                        return results.FirstOrDefault();
                    }
                    else
                    {
                        message = "Читателя с таким номером нет";
                        return reader;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return reader;
            }
        }

        public bool UpdateReaderPassword(Reader reader, string password, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var readers = db.GetCollection<Reader>("Readers");
                    reader.Password = password;
                    readers.Update(reader);
                }
                message = string.Format("Пароль читателя {0} изменен", reader.Surname,reader.Name);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public bool UpdateReaderAddress(Reader reader, string address, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var readers = db.GetCollection<Reader>("Readers");
                    reader.Address = address;
                    readers.Update(reader);
                }
                message = string.Format("Адрес читателя {0} изменен на {1}", reader.Id, address);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public bool UpdateReaderPhone(Reader reader, string phone, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var readers = db.GetCollection<Reader>("Readers");
                    reader.Phone = phone;
                    readers.Update(reader);
                }
                message = string.Format("Телефон читателя {0} изменен на {1}", reader.Id, phone);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public bool UpdateReaderEmail(Reader reader, string email, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var readers = db.GetCollection<Reader>("Readers");
                    reader.Email = email;
                    readers.Update(reader);
                }
                message = string.Format("Email читателя {0} изменен на {1}", reader.Id, email);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public bool UpdateReaderName(Reader reader, string name, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var readers = db.GetCollection<Reader>("Readers");
                    reader.Name = name;
                    readers.Update(reader);
                }
                message = string.Format("Имя читателя {0} изменено на {1}", reader.Id, name);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public bool UpdateReaderSurname(Reader reader, string surname, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var readers = db.GetCollection<Reader>("Readers");
                    reader.Surname = surname;
                    readers.Update(reader);
                }
                message = string.Format("Фамилия читателя {0} изменена на {1}", reader.Id, surname);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public bool UpdateReaderStatus(Reader reader, bool Isblocked, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var readers = db.GetCollection<Reader>("Readers");
                    reader.BlockedByAdmin = Isblocked;
                    readers.Update(reader);
                }
                message = string.Format("Доступ читателя {0} изменен на {1}", reader.Id, reader.BlockedByAdmin);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public List<Reader> GetReaders(out string message)
        {
            List<Reader> readers = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var tmp = db.GetCollection<Reader>("Readers");
                    readers = tmp.FindAll().ToList();
                    message = "";
                    return readers;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return readers;
            }
        }
    }
}
