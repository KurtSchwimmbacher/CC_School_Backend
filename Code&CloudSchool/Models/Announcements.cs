using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

public class Announcements
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int AnnouncementId {get; set;}

    public string Title {get; set;} = string.Empty;

    public string Description {get; set;} = string.Empty;

    public DateTime Date {get;set;} = DateTime.Now;

    public int LecturerId {get; set;} //foreign key 

}
