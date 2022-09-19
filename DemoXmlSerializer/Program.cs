﻿using System;
using System.IO;
using System.Xml.Serialization;

namespace DemoXmlSerializer
{
    public class Person
    {
        [XmlElement("FirstName")]
        public string Name { get; set; }
        [XmlElement("RoughAge")]
        public int Age { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Person p1 = new Person() { Name = "David", Age = 30 };
            var xs = new XmlSerializer(typeof(Person));
            //Serialize
            using Stream s1 = File.Create("person.xml");
            xs.Serialize(s1, p1);
            s1.Close();
            //Deserialize
            using Stream s2 = File.OpenRead("person.xml");
            var p2 = (Person)xs.Deserialize(s2);
            Console.WriteLine("*****Person Info*****");
            Console.WriteLine($"Name: {p2.Name}, Age: {p2.Age}");
            Console.WriteLine("*****Person.xml*****");
            string xmlData = File.ReadAllText("person.xml");
            Console.WriteLine(xmlData);
            s2.Close();
            Console.ReadLine();
        }
    }
}
