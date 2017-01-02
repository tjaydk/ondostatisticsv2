using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OndoStatisticsWebjob_2._0.Entities.clubEntities
{
    class ClubQuarterHistory
    {
        public ClubQuarterHistory(string quarterDate, int points)
        {
            this.quarterDate = quarterDate;
            this.points = points;
        }

        public string quarterDate { get; set; }
        public int points { get; set; }
    }
}
