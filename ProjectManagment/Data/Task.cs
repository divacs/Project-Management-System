
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Data
{

using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Task
{
        [Key]
    public int ID { get; set; }

    public string Assignee { get; set; }

    public string Status { get; set; }

    public int Progress { get; set; }

    public System.DateTime Deadline { get; set; }

    public string Description { get; set; }

    public int IDProject { get; set; }



    public virtual Project Project { get; set; }

    public virtual User User { get; set; }

}

}
