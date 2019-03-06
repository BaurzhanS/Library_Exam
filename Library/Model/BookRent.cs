using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public enum Status { rented, returned,damaged,overdue }
    public class BookRent
    {
        public int Id { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public Reader Reader { get; set; }
        public Book Book { get; set; }
        public Status BookStatus { get; set; }
    }
}
