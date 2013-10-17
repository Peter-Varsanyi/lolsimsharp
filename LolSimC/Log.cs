using System;

namespace LolSimC
{
    internal static class Log
    {
        private static double _time;

        public static string MakeHealthBar(double hp, double maxhp)
        {
            string bar = "";
            double kocka = hp/maxhp*6;
            for (int i = 0; i < 6; i++)
            {
                if (i < kocka)
                {
                    bar += "#";
                }
                else
                {
                    bar += ".";
                }
                ;
            }
            return bar;
        }

        public static void ToLog(Base source, String type, string message)
        {
            if (type.Equals("skillchange"))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(Prefix() + " Skill changed in object: " + source.Name + " : " + message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        public static void ToLog(Base source, String type)
        {
            if (type.Equals("objectdelete"))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(Prefix() + " " + source.Name + " deleted, because its dead");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        public static string Prefix()
        {
            return "[" + String.Format("{0:00.00}", _time) + "]";
        }

        public static void ToLog(Base source, Base target, Damage damage, String type)
        {
            if (type.Equals("autoattackdamage"))
            {
                if (source.IsCreep())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Prefix() + " HP: " + String.Format("{0:0}/{1:0}", target.Health, target.MaxHealth) +
                                      " from " + source.Name + " (" + damage + ")");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(Prefix() + " [" + MakeHealthBar(target.Health, target.MaxHealth) + "] " +
                                      target.Name + " (" + damage + ")");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(Prefix() + " [" + MakeHealthBar(target.Health, target.MaxHealth) + "] " + target.Name +
                                  " Skill: (" + damage + ")");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        public static void ToLog(Base source, Base target, String type)
        {
            if (type.Equals("targetfind"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(Prefix() + " " + source.Name + " looking for target, found: " + target.Name);
                Console.ForegroundColor = ConsoleColor.Gray;
                return;
            }
        }

        public static void ToLog(String text)
        {
            Console.WriteLine(Prefix() + text);
        }

        public static void AddTime(double time2)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(Prefix() + " Adding time: " + time2);
            Console.ForegroundColor = ConsoleColor.Gray;

            _time += time2;
        }
    }
}