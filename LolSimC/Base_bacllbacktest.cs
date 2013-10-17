using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LolSimC
{
    class Base_test
    {

        public delegate void unitDieHandler(string message);

        unitDieHandler unitdead;

        public void unitDie()
        {
            Console.WriteLine("im dead");
            if (unitdead != null)
            {
                unitdead("teszt");
            }
        }
        public void unitDie(unitDieHandler unit)
        {
            if (unit != null)
            {
                unit("aa");
            }
        }


        internal void registerCallback(unitDieHandler x)
        {
            unitdead = x;
        }
    }
}
