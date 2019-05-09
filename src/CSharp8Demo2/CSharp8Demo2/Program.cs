using System;

namespace CSharp8Demo2
{
    class Program
    {
        static void Main(string[] args)
        {
            PerformRecursivePattersDemo();
            // SHOW Conditional Breakpoints 
            // SHOW EDITING Project file 
        }

        private static void PerformRecursivePattersDemo()
        {
            var p = new Person("Nicko", "McBrain");

            var fullName = GetFullNameOldWay(p);
            Console.WriteLine($"Hello {fullName}");

            fullName = GetFullNameTernary(p);
            Console.WriteLine($"Hello {fullName}");

            fullName = GetFullNameNewRecursive(p);
            Console.WriteLine($"Hello {fullName}");

            fullName = GetFullNameNewSwitchExpression(p);
            Console.WriteLine($"Hello {fullName}");

            Console.ReadLine();
        }

        private static string GetFullNameOldWay(Person p)
        {
            var result = string.Empty;

            if (p.FirstName != null && p.MiddleName != null && p.LastName != null)
            {
                result = $"{p.FirstName} {p.MiddleName[0]}. {p.LastName}";
            }
            else if (p.FirstName != null && string.IsNullOrEmpty(p.MiddleName) && p.LastName != null)
            {
                result = $"{p.FirstName} {p.LastName}";
            }
            else if (p.FirstName != null && string.IsNullOrEmpty(p.MiddleName) && string.IsNullOrEmpty(p.LastName))
            {
                result = $"{p.FirstName}";
            }

            return result;
        }

        private static string GetFullNameTernary(Person p)
        {
            // this works... 
            return (p.MiddleName != null)
                ? $"{p.FirstName} {p.MiddleName[0]}. {p.LastName}"
                : $"{p.FirstName} {p.LastName}";

            // but what about other scenarios like FirstName only, LastName only, First & Middle without Last, etc..?
        }

        private static string GetFullNameNewRecursive(Person p)
        {
            // NEW C# 8.0 FEATURE:  Recursive Patterns (with Tuple values)
            switch (p.FirstName, p.MiddleName, p.LastName)
            {
                // Use tuple patterns to return desired value
                case (string f, string m, string l):
                    return $"{f} {m[0]} {l}";
                // but can also do this...
                case (string f, null, string l):
                    return $"{f} {l}";
                case (string f, string m, null):
                    return $"{f} {m}";
                case (string f, null, null):
                    return $"{f}";
                case (null, null, null):
                    return "Person";
                default:
                    return "";
            }
            // still lots of syntax, too many cases & return values 
        }

        private static string GetFullNameNewSwitchExpression(Person p)
        {
            // NEW C# 8.0 FEATURE:  SWITCH EXPRESSIONS
            return (p.FirstName, p.MiddleName, p.LastName) switch
            {
                (string f, string m, string l)  => $"{f} {m[0]} {l}",
                (string f, null,     string l)  => $"{f} {l}",
                (string f, string m, null)      => $"{f} {m}",
                (string f, null,     null)      => $"{f}",
                (null,     null,     null)      => "Person",
            };
        }

    }

}
