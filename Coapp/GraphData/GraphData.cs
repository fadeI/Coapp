
using System;
using System.Collections.Generic;



namespace REDGraphData
{
    public  class  GraphData
    {
      
        public List<Relation> AddVerticies()
        {
            List<Relation> relations = new List<Relation>();

            Table a1= new Table("authors");
            a1.Columns.Add(new Column("au_id"));
            Table b1 = new Table("titleauthor");
            b1.Columns.Add(new Column("au_id"));
            Relation r1 = new Relation(a1, b1, RelationType.OneToMany);
            relations.Add(r1);

            Table a2 = new Table("titles");
            a2.Columns.Add(new Column("title_id"));
            Table b2 = new Table("titleauthor");
            b2.Columns.Add(new Column("title_id"));
            Relation r2 = new Relation(a2, b2, RelationType.OneToMany);
            relations.Add(r2);

            Table a3 = new Table("titles");
            a3.Columns.Add(new Column("title_id"));
            Table b3 = new Table("sales");
            b3.Columns.Add(new Column("title_id"));
            Relation r3 = new Relation(a3, b3, RelationType.OneToMany);
            relations.Add(r3);


            Table a4 = new Table("titles");
            a4.Columns.Add(new Column("pub_id"));
            Table b4 = new Table("publishers");
            b4.Columns.Add(new Column("pub_id"));
            Relation r4 = new Relation(a4, b4, RelationType.ManyToOne);
            relations.Add(r4);

            Table a5 = new Table("stores");
            a5.Columns.Add(new Column("store_id"));
            Table b5 = new Table("sales");
            b5.Columns.Add(new Column("store_id"));
            Relation r5 = new Relation(a5, b5, RelationType.ManyToOne);
            relations.Add(r5);

            Table a6 = new Table("stores");
            a6.Columns.Add(new Column("store_id"));
            Table b6 = new Table("discounts");
            b6.Columns.Add(new Column("store_id"));
            Relation r6 = new Relation(a6, b6, RelationType.OneToMany);
            relations.Add(r6);

            Table a7 = new Table("pub_info");
            a7.Columns.Add(new Column("pub_id"));
            Table b7 = new Table("publishers");
            b7.Columns.Add(new Column("pub_id"));
            Relation r7 = new Relation(a7, b7, RelationType.OneToOne);
            relations.Add(r7);

            Table a8 = new Table("employees");
            a8.Columns.Add(new Column("emp_id"));
            Table b8 = new Table("publishers");
            b8.Columns.Add(new Column("emp_id"));
            Relation r8 = new Relation(a8, b8, RelationType.ManyToOne);
            relations.Add(r8);

            Table a9 = new Table("employees");
            a9.Columns.Add(new Column("job_id"));
            Table b9 = new Table("jobs");
            b9.Columns.Add(new Column("job_id"));
            Relation r9 = new Relation(a9, b9, RelationType.ManyToOne);
            relations.Add(r9);

            return relations;
        }

        public List<Table> AddTables()
        {
            List<Table> result = new List<Table>();
            Table authors = new Table("authors");
            authors.Columns.Add(new Column("au_id"));
            result.Add(authors);

            Table discounts = new Table("discounts");
            discounts.Columns.Add(new Column("store_id"));
            result.Add(discounts);

            Table employees = new Table("employees");
            employees.Columns.Add(new Column("emp_id"));
            employees.Columns.Add(new Column("job_id"));
            employees.Columns.Add(new Column("pub_id"));
            result.Add(employees);

            Table jobs = new Table("jobs");
            jobs.Columns.Add(new Column("job_id"));
            result.Add(jobs);

            Table pub_info = new Table("pub_info");
            pub_info.Columns.Add(new Column("pub_id"));
            result.Add(pub_info);

            Table publishers = new Table("publishers");
            publishers.Columns.Add(new Column("pub_id"));
            result.Add(publishers);

            Table roysched = new Table("roysched");
            roysched.Columns.Add(new Column("title_id"));
            result.Add(roysched);


            Table sales = new Table("sales");
            sales.Columns.Add(new Column("store_id"));
            sales.Columns.Add(new Column("ord_num"));
            sales.Columns.Add(new Column("title_id"));
            result.Add(sales);

            Table stores = new Table("stores");
            stores.Columns.Add(new Column("store_id"));
            result.Add(stores);

            Table titleauthor = new Table("titleauthor");
            titleauthor.Columns.Add(new Column("au_id"));
            titleauthor.Columns.Add(new Column("title_id"));
            result.Add(titleauthor);

            Table titles = new Table("titles");
            titles.Columns.Add(new Column("title_id"));
            titles.Columns.Add(new Column("pub_id"));
            result.Add(titles);

            return result;
        }
    }
}
