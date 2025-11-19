using System.Text;
namespace Lab2_Exercise2
{
    internal class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            MyTime time = new MyTime(Input("Введіть години: "), Input("Введіть хвилини: "), Input("Введіть секунди: "));

            Console.WriteLine(
                """
                    ------------------------------------------------------------------------------------------------------------------------
                                                                         ВИБІР МЕТОДУ
                    ------------------------------------------------------------------------------------------------------------------------
                    1)  Створює об'єкт класу в форматі H:mm:s із заданих годин, хвилин і секунд.
                    2)  Створює об'єкт класу в форматі H:mm:s із заданих секунд.
                    3)  ToSecSinceMidnight перетворює час у кількість секунд, що пройшли від початку доби.
                    4)  AddOneSecond додає до часу одну секунду.
                    5)  AddOneMinute додає до часу одну хвилину.
                    6)  AddOneHour додає до часу одну годину.
                    7)  AddSeconds додає до часу вказану кількість секунд s.
                    8)  Difference вертає різницю між двома моментами.
                    9)  WhatLesson формує рядок згідно розкладу дзвінків.
                    ------------------------------------------------------------------------------------------------------------------------
                    0) Вийти з програми
                    ------------------------------------------------------------------------------------------------------------------------
                    """);

            byte choiceBlock;
            do
            {
                Console.Write("Оберіть метод: ");

                do
                {
                    if (!byte.TryParse(Console.ReadLine()!, out choiceBlock) || choiceBlock > 9)
                        Error();
                    else break;
                } while (true);


                if (choiceBlock != 0)
                {
                    switch (choiceBlock)
                    {
                        case 1:
                            time = new MyTime(Input("Введіть години: "), Input("Введіть хвилини: "), Input("Введіть секунди: "));
                            Console.WriteLine(time.ToString());
                            break;
                        case 2:
                            time = new MyTime(Input("Введіть секунди: "));
                            Console.WriteLine(time.ToString());
                            break;
                        case 3:
                            Console.Write("Результат виконання: ");
                            Console.WriteLine(time.ToSecSinceMidnight());
                            break;
                        case 4:
                            time.AddOneSecond();
                            Console.Write("Результат виконання: ");
                            Console.WriteLine(time.ToString());
                            break;
                        case 5:
                            time.AddOneMinute();
                            Console.Write("Результат виконання: ");
                            Console.WriteLine(time.ToString());
                            break;
                        case 6:
                            time.AddOneHour();
                            Console.Write("Результат виконання: ");
                            Console.WriteLine(time.ToString());
                            break;
                        case 7:
                            time.AddSeconds(Input("Введіть скільки секунд додати: "));
                            Console.Write("Результат виконання: ");
                            Console.WriteLine(time.ToString());
                            break;
                        case 8:
                            MyTime time2 = new MyTime(Input("Введіть години другого часу: "), Input("Введіть хвилини другого часу: "), Input("Введіть секунди другого часу: "));
                            Console.Write("Результат виконання: ");
                            Console.WriteLine(time.Difference(time2) + "c");
                            break;
                        case 9:
                            Console.Write("Результат виконання: ");
                            Console.WriteLine(time.WhatLesson());
                            break;
                    }
                }
            } while (choiceBlock != 0);
        }
        public static void Error() => Console.WriteLine("Помилка! Повторіть спробу!");
        public static int Input(string s)
        {
            do
            {
                Console.Write(s);
                if (int.TryParse(Console.ReadLine(), out int a)) return a;
                else Error();
            }
            while (true);
        }
    }
}
