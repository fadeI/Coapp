using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REDGraphData
{
    public class Table
    {
        public string Name { get; set; }

        public List<Column> Columns{ get; set; }

        public Table(string name)
        {
            Name = name;
            Columns = new List<Column>();
        }

        public override string ToString()
        {
            string result = "Table: " + Name + " Columns: ";

            foreach (Column c in Columns)
            {
                result += c.ToString() + ", ";
            }

            return result;
        }

    }
}
