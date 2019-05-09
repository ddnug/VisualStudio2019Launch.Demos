using System;
using System.Collections.Generic;

namespace VSDemo1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            PerformIntelliCodeDemo();
            PerformNewRefactoringDemoWithDebugSearch();
        }


        private static void PerformNewRefactoringDemoWithDebugSearch()
        {
            var isLoggedIn = true;
            var userState = 3;
            var scheduledTime = DateTime.Now;
            var result = "Initialized";

            if (isLoggedIn && userState == 4 && scheduledTime <= DateTime.Now)
                result = "Authenticated & validated";

            Console.WriteLine(result);



            var collection = new List<Person>()
            {
                new Person("James", "Hetfield"),
                new Person("Lars", "Ulrich"),
                new Person("Kirk", "Hammett"),
                new Person("Cliff", "Burton")
            };

            Console.WriteLine("Metallica's lineup during Master of Puppets was:");
            foreach (var item in collection)
            {
                Console.WriteLine($"{item.FirstName} {item.LastName}");
            }
        }

        private static void PerformIntelliCodeDemo()
        {
            var people = new List<Person>();
        }
    }

    internal class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        internal Person(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}
