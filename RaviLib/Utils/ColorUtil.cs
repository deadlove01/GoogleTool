using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaviLib.Utils
{
    public class ColorUtil
    {
        public static Color HexStrToColor(string hexStr)
        {

            return ColorTranslator.FromHtml(hexStr.Replace("###", "#").Replace("##", "#"));
        }

        public static string ColorToHexStr(Color color)
        {
            return ColorTranslator.ToHtml(color);
        }
        

        public static String ColorToRGBStr(System.Drawing.Color c)
        {
            return "RGB(" + c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString() + ")";
        }
        
    }
}
