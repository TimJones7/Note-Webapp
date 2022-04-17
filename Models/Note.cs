using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


public enum PriorityLevel
{
    low = 0,
    medium = 1,
    high = 2,
    critical = 3
}


namespace MyFirstWebApp.Models
{
    public class Note
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string NoteTitle { get; set;}
        [Required]
        public string NoteBody { get; set; }

        public string User { get; set; }

        public DateTime TimeCreated { get; set; } = DateTime.Now;

        public bool Resolved { get; set; } = false;

        [EnumDataType(typeof(PriorityLevel))]
        public PriorityLevel Priority { get; set; }

        


        public Note()
        {

        }

    }
}
