using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPlusTool.Models
{
    public class Gmail
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string RecoveryEmail { get; set; }
        public bool IsUse { get; set; }
        public DateTime LastUsedTime { get; set; }
        public float UsingTime { get; set; }
        public string Note { get; set; }
    }
}
