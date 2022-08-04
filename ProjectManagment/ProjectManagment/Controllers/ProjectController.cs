using Data;
using ProjectManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagment.Controllers
{
   
    // returns all projects
    [Authorize(Roles = "Administrator, Project Manager")]
    public partial class ProjectController : Controller
    {
        // instance of class , we call methods from that class on that instance
        DALProject dal =new DALProject();
        DALTasks dalTask=new DALTasks(); 
        public virtual ActionResult ProjectHome()
            // iz baze uzme sve projekte i dodaje ih u listu 
        {
            List<ProjectModel> model = new List<ProjectModel>();
            foreach (var item in dal.GetAll())
            {
                model.Add(new ProjectModel()
                {
                     Assignee=item.Assignee,
                     Name=item.ProjectName,
                     ProjectCode=item.ProjectCode,
                     Tasks=item.Tasks,
                     User=item.User
                });
            }
            return View(model);
        }

        // returns all Project Managers
        public virtual ActionResult AddProject()
        { 
            ProjectModel allManagers = new ProjectModel();
            allManagers.Users = dal.GetManagers();
            allManagers.Assignee = User.Identity.Name;
            return View(allManagers);
        }

        // this is a post method, which means that it will add something,
        // these others are all GET, which do not have http post
        [HttpPost]
        public virtual ActionResult AddProject(ProjectModel model)
        {
            if (model.Name.Trim() == "") ModelState.AddModelError("Name", "Name is required.");
            if (ModelState.IsValid) {
                dal.Add(new Project(){ProjectName = model.Name.Trim(),Assignee=model.Assignee,Active=true});
                return RedirectToAction("ProjectHome", "Project");
            } 
            return View();
              
        }
        [Authorize(Roles ="Administrator")]
        public virtual ActionResult DeleteProject(string projectCode)
        {
            dal.Delete(new Project() { ProjectCode = Convert.ToInt32(projectCode)});
            return RedirectToAction("ProjectHome", "Project");
        }
        [Authorize(Roles ="Administrator")]
        public virtual ActionResult EditProject(string projectCode)
        {
            try
            {
                var project = dal.Get(new Project() { ProjectCode=Convert.ToInt32(projectCode)});
                if (project == null) { throw new ArgumentException("project is null"); }
                ProjectModel allManagers = new ProjectModel();
                allManagers.Users = dal.GetManagers();

                return View(new ProjectModel(){
                    Name=project.ProjectName,
                    ProjectCode=project.ProjectCode,
                    Assignee=project.Assignee ,
                    Users= allManagers.Users
                });
            }
            catch (Exception e)
            {
                return RedirectToAction("ProjectHome", "Project");
            }

        }
        [HttpPost]
        public virtual ActionResult EditProject(ProjectModel model)
        {
            if (ModelState.IsValid)
            {
                dal.Update(new Project()
                {
                    ProjectCode=model.ProjectCode,
                    ProjectName=model.Name.Trim(),
                    Assignee=model.Assignee,
                    Active=true
                });
                return RedirectToAction("ProjectHome", "Project");
            } 
            return View();

        }
        [Authorize(Roles ="Project Manager")]
        public virtual ActionResult popUpPartial(int id) {

            try
            {
                TempData["ProjectName"] = dal.Get(new Project() { ProjectCode = id }).ProjectName;
                TempData["AvgProgress"] = getProgresStatistics(id).ToString("F");
                TempData["numOfTask"] = dalTask.GetAll().Where(x => x.IDProject == id).Count();
                TempData["numOfTaskPerStatus"] = getTasksPerStatus(id);
                TempData["numOverdueTask"] = dalTask.GetAll().Where(x => x.IDProject == id && x.Deadline < DateTime.Now).Count();
                // the number of tasks due in the next two days
                TempData["numExpireTask"] = dalTask.GetAll().Where(x => x.IDProject == id && x.Deadline > DateTime.Now && x.Deadline < DateTime.Now.AddDays(2)).Count();
                return PartialView(id);
            }
            catch (Exception)
            {

                return RedirectToAction("ProjectHome", "Project");
            }
        }


        #region Statistics
        private double getProgresStatistics(int id) {
            try
            {
                var allTask = dalTask.GetAll().Where(x => x.IDProject == id);
                var avg = allTask.Sum(x => x.Progress) * 1.0 / allTask.Count();
                return allTask.Count()==0 ? (double)0 : (double)avg;
            }
            catch (Exception)
            {

                return (double)0;
            }
           
        }
        private Dictionary<string,int> getTasksPerStatus(int id)
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            var newStatus=dalTask.GetAll().Where(x => x.IDProject == id).Where(x => x.Status.ToLower() == "new").Count();
            var progressStatus=dalTask.GetAll().Where(x => x.IDProject == id).Where(x => x.Status.ToLower() == "in progress").Count();
            var finishedStatus=dalTask.GetAll().Where(x => x.IDProject == id).Where(x => x.Status.ToLower() == "finished").Count();

            data.Add("New", newStatus);
            data.Add("In Progress", progressStatus);
            data.Add("Finished", finishedStatus);

            return data;
        } 
        #endregion
    }
}