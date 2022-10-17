using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radiostation.DAL.Entities
{
    public class Track
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int PerformerId { get; set; }

        public int GenreId { get; set; }

        public DateTime CreationDate { get; set; }

        public string Duration { get; set; }

        public int Rating { get; set; }


        public Genre Genre { get; set; }

        public Performer Performer { get; set; }

        public List<Translation> Translations { get; set; }
    }
}
