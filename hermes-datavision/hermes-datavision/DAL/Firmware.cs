using System;
using System.Collections.Generic;

namespace hermes_datavision.DAL
{
    public partial class Firmware
    {
        public int FirmwareId { get; set; }
        public DateTime CreationDate { get; set; }
        public string VersionName { get; set; } = null!;
        public string VersionUrl { get; set; } = null!;
    }
}
