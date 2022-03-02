using System;
using System.Collections.Generic;

namespace TestJusan
{
    public class Counterparty
    {
        public int ID { get; set; } //идентификатор
        public int BIN { get; set; } //БИН/ИИН
        public DateTime Create_date { get; } //дата создания
        public string Create_author { get; } //автор создания
        public DateTime Update_date { get; set; } //  дата изменения
        public string Update_author { get; set; } //автор изменения

        public Counterparty() { }

        public Counterparty(int id, int bin)
        {
            ID = id;
            BIN = bin;
            Create_date = DateTime.Today;
            Create_author = "Anhi";
            Update_date = DateTime.Today;
            Update_author = "Anhi";
        }

        public Counterparty(int id, int bin, string сreate_author) : this(id, bin)
        {
            Create_date = DateTime.Today;
            Create_author = сreate_author;
            Update_date = DateTime.Today;
            Update_author = Create_author;
        }

        public void UpdateAuthor(string new_author)
        {
            Update_author = new_author;
        }
    }

    public class PhysicalPerson : Counterparty
    {
        public string Name { get; set; }
        public string Last_name { get; set; }
        public string Otchestvo { get; set; }
        public int Age { get; set; }

        public PhysicalPerson() { }

        public PhysicalPerson(int id, string name, string last_name, string otchestvo, int age, int bin)
            : base(id, bin)
        {
            Name = name;
            Age = age;
            Last_name = last_name;
            Otchestvo = otchestvo;
        }
        public PhysicalPerson(int id, string name, string last_name, string otchestvo, int age, int bin, string create_author)
            : base(id, bin, create_author)
        {
            Name = name;
            Age = age;
            Last_name = last_name;
            Otchestvo = otchestvo;
        }
    }

    public class JuridicalPerson : Counterparty
    {
        public string Name { get; set; }
        public List<PhysicalPerson> Physic_list { get; set; }
        public JuridicalPerson(int id, string name, int bin)
            : base(id, bin)
        {
            Name = name;
        }
        public JuridicalPerson(int id, string name, int bin, List<PhysicalPerson> people)
            : base(id, bin)
        {
            Name = name;
            Physic_list = people;
        }
        public int CountPeople()
        {
            return Physic_list.Count;
        }
    }
}
