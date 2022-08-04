using Data;
using ProjectManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagment.Controllers
{

    [Authorize(Roles = "Administrator,Project Manager")]
    public partial class UserController : Controller
    {
        DALUsers dal=new DALUsers();
        public virtual ActionResult UserHome()
        { 
            List<CreateUserModel> model = new List<CreateUserModel>();
            // prolazi kroz sve usere i kreira model za svakog od njih i dodaje u listu
            foreach (var item in dal.GetAll())
            {
                model.Add(new CreateUserModel() { 
                    Active=item.Active,
                    Email=item.Email,
                    IDRole=item.IDRole,
                    Name=item.Name,
                    Password=item.Password,
                    Projects=item.Projects,
                    Role=item.Role,
                    Surname=item.Surname,
                    Tasks=item.Tasks,
                    UserName=item.UserName
                });
            }

            return View(model);
        }
        public virtual ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public virtual ActionResult AddUser(CreateUserModel model)
        {
            // upisuje u bazu kad kliknemo Add User
            if (dal.UserExists(model.UserName) == true) ModelState.AddModelError("UserName", "Username already exists!."); 
            if (ModelState.IsValid)
            {
                try
                {
                    dal.Add(new User()
                    {
                        Email = model.Email!=null?model.Email.Trim():null,
                        IDRole = model.IDRole,
                        Name = model.Name.Trim(),
                        Password = model.Password.Trim(),
                        Surname = model.Surname.Trim(),
                        UserName = model.UserName.Trim(),
                        Active=true
                    }); 
                    return RedirectToAction("UserHome", "User"); 
                     
                }
                catch (Exception)
                {

                     
                }



            }
            return View(model);

        }
        public virtual ActionResult DeleteUser(string username)
        {
            dal.Delete(new User()
            {
                UserName = username
            });
            return RedirectToAction("UserHome", "User");
        }
        public virtual ActionResult EditUser(string username)
        {
            try
            {
                var user = dal.Get(new User() { UserName = username });

                if(user==null) throw new  Exception(); 

                return View(new CreateUserModel()
                {
                    Email = user.Email,
                    Surname = user.Surname,
                    IDRole = user.IDRole,
                    Name = user.Name,
                    Password = user.Password,
                    UserName = user.UserName,
                    Active=user.Active,
                    Roles = DALUserRoles.GetAllRoles()

                });
            }
            catch (Exception e)
            {
                return RedirectToAction("UserHome", "User");
            }

        }
        [HttpPost]
        // kad smo editovali on poksava da unese u bazu promene
        public virtual ActionResult EditUser(CreateUserModel model)
        {
            var user = dal.Get(new User() { UserName = model.UserName });

            if (ModelState.IsValid)
            {
                dal.Update(new User()
                {
                    UserName = model.UserName.Trim(),
                    Surname = model.Surname.Trim(),
                    Email = model.Email!=null?model.Email.Trim():null,
                    IDRole = model.IDRole,
                    Name = model.Name.Trim(),
                    Active=model.Active,
                    Password = model.Password.Trim()
                });
                return RedirectToAction("UserHome", "User");
            }
            return View(new CreateUserModel()
            {
                Email = user.Email,
                Surname = user.Surname,
                IDRole = user.IDRole,
                Name = user.Name,
                Password = user.Password,
                UserName = user.UserName,
                Active=model.Active,
                Roles = DALUserRoles.GetAllRoles()

            });
            return View();


        }
    }
}