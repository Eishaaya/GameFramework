using System;

namespace TestProj
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            //Console.WriteLine("Hacks!\0Hi!");

            using (var game = new Game1())
                game.Run();
        }
    }
}
