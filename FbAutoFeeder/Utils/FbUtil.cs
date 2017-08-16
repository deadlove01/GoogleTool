using FbAutoFeeder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FbAutoFeeder.Utils
{
    public class FbUtil
    {
        public static LoginFbInfo ConvertToFbInfo(string line)
        {
            string[] temp = line.Split('|');
            if(temp!=null)
            {
                return new  LoginFbInfo{
                    Email = temp[0],
                    Pass = temp[1]
                };
            }
            return null;
        }
    }
}
