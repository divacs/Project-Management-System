using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text; 
using System.Threading.Tasks;

namespace Data
{
    public class DALUserRoles
    {
        private static GetDatabaseEntities db = new GetDatabaseEntities(); 
        // returns user roles
        public static string[] GetUserRoles(string username)
        {
            var idroles = db.Users.Where(x => x.UserName == username).Select(x => x.IDRole).ToList();
            return db.Roles.Where(x => idroles.Contains(x.ID)).Select(x=>x.Name).ToArray();

        }
        // returns list of all roles
        public static List<Role> GetAllRoles()
        { 
            return db.Roles.ToList();

        }

    }
}
