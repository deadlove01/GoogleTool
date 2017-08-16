using Awesomium.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoogleTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            webControl1.Source = new Uri("https://plus.google.com/communities/103386775708787324155");
        }

        private void btnTranslate_Click(object sender, EventArgs e)
        {
            JSObject result = webControl1.ExecuteJavascriptWithResult(tbScript.Text.Trim());
            if(!result)
            {
                return;
            }
            result.InvokeAsync("click");
            Console.WriteLine(result);
           // webControl1.ExecuteJavascript("document.getElementById('source').value='Tôi là người Việt Nam'");
        }

        private void btnGetResult_Click(object sender, EventArgs e)
        {
            string result = webControl1.ExecuteJavascriptWithResult("document.getElementById('result_box').innerHTML");
            Console.WriteLine(result);
        }
    }
}
