using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OndoStatisticsRedis.Services
{
    public class TokenForm
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public bool IsTest { get; set; }
    }
}
