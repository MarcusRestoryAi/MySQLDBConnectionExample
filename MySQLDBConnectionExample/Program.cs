﻿using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
using System.Xml.Linq;

namespace MySQLDBConnectionExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");

            //Iniitera variabeler deklaration
            string server = "LOCALHOST";
            string database = "jensenmysqlapp";
            string username = "root";
            string pass = "SokrateS13";

            string strConn = $"SERVER={server};DATABASE={database};UID={username};PASSWORD={pass};";

            //Establera koppling till Databas
            MySqlConnection conn = new MySqlConnection(strConn);

            ConsoleKeyInfo input;

            //Meny
            do
            {
                Console.Clear();

                //Skriva ut en meny för användaren
                Console.WriteLine("Välj ditt val för DB funktion!");
                Console.WriteLine("------------------------------");
                Console.WriteLine("1. Skriv data");
                Console.WriteLine("2. Hämta data");
                Console.WriteLine("3. Avsluta");

                //Låta användaren välja ett alternativ
                input = Console.ReadKey();

                //Ta värdet till en SwitchCase
                switch (input.KeyChar.ToString())
                {
                    case "1":
                        Console.Clear();
                        WriteData(conn);
                        break;
                    case "2":
                        Console.Clear();
                        FetchData(conn);
                        break;
                    case "3":
                        break;
                    default:
                        Console.WriteLine("Du har matat in ett felaktigt värde. (Press any key to continue...)");
                        Console.ReadKey();
                        break;

                }


            } while (input.KeyChar.ToString() != "3");

        }

        public static void WriteData(MySqlConnection conn)
        {
            //Hämta data från användare
            Console.Write("Mata in namnet på en person: ");
            string name = Console.ReadLine();

            Console.Write("Mata in åldern på en person: ");
            int age = Convert.ToInt32(Console.ReadLine());

            // SQL Querry för INSERT
            string sqlQuerry = $"CALL persons_insert('{name}', {age});";

            // Skapa MySQLCOmmand objekt
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);

            //Exekvera MySQLCommand
            cmd.ExecuteReader();

            //Stänga DB koppling
            conn.Close();
        }

        public static void FetchData(MySqlConnection conn)
        {
            // SQL Querry för INSERT
            string sqlQuerry = "CALL persons_select();";

            // Skapa MySQLCOmmand objekt
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);

            //Exekvera MySQLCommand. Spara resultat i reader
            MySqlDataReader reader = cmd.ExecuteReader();

            //Tömma Persons list
            Person.persons.Clear();

            //While Loop för att skriva ut resultatet till Konsol
            while (reader.Read())
            {
                //Skriv ut person till Konsol
                Console.WriteLine($"Name: {reader["persons_name"]} Age: {reader["persons_age"]}");

                //Spara data till Lista
                new Person(Convert.ToInt32( reader["persons_id"]), reader["persons_name"].ToString(), Convert.ToInt32( reader["persons_age"]));
            }

            //Stänga DB koppling
            conn.Close();

            Console.WriteLine("Data Fetched successfully! Press any key to continue");
            Console.ReadKey();
        }

    }
}