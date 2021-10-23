using CashHandler.DBConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashHandler.Classes
{
    public class UsersClass
    {
        public IEnumerable<User> SelectAll()
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();
            IEnumerable<User> User = CashHandler.Users;

            return User;
        }

        public User SelectByID(User user)
        {
            if (checkExists(user))
            {
                CashHandlerEntities CashHandler = new CashHandlerEntities();
                User UserReturned = CashHandler.Users.Single(UserSelect => UserSelect.UserID == user.UserID);
                return UserReturned;
            }
            return null;
        }

        public void Insert(User user)
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();
            CashHandler.Users.Add(user);
            CashHandler.SaveChanges();
        }

        public void Update(User user)
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();
            User u = CashHandler.Users.Single(UserRow => UserRow.UserID == user.UserID);

            u = user;
            CashHandler.SaveChanges();
        }

        public void Delete(User user)
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();

            if (CashHandler.Users.Any(UserRow => UserRow.UserID == user.UserID))
            {
                User u = CashHandler.Users.Single(UserRow => UserRow.UserID == user.UserID);
                CashHandler.Users.Remove(u);
                CashHandler.SaveChanges();
            }
        }

        public void DeleteAll(User user)
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();

            if (CashHandler.Users.Any(UserRow => UserRow.UserID == user.UserID))
            {
                User u = CashHandler.Users.Single(UserRow => UserRow.UserID == user.UserID);
                CashHandler.Users.Remove(u);
                CashHandler.SaveChanges();
            }
        }

        public int getCount()
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();

            return CashHandler.Users.Count();
        }

        public bool checkExists(User user)
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();

            if (CashHandler.Users.Any(UserFound => UserFound.UserID == user.UserID))
            {
                return true;
            }
            return false;
        }
    }
}