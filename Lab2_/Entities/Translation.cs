using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radiostation.DAL.Entities
{
    public class Translation
    {
        public int Id { get; set; }

        public string Time { get; set; }

        public DateTime Date { get; set; }

        public int EmployeeId { get; set; }


        public int TrackId { get; set; }


        public Track Track { get; set; }

        public Employee Employee { get; set; }
    }
}
