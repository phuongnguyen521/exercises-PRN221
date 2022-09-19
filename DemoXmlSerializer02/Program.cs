using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace DemoXmlSerializer02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileName = "people.xml";
            var people = new List<Person> {
                new Person(30000M)
                {
                    FirstName = "Alice",
                    LastName = "Smith",
                    DateOfBirth = new DateTime(1972,3,16)
                },
                new Person(20000M)
                {
                    FirstName = "Charlie",
                    LastName = "Cox",
                    DateOfBirth = new DateTime(1984,5,4),
                    Children = new HashSet<Person>
                    {
                        new Person(0M)
                        {
                            FirstName = "Sally",
                            LastName = "Cox",
                            DateOfBirth = new DateTime(2000,7,12),
                        }
                    }
                }
            };
            //Create object that will format a list of Persons as XML
            var xs = new XmlSerializer(typeof(List<Person>));
            using FileStream stream = File.Create(fileName);
            xs.Serialize(stream, people);
            Console.WriteLine("Written {0:N0} bytes of XML to {1}", new FileInfo(fileName).Length, fileName);
            stream.Close();
            Console.WriteLine(new String('*', 30));
            //Display the serialized object graph
            Console.WriteLine(File.ReadAllText(fileName));
            Console.WriteLine(new String('*', 30));
            //Deserializing with XML
            using FileStream xmlLoad = File.Open(fileName, FileMode.Open);
            //Deserialize and cast the object graph into a list of Person
            var loadedPeople = (List < Person >)xs.Deserialize(xmlLoad);
            foreach (var item in loadedPeople)
            {
                Console.WriteLine($"{item.LastName} has {item.Children?.Count ?? 0} children");
            }
            xmlLoad.Close();
            Console.ReadLine();
        }
    }
}
