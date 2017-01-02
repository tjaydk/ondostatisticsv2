using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OndoStatisticsRedis.Services
{
    public class LoginForm
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsTest { get; set; }
    }
}
