using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp8._0
{
    class Service
    {
        private static readonly string _connectionString = 
            "Data Source=.\\SQLEXPRESS;Initial Catalog=Spider;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True";

        private static Person[]  _people = new[]
        {
            new Person("Tracey", "Jaron", "Downer"),
            new Person("Thorburn", "Wat", "Tuft"),
            new Person("Marisa", "Waters"),
            new Person("Rosy", "Christy", "Bannister"),
            new Person("Dominick", "Maximillian", "Ayers"),
            #region Other data
            //new Person (null, "Monique", "Smalls"),
            //new Person ("Corrina", null, null),
            //new Person ("America", "Randy", null),
            //new Person (null, null, "Dwerryhouse"),
            //new Person (null, "Kaleigh", null),
            //new Person (null, null, null)
            #endregion
        };

        public static IEnumerable<Person> GetSubscribers()
        {
            foreach (var p in _people)
            {
                yield return p;
            }
        }

        async public static IAsyncEnumerable<Person> GetSubscribersAsync()
        {
            foreach (var p in GetSubscribers())
            {
                await Task.Delay(500);
                yield return p;
            }
        }

        public static int GetUserCount()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = "SELECT count(*) FROM [User]";
                    return (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
