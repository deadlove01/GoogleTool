using Freebitcoin.Controller;
using Freebitcoin.Schedulers;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Freebitcoin
{
    public partial class FreeBitcoinForm : Form
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(FreeBitcoinForm));
        public FreeBitcoinForm()
        {
            InitializeComponent();

            XmlConfigurator.Configure();
        }
        DriverController ctrl;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ctrl = new DriverController();
                ctrl.Start();
                //SchedulerController.Instance.Start();
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error: {0}, Details: {1}", ex.Message, ex.StackTrace);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
 
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            SchedulerController.Instance.Dispose();
            Thread.Sleep(3000);
            btnGet.Enabled = true;
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            btnGet.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ctrl != null)
            {
                ctrl.Test(tbXpath.Text.Trim());
                Console.WriteLine("xong");
            }

            //string tempPath = Directory.GetCurrentDirectory() + "\\temp.png";
            //string desPath = Directory.GetCurrentDirectory() + "\\captcha.png";
            //Image img = Bitmap.FromFile(tempPath);
            //Rectangle rect = new Rectangle();
            ////610|830|300|150
            //int x = int.Parse(tbX.Text.Trim());
            //int y = int.Parse(tbY.Text.Trim());
            //int width = int.Parse(tbW.Text.Trim());
            //int height = int.Parse(tbH.Text.Trim()); 

            //// Create a rectangle using Width, Height and element location
            //rect = new Rectangle(x, y, width, height);

            //Console.WriteLine(rect.X + "|" + rect.Y + "|" + rect.Width + "|" + rect.Height);
            //// croping the image based on rect.
            //Bitmap bmpImage = new Bitmap(width, height);
            //using (Graphics gph = Graphics.FromImage(bmpImage))
            //{
            //    gph.DrawImage(img, new Rectangle(0, 0, bmpImage.Width, bmpImage.Height),
            //        rect
            //        , GraphicsUnit.Pixel);
            //}
            //pictureBox2.Image = bmpImage;
            ////var eImg = img.Clone(new Rectangle(element.Location, size), img.PixelFormat);
            //bmpImage.Save(desPath);
            ////var cropedImag = bmpImage.Clone(rect, bmpImage.PixelFormat);
            ////bmpImage.Dispose();

            //CaptchaController cc = new CaptchaController();
            //string result = string.Empty;
            //cc.SolveCatpcha(desPath, ref result);
           // Console.WriteLine("result: "+result);
        }

        private void FreeBitcoinForm_Load(object sender, EventArgs e)
        {
        }
    }
}
