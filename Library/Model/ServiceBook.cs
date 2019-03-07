using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Library
{
    public class ServiceBook
    {
        public bool RegisterBook(Book book, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"Library.db"))
                {
                    var books = db.GetCollection<Book>("Books");
                    books.Insert(book);
                }
                message = string.Format("Книга под №{0}, {1} добавлена успешно", book.ISDN, book.Name);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public bool DeleteBook(Book book, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var books = db.GetCollection<Book>("Books");
                    books.Delete(b => b.Equals(book));
                }
                message = string.Format("Книга под №{0}, {1} удалена", book.ISDN, book.Name);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public Book FindBookById(int id, out string message)
        {
            Book book = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"Library.db"))
                {
                    var books = db.GetCollection<Book>("Books");
                    var results = books.Find(b => b.Id == id);
                    if (results.Any())
                    {
                        message = "";
                        return results.FirstOrDefault();
                    }
                    else
                    {
                        message = "Книга с таким номером не найдена!";
                        return book;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return book;
            }
        }

        public bool UpdateBook(Book book, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"Library.db"))
                {
                    var books = db.GetCollection<Book>("Books");
                    books.Update(book);
                }
                message = string.Format("Книга под №{0}, {1} изменена", book.ISDN, book.Name);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public List<Book> GetBooks(out string message)
        {
            List<Book> books = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"Library.db"))
                {
                    var results = db.GetCollection<Book>("Books");
                    books = results.FindAll().ToList();
                    message = "";
                    return books;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return books;
            }
        }
        public bool UpdateBookStatus(Book book, Status status, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var books = db.GetCollection<Book>("Books");
                    book.status.BookStatus = status;
                    books.Update(book);
                }
                message = string.Format("Статус книги {0} изменен на {1}", book.Id, status);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
    }
}
