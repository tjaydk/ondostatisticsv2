using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblOndoRestaurantRelation
    {
        public int Id { get; set; }
        public string RestaurantIdPoll { get; set; }
        public int OndoId { get; set; }
    }
}
