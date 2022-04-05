using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFSR
{
    class StreamCipher
    {
        public static List<int> CipherWord(string word, int n, int[] mask, int[]seed) 
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            Byte[] encodedASCII_Word = ascii.GetBytes(word);                    // -- zamiana stringa na tablicę zakodowanych
                                                                                // liter za pomocą odpowiedników liczbowych w ASCII

            List<int> encodedBinaryWord = ASCIIToBinary(encodedASCII_Word);     // -- binarny ciąg

            int[] key = LFSR.DoLFSR(n, mask, seed);                             // -- wygenerowany klucz służący do zakodowania frazy
            int counter = 0;

            List<int> result = new List<int>();

            foreach (var item in encodedBinaryWord)                             // -- Pętla przechodzi przez wszystkie elementy słowa i koduje je kluczem LFSR
            {
                if (counter == key.Length)                                      // -- jeśli licznik dojdzie do końca klucza, to "skanuj klucz" od początku
                    counter = 0;

                result.Add(LFSR.XOR(item, key[counter]));                       // -- operacja XOR bitu frazy oraz klucza LFSR
                counter++;
            }
            return result;
        }

        public static string CipherWordToString(List<int> list)                 // -- zamienia listę na stringa
        {
            string text = "";
            foreach (var item in list)
            {
                text += item.ToString();
            }
            return text;
        }

        public static string CipherWordToString(string word, int n, int[] mask, int[] seed)     //-- przeciazenie metody
        {
            List<int> list = CipherWord(word, n, mask, seed);
            string text = "";           

            foreach (var item in list)
            {
                text += item.ToString()+" ";
            }
            return text;
        }

        private static List<int> IntToBinary(int number)
        {
            /*
             * Funkcja zamienia liczbę dziesietną na liczbę binarną
             */
            List<int> binary = new List<int>();         // -- lista z wynikiem po odwróceniu
            List<int> temp = new List<int>();           // -- lista z wynikiem przed odwróceniem

            while(number!=0)
            {
                temp.Add(number % 2);
                number /= 2;
            }

            for (int i = temp.Count-1; i >=0; i--)           
                binary.Add(temp[i]);                    // -- pętla odwraca wynik, aby mógł zostać prawidłowo odczytany
            
            return binary;
        }

        private static List<int> ASCIIToBinary(Byte[] ascii)
        {
            /*
             * Funkcja zamieniająca zakodowane słowo liczbowymi wartościami ascii na ich postać binarną.
             */

            List<int> temp = new List<int>();
            List<int> result = new List<int>();

            foreach (var byteNumber in ascii)
            {
                temp = IntToBinary(byteNumber);         // -- Zamiana int na liczbę binarną

                foreach (var item in temp)              
                    result.Add(item);                   // -- dodanie pojedynczej liczby binarnej do całego wyniku
            }
            return result;
        }
    }
}
