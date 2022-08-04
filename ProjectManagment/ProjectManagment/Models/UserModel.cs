using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectManagment.Models
{
    public class UserModel
    {
        [Key]
        [Required(ErrorMessage = "UserName is required.")] 
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
    }
     
    public class CreateUserModel {
        public CreateUserModel()
        {
            this.Projects = new HashSet<Project>();
            this.Tasks = new HashSet<Task>();
        }

        [Key]
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Rola is required.")]
        public int IDRole { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
        public bool Active { get; set; }

        [ForeignKey("IDRole")]
        public virtual Role Role { get; set; } 
        public ICollection<Role> Roles { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }

        public Role GetRole(int id) {
            return DALUserRoles.GetAllRoles().FirstOrDefault(x => x.ID == id);
        }
    }
}