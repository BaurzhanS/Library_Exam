using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Library
{
    public class ServiceBookRent
    {
        public bool RegisterBookRent(BookRent rent, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var rents = db.GetCollection<BookRent>("BookRent");
                    rent.BookStatus = Status.rented;
                    rents.Insert(rent);
                }
                message = string.Format("Зарегистрирована выдача книги {0}, читателю {1}", rent.Book.Name, rent.Book.Name);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public List<BookRent> GetAllBookRents(out string message)
        {
            List<BookRent> bookRents = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var results = db.GetCollection<BookRent>("BookRent");
                    bookRents = results.FindAll().ToList();
                    message = "";
                    return bookRents;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return bookRents;
            }
        }

        public List<BookRent> GetBooksOnRent(out string message)
        {
            List<BookRent> rentedBooks = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var results = db.GetCollection<BookRent>("BookRent");
                    rentedBooks = results.Find(f => f.BookStatus==Status.rented).ToList();
                    if (rentedBooks.Any())
                    {
                        message = "";
                        return rentedBooks;
                    }
                    else
                    {
                        message = string.Format("Выданных книг не имеется");
                        return rentedBooks;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return rentedBooks;
            }
        }

        public List<BookRent> GetBooksReturned(out string message)  
        {
            List<BookRent> returnedBooks = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var results = db.GetCollection<BookRent>("BookRent");
                    returnedBooks = results.Find(f => f.BookStatus == Status.returned).ToList();
                    if (returnedBooks.Any())
                    {
                        message = "";
                        return returnedBooks;
                    }
                    else
                    {
                        message = string.Format("Возвращенных книг не имеется");
                        return returnedBooks;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return returnedBooks;
            }
        }

        public List<BookRent> GetRentedBooksByReaderId(int Id, out string message)
        {
            List<BookRent> rentedBooks = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var results = db.GetCollection<BookRent>("BookRent");
                    rentedBooks = results.Find(f => f.Reader.Id==Id).ToList();
                    if (rentedBooks.Any())
                    {
                        message = "";
                        return rentedBooks;
                    }
                    else
                    {
                        message = string.Format("Читатель с номером {0} не брал книг", Id);
                        return rentedBooks;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return rentedBooks;
            }
        }

        public List<BookRent> GetRentedBooksByBookId(int Id, out string message)
        {
            List<BookRent> rentedBooks = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var results = db.GetCollection<BookRent>("BookRent");
                    rentedBooks = results.Find(f => f.Book.Id == Id).ToList();
                    if (rentedBooks.Any())
                    {
                        message = "";
                        return rentedBooks;
                    }
                    else
                    {
                        message = string.Format("Книгу с номером {0} не брали", Id);
                        return rentedBooks;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return rentedBooks;
            }
        }
    }

    
}
