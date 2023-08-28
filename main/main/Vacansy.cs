using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacansySpace
{
    class Salary
    {
        public string salary_from { get; set; }
        public string salary_to { get; set; }
        public Salary(string salary_from, string salary_to)
        {
            this.salary_from = salary_from;
            this.salary_to = salary_to;
        }
    }
    class Vacancy
    {
        public string id { get; set; }
        public string vacansy_name { get; set; }
        public Salary salary { get; set; }
        public string address { get; set; }
        public string url { get; set; }
        public Vacancy(string id, string vacansy_name, Salary salary, string address, string url)
        {
            this.id = id;
            this.vacansy_name = vacansy_name;
            this.salary = salary;
            this.address = address;
            this.url = url;
        }   
    }
}
