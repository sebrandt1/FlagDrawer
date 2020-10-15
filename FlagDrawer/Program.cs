using System;
using FlagDrawer.Drawer;
using FlagDrawer.Utils;
using static FlagDrawer.Drawer.Flag;

namespace FlagDrawer
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();
            Console.ReadKey();
        }
        private static void Start()
        {
            bool run = true;

            while (run)
            {
                Flag flag;
                FlagType flagType = (FlagType)Menu.ShowMenu("What kind of flag would you like to create?", Enum.GetNames(typeof(FlagType)));

                Console.Clear();

                FlagDirection direction = FlagDirection.None;
                if(flagType != FlagType.Cross)
                {
                    direction = (FlagDirection)Menu.ShowMenu("What direction should the flag be?", new string[]
                        {
                            "Horizontal",
                            "Vertical"
                        });
                }

                Console.Clear();

                Console.WriteLine("Input flag size (ie 9 will be 9 rows & 9 * 2 columns)");
                int size;
                while (!(int.TryParse(Console.ReadLine(), out size)))
                {
                    Console.WriteLine("Invalid input, try again!");
                }

                Console.Clear();

                ConsoleColor[] colors = new ConsoleColor[0];

                switch(flagType)
                {
                    case FlagType.TriColor:
                        colors = AskForColors(3);
                        flag = new TriColorFlag(size, direction);
                        flag.SetColors(colors);
                        flag.DrawFlag();
                        break;

                    case FlagType.TwoColor:
                        colors = AskForColors(2);
                        flag = new TwoColorFlag(size, direction);
                        flag.SetColors(colors);
                        flag.DrawFlag();
                        break;

                    case FlagType.Cross:
                        colors = AskForColors(2);
                        flag = new CrossFlag(size);
                        flag.SetColors(colors);
                        flag.DrawFlag();
                        break;
                }

                Console.WriteLine();
                Console.ResetColor();
                int exit = Menu.ShowMenu("Do you want to draw another flag?", new string[]
                    {
                        "Yes",
                        "No",
                    });

                run = exit == 0;
                Console.Clear();
            }
        }

        public static ConsoleColor[] AskForColors(int colors)
        {
            ConsoleColor[] result = new ConsoleColor[colors];
            for(int i = 0; i < result.Length; i++)
            {
                string colorIndex = i == 0 ? "first" : "second";
                result[i] = (ConsoleColor)Menu.ShowMenu($"Select {colorIndex} color.", Enum.GetNames(typeof(ConsoleColor)));
                Console.Clear();
            }
            return result;
        }
    }
}
