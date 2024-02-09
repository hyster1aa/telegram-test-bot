using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace VacancySpace
{
    class Area
    {
        [JsonProperty("name")]
        public string name { get; set; }
        public Area(string Name)
        {
            name = Name;
        }
    }
    class Salary
    {
        [JsonProperty("from")]
        public string from { get; set; }
        [JsonProperty("to")]
        public string to { get; set; }
        [JsonProperty("currency")]
        public string currency { get; set; }
        public Salary(string From, string To,string Currency) 
        {
            from = From;
            to = To;
            currency = Currency;
        }
        public override string ToString()
        {
            return $"From {from} to {to} {currency}";
        }
    }
    class Vacancy
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("alternate_url")]
        public string url { get; set; }
        [JsonProperty("area")]
        public Area area { get; set; }

        [JsonProperty("salary")]
        public Salary salary { get; set; }
        public Vacancy(string Name,string Url,Area area,Salary salary)
        {
            name = Name;
            url = Url;
            this.area=area;
            this.salary=salary;
        }
    }
    class VacanciesList
    {
        [JsonProperty("items")]
        public List<Vacancy> list { get; set; }=new List<Vacancy>();
    }

}
