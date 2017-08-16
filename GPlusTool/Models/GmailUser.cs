using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPlusTool.Models
{
    public class GmailUser
    {
        public Gmail Gmail { get; set; }
        public List<GPlusAction> Actions { get; set; }

        public GmailUser()
        {

        }

        public GmailUser(Gmail gmail, List<GPlusAction> actions)
        {
            this.Gmail = gmail;
            Actions = new List<GPlusAction>();
            for (int i = 0; i < actions.Count; i++)
            {
                Actions.Add((GPlusAction)actions[i].Clone());
            }
        }

    }
}
