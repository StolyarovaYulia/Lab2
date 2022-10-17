using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radiostation.DAL.Entities
{
    public class Performer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsGroup { get; set; }

        public string GroupList { get; set; }

        public string Description { get; set; }


        public List<Track> Tracks { get; set; }
    }
}
