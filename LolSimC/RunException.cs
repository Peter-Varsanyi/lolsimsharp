using System;

namespace LolSimC
{
    internal class RunException : Exception
    {
        public RunException()
        {
        }

        public RunException(string message)
            : base(message)
        {
            Errormessage = message;
            Log.ToLog(message);
        }

        public string Errormessage { get; set; }
    }
}