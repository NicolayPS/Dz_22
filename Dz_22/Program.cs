using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz_22
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите размерность массива : ");
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Func<Task<int[]>, int[]> func2 = new Func<Task<int[]>, int[]>(SumAndMaxArray);
            Task<int[]> task2 = task1.ContinueWith<int[]>(func2);

            Action<Task<int[]>> action = new Action<Task<int[]>>(PrintArray);
            Task task3 = task2.ContinueWith(action);

            task1.Start();
            Console.ReadKey();
        }

        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 100);
            }
            return array;
        }
        static int[] SumAndMaxArray(Task<int[]> task)
        {
            int[] array = task.Result;
            int s = 0;
            int max = array[0];
            for (int i = 0; i < array.Count(); i++)
            {
                s = s + array[i];
                if (array[i] > max)
                {
                    max = array[i];
                }
            }
            Console.WriteLine($"Сумма значений в массиве = {s} \nМаксимальное число в массиве = {max}");
            return array;

        }

        static void PrintArray(Task<int[]> task)
        {
            int[] array = task.Result;
            Console.Write("Массив: ");
            for (int i = 0; i < array.Count(); i++)
            {
                Console.Write($"{array[i]} ");
            }
        }
    }
}
