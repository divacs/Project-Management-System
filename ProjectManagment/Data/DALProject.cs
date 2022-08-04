using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    
    public class  DALProject:DAL<Project>
    {
        private static GetDatabaseEntities db = new GetDatabaseEntities();

        // returns all managers from database that are active
        public List<User> GetManagers() { 
            return db.Users.Where(x => x.IDRole == 2 && x.Active==true).ToList();
        }
        // adds a new project to the database
        public void Add(Project project)
        {
            db.Projects.Add(project); 
            db.SaveChanges();
        }
        // deletes project from database
        // FirstorDefault() method returns the first element of a sequence or a default value if element isn't there
        public void Delete(Project project)
        {
            var searchProject = db.Projects.FirstOrDefault(x => x.ProjectCode == project.ProjectCode && x.Active==true);
            searchProject.Active = false;
            db.SaveChanges();
        }
        // returns a project with some project code
        public Project Get(Project t)
        {
            return db.Projects.FirstOrDefault(x => x.ProjectCode == t.ProjectCode && x.Active==true);
        }

        // returns all projects
        public List<Project> GetAll()
        {
            return db.Projects.Where(x=>x.Active==true).ToList();
        }

        // updates project
        public void Update(Project t)
        {
            var search = db.Projects.FirstOrDefault(x => x.ProjectCode == t.ProjectCode && x.Active==true);
            search.ProjectName = t.ProjectName;
            search.Assignee = t.Assignee;  
            db.SaveChanges();
        }
    }
}
