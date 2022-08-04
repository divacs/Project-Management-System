using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManagment.Models
{
    // task create model doesnt have required status and progress because when add task it set automaticaly
    public class TaskModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Assignee { get; set; }
        [Required]
        public string Status { get; set; }
        public int Progress { get; set; }
        // it must have a deadline
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public System.DateTime Deadline { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int IDProject { get; set; }
        public Project Project{ get; set; }
        public User User{ get; set; }
        public virtual List<Project> Projects { get; set; }
        public virtual List<User> Users { get; set; }
        public List<string> Statuses { get; set; } 
    }
    public class TaskCreateModel
    {
        public int ID { get; set; }
        [Required]
        public string Assignee { get; set; } 
        public string Status { get; set; }
        public int Progress { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public System.DateTime Deadline { get; set; }
        [Required]
        public string Description { get; set; }
        [Required(ErrorMessage ="Project name is required.")]
        public int IDProject { get; set; }

        public virtual List<Project> Projects { get; set; }
        public virtual List<User> Users { get; set; }
        public List<string> Statuses { get; set; }
        public Project Project { get; set; }
        public User User { get; set; }
    }
}