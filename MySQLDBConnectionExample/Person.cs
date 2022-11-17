using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQLDBConnectionExample
{
    internal class Person
    {
        //Attribut
        public int id;
        public string name;
        public int age;

        //Statisk lista
        public static List<Person> persons = new List<Person>();

        //Konstruktor
        public Person(int id, string name, int age)
        {
            this.id = id;
            this.name = name;
            this.age = age;

            //Add THIS objekt to list
            persons.Add(this);
        }
    }
}
