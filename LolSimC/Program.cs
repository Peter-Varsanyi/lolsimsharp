using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LolSimC
{
    class Program
    {
        static void Main(string[] args)
        {
            // entry point, creating a new amumu with standard route
            // TODO: imlement standard rotue
            var p = new LolSim("Amumu","Standard");
            p.Run();
            Console.ReadKey();
        }
    }
}
