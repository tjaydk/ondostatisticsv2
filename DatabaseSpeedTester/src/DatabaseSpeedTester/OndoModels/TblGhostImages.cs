using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblGhostImages
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Heigth { get; set; }
        public int? Width { get; set; }
        public string Uri { get; set; }
    }
}
