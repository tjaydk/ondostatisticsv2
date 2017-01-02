using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OndoStatisticsRedis.Models.LoginModels
{
    public class ListOndoIdForm
    {
        public List<OndoIdForm> ListOfOndos { get; set; }
        public int Result { get; set; }
        public string ResultString { get; set; }
    }
}
