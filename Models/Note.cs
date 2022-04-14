using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebApp.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string NoteTitle { get; set;}
        public string NoteBody { get; set; }

      
        public Note()
        {

        }

    }

}
