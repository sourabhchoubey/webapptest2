using System;

namespace PhoneBookTestApp
{
    class Program
    {
        private static IPhoneBook phonebook = new PhoneBook();

        static void Main(string[] args)
        {
            try
            {
                // this we use if somehow we creaed the table but due to some network error.
                // we are unable to drop existing table so we calling before initialisation.
                DatabaseUtil.CleanUp();

                DatabaseUtil.initializeDatabase();

                // Create person objects and put them in the PhoneBook and database
                AddPersonsToPhoneBookAndDataBase();

                // Print the phone book out to System.out
                PrintPhoneBook();
                Console.WriteLine();
                // Find Cynthia Smith and print out just her entry
                FindAndPrintPerson("Cynthia Smith");

                //Insert the new person objects into the database
                InsertNewPersonFromConsole();

            }
            finally
            {
                
                DatabaseUtil.CleanUp();
            }
        }

        static void AddPersonsToPhoneBookAndDataBase()
        {
            // Adding persons to the PhoneBook
            phonebook.addPerson(new Person
            {
                name = "John Smith",
                phoneNumber = "(248) 123-4567",
                address = "1234 Sand Hill Dr, Royal Oak, MI"
            });

            phonebook.addPerson(new Person
            {
                name = "Cynthia Smith",
                phoneNumber = "(824) 128-8758",
                address = "875 Main St, Ann Arbor, MI"
            });
            // Add new person to the DataBase
            Person John = new Person
            {
                name = "John Smith",
                phoneNumber = "(248) 123-4567",
                address = "1234 Sand Hill Dr, Royal Oak, MI"
            };
            phonebook.insertperson(John);
            Person Cynthia = new Person
            {
                name = "Cynthia Smith",
                phoneNumber = "(824) 128-8758",
                address = "875 Main St, Ann Arbor, MI"
            };
            phonebook.insertperson(Cynthia);

            Console.WriteLine("Both Person Successfully created into the database");
        }

        static void PrintPhoneBook()
        {
            Console.WriteLine("Phone Book Entries:");

            // Since there's no GetAllPersons method in IPhoneBook,
            // iterate over specific persons or rethink the design for printing.
            // Here, I'm assuming you might iterate over known entries or change the design accordingly.

            // Example: Iterating over known persons for demonstration
            PrintPerson(phonebook.findPerson("John Smith"));
            PrintPerson(phonebook.findPerson("Cynthia Smith"));

            Console.WriteLine();
        }

        static void PrintPerson(Person person)
        {
            if (person != null)
            {
                Console.WriteLine($"Name: {person.name}, Phone Number: {person.phoneNumber}, Address: {person.address}");
            }
        }

        static void FindAndPrintPerson(string Name)
        {
            // Finding and printing a person by first and last name
            Person foundPerson = phonebook.findPerson(Name);

            if (foundPerson != null)
            {
                Console.WriteLine($"Found Person - Name: {foundPerson.name}, Phone Number: {foundPerson.phoneNumber}, Address: {foundPerson.address}");
            }
            else
            {
                Console.WriteLine($"Person '{Name}' not found.");
            }

            Console.WriteLine();
        }
        static void InsertNewPersonFromConsole()
        {
            Console.WriteLine("Enter details for the new person:");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Phone Number: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Address: ");
            string address = Console.ReadLine();

            // Create a Person object
            Person newPerson = new Person
            {
                name = name,
                phoneNumber = phoneNumber,
                address = address
            };

            // Add new person to the DataBase
            phonebook.insertperson(newPerson);

            //
            // Adding persons to the PhoneBook
            phonebook.addPerson(new Person
            {
                name = name,
                phoneNumber = phoneNumber,
                address = address
            });
            Console.WriteLine();
            Console.WriteLine("Below Person Created Successfully into Database");
            Console.WriteLine();
            // Print the updated Database entries
            PrintPerson(phonebook.findPerson(name));
        }

    }
}
