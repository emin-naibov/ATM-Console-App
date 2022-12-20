using System;
using System.Text;
using System.Threading;

namespace ATM_Console_App
{
    public static class Interface
    {
        public static void ConsoleOperations(int etap)
        {
            Console.Clear();
            if (etap == 1)
            {
                Console.WriteLine("Wrong Number!!!");
            }
            else if (etap == 2)
            {
                Console.WriteLine("Wrong Pin!!!");
            }
            else if (etap == 3)
            {
                Console.WriteLine("Wrong input amount!!!");
            }
            Thread.Sleep(2000);
            Console.Clear();
        }
        public static string PassInput()
        {
            StringBuilder password = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                    break;

                if (key.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password.Remove(password.Length - 1, 1);
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    password.Append(key.KeyChar);
                    Console.Write("*");
                }
            }
            return password.ToString();
        }
        public static int MenuNavigation(string[] str, int cursor_begin, int cursor_end)
        {
            int choose = 0;
            Console.CursorVisible = false;
            while (true)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (choose == i) Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(str[i]);
                    Console.ResetColor();
                }
                Console.SetCursorPosition(cursor_begin, cursor_end);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        if (choose - 1 >= 0) choose--;
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        if (choose + 1 < str.Length) choose++;
                        break;
                    case ConsoleKey.Enter:
                        Console.CursorVisible = true;
                        Console.Clear();
                        return choose;
                }
            }
        }
    }
}
