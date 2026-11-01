using System;

namespace HostelSystem.UI
{
    public static class ConsoleHelper
    {
        public static void pause()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }


        public static string readText(string msg)
        {
            Console.Write(msg);
            return Console.ReadLine();
        }

        public static int readNum(string msg)
        {
            Console.Write(msg);
            int n;
            if (int.TryParse(Console.ReadLine(), out n))
            {
                return n;
            }
            return 0;
        }

        public static float readFloat(string msg)
        {
            Console.Write(msg);
            float n;
            if (float.TryParse(Console.ReadLine(), out n))
            {
                return n;
            }
            return 0;
        }

        public static void head(string title)
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine(title);
            Console.WriteLine("------------------------------------------------");
        }
    }
}
