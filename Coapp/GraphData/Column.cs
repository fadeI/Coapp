using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REDGraphData
{
    public class Column
    {
        public string Name { get; set; }

        public Column(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
