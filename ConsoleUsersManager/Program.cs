using System;

namespace ConsoleUsersManager
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("? ( view / add / delete / update / getmgr / getclients )");
                string st = Console.ReadLine().Trim();
                if (!string.IsNullOrEmpty(st))
                    new InterfaceMgr().ConsoleInput(st);
            }
        }
    }
}