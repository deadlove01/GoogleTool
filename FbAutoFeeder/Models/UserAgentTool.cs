using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FbAutoFeeder.Models
{
    public class UserAgentTool
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(UserAgentTool));

        private List<UserAgent> userAgentList;
        private Random random;
        public UserAgentTool()
        {
            userAgentList = new List<UserAgent>();
            random = new Random();
        }

        public UserAgent RandomUserAgent()
        {
            Init();
            if (userAgentList.Count > 0)
            {
                
                return userAgentList[random.Next(0, userAgentList.Count)];
               
            }

            return null;
        }
        public UserAgent RandomUserAgent(string deviceType, string osType, string agentType, string[] agentNames)
        {
            Init();
            if(userAgentList.Count > 0)
            {
                List<UserAgent> filteredUserAgent = new List<UserAgent>();
                for (int i = 0; i < userAgentList.Count; i++)
                {
                    var ua = userAgentList[i];
                    if(ua.device_type.ToLower() == deviceType.ToLower()
                        && ua.os_type.ToLower() == osType.ToLower()
                        && ua.agent_type.ToLower() == agentType.ToLower()
                        && agentNames.Contains(ua.agent_name.ToLower())
                        )
                    {
                        filteredUserAgent.Add(ua);
                    }
                }

                if(filteredUserAgent.Count > 0)
                {
                    return filteredUserAgent[random.Next(0, filteredUserAgent.Count)];
                }
                
            }

            return null;
        }

        public void Init()
        {
            if(userAgentList.Count == 0)
            {
                var jsonText = File.ReadAllText(Directory.GetCurrentDirectory() + "\\useragent.json");
                if(jsonText != null)
                {
                    userAgentList = JsonConvert.DeserializeObject<List<UserAgent>>(jsonText);
                    Console.WriteLine(userAgentList.Count);
                }
            }
        }
    }
}
