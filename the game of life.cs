using System;

namespace game
{
    class Program
    {
        //drukowanie tablicy
        public static int printCounter = 0;
        public static void Print(char[,] arr){

            Console.WriteLine("Generation " + ++printCounter);

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++) {
                    Console.Write(arr[i, j]);
                }
                Console.WriteLine();
            }
        }
        //wypełnianie tablicy martwymi komorkami 
        public static void Fill(char[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i, j] = '.';
                } 
            }
        }
        //funkcja służąca do przechodzenia z górnej krawędzi do dolnej, lewej do prawej i odwrotnie
        // n - index, l - długość wersu/kolumny tablicy
        public static int Normalize(int n, int l)
        {
            if (n == -1) return l - 1;
            else if (n == l) return 0;
            else return n;
        }
        public static char[,] Check(char[,] arr1)
        {
            char[,] arr2 = new char[arr1.GetLength(0), arr1.GetLength(1)];
            Fill(arr2);

            // pierwsze dwie pętle iterują po kolejnych elementach tablicy
            for (int i = 0; i < arr1.GetLength(0); i++)
            {
                for (int j = 0; j < arr1.GetLength(1); j++)
                {
                    int counter = 0;

                    //dwie następne odpowiedzialne są za sprawdzanie wszystkich elementów wokól
                    for (int x = i - 1; x <= i + 1; x++)
                    {
                        int p = Normalize(x, arr1.GetLength(0));

                        for (int y = j - 1; y <= j + 1; y++)
                        {  
                            int r = Normalize(y, arr1.GetLength(1));
                            //sprawdzanie czy nie jest to element środkowy w kwadracie do sprawdzenia
                            if ((p != i || r != j) && arr1[p, r] == 'X')
                            {
                                counter++;   
                            }
                        }
                    }
                    // MECHANIZM OŻYWIANIA
                    //warunek dla żywej komórki
                    if ((counter == 2 || counter == 3) && arr1[i, j] == 'X')
                    {
                        arr2[i, j] = 'X';
                    }
                    //warunek dla martwej komórki
                    if (counter == 3 && arr1[i, j] == '.')
                    {
                        arr2[i, j] = 'X';
                    }
                    //w każdym innym przypadku komórka pozostaje martwa
                }
                
            }
            return arr2;
        }

        static void Main(string[] args)
        {
            char[,] genArr = new char[12, 20];

            //hard coded
            Fill(genArr);
            //czwarty wiersz
            genArr[4, 7] = 'X';
            genArr[4, 9] = 'X';
            genArr[4, 10] = 'X';
            //piąty wiersz
            genArr[5, 7] = 'X';
            genArr[5, 8] = 'X';
            genArr[5, 9] = 'X';
            //szósty wiersz
            genArr[6, 8] = 'X';
            //wypisanie aktualnego układu dla pierwszej generacji
            Print(genArr);

            Console.WriteLine("Podaj ilość generacji do sprawdzenia:");
            int numberOfGenerations = int.Parse(Console.ReadLine());

            while (numberOfGenerations != 1)
            {
                genArr = Check(genArr);
                Print(genArr);
                numberOfGenerations--;
            }

            Console.ReadLine();
        }
    }
}
