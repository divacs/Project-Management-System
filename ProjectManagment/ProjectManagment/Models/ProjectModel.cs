using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManagment.Models
{
    public class ProjectModel
    {
        public ProjectModel()
        {
            this.Tasks = new HashSet<Task>();
        }
        [Key]
        public int ProjectCode { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Project manager is required.")]
        public string Assignee { get; set; }
        public bool Active { get; set; }
        public virtual User User { get; set; }
        public ICollection<User> Users { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
          
    } 
    
}