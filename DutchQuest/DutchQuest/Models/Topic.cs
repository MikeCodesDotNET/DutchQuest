using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchQuest.Models
{
    public class Topic : EntityData
    {
        public string Title { get; set; }
        public List<Word> Words { get; set; }
    }
}
