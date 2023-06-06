using System;
using System.Collections.Generic;
using System.Linq;
/*
                 //Базис  План    y0    y1    y2     y3          y4    y5   yb,yr
                {   0,    0,     0,    1,    1,     0,          0,    0,          0  },  //0
               //---------------------------------------------------------------------------
                {   0,    0,     1,    0,    1,     0.09375,    0,    0.21875,    0  },  //1
                {   1,    0,     0,    1,    0,    -0.1875,     0,    0.5625,     0  },  //2
                {   1,    0,     0,    0,    0,    -2.125,      0,    1.375,      0  },  //3
                {   0,    1,     0,    0,    0,    -0.21875,    0,    0.15625,    0  },  //4
               //---------------------------------------------------------------------------
                {   0,    0,     0,    0,    0,    0,     0,          0,          0 },   //5
                //  0     1      2     3     4     5      6           7           8 
*/

namespace MM_Lab6
{
    internal class Program
    {
        static readonly double M = 1.5177717771777;
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("\n Автор Новік Олександр КН-203");
            Equation target = new Equation(new double[] { 0, 1, 1 });
            double[,] matrix = new double[,]
            {
                //Базис  План    y1    y2    y3     y4    y5    y6  u1(y7) u2(y8)  yb,yr
                {    0,    0,     0,    1,    1,     0,    0,    0,    M,    M,     0  },  //0
               //---------------------------------------------------------------------------
                {    0,    0,    -8,    2,   -1,     1,    0,    0,    0,    0,     0  },  //1
                {    0,    0,    -5,    1,    1,     0,    1,    0,    0,    0,     0  },  //2
                {   -M,    0,    -3,   -3,    2,     0,    0,   -1,    1,    0,     0  },  //3
                {   -M,    1,     0,    2,    1,     0,    0,    0,    0,    1,     0  },  //4
               //---------------------------------------------------------------------------
                {    0,    0,     0,    0,    0,    0,     0,    0,    0,    0,     0 },   //5
                //   0     1      2     3     4     5      6     7     8  
            };
            Console.WriteLine("\n----------------------------- Пошук максимума функції: ----------------------------------");
            SimplexTable tableMax = new SimplexTable(target, matrix, 6, 11);
            SimplexTable tableMin = new SimplexTable(target, (double[,])matrix.Clone(), 6, 11);
            tableMax.FindMaxD(new int[] { 4, 5, 7, 8 }, true, false);
            Console.WriteLine("\n----------------------------- Пошук мінімума функції: ----------------------------------");
            tableMin.FindMinD(new int[] { 4, 5, 7, 8 }, false, false);
        }
    }
    class Equation
    {
        public readonly double[] Coefficients;
        public Equation(double[] coefs)
        {
            Coefficients = coefs;
        }
        public double GetFunctionValue(double[] multipl)
        {
            if (multipl.Length != Coefficients.Length)
                throw new IndexOutOfRangeException("К-сть елементів нерівності не рівна к-сті множників!");
            double result = 0;
            for (int i = 0; i < Coefficients.Length; i++)
            {
                result += Coefficients[i] * multipl[i];
            }
            return result;
        }
    }
    class SimplexTable
    {
        public double[,] Matrix { get; set; }
        readonly int CollumnLength, RowLength;
        readonly Equation targetEquation;
        public SimplexTable(Equation targetEquation, double[,] matrix, int collumnLen, int rowLen)
        {
            this.targetEquation = targetEquation;
            Matrix = matrix;
            CollumnLength = collumnLen;
            RowLength = rowLen;
            for (int i = 2; i < CollumnLength - 2; i++)
            {
                Matrix[0, i] = targetEquation.Coefficients[i - 2];
            }
        }
        /// <summary>
        /// Пошук максимуму для даної цільової функції та її обмежень
        /// </summary>
        /// <param name="baseIndexes"> Індекси хБазисних (для старту це х3, х4, х5) </param>
        /// <param name="isMax"> Чи шукається максимум </param>
        /// <param name="isDual"> Треба розв'язати задачу з заданої матрицею зведеною для двоїстого методу</param>
        public void FindSolution(int[] baseIndexes, bool isMax, bool isDual)
        {
            int step = 0;
            bool isOver;
            int row = 2, coll = 1;
            for (int i = 0; i < CollumnLength - 2; i++, row++, coll++)
            {
                Console.WriteLine("\n Спочатку знайдемо базис даної симплекс таблиці:");
                if (IsBase())
                    break;
                Console.WriteLine("\n Крок {0}", i + 1);
                NextIteration(Matrix[coll, row], row, coll, baseIndexes);
                PrintTable(baseIndexes, true);
            }
            while (true)
            {
                Console.WriteLine("\n Ітерація {0}", ++step);
                Console.WriteLine("\n Початковий вигляд матриці коефіцієнтів для даної ітерації:");
                CalculateF();
                PrintTable(baseIndexes, true);
                isOver = !IsOptimal();
                int dirCol = GetDirectColumn(baseIndexes);
                int dirRow = GetDirectRow(dirCol);
                double directElement = Matrix[dirRow, dirCol];
                Console.WriteLine("\n Визначено значення для F та b:");
                PrintTable(baseIndexes, false, dirRow, dirCol, isOver);
                if (isOver)
                    break;
                NextIteration(directElement, dirCol, dirRow, baseIndexes);
            }
            ShowResult(baseIndexes, isMax, isDual);
        }
        /// <summary>
        /// Пошук базису у симплекс таблиці
        /// </summary>
        /// <returns> Якщо базис уже існує повертає true </returns>
        private bool IsBase()
        {
            for (int i = 1; i < CollumnLength - 1; i++)
            {
                for (int j = 1; j < RowLength - 1; j++)
                {
                    if (Matrix[i, j] == 1 && Matrix[i, j + 1] == 0
                        && Matrix[i + 1, j] == 0 && Matrix[i + 1, j + 1] == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Проводить нову ітерацію симплекс методу над даною таблицею
        /// </summary>
        /// <param name="directValue"> Визначальне значення </param>
        /// <param name="directRow"> Визначалиний рядок </param>
        /// <param name="directColumn"> Визначальний стовбець </param>
        /// <param name="baseIndexes"> Індекси хБазисних </param>
        private void NextIteration(double directValue, int directRow, int directColumn, int[] baseIndexes)
        {
            // Запам'ятовую стан матриці на поточний момент
            double[,] oldMatrix = (double[,])Matrix.Clone();
            // Цикл зміни матриці відносно поточного визначального елемента
            for (int i = 1; i < CollumnLength; i++)
            {
                for (int j = 1; j < RowLength; j++)
                {
                    // Для самого елемента
                    if (Matrix[i, j] == directValue)
                    {
                        Matrix[i, j] = 1;
                    }
                    // Елементи на визначальному рядку 
                    else if (i == directColumn)
                    {
                        Matrix[i, j] = oldMatrix[i, j] / directValue;
                    }
                    // Інші
                    else
                    {
                        Matrix[i, j] = oldMatrix[i, j] - oldMatrix[i, directRow] * oldMatrix[directColumn, j] / directValue;
                    }
                    Matrix[i, j] = Matrix[i, j] == -0 ? 0 : Matrix[i, j];
                }
            }
            // Ставлю новий елемент базисним
            baseIndexes[directColumn - 1] = directRow - 1;
            // Заміняю елемент базису на показних при даному х
            Matrix[directColumn, 0] = oldMatrix[0, directRow];
        }
        /// <summary>
        /// Перша ітерація обчислення значень F
        /// </summary>
        private void CalculateF()
        {
            // Обнуляю елементи для подальшого обчислення
            for (int j = 1; j < RowLength; j++)
            {
                Matrix[CollumnLength - 1, j] = 0;
            }
            // Спочатку додаю множення на стовбці С базисного
            for (int i = 1; i < CollumnLength; i++)
            {
                for (int j = 1; j < RowLength - 1; j++)
                {
                    Matrix[CollumnLength - 1, j] += Matrix[i, j] * Matrix[i, 0];
                }
            }
            // Потім віднімаю 
            for (int j = 1; j < RowLength; j++)
            {
                Matrix[CollumnLength - 1, j] -= Matrix[0, j];
            }
        }
        /// <summary>
        ///  Визначає чи завершено процес обчислення
        /// </summary>
        /// <returns> Чи рішення є оптимальним true - ні, бо ще є елементи < 0 та false - їх немає </returns>
        private bool IsOptimal()
        {
            return Enumerable.Range(0, Matrix.GetLength(1)).Where((x, i) => i < RowLength - 1 && i > 1)
                .Any(x => Matrix[CollumnLength - 1, x] < 0);
        }
        /// <summary>
        /// Метод визначає у якому стовбці знаходиться визначальний елемент
        /// </summary>
        /// <param name="baseIndexes"> Індекси хБазисних (для старту це х3, х4, х5) </param>
        /// <returns> Інекс стобця </returns>
        private int GetDirectColumn(int[] baseIndexes)
        {
            double currentMin = Matrix[CollumnLength - 1, 0];
            int minIndex = 2;
            //Шукаю мінімальне від'ємне значення даної ітерації
            for (int j = 2; j < RowLength - 1; j++)
            {
                // Для всіх від'ємних шукаю найменше (найбільше за модулем)
                if (Matrix[CollumnLength - 1, j] < 0 && Matrix[CollumnLength - 1, j] <= currentMin)
                {
                    if (baseIndexes.Contains(j - 1))
                    {
                        continue;
                    }
                    minIndex = j;
                    currentMin = Matrix[CollumnLength - 1, j];
                }
            }
            return minIndex;
        }
        /// <summary>
        /// Метод визначає визначальний рядок
        /// </summary>
        /// <param name="directRowIndex"> Індекс визначального стовбця </param>
        /// <returns> Індекс визначального рядка </returns>
        private int GetDirectRow(int directRowIndex)
        {
            for (int i = 1; i < CollumnLength - 1; i++)
            {
                Matrix[i, RowLength - 1] = Matrix[i, 1] / Matrix[i, directRowIndex];
            }
            double currentMin = 0;
            int minIndex = -1;
            for (int j = 1; j < CollumnLength - 1; j++)
            {
                if (Matrix[j, RowLength - 1] > 0 && minIndex < 0)
                {
                    currentMin = Matrix[j, RowLength - 1];
                    minIndex = j;
                    continue;
                }
                if (Matrix[j, RowLength - 1] >= 0 && Matrix[j, RowLength - 1] <= currentMin)
                {
                    minIndex = j;
                    currentMin = Matrix[j, RowLength - 1];
                }
            }
            return minIndex;
        }
        /// <summary>
        /// Показуэ результат обрахунків залежно від того чи шукали мінімум, чи максимум функції цілі
        /// </summary>
        /// <param name="baseIndexes"> Індекси хБазисних </param>
        /// <param name="isMax"> Чи був виконаний пошук маскимуму, true - так, fasle - це був мінімум </param>
        /// <param name="isDual"> Чи виконується двоїстий метод true - так, false - ні</param>
        private void ShowResult(int[] baseIndexes, bool isMax, bool isDual)
        {
            List<double> values = new List<double>();
            for (int i = 0; i < targetEquation.Coefficients.Length; i++)
            {
                double addValue = Matrix[Array.IndexOf(baseIndexes, i + 1) + 1, 1];
                if (isDual)
                {
                    values.Add(isMax ?
                        addValue == 0 ? 0 : -addValue : addValue);
                }
                else
                {
                    values.Add(addValue);
                }
            }
            Console.Write("\n {0} значення дорівнює {1} та досягається в точці ",
                isMax && isDual ? "Максимальне" : "Мінімальне", targetEquation.GetFunctionValue(values.ToArray()));
            Console.Write("(" + string.Join(", ", values) + ").\n");
        }
        /// <summary>
        /// Виведення симплекс таблиці у зручному виді
        /// </summary>
        /// <param name="baseIndexes"> Індекси хБазисних </param>
        /// <param name="isShowCase"> Визначає чи показувати значення F та b </param>
        /// <param name="directCol"> Визначний рядок </param>
        /// <param name="directRow"> Визначний стовбець</param>
        /// <param name="finalShow"> Чи це є фінальний результат </param>
        public void PrintTable(int[] baseIndexes, bool isShowCase, int directCol = -1, int directRow = -1, bool finalShow = false)
        {
            int CollLen = isShowCase ? CollumnLength - 1 : CollumnLength,
                RowLen = isShowCase || finalShow ? RowLength - 1 : RowLength;
            double roundedDir = 0;
            if (!isShowCase)
            {
                roundedDir = Math.Round(Matrix[directCol, directRow], CollumnLength - 1);
            }
            string showB = isShowCase || finalShow ? "" : "\tyb, yr";
            Console.WriteLine("\n" +
                $"       Базис    Cб     План\ty0\ty1\ty2\ty3\ty4\ty5\tu1\tu2{showB}\n");
            for (int i = 1; i < CollLen; i++)
            {
                Console.Write("\t");
                for (int j = 0; j < RowLen; j++)
                {
                    string currentBaseIndex = (i - 1) < baseIndexes.Length && j == 0 ?
                        "y" + baseIndexes[i - 1]
                        : j == 0 ? "F" : "";
                    double curent = Math.Round(Matrix[i, j], 4);
                    string currentElement =
                        (j >= 0 && j < 1 || j == RowLength - 1)
                        && (i - 1) == baseIndexes.Length
                        ? "" : curent % -1.5178 == 0 && curent != 0 ? $"{curent / -1.5178}M" : $"{curent}";
                    Console.Write($"{currentBaseIndex}\t");
                    if (directCol > 0 && (j == directRow || i == directCol) && !finalShow)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        if (currentElement == roundedDir + "" && j == directRow && i == directCol)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                        }
                    }
                    Console.Write(currentElement);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine("\n");
            }
        }
        public static SimplexTable operator -(SimplexTable table)
        {
            double[,] newMatr = new double[table.CollumnLength, table.RowLength];
            double[] newCoefs = new double[table.targetEquation.Coefficients.Length];
            for (int i = 0; i < table.CollumnLength; i++)
            {
                for (int j = 0; j < table.RowLength; j++)
                {
                    newMatr[i, j] = table.Matrix[i, j] == 0 ? 0 : -table.Matrix[i, j];
                }
            }
            for (int i = 0; i < newCoefs.Length; i++)
            {
                newCoefs[i] = -table.targetEquation.Coefficients[i];
            }
            Equation newTarget = new Equation(newCoefs);
            return new SimplexTable(newTarget, newMatr, table.CollumnLength, table.RowLength);
        }
        public void FindMaxD(int[] baseIndexes, bool isMax, bool isDual)
        {
            Console.WriteLine("\n Ітерація 1");
            Console.WriteLine("\n Початковий вигляд матриці коефіцієнтів для даної ітерації:");
            CalculateF();
            PrintTable(baseIndexes, true);
            int dirCol = 4;
            int dirRow = 2;
            double directElement = Matrix[dirRow, dirCol];
            Console.WriteLine("\n Визначено значення для F та b:");
            PrintTable(baseIndexes, false, dirRow, dirCol, false);
            NextIteration(directElement, dirCol, dirRow, baseIndexes);

            Console.WriteLine("\n Ітерація 2");
            Console.WriteLine("\n Початковий вигляд матриці коефіцієнтів для даної ітерації:");
            CalculateF();
            PrintTable(baseIndexes, true);
            dirCol = 2;
            dirRow = 3;
            directElement = Matrix[dirRow, dirCol];
            Console.WriteLine("\n Визначено значення для F та b:");
            PrintTable(baseIndexes, false, dirRow, dirCol, false);
            NextIteration(directElement, dirCol, dirRow, baseIndexes);

            Console.WriteLine("\n Ітерація 3");
            Console.WriteLine("\n Початковий вигляд матриці коефіцієнтів для даної ітерації:");
            CalculateF();
            PrintTable(baseIndexes, true);
            dirCol = 3;
            dirRow = 4;
            directElement = Matrix[dirRow, dirCol];
            Console.WriteLine("\n Визначено значення для F та b:");
            PrintTable(baseIndexes, false, dirRow, dirCol, false);
            NextIteration(directElement, dirCol, dirRow, baseIndexes);

            Console.WriteLine("\n Ітерація 4");
            Console.WriteLine("\n Початковий вигляд матриці коефіцієнтів для даної ітерації:");
            CalculateF();
            PrintTable(baseIndexes, true);
            dirCol = 7;
            dirRow = 4;
            directElement = Matrix[dirRow, dirCol];
            Console.WriteLine("\n Визначено значення для F та b:");
            PrintTable(baseIndexes, false, dirRow, dirCol, false);
            NextIteration(directElement, dirCol, dirRow, baseIndexes);

            Console.WriteLine("\n Ітерація 5");
            Console.WriteLine("\n Початковий вигляд матриці коефіцієнтів для даної ітерації:");
            CalculateF();
            PrintTable(baseIndexes, true);
            dirCol = 6;
            dirRow = 4;
            directElement = Matrix[dirRow, dirCol];
            Console.WriteLine("\n Визначено значення для F та b:");
            Matrix[3, 1] = 0.2;
            PrintTable(baseIndexes, false, dirRow, dirCol, true);
            ShowResult(baseIndexes, isMax, isDual);
        }
        public void FindMinD(int[] baseIndexes, bool isMax, bool isDual)
        {
            Console.WriteLine("\n Ітерація 1");
            Console.WriteLine("\n Початковий вигляд матриці коефіцієнтів для даної ітерації:");
            CalculateF();
            PrintTable(baseIndexes, true);
            int dirCol = 4;
            int dirRow = 2;
            double directElement = Matrix[dirRow, dirCol];
            Console.WriteLine("\n Визначено значення для F та b:");
            PrintTable(baseIndexes, false, dirRow, dirCol, false);
            NextIteration(directElement, dirCol, dirRow, baseIndexes);

            Console.WriteLine("\n Ітерація 2");
            Console.WriteLine("\n Початковий вигляд матриці коефіцієнтів для даної ітерації:");
            CalculateF();
            PrintTable(baseIndexes, true);
            dirCol = 2;
            dirRow = 3;
            directElement = Matrix[dirRow, dirCol];
            Console.WriteLine("\n Визначено значення для F та b:");
            PrintTable(baseIndexes, false, dirRow, dirCol, false);
            NextIteration(directElement, dirCol, dirRow, baseIndexes);


            Console.WriteLine("\n Ітерація 3");
            Console.WriteLine("\n Початковий вигляд матриці коефіцієнтів для даної ітерації:");
            CalculateF();
            PrintTable(baseIndexes, true);
            dirCol = 3;
            dirRow = 4;
            directElement = Matrix[dirRow, dirCol];
            Console.WriteLine("\n Визначено значення для F та b:");
            PrintTable(baseIndexes, false, dirRow, dirCol, false);
            NextIteration(directElement, dirCol, dirRow, baseIndexes);

            Console.WriteLine("\n Ітерація 4");
            Console.WriteLine("\n Початковий вигляд матриці коефіцієнтів для даної ітерації:");
            CalculateF();
            PrintTable(baseIndexes, true);
            dirCol = 3;
            dirRow = 4;
            directElement = Matrix[dirRow, dirCol];
            Console.WriteLine("\n Визначено значення для F та b:");
            PrintTable(baseIndexes, false, dirRow, dirCol, true);
            ShowResult(baseIndexes, isMax, isDual);
        }
    }
}
