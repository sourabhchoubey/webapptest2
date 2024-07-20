using System;
using System.Data.SQLite;
using MongoDB.Driver.Core.Configuration;

namespace PhoneBookTestApp
{
    public class DatabaseUtil
    {
        public static void initializeDatabase()
        {
            var dbConnection = new SQLiteConnection("Data Source= MyDatabase.sqlite;Version=3;");
            dbConnection.Open();

            try
            {
                SQLiteCommand command =
                    new SQLiteCommand(
                        "create table PHONEBOOK (NAME varchar(255), PHONENUMBER varchar(255), ADDRESS varchar(255))",
                        dbConnection);
                command.ExecuteNonQuery();

                command =
                    new SQLiteCommand(
                        "INSERT INTO PHONEBOOK (NAME, PHONENUMBER, ADDRESS) VALUES('Chris Johnson','(321) 231-7876', '452 Freeman Drive, Algonac, MI')",
                        dbConnection);
                command.ExecuteNonQuery();

                command =
                    new SQLiteCommand(
                        "INSERT INTO PHONEBOOK (NAME, PHONENUMBER, ADDRESS) VALUES('Dave Williams','(231) 502-1236', '285 Huron St, Port Austin, MI')",
                        dbConnection);
                command.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public static SQLiteConnection GetConnection()
        {
            var dbConnection = new SQLiteConnection("Data Source= MyDatabase.sqlite;Version=3;");
            dbConnection.Open();

            return dbConnection;
        }

        public static void InsertPerson(Person person)
        {
            try
            {
                SQLiteCommand command = new SQLiteCommand(
                    "INSERT INTO PHONEBOOK (NAME, PHONENUMBER, ADDRESS) VALUES (@Name, @PhoneNumber, @Address)",
                    GetConnection());
                command.Parameters.AddWithValue("@Name", person.name);
                command.Parameters.AddWithValue("@PhoneNumber", person.phoneNumber);
                command.Parameters.AddWithValue("@Address", person.address);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting person into database: {ex.Message}");
                throw;
            }
        }

        public static void CleanUp()
        {
            var dbConnection = new SQLiteConnection("Data Source= MyDatabase.sqlite;Version=3;");
            dbConnection.Open();

            try
            {
                // Check if the PHONEBOOK table exists
                SQLiteCommand checkTableCommand = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='PHONEBOOK';", dbConnection);
                var tableName = checkTableCommand.ExecuteScalar();

                if (tableName != null && tableName.ToString() == "PHONEBOOK")
                {
                    // Table exists, so drop it
                    SQLiteCommand dropTableCommand = new SQLiteCommand("DROP TABLE PHONEBOOK;", dbConnection);
                    dropTableCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cleaning up database: {ex.Message}");
                throw;
            }
            finally
            {
                dbConnection.Close();
            }
        }

    }
}
