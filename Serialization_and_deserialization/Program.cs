using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using MySql.Data.MySqlClient;

namespace Serialization_and_deserialization
{

    public class Program
    {

        public static void Main(string[] args) 
        {
            List<StudentDetails> student = new List<StudentDetails>();
            student.Add(new StudentDetails
            {
                AdmissionNumber = 1999,
                StudentName = "angela wambugu",
                Course = "BICS",
                Year = 4,
            });
            student.Add(new StudentDetails
            {
                StudentNumber =78651,
                StudentName = "mark kirui",
                Course = "medicine",
                Year = 3,
            });

            Serialize(student);
            Deserialize();
            DB();

        }

        static public void Serialize(List<StudentDetails> details)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<StudentDetails>));
            StreamWriter writer = new StreamWriter(@"C:\Users\Stace Omwando\source\repos\Serialization_and_deserialization\Serialization_and_deserialization\Xml.xml");
            serializer.Serialize(writer, details);
            writer.Close();
            Console.WriteLine("_________________________________________________");
            Console.WriteLine("");
            Console.WriteLine("Successfully Serialized Student Data");
            Console.WriteLine("");
            Console.WriteLine("_________________________________________________");
        }
        static public void DB_Serialize(List<StudentDetails> details)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<StudentDetails>));
            StreamWriter writer = new StreamWriter(@"C:\Users\Stace Omwando\source\repos\Serialization_and_deserialization\Serialization_and_deserialization\Db_xml.xml");
            serializer.Serialize(writer, details);
            writer.Close();
            Console.WriteLine("_________________________________________________");
            Console.WriteLine("");
            Console.WriteLine("Successfully Serialized Database Data");
            Console.WriteLine("");
            Console.WriteLine("_________________________________________________");
        }
        static public void Deserialize()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<StudentDetails>));
            using (FileStream fileStream = new FileStream(@"C:\Users\Stace Omwando\source\repos\Serialization_and_deserialization\Serialization_and_deserialization\Xml.xml", FileMode.Open))
            {
                List<StudentDetails> result = (List<StudentDetails>)serializer.Deserialize(fileStream);
                Console.WriteLine("");
                Console.WriteLine("_________________________________________________");
                Console.WriteLine("Deserializing DB Data...");
                foreach (StudentDetails details in result)
                {
                    Console.WriteLine("_________________________________________________");
                    Console.WriteLine("");
                    Console.WriteLine("Student Number   :   " + details.StudentNumber);
                    Console.WriteLine("Student Name     :   " + details.StudentName);
                    Console.WriteLine("Course           :   " + details.Course);
                    Console.WriteLine("Year             :   " + details.Year);
                    Console.WriteLine("_________________________________________________");
                    Console.WriteLine("");
                }

            }

        }
        static public void DB()
        {
            string cs = @"server=localhost;userid=root;password=;database=student";

            using var con = new MySqlConnection(cs);
            con.Open();

            string sql = "SELECT * FROM student";
            using var cmd = new MySqlCommand(sql, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();
            List<StudentDetails> student = new List<StudentDetails>();

            while (rdr.Read())
            {
                student.Add(new StudentDetails
                {
                    StudentNumber = rdr.GetInt32(0),
                    StudentName = rdr.GetString(1),
                    Course = rdr.GetString(2),
                    Year = rdr.GetInt32(3),
                });
            }
            DB_Serialize(student);
        }
    }
}

