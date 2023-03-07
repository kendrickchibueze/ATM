using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.ATM_UI
{
    public class Utility
    {

        public static  void printDotAnimation(int timer = 10)
        {
            for (var x = 0; x < timer; x++)
            {
                PrintColorDot(ConsoleColor.Yellow, ".");

                Thread.Sleep(100);
            }
            Console.WriteLine();
        }



        public static void PrintColorMessage(ConsoleColor color, string message)
        {

            Console.ForegroundColor = color;

            
            Console.WriteLine(message);

            
            Console.ResetColor();
        }


        public static void PrintColorDot(ConsoleColor color, string message)
        {

            Console.ForegroundColor = color;


            Console.Write(message);


            Console.ResetColor();
        }
    }
}
