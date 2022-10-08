using System;
using System.Collections.Generic;

namespace hermes_datavision.DAL
{
    public partial class RemoraRecord
    {
        public int RemoraRecordId { get; set; }
        public DateTime CreationDate { get; set; }
        public double Depth { get; set; }
        public double Degrees { get; set; }
        public double TimestampDouble { get; set; }
        public int Timestamp { get; set; }
        public int RemoraId { get; set; }

        public virtual Remora Remora { get; set; } = null!;
    }
}
