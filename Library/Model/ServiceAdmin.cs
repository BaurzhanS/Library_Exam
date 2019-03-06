using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Library
{
    public class ServiceAdmin
    {
        public bool RegisterAdmin(Administrator admin, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"Library.db"))
                {
                    var admins = db.GetCollection<Administrator>("Administrators");
                    admins.Insert(admin);
                }
                message = string.Format("Администратор {0},{1} добавлен успешно", admin.Surname,admin.Name);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public bool DeleteAdmin(Administrator admin, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"Library.db"))
                {
                    var admins = db.GetCollection<Administrator>("Administrators");
                    admins.Delete(adm => adm.Equals(admin));
                }
                message = string.Format("Администратор {0},{1} удален", admin.Surname, admin.Name);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public Administrator LogOnAdmin(string login,string password, out string message)
        {
            Administrator admin = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"Library.db"))
                {
                    var admins = db.GetCollection<Administrator>("Administrators");
                    var results = admins.Find(f => f.Login == login && f.Password == password);
                    if (results.Any())
                    {
                        message = "";
                        return results.FirstOrDefault();
                    }
                    else
                    {
                        message = "Неправильный логин или пароль!";
                        return admin;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return admin;
            }
        }

        public List<Administrator> GetAdministrators(out string message)
        {
            List<Administrator> admins = null;
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var tmp = db.GetCollection<Administrator>("Administrators");
                    admins = tmp.FindAll().ToList();
                    message = "";
                    return admins;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return admins;
            }
        }

        public bool UpdateAdminPassword(Administrator admin, string password, out string message)
        {
            try
            {
                using (LiteDatabase db = new LiteDatabase(@"library.db"))
                {
                    var adm = db.GetCollection<Administrator>("Administrators");
                    admin.Password = password;
                    adm.Update(admin);
                }
                message = string.Format("Пароль администратора {0} изменен", admin.Id);
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
