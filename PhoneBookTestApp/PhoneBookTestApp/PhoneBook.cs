using System;
using System.Collections.Generic;

namespace PhoneBookTestApp
{
    public class PhoneBook : IPhoneBook
    {
        private List<Person> phoneBook = new List<Person>();

        public void addPerson(Person newPerson)
        {
            phoneBook.Add(newPerson);
        }

        public Person findPerson(string Name)
        {
            return phoneBook.Find(p => p.name.Equals($"{Name}", StringComparison.OrdinalIgnoreCase));
        }

        public void insertperson(Person newPerson)
        {
            DatabaseUtil.InsertPerson(newPerson);
        }

    }
}
