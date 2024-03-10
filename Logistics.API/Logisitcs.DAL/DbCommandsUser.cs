using Logisitcs.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace Logisitcs.DAL
{
    public static class DbCommandsUser
    {
        #region User

        public static void AddUser(User user)
        {
            using var db = new LogisticsDbContext();
            db.Users.Add(user);
            db.SaveChanges();
        }

        public static IEnumerable<User> GetAllUsers()
        {
            var db = new LogisticsDbContext();
            return db.Users.ToList();
        }

        public static void DeleteUser(string guid)
        {
            using var db = new LogisticsDbContext();
            var article = db.Users.Find(guid);
            if (article == null) return;
            db.Users.Remove(article);
            db.SaveChanges();
        }

        public static void UpdateUser(User user)
        {
            using var db = new LogisticsDbContext();
            db.Users.Update(user);
            db.SaveChanges();
        }

        public static User GetUserByMail(string UserMail)
        {
            using var db = new LogisticsDbContext();
            List<User> userList = db.Users.ToList();
            User user = userList.SingleOrDefault(m => m.UserEmail == UserMail);
            return user;
        }

        public static User GetUser(string guid)
        {
            using var db = new LogisticsDbContext();
            return db.Users.Find(guid);
        }

        #endregion User

        #region UserRole

        public static IEnumerable<UserRole> GetAllUserRoles()
        {
            var db = new LogisticsDbContext();
            return db.UserRoles.ToList();
        }

        public static UserRole GetUserRole(int roleId)
        {
            using var db = new LogisticsDbContext();
            return db.UserRoles.Find(roleId);
        }

        public static int GetUserRoleByName(string role)
        {
            using var db = new LogisticsDbContext();
            return int.Parse(db.UserRoles.Single(x => x.Role == role).RoleId.ToString());
        }

        #endregion UserRole
    }
}