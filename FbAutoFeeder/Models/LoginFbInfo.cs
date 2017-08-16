using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FbAutoFeeder.Models
{
    public class LoginFbInfo
    {
        public string Email { get; set; }
        public string Pass { get; set; }
        public string Cookie { get; set; }
        public string UserAgent { get; set; }
        public bool CheckPoint { get; set; }

        public override string ToString()
        {
            return $"{Email}|{Pass}|{Cookie}|{UserAgent}";
        }
    }
}
