using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JotFinalProject.Models
{
    public class Note
    {
        public int ID { get; set; }

        public string UserID { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public int CategoryID { get; set; }

        public Category Category { get; set; }
    }
}
