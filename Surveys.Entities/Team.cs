using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveys.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public byte[] Logo { get; set; }

        public override string ToString()
        {
            return $"{Id} | {Name} | {Color}";
        }
    }
}
