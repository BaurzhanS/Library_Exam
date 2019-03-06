using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Reader: IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Access access { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool BlockedByAdmin { get; set; }
        public Reader()
        {
            BlockedByAdmin = false;
        }
        public List<Book> books;

        public override string ToString()
        {
            return string.Format("{0};{1};{2};{3};{4};{5};{6};{7}", Id, Name, Surname, Login, Password, Address, Phone, Email);
        }
    }
}
