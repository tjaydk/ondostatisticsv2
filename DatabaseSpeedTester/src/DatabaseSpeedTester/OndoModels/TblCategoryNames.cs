﻿using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblCategoryNames
    {
        public int Id { get; set; }
        public int? OndoId { get; set; }
        public string Name { get; set; }
        public int? Value { get; set; }
    }
}