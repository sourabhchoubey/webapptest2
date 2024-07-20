namespace PhoneBookTestApp
{
    public interface IPhoneBook
    {
        Person findPerson(string firstName);
        void addPerson(Person newPerson);
        void insertperson(Person newPerson);
    }
}
