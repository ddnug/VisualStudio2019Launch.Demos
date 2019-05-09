using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
//#nullable enable

namespace CSharp8._0
{

    public class Person
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        public Person(string firstName, string middleName, string lastName)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }
    }


    /// <summary>
    /// C# 8 demo to highlight 
    /// (a) nullable reference types
    /// (b) Async Streams - C# already has support for iterators and asynchronous methods. In C# 8.0, the two are combined into asynchronous streams. 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var subscribers = Service.GetSubscribers();
            var names = GetNames(subscribers);

            foreach (var name in names)
            {
                Console.WriteLine($"{name} has subscribed to the service");
            }
        }

        static IEnumerable<string> GetNames(IEnumerable<Person> people)
        {
            foreach (var p in people)
            {
                yield return GetName(p);
            }
        }

        static string GetName(Person p)
        {
            return $"{p.FirstName} {p.MiddleName[0]}. {p.LastName}";
        }

        #region async
        //static async Task Main(string[] args)
        //{
        //    var subscribers = Service.GetSubscribersAsync();
        //    var names = GetNames(subscribers);

        //    await foreach (var name in names)
        //    {
        //        Console.WriteLine($"{name} has subscribed to the service");
        //    }
        //}

        //static async IAsyncEnumerable<string> GetNames(IAsyncEnumerable<Person> people)
        //{
        //    await foreach (var p in people)
        //    {
        //        yield return GetName(p);
        //    }
        //}
        #endregion


    }
}
