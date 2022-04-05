using System;
using System.Collections.Generic;
using System.Text;

namespace LFSR
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Podaj ile bitów ma mieć rejstr: ");
            int n = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Podaj maskę: ");
            int[] mask = Tab(n);

            Console.WriteLine("Podaj ziarno: ");
            int[] seed = Tab(n);

            Console.Write("Podaj frazę, która ma zostać zakodowana: ");
            string word = Console.ReadLine();

            int[] key = LFSR.DoLFSR(n, mask, seed);

            Console.WriteLine("Klucz LFSR: ");
            foreach (var item in key)
                Console.Write(item + " ");


            Console.WriteLine();

            Console.WriteLine("Szyfrowanie strumieniowe: ");
            Console.WriteLine(StreamCipher.CipherWordToString(word, n, mask, seed));
        }

        private static int[] Tab(int n)
        {
            int[] result = new int[n];

            for (int i = 0; i < n; i++)
            {
                Console.Write("Bit {0}: ",i);
                result[i] = Convert.ToInt32(Console.ReadLine());
            }
            return result;
        }

    }
}
