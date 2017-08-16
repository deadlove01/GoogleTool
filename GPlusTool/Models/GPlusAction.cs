using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPlusTool.Models
{
    public enum GPLUS_ACTIONS
    {
        PLUS_1_FEEDS =0,
        JOIN_COMMUNITY,
        FOLLOW_MEMBER,
        POST_IMAGE_HOME,
       // POST_IMAGE_HOME_COMMENT,
        POST_IMAGE_COMMUNITY,
      //  POST_COMMENT_HOME,
        POST_COMMENT_COMMUNITY,
        POST_LINK_LIST_COMMUNITY_LIST,
        COMBO_POST_LINK,
        COMBO_POST_IMAGE

    };

    public class SingleGPlusAction
    {
        public GPLUS_ACTIONS Action { get; set; }
        public int MinDelay { get; set; }
        public int MaxDelay { get; set; }
        public int Remains { get; set; }
        public Gmail User { get; set; }


        public override string ToString()
        {
            return String.Concat(Action, "|", MinDelay, "|", MaxDelay, "|", Remains, "|", User.Email);
        }
    }
    public class GPlusAction: ICloneable
    {
        public string ActionName { get; set; }
        public int MinDelay { get; set; }
        public int MaxDelay { get; set; }
        public GPLUS_ACTIONS Action { get; set; }
        public bool IsUse { get; set; }
        public int Times { get; set; }
        public GPlusAction()
        {
                
        }

        public GPlusAction(string actionName, int min, int max, GPLUS_ACTIONS act, int times)
        {
            this.ActionName = actionName;
            this.MinDelay = min;
            this.MaxDelay = max;
            this.Action = act;
            this.Times = times;
        }

        public override string ToString()
        {
            return String.Concat(ActionName, "|", MinDelay, "|", MaxDelay, "|", Action, "|", IsUse);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
