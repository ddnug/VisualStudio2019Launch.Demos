using System;
using System.IO;
using System.Xml;

namespace CSharp8Demo2
{
    class Program
    {
        static void Main(string[] args)
        {
            PerformRangesDemo();
            PerformRecursivePattersDemo();
            PeformUsingDemo();
            // SHOW Conditional Breakpoints 
            // SHOW EDITING Project file 
        }

        private static void PeformUsingDemo()
        {
            // === Example #1

            var filename = @"c:\data\somefile.txt";
            var filenameXml = @"c:\data\somefile.xml";
            // Pre C# 8.0
            using (var reader = new StreamReader(filename))
            {
                var contents = reader.ReadToEnd();
                Console.WriteLine($"Read {contents.Length} characters from file.");
            }

            // 8.0 approach 
            using var reader2 = new StreamReader(filename);
            var contents2 = reader2.ReadToEnd();
            Console.WriteLine($"Read {contents2.Length} characters from file.");

            // === Example #2

            // Pre C# 8
            using (var reader3 = new StreamReader(filename))
            using (var reader4 = XmlReader.Create(filenameXml))
            {
                // process the files
            }

            // C# 8
            using var reader5 = new StreamReader(filename);
            using var reader6 = XmlReader.Create(filenameXml);

        }

        private static void PerformRangesDemo()
        {
            // From: https://www.dotnetcurry.com/csharp/1489/csharp-8-visual-studio-2019

            // C# 8.0 introduces new syntax for expressing a range of values.
            Range range = 1..5;

            //The starting index of a range is inclusive, and the ending index is exclusive.Alternatively, the ending can be specified as an offset from the end:
            range = 1..^1;  // 1-2

            //The new type can be used as an indexer for arrays.Both ranges specified above will give the same result when used with the following snippet of code:
            var array = new[] { 0, 1, 2, 3, 4, 5 };
            var subArray = array[range]; // = { 1, 2, 3, 4 }

            // The new syntax can also be used to define:
            // (a) An open-ended range from the beginning to a specific index:
            subArray = array[..^1]; // = { 0, 1, 2, 3, 4 }

            // (b) An open-ended range from a specific index to the end:
            subArray = array[1..]; // = { 1, 2, 3, 4, 5 }

            // (c) An index of a single item specified as an offset from the end.
            var item = array[^1]; // = 5

            // The use of ranges and indices is not limited to arrays. They can also be used with the Span<T> type (you can read more about Span<T> in my DNC Magazine article C# 7.1, 7.2 and 7.3 - New Features):
            array = new[] { 0, 1, 2, 3, 4, 5 };
            var span = array.AsSpan(1, 4); // = { 1, 2, 3, 4 }

            //var subSpan = span[1..^1]; // = { 2, 3 }

            //Although that’s the full extent to which the Range type can be used with existing types in .NET Core 3.0 Preview 2, there are plans to provide overloads with Range-typed parameters for other methods as well, e.g.:
            //subSpan = span.Slice(range);
            //var substring = "range"[range];
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
