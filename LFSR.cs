using System;
using System.Collections.Generic;

namespace LFSR
{
    class LFSR
    {       
        public static int[] DoLFSR(int n, int[] mask, int[] seed)// typy????
        {
            /*
             * n - długość ciągu - ilosć bitów
             * mask - maska - bity, które mają wykonywać między sobą operacje XOR
             * seed - przykładowe ziarno
             */

            List<int> result = new List<int>();     // -- lista na wynik działania LFSR
            bool firstMaskBit = true;               // -- czy pierwsza 1 w masce
            bool firstRow = true;                   // -- sprawdza czy jest to pierwszy wiersz algorytmu,
                                                    //    który powinien korzystać z seeda ( kolejne korzystają ze zmiennej nextRow)
            int[] nextRow = new int[n];             // -- wygląd kolejnego wiersza rozpisanego algorytmu            
            int maxCycle = (int)Math.Pow(2, n) - 1; // -- liczenie ilosci cykli 2^n - 1

            while(result.Count != maxCycle) // -- działaj dopóki liczba elementów na liście będzie różna od maksymalnej ilości cykli
            {
                int lastXORresult = 2;      // -- ostatni wynik wykonanej operacji XOR
                
                for (int i = 0; i < n; i++)
                {
                    if (mask[i] == 1)       // -- jeśli wystąpiła jedynka w masce
                    {
                        if (firstMaskBit == true) // -- jeśli w wierszu branym do obliczeń, w masce jedynka występuje po raz pierwszy
                        {
                            /*
                             * Pierwsza zmienna do operacji XOR jest wpisywana do oddzielnej zmiennej
                             * Na tym etapie algorytmu jest to pierwszy składnik opercaji XOR (pierwsza jedynka w masce)
                             * 
                             * firstRow == true => ostatni wynik operacji XOR = aktualny bit ziarna
                             * firstRow == false => ostatni wynik operacji XOR = aktualny bit utworzonego wiersza w poprzednim przejściu algorymu
                             */
                            lastXORresult = (firstRow) ? seed[i] : nextRow[i];
                            firstMaskBit = false; // pierwsza jedynka w masce juz wystąpiła
                        }

                        else        // -- jeśli w lastXORresult jest już pobrany ostatni wynik operacji
                            lastXORresult = (firstRow) ? XOR(lastXORresult, seed[i]) : XOR(lastXORresult, nextRow[i]);
                            /*
                             * firstRow == true => zrób operację XOR używajac ziarna
                             * firstRow == false => zrób operację XOR używając nowo wygenerowanego wiersza w poprzednim przejściu algorytmu
                             */
                    }

                    if (i == n - 1)     // -- jesli i wskazuje na najmłodszy bit
                    {
                        int[] temp = new int[n];            // -- tablica tymczasowa przechowująca ziarno w 1-szym przejściu lub nextRow w kolejnych
                        if (firstRow == true)
                            Array.Copy(seed, temp, n);      // -- kopiuj tab seed do tablicy temp                       
                        else
                            Array.Copy(nextRow, temp, n);   // -- kopiuj tab nextRow do tablicy temp

                        result.Add(temp[i]);                // -- dopisz najmłodszy bit do wyniku 
                        nextRow[0] = lastXORresult;         // -- w nowym wierszu zapisz operację XOR na najstarszym bicie

                        for (int j = 1; j < n; j++)
                            nextRow[j] = temp[j - 1];       // -- przepisz resztę bitów po przesunięciu

                        if(firstRow) firstRow = false;      // -- koniec pierwszego wiersza
                    }
                }

                firstMaskBit = true;    // -- przechodząc do nowego wiersza trzeba zresetować ustawienia pomocnicze odnośnie maski
            }

            return result.ToArray();
        }

        public static int XOR(int a, int b)
        {
            return (a == b) ? 0 : 1;
        }
    }
}
