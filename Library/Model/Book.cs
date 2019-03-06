using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public enum Type { math, fiction, accounting, biography, history, finance, statistics }
    public class Book
    {
        public int Id { get; set; }
        public string ISDN { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public Type BookType { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public int Edition { get; set; }
        public BookRent status { get; set; }

        public override string ToString()
        {
            return string.Format("ID: {0}\tISDN: {1}\tНазвание: {2}\tАвтор: {3}\tДата публикации: {4}\tЖанр: {5}\tСтатус: {6}",
                Id, ISDN, Name, Author, PublishDate, BookType, status);
        }
    }
}
