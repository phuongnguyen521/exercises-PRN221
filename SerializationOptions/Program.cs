using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SerializationOptions
{
    public class Employee
    {
        [JsonPropertyName("FullName")]
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string json = @"{
                ""FullName"":""Mark"",
                ""Email"":""Mark@gmail.com"",
                ""Salary"": 1000,
            }";
            var option = new JsonSerializerOptions()
            {
                WriteIndented = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true,
            };
            var emp = JsonSerializer.Deserialize<Employee>(json, option);
            Console.WriteLine($"Name: {emp.Name}, Email:{emp.Email}, Salary: {emp.Salary}");
            Console.ReadLine();
        }
    }
}
