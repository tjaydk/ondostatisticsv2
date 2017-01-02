using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OndoStatisticsRedis.Exceptions
{
    public class OndoNotFoundException : Exception
    {
        public OndoNotFoundException()
        {

        }

        public OndoNotFoundException(string message) : base(message)
        {

        }

        public OndoNotFoundException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
