using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace OndoStatisticsRedis.Models.LoginModels
{
    public class Auth
    {
        [DefaultValue(false)]
        public bool type1 { get; set; }
        [DefaultValue(false)]
        public bool type2 { get; set; }
        [DefaultValue(false)]
        public bool type3 { get; set; }
    }
}
