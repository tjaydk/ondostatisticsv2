using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OndoStatisticsRedis.Exceptions
{
    public class NotValidJsonException : Exception
    {
        public NotValidJsonException()
        {

        }

        public NotValidJsonException(string message) : base(message)
        {

        }

        public NotValidJsonException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
