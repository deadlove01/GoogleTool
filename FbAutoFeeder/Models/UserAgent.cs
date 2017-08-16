using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FbAutoFeeder.Models
{
    public class UserAgent
    {
        public string agent_string { get; set; }
        public string agent_type { get; set; }
        public string agent_name { get; set; }
        public string os_type { get; set; }
        public string os_name { get; set; }
        public string device_type { get; set; }


        public override string ToString()
        {
            return $"device_type: {device_type}| os_type: {os_type}| agent_name: {agent_name}| agent_type: {agent_type}| agent_string: {agent_string}";
        }
    }
}
