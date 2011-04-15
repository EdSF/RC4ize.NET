using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RC4.NET;

namespace RC4.NET.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] key = Encoding.ASCII.GetBytes("FACAFACAFA");
            byte[] iv = Encoding.ASCII.GetBytes("0102030405");

            RC4Transform t = new RC4Transform(key, iv);
            Encoding enco = Encoding.GetEncoding("iso-8859-1");

            Console.WriteLine("Welcome to the RC4.NET Test Console App");
            Console.WriteLine();
            Console.Write("Type something to be encrypted: ");
            String s = Console.ReadLine();
            Console.WriteLine();

            if (String.IsNullOrEmpty(s)) 
                return; // exit program

            Console.WriteLine("To be encrypted:    {0}", s);

            s = s.RC4ize(key, iv, enco);
            Console.WriteLine("Encrypted version:  {0}", s);
            Console.WriteLine("Hex representation: {0}", Hexify(s, enco));

            s = s.RC4ize(key, iv, enco);
            Console.WriteLine();
            Console.WriteLine("Decrypted back:     {0}", s);

            Console.WriteLine();
            Console.Write("Done!");
            Console.Read();
        }

        private static string Hexify(string s, Encoding e)
        {
            var array = e.GetBytes(s)
                .Select(b => b.ToString("x2"))
                .ToArray();

            return String.Join("", array);
        }
    }
}
