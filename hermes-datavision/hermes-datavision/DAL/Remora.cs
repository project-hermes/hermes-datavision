using System;
using System.Collections.Generic;

namespace hermes_datavision.DAL
{
    public partial class Remora
    {
        public Remora()
        {
            RemoraRecords = new HashSet<RemoraRecord>();
        }

        public int RemoraId { get; set; }
        public DateTime CreationDate { get; set; }
        public string DeviceId { get; set; } = null!;
        public string DiveId { get; set; } = null!;
        public string Mode { get; set; } = null!;
        public int StartTime { get; set; }
        public double StartLat { get; set; }
        public double StartLng { get; set; }
        public int Freq { get; set; }
        public int EndTime { get; set; }
        public double EndLat { get; set; }
        public double EndLng { get; set; }

        public virtual ICollection<RemoraRecord> RemoraRecords { get; set; }
    }
}
