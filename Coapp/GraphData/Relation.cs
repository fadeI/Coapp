using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REDGraphData
{
    public class Relation
    {
        public Table TableA { get; set; }

        public Table TableB { get; set; }

        public RelationType RelationType { get; set; }

        public Relation(Table tableA, Table tableB, RelationType relationType)
        {
            TableA = tableA;
            TableB = tableB;
            RelationType = relationType;
        }

        public override string ToString()
        {
            return TableA + " " + RelationType + " " + TableB;
        }

    }

    public enum RelationType
    {
        OneToOne,
        OneToMany,
        ManyToOne,
        ManyToMany

        
    }

}
